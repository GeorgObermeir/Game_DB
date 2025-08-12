
using DataAccesLayer.Contracts;
using DataAccessLayer.Contracts;
using DataAccessLayer.Repositories;
using DomainModel.Models;
using GameDB.UI;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace aGameDB.UI
{
    public partial class GameOverviewForm : Form
    {
        private List<Game> Games = new List<Game>();
        private List<Game_Store_Plattform> Games_St_Pl = new List<Game_Store_Plattform>();
        private readonly IStoreRepository _storeRepository;
        private readonly IServiceProvider _serviceProvider;
        private readonly IGameRepository _gameRepository;
        private readonly IPriceRepository _priceRepository;
        private readonly ISpielerzahlRepository _spielerzahlRepository;
        private readonly IGenreRepository _genreRepository;
        private readonly IGameGenreRepository _gameGenreRepository;
        private readonly IPlattformRepository _plattfromRepository;
        private readonly IStore_PlattformRepository _store_PlattformRepository;
        private readonly IGame_Store_PlattformRepository _game_Store_PlattformRepository;
        private readonly IAltersangabeRepository _altersangabeRepository;
        private readonly IStatusRepository _statusRepository;

        public GameOverviewForm(IStoreRepository storeRepository,
                                IServiceProvider serviceProvider,
                                IGameRepository gameRepository, IPriceRepository priceRepository,
                                ISpielerzahlRepository spielerzahlRepository,
                                IGenreRepository genreRepository,
                                IGameGenreRepository gameGenreRepository,
                                IPlattformRepository plattfromRepository,
                                IStore_PlattformRepository store_PlattformRepository,
                                IGame_Store_PlattformRepository game_Store_PlattformRepository,
                                IAltersangabeRepository altersangabeRepository, 
                                IStatusRepository statusRepository)
        {
            InitializeComponent();
            _storeRepository = storeRepository;
            _serviceProvider = serviceProvider;
            _gameRepository = gameRepository;
            _priceRepository = priceRepository;
            _spielerzahlRepository = spielerzahlRepository;
            _genreRepository = genreRepository;
            _gameGenreRepository = gameGenreRepository;
            _plattfromRepository = plattfromRepository;
            _store_PlattformRepository = store_PlattformRepository;
            _game_Store_PlattformRepository = game_Store_PlattformRepository;
            _altersangabeRepository = altersangabeRepository;
            _statusRepository = statusRepository;

        }

       
        
        private Image CropToFit(Image img, int targetWidth, int targetHeight)
        {
            float ratioImg = (float)img.Width / img.Height;
            float ratioTarget = (float)targetWidth / targetHeight;

            Rectangle cropArea;

            if (ratioImg > ratioTarget)
            {
                // Bild ist breiter als Ziel: Crop links und rechts
                int width = (int)(img.Height * ratioTarget);
                int x = (img.Width - width) / 2;
                cropArea = new Rectangle(x, 0, width, img.Height);
            }
            else
            {
                // Bild ist höher als Ziel: Crop oben und unten
                int height = (int)(img.Width / ratioTarget);
                int y = (img.Height - height) / 2;
                cropArea = new Rectangle(0, y, img.Width, height);
            }

            Bitmap bmp = new Bitmap(targetWidth, targetHeight);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(img, new Rectangle(0, 0, targetWidth, targetHeight), cropArea, GraphicsUnit.Pixel);
            }

            return bmp;
        }

        private Panel AddGameToUI(Game game)
        {
            //Create panel
            Panel panel = new Panel();
            panel.Name = $"PnlGame{game.GID}";
            panel.BackColor = Color.White;
            panel.Size = new Size(125, 205);
            panel.Margin = new Padding(10);
            panel.Tag = game.GID;

            //Create picture box
            PictureBox picBox = new PictureBox();
            picBox.Name = $"PbGameImage{game.GID}";
            picBox.Size = new Size(100, 148);
            picBox.Location = new Point(12, 10);
            picBox.SizeMode = PictureBoxSizeMode.Normal; // wichtig: keine automatische Skalierung

            if (File.Exists(game.ImagePath))
            {
                int width = picBox.Width > 0 ? picBox.Width : 100;
                int height = picBox.Height > 0 ? picBox.Height : 148;

                var original = Image.FromFile(game.ImagePath);
                var cropped = CropToFit(original, width, height);
                original.Dispose();

                picBox.Image = cropped;
                picBox.SizeMode = PictureBoxSizeMode.Normal;
            }
            else
            {
                picBox.Image = null;
            }


            picBox.Tag = game.GID;

            //Create title label
            Label labelTitle = new Label();
            labelTitle.Name = $"LblGameTitle{game.GID}";
            labelTitle.Text = game.GameName;
            labelTitle.Location = new Point(12, 165);
            labelTitle.ForeColor = Color.Black;
            labelTitle.Font = new Font(this.Font.FontFamily, 9.5f, FontStyle.Regular);
            labelTitle.AutoSize = true;
            labelTitle.Tag = game.GID;

            //Create year label
            Label labelYear = new Label();
            labelYear.Name = $"LblGameYear{game.GID}";
            labelYear.Text = game.ReleaseDate.Year.ToString();
            labelYear.Location = new Point(12, 185);
            labelYear.ForeColor = Color.Gray;
            labelYear.Font = new Font(this.Font.FontFamily, 9.5f, FontStyle.Regular);
            labelYear.Tag = game.GID;

            //Set Context Menu
            panel.ContextMenuStrip = contextMenuStrip1;

            //Add controls to panel 
            panel.Controls.Add(picBox);
            panel.Controls.Add(labelTitle);
            panel.Controls.Add(labelYear);

            //Add Event Handlers 
            panel.MouseClick += new MouseEventHandler(Edit_MouseClick);

            foreach (Control c in panel.Controls)
            {
                c.MouseClick += new MouseEventHandler(Edit_MouseClick);
            }


            panel.MouseEnter += new EventHandler(Panel_MouseEnter);

            panel.MouseLeave += new EventHandler(Panel_MouseLeave);


            panel.MouseEnter += Panel_MouseEnter;
            panel.MouseLeave += Panel_MouseLeave;

            foreach (Control child in panel.Controls)
            {
                child.MouseEnter += Panel_MouseEnter;
                child.MouseLeave += Panel_MouseLeave;
            }



            //Add panel to flowlayoutpanel
            FlowGame.Controls.Add(panel);

            return panel;
        }



        private void Panel_MouseEnter(object sender, EventArgs e)
        {

            Panel panel = sender as Panel;
            PictureBox picBox = sender as PictureBox;
            // Label labelTitle = sender as Label;

            if (panel != null)
            {
                panel.BorderStyle = BorderStyle.FixedSingle;
                panel.BackColor = Color.LightGray;
                panel.Cursor = Cursors.Hand;
            }
        }

        private void Panel_MouseLeave(object sender, EventArgs e)
        {
            Panel panel = GetParentPanel(sender);

            if (panel != null)
            {
                // Prüfen, ob Maus wirklich nicht mehr im Panel ist
                Point mousePos = panel.PointToClient(Cursor.Position);
                if (!panel.ClientRectangle.Contains(mousePos))
                {
                    panel.BorderStyle = BorderStyle.None;
                    panel.BackColor = Color.White;
                    panel.Cursor = Cursors.Default;
                }
            }
        }

        private Panel GetParentPanel(object sender)
        {
            if (sender is Panel p)
                return p;

            if (sender is Control ctrl && ctrl.Parent is Panel parentPanel)
                return parentPanel;

            return null;
        }


        private void Edit_MouseClick(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Left)
            {
                Control c;
                int id;
                int index;
                Game game;
                AddEditGamesForm form;

                //Get game using control tag/id
                c = (Control)sender;
                id = (int)c.Tag;
                //      game = Games.Find(x => x.GID == id);
                game = _gameRepository.GetGameById(id);

                //Open Add/Edit form
                form = new AddEditGamesForm(game, _storeRepository, _serviceProvider, _gameRepository, _priceRepository, _spielerzahlRepository, this, _genreRepository, _gameGenreRepository, _plattfromRepository, _store_PlattformRepository, _game_Store_PlattformRepository, _altersangabeRepository, _statusRepository);


                form.ShowDialog();

                //Update game in list and UI
                if (form.DataSaved)
                {
                    index = Games.FindIndex(x => x.GID == id);
                    Games[index].Copy(form.EditedGame);
                    UpdateGameInUI(Games[index]);
                }




            }

        }



        private void AddGameBtn_Click(object sender, EventArgs e)
        {


            AddEditGamesForm form = new AddEditGamesForm(
                null,               // kein bestehendes Spiel, weil neues Spiel hinzugefügt wird
                _storeRepository,
                _serviceProvider,
                _gameRepository,
                _priceRepository,
                _spielerzahlRepository,
                  this,
                  _genreRepository,
                  _gameGenreRepository,
                  _plattfromRepository,
                  _store_PlattformRepository,
                  _game_Store_PlattformRepository,
                  _altersangabeRepository,
                  _statusRepository

            );

            form.ShowDialog();

            if (form.DataSaved)
            {
                RefreshGameOverview();
            }
        }

        



        private void UpdateGameInUI(Game game)
        {


            Control control;
            PictureBox picBox;
            string name;

            //Find picturebox and update game image
            name = String.Format("PbGameImage{0}", game.GID);
            control = this.Controls.Find(name, true).FirstOrDefault();
            picBox = (PictureBox)control;



            if (File.Exists(game.ImagePath))
                picBox.Image = Image.FromFile(game.ImagePath);
            else
                picBox.Image = null;


            //Find game title label and update text
            name = String.Format("LblGameTitle{0}", game.GID);
            control = this.Controls.Find(name, true).FirstOrDefault();
            control.Text = game.GameName;

            //Find game year label and update text
            name = String.Format("LblGameYear{0}", game.GID);
            control = this.Controls.Find(name, true).FirstOrDefault();
            control.Text = game.ReleaseDate.Year.ToString();
        }

        private void GameOverviewForm_Load(object sender, EventArgs e)
        {
            RefreshGameOverview();


        }

        public void RefreshGameOverview()
        {
            FlowGame.Controls.Clear();

            

            Games = _gameRepository.GetGames();
            

            Games_St_Pl = _game_Store_PlattformRepository.GetGamesGSP();



            foreach (Game game in Games)
            {
                Panel panel = AddGameToUI(game);
            }


        }



        private void DeleteGameFromUIGSP(Game_Store_Plattform game_Store_Plattform)
        {
            Control panel;
            string name;

            //Find panel 
            name = String.Format("PnlGame{0}", game_Store_Plattform.GameID);
            panel = this.Controls.Find(name, true).FirstOrDefault();

            //Remove event handlers
            panel.MouseClick -= new MouseEventHandler(Edit_MouseClick);

            foreach (Control c in panel.Controls)
            {
                c.MouseClick -= new MouseEventHandler(Edit_MouseClick);
            }

            //Remove panel
            FlowGame.Controls.Remove(panel);
            panel.Dispose();
        }



        

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RefreshGameOverview();

            ToolStripMenuItem menuItem;
            ContextMenuStrip menuStrip;
            Control control;
            int id;
            int index = -1;     //-1 als sicherheitsmaßnahme falls x.GID nicht gefunden wird
            Game game;
            Game_Store_Plattform game_Store_Plattform;


            //Find selected control
            menuItem = (ToolStripMenuItem)sender;
            menuStrip = (ContextMenuStrip)menuItem.GetCurrentParent();
            control = menuStrip.SourceControl;

            //Get game from control tag/id
            id = (int)control.Tag;
            game = Games.Find(x => x.GID == id);


            //für gsp
            id = (int)control.Tag;
            game_Store_Plattform = Games_St_Pl.Find(x => x.GameID == id);

            
            

            string allIDs = string.Join(", ", Games_St_Pl.Select(g => g.GameID));
             MessageBox.Show($"Gesuchte ID: {id}\nAlle Game-IDs: {allIDs}");

           

            //Delete gsp from list
            index = Games_St_Pl.FindIndex(x => x.GameID == id);

          
            Games_St_Pl.RemoveAt(index);

            //Delete game from UI
       
            DeleteGameFromUIGSP(game_Store_Plattform);


            //Delete game from Game_Store_Plattform in DB                
            _game_Store_PlattformRepository.DeleteGame_Store_Plattform(game_Store_Plattform);
            
            // Also delete connection game in db
            _gameRepository.DeleteGame(game);

        }

        private void SearchTxt_TextChanged(object sender, EventArgs e)
        {
            FlowGame.Controls.Clear();
                       

            Games = _gameRepository.GetGames(SearchTxt.Text); ;

            foreach (Game game in Games)
            {
                Panel panel = AddGameToUI(game);
            }

            

        }
    }
}
