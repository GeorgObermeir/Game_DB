using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Models
{
    public class Altersangabe
    {
        public int agID { get; set; }

   //     [Column("Altersangabe")] // <-- DB-Spaltenname bleibt "Altersangabe"
        public byte AltersangabeValue { get; set; }


        public Altersangabe(byte altersangabeValue) 
        {
            AltersangabeValue = altersangabeValue;


        }

        Altersangabe() { }

    }
}
