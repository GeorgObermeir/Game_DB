using aGameDB.UI;
using aMirrorGameDB.UI;
using DataAccesLayer.Contracts;
using DataAccesLayer.Repositories;
using DataAccessLayer.Contracts;
using DataAccessLayer.Repositories;
using DomainModel.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualBasic.Devices;
using Microsoft.VisualBasic.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.ComponentModel.Design.ObjectSelectorEditor;



namespace GameDB.UI
{
    public partial class AddEditGamesForm : Form
    {
        private readonly IStoreRepository _storeRepository;
        private readonly IServiceProvider _serviceProvider;
        private readonly IGameRepository _gameRepository;
        private readonly ISpielerzahlRepository _spielerzahlRepository;
        private readonly IPriceRepository _priceRepository;
        private readonly IGenreRepository _genreRepository;
        private readonly IGameGenreRepository _gameGenreRepository;
        private readonly IPlattformRepository _plattformRepository;
        private readonly IStore_PlattformRepository _store_PlattformRepository; //hier ist eine störung
        private readonly IGame_Store_PlattformRepository _game_Store_PlattformRepository;
        private readonly IAltersangabeRepository _altersangabeRepository;
        private readonly IStatusRepository _statusRepository;


        private readonly GameOverviewForm _overviewForm;

        private List<string> selectedGenres = new List<string>();
        private List<string> selectedStores = new List<string>();
        private List<string> selectedPlattforms = new List<string>();
        List<Store_Plattform> selectedStorePlattformPairs = new List<Store_Plattform>();




        private Game _game;
        private Boolean IsEdit;
        private Game OriginalGame;
        public Game EditedGame;
        public Game NewGame;
        public GameGenre NewGameGenre;
        public Boolean DataSaved;


        public AddEditGamesForm(Game game, IStoreRepository storeRepository, IServiceProvider serviceProvider, IGameRepository gameRepository, IPriceRepository priceRepository, ISpielerzahlRepository spielerzahlRepository, GameOverviewForm overviewForm, IGenreRepository genreRepository, IGameGenreRepository gameGenreRepository, IPlattformRepository plattformRepository, IStore_PlattformRepository store_PlattformRepository, IGame_Store_PlattformRepository game_Store_PlattformRepository, IAltersangabeRepository altersangabeRepository, IStatusRepository statusRepository)
        {
            InitializeComponent();
            _game = game;
            _storeRepository = storeRepository;
            _serviceProvider = serviceProvider;
            _gameRepository = gameRepository;
            _priceRepository = priceRepository;
            _spielerzahlRepository = spielerzahlRepository;
            _genreRepository = genreRepository;
            _gameGenreRepository = gameGenreRepository;
            _plattformRepository = plattformRepository;
            _store_PlattformRepository = store_PlattformRepository;
            _game_Store_PlattformRepository = game_Store_PlattformRepository;
            _altersangabeRepository = altersangabeRepository;
            _statusRepository = statusRepository;
            _overviewForm = overviewForm;


            // Für errors 
            _gameRepository.OnError += OnErrorOccured;
            _genreRepository.OnError += OnErrorOccured;
            _gameGenreRepository.OnError += OnErrorOccured;
            _priceRepository.OnError += OnErrorOccured;
            _spielerzahlRepository.OnError += OnErrorOccured;
            _storeRepository.OnError += OnErrorOccured;
            _plattformRepository.OnError += OnErrorOccured;
            _store_PlattformRepository.OnError += OnErrorOccured;
            _game_Store_PlattformRepository.OnError += OnErrorOccured;
            _altersangabeRepository.OnError += OnErrorOccured;
            _statusRepository.OnError += OnErrorOccured;
            //

            IsEdit = game != null;
            OriginalGame = game;


        }


        private void OnErrorOccured(string errorMessage)
        {
            MessageBox.Show(errorMessage);
        }

        private void AddStoreBtn_Click(object sender, EventArgs e)
        {
  
            // Holt eine Instanz von GenreSelectionForm über Dependency Injection.
            StoreSelectionForm form = _serviceProvider.GetRequiredService<StoreSelectionForm>();
            form.StartPosition = FormStartPosition.CenterParent;
            // Öffnet das Formular als modales Fenster. (Benutzer muss es zuerst schließen)



            // form.ShowDialog Öffnet ein modales Fenster.Benutzer muss es zuerst schließen
            if (form.ShowDialog() == DialogResult.OK)
            {

                string selected = form.SelectedStore;

                if (!selectedStores.Contains(selected))
                {
                    selectedStores.Add(selected);
                    AddStoreTag(selected);

                    //if xbox store then manual choice
                    if (!selected.Equals("Xbox Store"))
                        AddPattformFromStore(selected);
                }
                else
                {
                    MessageBox.Show("Der Store wurde bereits hinzugefügt.");
                }
            }

        }

        private void AddPattformFromStore(string selected)
        {

            //hier nehmen wir eine Plattform from db where plattoform in store or store in plattform

            var storeID = _storeRepository.GetStoreIDByName(selected);

            var plattformID = _store_PlattformRepository.GetStore_PlattformVonStore(storeID);
            if (plattformID != -1)
            {
                //hier müsser wir get store from db

                var plattformName = _plattformRepository.GetPlattformNameByID(plattformID);
                if (!selectedPlattforms.Contains(plattformName))
                {
                    selectedPlattforms.Add(plattformName);
                    AddPlattformTag(plattformName);
                }


            }
        }

        private void AddGenreBtn_Click(object sender, EventArgs e)
        {
            // Holt eine Instanz von GenreSelectionForm über Dependency Injection.
            GenreSelectionForm form = _serviceProvider.GetRequiredService<GenreSelectionForm>();
            form.StartPosition = FormStartPosition.CenterParent;
            // Öffnet das Formular als modales Fenster. (Benutzer muss es zuerst schließen)



            // form.ShowDialog Öffnet ein modales Fenster.Benutzer muss es zuerst schließen
            if (form.ShowDialog() == DialogResult.OK)
            {

                string selected = form.SelectedGenre;

                if (!selectedGenres.Contains(selected))
                {
                    selectedGenres.Add(selected);
                    AddGenreTag(selected);
                }
                else
                {
                    MessageBox.Show("Das Genre wurde bereits hinzugefügt.");
                }
            }

        }

        private void AddStoreTag(string store)
        {
            // Erstellt ein Panel was in das FlowLayoutpanel eingefügt wird
            Panel tagPanel1 = new Panel();
            tagPanel1.BackColor = Color.LightBlue;
            tagPanel1.Padding = new Padding(5);
            tagPanel1.Margin = new Padding(4);
            tagPanel1.AutoSize = true;
            tagPanel1.BorderStyle = BorderStyle.FixedSingle;


            //Behinhaltet den Text des Panels
            Label lbl = new Label();
            lbl.Text = store;
            lbl.AutoSize = true;
            lbl.Font = new Font(lbl.Font.FontFamily, 8);


            //erstelle btn für remove
            Button removeBtn = new Button
            {
                Text = "XX",
                BackColor = Color.Crimson,
                ForeColor = Color.Black,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(20, 30),
                Margin = new Padding(3),
                Font = new Font(lbl.Font.FontFamily, 8),
                Tag = store,



            };

            removeBtn.FlatAppearance.BorderSize = 0;


            removeBtn.Click += (sender, e) =>
            {
                StoreFlow.Controls.Remove(tagPanel1);
                selectedStores.Remove(store);
                //implement remove plattform too, when have only one store^^
            };



            tagPanel1.Controls.Add(lbl);
            tagPanel1.Controls.Add(removeBtn);
            StoreFlow.Controls.Add(tagPanel1);

        }

        private void AddPlattformTag(string plattfrom)
        {
            // Erstellt ein Panel was in das FlowLayoutpanel eingefügt wird
            Panel tagPanel1 = new Panel();
            tagPanel1.BackColor = Color.LightBlue;
            tagPanel1.Padding = new Padding(0);
            tagPanel1.Margin = new Padding(4);
            tagPanel1.AutoSize = true;
            tagPanel1.BorderStyle = BorderStyle.FixedSingle;

            //Behinhaltet den Text des Panels
            Label lbl = new Label();
            lbl.Text = plattfrom;
            lbl.AutoSize = true;
            lbl.Font = new Font(lbl.Font.FontFamily, 8);


            //remove Btn
            Button removeBtn = new Button
            {
                Text = "XX",
                BackColor = Color.Crimson,
                ForeColor = Color.Black,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(15, 30),
                Margin = new Padding(3),
                Font = new Font(lbl.Font.FontFamily, 8),
                Tag = plattfrom,



            };

            removeBtn.FlatAppearance.BorderSize = 0;


            removeBtn.Click += (sender, e) =>
            {
                PlattfromFlow.Controls.Remove(tagPanel1);
                selectedPlattforms.Remove(plattfrom);

            };



            tagPanel1.Controls.Add(lbl);
            tagPanel1.Controls.Add(removeBtn);

            PlattfromFlow.Controls.Add(tagPanel1);

        }

        private void AddGenreTag(string genre)
        {
            // Erstellt ein Panel was in das FlowLayoutpanel eingefügt wird
            Panel tagPanel = new Panel();
            tagPanel.BackColor = Color.LightBlue;
            tagPanel.Padding = new Padding(0);
            tagPanel.Margin = new Padding(4);
            tagPanel.AutoSize = true;
            tagPanel.BorderStyle = BorderStyle.FixedSingle;

            //Behinhaltet den Text des Panels
            Label lbl = new Label();
            lbl.Text = genre;
            lbl.AutoSize = true;
            lbl.Font = new Font(lbl.Font.FontFamily, 8);


            //erstelle Btn für remove
            Button removeBtn = new Button
            {
                Text = "XX",
                BackColor = Color.Crimson,
                ForeColor = Color.Black,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(20, 30),
                Margin = new Padding(3),
                Font = new Font(lbl.Font.FontFamily, 8),
                Tag = genre,



            };

            removeBtn.FlatAppearance.BorderSize = 0;


            removeBtn.Click += (sender, e) =>
            {
                GenreFlow.Controls.Remove(tagPanel);
                selectedGenres.Remove(genre);

            };




            tagPanel.Controls.Add(lbl);
            tagPanel.Controls.Add(removeBtn);
            GenreFlow.Controls.Add(tagPanel);


        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            List<String> errors;

            errors = ValidateInput();

            if (errors.Count > 0)
            {
                ShowErrors(errors, 5);
                return;
            }


            StoreInput();
            DataSaved = true;
            this.Close();
        }

        private List<string> ValidateInput()
        {
            List<String> errors = new List<string>();

            if (string.IsNullOrWhiteSpace(GameTxt.Text))
                errors.Add("Title required");

            if (GenreFlow.Controls.Count.Equals(0))
                errors.Add("Genre required");

            if (StoreFlow.Controls.Count.Equals(0))
                errors.Add("Store required");

            if (PlattfromFlow.Controls.Count.Equals(0))
                errors.Add("Plattform required");

            if (string.IsNullOrWhiteSpace(ImagePathTxt.Text))
                errors.Add("Image Path required");

            return errors;
        }


        private void StoreInput()
        {


            string title = GameTxt.Text;
            DateTime releaseDate = ReleaseDatePicker.Value;
            string spielerzahlName = PlayerCountCbx.Text;
            //     MessageBox.Show(spielerzahlName);

            string imagePathforGame = ImagePathTxt.Text;
            byte[] image = File.ReadAllBytes(imagePathforGame);



            decimal priceValue = PriceNum.Value;
            string imgPath = ImagePathTxt.Text;

            // TODO: IDs aus Repos holen, hier als Beispiel:
            int spielerzahlID = _spielerzahlRepository.GetSpielerzahlIDByName(spielerzahlName);
            MessageBox.Show($"Spielerzhal ID gefunden {spielerzahlID}");

            int priceID = _priceRepository.GetPriceIDByValue(priceValue); // oder Preis direkt speichern ohne FK



            Game game = new Game(GameTxt.Text, ReleaseDatePicker.Value, spielerzahlID, image, ImagePathTxt.Text, (int)PriceNum.Value);





            if (IsEdit)
            {
                EditedGame = new Game(
                    title,
                    releaseDate,
                    spielerzahlID,
                    image,
                    imgPath,
                    priceID,
                    OriginalGame.GID
                );

                _gameRepository.EditGame(game);
            }
            else
            {
                NewGame = new Game(
                    title,
                    releaseDate,
                    spielerzahlID,
                    image,
                    imgPath,
                    priceID
                // kein gID setzen, DB macht das
                );


                // Füge das neue Spiel in die Datenbank ein und speichere die GID (Primärschlüssel)
                NewGame.GID = _gameRepository.AddGame(NewGame);


                // Fügt dem spiel Genre hinzu
                foreach (string genreName in selectedGenres)
                {
                    int genreId = _genreRepository.GetGenreIdByName(genreName);

                    // Erstelle ein neues GameGenre-Objekt zur Verknüpfung von Spiel und Genre
                    var gameGenre = new GameGenre(NewGame.GID, genreId);
                    _gameGenreRepository.AddGameGenre(gameGenre);
                }


                //neu
                AddGame_Store_PlattformtoDB();



            }


        }

        private void AddGame_Store_PlattformtoDB()
        {


            int selectedUsk = int.Parse(UskCbx.Text);
            int altersangabeId = _altersangabeRepository.GetAltersangabeIdByValue(selectedUsk);

            string selectedStatus = OwnershipCbx.Text;
            int statusId = _statusRepository.GetStatusIDByName(selectedStatus);

            foreach (string storeName in selectedStores)
            {
                int storeId = _storeRepository.GetStoreIDByName(storeName);

                foreach (string plattformName in selectedPlattforms)
                {
                    int plattformId = _plattformRepository.GetPlattformIDByName(plattformName);

                    // Überprüft ob es die kombination gibt, wenn nicht überspringe sonst FK verletzung  
                    if (!_store_PlattformRepository.StorePlattformExists(storeId, plattformId))
                    {
                        // Kombination existiert nicht – überspringen
                        continue;
                    }


                    var gsp = new Game_Store_Plattform
                    {
                        GameID = NewGame.GID,
                        StoreID = storeId,
                        PlattformID = plattformId,
                        StatusID = statusId,
                        AltersangabeID = altersangabeId,
                        Description = DescriptionTxt.Text
                    };

                    _game_Store_PlattformRepository.AddGame_Store_Plattform(gsp);
                }
            }

        }




        private void ShowErrors(List<string> errors, int max)
        {
            MessageBoxIcon icon;
            MessageBoxButtons buttons;
            string text = null;

            icon = MessageBoxIcon.Error;
            buttons = MessageBoxButtons.OK;

            if (max > errors.Count)
                max = errors.Count;

            for (int i = 0; i < max; i++)
            {
                text += errors[i] + "\n";
            }

            MessageBox.Show(text, "", buttons, icon);
        }

        private void ClearBtn_Click(object sender, EventArgs e)
        {
            ClearAllFields();
        }

        private void ClearAllFields()
        {
            GameTxt.Text = string.Empty;
            DescriptionTxt.Text = string.Empty;
            ReleaseDatePicker.Text = string.Empty;
            PriceNum.Value = 0;
            OwnershipCbx.Text = string.Empty;            
            ImagePathTxt.Text = string.Empty;
            PlayerCountCbx.Text = string.Empty;
            UskCbx.Text = string.Empty;
            GenreFlow.Controls.Clear();
            StoreFlow.Controls.Clear();
            PlattfromFlow.Controls.Clear();
            selectedStores.Clear();
            selectedPlattforms.Clear();
            selectedGenres.Clear();
        }


        private void ImagePathBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Bilddateien (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png|Alle Dateien (*.*)|*.*";
            openFileDialog.Title = "Select Image";


            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Dateipfad in die TextBox schreiben
                ImagePathTxt.Text = openFileDialog.FileName;
            }
        }

        private void AddEditGames_Load(object sender, EventArgs e)
        {            
                EditBtn.Visible = false;
                GenreSelectionForm form = _serviceProvider.GetRequiredService<GenreSelectionForm>();


                if (_game != null)
                {

                    SaveBtn.Visible = false;
                    EditBtn.Visible = true;


                    // Lade daten in AddEditform:


                    var gsp = _game_Store_PlattformRepository.GetGSPGameById(_game.GID);
                    if (gsp != null)
                    {                       
                        DescriptionTxt.Text = gsp.Description;                       
                    }




                    GameTxt.Text = _game.GameName;
                    ReleaseDatePicker.Value = _game.ReleaseDate;
                    // _game.Price ist pID
                    decimal priceValue = _priceRepository.GetPriceValueById(_game.Price);
                    PriceNum.Value = priceValue;
                    ImagePathTxt.Text = _game.ImagePath;

                    LoadGenresForGame(_game.GID);

                    //storePF
                    LoadStorePlafftorm(_game.GID);

                    var sz = _spielerzahlRepository.GetSpielerzahlTypes();
                    PlayerCountCbx.DataSource = sz;
                    PlayerCountCbx.DisplayMember = "SpielerzahlName";
                    PlayerCountCbx.ValueMember = "szID";
                    PlayerCountCbx.SelectedValue = _game.SpielerzahlID;


                    var st = _statusRepository.GetStatus();
                    OwnershipCbx.DataSource = st;
                    OwnershipCbx.DisplayMember = "StatusName";
                    OwnershipCbx.ValueMember = "staID";
                OwnershipCbx.SelectedValue = gsp.StatusID;


                    var usk = _altersangabeRepository.GetAltersangabe();
                    UskCbx.DataSource = usk;
                    UskCbx.DisplayMember = "AltersangabeValue";
                    UskCbx.ValueMember = "agID";
                    UskCbx.SelectedValue = gsp.AltersangabeID;


                }

                
            
            


        }

        private void LoadGenresForGame(int gameId)
        {
            var genres = _gameGenreRepository.GetGenresByGameId(gameId);

            GenreFlow.Controls.Clear();

            foreach (var genre in genres)
            {

                selectedGenres.Add(genre.GenreName); 
                AddGenreTag(genre.GenreName);  // GenreName ist der Name der Genre-Eigenschaft
            }
        }

        private void LoadStorePlafftorm(int gameId)
        {


            var sps = _game_Store_PlattformRepository.GetGameStorePlattformFromGame(gameId);

            StoreFlow.Controls.Clear();
            PlattfromFlow.Controls.Clear();

            foreach (var sp in sps)
            {

                if (!selectedPlattforms.Contains(sp.PlattformName))
                {
                    selectedPlattforms.Add(sp.PlattformName);
                    AddPlattformTag(sp.PlattformName);
                }

                if (!selectedStores.Contains(sp.StoreName))
                {
                    selectedStores.Add(sp.StoreName);
                    AddStoreTag(sp.StoreName);
                }

            }



        }

        private void EditGame()
        {
            string spielerzahlName = PlayerCountCbx.Text;
            int spielerzahlID = _spielerzahlRepository.GetSpielerzahlIDByName(spielerzahlName);

            string imagePathforGame = ImagePathTxt.Text;
            byte[] image = File.ReadAllBytes(imagePathforGame);

            decimal priceValue = PriceNum.Value;
            int priceID = _priceRepository.GetPriceIDByValue(priceValue);


            Game game = new Game(GameTxt.Text, ReleaseDatePicker.Value, spielerzahlID, image, ImagePathTxt.Text, priceID, _game.GID);

            _gameRepository.EditGame(game);
        }

        

        private void EditGameStorePlattform()
        {
            if (!int.TryParse(UskCbx.Text, out int selectedUsk))
            {
                OnErrorOccured("Ungültige USK-Angabe.");
                return;
            }

            int altersangabeId = _altersangabeRepository.GetAltersangabeIdByValue(selectedUsk);
            int statusId = _statusRepository.GetStatusIDByName(OwnershipCbx.Text);

            // Schritt 1: Lade alle alten Kombinationen aus DB
            var existingGSPs = _game_Store_PlattformRepository.GetGameStorePlattformFromGame(_game.GID)
                .Select(gsp => (gsp.StoreID, gsp.PlattformID))
                .ToHashSet();  

            // Schritt 2: Baue alle neuen Kombinationen auf
            var newGSPs = new HashSet<(int storeId, int plattformId)>();

            foreach (var storeName in selectedStores)
            {
                int storeId = _storeRepository.GetStoreIDByName(storeName);
                if (storeId == 0) continue;

                foreach (var plattformName in selectedPlattforms)
                {
                    int plattformId = _plattformRepository.GetPlattformIDByName(plattformName);
                    if (plattformId == 0) continue;

                    if (!_store_PlattformRepository.StorePlattformExists(storeId, plattformId))
                        continue;

                    newGSPs.Add((storeId, plattformId));

                    var gsp = new Game_Store_Plattform(_game.GID, storeId, plattformId, statusId, altersangabeId, DescriptionTxt.Text);

                    if (_game_Store_PlattformRepository.Exists(_game.GID, storeId, plattformId))
                        _game_Store_PlattformRepository.EditGameStorePlattform(gsp);
                    else
                        _game_Store_PlattformRepository.AddGame_Store_Plattform(gsp);
                }
            }

            // Schritt 3: Lösche alle alten Kombinationen, die nicht mehr in newGSPs vorkommen
            foreach (var (storeId, plattformId) in existingGSPs)
            {
                if (!newGSPs.Contains((storeId, plattformId)))
                {
                    var toDelete = new Game_Store_Plattform(_game.GID, storeId, plattformId);
                    _game_Store_PlattformRepository.DeleteStore_Plattfrom(toDelete);
                }
            }
        }

        
        private void EditGame_Genre()
        {

            var existingGameGenres = _gameGenreRepository.GetGenresByGameId(_game.GID);         

            //REMOVE              list       command           predicate
            var result = existingGameGenres.Where(s => !selectedGenres.Any(e => e == s.GenreName)); 
            foreach (var genre in result)
            {
                var gameGenre = new GameGenre(_game.GID, genre.genID);
                _gameGenreRepository.DeleteGameGenre(gameGenre);
            }

            //SAVE
            var result1 = selectedGenres.Where(s => !existingGameGenres.Any(e => e.GenreName == s));
            foreach (var s in result1)
            {
                int genreId = _genreRepository.GetGenreIdByName(s);
                var gameGenre = new GameGenre(_game.GID, genreId);
                _gameGenreRepository.AddGameGenre(gameGenre);
            }




            

        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            List<String> errors;

            errors = ValidateInput();

            if (errors.Count > 0)
            {
                ShowErrors(errors, 5);
                return;
            }

            EditGame();
            EditGameStorePlattform();
            EditGame_Genre();

            EditBtn.Visible = false;



            // Wenn AddEdit form OK zurückgibt Close()
            this.DialogResult = DialogResult.OK;
            Close();

            _overviewForm.RefreshGameOverview();

        }



        private void AddPlattformBtn_Click(object sender, EventArgs e)
        {
            // Holt eine Instanz von GenreSelectionForm über Dependency Injection.
            PlattformSelectionForm form = _serviceProvider.GetRequiredService<PlattformSelectionForm>();
            form.StartPosition = FormStartPosition.CenterParent;
            // Öffnet das Formular als modales Fenster. (Benutzer muss es zuerst schließen)



            // form.ShowDialog Öffnet ein modales Fenster.Benutzer muss es zuerst schließen
            if (form.ShowDialog() == DialogResult.OK)
            {

                string selected = form.SelectedPlattform;

                if (!selectedPlattforms.Contains(selected))
                {
                    selectedPlattforms.Add(selected);
                    // AddGenreTag(selected);
                    AddPlattformTag(selected);
                }
                else
                {
                    MessageBox.Show("Die Plattform wurde bereits hinzugefügt.");
                }
            }
        }

       
    }


}
 