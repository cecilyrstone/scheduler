using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace Scheduler.Repository
{
    public abstract class MySqlRepository
    {
        public readonly string Server = "wgudb.ucertify.com";
        public readonly string Database = "U04bgv";
        public readonly string Uid = "U04bgv";
        public readonly string Password = "53688194549";
        public MySqlConnection connection { get; set; }

        public MySqlRepository()
        {
            connection = new MySqlConnection(ConnectionString);
        }

        public string ConnectionString
        {
            get { return $"SERVER={Server}; DATABASE={Database}; Uid={Uid}; Pwd={Password};" + "SslMode=None; Convert Zero Datetime = True;"; }
        }

        public abstract void PopulateData();

        public void TruncateTables()
        {
            connection.Open();

            try
            {
                var truncateAppointment = new MySqlCommand("SET FOREIGN_KEY_CHECKS=0; TRUNCATE TABLE appointment;", connection);
                var truncateUser = new MySqlCommand("SET FOREIGN_KEY_CHECKS=0; TRUNCATE TABLE user;", connection);
                var truncateCustomer = new MySqlCommand("SET FOREIGN_KEY_CHECKS=0; TRUNCATE TABLE customer;", connection);
                var truncateAddress = new MySqlCommand("SET FOREIGN_KEY_CHECKS=0; TRUNCATE TABLE address;", connection);
                var truncateCity = new MySqlCommand("SET FOREIGN_KEY_CHECKS=0; TRUNCATE TABLE city;", connection);
                var truncateCountry = new MySqlCommand("SET FOREIGN_KEY_CHECKS=0; TRUNCATE TABLE country; SET FOREIGN_KEY_CHECKS=1;", connection);

                truncateAppointment.ExecuteNonQuery();
                truncateUser.ExecuteNonQuery();
                truncateCustomer.ExecuteNonQuery();
                truncateAddress.ExecuteNonQuery();
                truncateCity.ExecuteNonQuery();
                truncateCountry.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }
    }
}
