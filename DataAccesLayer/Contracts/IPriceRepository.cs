using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.Models;

namespace DataAccessLayer.Contracts
{
    public interface IPriceRepository
    {
        public event Action<string> OnError;
        int GetPriceIDByValue(decimal priceValue);

        public decimal GetPriceValueById(int priceId);
    }
}
