using DataAccesLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DataAccessLayer.Contracts;
using DomainModel.Models;

namespace DataAccessLayer.Repositories
{
    public class AltersangabeRepository : IAltersangabeRepository
    {
        public event Action<string> OnError;

        private void ErrorOccured(string errorMessage)
        {
            OnError?.Invoke(errorMessage);
        }

        public int GetAltersangabeIdByValue(int alterswert)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
                {
                    string query = "SELECT agID FROM Altersangabe WHERE Altersangabe = @Wert";
                    int? id = connection.QueryFirstOrDefault<int?>(query, new { Wert = alterswert });

                    if (id.HasValue)
                        return id.Value;
                    else
                        throw new Exception($"Altersangabe '{alterswert}' nicht gefunden.");
                }
            }
            catch (Exception ex)
            {
                ErrorOccured(ex.Message);
                return -1;
            }
        }


        public List<Altersangabe> GetAltersangabe()
        {
            try
            {

                string query = "select ag.agID, ag.Altersangabe as AltersangabeValue from Altersangabe ag";

                using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
                {
                    return (connection.Query<Altersangabe>(query)).ToList();
                }
            }
            catch (Exception ex)
            {
                string errorMessage = "An error happened while getting Altersangabe.";
                ErrorOccured(errorMessage);
                return new List<Altersangabe>();
            }
        }





    }
}
