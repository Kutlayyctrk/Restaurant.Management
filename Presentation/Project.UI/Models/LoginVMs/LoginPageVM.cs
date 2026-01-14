using Project.Application.DTOs;

namespace Project.UI.Models.LoginVMs
{
    public class LoginPageVM
    {
        public string UserName { get; set; }

        public string Password { get; set; }
        public bool RememberMe { get; set; }

    }
}
