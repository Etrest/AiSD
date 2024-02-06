using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace course
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();

            this.passBox.AutoSize = false;
            this.passBox.Size = new Size(this.passBox.Size.Width, 54);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void loginBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void passBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void autoriz_Click(object sender, EventArgs e)
        {
            String userLogin = loginBox.Text;
            String userPass = passBox.Text;

            UDB db = new UDB();

            DataTable infoTable = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command = new MySqlCommand("SELECT * FROM `userslist` WHERE `login` = @ul AND `pass` = @up", db.getConnection());
            command.Parameters.Add("@ul", MySqlDbType.VarChar).Value = userLogin;
            command.Parameters.Add("@up", MySqlDbType.VarChar).Value = userPass;

            adapter.SelectCommand = command;
            adapter.Fill(infoTable);

            if (infoTable.Rows.Count > 0)
            {
                this.Hide();
                MainFrame mainFrame = new MainFrame();
                mainFrame.Show();
                db.closeConnection();

            }
            else
                MessageBox.Show("Такого пользователя нет");
        }

        private void regPageButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            RegForm regForm = new RegForm();
            regForm.Show();
        }
    }
}
