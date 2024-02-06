
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace course
    {
        public partial class RegForm : Form
        {
            public RegForm()
            {
                InitializeComponent();

                loginBox.Text = "Введите ваш будущий логин";

            }

            private void loginBox_Enter(object sender, EventArgs e)
            {
                if (loginBox.Text == "Введите ваш будущий логин")
                {
                    loginBox.Text = "";
                }
            }

            private void loginBox_Leave(object sender, EventArgs e)
            {
                if (loginBox.Text == "")
                {
                    loginBox.Text = "Введите ваш будущий логин";
                }
            }

            private void reg_Click(object sender, EventArgs e)
            {
                if (loginBox.Text == "Введите ваш будущий логин")
                {
                    MessageBox.Show("Введите логин");
                    return;
                }

                if (passBox.Text == "")
                {
                    MessageBox.Show("Введите пароль");
                    return;
                }

                if (checkUnic())
                {
                    return;
                }



                UDB data = new UDB();
                MySqlCommand command = new MySqlCommand("INSERT INTO `userslist` (`id`, `login`, `pass`) VALUES (NULL, @login, @pass);", data.getConnection());

                command.Parameters.Add("@login", MySqlDbType.VarChar).Value = loginBox.Text;
                command.Parameters.Add("@pass", MySqlDbType.VarChar).Value = passBox.Text;

                data.getConnection();


                if (command.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Аккаунт был создан");
                }
                else
                {
                    MessageBox.Show("Аккаунт создан не был");
                }

                data.closeConnection();
            }

            public Boolean checkUnic()
            {
                UDB db = new UDB();

                DataTable infoTable = new DataTable();

                MySqlDataAdapter adapter = new MySqlDataAdapter();

                MySqlCommand command = new MySqlCommand("SELECT * FROM `userslist` WHERE `login` = @ul", db.getConnection());
                command.Parameters.Add("@ul", MySqlDbType.VarChar).Value = loginBox.Text;

                adapter.SelectCommand = command;
                adapter.Fill(infoTable);

                if (infoTable.Rows.Count > 0)
                {
                    MessageBox.Show("Такой логин уже есть. Введите другой.");
                    return true;
                }
                else
                {
                    return false;
                }

            }

            private void LoginBackButton_Click(object sender, EventArgs e)
            {
                this.Hide();
                LoginForm logForm = new LoginForm();
                logForm.Show();

            }
        }
    }

}
}
