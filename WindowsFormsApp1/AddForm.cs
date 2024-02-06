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
    public partial class AddForm : Form
    {
        public AddForm()
        {
            InitializeComponent();
        }

        private void addDataButton_Click(object sender, EventArgs e)
        {
            EDB data = new EDB();
            MySqlCommand command = new MySqlCommand("INSERT INTO `employee` (`id`, `name`, `surname`, `prof`, `spec`, `enterdate`) VALUES(null, @name, @surname, @prof, @spec, @data);", data.getConnection());

            command.Parameters.Add("@name", MySqlDbType.VarChar).Value = nameBox.Text;
            command.Parameters.Add("@surname", MySqlDbType.VarChar).Value = surnameBox.Text;
            command.Parameters.Add("@prof", MySqlDbType.VarChar).Value = profBox.Text;
            command.Parameters.Add("@spec", MySqlDbType.VarChar).Value = specBox.Text;
            command.Parameters.Add("@data", MySqlDbType.DateTime).Value = dateEnter.Value;

            if (command.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Информация добавлена");
            }
            else
            {
                MessageBox.Show("Информация добавлена не была. Проверьте корректность ввода");
            }



            data.closeConnection();
        }
    }
}
