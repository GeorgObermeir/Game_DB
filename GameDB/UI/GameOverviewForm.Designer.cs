namespace aGameDB.UI
{
    partial class GameOverviewForm
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
            components = new System.ComponentModel.Container();
            FlowGame = new FlowLayoutPanel();
            AddGameBtn = new Button();
            contextMenuStrip1 = new ContextMenuStrip(components);
            deleteToolStripMenuItem = new ToolStripMenuItem();
            SearchTxt = new TextBox();
            contextMenuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // FlowGame
            // 
            FlowGame.AutoScroll = true;
            FlowGame.Location = new Point(12, 59);
            FlowGame.Name = "FlowGame";
            FlowGame.Size = new Size(927, 493);
            FlowGame.TabIndex = 0;
            // 
            // AddGameBtn
            // 
            AddGameBtn.Location = new Point(794, 12);
            AddGameBtn.Name = "AddGameBtn";
            AddGameBtn.Size = new Size(120, 39);
            AddGameBtn.TabIndex = 36;
            AddGameBtn.Text = "Add Game";
            AddGameBtn.UseVisualStyleBackColor = true;
            AddGameBtn.Click += AddGameBtn_Click;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { deleteToolStripMenuItem });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(108, 26);
            // 
            // deleteToolStripMenuItem
            // 
            deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            deleteToolStripMenuItem.Size = new Size(107, 22);
            deleteToolStripMenuItem.Text = "Delete";
            deleteToolStripMenuItem.Click += deleteToolStripMenuItem_Click;
            // 
            // SearchTxt
            // 
            SearchTxt.Location = new Point(12, 18);
            SearchTxt.Name = "SearchTxt";
            SearchTxt.PlaceholderText = "Search for a game...";
            SearchTxt.Size = new Size(776, 29);
            SearchTxt.TabIndex = 37;
            SearchTxt.TextChanged += SearchTxt_TextChanged;
            // 
            // GameOverviewForm
            // 
            AutoScaleDimensions = new SizeF(9F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(951, 564);
            Controls.Add(SearchTxt);
            Controls.Add(AddGameBtn);
            Controls.Add(FlowGame);
            Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(4);
            Name = "GameOverviewForm";
            Text = "GameOverview";
            Load += GameOverviewForm_Load;
            contextMenuStrip1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button AddGameBtn;
        public FlowLayoutPanel FlowGame;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem deleteToolStripMenuItem;
        private TextBox SearchTxt;
    }
}