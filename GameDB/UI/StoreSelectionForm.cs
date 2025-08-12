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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace aGameDB.UI
{
    public partial class StoreSelectionForm : Form
    {
        public string SelectedStore { get; private set; }

        private readonly IStoreRepository _storeRepository;
        public StoreSelectionForm(IStoreRepository storeRepository)
        {
            InitializeComponent();
            _storeRepository = storeRepository;

            

        }

       


        private void btnOKStore_Click(object sender, EventArgs e)
        {
            if (StoreSelectCbx.SelectedItem != null)
            {
                Store store = new Store(StoreSelectCbx.Text);

                _storeRepository.AddStore(store);


                SelectedStore = (StoreSelectCbx.SelectedItem as Store)?.StoreName;
                DialogResult = DialogResult.OK;

            }
        }


        private void RefreshStoreTypes()
        {

            StoreSelectCbx.DataSource = _storeRepository.GetStore();
            StoreSelectCbx.DisplayMember = "StoreName";

            
        }
      

        private void StoreSelectionForm_Load(object sender, EventArgs e)
        {
            RefreshStoreTypes();
        }
    }
}
