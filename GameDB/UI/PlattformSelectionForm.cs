using DataAccesLayer.Contracts;
using DataAccessLayer.Contracts;
using DataAccessLayer.Repositories;
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

namespace aMirrorGameDB.UI
{
    public partial class PlattformSelectionForm : Form
    {
        public string SelectedPlattform { get; private set; }

        private readonly IPlattformRepository _plattformRepository;


        public PlattformSelectionForm(IPlattformRepository plattformRepository)
        {
            InitializeComponent();

            _plattformRepository = plattformRepository;


        }

        



        private void btnOKPlattform_Click(object sender, EventArgs e)
        {
            if (PlattformSelectCbx.SelectedItem != null)
            {
                Plattform plattform = new Plattform(PlattformSelectCbx.Text);

                _plattformRepository.AddPlattform(plattform);



                SelectedPlattform = (PlattformSelectCbx.SelectedItem as Plattform)?.PlattformName;
                DialogResult = DialogResult.OK;
            }
        }


        private void RefreshPlattfromTypes()
        {

            PlattformSelectCbx.DataSource = _plattformRepository.GetPlattform();
            PlattformSelectCbx.DisplayMember = "PlattformName";


        }

        private void PlattformSelectionForm_Load(object sender, EventArgs e)
        {
            RefreshPlattfromTypes();
        }
    }
}
