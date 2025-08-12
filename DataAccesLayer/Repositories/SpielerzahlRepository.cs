
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DataAccessLayer.Contracts;
using DataAccesLayer.Repositories;
using static System.Runtime.InteropServices.JavaScript.JSType;
using DomainModel.Models;

namespace DataAccessLayer.Repositories
{
    public class SpielerzahlRepository : ISpielerzahlRepository
    {
        public event Action<string> OnError;

        

        public int GetSpielerzahlIDByName(string name)
        {
            try 
            {
                using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
                {
                    string query = "SELECT szID FROM Spielerzahl WHERE Spielerzahl = @Name";
                    int? id = connection.QueryFirstOrDefault<int?>(query, new { Name = name });
                    if (id.HasValue)
                        return id.Value;
                    else
                        throw new Exception($"Spielerzahl nicht gefunden.");

                }
                            
                    
            }
            catch (Exception ex) 
            {
                string errorMessage = ex.Message;
                ErrorOccured(errorMessage);
                return -1;
            }
            
        }


        public string GetSpielerzahlNameByID(int id)
        {
            using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                string query = "SELECT Spielerzahl FROM Spielerzahl WHERE szID = @Id";
                string name = connection.QueryFirstOrDefault<string>(query, new { Id = id });
                if (name != null)
                    return name;
                else
                    throw new Exception($"Spielerzahl mit ID '{id}' nicht gefunden.");
            }
        }

        public List<Spielerzahl> GetSpielerzahlTypes()
        {
            try
            {
               
                string query = "select s.szID, s.Spielerzahl as SpielerzahlName from Spielerzahl s";

                using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
                {
                    return (connection.Query<Spielerzahl>(query)).ToList();
                }
            }
              catch (Exception ex)
            {
                string errorMessage = "An error happened while getting Spielerzahl.";
                ErrorOccured(errorMessage);
                return new List<Spielerzahl>();
            }
        }


        private void ErrorOccured(string errorMessage)
        {
            if (OnError != null)
                OnError.Invoke(errorMessage);
        }

    }
}
