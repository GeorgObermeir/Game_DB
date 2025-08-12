using aGameDB.UI;
using aMirrorGameDB.UI;
using DataAccesLayer.Contracts;
using DataAccesLayer.Repositories;
using DataAccessLayer.Contracts;
using DataAccessLayer.Repositories;
using DomainModel.Models;
using GameDB.UI;
using Microsoft.Extensions.DependencyInjection;


namespace GameDB
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            ServiceCollection services = ConfigureServices();
            ServiceProvider serviceProvider = services.BuildServiceProvider();
          

            var startForm = serviceProvider.GetRequiredService<GameOverviewForm>();
            Application.Run(startForm);
        }

        static ServiceCollection ConfigureServices()
        {
            ServiceCollection services = new ServiceCollection();



            services.AddTransient<IStoreRepository>(_ => new StoreRepository());
            services.AddTransient<IGenreRepository>(_ => new GenreRepository());
            services.AddTransient<IGameRepository>(_ => new GameRepository());
            services.AddTransient<IPriceRepository>(_ => new PriceRepository());
            services.AddTransient<ISpielerzahlRepository>(_ => new SpielerzahlRepository());
            services.AddTransient<IGameGenreRepository>(_ => new GameGenreRepository());
            services.AddTransient<IPlattformRepository>(_ => new PlattformRepository());
            services.AddTransient<IStore_PlattformRepository>(_ => new Store_PlattformRepository());
            services.AddTransient<IGame_Store_PlattformRepository>(_ => new Game_Store_PlattformRepository());
            services.AddTransient<IAltersangabeRepository>(_ => new AltersangabeRepository());
            services.AddTransient<IStatusRepository>(_ => new StatusRepository());
            



            services.AddTransient<AddEditGamesForm>();
            services.AddTransient<GameOverviewForm>();
            services.AddTransient<GenreSelectionForm>();
            services.AddTransient<StoreSelectionForm>();
            services.AddTransient<PlattformSelectionForm>();


            return services;

        }

       

    }
}