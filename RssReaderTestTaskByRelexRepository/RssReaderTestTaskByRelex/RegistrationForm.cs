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
    public partial class RegistrationForm : Form
    {
        private Account _newUser = new Account();

        public RegistrationForm()
        {
            InitializeComponent();
        }

        // регистрация пользователя
        private void regestrationButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(nameTextBox.Text) && _newUser.CheckName(nameTextBox.Text))
                if (!string.IsNullOrWhiteSpace(loginTextBox.Text) && _newUser.CheckLogin(loginTextBox.Text) && _newUser.FoundLoginInDataBase(loginTextBox.Text))
                    if (!string.IsNullOrEmpty(passwordTextBox.Text) && _newUser.CheckPassword(passwordTextBox.Text, repeatPasswordTextBox.Text))
                    {
                       _newUser.RegistrationAccount(nameTextBox.Text, loginTextBox.Text, passwordTextBox.Text);
                        if (_newUser.Registration)
                        {
                            
                            nameTextBox.Clear();
                            loginTextBox.Clear();
                            passwordTextBox.Clear();
                            repeatPasswordTextBox.Clear();
                            var result =  MessageBox.Show("Войти?", "Регистрация прошла успешно", MessageBoxButtons.YesNo);
                            //если да, то открыть форму входа
                            // нет, закрыть приложение
                            if (result == DialogResult.Yes)
                            {
                                this.Visible = false;
                            }
                            else
                            {
                                this.Close();
                                Application.Exit();
                            }
                        }

                    }
                    else
                    {
                        passwordTextBox.Clear();
                        repeatPasswordTextBox.Clear();
                        passwordTextBox.Focus();
                        errorLabel.Text = "Неверно введен пароль";
                        errorLabel.Visible = true;
                    }
                else
                {
                    loginTextBox.Focus();
                    loginTextBox.SelectionStart = 0;
                    loginTextBox.SelectionLength = loginTextBox.TextLength;
                    errorLabel.Text = "Недопустимые символы в поле: Логин";
                    errorLabel.Visible = true;
                }
            else
            {
                nameTextBox.Focus();
                nameTextBox.SelectionStart = 0;
                nameTextBox.SelectionLength = nameTextBox.TextLength;
                errorLabel.Text = "Недопустимые символы в поле: ФИО";
                errorLabel.Visible = true;
            }

        }
    }
}
