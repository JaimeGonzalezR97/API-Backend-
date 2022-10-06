using HellowWorldApi.Data;
using HellowWorldApi.Data.Entities;
using HellowWorldApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        [HttpGet("personHellowWorld")]
        public async Task<List<Person>> get()
        {
            return await _libraryDbContext.Persons.ToListAsync();
        }

        [HttpPost]
        public PersonResponse post([FromBody] Person person)
        { 
            _libraryDbContext.Persons.Add(person);
            _libraryDbContext.SaveChanges();
            Console.WriteLine(person.Name);
          
            return  new PersonResponse()
            {
                Status = 200,
                Message = JsonSerializer.Serialize<Person>(person) 
            };
        }

        [HttpPut]
        [Route("{id}")]
        public PersonResponse put([FromRoute] int id, [FromBody] Person person)
        {
            var modify = _libraryDbContext.Persons.Find(id);
            if (modify != null) 
            {
                modify.Name = person.Name;
                modify.Speak = person.Speak;
                modify.Estatura = person.Estatura;
                modify.Edad = person.Edad;
                _libraryDbContext.Entry(modify).State = EntityState.Modified;
                _libraryDbContext.SaveChanges();

                return new PersonResponse()
                {
                    Status = 200,
                    Message = JsonSerializer.Serialize<Person>(person) //cambiar a objeto
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
        public PersonResponse patch([FromRoute] int id, [FromBody] string speak)
        {// recibir array de propiedades a modificar 
            people.Add(jaime);
            people.Add(jaime);
            people.Add(jaime);
            Console.WriteLine($"{people[id].Speak}, this is the property from the id");

            people[id].Speak = speak;

            Console.WriteLine($"{people[id].Speak}, this is the  property that was updated from the id");

            return new PersonResponse()
            {
                Status = 200,
                Message = JsonSerializer.Serialize<PersonDto>(people[id])
            };
        }

        [HttpDelete]
        [Route("{id}")]
        public PersonResponse delete([FromRoute] int id)
        {
            var deleted = _libraryDbContext.Persons.Find(id);
            if (deleted != null)
            {
                //people[id] = null;
                _libraryDbContext.Persons.Remove(deleted);
                _libraryDbContext.SaveChanges();
                return new PersonResponse
                {
                    Status=200,
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