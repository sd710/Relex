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
    public partial class UserForm : Form
    {
        private Account _user = new Account();
        private Rss _userRssChannel = new Rss();
        private Rss _allRssChannel;// = new Rss();

        private int _channelSelectedIndex = 0;
        private bool upgrade = false;
        public UserForm()
        {
            InitializeComponent();
            _allRssChannel = LoginOrRegestrationForm._allRssChannel;
        }

        // загрузка формы
        private void UserForm_Load(object sender, EventArgs e)
        {
            _user.DownloadAllInformation(LoginOrRegestrationForm.GetIdActiveUser());

            this.Text = "RSS Новости, " + _user.Login;

            if (userBackgroundWorker.IsBusy != true)
            {
                userBackgroundWorker.RunWorkerAsync();
            }

            _allRssChannel.LoadCheckList(rssCheckedListBox);

            if (!_userRssChannel.CheckRssChannel())
                userTabControl.SelectTab(1);
        }

        // отдельный поток для обновления новостей или первоначальной загрузки новостей
        private void userBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
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

        // обновление новостей
        private void LoadNewsThread(object stateInfo)
        {
            _userRssChannel.UpgradeNews(Int32.Parse(stateInfo.ToString()));
            _userRssChannel.LoadInDataBaseFromInternet();
        }

        // загрузка ползовательских новостей при открытии вкладки
        private void rssTabPage_Enter(object sender, EventArgs e)
        {
            channelComboBox.Items.Clear();
            if (_userRssChannel.CheckRssChannel())
            {
                channelComboBox.Items.Clear();
                List<string> channelsName = _userRssChannel.AddChannelsToComboBox();
                foreach (string chName in channelsName)
                {
                    channelComboBox.Items.Add(chName);
                }
                channelComboBox.SelectedIndex = 0;
            }
        }

         // выбор источника новостей
        private void channelComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            titleComboBox.Items.Clear();
            int channelSelectedIndex = channelComboBox.SelectedIndex;
            List<string> titlesName = _userRssChannel.SelectRss(channelSelectedIndex);
            foreach (string tName in titlesName)
            {
                titleComboBox.Items.Add(tName);
            }
            titleComboBox.SelectedIndex = 0;


            _userRssChannel.UpgradeNews(channelComboBox.SelectedIndex);
            _userRssChannel.LoadInDataBaseFromInternet();
        }

        // выбор новости
        private void titleComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            discriptionTextBox.Text = _userRssChannel.GetDescrition(channelComboBox.SelectedIndex, titleComboBox.SelectedIndex);
            urlAdressLinkLabel.Text = "Go To:" + _userRssChannel.GetUrlLink(channelComboBox.SelectedIndex, titleComboBox.SelectedIndex);
        }

        // переход по ссылке
        private void adressLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _userRssChannel.ClickLinkUrl(channelComboBox.SelectedIndex, titleComboBox.SelectedIndex);
        }

        // вкладка Мои Данные
        private void userAccountTabPage_Enter(object sender, EventArgs e)
        {
            _userRssChannel.SelectUserRss(rssCheckedListBox);
        }

        // выбор нужных рассылок
        private void selectRssButton_Click(object sender, EventArgs e)
        {
            if (_userRssChannel.Count() <= rssCheckedListBox.CheckedItems.Count)
            {
                foreach (string rssName in rssCheckedListBox.CheckedItems.OfType<string>())
                {
                    _userRssChannel.AddRssForUserInDataBase(_user.Id, rssName);
                }

                _userRssChannel.DownloadUserRssFromDataBase(_user.Id);
                _userRssChannel.DownloadUserNewsFromDataBase();
            }
            else
            {
                _userRssChannel.DeleteRssForUserInDataBase(_user.Id, rssCheckedListBox);
            }
        }

        // редактировать свои данные
        private void editDataButton_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            EditDataForm Form = new EditDataForm();
            Form.Show();
        }

        // закрытие приложения
        private void UserForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        
        // запуск фонового обновления
        private void UserForm_Activated(object sender, EventArgs e)
        {
            if (userBackgroundWorker.IsBusy != true)
            {
                userBackgroundWorker.RunWorkerAsync();
            }
        }
    }
}
