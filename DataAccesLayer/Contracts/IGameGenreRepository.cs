using DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DataAccessLayer.Contracts
{
    public interface IGameGenreRepository
    {
        public event Action<string> OnError;

        public void AddGameGenre(GameGenre gameGenre);

        public List<Genre> GetGenresByGameId(int gameId);

        public void DeleteGameGenre(GameGenre gameGenre);

    }
}
