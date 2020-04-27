using System.Collections.Generic;

namespace Tut7Proj.Models
{
    public class LoginModel
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } // for simplification user can have only 1 role for now and he himself assgns it when logging in
        // public IEnumerable<string> Roles { get; set;} // for this I'll have to add 2 tables to db (roles and user_roles (associatie entity))
    }
}