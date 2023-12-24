using System.Data;
using TestGeodanApi.DTO;
using TestGeodanApi.Models;

namespace TestGeodanApi.Interfaces
{
    public interface IPerson
    {
        public Person? CreatePerson(PersonCreateDto person);
        public List<Person>? ReadPerson(int? IdPerson);
        public Person? UpdatePerson(PersonUpdateDto person);
    }
}
