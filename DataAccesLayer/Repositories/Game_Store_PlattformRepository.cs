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
    public class Game_Store_PlattformRepository : IGame_Store_PlattformRepository
    {
        public event Action<string> OnError;
        public void AddGame_Store_Plattform(Game_Store_Plattform game_Store_Plattform)
        {
            try
            {
                string query = "INSERT INTO Game_Store_Plattform (Game, Store, Plattform, Status, Altersangabe, Description) VALUES (@Game, @Store, @Plattform, @Status, @Altersangabe, @Description)";
                using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
                {
                    connection.Execute(query, new
                    {
                        Game = game_Store_Plattform.GameID,
                        Store = game_Store_Plattform.StoreID,
                        Plattform = game_Store_Plattform.PlattformID,
                        Status = game_Store_Plattform.StatusID,
                        Altersangabe = game_Store_Plattform.AltersangabeID,
                        Description = game_Store_Plattform.Description, 

                    });
                }
            }
            catch (SqlException ex) when (ex.Number == 2627)
            {
                // Duplikat – ignoriere oder logge ruhig
                ErrorOccured("Eintrag existiert bereits und wurde nicht erneut hinzugefügt.");
            }
            catch (Exception ex)
            {
                ErrorOccured($"Fehler bei AddGame_Store_Plattform: {ex.Message}");
            }
        }

        public void DeleteGame_Store_Plattform(Game_Store_Plattform game_Store_Plattform)
        {
            try
            {
                string query = @$"delete from Game_Store_Plattform where Game ={game_Store_Plattform.GameID}";

                using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
                {
                    connection.Execute(query);
                }
            }
            catch (Exception ex)
            {
                string errorMessage = "An error happened while deleting Game_Store_Plattform.";
                ErrorOccured(errorMessage);
            }
        }

        public void DeleteStore_Plattfrom(Game_Store_Plattform game_Store_Plattform)
        {
            try
            {
                string query = @"DELETE FROM Game_Store_Plattform WHERE Game = @GameID AND Store = @StoreID AND Plattform = @PlattformID";

                using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
                {
                    connection.Execute(query, game_Store_Plattform);
                }
            }
            catch (Exception ex)
            {
                string errorMessage = "An error happened while deleting Game.";
                ErrorOccured(errorMessage);
            }
        }

        private void ErrorOccured(string errorMessage)
        {
            if (OnError != null)
                OnError.Invoke(errorMessage);
        }

        public Game_Store_Plattform GetGSPGameById(int id)
        {
            try
            {
                string query = @"SELECT Game AS GameID, Store AS StoreID, Plattform AS PlattformID, Status AS StatusID,
                         Altersangabe AS AltersangabeID, Description
                         FROM Game_Store_Plattform
                         WHERE Game = @Id";

                using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
                {
                    return connection.QueryFirstOrDefault<Game_Store_Plattform>(query, new { Id = id });
                }
            }
            catch (Exception ex)
            {
                string errorMessage = "Ein Fehler ist beim Laden von v_Spielinfo aufgetreten.";
                ErrorOccured(errorMessage);
                return null;
            }
        }

        public List<Game_Store_Plattform> GetGamesGSP(string? name = "")
        {
            try
            {
                //       string query = "select * from ";
                string query = "SELECT Game AS GameID, Store AS StoreID, Plattform AS PlattformID, Status AS StatusID, Altersangabe AS AltersangabeID, Description FROM Game_Store_Plattform";



                if (!string.IsNullOrEmpty(name))
                    query += $" where Game like '{name}%'";

                using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
                {
                    return (connection.Query<Game_Store_Plattform>(query)).ToList();
                }
            }
            catch (Exception ex)
            {
                string errorMessage = "An error happened while getting games.";
                ErrorOccured(errorMessage);
                return new List<Game_Store_Plattform>();
            }
        }


        public List<Game_Store_Plattform> GetGameStorePlattformFromGame(int gameId)
        {
            try
            {
                string query = @"SELECT DISTINCT GameID, StoreID, PlattformID, StoreName, PlattformName, StatusID, AltersangabeID, Description
                 FROM v_Game_Store_Plattform
                 WHERE GameID = @GameId";


                using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
                {
                    return connection.Query<Game_Store_Plattform>(query, new { GameId = gameId }).ToList();
                }
            }
            catch (Exception ex)
            {
                ErrorOccured("Fehler beim Laden der Store/Plattform-Daten.");
                return new List<Game_Store_Plattform>();
            }
        }


        public void EditGameStorePlattform(Game_Store_Plattform game_Store_Plattform)
        {
            try
            {
                string query = @"update Game_Store_Plattform
                             set
                             Game  = @GameID,
                             Store  = @StoreID,
                             Plattform  = @PlattformID,
                             Status  = @StatusID,
                             Altersangabe  = @AltersangabeID,
                             Description = @Description
                             WHERE Game = @GameID AND Store = @StoreID AND Plattform  = @PlattformID ";

                using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
                {
                    connection.Execute(query, game_Store_Plattform);
                }
            }
            catch (SqlException ex)
            {
                string errorMessage = "";
                if (ex.Number == 2627)
                    errorMessage = "That Game_Store_Plattform already exists.";
                else
                    errorMessage = "An error happened in the database.";
                ErrorOccured(errorMessage);
            }
            catch (Exception ex)
            {
                string errorMessage = "An error happened while adding Game_Store_Plattform.";
                ErrorOccured(errorMessage);
            }

        }


        public bool Exists(int gameId, int storeId, int plattformId)
        {
            string query = @"SELECT COUNT(*) 
                     FROM Game_Store_Plattform 
                     WHERE Game = @Game AND Store = @Store AND Plattform = @Plattform";

            using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                int count = connection.ExecuteScalar<int>(query, new
                {
                    Game = gameId,
                    Store = storeId,
                    Plattform = plattformId
                });

                return count > 0;
            }
        }



    }
}
