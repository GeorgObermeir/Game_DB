using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Models
{
    public  class Store
    {
        public int stID {  get; set; }
        
        public string StoreName {  get; set; }
        
        public Store(string storeName)
        {
            StoreName = storeName;
        }

        public Store() { }

    }
}
