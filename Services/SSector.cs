using Microsoft.Extensions.Options;
using System.Data;
using TestGeodanApi.DAL;
using TestGeodanApi.Interfaces;
using TestGeodanApi.Models;

namespace TestGeodanApi.Services
{
    public class SSector : ISector
    {
        public DbConnexion dbc;
        public SSector(IOptions<Parameters> appSetting)
        {
            dbc = new DbConnexion(appSetting.Value.ConnexionString);
        }
        public List<Sector> GetSectors()
        {
            DataSet ds = dbc.GetSectors(null);
            if (ds != null)
            {
                if (ds.Tables.Count == 0) { return null; }

                List<Sector> sectors = new List<Sector>();

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    sectors.Add(new Sector
                    {
                        Id = (int)dr["Id"],
                        Name = (string)dr["Name"],
                        Level = (int)dr["Level"]
                    });
                }
                return sectors;
            }
            return null;
        }
    }
}
