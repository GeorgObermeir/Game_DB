using DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Contracts
{
    public  interface IGame_Store_PlattformRepository
    {
        public event Action<string> OnError;
        public void AddGame_Store_Plattform(Game_Store_Plattform game_Store_Plattform);

        public void DeleteGame_Store_Plattform(Game_Store_Plattform game_Store_Plattform);

        public Game_Store_Plattform GetGSPGameById(int id);

        public List<Game_Store_Plattform> GetGamesGSP(string? name = "");

        public List<Game_Store_Plattform> GetGameStorePlattformFromGame(int gameId);

        public void EditGameStorePlattform(Game_Store_Plattform game_Store_Plattform);
        public bool Exists(int gameId, int storeId, int plattformId);

        public void DeleteStore_Plattfrom(Game_Store_Plattform game_Store_Plattform);

    }
}
