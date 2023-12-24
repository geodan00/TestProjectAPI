using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Data;
using TestGeodanApi.DTO;
using TestGeodanApi.Interfaces;
using TestGeodanApi.Models;

namespace TestGeodanApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : Controller
    {

        private readonly IPerson sPerson;

        public PersonController(IPerson iPerson)
        {
            sPerson = iPerson;
        }
        [HttpPost]
        public async Task<ActionResult<Person>> CreatePerson([Required][FromBody] PersonCreateDto person)
        {
            try
            {
                Person data = sPerson.CreatePerson(person);
                if (data == null)
                {
                    return BadRequest();
                }
                return data;
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> GetPerson(int id)
        {
            try
            {

                Person? person = sPerson.ReadPerson(id).First();
                if (person == null)
                {
                    return NotFound();
                }

                return person;
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<Person>>> GetAllPerson()
        {
            try
            {
                List<Person>? persons = sPerson.ReadPerson(null);
                if (persons == null)
                {
                    return NotFound();
                }

                return persons;
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult<Person>> UpdatePerson([Required][FromBody] PersonUpdateDto person)
        {
            try
            {
                Person data = sPerson.UpdatePerson(person);
                if (data == null)
                {
                    return BadRequest();
                }
                return data;
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
