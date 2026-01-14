using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Project.Application.DTOs;
using Project.Application.MailService;
using Project.Application.Managers;
using Project.Contract.Repositories;
using Project.Domain.Entities.Concretes;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using Microsoft.EntityFrameworkCore.Update;



namespace Project.InnerInfrastructure.ManagerConcretes
{


    public class AppUserManager(IAppUserRepository appUserRepository, IMapper mapper, IValidator<AppUserDTO> appUserValidator, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, IMailSender mailSender, IConfiguration configuration,SignInManager<AppUser> signInManager) : BaseManager<AppUser, AppUserDTO>(appUserRepository, mapper, appUserValidator), IAppUserManager
    {

        
        private readonly IMapper _mapper = mapper;
        private readonly IValidator<AppUserDTO> _appUserValidator = appUserValidator;
        private readonly UserManager<AppUser> _userManager = userManager;
        private readonly RoleManager<AppRole> _roleManager = roleManager;
        private readonly IMailSender _mailSender = mailSender;
        private readonly IConfiguration _configuration = configuration;
        private readonly SignInManager<AppUser> _signInManager =signInManager;
        public override async Task<string> CreateAsync(AppUserDTO dto)
        {
            ValidationResult validationResult = await _appUserValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                return string.Join("|", validationResult.Errors.Select(x => x.ErrorMessage));
            }

            AppUser user = _mapper.Map<AppUser>(dto);
            user.InsertedDate = DateTime.Now;
            user.Status = Domain.Enums.DataStatus.Inserted;

            IdentityResult createResult = await _userManager.CreateAsync(user, dto.Password);
            if (!createResult.Succeeded)
            {
                return string.Join("|", createResult.Errors.Select(x => x.Description));
            }
            if (dto.RoleIds is { Count: > 0 })
            {
                foreach (int roleId in dto.RoleIds)
                {
                    AppRole role = await _roleManager.FindByIdAsync(roleId.ToString());
                    if (role != null)
                    {
                        IdentityResult addToRoleResult = await _userManager.AddToRoleAsync(user, role.Name);
                        if (!addToRoleResult.Succeeded)
                        {
                            return string.Join("|", addToRoleResult.Errors.Select(x => x.Description));
                        }
                    }
                }
            }

            if (!string.IsNullOrWhiteSpace(user.Email) && _mailSender != null)
            {
                string baseUrl = _configuration["AppUrl:BaseUrl"]; //Baseurl'i appsettingsden alıyoruz.
                string tokenn = await _userManager.GenerateEmailConfirmationTokenAsync(user); //Email onay tokenı oluşturuluyor.

                string encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(tokenn)); //Token base64 ile encode ediliyor.
                string encodedUserId = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(user.Id.ToString())); //UserId base64 ile encode ediliyor. Bozulma durumunu engellemek için encode ettim.

                string activationLink = $"{baseUrl}/api/AppUser/ConfirmEmail?userId={encodedUserId}&token={encodedToken}"; // TODO: Buradaki controller  ve action ismi düzeltilecek Controller yaratıldıktan sonra.
                await _mailSender.SendActivationMailAsync(user.Email, activationLink);
            }
            return "Kullanıcı Başarıyla Oluşturuldu.";
        }

        public override async Task<string> HardDeleteAsync(int id)
        {
            AppUser user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return $"Kullanıcı bulunamadı";
            }
            if (user.Status != Domain.Enums.DataStatus.Deleted)
            {
                return "Kullanıcı silinmeden önce pasif duruma getirilmelidir.";
            }

            IdentityResult ıdentityResult = await _userManager.DeleteAsync(user);
            if (!ıdentityResult.Succeeded)
            {
                return string.Join("|", ıdentityResult.Errors.Select(x => x.Description));
            }
            return "Kullanıcı başarıyla veritabanından silindi.";
        }

        public async Task<string> LoginAsync(AppUserDTO dto)
        {
          ValidationResult validationResult= await _appUserValidator.ValidateAsync(dto);
            if(!validationResult.IsValid)
            {
                return string.Join("|", validationResult.Errors.Select(x => x.ErrorMessage));
            }

            AppUser user= await _userManager.FindByNameAsync(dto.UserName);
            if(user==null)
            {
                return "Geçersiz email veya şifre.";
            }

            SignInResult signInResult= await _signInManager.PasswordSignInAsync(user,dto.Password,isPersistent:false,lockoutOnFailure:false);
            if(!signInResult.Succeeded)
            {
                return "Geçersiz email veya şifre.";
            }


            var roles = await _userManager.GetRolesAsync(user);
            if(roles==null || roles.Count==0)
            {
                return "Kullanıcının rolü bulunmamaktadır.";
            }
            return "Giriş başarılı|"+string.Join(",",roles);
        }

    }
}
