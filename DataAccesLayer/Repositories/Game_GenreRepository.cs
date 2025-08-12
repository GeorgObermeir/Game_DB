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

namespace DataAccessLayer.Repositories
{
    public class GameGenreRepository : IGameGenreRepository
    {
        public event Action<string> OnError;

        private void ErrorOccured(string errorMessage)
        {
            OnError?.Invoke(errorMessage);
        }

        public void AddGameGenre(GameGenre gameGenre)
        {
            try
            {
                string query = "INSERT INTO Game_Genre (Game, Genre) VALUES (@Game, @Genre)";
                using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
                {
                    connection.Execute(query, new
                    {
                        Game = gameGenre.GameID,
                        Genre = gameGenre.GenreID
                    });
                }
            }
            catch (Exception ex)
            {
                ErrorOccured($"Fehler beim Verknüpfen von Spiel und Genre: {ex.Message}");
            }
        }

        


        public List<Genre> GetGenresByGameId(int gameId)
        {
            try
            {
                string query = @"SELECT gen.genID, gen.Genre AS GenreName
                                 FROM Genre gen
                                 INNER JOIN Game_Genre gg ON gen.genID = gg.Genre
                                 WHERE gg.Game = @GameId";

                using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
                {
                    return connection.Query<Genre>(query, new { GameId = gameId }).ToList();
                }
            }
            catch (Exception ex)
            {
                ErrorOccured("Fehler beim Laden der Genres für das Spiel.");
                return new List<Genre>();
            }
        }



        public void DeleteGameGenre(GameGenre gameGenre)
        {
            try
            {
                string query = @"DELETE FROM Game_Genre WHERE Game = @GameID AND Genre = @GenreID";

                using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
                {
                    connection.Execute(query, gameGenre);
                }
            }
            catch (Exception ex)
            {
                string errorMessage = "An error happened while deleting Game.";
                ErrorOccured(errorMessage);
            }
        }



    }
}
