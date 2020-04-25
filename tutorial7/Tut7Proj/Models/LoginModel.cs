namespace Tut7Proj.Models
{
    public class LoginModel
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public LoginClaims LoginClaims { get; set; }
    }
}