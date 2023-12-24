using System.Data;
using TestGeodanApi.DTO;
using TestGeodanApi.Models;

namespace TestGeodanApi.Interfaces
{
    public interface IDbConnexion
    {
        public DataSet CreatePerson(string name, string by);
        public DataSet ReadPerson(int? IdPerson);
        public DataSet UpdatePerson(PersonUpdateDto person);
        public DataSet GetSectors(int? IdPerson);
        public DataSet LogIn(Users user);
        public DataSet AttachSectorToPerson(int IdPerson, int IdSector);
        public DataSet DistickAttachPerson();
    }
}
