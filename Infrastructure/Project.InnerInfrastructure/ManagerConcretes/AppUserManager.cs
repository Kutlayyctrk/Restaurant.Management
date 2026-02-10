using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Project.Application.DTOs;
using Project.Application.Enums;  
using Project.Application.MailService;
using Project.Application.Managers;
using Project.Application.Results;
using Project.Contract.Repositories;
using Project.Domain.Entities.Concretes;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Project.InnerInfrastructure.ManagerConcretes
{
    public class AppUserManager : BaseManager<AppUser, AppUserDTO>, IAppUserManager
    {
        private readonly IMapper _mapper;
        private readonly IValidator<AppUserDTO> _appUserValidator;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IMailSender _mailSender;
        private readonly IConfiguration _configuration;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IAppUserRepository _appUserRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AppUserManager(
            IAppUserRepository appUserRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IValidator<AppUserDTO> appUserValidator,
            UserManager<AppUser> userManager,
            RoleManager<AppRole> roleManager,
            IMailSender mailSender,
            IConfiguration configuration,
            SignInManager<AppUser> signInManager,
            ILogger<AppUserManager> logger
        ) : base(appUserRepository, unitOfWork, mapper, appUserValidator, logger)
        {
            _mapper = mapper;
            _appUserValidator = appUserValidator;
            _userManager = userManager;
            _roleManager = roleManager;
            _mailSender = mailSender;
            _configuration = configuration;
            _signInManager = signInManager;
            _appUserRepository = appUserRepository;
            _unitOfWork = unitOfWork;
        }

        public override async Task<Result> CreateAsync(AppUserDTO dto)
        {
            ValidationResult validationResult = await _appUserValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                List<string> errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return Result.Failure(OperationStatus.ValidationError, errors);
            }

            AppUser user = _mapper.Map<AppUser>(dto);
            user.InsertedDate = DateTime.Now;
            user.Status = Project.Domain.Enums.DataStatus.Inserted;

            IdentityResult createResult = await _userManager.CreateAsync(user, dto.Password);
            if (!createResult.Succeeded)
                return Result.Failure(OperationStatus.Failed, "Kullanıcı oluşturulamadı.");

            if (dto.RoleIds is { Count: > 0 })
            {
                foreach (int roleId in dto.RoleIds)
                {
                    AppRole role = await _roleManager.FindByIdAsync(roleId.ToString());
                    if (role != null)
                    {
                        IdentityResult addToRoleResult = await _userManager.AddToRoleAsync(user, role.Name);
                        if (!addToRoleResult.Succeeded)
                            return Result.Failure(OperationStatus.Failed, "Rol atanamadı.");
                    }
                }
            }

            if (!string.IsNullOrWhiteSpace(user.Email) && _mailSender != null)
            {
                string baseUrl = _configuration["AppUrl:BaseUrl"];
                string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                string encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
                string encodedUserId = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(user.Id.ToString()));

                string activationLink = $"{baseUrl}/LoginAndRegister/ConfirmEmail?userId={encodedUserId}&token={encodedToken}";
                await _mailSender.SendActivationMailAsync(user.Email, activationLink);
            }

            return Result.Succeed("Kullanıcı oluşturuldu.");
        }
        public async Task<List<AppUserDTO>> GetConfirmedUsersAsync()
        {
          
            List<AppUser> confirmedUsers = await _appUserRepository.WhereAsync(u => u.EmailConfirmed);

        
            return _mapper.Map<List<AppUserDTO>>(confirmedUsers);
        }

        public override async Task<Result> HardDeleteByIdAsync(int id)
        {
            AppUser user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
                return Result.Failure(OperationStatus.NotFound, "Kullanıcı bulunamadı.");

            if (user.Status != Project.Domain.Enums.DataStatus.Deleted)
                return Result.Failure(OperationStatus.Failed, "Önce soft delete yapılmalıdır.");

            IdentityResult identityResult = await _userManager.DeleteAsync(user);
            if (!identityResult.Succeeded)
                return Result.Failure(OperationStatus.Failed, "Kullanıcı silinemedi.");

            return Result.Succeed("Kullanıcı silindi.");
        }

        public async Task<Result> LoginAsync(AppUserDTO dto)
        {
            ValidationResult validationResult = await _appUserValidator.ValidateAsync(dto,
                options => options.IncludeRuleSets("Login"));
            if (!validationResult.IsValid)
            {
                List<string> errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return Result.Failure(OperationStatus.ValidationError, errors);
            }

            AppUser user = await _userManager.FindByNameAsync(dto.UserName);
            if (user == null)
                return Result.Failure(OperationStatus.NotFound, "Kullanıcı bulunamadı.");

            if (!user.EmailConfirmed)
                return Result.Failure(OperationStatus.Failed, "E-posta onaylanmamış.");

            SignInResult signInResult = await _signInManager.PasswordSignInAsync(user, dto.Password, false, false);
            if (!signInResult.Succeeded)
                return Result.Failure(OperationStatus.Failed, "Geçersiz kullanıcı adı veya şifre.");

            IList<string> roles = await _userManager.GetRolesAsync(user);
            if (roles == null || roles.Count == 0)
                return Result.Failure(OperationStatus.Failed, "Kullanıcıya atanmış rol bulunamadı.");

            return Result.Succeed("Giriş başarılı.");
        }

        public async Task<Result> ConfirmEmailAsync(string encodedUserId, string encodedToken)
        {
            string decodedUserId = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(encodedUserId));
            string decodedToken = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(encodedToken));

            AppUser user = await _userManager.FindByIdAsync(decodedUserId);
            if (user == null)
                return Result.Failure(OperationStatus.NotFound, "Kullanıcı bulunamadı.");

            IdentityResult result = await _userManager.ConfirmEmailAsync(user, decodedToken);
            if (result.Succeeded)
                return Result.Succeed("E-posta onaylandı.");

            return Result.Failure(OperationStatus.Failed, "E-posta onaylanamadı.");
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}