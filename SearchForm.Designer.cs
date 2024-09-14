namespace SeiyuuSync
{
    partial class SearchForm
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
            textBox1 = new TextBox();
            AddButton = new Button();
            CompareButton = new Button();
            dgvAnimeList = new DataGridView();
            Column1 = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)dgvAnimeList).BeginInit();
            SuspendLayout();
            // 
            // textBox1
            // 
            textBox1.ForeColor = SystemColors.ScrollBar;
            textBox1.Location = new Point(270, 74);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(267, 31);
            textBox1.TabIndex = 0;
            textBox1.Text = "search anime...";
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // AddButton
            // 
            AddButton.Location = new Point(200, 705);
            AddButton.Name = "AddButton";
            AddButton.Size = new Size(112, 34);
            AddButton.TabIndex = 1;
            AddButton.Text = "Add";
            AddButton.UseVisualStyleBackColor = true;
            AddButton.Click += button1_Click;
            // 
            // CompareButton
            // 
            CompareButton.Location = new Point(425, 705);
            CompareButton.Name = "CompareButton";
            CompareButton.Size = new Size(112, 34);
            CompareButton.TabIndex = 3;
            CompareButton.Text = "Compare";
            CompareButton.UseVisualStyleBackColor = true;
            // 
            // dgvAnimeList
            // 
            dgvAnimeList.AllowUserToAddRows = false;
            dgvAnimeList.AllowUserToDeleteRows = false;
            dgvAnimeList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvAnimeList.Columns.AddRange(new DataGridViewColumn[] { Column1 });
            dgvAnimeList.Location = new Point(211, 178);
            dgvAnimeList.Name = "dgvAnimeList";
            dgvAnimeList.ReadOnly = true;
            dgvAnimeList.RowHeadersVisible = false;
            dgvAnimeList.RowHeadersWidth = 62;
            dgvAnimeList.Size = new Size(360, 398);
            dgvAnimeList.TabIndex = 4;
            dgvAnimeList.CellContentClick += dataGridView1_CellContentClick_1;
            // 
            // Column1
            // 
            Column1.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Column1.HeaderText = "Column1";
            Column1.MinimumWidth = 8;
            Column1.Name = "Column1";
            Column1.ReadOnly = true;
            // 
            // SearchForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 761);
            Controls.Add(dgvAnimeList);
            Controls.Add(CompareButton);
            Controls.Add(AddButton);
            Controls.Add(textBox1);
            Name = "SearchForm";
            Text = "SearchForm";
            ((System.ComponentModel.ISupportInitialize)dgvAnimeList).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBox1;
        private Button AddButton;
        private Button CompareButton;
        private DataGridView dgvAnimeList;
        private DataGridViewTextBoxColumn Column1;
    }
}