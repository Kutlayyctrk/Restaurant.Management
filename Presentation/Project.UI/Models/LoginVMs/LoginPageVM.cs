using Project.Application.DTOs;

namespace Project.UI.Models.LoginVMs
{
    public class LoginPageVM
    {
        public AppUserDTO User { get; set; }
        public string ErrorMessage { get; set; }
        public bool RememberMe { get; set; }

    }
}
