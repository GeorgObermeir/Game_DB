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
    public class StatusRepository : IStatusRepository
    {
        public event Action<string> OnError;

        private void ErrorOccured(string errorMessage)
        {
            OnError?.Invoke(errorMessage);
        }

        public int GetStatusIDByName(string name)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
                {
                    string query = "SELECT staID FROM Status WHERE Status = @Name";
                    int? id = connection.QueryFirstOrDefault<int?>(query, new { Name = name });
                    if (id.HasValue)
                        return id.Value;
                    else
                        throw new Exception($"Status nicht gefunden.");

                }


            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                ErrorOccured(errorMessage);
                return -1;
            }

        }

        

        public List<Status> GetStatus()
        {
            try
            {

                string query = "select sta.staID, sta.Status as StatusName from Status sta";

                using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
                {
                    return (connection.Query<Status>(query)).ToList();
                }
            }
            catch (Exception ex)
            {
                string errorMessage = "An error happened while getting Status.";
                ErrorOccured(errorMessage);
                return new List<Status>();
            }
        }





    }
}
