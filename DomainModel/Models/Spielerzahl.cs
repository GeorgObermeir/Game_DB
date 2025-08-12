using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Models
{
    public class Spielerzahl
    {
        public int szID { get; set; }

        public string SpielerzahlName { get; set; }

        public Spielerzahl(int szid, string spielerzahlName)
        {
            szID = szid;
            SpielerzahlName = spielerzahlName;

        }

        public Spielerzahl(string spielerzahlName) 
        {
            SpielerzahlName = spielerzahlName;

        }

        public Spielerzahl() { }

    }
}
