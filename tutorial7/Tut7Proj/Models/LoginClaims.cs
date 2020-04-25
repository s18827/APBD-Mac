using System.Collections.Generic;

namespace Tut7Proj.Models
{
    public class LoginClaims
    {
        public string NameIdentifier { get; set; }
        public string Name { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}