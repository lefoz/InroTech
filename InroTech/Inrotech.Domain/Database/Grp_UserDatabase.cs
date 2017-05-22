using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Data.Sql;
using MySql.Data.MySqlClient;

namespace Inrotech.Domain.UserDb
{

    public class Grp_UserDatabase : IUserDb
    {
        private String ConnectorString;
        MySqlConnection conn;

        public Grp_UserDatabase()
        {
            ConnectorString = @"server=mysql45.unoeuro.com;Port=3306;userid=kasper_mad_com;
            password=Gruppe3;database=kasper_madsen_com_db2";
        }

        public bool GetUser(string name, string pass)
        {
            var _res = false;
            try
            {
                conn = new MySqlConnection(ConnectorString);
                conn.Open();

                string stm = "SELECT `id`, `name`, `uservar` FROM `inro_user` WHERE `name`= '"+ name + "' AND `uservar`= '"+ pass + "'";
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

    }
}
