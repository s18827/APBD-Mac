using System.Collections.Generic;

namespace Tut7Proj.DTOs.Requests
{
    public class LoginRequest
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}