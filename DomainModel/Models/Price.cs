using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Models
{
    public class Price
    {
        public int pID { get; set; }

        public decimal PriceValue { get; set; }



        public Price(decimal pricevalue) 
        {
            PriceValue = pricevalue;

        }

        public Price() { }
    }
}
