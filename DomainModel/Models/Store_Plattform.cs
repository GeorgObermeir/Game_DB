using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Models
{
    public class Store_Plattform
    {
        
        public int StoreID { get; set; }
      
        public int PlattformID { get; set; }

        public string StoreName { get; set; }
        public string PlattformName { get; set; }

        public Store_Plattform(int storeID, int plattoformID) 
        {
            StoreID = storeID;
            PlattformID = plattoformID;

        }

        public Store_Plattform() { }

    }
}
