using DataAccesLayer.Repositories;
using DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Contracts
{
    public interface IStore_PlattformRepository
    {

        public event Action<string> OnError;
     //   public int AddStore_Plattform(Store_Plattform store_Plattform);


        public Store_Plattform GetStorePlattformByGameId(int storeId, int plattformId);

        public List<Store_Plattform> GetStorePlattformFromGame(int gameId);

        public bool StorePlattformExists(int storeId, int plattformId);

        public int GetStore_PlattformVonStore(int plattform);

        public bool AddStore_Plattform(Store_Plattform store_Plattform);

    }
}
