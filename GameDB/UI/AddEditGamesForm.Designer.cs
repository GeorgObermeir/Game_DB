namespace GameDB.UI
{
    partial class AddEditGamesForm
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
            GameTxt = new TextBox();
            Game = new Label();
            labl3 = new Label();
            GenreFlow = new FlowLayoutPanel();
            AddGenreBtn = new Button();
            label1 = new Label();
            PlayerCountCbx = new ComboBox();
            UskCbx = new ComboBox();
            label2 = new Label();
            label3 = new Label();
            PriceNum = new NumericUpDown();
            label5 = new Label();
            label7 = new Label();
            DescriptionTxt = new RichTextBox();
            SaveBtn = new Button();
            ReleaseDatePicker = new DateTimePicker();
            AddStoreBtn = new Button();
            StoreFlow = new FlowLayoutPanel();
            ImagePathTxt = new TextBox();
            ClearBtn = new Button();
            OwnershipCbx = new ComboBox();
            ImagePathBtn = new Button();
            EditBtn = new Button();
            AddPlattformBtn = new Button();
            PlattfromFlow = new FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)PriceNum).BeginInit();
            SuspendLayout();
            // 
            // GameTxt
            // 
            GameTxt.Location = new Point(161, 20);
            GameTxt.Name = "GameTxt";
            GameTxt.Size = new Size(337, 35);
            GameTxt.TabIndex = 2;
            // 
            // Game
            // 
            Game.AutoSize = true;
            Game.Location = new Point(12, 23);
            Game.Name = "Game";
            Game.Size = new Size(67, 30);
            Game.TabIndex = 5;
            Game.Text = "Game";
            // 
            // labl3
            // 
            labl3.AutoSize = true;
            labl3.Location = new Point(12, 162);
            labl3.Name = "labl3";
            labl3.Size = new Size(133, 30);
            labl3.TabIndex = 7;
            labl3.Text = "Release Date";
            // 
            // GenreFlow
            // 
            GenreFlow.AutoScroll = true;
            GenreFlow.Location = new Point(759, 115);
            GenreFlow.Name = "GenreFlow";
            GenreFlow.Size = new Size(186, 81);
            GenreFlow.TabIndex = 15;
            // 
            // AddGenreBtn
            // 
            AddGenreBtn.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            AddGenreBtn.Location = new Point(558, 135);
            AddGenreBtn.Name = "AddGenreBtn";
            AddGenreBtn.Size = new Size(182, 38);
            AddGenreBtn.TabIndex = 16;
            AddGenreBtn.Text = "Add Genre:";
            AddGenreBtn.UseVisualStyleBackColor = true;
            AddGenreBtn.Click += AddGenreBtn_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(427, 299);
            label1.Name = "label1";
            label1.Size = new Size(128, 30);
            label1.TabIndex = 24;
            label1.Text = "Player count";
            // 
            // PlayerCountCbx
            // 
            PlayerCountCbx.FormattingEnabled = true;
            PlayerCountCbx.Items.AddRange(new object[] { "Singleplayer", "Multiplayer" });
            PlayerCountCbx.Location = new Point(573, 299);
            PlayerCountCbx.Name = "PlayerCountCbx";
            PlayerCountCbx.Size = new Size(136, 38);
            PlayerCountCbx.TabIndex = 25;
            // 
            // UskCbx
            // 
            UskCbx.FormattingEnabled = true;
            UskCbx.Items.AddRange(new object[] { "0", "6", "12", "16", "18" });
            UskCbx.Location = new Point(815, 299);
            UskCbx.Name = "UskCbx";
            UskCbx.Size = new Size(130, 38);
            UskCbx.TabIndex = 27;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(743, 302);
            label2.Name = "label2";
            label2.Size = new Size(50, 30);
            label2.TabIndex = 26;
            label2.Text = "USK";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(18, 232);
            label3.Name = "label3";
            label3.Size = new Size(112, 30);
            label3.TabIndex = 30;
            label3.Text = "Ownership";
            // 
            // PriceNum
            // 
            PriceNum.DecimalPlaces = 2;
            PriceNum.Location = new Point(409, 230);
            PriceNum.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            PriceNum.Name = "PriceNum";
            PriceNum.Size = new Size(108, 35);
            PriceNum.TabIndex = 32;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(345, 236);
            label5.Name = "label5";
            label5.Size = new Size(58, 30);
            label5.TabIndex = 31;
            label5.Text = "Price";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(12, 89);
            label7.Name = "label7";
            label7.Size = new Size(118, 30);
            label7.TabIndex = 38;
            label7.Text = "Description";
            // 
            // DescriptionTxt
            // 
            DescriptionTxt.Location = new Point(161, 72);
            DescriptionTxt.Name = "DescriptionTxt";
            DescriptionTxt.Size = new Size(337, 74);
            DescriptionTxt.TabIndex = 39;
            DescriptionTxt.Text = "";
            // 
            // SaveBtn
            // 
            SaveBtn.Location = new Point(13, 352);
            SaveBtn.Name = "SaveBtn";
            SaveBtn.Size = new Size(932, 45);
            SaveBtn.TabIndex = 43;
            SaveBtn.Text = "Save";
            SaveBtn.UseVisualStyleBackColor = true;
            SaveBtn.Click += SaveBtn_Click;
            // 
            // ReleaseDatePicker
            // 
            ReleaseDatePicker.Location = new Point(161, 165);
            ReleaseDatePicker.Name = "ReleaseDatePicker";
            ReleaseDatePicker.Size = new Size(334, 35);
            ReleaseDatePicker.TabIndex = 44;
            // 
            // AddStoreBtn
            // 
            AddStoreBtn.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            AddStoreBtn.Location = new Point(558, 46);
            AddStoreBtn.Name = "AddStoreBtn";
            AddStoreBtn.Size = new Size(182, 38);
            AddStoreBtn.TabIndex = 49;
            AddStoreBtn.Text = "Add Store:";
            AddStoreBtn.UseVisualStyleBackColor = true;
            AddStoreBtn.Click += AddStoreBtn_Click;
            // 
            // StoreFlow
            // 
            StoreFlow.AutoScroll = true;
            StoreFlow.Location = new Point(760, 24);
            StoreFlow.Name = "StoreFlow";
            StoreFlow.Size = new Size(186, 81);
            StoreFlow.TabIndex = 15;
            // 
            // ImagePathTxt
            // 
            ImagePathTxt.Location = new Point(199, 301);
            ImagePathTxt.Name = "ImagePathTxt";
            ImagePathTxt.Size = new Size(222, 35);
            ImagePathTxt.TabIndex = 54;
            // 
            // ClearBtn
            // 
            ClearBtn.Location = new Point(12, 403);
            ClearBtn.Name = "ClearBtn";
            ClearBtn.Size = new Size(933, 45);
            ClearBtn.TabIndex = 55;
            ClearBtn.Text = "Clear all fields";
            ClearBtn.UseVisualStyleBackColor = true;
            ClearBtn.Click += ClearBtn_Click;
            // 
            // OwnershipCbx
            // 
            OwnershipCbx.FormattingEnabled = true;
            OwnershipCbx.Items.AddRange(new object[] { "Owned", "Not Owned", "Interested" });
            OwnershipCbx.Location = new Point(161, 229);
            OwnershipCbx.Name = "OwnershipCbx";
            OwnershipCbx.Size = new Size(160, 38);
            OwnershipCbx.TabIndex = 56;
            // 
            // ImagePathBtn
            // 
            ImagePathBtn.Location = new Point(12, 299);
            ImagePathBtn.Name = "ImagePathBtn";
            ImagePathBtn.Size = new Size(181, 37);
            ImagePathBtn.TabIndex = 57;
            ImagePathBtn.Text = "Select Image:";
            ImagePathBtn.UseVisualStyleBackColor = true;
            ImagePathBtn.Click += ImagePathBtn_Click;
            // 
            // EditBtn
            // 
            EditBtn.Location = new Point(12, 352);
            EditBtn.Name = "EditBtn";
            EditBtn.Size = new Size(931, 45);
            EditBtn.TabIndex = 58;
            EditBtn.Text = "Edit game";
            EditBtn.UseVisualStyleBackColor = true;
            EditBtn.Click += EditBtn_Click;
            // 
            // AddPlattformBtn
            // 
            AddPlattformBtn.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            AddPlattformBtn.Location = new Point(558, 228);
            AddPlattformBtn.Name = "AddPlattformBtn";
            AddPlattformBtn.Size = new Size(182, 38);
            AddPlattformBtn.TabIndex = 59;
            AddPlattformBtn.Text = "Add Plattform:";
            AddPlattformBtn.UseVisualStyleBackColor = true;
            AddPlattformBtn.Click += AddPlattformBtn_Click;
            // 
            // PlattfromFlow
            // 
            PlattfromFlow.AutoScroll = true;
            PlattfromFlow.Location = new Point(760, 202);
            PlattfromFlow.Name = "PlattfromFlow";
            PlattfromFlow.Size = new Size(186, 81);
            PlattfromFlow.TabIndex = 60;
            // 
            // AddEditGamesForm
            // 
            AutoScaleDimensions = new SizeF(12F, 30F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(955, 454);
            Controls.Add(PlattfromFlow);
            Controls.Add(AddPlattformBtn);
            Controls.Add(EditBtn);
            Controls.Add(ImagePathBtn);
            Controls.Add(OwnershipCbx);
            Controls.Add(ClearBtn);
            Controls.Add(ImagePathTxt);
            Controls.Add(StoreFlow);
            Controls.Add(AddStoreBtn);
            Controls.Add(ReleaseDatePicker);
            Controls.Add(SaveBtn);
            Controls.Add(DescriptionTxt);
            Controls.Add(label7);
            Controls.Add(PriceNum);
            Controls.Add(label5);
            Controls.Add(label3);
            Controls.Add(UskCbx);
            Controls.Add(label2);
            Controls.Add(PlayerCountCbx);
            Controls.Add(label1);
            Controls.Add(AddGenreBtn);
            Controls.Add(GenreFlow);
            Controls.Add(labl3);
            Controls.Add(Game);
            Controls.Add(GameTxt);
            Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(5, 6, 5, 6);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "AddEditGamesForm";
            Text = "Game";
            Load += AddEditGames_Load;
            ((System.ComponentModel.ISupportInitialize)PriceNum).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TextBox GameTxt;
        private Label Game;
        private Label labl3;
        private FlowLayoutPanel GenreFlow;
        private Button AddGenreBtn;
        private Label label1;
        private ComboBox PlayerCountCbx;
        private ComboBox UskCbx;
        private Label label2;
        private Label label3;
        private NumericUpDown PriceNum;
        private Label label5;
        private Label label7;
        private RichTextBox DescriptionTxt;
        private Button SaveBtn;
        private DateTimePicker ReleaseDatePicker;
        private Button AddStoreBtn;
        private FlowLayoutPanel StoreFlow;
        private TextBox ImagePathTxt;
        private Button ClearBtn;
        private ComboBox OwnershipCbx;
        private Button ImagePathBtn;
        private Button EditBtn;
        private Button AddPlattformBtn;
        private FlowLayoutPanel PlattfromFlow;
    }
}