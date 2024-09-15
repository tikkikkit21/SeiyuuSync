namespace SeiyuuSync.Forms
{
    partial class AnimeListForm
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
            tableLayoutPanel1 = new TableLayoutPanel();
            label1 = new Label();
            dgvAnimeList = new DataGridView();
            colAnimeName = new DataGridViewTextBoxColumn();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvAnimeList).BeginInit();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Controls.Add(label1, 0, 0);
            tableLayoutPanel1.Controls.Add(dgvAnimeList, 0, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(284, 450);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Dock = DockStyle.Fill;
            label1.Location = new Point(3, 0);
            label1.Name = "label1";
            label1.Size = new Size(278, 30);
            label1.TabIndex = 0;
            label1.Text = "All My Animes";
            label1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // dgvAnimeList
            // 
            dgvAnimeList.AllowUserToAddRows = false;
            dgvAnimeList.AllowUserToDeleteRows = false;
            dgvAnimeList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvAnimeList.Columns.AddRange(new DataGridViewColumn[] { colAnimeName });
            dgvAnimeList.Dock = DockStyle.Fill;
            dgvAnimeList.Location = new Point(3, 33);
            dgvAnimeList.Name = "dgvAnimeList";
            dgvAnimeList.ReadOnly = true;
            dgvAnimeList.RowHeadersVisible = false;
            dgvAnimeList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAnimeList.Size = new Size(278, 414);
            dgvAnimeList.TabIndex = 1;
            // 
            // colAnimeName
            // 
            colAnimeName.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colAnimeName.HeaderText = "Anime";
            colAnimeName.Name = "colAnimeName";
            colAnimeName.ReadOnly = true;
            // 
            // AnimeListForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(284, 450);
            Controls.Add(tableLayoutPanel1);
            Name = "AnimeListForm";
            Text = "AnimeListForm";
            Load += AnimeListForm_Load;
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvAnimeList).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private Label label1;
        private DataGridView dgvAnimeList;
        private DataGridViewTextBoxColumn colAnimeName;
    }
}