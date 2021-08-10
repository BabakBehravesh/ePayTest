using ApplicationCore;
using ePay.ApplicationCore.DTOs;
using ePay.ApplicationCore.Models;
using ePay.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.BLL;


namespace ePay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly ePayContext _context;

        public PersonController(ePayContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<IList<PersonDto>> GetAllPersonIntoDTo()
        {
            return await _context.Persons
                //.OrderBy(x => x.LastName).ThenBy(x => x.FirstName)
                .Select(x => PersonToPersonDTO(x))
                .ToListAsync();
        }


        private async Task<List<Person>> GetAllPerson()
        {
            return await _context.Persons
                //.OrderBy(x => x.LastName).ThenBy(x=>x.FirstName)
                .Select(x => x)

                .ToListAsync();
        }

 
        [HttpPost]
        public async Task<IList<PersonDto>> CreatePerson(List<PersonDto> personDtoList)
        {
            var personsInStorage = await GetAllPerson();
            // Delete to avoid dirty read by other processes. Another solution can be setting a read lock
            _context.Persons.RemoveRange(personsInStorage);
            await _context.SaveChangesAsync();

            foreach (var personDto in personDtoList)
            {

                // to be moved to BLL
                bool isAllFieldsSupplied = IsAllFieldsSupplied(personDto);
                bool isAgeValid = IsAgeValid(personDto);
                bool isNewId = IsNewId(personDto, personsInStorage);

                if (isAllFieldsSupplied == false
                    || isAgeValid == false
                    || isNewId == false)
                    continue; // ignore the invalid input and go for the next input

                // to be using DI container to inject dependency
                var person = new Person
                {
                    Id = personDto.Id, 
                    FirstName = personDto.FirstName, // to be given by an algo- implemented in simulator CoreAnswers/CoreFunctionality_ItemInsert
                    LastName = personDto.LastName, // to be given by an algo- implemented in simulator CoreAnswers/CoreFunctionality_ItemInsert
                    Age = personDto.Age  // to be given by an algo
                };

                // The order of items in peresonsInStorage is ordered by LastName and then the firstName but when it persited on DB it automatically sorted by Id.
                InsertionService.Insert(person, personsInStorage);                
            }
            await _context.EnableIdentityInsert<Person>();

            // [TODO]the code to be moved to repository - repository pattern to be implemented 
            _context.Persons.AddRange(personsInStorage);
            _context.SaveChangesWithIdentityInsert<Person>();

            return await GetAllPersonIntoDTo();
        }



        private static PersonDto PersonToPersonDTO(Person person) =>
            new PersonDto
            {
                Id = person.Id,
                FirstName = person.FirstName,
                LastName = person.LastName
            };



 

        ////////////Validation section

        private bool IsAllFieldsSupplied(PersonDto personDto)
        {
            if (String.IsNullOrEmpty(personDto.FirstName)
                || String.IsNullOrEmpty(personDto.LastName)
                || personDto.Age < 0)

                return false;
            else
                return true;
        }

        private bool IsAgeValid(PersonDto personDto)
        {
            if (personDto.Age < 18)

                return false;
            else
                return true;
        }

        private bool IsNewId(PersonDto personDto, List<Person> personsInStorage)
        {
            var num = personsInStorage.Where(p=> p.Id== personDto.Id).Count();
            if (num == 0)
                return true;
            else
                return false;
        }

    }
}
