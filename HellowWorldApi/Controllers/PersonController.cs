using HellowWorldApi.Data;
using HellowWorldApi.Data.Entities;
using HellowWorldApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text.Json;

namespace HellowWorldApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : Controller
    {
        private readonly LibraryDbContext _libraryDbContext;
        public PersonController(LibraryDbContext libraryDbContext)
        {
            _libraryDbContext = libraryDbContext;
        }
        List<PersonDto> people = new List<PersonDto>();
        PersonDto jaime = new PersonDto()
        {
            Speak = "Hellow world",
            Name = "Jaime",
            Edad = 22,
            Estatura = 1.80
        };

        [HttpGet]
        public async Task<List<Person>> get()
        {
            return await _libraryDbContext.Persons.ToListAsync();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<PersonResponse> getById([FromRoute] int id)
        {
            Person personFromDb = await _libraryDbContext.Persons.FindAsync(id);
            if (personFromDb != null)
            {
                return new PersonResponse()
                {
                    Status = 200,
                    Message = "This person has been successfully retrieved",
                    Data = personFromDb
                };
            }
            else
            {
                return new PersonResponse()
                {
                    Status = 400,
                    Message = "This person does not exist"
                };
            }
            
        }


        [HttpPost]
        public async Task<PersonResponse> post([FromBody] PersonDto personDto)
        {
            Person person = new Person()
            {
                Name = personDto.Name,
                Speak = personDto.Speak,
                Edad = personDto.Edad,
                Estatura = personDto.Estatura,
            };
            await _libraryDbContext.Persons.AddAsync(person);
            await _libraryDbContext.SaveChangesAsync();
            Console.WriteLine(person.Name);
          
            return new PersonResponse()
            {
                Status = 200,
                Message = "This person has been successfully saved",
                Data = person
            };
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<PersonResponse> put([FromRoute] int id, [FromBody] PersonDto personDto)
        {
            Person personFromDb = await _libraryDbContext.Persons.FindAsync(id);
            if (personFromDb != null) 
            {
                personFromDb.Name = personDto.Name;
                personFromDb.Speak = personDto.Speak;
                personFromDb.Estatura = personDto.Estatura;
                personFromDb.Edad = personDto.Edad;
                _libraryDbContext.Entry(personFromDb).State = EntityState.Modified;
                await _libraryDbContext.SaveChangesAsync();

                return new PersonResponse()
                {
                    Status = 200,
                    Message = $"This person with id {id} was successfully changed",
                    Data = personFromDb
                };
            }
            else
            {
                return new PersonResponse
                {
                    Status = 400,
                    Message = $"Person with id {id} does not exist"
                };
            }
        }

        [HttpPatch]
        [Route("{id}")]
        public async Task<PersonResponse> patch([FromRoute] int id, [FromBody] PersonPatchDto personPatchDto)
        {// recibir array de propiedades a modificar 
            Person personFromDb = await _libraryDbContext.Persons.FindAsync(id);

            if(personFromDb != null)
            {
                personFromDb.Name = personPatchDto.Name;
                personFromDb.Speak = personPatchDto.Speak;
                _libraryDbContext.Entry(personFromDb).State = EntityState.Modified;
                await _libraryDbContext.SaveChangesAsync();

                return new PersonResponse
                {
                    Status = 200,
                    Message = $"Person with id {id} succesfully change",
                    Data = personFromDb
                };
            }
            else
            {
                return new PersonResponse
                {
                    Status = 400,
                    Message = $"Person with id {id} does not exist"
                };
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<PersonResponse> delete([FromRoute] int id)
        {
            Person deleted = await _libraryDbContext.Persons.FindAsync(id);
            if (deleted != null)
            {
                //people[id] = null;
                _libraryDbContext.Persons.Remove(deleted);
                await _libraryDbContext.SaveChangesAsync();
                return new PersonResponse
                {
                    Status = 200,
                    Message = $"Person with id {id} successfully deleted"
                };
            }
            else
            {
                return new PersonResponse
                {
                    Status = 400,
                    Message = $"Person with id {id} does not exist"
                };
            }
        }  
    }
}