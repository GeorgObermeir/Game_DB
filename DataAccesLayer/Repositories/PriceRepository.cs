
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

namespace DataAccessLayer.Repositories
{
    public class PriceRepository : IPriceRepository
    {
        public event Action<string> OnError;

        public int GetPriceIDByValue(decimal priceValue)
        {
            using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                // Erst prüfen, ob Preis schon existiert
                string selectQuery = "SELECT pID FROM Price WHERE Preis = @PriceValue";
                int? id = connection.QueryFirstOrDefault<int?>(selectQuery, new { PriceValue = priceValue });

                if (id.HasValue)
                {
                    return id.Value;
                }
                else
                {
                    // Preis noch nicht da, also neu einfügen und ID zurückgeben
                    string insertQuery = "INSERT INTO Price (Preis) VALUES (@PriceValue); SELECT CAST(SCOPE_IDENTITY() as int)";
                    int newId = connection.QuerySingle<int>(insertQuery, new { PriceValue = priceValue });
                    return newId;
                }
            }
        }


        public decimal GetPriceValueById(int priceId)
        {
            using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                string query = "SELECT Preis FROM Price WHERE pID = @PriceId";
                return connection.QuerySingleOrDefault<decimal>(query, new { PriceId = priceId });
            }
        }

        private void ErrorOccured(string errorMessage)
        {
            if (OnError != null)
                OnError.Invoke(errorMessage);
        }
    }
}
