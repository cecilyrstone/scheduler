using MySql.Data.MySqlClient;
using Scheduler.Models;
using Scheduler.Services;
using System;
using System.Data;
using System.IO;
using System.Reflection;
using System.Resources;

namespace Scheduler.Repository
{
    public class LoginRepository
    {
        public readonly string Server = "52.206.157.109";
        public readonly string Database = "U04bgv";
        public readonly string Uid = "U04bgv";
        public readonly string Password = "53688194549";
        public User LoggedInUser;

        public string ConnectionString
        {
            get { return $"SERVER={Server}; DATABASE={Database}; Uid={Uid}; Pwd={Password};" + "SslMode=None; Convert Zero Datetime = True;"; }
        }

        public LoginRepository()
        {
            AddAutoIncrement();
            PopulateData();
        }

        public LoginResult TryLogin(string username, string password)
        {
            ResourceManager spanishResources = new ResourceManager("Scheduler.Repository.Spanish", Assembly.GetExecutingAssembly());
            ResourceManager englishResources = new ResourceManager("Scheduler.Repository.English", Assembly.GetExecutingAssembly());
            LocationService location = new LocationService();
            var result = new LoginResult();
            result.Successful = false;

            MySqlConnection connection = new MySqlConnection(ConnectionString);
            connection.Open();

            var sql = $"SELECT * FROM user WHERE userName = @UserName AND password = @Password";

            try
            {
                MySqlCommand cmd = new MySqlCommand(sql, connection);
                cmd.Parameters.Add("@UserName", MySqlDbType.VarChar);
                cmd.Parameters.Add("@Password", MySqlDbType.VarChar);

                cmd.Parameters["@UserName"].Value = username;
                cmd.Parameters["@Password"].Value = password;
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    result.Successful = true;
                    result.User = ConstructUser(reader);
                    LogActivity(result.User);
                }
                else
                {
                    result.Message = (location.CurrentCountryName == "Mexico") ?
                                     spanishResources.GetString("UsernameAndPasswordDidNotMatch") :
                                     englishResources.GetString("UsernameAndPasswordDidNotMatch");
                }
                reader.Close();
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

            return result;
        }

        public User GetUser(string username)
        {
            var user = new User();
            MySqlConnection connection = new MySqlConnection(ConnectionString);
            connection.Open();

            var sql = $"SELECT * FROM user WHERE userName = @UserName";

            try
            {
                MySqlCommand cmd = new MySqlCommand(sql, connection);
                cmd.Parameters.Add("@UserName", MySqlDbType.VarChar);
                cmd.Parameters["@UserName"].Value = username;

                var reader = cmd.ExecuteReader();
                user = ConstructUser(reader);
                reader.Close();
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

            return user;
        }

        public void SaveNewUser(User user)
        {
            MySqlConnection connection = new MySqlConnection(ConnectionString);
            connection.Open();
            var sql = $"INSERT INTO user (userName, password, active, createDate, createdBy, lastUpdate, lastUpdateBy) " +
                      $"VALUES (@UserName, @Password, @Active, @CreateDate, @CreatedBy, @LastUpdate, @LastUpdateBy)";
            try
            {
                MySqlCommand cmd = new MySqlCommand(sql, connection);
                cmd.Parameters.Add("@UserName", MySqlDbType.VarChar);
                cmd.Parameters.Add("@Password", MySqlDbType.VarChar);
                cmd.Parameters.Add("@Active", MySqlDbType.VarChar);
                cmd.Parameters.Add("@CreateDate", MySqlDbType.VarChar);
                cmd.Parameters.Add("@CreatedBy", MySqlDbType.VarChar);
                cmd.Parameters.Add("@LastUpdate", MySqlDbType.VarChar);
                cmd.Parameters.Add("@LastUpdateBy", MySqlDbType.VarChar);

                cmd.Parameters["@UserName"].Value = user.UserName;
                cmd.Parameters["@Password"].Value = user.Password;
                cmd.Parameters["@Active"].Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                cmd.Parameters["@CreateDate"].Value = user.Password;
                cmd.Parameters["@CreatedBy"].Value = LoggedInUser.UserName;
                cmd.Parameters["@LastUpdate"].Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                cmd.Parameters["@LastUpdateBy"].Value = LoggedInUser.UserName;
                cmd.ExecuteNonQuery();
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

        private User ConstructUser(MySqlDataReader reader)
        {
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    return new User
                    {
                        Id = reader.GetInt32("userId"),
                        UserName = reader.GetString("userName"),
                        Password = reader.GetString("password"),
                        Active = reader.GetBoolean("active")
                    };
                }
            }

            return null;
        }

        public void PopulateData()
        {
            var user1 = new User
            {
                UserName = "mom",
                Password = "coffee",
                Active = true
            };

            var user2 = new User
            {
                UserName = "dad",
                Password = "naps",
                Active = false
            };

            var user3 = new User
            {
                UserName = "test",
                Password = "test",
                Active = false
            };

            LoggedInUser = user1;

            SaveNewUser(user1);
            SaveNewUser(user2);
            SaveNewUser(user3);
        }

        public void AddAutoIncrement()
        {
            MySqlConnection connection = new MySqlConnection(ConnectionString);
            connection.Open();
            var sql = $"ALTER TABLE user MODIFY userId MEDIUMINT NOT NULL AUTO_INCREMENT;";
            try
            {
                MySqlCommand cmd = new MySqlCommand(sql, connection);
                cmd.ExecuteNonQuery();
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

        private void LogActivity(User user)
        {
            using (StreamWriter writetext = new StreamWriter("logins.txt"))
            {
                writetext.WriteLine(user.UserName + " successful login at " + DateTime.Now.ToString());
            }
        }
    }
}
