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
    public partial class FindForm : Form
    {
        public FindForm()
        {
            InitializeComponent();
        }



        private void findDataButton_Click(object sender, EventArgs e)
        {

            this.findRegGrid.Rows.Clear();

            EDB data = new EDB();
            MySqlCommand command = new MySqlCommand("Select * from `employee` where `name` = @mcUserName or `surname` = @mcSurUserName or `prof` = @mcProfUserName or `spec` = @mcSpecUserName;", data.getConnection());

            command.Parameters.AddWithValue("@mcUserName", nameBox.Text);
            command.Parameters.AddWithValue("@mcSurUserName", surnameBox.Text);
            command.Parameters.AddWithValue("@mcProfUserName", profBox.Text);
            command.Parameters.AddWithValue("@mcSpecUserName", specBox.Text);

            MySqlDataReader reader = command.ExecuteReader();

            List<string[]> database = new List<string[]>();

            while (reader.Read())
            {
                database.Add(new string[6]);

                database[database.Count - 1][0] = reader[0].ToString();
                database[database.Count - 1][1] = reader[1].ToString();
                database[database.Count - 1][2] = reader[2].ToString();
                database[database.Count - 1][3] = reader[3].ToString();
                database[database.Count - 1][4] = reader[4].ToString();
                database[database.Count - 1][5] = reader[5].ToString();

            }
            reader.Close();
            data.closeConnection();

            foreach (string[] s in database)
            {
                findRegGrid.Rows.Add(s);
            }
        }

        private void editDataButton_Click(object sender, EventArgs e)
        {

            EDB data = new EDB();
            MySqlCommand command = new MySqlCommand("UPDATE `employee` SET `name` = @editName, `surname` = @editSurname, `prof` = @editProf, `spec` = @editSpeс, `enterdate` = @editDate WHERE `name` = @mcUserName AND `surname` = @editSurname", data.getConnection());

            command.Parameters.AddWithValue("@editName", editNameBox.Text);
            command.Parameters.AddWithValue("@editSurname", editSurnameBox.Text);
            command.Parameters.AddWithValue("@editProf", editProfBox.Text);
            command.Parameters.AddWithValue("@editSpeс", editSpecBox.Text);
            command.Parameters.AddWithValue("@editDate", editDateEnter.Value);

            command.Parameters.AddWithValue("@mcUserName", nameBox.Text);
            command.Parameters.AddWithValue("@mcSurUserName", surnameBox.Text);

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
