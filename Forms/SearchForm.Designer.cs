﻿namespace SeiyuuSync
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SearchForm));
            tbxSearch = new TextBox();
            dgvAnimeList = new DataGridView();
            colAnimeId = new DataGridViewTextBoxColumn();
            colAnimeName = new DataGridViewTextBoxColumn();
            btnSearch = new Button();
            tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel2 = new TableLayoutPanel();
            CompareButton = new Button();
            AddButton = new Button();
            tableLayoutPanel3 = new TableLayoutPanel();
            voiceActorFlow = new FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)dgvAnimeList).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            SuspendLayout();
            // 
            // tbxSearch
            // 
            tbxSearch.Dock = DockStyle.Fill;
            tbxSearch.ForeColor = SystemColors.ScrollBar;
            tbxSearch.Location = new Point(2, 12);
            tbxSearch.Margin = new Padding(2, 12, 2, 12);
            tbxSearch.Name = "tbxSearch";
            tbxSearch.Size = new Size(228, 23);
            tbxSearch.TabIndex = 0;
            tbxSearch.KeyDown += tbxSearch_KeyDown;
            // 
            // dgvAnimeList
            // 
            dgvAnimeList.AllowUserToAddRows = false;
            dgvAnimeList.AllowUserToDeleteRows = false;
            dgvAnimeList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvAnimeList.Columns.AddRange(new DataGridViewColumn[] { colAnimeId, colAnimeName });
            tableLayoutPanel1.SetColumnSpan(dgvAnimeList, 10);
            dgvAnimeList.Dock = DockStyle.Fill;
            dgvAnimeList.Location = new Point(30, 47);
            dgvAnimeList.Margin = new Padding(2);
            dgvAnimeList.MultiSelect = false;
            dgvAnimeList.Name = "dgvAnimeList";
            dgvAnimeList.ReadOnly = true;
            dgvAnimeList.RowHeadersVisible = false;
            dgvAnimeList.RowHeadersWidth = 62;
            dgvAnimeList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAnimeList.Size = new Size(276, 639);
            dgvAnimeList.TabIndex = 4;
            // 
            // colAnimeId
            // 
            colAnimeId.HeaderText = "ID";
            colAnimeId.MinimumWidth = 25;
            colAnimeId.Name = "colAnimeId";
            colAnimeId.ReadOnly = true;
            colAnimeId.Width = 50;
            // 
            // colAnimeName
            // 
            colAnimeName.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colAnimeName.HeaderText = "Anime";
            colAnimeName.MinimumWidth = 8;
            colAnimeName.Name = "colAnimeName";
            colAnimeName.ReadOnly = true;
            // 
            // btnSearch
            // 
            btnSearch.Dock = DockStyle.Fill;
            btnSearch.Location = new Point(239, 6);
            btnSearch.Margin = new Padding(7, 6, 7, 6);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(86, 29);
            btnSearch.TabIndex = 5;
            btnSearch.Text = "Search";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 20;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5.00003147F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5.00003147F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5.00003147F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5.00003147F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5.00003147F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5.00003147F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5.00003147F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5.00003147F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5.00003147F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5.00003147F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5.00003147F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5.00003147F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5.00003147F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5.00003147F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5.00003147F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5.00003147F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5.00003147F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5.00003147F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5.00003147F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 4.99940538F));
            tableLayoutPanel1.Controls.Add(dgvAnimeList, 1, 1);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 4, 2);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel3, 4, 0);
            tableLayoutPanel1.Controls.Add(voiceActorFlow, 11, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Margin = new Padding(2);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 12F));
            tableLayoutPanel1.Size = new Size(560, 718);
            tableLayoutPanel1.TabIndex = 6;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel1.SetColumnSpan(tableLayoutPanel2, 12);
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Controls.Add(CompareButton, 1, 0);
            tableLayoutPanel2.Controls.Add(AddButton, 0, 0);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(114, 690);
            tableLayoutPanel2.Margin = new Padding(2);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Size = new Size(332, 26);
            tableLayoutPanel2.TabIndex = 6;
            // 
            // CompareButton
            // 
            CompareButton.Dock = DockStyle.Fill;
            CompareButton.Location = new Point(201, 2);
            CompareButton.Margin = new Padding(35, 2, 35, 2);
            CompareButton.Name = "CompareButton";
            CompareButton.Size = new Size(96, 22);
            CompareButton.TabIndex = 3;
            CompareButton.Text = "Compare";
            CompareButton.UseVisualStyleBackColor = true;
            CompareButton.Click += CompareButton_Click;
            // 
            // AddButton
            // 
            AddButton.Dock = DockStyle.Fill;
            AddButton.Location = new Point(35, 2);
            AddButton.Margin = new Padding(35, 2, 35, 2);
            AddButton.Name = "AddButton";
            AddButton.Size = new Size(96, 22);
            AddButton.TabIndex = 1;
            AddButton.Text = "Add";
            AddButton.UseVisualStyleBackColor = true;
            AddButton.Click += AddButton_Click;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 2;
            tableLayoutPanel1.SetColumnSpan(tableLayoutPanel3, 12);
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            tableLayoutPanel3.Controls.Add(tbxSearch, 0, 0);
            tableLayoutPanel3.Controls.Add(btnSearch, 1, 0);
            tableLayoutPanel3.Dock = DockStyle.Fill;
            tableLayoutPanel3.Location = new Point(114, 2);
            tableLayoutPanel3.Margin = new Padding(2);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 1;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.Size = new Size(332, 41);
            tableLayoutPanel3.TabIndex = 7;
            // 
            // voiceActorFlow
            // 
            voiceActorFlow.AutoScroll = true;
            tableLayoutPanel1.SetColumnSpan(voiceActorFlow, 8);
            voiceActorFlow.Dock = DockStyle.Fill;
            voiceActorFlow.FlowDirection = FlowDirection.TopDown;
            voiceActorFlow.Location = new Point(310, 47);
            voiceActorFlow.Margin = new Padding(2);
            voiceActorFlow.Name = "voiceActorFlow";
            voiceActorFlow.Size = new Size(220, 639);
            voiceActorFlow.TabIndex = 8;
            voiceActorFlow.WrapContents = false;
            // 
            // SearchForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(560, 718);
            Controls.Add(tableLayoutPanel1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(2);
            Name = "SearchForm";
            Text = "Search For Anime";
            ((System.ComponentModel.ISupportInitialize)dgvAnimeList).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel3.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TextBox textBox1;
        private DataGridView dgvAnimeList;
        private DataGridViewTextBoxColumn Column1;
        private Button btnSearch;
        private TextBox tbxSearch;
        private TableLayoutPanel tableLayoutPanel1;
        private Button AddButton;
        private Button CompareButton;
        private TableLayoutPanel tableLayoutPanel2;
        private TableLayoutPanel tableLayoutPanel3;
        private DataGridViewTextBoxColumn colAnimeId;
        private DataGridViewTextBoxColumn colAnimeName;
        private FlowLayoutPanel voiceActorFlow;
    }
}