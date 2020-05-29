using System;
using System.Collections.Generic;

namespace Tut10Proj.Entities
{
    public partial class User
    {
        public int IdUser { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string PasswordSalt { get; set; }
        public string Role { get; set; }
        public string RefreshToken { get; set; }
    }
}
