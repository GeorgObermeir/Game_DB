using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Models
{
    public  class Genre
    {
        public int genID { get; set; }

        public string GenreName { get; set; }
        
        public Genre(string genreName)
        {
            GenreName = genreName;
        }

        public Genre() { }
    }

    public class GenreComparer : IEqualityComparer<Genre>
    {
        public bool Equals(Genre x, Genre y) => x.genID == y.genID;
        public int GetHashCode(Genre obj) => obj.genID.GetHashCode();
    }
}
