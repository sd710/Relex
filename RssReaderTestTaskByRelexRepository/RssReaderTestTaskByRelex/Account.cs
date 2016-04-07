using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace RssReaderTestTaskByRelex
{
    class Account
    {
        private int _id = -1;
        private string _name;
        private string _login;
        private string _password;
        private int _roleId = -1;
        private string _roleName;
        public bool Registration = true;
        public bool logged = false;
        private static string _stringConnection;// = LoginOrRegestrationForm.GetStringConnection();

        public static void SetStringConnection(string strConn)
        {
            _stringConnection = strConn;
        }

        // получение данных о пользователе если он залогинился
        private void GetAccountUserFromDataBase(string login, string password)
        {
            SqlConnection sqlConn = null;
            try
            {
                using (sqlConn = new SqlConnection(_stringConnection))
                {
                    SqlCommand sqlCmd = new SqlCommand();
                    sqlCmd.CommandText = "SELECT * FROM dbo.Account WHERE UserLogin = @login and UserPassword = @password";
                    sqlCmd.CommandType = System.Data.CommandType.Text;
                    sqlCmd.Connection = sqlConn;

                    SqlParameter sqlParam = new SqlParameter();
                    sqlParam.ParameterName = "@login";
                    sqlParam.Value = login;
                    sqlParam.SqlDbType = System.Data.SqlDbType.VarChar;

                    sqlCmd.Parameters.Add(sqlParam);

                    sqlParam = new SqlParameter();
                    sqlParam.ParameterName = "@password";
                    sqlParam.Value = password;
                    sqlParam.SqlDbType = System.Data.SqlDbType.VarChar;

                    sqlCmd.Parameters.Add(sqlParam);

                    sqlConn.Open();
                    SqlDataReader readerAccount = sqlCmd.ExecuteReader();
                    if (readerAccount.HasRows)
                        if (readerAccount.Read())
                        {
                            this._id = readerAccount.GetInt32(0);
                            this._login = readerAccount.GetString(1);
                            this._password = readerAccount.GetString(2);
                            this._name = readerAccount.GetString(3);
                            logged = true;
                        }
                    readerAccount.Close();
                    sqlConn.Close();
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        // получение информации по логин
        public void GetAccountUserFromDataBase(string login)
        {
            try
            {
                using (var sqlConn = new SqlConnection(_stringConnection))
                {
                    SqlCommand sqlCmd = new SqlCommand();

                    sqlCmd.CommandText = "SELECT * FROM dbo.Account WHERE UserLogin = @login";
                    sqlCmd.CommandType = System.Data.CommandType.Text;
                    sqlCmd.Connection = sqlConn;

                    SqlParameter sqlParam = new SqlParameter();
                    sqlParam.ParameterName = "@login";
                    sqlParam.Value = login;
                    sqlParam.SqlDbType = System.Data.SqlDbType.VarChar;
                    sqlCmd.Parameters.Add(sqlParam);

                    sqlConn.Open();
                    SqlDataReader readerAccount = sqlCmd.ExecuteReader();

                    if (readerAccount.HasRows)
                    {
                        if (readerAccount.Read())
                        {
                            this.Id = readerAccount.GetInt32(0);
                            this.Login = readerAccount.GetString(1);
                            this.Password = readerAccount.GetString(2);
                            this.Name = readerAccount.GetString(3);
                            logged = true;
                        }
                    }
                    readerAccount.Close();
                    sqlConn.Close();
                    sqlConn.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Registration = true;
            }
        }


        // получение данных по id из БД
        private void GetAccountUserFromDataBase(int userId)
        {
            try
            {
                using (var sqlConn = new SqlConnection(_stringConnection))
                {
                    SqlCommand sqlCmd = new SqlCommand();

                    sqlCmd.CommandText = "SELECT * FROM dbo.Account WHERE UserId = @userId";
                    sqlCmd.CommandType = System.Data.CommandType.Text;
                    sqlCmd.Connection = sqlConn;

                    SqlParameter sqlParam = new SqlParameter();
                    sqlParam.ParameterName = "@userId";
                    sqlParam.Value = userId;
                    sqlParam.SqlDbType = System.Data.SqlDbType.Int;
                    sqlCmd.Parameters.Add(sqlParam);

                    sqlConn.Open();

                    SqlDataReader readerAccount = sqlCmd.ExecuteReader();

                    if (readerAccount.HasRows)
                        if (readerAccount.Read())
                        {
                            this._id = userId;
                            this._login = readerAccount.GetString(1);
                            this._password = readerAccount.GetString(2);
                            this._name = readerAccount.GetString(3);
                            logged = true;
                        }

                    readerAccount.Close();
                    sqlConn.Close();
                    sqlConn.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Registration = true;
            }
        }

        //загрузка всех данных пользователя по id
        public void DownloadAllInformation(int userId)
        {
            GetAccountUserFromDataBase(userId);
            GetRoleUserFromDataBase();
        }

        // роль залогиневшегося пользователя
        // получение роли (RoleId, RoleName) пользователя по UserId.
        private void GetRoleUserFromDataBase()
        {
            try
            {
                using (var sqlConn = new SqlConnection(_stringConnection))
                {
                    SqlCommand sqlCmd = new SqlCommand();
                    sqlCmd.CommandText = "SELECT dbo.Role.RoleId, dbo.Role.RoleName FROM dbo.Role INNER JOIN dbo.User_Role ON dbo.Role.RoleId = dbo.User_Role.RoleId WHERE dbo.User_Role.UserId = @userId";
                    sqlCmd.CommandType = System.Data.CommandType.Text;
                    sqlCmd.Connection = sqlConn;

                    SqlParameter sqlParam = new SqlParameter();
                    sqlParam.ParameterName = "@userId";
                    sqlParam.Value = this._id;
                    sqlParam.SqlDbType = System.Data.SqlDbType.Int;
                    sqlCmd.Parameters.Add(sqlParam);

                    sqlConn.Open();

                    SqlDataReader readerAccount = sqlCmd.ExecuteReader();

                    if (readerAccount.HasRows)
                        if (readerAccount.Read())
                        {
                            this._roleId = readerAccount.GetInt32(0);
                            this._roleName = readerAccount.GetString(1);
                        }
                    readerAccount.Close();
                    sqlConn.Close();
                    sqlConn.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // добавление роли новому пользователю.
        private void AddUserRoleInDataBase()
        {
            rssDataDataSetTableAdapters.User_RoleTableAdapter UserRole = new rssDataDataSetTableAdapters.User_RoleTableAdapter();
            UserRole.Insert(this._id, this._roleId);
        }

        //добавление user в бд
        private void AddUserInDataBase()
        {
            rssDataDataSetTableAdapters.AccountTableAdapter newAcc = new rssDataDataSetTableAdapters.AccountTableAdapter();
            newAcc.Insert(this._login, this._password, this._name);
        }

        // залогинился или нет
        public bool CheckAccountInformation(string login, string password)
        {
            GetAccountUserFromDataBase(login, password);
            GetRoleUserFromDataBase();
            if (logged)
                return true;
            else
                return false;
        }

        // пользователь должен создать уникальный логин, проверка существует ли введеный логин
        public bool FoundLoginInDataBase(string login)
        {
            try
            {
                using (var sqlConn = new SqlConnection(_stringConnection))
                {
                    SqlCommand sqlCmd = new SqlCommand();
                    sqlCmd.CommandText = "SELECT UserLogin FROM dbo.Account WHERE UserLogin = @login";
                    sqlCmd.CommandType = System.Data.CommandType.Text;
                    sqlCmd.Connection = sqlConn;

                    SqlParameter sqlParam = new SqlParameter();
                    sqlParam.ParameterName = "@login";
                    sqlParam.Value = login;
                    sqlParam.SqlDbType = System.Data.SqlDbType.VarChar;
                    sqlCmd.Parameters.Add(sqlParam);

                    sqlConn.Open();

                    SqlDataReader readerAccount = sqlCmd.ExecuteReader();

                    if (readerAccount.HasRows)
                        if (readerAccount.Read())
                            return false;
                    readerAccount.Close();
                    sqlConn.Close();
                    sqlConn.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            return true;
        }
        
        // регистрация пользователя и считывание данных в экземпляр класса
        public void RegistrationAccount(string name, string login, string pass)
        {
            _name = name;
            _login = login;
            _password = pass;
            _roleId = 2;
            AddUserInDataBase();
            GetAccountUserFromDataBase(_login, _password);
            AddUserRoleInDataBase();
            GetRoleUserFromDataBase();           
        }
        
        // сохранение изменений личных данных пользователя
        public void SaveChangeInDataUser(Account newUser)
        {
            rssDataDataSetTableAdapters.AccountTableAdapter usersData = new rssDataDataSetTableAdapters.AccountTableAdapter();
            if (string.IsNullOrEmpty(newUser.Password))
            {
                usersData.Update(newUser.Login, this.Password, newUser.Name, this.Id, this.Login, this.Password, this.Name);
            }
            else
            {
                usersData.Update(newUser.Login, newUser.Password, newUser.Name, this.Id, this.Login, this.Password, this.Name);
            }
        }

        // Сохранение изменений данных пользователя администратором.
        public void AdminSaveChangeInDataUser(Account tmpUser, Account newUser)
        {
            rssDataDataSetTableAdapters.AccountTableAdapter usersData = new rssDataDataSetTableAdapters.AccountTableAdapter();
            if (string.IsNullOrEmpty(newUser.Password))
            {
                usersData.Update(newUser.Login, tmpUser.Password, newUser.Name, tmpUser.Id, tmpUser.Login, tmpUser.Password, tmpUser.Name);
            }
            else
            {
                usersData.Update(newUser.Login, newUser.Password, newUser.Name, tmpUser.Id, tmpUser.Login, tmpUser.Password, tmpUser.Name);
            }
        }

        // получение всех логинов из БД
        public void GetAllNameUserFromDataBase(ComboBox allUserNameComboBox)
        {
            try
            {
                using (var sqlConn = new SqlConnection(_stringConnection))
                {
                    SqlCommand sqlCmd = new SqlCommand();
                    sqlCmd.CommandText = "SELECT UserLogin FROM dbo.Account";
                    sqlCmd.CommandType = System.Data.CommandType.Text;
                    sqlCmd.Connection = sqlConn;

                    sqlConn.Open();

                    SqlDataReader readerAccount = sqlCmd.ExecuteReader();

                    if (readerAccount.HasRows)
                        while (readerAccount.Read())
                            allUserNameComboBox.Items.Add(readerAccount.GetString(0));
                    readerAccount.Close();
                    sqlConn.Close();
                    sqlConn.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        

        public string RoleName
        {
            get { return _roleName; }
            set { _roleName = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Login
        {
            get { return _login; }
            set { _login = value; }
        }

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public bool CheckName(string name)
        {
            foreach (char symbol in name)
            {
                if (!((symbol >= 'A' && symbol <= 'Z') || (symbol >= 'a' && symbol <= 'z') || (symbol >= 'А' && symbol <= 'я') || symbol == ' ' || symbol == 'ё'))
                    return false;
            }
            return true;
        }

        public bool CheckLogin(string login)
        {
            foreach (char symbol in login)
            {
                if (!(symbol >= 'A' && symbol <= 'Z' || symbol >= 'a' && symbol <= 'z' || symbol >= '0' && symbol <= '9' || symbol == ' ' || symbol == '_'))
                    return false;
            }
            return true;
        }

        public bool CheckPassword(string pass_1, string pass_2)
        {
            if (pass_1 != pass_2)
                return false;
            return true;
        }
    }
}