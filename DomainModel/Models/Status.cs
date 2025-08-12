using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Models
{
    public class Status
    {
        public int staID { get; set; }


        public string StatusName { get; set; }

        public Status (string statusName) 
        {
            StatusName = statusName;

        }

        Status() { }

    }
}
