using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Test2.DTOs.Requests;
using Test2.DTOs.Responses;
// using Test2.DTOs.Requests;
// using Test2.DTOs.Responses;
using Test2.Entities;

namespace Test2.Services
{
    public interface IDbService
    {
        Task<IEnumerable<GetPetResponse>> ListPetsRegistered(int? year);

        Task<Pet> AddPet(AddPetRequest request);
    }
}