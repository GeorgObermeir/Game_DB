namespace aMirrorGameDB.UI
{
    partial class PlattformSelectionForm
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
            PlattformSelectCbx = new ComboBox();
            btnOKPlattform = new Button();
            SuspendLayout();
            // 
            // PlattformSelectCbx
            // 
            PlattformSelectCbx.FormattingEnabled = true;
            PlattformSelectCbx.Items.AddRange(new object[] { "PC", "PS5", "Xbox" });
            PlattformSelectCbx.Location = new Point(28, 24);
            PlattformSelectCbx.Name = "PlattformSelectCbx";
            PlattformSelectCbx.Size = new Size(173, 23);
            PlattformSelectCbx.TabIndex = 54;
            // 
            // btnOKPlattform
            // 
            btnOKPlattform.Location = new Point(60, 53);
            btnOKPlattform.Name = "btnOKPlattform";
            btnOKPlattform.Size = new Size(103, 38);
            btnOKPlattform.TabIndex = 55;
            btnOKPlattform.Text = "OK";
            btnOKPlattform.UseVisualStyleBackColor = true;
            btnOKPlattform.Click += btnOKPlattform_Click;
            // 
            // PlattformSelectionForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(235, 103);
            Controls.Add(btnOKPlattform);
            Controls.Add(PlattformSelectCbx);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MinimizeBox = false;
            Name = "PlattformSelectionForm";
            Text = "PlattformSelectionForm";
            Load += PlattformSelectionForm_Load;
            ResumeLayout(false);
        }

        #endregion

        private ComboBox PlattformSelectCbx;
        private Button btnOKPlattform;
    }
}