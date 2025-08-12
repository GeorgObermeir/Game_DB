using DataAccessLayer.Contracts;
using DomainModel.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace aGameDB.UI
{
    public partial class GenreSelectionForm : Form
    {
        public string SelectedGenre { get; private set; }

        private readonly IGenreRepository _genreRepository;
        private readonly IGameGenreRepository _gameGenreRepository;
        private readonly IGameRepository _gameRepository;
        

        public GenreSelectionForm(IGenreRepository genreRepository, IGameGenreRepository gameGenreRepository, IGameRepository gameRepository)
        {
            InitializeComponent();
            _genreRepository = genreRepository;
            _gameGenreRepository = gameGenreRepository;
            _gameRepository = gameRepository;
            
            //GenreSelectCbx.Items.AddRange(genre);
            //if (GenreSelectCbx.Items.Count > 0)
            //    GenreSelectCbx.SelectedIndex = 0;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (GenreSelectCbx.SelectedItem != null)
            {
                Genre genre = new Genre(GenreSelectCbx.Text);

                _genreRepository.AddGenre(genre);
               

                
                SelectedGenre = (GenreSelectCbx.SelectedItem as Genre)?.GenreName;
                DialogResult = DialogResult.OK;
            }
        }

        


        private void RefreshGenreTypes()
        {

            GenreSelectCbx.DataSource = _genreRepository.GetGenre();
            GenreSelectCbx.DisplayMember = "GenreName";
        }

        private void GenreSelectionForm_Load(object sender, EventArgs e)
        {
            RefreshGenreTypes();
        }
    }
}
