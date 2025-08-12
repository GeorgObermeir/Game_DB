namespace aGameDB.UI
{
    partial class GenreSelectionForm
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
            GenreSelectCbx = new ComboBox();
            btnOKGenre = new Button();
            SuspendLayout();
            // 
            // GenreSelectCbx
            // 
            GenreSelectCbx.FormattingEnabled = true;
            GenreSelectCbx.Location = new Point(12, 22);
            GenreSelectCbx.Name = "GenreSelectCbx";
            GenreSelectCbx.Size = new Size(214, 23);
            GenreSelectCbx.TabIndex = 0;
            // 
            // btnOKGenre
            // 
            btnOKGenre.Location = new Point(74, 60);
            btnOKGenre.Name = "btnOKGenre";
            btnOKGenre.Size = new Size(91, 43);
            btnOKGenre.TabIndex = 1;
            btnOKGenre.Text = "OK";
            btnOKGenre.UseVisualStyleBackColor = true;
            btnOKGenre.Click += btnOK_Click;
            // 
            // GenreSelectionForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(235, 115);
            Controls.Add(btnOKGenre);
            Controls.Add(GenreSelectCbx);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "GenreSelectionForm";
            Text = "GenreSelection";
            Load += GenreSelectionForm_Load;
            ResumeLayout(false);
        }

        #endregion

        private ComboBox GenreSelectCbx;
        private Button btnOKGenre;
    }
}