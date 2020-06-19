using System.Xml.Linq;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using Test2.Entities;
using Test2.DTOs.Responses;
using Test2.DTOs.Requests;

namespace Test2.Services
{
    public class SqlServerDbService : IDbService
    {
        private readonly s18827DbContext _context;

        public SqlServerDbService(s18827DbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<GetPetResponse>> ListPetsRegistered(int? year)
        {
            IEnumerable<GetPetResponse> respList = null;
            if (year == null) respList = await _context.Pets.Select(p => new GetPetResponse
            {
                IdPet = p.IdPet,
                IdBreedType = p.IdBreedType,
                Name = p.Name,
                IsMale = p.IsMale,
                DateRegistered = p.DateRegistered,
                ApproxDateOfBirth = p.ApproxDateOfBirth,
                DateAdopted = p.DateAdopted,
                VolunteerList = _context.Volunteers_Pets.Select(v_p => new GetVolunteerResponse
                {
                    Name = _context.Volunteers.Where(v => v.IdVolunteer == v_p.IdVolunteer).Select(v => v.Name).FirstOrDefault(),
                    Surname = _context.Volunteers.Where(v => v.IdVolunteer == v_p.IdVolunteer).Select(v => v.Surname).FirstOrDefault(),
                    Phone = _context.Volunteers.Where(v => v.IdVolunteer == v_p.IdVolunteer).Select(v => v.Phone).FirstOrDefault()
                }).ToList()
            }).ToListAsync();
            else
            {
                respList = await _context.Pets.Where(p => p.DateRegistered.Year == year).Select(p => new GetPetResponse
                {
                    IdPet = p.IdPet,
                    IdBreedType = p.IdBreedType,
                    Name = p.Name,
                    IsMale = p.IsMale,
                    DateRegistered = p.DateRegistered,
                    ApproxDateOfBirth = p.ApproxDateOfBirth,
                    DateAdopted = p.DateAdopted,
                    VolunteerList = _context.Volunteers_Pets.Select(v_p => new GetVolunteerResponse
                    {
                        Name = _context.Volunteers.Where(v => v.IdVolunteer == v_p.IdVolunteer).Select(v => v.Name).FirstOrDefault(),
                        Surname = _context.Volunteers.Where(v => v.IdVolunteer == v_p.IdVolunteer).Select(v => v.Surname).FirstOrDefault(),
                        Phone = _context.Volunteers.Where(v => v.IdVolunteer == v_p.IdVolunteer).Select(v => v.Phone).FirstOrDefault()
                    }).ToList()
                }).ToListAsync();
            }
            if (respList.ElementAt(0) == null) throw new ArgumentNullException();
            return respList;
        }

        public async Task<Pet> AddPet(AddPetRequest request)
        {
            Pet newPet = null;
            var breedTypeExists = await _context.BreedTypes.Where(bt => bt.Name == request.BreedName).FirstOrDefaultAsync();

            var idBreedType = breedTypeExists.IdBreedType;
            if (breedTypeExists == null) // create new breed
            {
                var newBreedType = new BreedType
                {
                    IdBreedType = await _context.BreedTypes.MaxAsync(bt => bt.IdBreedType) + 1,
                    Name = request.BreedName
                };
                idBreedType = newBreedType.IdBreedType;
                _context.Entry(newBreedType).State = EntityState.Added;
                await _context.SaveChangesAsync();
            }
            newPet = new Pet
            {
                IdPet = await _context.Pets.MaxAsync(p => p.IdPet) + 1,
                IdBreedType = idBreedType,
                Name = request.Name,
                IsMale = request.IsMale,
                DateRegistered = request.DateRegistered,
                ApproxDateOfBirth = request.ApproxDateOfBirth
            };
            _context.Entry(newPet).State = EntityState.Added;
            await _context.SaveChangesAsync();

            if (newPet == null) throw new ArgumentException();
            return newPet;
        }

    }
}