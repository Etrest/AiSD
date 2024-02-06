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
    public partial class DelForm : Form
    {
        public DelForm()
        {
            InitializeComponent();
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            EDB data = new EDB();
            MySqlCommand command = new MySqlCommand("DELETE FROM `employee` WHERE `name` = @name AND `surname` = @surname;", data.getConnection());

            command.Parameters.Add("@name", MySqlDbType.VarChar).Value = nameBox.Text;
            command.Parameters.Add("@surname", MySqlDbType.VarChar).Value = surnameBox.Text;


            if (command.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Данные удалены");
            }
            else
            {
                MessageBox.Show("О подобном работнике данных не найдено");
            }


            data.closeConnection();
        }
    }
}
