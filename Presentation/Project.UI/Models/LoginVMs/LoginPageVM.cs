using Project.Application.DTOs;
using System.ComponentModel.DataAnnotations;

namespace Project.UI.Models.LoginVMs
{
    public class LoginPageVM
    {
        [Required (ErrorMessage = "Kullanıcı adı gereklidir.")]  
        public string UserName { get; set; }
        [Required (ErrorMessage = "Parola gereklidir.")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }

    }
}
