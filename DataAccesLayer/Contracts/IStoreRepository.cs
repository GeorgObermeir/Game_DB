using DataAccesLayer.Repositories;
using DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesLayer.Contracts
{
    public interface IStoreRepository
    {
        public event Action<string> OnError;
        public void AddStore(Store store);

        public List<Store> GetStore();

        public int GetStoreIDByName(string name);

        public string GetStoreNameByID(int id);


    }
}
