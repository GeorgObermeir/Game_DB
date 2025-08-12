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
using DataAccessLayer.Contracts;
using static System.Net.Mime.MediaTypeNames;

namespace DataAccessLayer.Repositories
{
    public class GameRepository : IGameRepository
    {
        public event Action<string> OnError;

        

        public int AddGame(Game game)
        {
            try
            {
                string query = @"
            INSERT INTO Game (Game, Release, Spielerzahl, ImageGame, ImagePath, Price)
            VALUES (@GameName, @ReleaseDate, @SpielerzahlID, @ImageGame, @ImagePath, @Price);
            SELECT CAST(SCOPE_IDENTITY() AS INT);";

                using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
                {
                    int newId = connection.QuerySingle<int>(query, game);
                    return newId;
                }
            }
            catch (SqlException ex)
            {
                string errorMessage = "";
                if (ex.Number == 2627)
                    errorMessage = "That game already exists.";
                else
                    errorMessage = "An error happened in the database.";
                ErrorOccured(errorMessage);
                return 0;
            }
            catch (Exception ex)
            {
                string errorMessage = "An error happened while adding a game.";
                ErrorOccured(errorMessage);
                return 0;
            }
        }


        private void ErrorOccured(string errorMessage)
        {
            if (OnError != null)
                OnError.Invoke(errorMessage);
        }

 
        public   List<Game> GetGames(string? name = "")
        {
            try
            {
            //       string query = "select * from Game";
                string query = "Select GID, Game AS GameName, Release AS ReleaseDate, Spielerzahl AS SpielerzahlID, ImageGame, ImagePath, Price FROM Game";

                if (!string.IsNullOrEmpty(name))
                    query += $" where Game like '{name}%'";

                using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
                {
                    return (connection.Query<Game>(query)).ToList();
                }
            }
            catch (Exception ex)
            {
                string errorMessage = "An error happened while getting games.";
                ErrorOccured(errorMessage);
                return new List<Game>();
            }
        }

        public  void DeleteGame(Game game)
        {
            try
            {
                string query = @$"delete from Game where gID={game.GID}";

                using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
                {
                     connection.Execute(query);
                }
            }
            catch (Exception ex)
            {
                string errorMessage = "An error happened while deleting Game.";
                ErrorOccured(errorMessage);
            }
        }

        public Game GetGameById(int id)
        {
            try
            {
                string query = @"SELECT GID, Game AS GameName, Release AS ReleaseDate, ImageGame, ImagePath, Spielerzahl AS SpielerzahlID, Price FROM Game WHERE GID = @Id";

                using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
                {
                    return connection.QueryFirstOrDefault<Game>(query, new { Id = id });
                }
            }
            catch (Exception ex)
            {
                string errorMessage = "Ein Fehler ist beim Laden des Spiels aufgetreten.";
                ErrorOccured(errorMessage);
                return null;
            }
        }


        public void EditGame(Game game)
        {
            try
            {
                string query = @"update Game
                             set
                             Game = @GameName,
                             Release = @ReleaseDate,
                             Spielerzahl = @SpielerzahlID,
                             ImageGame = @ImageGame,
                             ImagePath = @ImagePath,
                             Price = @Price
                             where gID = @GID";

                using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
                {
                     connection.Execute(query, game);
                }
            }
            catch (SqlException ex)
            {
                string errorMessage = "";
                if (ex.Number == 2627)
                    errorMessage = "That game already exists.";
                else
                    errorMessage = "An error happened in the database.";
                ErrorOccured(errorMessage);
            }
            catch (Exception ex)
            {
                string errorMessage = "An error happened while adding game.";
                ErrorOccured(errorMessage);
            }

        }

    }
}
