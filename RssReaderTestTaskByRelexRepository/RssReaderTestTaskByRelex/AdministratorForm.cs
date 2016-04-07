using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace RssReaderTestTaskByRelex
{
    public partial class AdministratorForm : Form
    {
        private Account _user = new Account();
        private Rss _allRssChannel;// = new Rss();
        private Rss _userRssChannel = new Rss();
        Account tmpUser = new Account();
        private int _channelSelectedIndex = 0;
        private bool upgrade = false;

        public AdministratorForm()
        {
            InitializeComponent();
            _allRssChannel = LoginOrRegestrationForm._allRssChannel;
        }

        // добавление рассылки
        private void addUrlButton_Click(object sender, EventArgs e)
        {
            if (rssUrlTextBox.Text != "")
            {
                _allRssChannel.AddNewRssChannel(rssUrlTextBox.Text);
                _allRssChannel.LoadCheckList(rssCheckedListBox);
                rssUrlTextBox.Clear();
            }
        }

        // выбор интересуемых рассылок
        private void selectRssButton_Click(object sender, EventArgs e)
        {
            foreach (string rssName in rssCheckedListBox.CheckedItems.OfType<string>())
            {
                _userRssChannel.AddRssForUserInDataBase(_user.Id, rssName);
                if (adminBackgroundWorker.IsBusy != true)
                {
                    upgrade = false;
                    adminBackgroundWorker.RunWorkerAsync();
                }
            }
        }

        // загрузка формы
        private void AdministratorForm_Load(object sender, EventArgs e)
        {
            _user.DownloadAllInformation(LoginOrRegestrationForm.GetIdActiveUser());

            if (adminBackgroundWorker.IsBusy != true)
            {
                adminBackgroundWorker.RunWorkerAsync();
            }

            _allRssChannel.LoadCheckList(rssCheckedListBox);
        }

        // загрузка или обновления в фоновом потоке
        private void adminBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (!upgrade)
            {
                _userRssChannel.DownloadUserRssFromDataBase(_user.Id);
                _userRssChannel.DownloadUserNewsFromDataBase();
                upgrade = true;
            }
            else
                LoadNewsThread(_channelSelectedIndex);


            _allRssChannel.LoadInDataBaseFromInternet();
        }

        // загрузка названия rss новостей
        private void selectRssTabPage_Enter(object sender, EventArgs e)
        {
            try
            {
                channelComboBox.Items.Clear();
                List<string> channelsName = _userRssChannel.AddChannelsToComboBox();
                foreach (string chName in channelsName)
                {
                    channelComboBox.Items.Add(chName);
                }
                if (channelComboBox.Items.Count != 0)
                    channelComboBox.SelectedIndex = 0;
                else
                    MessageBox.Show("Новостей нет", "Внимание");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // выбор рассылки
        private void channelComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {            
            titleComboBox.Items.Clear();
            _channelSelectedIndex = channelComboBox.SelectedIndex;
            List<string> titlesName = _userRssChannel.SelectRss(_channelSelectedIndex);
            foreach (string tName in titlesName)
            {
                titleComboBox.Items.Add(tName);
            }
            titleComboBox.SelectedIndex = 0;
        }

        // обновление новостей в отдельном потоке
        private void LoadNewsThread(object stateInfo)
        {
            _userRssChannel.UpgradeNews(Int32.Parse(stateInfo.ToString()));
            _userRssChannel.LoadInDataBaseFromInternet();
        }


        // выбор новости
        private void titleComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            discriptionTextBox.Text = _userRssChannel.GetDescrition(channelComboBox.SelectedIndex, titleComboBox.SelectedIndex);
            urlAdressLinkLabel.Text = "Go To:" + _userRssChannel.GetUrlLink(channelComboBox.SelectedIndex, titleComboBox.SelectedIndex);
        }

        // переход по ссылке
        private void urlAdressLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _userRssChannel.ClickLinkUrl(channelComboBox.SelectedIndex, titleComboBox.SelectedIndex);
        }

        // исправить параметры передаваемы в функцию
        private void editUserTabPage_Enter(object sender, EventArgs e)
        {
            allUsersComboBox.Items.Clear();
            passwordRepeatTextBox.ReadOnly = true;
            tmpUser.GetAllNameUserFromDataBase(allUsersComboBox);
            allUsersComboBox.SelectedIndex = 0;
        }

        // выбор пользователя для изменения его данных
        private void allUsersComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            tmpUser.GetAccountUserFromDataBase(allUsersComboBox.SelectedItem.ToString());
            loginTextBox.Text = tmpUser.Login;
            nameTextBox.Text = tmpUser.Name;
            warningLabel.Visible = false;
        }

        
        private void passwordTextBox_Enter(object sender, EventArgs e)
        {
            passwordRepeatTextBox.ReadOnly = false;
        }

        private void passwordTextBox_Leave(object sender, EventArgs e)
        {
            if (passwordTextBox.Text == "")
                passwordRepeatTextBox.ReadOnly = true;
        }

        // сохранение изменений пользователя
        private void saveDataButton_Click(object sender, EventArgs e)
        {
            warningLabel.Text = "";
            warningLabel.Visible = false;
            if (tmpUser.CheckLogin(loginTextBox.Text))
                if (tmpUser.CheckName(nameTextBox.Text))
                    if (!string.IsNullOrWhiteSpace(passwordTextBox.Text))
                        if (_user.CheckPassword(passwordTextBox.Text, passwordRepeatTextBox.Text))
                        {
                            Account newUser = new Account();
                            newUser.Login = loginTextBox.Text;
                            newUser.Login = nameTextBox.Text;
                            newUser.Password = passwordTextBox.Text;

                            tmpUser.AdminSaveChangeInDataUser(tmpUser, newUser);
                            warningLabel.Visible = true;
                            warningLabel.Text = "Данные сохранены";
                        }
                        else
                        {
                            warningLabel.Visible = true;
                            warningLabel.Text = "Пароли не совпадают";
                        }

                    else
                    {
                        Account newUser = new Account();
                        newUser.Login = loginTextBox.Text;
                        newUser.Name = nameTextBox.Text;

                        tmpUser.AdminSaveChangeInDataUser(tmpUser, newUser);
                        warningLabel.Visible = true;
                        warningLabel.Text = "Данные сохранены";
                    }
                else
                {
                    warningLabel.Visible = true;
                    warningLabel.Text = "поле ФИО имеет недопустимые символы";
                }
            else
            {
                warningLabel.Visible = true;
                warningLabel.Text = "поле Логин имеет недопустимые символы";
            }
            passwordTextBox.Clear();
            passwordRepeatTextBox.Clear();
        }

        private void AdministratorForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        // запуск обновлений в отдельном потоке
        private void AdministratorForm_Activated(object sender, EventArgs e)
        {
            if (adminBackgroundWorker.IsBusy != true)
            {
                adminBackgroundWorker.RunWorkerAsync();
            }
        }
    }
}
