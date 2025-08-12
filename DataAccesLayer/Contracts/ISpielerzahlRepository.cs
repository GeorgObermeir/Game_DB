using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.Models;

namespace DataAccessLayer.Contracts
{
    public interface ISpielerzahlRepository
    {
        int GetSpielerzahlIDByName(string name);

        public string GetSpielerzahlNameByID(int id);

        public List<Spielerzahl> GetSpielerzahlTypes();

        public event Action<string> OnError;
    }
}
