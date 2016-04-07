namespace RssReaderTestTaskByRelex
{
    partial class AdministratorForm
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
            this.administratorTabControl = new System.Windows.Forms.TabControl();
            this.rssTabPage = new System.Windows.Forms.TabPage();
            this.selectRssButton = new System.Windows.Forms.Button();
            this.rssCheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.addUrlButton = new System.Windows.Forms.Button();
            this.rssUrlTextBox = new System.Windows.Forms.TextBox();
            this.rssUrlLabel = new System.Windows.Forms.Label();
            this.selectRssTabPage = new System.Windows.Forms.TabPage();
            this.urlAdressLinkLabel = new System.Windows.Forms.LinkLabel();
            this.discriptionTextBox = new System.Windows.Forms.TextBox();
            this.titleComboBox = new System.Windows.Forms.ComboBox();
            this.channelComboBox = new System.Windows.Forms.ComboBox();
            this.editUserTabPage = new System.Windows.Forms.TabPage();
            this.warningLabel = new System.Windows.Forms.Label();
            this.saveDataButton = new System.Windows.Forms.Button();
            this.passwordRepeatTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.loginTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.passwordTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.allUsersComboBox = new System.Windows.Forms.ComboBox();
            this.adminBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.administratorTabControl.SuspendLayout();
            this.rssTabPage.SuspendLayout();
            this.selectRssTabPage.SuspendLayout();
            this.editUserTabPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // administratorTabControl
            // 
            this.administratorTabControl.Controls.Add(this.rssTabPage);
            this.administratorTabControl.Controls.Add(this.selectRssTabPage);
            this.administratorTabControl.Controls.Add(this.editUserTabPage);
            this.administratorTabControl.Location = new System.Drawing.Point(12, 12);
            this.administratorTabControl.Name = "administratorTabControl";
            this.administratorTabControl.SelectedIndex = 0;
            this.administratorTabControl.Size = new System.Drawing.Size(340, 237);
            this.administratorTabControl.TabIndex = 0;
            // 
            // rssTabPage
            // 
            this.rssTabPage.Controls.Add(this.selectRssButton);
            this.rssTabPage.Controls.Add(this.rssCheckedListBox);
            this.rssTabPage.Controls.Add(this.addUrlButton);
            this.rssTabPage.Controls.Add(this.rssUrlTextBox);
            this.rssTabPage.Controls.Add(this.rssUrlLabel);
            this.rssTabPage.Location = new System.Drawing.Point(4, 22);
            this.rssTabPage.Name = "rssTabPage";
            this.rssTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.rssTabPage.Size = new System.Drawing.Size(332, 211);
            this.rssTabPage.TabIndex = 0;
            this.rssTabPage.Text = "RSS";
            this.rssTabPage.UseVisualStyleBackColor = true;
            // 
            // selectRssButton
            // 
            this.selectRssButton.Location = new System.Drawing.Point(251, 185);
            this.selectRssButton.Name = "selectRssButton";
            this.selectRssButton.Size = new System.Drawing.Size(75, 23);
            this.selectRssButton.TabIndex = 5;
            this.selectRssButton.Text = "Выбрать";
            this.selectRssButton.UseVisualStyleBackColor = true;
            this.selectRssButton.Click += new System.EventHandler(this.selectRssButton_Click);
            // 
            // rssCheckedListBox
            // 
            this.rssCheckedListBox.FormattingEnabled = true;
            this.rssCheckedListBox.Location = new System.Drawing.Point(6, 41);
            this.rssCheckedListBox.Name = "rssCheckedListBox";
            this.rssCheckedListBox.Size = new System.Drawing.Size(320, 139);
            this.rssCheckedListBox.TabIndex = 4;
            // 
            // addUrlButton
            // 
            this.addUrlButton.Location = new System.Drawing.Point(261, 13);
            this.addUrlButton.Name = "addUrlButton";
            this.addUrlButton.Size = new System.Drawing.Size(65, 23);
            this.addUrlButton.TabIndex = 3;
            this.addUrlButton.Text = "Добавить";
            this.addUrlButton.UseVisualStyleBackColor = true;
            this.addUrlButton.Click += new System.EventHandler(this.addUrlButton_Click);
            // 
            // rssUrlTextBox
            // 
            this.rssUrlTextBox.Location = new System.Drawing.Point(46, 15);
            this.rssUrlTextBox.Name = "rssUrlTextBox";
            this.rssUrlTextBox.Size = new System.Drawing.Size(209, 20);
            this.rssUrlTextBox.TabIndex = 2;
            // 
            // rssUrlLabel
            // 
            this.rssUrlLabel.AutoSize = true;
            this.rssUrlLabel.Location = new System.Drawing.Point(3, 18);
            this.rssUrlLabel.Name = "rssUrlLabel";
            this.rssUrlLabel.Size = new System.Drawing.Size(46, 13);
            this.rssUrlLabel.TabIndex = 1;
            this.rssUrlLabel.Text = "RSS url:";
            // 
            // selectRssTabPage
            // 
            this.selectRssTabPage.Controls.Add(this.urlAdressLinkLabel);
            this.selectRssTabPage.Controls.Add(this.discriptionTextBox);
            this.selectRssTabPage.Controls.Add(this.titleComboBox);
            this.selectRssTabPage.Controls.Add(this.channelComboBox);
            this.selectRssTabPage.Location = new System.Drawing.Point(4, 22);
            this.selectRssTabPage.Name = "selectRssTabPage";
            this.selectRssTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.selectRssTabPage.Size = new System.Drawing.Size(332, 211);
            this.selectRssTabPage.TabIndex = 1;
            this.selectRssTabPage.Text = "My News";
            this.selectRssTabPage.UseVisualStyleBackColor = true;
            this.selectRssTabPage.Enter += new System.EventHandler(this.selectRssTabPage_Enter);
            // 
            // urlAdressLinkLabel
            // 
            this.urlAdressLinkLabel.AutoSize = true;
            this.urlAdressLinkLabel.Location = new System.Drawing.Point(6, 194);
            this.urlAdressLinkLabel.Name = "urlAdressLinkLabel";
            this.urlAdressLinkLabel.Size = new System.Drawing.Size(40, 13);
            this.urlAdressLinkLabel.TabIndex = 3;
            this.urlAdressLinkLabel.TabStop = true;
            this.urlAdressLinkLabel.Text = "Go To:";
            this.urlAdressLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.urlAdressLinkLabel_LinkClicked);
            // 
            // discriptionTextBox
            // 
            this.discriptionTextBox.Location = new System.Drawing.Point(7, 61);
            this.discriptionTextBox.Multiline = true;
            this.discriptionTextBox.Name = "discriptionTextBox";
            this.discriptionTextBox.Size = new System.Drawing.Size(319, 130);
            this.discriptionTextBox.TabIndex = 2;
            // 
            // titleComboBox
            // 
            this.titleComboBox.FormattingEnabled = true;
            this.titleComboBox.Location = new System.Drawing.Point(7, 34);
            this.titleComboBox.Name = "titleComboBox";
            this.titleComboBox.Size = new System.Drawing.Size(319, 21);
            this.titleComboBox.TabIndex = 1;
            this.titleComboBox.SelectedIndexChanged += new System.EventHandler(this.titleComboBox_SelectedIndexChanged);
            // 
            // channelComboBox
            // 
            this.channelComboBox.FormattingEnabled = true;
            this.channelComboBox.Location = new System.Drawing.Point(6, 6);
            this.channelComboBox.Name = "channelComboBox";
            this.channelComboBox.Size = new System.Drawing.Size(320, 21);
            this.channelComboBox.TabIndex = 0;
            this.channelComboBox.SelectedIndexChanged += new System.EventHandler(this.channelComboBox_SelectedIndexChanged);
            // 
            // editUserTabPage
            // 
            this.editUserTabPage.Controls.Add(this.warningLabel);
            this.editUserTabPage.Controls.Add(this.saveDataButton);
            this.editUserTabPage.Controls.Add(this.passwordRepeatTextBox);
            this.editUserTabPage.Controls.Add(this.label4);
            this.editUserTabPage.Controls.Add(this.loginTextBox);
            this.editUserTabPage.Controls.Add(this.label3);
            this.editUserTabPage.Controls.Add(this.nameTextBox);
            this.editUserTabPage.Controls.Add(this.label2);
            this.editUserTabPage.Controls.Add(this.passwordTextBox);
            this.editUserTabPage.Controls.Add(this.label1);
            this.editUserTabPage.Controls.Add(this.allUsersComboBox);
            this.editUserTabPage.Location = new System.Drawing.Point(4, 22);
            this.editUserTabPage.Name = "editUserTabPage";
            this.editUserTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.editUserTabPage.Size = new System.Drawing.Size(332, 211);
            this.editUserTabPage.TabIndex = 2;
            this.editUserTabPage.Text = "Users";
            this.editUserTabPage.UseVisualStyleBackColor = true;
            this.editUserTabPage.Enter += new System.EventHandler(this.editUserTabPage_Enter);
            // 
            // warningLabel
            // 
            this.warningLabel.AutoSize = true;
            this.warningLabel.ForeColor = System.Drawing.Color.Red;
            this.warningLabel.Location = new System.Drawing.Point(9, 142);
            this.warningLabel.Name = "warningLabel";
            this.warningLabel.Size = new System.Drawing.Size(87, 13);
            this.warningLabel.TabIndex = 20;
            this.warningLabel.Text = "Ошибка данных";
            this.warningLabel.Visible = false;
            // 
            // saveDataButton
            // 
            this.saveDataButton.Location = new System.Drawing.Point(251, 137);
            this.saveDataButton.Name = "saveDataButton";
            this.saveDataButton.Size = new System.Drawing.Size(75, 23);
            this.saveDataButton.TabIndex = 18;
            this.saveDataButton.Text = "cохранить";
            this.saveDataButton.UseVisualStyleBackColor = true;
            this.saveDataButton.Click += new System.EventHandler(this.saveDataButton_Click);
            // 
            // passwordRepeatTextBox
            // 
            this.passwordRepeatTextBox.Location = new System.Drawing.Point(95, 111);
            this.passwordRepeatTextBox.Name = "passwordRepeatTextBox";
            this.passwordRepeatTextBox.Size = new System.Drawing.Size(231, 20);
            this.passwordRepeatTextBox.TabIndex = 16;
            this.passwordRepeatTextBox.UseSystemPasswordChar = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 105);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 26);
            this.label4.TabIndex = 19;
            this.label4.Text = "Подтвердите\r\n    пароль:";
            // 
            // loginTextBox
            // 
            this.loginTextBox.Location = new System.Drawing.Point(95, 33);
            this.loginTextBox.Name = "loginTextBox";
            this.loginTextBox.Size = new System.Drawing.Size(231, 20);
            this.loginTextBox.TabIndex = 12;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 17;
            this.label3.Text = "Пароль:";
            // 
            // nameTextBox
            // 
            this.nameTextBox.Location = new System.Drawing.Point(95, 59);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(231, 20);
            this.nameTextBox.TabIndex = 13;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "ФИО:";
            // 
            // passwordTextBox
            // 
            this.passwordTextBox.Location = new System.Drawing.Point(95, 85);
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.Size = new System.Drawing.Size(231, 20);
            this.passwordTextBox.TabIndex = 15;
            this.passwordTextBox.UseSystemPasswordChar = true;
            this.passwordTextBox.Enter += new System.EventHandler(this.passwordTextBox_Enter);
            this.passwordTextBox.Leave += new System.EventHandler(this.passwordTextBox_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Логин:";
            // 
            // allUsersComboBox
            // 
            this.allUsersComboBox.FormattingEnabled = true;
            this.allUsersComboBox.Location = new System.Drawing.Point(47, 6);
            this.allUsersComboBox.Name = "allUsersComboBox";
            this.allUsersComboBox.Size = new System.Drawing.Size(195, 21);
            this.allUsersComboBox.TabIndex = 0;
            this.allUsersComboBox.SelectedIndexChanged += new System.EventHandler(this.allUsersComboBox_SelectedIndexChanged);
            // 
            // adminBackgroundWorker
            // 
            this.adminBackgroundWorker.WorkerReportsProgress = true;
            this.adminBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.adminBackgroundWorker_DoWork);
            // 
            // AdministratorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(364, 261);
            this.Controls.Add(this.administratorTabControl);
            this.Name = "AdministratorForm";
            this.Text = "Администратор";
            this.Activated += new System.EventHandler(this.AdministratorForm_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AdministratorForm_FormClosing);
            this.Load += new System.EventHandler(this.AdministratorForm_Load);
            this.administratorTabControl.ResumeLayout(false);
            this.rssTabPage.ResumeLayout(false);
            this.rssTabPage.PerformLayout();
            this.selectRssTabPage.ResumeLayout(false);
            this.selectRssTabPage.PerformLayout();
            this.editUserTabPage.ResumeLayout(false);
            this.editUserTabPage.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl administratorTabControl;
        private System.Windows.Forms.TabPage rssTabPage;
        private System.Windows.Forms.TabPage selectRssTabPage;
        private System.Windows.Forms.TabPage editUserTabPage;
        private System.Windows.Forms.Button addUrlButton;
        private System.Windows.Forms.TextBox rssUrlTextBox;
        private System.Windows.Forms.Label rssUrlLabel;
        private System.Windows.Forms.Button selectRssButton;
        private System.Windows.Forms.CheckedListBox rssCheckedListBox;
        private System.Windows.Forms.LinkLabel urlAdressLinkLabel;
        private System.Windows.Forms.TextBox discriptionTextBox;
        private System.Windows.Forms.ComboBox titleComboBox;
        private System.Windows.Forms.ComboBox channelComboBox;
        private System.Windows.Forms.ComboBox allUsersComboBox;
        private System.Windows.Forms.Label warningLabel;
        private System.Windows.Forms.Button saveDataButton;
        private System.Windows.Forms.TextBox passwordRepeatTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox loginTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox passwordTextBox;
        private System.Windows.Forms.Label label1;
        private System.ComponentModel.BackgroundWorker adminBackgroundWorker;
    }
}