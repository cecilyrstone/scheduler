using MySql.Data.MySqlClient;
using Scheduler.Models;
using Scheduler.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using System.Resources;

namespace Scheduler.Repository
{
    public class LoginRepository : MySqlRepository
    {
        public User LoggedInUser;

        public LoginRepository(bool firstInstance = true)
        {
            if (firstInstance)
                PopulateData();
        }

        public LoginResult TryLogin(string username, string password)
        {
            ResourceManager spanishResources = new ResourceManager("Scheduler.Repository.Spanish", Assembly.GetExecutingAssembly());
            ResourceManager englishResources = new ResourceManager("Scheduler.Repository.English", Assembly.GetExecutingAssembly());
            LocationService location = new LocationService();
            var result = new LoginResult();
            result.Successful = false;

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

        public List<User> GetAllUsers()
        {
            var users = new List<User>();
            connection.Open();

            var sql = $"SELECT * FROM user";

            try
            {
                MySqlCommand cmd = new MySqlCommand(sql, connection);
                var reader = cmd.ExecuteReader();
                users = ConstructUsers(reader);
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

            return users;
        }

        public void SaveNewUser(User user)
        {
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
                cmd.Parameters["@Active"].Value = user.Active ? 1 : 0;
                cmd.Parameters["@CreateDate"].Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
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

        private List<User> ConstructUsers(MySqlDataReader reader)
        {
            if (reader.HasRows)
            {
                var users = new List<User>();

                while (reader.Read())
                {
                    var user = new User
                    {
                        Id = reader.GetInt32("userId"),
                        UserName = reader.GetString("userName"),
                        Password = reader.GetString("password"),
                        Active = reader.GetBoolean("active")
                    };

                    users.Add(user);
                }

                return users;
            }

            return null;
        }

        public override void PopulateData()
        {
            TruncateTables();

            var user1 = new User
            {
                UserName = "cecily",
                Password = "coffee",
                Active = true
            };

            var user2 = new User
            {
                UserName = "paul",
                Password = "naps",
                Active = true
            };

            var user3 = new User
            {
                UserName = "test",
                Password = "test",
                Active = true
            };

            LoggedInUser = user1;

            SaveNewUser(user1);
            SaveNewUser(user2);
            SaveNewUser(user3);
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
