using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Data.Sql;
using MySql.Data.MySqlClient;

namespace Inrotech.Domain.Sim
{

    public class Sim_database
    {
        private Random rand = new Random();
        private const String ConnectorString = @"server=mysql45.unoeuro.com;Port=3306;userid=kasper_mad_com;
            password=Gruppe3;database=kasper_madsen_com_db2";
        MySqlConnection conn;

        public Sim_database()
        {
            //https://mysql.unoeuro.com/index.php
        }
        private int GetSim_database()
        {
            return 1;
        }

        //hashes the password, adding a salt of random int. returns a list with two int.
        public bool Sim_GetUser(string name, string password)
        {
            var _res = false;
            try
            {
                conn = new MySqlConnection(ConnectorString);
                conn.Open();

                string stm = "SELECT `id`, `name`, `uservar` FROM `inro_user` WHERE `name`= '"+name+"' AND `uservar`= '"+password+"'";
                MySqlCommand cmd = new MySqlCommand(stm, conn);

                MySqlDataReader dataReader = cmd.ExecuteReader();

                if (dataReader.HasRows)
                {
                    _res=true;
                }

            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error: {0}", ex.ToString());

            }
            finally
            {

                if (conn != null)
                {
                    conn.Close();
                }

            }

            return _res;
        }

        //gets user from database
        private void GetSim_userdatabase(string userid, string pass)
        {
        }

    }
}
