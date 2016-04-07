namespace RssReaderTestTaskByRelex
{
    partial class UserForm
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
            this.userTabControl = new System.Windows.Forms.TabControl();
            this.rssTabPage = new System.Windows.Forms.TabPage();
            this.urlAdressLinkLabel = new System.Windows.Forms.LinkLabel();
            this.discriptionTextBox = new System.Windows.Forms.TextBox();
            this.titleComboBox = new System.Windows.Forms.ComboBox();
            this.channelComboBox = new System.Windows.Forms.ComboBox();
            this.userAccountTabPage = new System.Windows.Forms.TabPage();
            this.editDataButton = new System.Windows.Forms.Button();
            this.selectRssButton = new System.Windows.Forms.Button();
            this.rssCheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.userBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.userTabControl.SuspendLayout();
            this.rssTabPage.SuspendLayout();
            this.userAccountTabPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // userTabControl
            // 
            this.userTabControl.Controls.Add(this.rssTabPage);
            this.userTabControl.Controls.Add(this.userAccountTabPage);
            this.userTabControl.Location = new System.Drawing.Point(12, 12);
            this.userTabControl.Name = "userTabControl";
            this.userTabControl.SelectedIndex = 0;
            this.userTabControl.Size = new System.Drawing.Size(428, 205);
            this.userTabControl.TabIndex = 0;
            // 
            // rssTabPage
            // 
            this.rssTabPage.Controls.Add(this.urlAdressLinkLabel);
            this.rssTabPage.Controls.Add(this.discriptionTextBox);
            this.rssTabPage.Controls.Add(this.titleComboBox);
            this.rssTabPage.Controls.Add(this.channelComboBox);
            this.rssTabPage.Location = new System.Drawing.Point(4, 22);
            this.rssTabPage.Name = "rssTabPage";
            this.rssTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.rssTabPage.Size = new System.Drawing.Size(420, 179);
            this.rssTabPage.TabIndex = 0;
            this.rssTabPage.Text = "Мои Новости";
            this.rssTabPage.UseVisualStyleBackColor = true;
            this.rssTabPage.Enter += new System.EventHandler(this.rssTabPage_Enter);
            // 
            // urlAdressLinkLabel
            // 
            this.urlAdressLinkLabel.AutoSize = true;
            this.urlAdressLinkLabel.Location = new System.Drawing.Point(6, 162);
            this.urlAdressLinkLabel.Name = "urlAdressLinkLabel";
            this.urlAdressLinkLabel.Size = new System.Drawing.Size(40, 13);
            this.urlAdressLinkLabel.TabIndex = 3;
            this.urlAdressLinkLabel.TabStop = true;
            this.urlAdressLinkLabel.Text = "Go To:";
            this.urlAdressLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.adressLinkLabel_LinkClicked);
            // 
            // discriptionTextBox
            // 
            this.discriptionTextBox.Location = new System.Drawing.Point(6, 60);
            this.discriptionTextBox.Multiline = true;
            this.discriptionTextBox.Name = "discriptionTextBox";
            this.discriptionTextBox.Size = new System.Drawing.Size(408, 99);
            this.discriptionTextBox.TabIndex = 2;
            // 
            // titleComboBox
            // 
            this.titleComboBox.FormattingEnabled = true;
            this.titleComboBox.Location = new System.Drawing.Point(6, 33);
            this.titleComboBox.Name = "titleComboBox";
            this.titleComboBox.Size = new System.Drawing.Size(408, 21);
            this.titleComboBox.TabIndex = 1;
            this.titleComboBox.SelectedIndexChanged += new System.EventHandler(this.titleComboBox_SelectedIndexChanged);
            // 
            // channelComboBox
            // 
            this.channelComboBox.FormattingEnabled = true;
            this.channelComboBox.Location = new System.Drawing.Point(6, 6);
            this.channelComboBox.Name = "channelComboBox";
            this.channelComboBox.Size = new System.Drawing.Size(408, 21);
            this.channelComboBox.TabIndex = 0;
            this.channelComboBox.SelectedIndexChanged += new System.EventHandler(this.channelComboBox_SelectedIndexChanged);
            // 
            // userAccountTabPage
            // 
            this.userAccountTabPage.Controls.Add(this.editDataButton);
            this.userAccountTabPage.Controls.Add(this.selectRssButton);
            this.userAccountTabPage.Controls.Add(this.rssCheckedListBox);
            this.userAccountTabPage.Location = new System.Drawing.Point(4, 22);
            this.userAccountTabPage.Name = "userAccountTabPage";
            this.userAccountTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.userAccountTabPage.Size = new System.Drawing.Size(420, 179);
            this.userAccountTabPage.TabIndex = 1;
            this.userAccountTabPage.Text = "Мои Данные";
            this.userAccountTabPage.UseVisualStyleBackColor = true;
            this.userAccountTabPage.Enter += new System.EventHandler(this.userAccountTabPage_Enter);
            // 
            // editDataButton
            // 
            this.editDataButton.Location = new System.Drawing.Point(6, 151);
            this.editDataButton.Name = "editDataButton";
            this.editDataButton.Size = new System.Drawing.Size(79, 23);
            this.editDataButton.TabIndex = 2;
            this.editDataButton.Text = "мои данные";
            this.editDataButton.UseVisualStyleBackColor = true;
            this.editDataButton.Click += new System.EventHandler(this.editDataButton_Click);
            // 
            // selectRssButton
            // 
            this.selectRssButton.Location = new System.Drawing.Point(339, 151);
            this.selectRssButton.Name = "selectRssButton";
            this.selectRssButton.Size = new System.Drawing.Size(75, 23);
            this.selectRssButton.TabIndex = 1;
            this.selectRssButton.Text = "выбрать";
            this.selectRssButton.UseVisualStyleBackColor = true;
            this.selectRssButton.Click += new System.EventHandler(this.selectRssButton_Click);
            // 
            // rssCheckedListBox
            // 
            this.rssCheckedListBox.FormattingEnabled = true;
            this.rssCheckedListBox.Location = new System.Drawing.Point(6, 6);
            this.rssCheckedListBox.Name = "rssCheckedListBox";
            this.rssCheckedListBox.Size = new System.Drawing.Size(408, 139);
            this.rssCheckedListBox.TabIndex = 0;
            // 
            // userBackgroundWorker
            // 
            this.userBackgroundWorker.WorkerReportsProgress = true;
            this.userBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.userBackgroundWorker_DoWork);
            // 
            // UserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(452, 222);
            this.Controls.Add(this.userTabControl);
            this.Name = "UserForm";
            this.Text = "RSS Новости";
            this.Activated += new System.EventHandler(this.UserForm_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.UserForm_FormClosing);
            this.Load += new System.EventHandler(this.UserForm_Load);
            this.userTabControl.ResumeLayout(false);
            this.rssTabPage.ResumeLayout(false);
            this.rssTabPage.PerformLayout();
            this.userAccountTabPage.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl userTabControl;
        private System.Windows.Forms.TabPage rssTabPage;
        private System.Windows.Forms.TabPage userAccountTabPage;
        private System.Windows.Forms.LinkLabel urlAdressLinkLabel;
        private System.Windows.Forms.TextBox discriptionTextBox;
        private System.Windows.Forms.ComboBox titleComboBox;
        private System.Windows.Forms.ComboBox channelComboBox;
        private System.Windows.Forms.Button selectRssButton;
        private System.Windows.Forms.CheckedListBox rssCheckedListBox;
        private System.Windows.Forms.Button editDataButton;
        private System.ComponentModel.BackgroundWorker userBackgroundWorker;
    }
}