using DataAccesLayer.Repositories;
using DataAccessLayer.Contracts;
using DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Xml.Linq;

namespace DataAccessLayer.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        public event Action<string> OnError;

        //public void AddGenre(Genre genre)
        //{
        //    try
        //    {
        //        string query = @"insert into Genre 
        //        (Genre) values (@Genre)";

        //        using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
        //        {
        //             connection.Execute(query, genre);
        //        }
        //    }

        //    catch (Exception ex)
        //    {
        //        string errorMessage = "An error happened while adding a genre.";
        //        ErrorOccured(errorMessage);
        //    }
        //}

        public int AddGenre(Genre genre)
        {
            using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                // Erst prüfen, ob Genre schon existiert
                string selectQuery = "SELECT genID FROM Genre WHERE Genre = @GenreName";
                int? id = connection.QueryFirstOrDefault<int?>(selectQuery, new { GenreName = genre.GenreName });

                if (id.HasValue)
                {
                    return id.Value;
                }
                else
                {
                    // Genre noch nicht da, also neu einfügen und ID zurückgeben
                    string insertQuery = "INSERT INTO Genre (Genre) VALUES (@GenreName); SELECT CAST(SCOPE_IDENTITY() as int)";
                    int newId = connection.QuerySingle<int>(insertQuery, new { GenreName = genre.GenreName });
                    return newId;
                }
            }
        }



        private void ErrorOccured(string errorMessage)
        {
            if (OnError != null)
                OnError.Invoke(errorMessage);
        }

        public List<Genre> GetGenre()
        {
            try
            {
                string query = "SELECT genID, Genre AS GenreName FROM Genre";


                using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
                {
                    return ( connection.Query<Genre>(query)).ToList();
                }
            }
            catch (Exception ex)
            {
                string errorMessage = "An error happened while getting genre.";
                ErrorOccured(errorMessage);
                return new List<Genre>();
            }
        }

        public int GetGenreIdByName(string genreName)
        {
            using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                string query = "SELECT genID FROM Genre WHERE Genre = @GenreName";
                return connection.QuerySingleOrDefault<int>(query, new { GenreName = genreName });
            }
        }

        public Genre GetGenreById(int id)
        {
            try
            {
                string query = @"SELECT genID  FROM Genre WHERE genID = @Id";

                using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
                {
                    return connection.QueryFirstOrDefault<Genre>(query, new { Id = id });
                }
            }
            catch (Exception ex)
            {
                string errorMessage = "Ein Fehler ist beim Laden des Spiels aufgetreten.";
                ErrorOccured(errorMessage);
                return null;
            }
        }

    }
}
