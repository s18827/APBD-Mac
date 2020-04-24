using System.Collections;
using System.Collections.Generic;
using ExTest1.Models;
using ExTest1.Services;


namespace ExTest1.DTOs.Responses
{
    public class GetAnimalsResponse
    {
        public IEnumerable<AnimalFoundTemplate> List {get; set; }
    }
}