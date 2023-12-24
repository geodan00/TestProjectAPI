using Microsoft.Extensions.Options;
using System;
using System.Data;
using System.Reflection.Metadata;
using TestGeodanApi.DAL;
using TestGeodanApi.DTO;
using TestGeodanApi.Interfaces;
using TestGeodanApi.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TestGeodanApi.Services
{
    public class SPerson : IPerson
    {
        public DbConnexion dbc;
        public SPerson(IOptions<Parameters> appSetting)
        {
            dbc = new DbConnexion(appSetting.Value.ConnexionString);
        }
        public Person? CreatePerson(PersonCreateDto person)
        {
            DataSet ds = dbc.CreatePerson(person.Name, person.CreateBy);
            if (ds != null)
            {
                if (ds.Tables.Count != 0)
                {
                    int IdPerson = (int)ds.Tables[0].Rows[0]["Id"];

                    if (IdPerson != null) {
                        foreach (PersonCreateSectorDto sec in person.Sectors)
                        {
                            dbc.AttachSectorToPerson(IdPerson, sec.Id);
                        }
                        return this.ReadPerson(IdPerson).First();
                    }

                }
            }
            return null;
        }

        private Person ReadSinglePerson(int? IdPerson)
        {

            DataSet ds = dbc.ReadPerson(IdPerson);
            if (ds != null)
            {
                if (ds.Tables.Count == 0) { return null; }
                Person person = new Person();
                List<Sector> sectors = new List<Sector>();

                person.Id = (int)ds.Tables[0].Rows[0]["Id"];
                person.Name = (string)ds.Tables[0].Rows[0]["Name"];
                person.CreateAt = Convert.ToString(ds.Tables[0].Rows[0]["CreateAt"]);
                person.UpdateAt = Convert.ToString(ds.Tables[0].Rows[0]["UpdateAt"]);
                person.CreateBy = (string)ds.Tables[0].Rows[0]["CreateBy"];

                foreach (DataRow dr in ds.Tables[0].Rows)
                {

                    sectors.Add(new Sector
                    {
                        Id = (int)dr["IdSector"],
                        Name = (string)dr["NameSector"],
                        Level = (int)dr["Level"]
                    });
                }
                person.Sectors = sectors;
                return person;
            }
            else { return null; }
        }

        public List<Person>? ReadPerson(int? IdPerson)
        {
            if (IdPerson != null)
            {
                return ReadSinglePerson(IdPerson) != null? (new List<Person> { ReadSinglePerson(IdPerson) }) : null;
            }
            else
            {
                DataSet ds = dbc.DistickAttachPerson();
                if (ds != null)
                {
                    if (ds.Tables.Count == 0) { return null; }

                    List<Person> persons = new List<Person>();

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        persons.Add(this.ReadSinglePerson((int)dr["IdPerson"]));
                    }
                    return persons;
                }
                return null;
            }
        }

        public Person? UpdatePerson(PersonUpdateDto person)
        {
            DataSet ds = dbc.UpdatePerson(person);
            if (ds != null)
            {
                foreach (PersonCreateSectorDto sec in person.person.Sectors)
                {
                    dbc.AttachSectorToPerson(person.Id, sec.Id);
                }
                return this.ReadPerson(person.Id).First();
            }
            return null;
        }
    }
}
