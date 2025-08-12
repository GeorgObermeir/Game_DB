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
    public  interface IPlattformRepository
    {

        public event Action<string> OnError;


        public void AddPlattform(Plattform plattform);

        public List<Plattform> GetPlattform();

        public int GetPlattformIDByName(string name);

        public string GetPlattformNameByID(int id);


    }
}
