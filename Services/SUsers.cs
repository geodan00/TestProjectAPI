using Microsoft.Extensions.Options;
using System.Data;
using TestGeodanApi.DAL;
using TestGeodanApi.Interfaces;
using TestGeodanApi.Models;

namespace TestGeodanApi.Services
{
    public class SUsers : IUsers
    {
        public DbConnexion dbc;
        public SUsers(IOptions<Parameters> appSetting)
        {
            dbc = new DbConnexion(appSetting.Value.ConnexionString);
        }
        public Users LogIn(Users user)
        {
            DataSet ds = dbc.LogIn(user);
            if (ds != null)
            {
                if (ds.Tables.Count == 0) { return null; }
                user.Password = string.Empty;
                return user;
            }
            return null;
        }
    }
}
