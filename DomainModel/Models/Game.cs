using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DomainModel.Models
{
    public class Game
    {
        public int GID { get; set; }

        public int SpielerzahlID { get; set; }

        public int PriceID { get; set; }

        [Column("Game")]    // <-- DB-Spaltenname bleibt "Store"


        public string GameName { get; set; }
        public DateTime ReleaseDate { get; set; }
        public byte[] ImageGame { get; set; }       
        public string ImagePath { get; set; }
        public int Price { get; set; }

        public Game(string gameName, DateTime releaseDate, int spielerzahlID, byte[]? image, string imagePath, int price, int? gID = null)
        
        {

            GameName = gameName;
            ReleaseDate = releaseDate;
            SpielerzahlID = spielerzahlID;
            ImageGame = image;
            ImagePath = imagePath;
            Price = price;

            if (gID != null)
            {
                GID = (int)gID;
            }
        }


        public Game() { }

        public void Copy(Game game)
        {                     
            GID = game.GID;
            GameName = game.GameName;
            ReleaseDate = game.ReleaseDate;            
            ImagePath = game.ImagePath;
            Price = game.Price;


        }

    }
}
