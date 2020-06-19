using System;

namespace Test2.DTOs.Requests
{
    public class AddPetRequest
    {
        /*
{
"BreedName": "Australian Cattle Dog",
"Name": "Max",
"IsMale: "1",
"DateRegistered": "2020-02-02 07:07:12", "ApprocimatedDateOfBirth: "2020-02-02 07:07:12"
}
        */

        public string BreedName { get; set; }
        public string Name { get; set; }
        public bool IsMale { get; set; }
        public DateTime DateRegistered { get; set; }
        public DateTime ApproxDateOfBirth { get; set; }
    }
}