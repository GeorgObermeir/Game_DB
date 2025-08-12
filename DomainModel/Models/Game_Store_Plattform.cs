using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Models
{
    public class Game_Store_Plattform
    {
        
        public int GameID { get; set; }
       
        public int StoreID { get; set; }
        public string StoreName { get; set; }


        public int PlattformID { get; set; }
        public string PlattformName { get; set; }


        public int StatusID { get; set; }
       
        public int AltersangabeID { get; set; }

        public string Description { get; set; }



        public Game_Store_Plattform(int gameID, int storeID, string? storeName, int plattformID, string? plattformName,
                                int statusID, int altersangabeID, string description)
        {
            GameID = gameID;
            StoreID = storeID;
            StoreName = storeName;
            PlattformID = plattformID;
            PlattformName = plattformName;
            StatusID = statusID;
            AltersangabeID = altersangabeID;
            Description = description;
        }

        public Game_Store_Plattform(int gameID, int storeID, int plattformID,
                        int statusID, int altersangabeID, string description)
        {
            GameID = gameID;
            StoreID = storeID;
            PlattformID = plattformID;
            StatusID = statusID;
            AltersangabeID = altersangabeID;
            Description = description;
        }

        public Game_Store_Plattform(int gameID, int storeID, int plattformID)
        {
            GameID = gameID;
            StoreID = storeID;
            PlattformID = plattformID;

        }

        public Game_Store_Plattform () { }

    }
}
