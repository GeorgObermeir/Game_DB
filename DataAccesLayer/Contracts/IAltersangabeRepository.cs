using DataAccesLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.Models;

namespace DataAccessLayer.Contracts
{
    public interface IAltersangabeRepository
    {
        public event Action<string> OnError;

     
        public int GetAltersangabeIdByValue(int id);

        public List<Altersangabe> GetAltersangabe();

    }
}
