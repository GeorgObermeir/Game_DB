using DataAccesLayer.Repositories;
using DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using static System.Formats.Asn1.AsnWriter;
using DataAccessLayer.Contracts;

namespace DataAccessLayer.Repositories
{
    public class PlattformRepository : IPlattformRepository
    {

        public event Action<string> OnError;


        public  void AddPlattform(Plattform plattform)
        {
            try
            {
                string query = @"insert into Plattform 
                (Plattform) 
                values (@PlattformName)";

                using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
                {
                    connection.Execute(query, plattform);
                }
            }
            catch (SqlException ex)
            {
                string errorMessage = "";
                if (ex.Number == 2627)
                    errorMessage = "That plattform already exists.";
                else
                    errorMessage = "An error happened in the database.";
                ErrorOccured(errorMessage);
            }
            catch (Exception ex)
            {
                string errorMessage = "An error happened while adding a plattform.";
                ErrorOccured(errorMessage);
            }
        }

        private void ErrorOccured(string errorMessage)
        {
            if (OnError != null)
                OnError.Invoke(errorMessage);
        }

        public List<Plattform> GetPlattform()
        {
            try
            {
                string query = "SELECT pfID, Plattform AS PlattformName FROM Plattform";


                using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
                {
                    return (connection.Query<Plattform>(query)).ToList();
                }
            }
            catch (Exception ex)
            {
                string errorMessage = "An error happened while getting Plattform.";
                ErrorOccured(errorMessage);
                return new List<Plattform>();
            }
        }

        public int GetPlattformIDByName(string name)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
                {
                    string query = "SELECT pfID FROM Plattform WHERE Plattform = @Name";
                    int? id = connection.QueryFirstOrDefault<int?>(query, new { Name = name });
                    if (id.HasValue)
                        return id.Value;
                    else
                        throw new Exception($"Plattform nicht gefunden.");

                }


            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                ErrorOccured(errorMessage);
                return -1;
            }

        }

        public string GetPlattformNameByID(int id)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
                {
                    string query = "SELECT p.Plattform FROM Plattform p WHERE p.pfID = @pfID";
                    string? name = connection.QueryFirstOrDefault<string?>(query, new { pfID = id });
                    if (name != null)
                        return name;
                    else
                        throw new Exception($"Plattform nicht gefunden.");

                }


            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                ErrorOccured(errorMessage);
                return "";
            }

        }




    }
}
