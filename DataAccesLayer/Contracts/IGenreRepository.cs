using DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Contracts
{
    public interface IGenreRepository
    {
        public event Action<string> OnError;
        public int AddGenre(Genre genre);
        public List<Genre> GetGenre();

        public int GetGenreIdByName(string genreName);

        public Genre GetGenreById(int id);
    }
}
