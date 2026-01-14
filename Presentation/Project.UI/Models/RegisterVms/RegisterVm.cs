using System.ComponentModel.DataAnnotations;

namespace Project.UI.Models.RegisterVms
{
    public class RegisterVm
    {
        [Required(ErrorMessage = "Kullanıcı adı zorunludur.")]
        [MinLength(5, ErrorMessage = "Kullanıcı adı en az 5 karakter olmalıdır.")]
        [MaxLength(25, ErrorMessage = "Kullanıcı adı en fazla 25 karakter olmalıdır.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email alanı zorunludur.")]
        [EmailAddress(ErrorMessage = "Geçerli bir email adresi giriniz.")]
        [MaxLength(30, ErrorMessage = "Email en fazla 30 karakter olmalıdır.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Email tekrar alanı zorunludur.")]
        [Compare("Email", ErrorMessage = "Email alanları uyuşmuyor.")]
        public string ConfirmedEmail { get; set; }

        [Required(ErrorMessage = "Şifre zorunludur.")]
        [MinLength(6, ErrorMessage = "Şifre en az 6 karakter olmalıdır.")]
        public string Password { get; set; }

        public int Role { get; set; } = 11;
    }
}