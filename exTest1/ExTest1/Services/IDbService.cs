using System.Collections.Generic;
using ExTest1.DTOs.Requests;
using ExTest1.DTOs.Responses;
using ExTest1.Models;

namespace ExTest1.Services
{
    public interface IDbService
    {
        GetAnimalsResponse GetAnimals(string sortBy);

        AddAnimalResponse AddAnimal(AddAnimalRequest request);



        // --------------------------------------------
        // void SaveLogData(IEnumerable<string> data);
    }
}