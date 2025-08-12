using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using DomainModel.Models;
using Dapper;
using DataAccesLayer.Contracts;
using DataAccesLayer.Repositories;


namespace DataAccessLayer.Repositories
{
    public class StoreRepository : IStoreRepository
    {
        public event Action<string> OnError;
        public void AddStore(Store store)
        {
            try
            {
                string query = @"insert into Store 
                (Store) 
                values (@StoreName)";

                using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
                {
                     connection.Execute(query, store);
                }
            }
            catch (SqlException ex)
            {
                string errorMessage = "";
                if (ex.Number == 2627)
                    errorMessage = "That store already exists.";
                else
                    errorMessage = "An error happened in the database.";
                ErrorOccured(errorMessage);
            }
            catch (Exception ex)
            {
                string errorMessage = "An error happened while adding a store.";
                ErrorOccured(errorMessage);
            }


        }

        private void ErrorOccured(string errorMessage)
        {
            if (OnError != null)
                OnError.Invoke(errorMessage);
        }

        public List<Store> GetStore()
        {
            try
            {
                string query = "SELECT stID, Store AS StoreName FROM Store";


                using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
                {
                    return (connection.Query<Store>(query)).ToList();
                }
            }
            catch (Exception ex)
            {
                string errorMessage = "An error happened while getting Store.";
                ErrorOccured(errorMessage);
                return new List<Store>();
            }
        }

        public int GetStoreIDByName(string name)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
                {
                    string query = "SELECT stID FROM Store WHERE Store = @Name";
                    int? id = connection.QueryFirstOrDefault<int?>(query, new { Name = name });
                    if (id.HasValue)
                        return id.Value;
                    else
                        throw new Exception($"Store nicht gefunden.");

                }


            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                ErrorOccured(errorMessage);
                return -1;
            }

        }

        public string GetStoreNameByID(int id)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
                {
                    string query = "SELECT s.Store FROM Store s WHERE s.stID = @stID";
                    string? v = connection.QueryFirstOrDefault<string?>(query, new { stID = id });
                    if (v != null)
                        return v;
                    else
                        throw new Exception($"Store nicht gefunden.");

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
