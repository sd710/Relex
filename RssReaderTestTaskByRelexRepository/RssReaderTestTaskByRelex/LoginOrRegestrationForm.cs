using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RssReaderTestTaskByRelex
{
    public partial class LoginOrRegestrationForm : Form
    {
        private Account _activeUser = new Account();

        private static string _stringConnecton;
        private static int _idActiveUser = -1;

        public static Rss _allRssChannel = new Rss();
        

        public static string GetStringConnection()
        {
            return _stringConnecton;
        }

        public static int GetIdActiveUser()
        {
            return _idActiveUser;
        }

        public LoginOrRegestrationForm()
        {
            InitializeComponent();
        }
        
        // вход в свою учетную запись
        private void signInButton_Click(object sender, EventArgs e)
        {
            if (_activeUser.CheckAccountInformation(loginTextBox.Text, passwordTextBox.Text))
            { 
                _idActiveUser = _activeUser.Id;
                errorLabel.Visible = false;
                switch (_activeUser.RoleName)
                {
                    case "Administrator": this.Visible = false;
                        AdministratorForm adminForm = new AdministratorForm();
                        adminForm.Show();
                        break;
                    case "User": this.Visible = false;
                        UserForm userForm = new UserForm();
                        userForm.Show();
                        break;
                }
            }
            else
                errorLabel.Visible = true;
        }

        // регистрация новой учетной записи
        private void joinButton_Click(object sender, EventArgs e)
        {
            RegistrationForm Form = new RegistrationForm();
            Form.Show();
        }

        // загрузка формы
        private void LoginOrRegestrationForm_Load(object sender, EventArgs e)
        {
            try
            {
                _stringConnecton = System.Configuration.ConfigurationManager.ConnectionStrings["RssReaderTestTaskByRelex.Properties.Settings.rssDataConnectionString"].ConnectionString;
                Account.SetStringConnection(_stringConnecton);
                Rss.SetStringConnection(_stringConnecton);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            // загружаем информацию в отдельном потоке, чтобы интерфейс пользователя не вис
            if (loadRssBackgroundWorker.IsBusy != true)
            {
                loadRssBackgroundWorker.RunWorkerAsync();
            }
        }

        private void loadRssBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            _allRssChannel.DownloadAllRssFromDataBase();
            _allRssChannel.DownloadAllNewsFromInternet(); // при запуске приложения, если есть интернет загружаются только новые новости из rss
            if (!_allRssChannel.DownloadInternet)
            {
                _allRssChannel.DownloadAllNewsFromDataBase(); // Если нет Интернета, то загружаются все накопившиеся новости из БД.
            }
        }
    }
}
