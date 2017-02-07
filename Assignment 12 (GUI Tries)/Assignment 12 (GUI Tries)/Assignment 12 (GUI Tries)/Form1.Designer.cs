namespace Assignment_12__GUI_Tries_
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.InputPannel = new System.Windows.Forms.Panel();
            this.AutoBuildTreieCheckBox = new System.Windows.Forms.CheckBox();
            this.BuildTrieButton = new System.Windows.Forms.Button();
            this.GetInputButton = new System.Windows.Forms.Button();
            this.DictionaryBox = new System.Windows.Forms.TextBox();
            this.GetInputButtonToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.BuildTrieToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.OutputPanel = new System.Windows.Forms.Panel();
            this.StringsListBox = new System.Windows.Forms.ListBox();
            this.InputBox = new System.Windows.Forms.TextBox();
            this.GetChildrenOfRootButton = new System.Windows.Forms.Button();
            this.OutputTextBox = new System.Windows.Forms.TextBox();
            this.InputPannel.SuspendLayout();
            this.OutputPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // InputPannel
            // 
            this.InputPannel.Controls.Add(this.AutoBuildTreieCheckBox);
            this.InputPannel.Controls.Add(this.BuildTrieButton);
            this.InputPannel.Controls.Add(this.GetInputButton);
            this.InputPannel.Controls.Add(this.DictionaryBox);
            this.InputPannel.Dock = System.Windows.Forms.DockStyle.Left;
            this.InputPannel.Location = new System.Drawing.Point(0, 0);
            this.InputPannel.Name = "InputPannel";
            this.InputPannel.Size = new System.Drawing.Size(341, 561);
            this.InputPannel.TabIndex = 0;
            // 
            // AutoBuildTreieCheckBox
            // 
            this.AutoBuildTreieCheckBox.AutoSize = true;
            this.AutoBuildTreieCheckBox.Checked = true;
            this.AutoBuildTreieCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.AutoBuildTreieCheckBox.Location = new System.Drawing.Point(93, 32);
            this.AutoBuildTreieCheckBox.Name = "AutoBuildTreieCheckBox";
            this.AutoBuildTreieCheckBox.Size = new System.Drawing.Size(125, 17);
            this.AutoBuildTreieCheckBox.TabIndex = 4;
            this.AutoBuildTreieCheckBox.Text = "Automagicly build trie";
            this.AutoBuildTreieCheckBox.UseVisualStyleBackColor = true;
            this.AutoBuildTreieCheckBox.CheckedChanged += new System.EventHandler(this.AutoBuildTreieCheckBox_CheckedChanged);
            // 
            // BuildTrieButton
            // 
            this.BuildTrieButton.Enabled = false;
            this.BuildTrieButton.Location = new System.Drawing.Point(241, 28);
            this.BuildTrieButton.Name = "BuildTrieButton";
            this.BuildTrieButton.Size = new System.Drawing.Size(75, 23);
            this.BuildTrieButton.TabIndex = 2;
            this.BuildTrieButton.Text = "Build Trie";
            this.BuildTrieToolTip.SetToolTip(this.BuildTrieButton, "Builds the trie with strings from the dictionary");
            this.BuildTrieButton.UseVisualStyleBackColor = true;
            this.BuildTrieButton.Click += new System.EventHandler(this.BuildTrieButton_Click);
            // 
            // GetInputButton
            // 
            this.GetInputButton.Location = new System.Drawing.Point(12, 28);
            this.GetInputButton.Name = "GetInputButton";
            this.GetInputButton.Size = new System.Drawing.Size(75, 23);
            this.GetInputButton.TabIndex = 1;
            this.GetInputButton.Text = "Get Input";
            this.GetInputButtonToolTip.SetToolTip(this.GetInputButton, "Shows the open file dialog\r\nthen loads the contents of selected file into textbox" +
        " below\r\n\r\n");
            this.GetInputButton.UseVisualStyleBackColor = true;
            this.GetInputButton.Click += new System.EventHandler(this.GetInputButton_Click);
            // 
            // DictionaryBox
            // 
            this.DictionaryBox.Location = new System.Drawing.Point(3, 57);
            this.DictionaryBox.Multiline = true;
            this.DictionaryBox.Name = "DictionaryBox";
            this.DictionaryBox.ReadOnly = true;
            this.DictionaryBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.DictionaryBox.Size = new System.Drawing.Size(335, 492);
            this.DictionaryBox.TabIndex = 0;
            // 
            // GetInputButtonToolTip
            // 
            this.GetInputButtonToolTip.Tag = "stuff and things";
            // 
            // OutputPanel
            // 
            this.OutputPanel.Controls.Add(this.StringsListBox);
            this.OutputPanel.Controls.Add(this.InputBox);
            this.OutputPanel.Controls.Add(this.GetChildrenOfRootButton);
            this.OutputPanel.Controls.Add(this.OutputTextBox);
            this.OutputPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.OutputPanel.Location = new System.Drawing.Point(347, 0);
            this.OutputPanel.Name = "OutputPanel";
            this.OutputPanel.Size = new System.Drawing.Size(437, 561);
            this.OutputPanel.TabIndex = 1;
            // 
            // StringsListBox
            // 
            this.StringsListBox.BackColor = System.Drawing.SystemColors.Control;
            this.StringsListBox.FormattingEnabled = true;
            this.StringsListBox.Location = new System.Drawing.Point(3, 83);
            this.StringsListBox.Name = "StringsListBox";
            this.StringsListBox.Size = new System.Drawing.Size(184, 459);
            this.StringsListBox.TabIndex = 5;
            this.StringsListBox.DoubleClick += new System.EventHandler(this.StringsListBox_DoubleClick);
            // 
            // InputBox
            // 
            this.InputBox.Enabled = false;
            this.InputBox.Location = new System.Drawing.Point(4, 57);
            this.InputBox.Name = "InputBox";
            this.InputBox.Size = new System.Drawing.Size(183, 20);
            this.InputBox.TabIndex = 4;
            this.InputBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.InputBox_KeyUp);
            // 
            // GetChildrenOfRootButton
            // 
            this.GetChildrenOfRootButton.Enabled = false;
            this.GetChildrenOfRootButton.Location = new System.Drawing.Point(3, 28);
            this.GetChildrenOfRootButton.Name = "GetChildrenOfRootButton";
            this.GetChildrenOfRootButton.Size = new System.Drawing.Size(130, 23);
            this.GetChildrenOfRootButton.TabIndex = 1;
            this.GetChildrenOfRootButton.Text = "Dispaly Childern of Root";
            this.GetChildrenOfRootButton.UseVisualStyleBackColor = true;
            this.GetChildrenOfRootButton.Click += new System.EventHandler(this.GetChildrenOfRootButton_Click);
            // 
            // OutputTextBox
            // 
            this.OutputTextBox.Location = new System.Drawing.Point(193, 83);
            this.OutputTextBox.Multiline = true;
            this.OutputTextBox.Name = "OutputTextBox";
            this.OutputTextBox.ReadOnly = true;
            this.OutputTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.OutputTextBox.Size = new System.Drawing.Size(242, 459);
            this.OutputTextBox.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.OutputPanel);
            this.Controls.Add(this.InputPannel);
            this.Name = "Form1";
            this.Text = "Form1";
            this.InputPannel.ResumeLayout(false);
            this.InputPannel.PerformLayout();
            this.OutputPanel.ResumeLayout(false);
            this.OutputPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel InputPannel;
        private System.Windows.Forms.Button GetInputButton;
        private System.Windows.Forms.TextBox DictionaryBox;
        private System.Windows.Forms.ToolTip GetInputButtonToolTip;
        private System.Windows.Forms.Button BuildTrieButton;
        private System.Windows.Forms.ToolTip BuildTrieToolTip;
        private System.Windows.Forms.Panel OutputPanel;
        private System.Windows.Forms.Button GetChildrenOfRootButton;
        private System.Windows.Forms.TextBox OutputTextBox;
        private System.Windows.Forms.TextBox InputBox;
        private System.Windows.Forms.CheckBox AutoBuildTreieCheckBox;
        private System.Windows.Forms.ListBox StringsListBox;
    }
}

