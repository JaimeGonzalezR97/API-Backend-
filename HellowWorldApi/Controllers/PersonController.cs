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
        public PersonResponse post([FromBody] PersonDto person)
        { 

            PersonDto person1 = new PersonDto();
            person1 = person;
            Console.WriteLine(person1.Edad);
            people.Add(person1);
            return new PersonResponse()
            {
                Status = 200,
                Message = JsonSerializer.Serialize<PersonDto>(person1) 
            };
        }

        [HttpPut]
        [Route("{id}")]
        public PersonResponse put([FromRoute] int id, [FromBody] PersonDto person)
        {
            people.Add(jaime);
            people.Add(jaime);
            people.Add(jaime);
            PersonDto personFromId = people[id];
            Console.WriteLine($"{personFromId.Speak},{personFromId.Name},{personFromId.Edad},{personFromId.Estatura} this is the person from the id");

            personFromId = new PersonDto()
            {
                Speak = person.Speak,
                Name = person.Name,
                Edad = person.Edad,
                Estatura = person.Estatura
            };

            people[id] = personFromId;

            foreach (var person1 in people)
            {

                Console.WriteLine($"{person1.Speak},{person1.Name},{person1.Edad},{person1.Estatura} these are the people from the array");
            }

            return new PersonResponse()
            {
                Status = 200,
                Message = JsonSerializer.Serialize<PersonDto>(personFromId) //cambiar a objeto
            };
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
            people.Add(jaime);
            people.Add(jaime);
            people.Add(jaime);
            if (people[id] != null)
            {
                //people[id] = null;
                people.Remove(people[id]);
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