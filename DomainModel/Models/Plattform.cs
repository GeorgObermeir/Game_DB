using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Models
{
    public class Plattform
    {
        public int pfID {  get; set; }


        public string PlattformName { get; set; }

        public Plattform(string plattformName) 
        {

            PlattformName = plattformName;
        }

        Plattform() { }

    }
}
