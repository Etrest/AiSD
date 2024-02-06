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
using Excel = Microsoft.Office.Interop.Excel;

namespace course
{
    public partial class MainFrame : Form
    {
        public MainFrame()
        {
            InitializeComponent();
            LoadData();
        }

        public void LoadData()
        {
            EDB dg = new EDB();

            MySqlCommand command = new MySqlCommand("SELECT * FROM `employee` ORDER BY `id`", dg.getConnection());
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
            dg.closeConnection();

            foreach (string[] s in database)
            {
                DataView.Rows.Add(s);
            }
        }

        private void DataView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void addButton_Click(object sender, EventArgs e)
        {
            AddForm addForm = new AddForm();

            addForm.Show();
        }

        private void delButton_Click(object sender, EventArgs e)
        {
            DelForm delForm = new DelForm();
            delForm.Show();
        }

        private void findButton_Click(object sender, EventArgs e)
        {
            FindForm findForm = new FindForm();
            findForm.Show();
        }

        private void renewGrid_Click(object sender, EventArgs e)
        {
            this.DataView.Rows.Clear();
            LoadData();
        }

        private void export_Click(object sender, EventArgs e)
        {
            Excel.Application exEx = new Excel.Application();

            exEx.Workbooks.Add();
            Excel.Worksheet expi = (Excel.Worksheet)exEx.ActiveSheet;

            for (int i = 0; i <= DataView.RowCount - 1; i++)
            {
                for (int j = 0; j <= DataView.ColumnCount - 1; j++)
                {
                    expi.Cells[i + 1, j + 1] = DataView[j, i].Value.ToString();
                }

            }

            exEx.Visible = true;
        }
    }
}
