namespace aGameDB.UI
{
    partial class StoreSelectionForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnOKStore = new Button();
            StoreSelectCbx = new ComboBox();
            SuspendLayout();
            // 
            // btnOKStore
            // 
            btnOKStore.Location = new Point(47, 50);
            btnOKStore.Name = "btnOKStore";
            btnOKStore.Size = new Size(103, 38);
            btnOKStore.TabIndex = 0;
            btnOKStore.Text = "OK";
            btnOKStore.UseVisualStyleBackColor = true;
            btnOKStore.Click += btnOKStore_Click;
            // 
            // StoreSelectCbx
            // 
            StoreSelectCbx.FormattingEnabled = true;
            StoreSelectCbx.Items.AddRange(new object[] { "Steam  ", "EpicGames  ", "Origin  ", "Battle.net  ", "PlayStation Store  ", "Xbox Store  ", "Nintendo Store  ", "Mobile Store" });
            StoreSelectCbx.Location = new Point(12, 21);
            StoreSelectCbx.Name = "StoreSelectCbx";
            StoreSelectCbx.Size = new Size(183, 23);
            StoreSelectCbx.TabIndex = 1;
            // 
            // StoreSelectionForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(207, 107);
            Controls.Add(StoreSelectCbx);
            Controls.Add(btnOKStore);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "StoreSelectionForm";
            Text = "StoreSelectionForm";
            Load += StoreSelectionForm_Load;
            ResumeLayout(false);
        }

        #endregion

        private Button btnOKStore;
        private ComboBox StoreSelectCbx;
    }
}