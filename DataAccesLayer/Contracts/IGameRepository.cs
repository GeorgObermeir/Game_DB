using DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Contracts
{
    public interface IGameRepository
    {
        public event Action<string> OnError;

        public int AddGame(Game game);

        public List<Game> GetGames(string? name = "");

        public void DeleteGame(Game game);

        public Game GetGameById(int id);

        public void EditGame(Game game);

    }
}
