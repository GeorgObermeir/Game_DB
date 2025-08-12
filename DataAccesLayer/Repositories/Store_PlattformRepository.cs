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
using static System.Runtime.InteropServices.JavaScript.JSType;
using DataAccessLayer.Contracts;

namespace DataAccessLayer.Repositories
{
    public class Store_PlattformRepository : IStore_PlattformRepository
    {
        public event Action<string> OnError;

        public bool AddStore_Plattform(Store_Plattform store_Plattform)
        {
            try
            {
                string query = @"
            INSERT INTO Store_Plattform (Store, Plattform)
            VALUES (@StoreID, @PlattformID);";

                using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
                {
                    connection.Execute(query, new { StoreID = store_Plattform.StoreID, PlattformID = store_Plattform.PlattformID });
                }
                return true;
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627) // Duplicate key error
                    ErrorOccured("Diese Store-Plattform Kombination existiert bereits.");
                else
                    ErrorOccured("Datenbankfehler: " + ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                ErrorOccured("Fehler beim Hinzufügen der Store_Plattform: " + ex.Message);
                return false;
            }
        }


        //public int AddStore_Plattform(Store_Plattform store_Plattform)
        //{
        //    try
        //    {
        //        string query = @"
        //    INSERT INTO Store_Plattform (Store, Plattform)
        //    VALUES (@StoreID, @PlattformID)"; 

        //        using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
        //        {
        //            int newId = connection.QuerySingle<int>(query, store_Plattform);
        //            return newId;
        //        }
        //    }
        //    catch (SqlException ex)
        //    {
        //        string errorMessage = "";
        //        if (ex.Number == 2627)
        //            errorMessage = "That store and plattform already exists.";
        //        else
        //            errorMessage = "An error happened in the database.";
        //        ErrorOccured(errorMessage);
        //        return 0;
        //    }
        //    catch (Exception ex)
        //    {
        //        string errorMessage = $"An error happened while adding Store_Plattform: {ex.Message}";
        //        ErrorOccured(errorMessage);
        //        return 0;
        //    }
        //}

        public Store_Plattform GetStorePlattformByGameId(int storeId, int plattformId)
        {
            try
            {
                string query = @"select st.stID, st.Store as StoreName, pf.pfID, pf.Plattform               as PlattformName
                                From Store st
                                INNER JOIN Store_Plattform spf ON st.stID = spf.Store
                                INNER JOIN Plattform pf ON pf.pfID = spf.Plattform
                                WHERE spf.Store = @StoreId AND spf.Plattform =                        @PlattformId";

                using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
                {
                    return connection.QueryFirstOrDefault<Store_Plattform>(query, new { StoreId = storeId, PlattformId = plattformId });
                }
            }
            catch (Exception ex)
            {
                ErrorOccured("Fehler beim Laden von Store/Plattform.");
                return null;
            }
        }

        private void ErrorOccured(string errorMessage)
        {
            if (OnError != null)
                OnError.Invoke(errorMessage);
        }

        public List<Store_Plattform> GetStorePlattformFromGame(int gameId)
        {
            try
            {
                string query = @"select GID from v_Spielinfo
        WHERE GID = @GameId";

                using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
                {
                    return connection.Query<Store_Plattform>(query, new { GameId = gameId }).ToList();
                }
            }
            catch (Exception ex)
            {
                ErrorOccured("Fehler beim Ermitteln von Store/Plattform aus Spiel.");
                return new List<Store_Plattform>();
            }
        }

        public bool StorePlattformExists(int storeId, int plattformId)
        {
            const string query = "SELECT COUNT(*) FROM Store_Plattform WHERE Store = @Store AND Plattform = @Plattform";

            using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                int count = connection.ExecuteScalar<int>(query, new { Store = storeId, Plattform = plattformId });
                return count > 0;
            }
        }

        //kannst du neue func implenetieren? nimt eine Platfform aus Store in Store_Plattform
        //select sp.Store_Plattform from Store_Plattform sp where sp.Plattform  = $v

        public int GetStore_PlattformVonStore(int plattform)
        {
            try
            {
                string query = "select sp.Plattform from Store_Plattform sp where sp.Store = @v";
                
                using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
                {
                    return connection.QueryFirstOrDefault<int>(query, new { v = plattform });
                }
            }
            catch (Exception ex)
            {
                ErrorOccured("Fehler beim Laden von Store/Plattform.");
                return -1;
            }
        }

    }


}

