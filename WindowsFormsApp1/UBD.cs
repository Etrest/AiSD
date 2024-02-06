using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace course
{
    class UDB
    {
        //Класс подключения к Базе данных Пользователей
        MySqlConnection connection = new MySqlConnection("server=localhost;port=3306;user=root;password=root;database=users");

        public MySqlConnection getConnection()
        {
            //подключение к БД
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
            return connection;
        }
        public MySqlConnection closeConnection()
        {
            //Отключение от БД
            if (connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
            return connection;
        }

        public MySqlConnection connectionStatus()
        {
            return connection;
        }

    }
}

