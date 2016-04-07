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
    public partial class EditDataForm : Form
    {
        Account user = new Account();
        Account newUser = new Account();
        
        public EditDataForm()
        {
            InitializeComponent();
        }

        private void EditDataForm_Load(object sender, EventArgs e)
        {
            user.DownloadAllInformation(LoginOrRegestrationForm.GetIdActiveUser());
            
            loginTextBox.Text = user.Login;
            nameTextBox.Text = user.Name;
            passwordRepeatTextBox.ReadOnly = true;
        }

        private void GetDataUserFromForm(string login, string name, string password)
        {
            newUser.Login = login;
            newUser.Name = name;
            newUser.Password = password;
        }

        // сохранение изменений
        private void saveDataButton_Click(object sender, EventArgs e)
        {
            warningLabel.Text = "";
            warningLabel.Visible = false;

            if (user.CheckLogin(loginTextBox.Text))
                if (user.CheckName(nameTextBox.Text))
                    if (!string.IsNullOrWhiteSpace(passwordTextBox.Text))
                        if (user.CheckPassword(passwordTextBox.Text, passwordRepeatTextBox.Text))
                        {
                            GetDataUserFromForm(loginTextBox.Text, nameTextBox.Text, passwordTextBox.Text);
                            user.SaveChangeInDataUser(newUser);
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
                        GetDataUserFromForm(loginTextBox.Text, nameTextBox.Text, passwordTextBox.Text);
                        user.SaveChangeInDataUser(newUser);
                        warningLabel.Visible = true;
                        warningLabel.Text = "Данные сохранены";
                    }
                else
                {
                    
                    warningLabel.Text = "поле ФИО имеет недопустимые символы";
                }
            else
            {
                warningLabel.Visible = true;
                warningLabel.Text = "поле Логин имеет недопустимые символы";
            }
        }

        private void passwordTextBox_Enter(object sender, EventArgs e)
        {
            passwordRepeatTextBox.ReadOnly = false;
        }

        private void passwordTextBox_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(passwordTextBox.Text))
                passwordRepeatTextBox.ReadOnly = true;
        }


        private void EditDataForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            
            UserForm userFrom = new UserForm();
            userFrom.Show();
        }
    }
}
