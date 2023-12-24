using TestGeodanApi.Models;
using Microsoft.Extensions.Options;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using TestGeodanApi.Interfaces;
using System.Xml.Linq;
using TestGeodanApi.Services;
using System;
using TestGeodanApi.DTO;
using Microsoft.AspNetCore.Http.HttpResults;

namespace TestGeodanApi.DAL
{
    public class DbConnexion : IDbConnexion
    {
        public SqlConnection Connection { get; set; }
        public DbConnexion(string ConnexionString)
        {
            Connection = new SqlConnection(ConnexionString);
        }

        public DataSet LogIn(Users user)
        {
            Connection.Open();
            SqlCommand cmd = new SqlCommand("sp_LogIn", Connection);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UserName", SqlDbType.VarChar).Value = user.UserName;
            cmd.Parameters.AddWithValue("@Password", SqlDbType.VarChar).Value = user.Password;

            Connection.Close();

            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataSet data = new DataSet();
            sda.Fill(data);

            return data;
        }

        public DataSet CreatePerson(string name, string by)
        {
            Connection.Open();
            SqlCommand cmd = new SqlCommand("sp_CreatePerson", Connection);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Name", SqlDbType.VarChar).Value = name;
            cmd.Parameters.AddWithValue("@By", SqlDbType.VarChar).Value = by;

            Connection.Close();

            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataSet data = new DataSet();
            sda.Fill(data);

            return data;
        }

        public DataSet ReadPerson(int? IdPerson)
        {
            Connection.Open();
            SqlCommand cmd = new SqlCommand("sp_ReadPerson", Connection);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Id", SqlDbType.VarChar).Value = IdPerson;

            Connection.Close();

            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataSet data = new DataSet();
            sda.Fill(data);

            return data;
        }

        public DataSet UpdatePerson(PersonUpdateDto person)
        {
            Connection.Open();
            SqlCommand cmd = new SqlCommand("sp_UpdatePerson", Connection);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Id", SqlDbType.Int).Value = person.Id; 
            cmd.Parameters.AddWithValue("@Name", SqlDbType.VarChar).Value = person.person.Name;
            cmd.Parameters.AddWithValue("@CreateBy", SqlDbType.VarChar).Value = person.person.CreateBy;

            Connection.Close();

            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataSet data = new DataSet();
            sda.Fill(data);

            return data;
        }

        public DataSet GetSectors(int? IdPerson)
        {
            Connection.Open();
            SqlCommand cmd = new SqlCommand("sp_ReadSector", Connection);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@IdPerson", SqlDbType.VarChar).Value = IdPerson;

            Connection.Close();

            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataSet data = new DataSet();
            sda.Fill(data);

            return data;
        }

        public DataSet AttachSectorToPerson(int IdPerson, int IdSector)
        {
            Connection.Open();
            SqlCommand cmd = new SqlCommand("sp_AttachSectorToPerson", Connection);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@IdPerson", SqlDbType.VarChar).Value = IdPerson;
            cmd.Parameters.AddWithValue("@IdSector", SqlDbType.VarChar).Value = IdSector;

            Connection.Close();

            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataSet data = new DataSet();
            sda.Fill(data);

            return data;
        }
        public DataSet DistickAttachPerson()
        {
            Connection.Open();
            SqlCommand cmd = new SqlCommand("sp_DistickAttachPerson", Connection);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            Connection.Close();

            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataSet data = new DataSet();
            sda.Fill(data);

            return data;
        }
    }
}
