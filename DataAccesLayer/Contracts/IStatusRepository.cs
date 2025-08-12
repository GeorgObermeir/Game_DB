using DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Contracts
{
    public interface IStatusRepository
    {
        public event Action<string> OnError;


        public List<Status> GetStatus();
        public int GetStatusIDByName(string name);
    }
}
