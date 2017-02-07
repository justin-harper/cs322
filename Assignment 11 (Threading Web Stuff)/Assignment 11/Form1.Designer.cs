namespace Assignment_11__Threading_Web_Stuff_
{
    partial class Assignment11
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
            this.StartSortingButton = new System.Windows.Forms.Button();
            this.URLBox = new System.Windows.Forms.TextBox();
            this.URLLabel = new System.Windows.Forms.Label();
            this.SortingTextBox = new System.Windows.Forms.TextBox();
            this.DownloadHTMLButton = new System.Windows.Forms.Button();
            this.WebPanel = new System.Windows.Forms.Panel();
            this.HTMLBox = new System.Windows.Forms.TextBox();
            this.SortPanel = new System.Windows.Forms.Panel();
            this.ResetButton = new System.Windows.Forms.Button();
            this.Author = new System.Windows.Forms.Label();
            this.WebPanel.SuspendLayout();
            this.SortPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // StartSortingButton
            // 
            this.StartSortingButton.Location = new System.Drawing.Point(3, 3);
            this.StartSortingButton.Name = "StartSortingButton";
            this.StartSortingButton.Size = new System.Drawing.Size(245, 23);
            this.StartSortingButton.TabIndex = 0;
            this.StartSortingButton.Text = "Start Sorting";
            this.StartSortingButton.UseVisualStyleBackColor = true;
            this.StartSortingButton.Click += new System.EventHandler(this.StartSortingButton_Click);
            // 
            // URLBox
            // 
            this.URLBox.AcceptsReturn = true;
            this.URLBox.Location = new System.Drawing.Point(41, 6);
            this.URLBox.Name = "URLBox";
            this.URLBox.Size = new System.Drawing.Size(241, 20);
            this.URLBox.TabIndex = 2;
            this.URLBox.Text = "http://www.wsu.edu";
            // 
            // URLLabel
            // 
            this.URLLabel.AutoSize = true;
            this.URLLabel.Location = new System.Drawing.Point(6, 10);
            this.URLLabel.Name = "URLLabel";
            this.URLLabel.Size = new System.Drawing.Size(29, 13);
            this.URLLabel.TabIndex = 3;
            this.URLLabel.Text = "URL";
            // 
            // SortingTextBox
            // 
            this.SortingTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.SortingTextBox.BackColor = System.Drawing.SystemColors.Control;
            this.SortingTextBox.Location = new System.Drawing.Point(3, 32);
            this.SortingTextBox.Multiline = true;
            this.SortingTextBox.Name = "SortingTextBox";
            this.SortingTextBox.ReadOnly = true;
            this.SortingTextBox.Size = new System.Drawing.Size(245, 495);
            this.SortingTextBox.TabIndex = 2;
            // 
            // DownloadHTMLButton
            // 
            this.DownloadHTMLButton.Location = new System.Drawing.Point(4, 30);
            this.DownloadHTMLButton.Name = "DownloadHTMLButton";
            this.DownloadHTMLButton.Size = new System.Drawing.Size(278, 23);
            this.DownloadHTMLButton.TabIndex = 4;
            this.DownloadHTMLButton.Text = "Get HTML";
            this.DownloadHTMLButton.UseVisualStyleBackColor = true;
            this.DownloadHTMLButton.Click += new System.EventHandler(this.DownloadHTMLButton_Click);
            // 
            // WebPanel
            // 
            this.WebPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.WebPanel.Controls.Add(this.HTMLBox);
            this.WebPanel.Controls.Add(this.DownloadHTMLButton);
            this.WebPanel.Controls.Add(this.URLBox);
            this.WebPanel.Controls.Add(this.URLLabel);
            this.WebPanel.Location = new System.Drawing.Point(487, 20);
            this.WebPanel.Name = "WebPanel";
            this.WebPanel.Size = new System.Drawing.Size(285, 530);
            this.WebPanel.TabIndex = 5;
            // 
            // HTMLBox
            // 
            this.HTMLBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.HTMLBox.BackColor = System.Drawing.SystemColors.Control;
            this.HTMLBox.Location = new System.Drawing.Point(4, 60);
            this.HTMLBox.Multiline = true;
            this.HTMLBox.Name = "HTMLBox";
            this.HTMLBox.ReadOnly = true;
            this.HTMLBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.HTMLBox.Size = new System.Drawing.Size(278, 470);
            this.HTMLBox.TabIndex = 5;
            // 
            // SortPanel
            // 
            this.SortPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.SortPanel.Controls.Add(this.SortingTextBox);
            this.SortPanel.Controls.Add(this.StartSortingButton);
            this.SortPanel.Location = new System.Drawing.Point(12, 20);
            this.SortPanel.Name = "SortPanel";
            this.SortPanel.Size = new System.Drawing.Size(256, 530);
            this.SortPanel.TabIndex = 6;
            // 
            // ResetButton
            // 
            this.ResetButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ResetButton.Location = new System.Drawing.Point(274, 268);
            this.ResetButton.Name = "ResetButton";
            this.ResetButton.Size = new System.Drawing.Size(207, 43);
            this.ResetButton.TabIndex = 7;
            this.ResetButton.Text = "Reset Text Boxes";
            this.ResetButton.UseVisualStyleBackColor = true;
            this.ResetButton.Click += new System.EventHandler(this.ResetButton_Click);
            // 
            // Author
            // 
            this.Author.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Author.AutoSize = true;
            this.Author.Location = new System.Drawing.Point(319, 534);
            this.Author.Name = "Author";
            this.Author.Size = new System.Drawing.Size(133, 13);
            this.Author.TabIndex = 8;
            this.Author.Text = "Justin Harper CS322 SP15";
            // 
            // Assignment11
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.Author);
            this.Controls.Add(this.ResetButton);
            this.Controls.Add(this.SortPanel);
            this.Controls.Add(this.WebPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "Assignment11";
            this.Text = "Form1";            
            this.WebPanel.ResumeLayout(false);
            this.WebPanel.PerformLayout();
            this.SortPanel.ResumeLayout(false);
            this.SortPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button StartSortingButton;
        private System.Windows.Forms.TextBox URLBox;
        private System.Windows.Forms.Label URLLabel;
        private System.Windows.Forms.TextBox SortingTextBox;
        private System.Windows.Forms.Button DownloadHTMLButton;
        private System.Windows.Forms.Panel WebPanel;
        private System.Windows.Forms.TextBox HTMLBox;
        private System.Windows.Forms.Panel SortPanel;
        private System.Windows.Forms.Button ResetButton;
        private System.Windows.Forms.Label Author;
    }
}

