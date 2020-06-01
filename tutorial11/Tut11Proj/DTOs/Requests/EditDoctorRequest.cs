using System.Collections.Generic;
using Tut11Proj.Entities;

namespace Tut11Proj.DTOs.Requests
{
    public class EditDoctorRequest
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public virtual ICollection<Prescription> Precriptions { get; set; }
    }
}