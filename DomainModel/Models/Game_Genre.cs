using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Models
{
    public class GameGenre
    {
        [Column("Game")]
        public int GameID { get; set; }

        [Column("Genre")]
        public int GenreID { get; set; }

        

        public GameGenre() { }

        public GameGenre(int gameId, int genreId)
        {
            GameID = gameId;
            GenreID = genreId;
        }
    }
}
