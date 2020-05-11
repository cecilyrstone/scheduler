using MySql.Data.MySqlClient;
using Scheduler.Models;
using Scheduler.Services;
using System;
using System.Collections.Generic;
using System.Data;

namespace Scheduler.Repository
{
    public class AppointmentRepository : MySqlRepository
    {
        public LocationService LocationService { get; set; }
        public User LoggedInUser { get; set; }

        public AppointmentRepository(User user)
        {
            LoggedInUser = user;
            // Leaving the method here because the database schema seems volatile, for something that cannot be changed.
            // AddAutoIncrement();
            LocationService = new LocationService();
            PopulateData();
        }

        public Appointment GetAppointment(int id)
        {
            var appointment = new Appointment();
            MySqlConnection connection = new MySqlConnection(ConnectionString);
            connection.Open();

            var sql = $"SELECT * FROM appointment WHERE appointmentId = @Id";

            try
            {
                MySqlCommand cmd = new MySqlCommand(sql, connection);
                cmd.Parameters.Add("@Id", MySqlDbType.Int32);
                cmd.Parameters["@Id"].Value = id;
                var reader = cmd.ExecuteReader();
                appointment = ConstructAppointment(reader);
                LocationService.AdjustAppointmentTimeForZone(appointment);
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

            return appointment;
        }

        public void SaveAppointment(Appointment appointment)
        {
            MySqlConnection connection = new MySqlConnection(ConnectionString);
            connection.Open();
            var sql = $"INSERT INTO appointment (customerId, title, description, location, contact, url, start, end, createDate, createdBy, lastUpdate, lastUpdateBy) " +
                      $"VALUES (@CustomerId, @Title, " +
                      $"@Description, @Location, @Contact," +
                      $"@Url, @Start, @End, " +
                      $"@CreateDate, @CreatedBy, @LastUpdate, @LastUpdateBy);";
            try
            {
                MySqlCommand cmd = new MySqlCommand(sql, connection);
                cmd.Parameters.Add("@CustomerId", MySqlDbType.Int32);
                cmd.Parameters.Add("@Title", MySqlDbType.VarChar);
                cmd.Parameters.Add("@Description", MySqlDbType.VarChar);
                cmd.Parameters.Add("@Location", MySqlDbType.VarChar);
                cmd.Parameters.Add("@Contact", MySqlDbType.VarChar);
                cmd.Parameters.Add("@Url", MySqlDbType.VarChar);
                cmd.Parameters.Add("@Start", MySqlDbType.VarChar);
                cmd.Parameters.Add("@End", MySqlDbType.VarChar);
                cmd.Parameters.Add("@CreateDate", MySqlDbType.VarChar);
                cmd.Parameters.Add("@CreatedBy", MySqlDbType.VarChar);
                cmd.Parameters.Add("@LastUpdate", MySqlDbType.VarChar);
                cmd.Parameters.Add("@LastUpdateBy", MySqlDbType.VarChar);

                cmd.Parameters["@CustomerId"].Value = appointment.CustomerId;
                cmd.Parameters["@Title"].Value = appointment.Title;
                cmd.Parameters["@Description"].Value = appointment.Description;
                cmd.Parameters["@Location"].Value = appointment.Location;
                cmd.Parameters["@Contact"].Value = appointment.Contact;
                cmd.Parameters["@Url"].Value = appointment.Url;
                cmd.Parameters["@Start"].Value = appointment.Start.ToString("yyyy-MM-dd HH:mm:ss");
                cmd.Parameters["@End"].Value = appointment.End.ToString("yyyy-MM-dd HH:mm:ss");
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

        public void UpdateAppointment(Appointment appointment)
        {
            MySqlConnection connection = new MySqlConnection(ConnectionString);
            connection.Open();
            var sql = $"UPDATE appointment " +
                      $"SET customerId = @CustomerId, " +
                      $"userId = @UserId, " +
                      $"title = @Title, " +
                      $"description = @Description, " +
                      $"location = @Location, " +
                      $"contact = @Contact, " +
                      $"type = @Type, " +
                      $"url = @Url, " +
                      $"start = @Start, " +
                      $"end = @End" +
                      $"lastUpdate = @LastUpdate, " +
                      $"lastUpdateBy = @LastUpdateBy WHERE appointmentId = @AppointmentId;";
            try
            {
                MySqlCommand cmd = new MySqlCommand(sql, connection);
                cmd.Parameters.Add("@AppointmentId", MySqlDbType.Int32);
                cmd.Parameters.Add("@CustomerId", MySqlDbType.Int32);
                cmd.Parameters.Add("@UserId", MySqlDbType.Int32);
                cmd.Parameters.Add("@Title", MySqlDbType.VarChar);
                cmd.Parameters.Add("@Description", MySqlDbType.VarChar);
                cmd.Parameters.Add("@Location", MySqlDbType.VarChar);
                cmd.Parameters.Add("@Contact", MySqlDbType.VarChar);
                cmd.Parameters.Add("@Type", MySqlDbType.VarChar);
                cmd.Parameters.Add("@Url", MySqlDbType.VarChar);
                cmd.Parameters.Add("@Start", MySqlDbType.VarChar);
                cmd.Parameters.Add("@End", MySqlDbType.VarChar);
                cmd.Parameters.Add("@LastUpdate", MySqlDbType.VarChar);
                cmd.Parameters.Add("@LastUpdateBy", MySqlDbType.VarChar);

                cmd.Parameters["@AppointmentId"].Value = appointment.Id;
                cmd.Parameters["@CustomerId"].Value = appointment.CustomerId;
                cmd.Parameters["@Title"].Value = appointment.Title;
                cmd.Parameters["@Description"].Value = appointment.Description;
                cmd.Parameters["@Location"].Value = appointment.Location;
                cmd.Parameters["@Contact"].Value = appointment.Contact;
                cmd.Parameters["@Url"].Value = appointment.Url;
                cmd.Parameters["@Start"].Value = TimeZoneInfo.ConvertTimeToUtc(appointment.Start);
                cmd.Parameters["@End"].Value = TimeZoneInfo.ConvertTimeToUtc(appointment.End);
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

        public void DeleteAppointment(int id)
        {
            MySqlConnection connection = new MySqlConnection(ConnectionString);
            connection.Open();

            try
            {
                var sql = $"DELETE FROM appointment WHERE appointmentId = @Id;";
                MySqlCommand cmd = new MySqlCommand(sql, connection);
                cmd.Parameters.Add("@Id", MySqlDbType.Int32);
                cmd.Parameters["@Id"].Value = id;
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

        public List<Appointment> GetAllAppointments()
        {
            var appointments = new List<Appointment>();

            MySqlConnection connection = new MySqlConnection(ConnectionString);
            connection.Open();

            var sql = $"SELECT * FROM appointment;";

            try
            {
                MySqlCommand cmd = new MySqlCommand(sql, connection);
                var reader = cmd.ExecuteReader();
                appointments = ConstructAppointments(reader);
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

            return appointments;
        }

        private Appointment ConstructAppointment(MySqlDataReader reader)
        {
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    return new Appointment
                    {
                        Id = reader.GetInt32("appointmentId"),
                        CustomerId = reader.GetInt32("customerId"),
                        Title = reader.GetString("title"),
                        Description = reader.GetString("description"),
                        Location = reader.GetString("location"),
                        Contact = reader.GetString("contact"),
                        Url = reader.GetString("url"),
                        Start = reader.GetDateTime("start"),
                        End = reader.GetDateTime("end"),
                    };
                }
            }

            return null;
        }

        private List<Appointment> ConstructAppointments(MySqlDataReader reader)
        {
            if (reader.HasRows)
            {
                var appointments = new List<Appointment>();

                while (reader.Read())
                {
                    var appointment = new Appointment
                    {
                        Id = reader.GetInt32("appointmentId"),
                        CustomerId = reader.GetInt32("customerId"),
                        Title = reader.GetString("title"),
                        Description = reader.GetString("description"),
                        Location = reader.GetString("location"),
                        Contact = reader.GetString("contact"),
                        Url = reader.GetString("url"),
                        Start = reader.GetDateTime("start"),
                        End = reader.GetDateTime("end"),
                    };

                    appointments.Add(appointment);
                }
                LocationService.AdjustAppointmentTimesForZone(appointments);

                return appointments;
            }

            return null;
        }

        public override void PopulateData()
        {
            var appointment1 = new Appointment
            {
                CustomerId = 1,
                Title = "Eat bananas with the baby",
                Description = "The baby wants to eat bananas. But she can't do it alone. She has to force feed you first",
                Location = "Home",
                Contact = "The baby",
                Url = "",
                Start = DateTime.Now.AddDays(2),
                End = DateTime.Now.AddDays(2).AddHours(1)
            };

            var appointment2 = new Appointment
            {
                CustomerId = 2,
                Title = "Go to school program",
                Description = "There is a bug themed kindergarten program. You must handmake a costume somehow",
                Location = "School",
                Contact = "The Julie",
                Url = "",
                Start = DateTime.Now.AddDays(7),
                End = DateTime.Now.AddDays(7).AddHours(4)
            };

            var appointment3 = new Appointment
            {
                CustomerId = 3,
                Title = "Take the dog to the vet",
                Description = "She won't heat unless you put ketchup on her food. Is she sick or just spoiled?",
                Location = "The Vet",
                Contact = "The Dog",
                Url = "",
                Start = DateTime.Now.AddDays(9),
                End = DateTime.Now.AddDays(9).AddHours(2)
            };

            var appointment4 = new Appointment
            {
                CustomerId = 1,
                Title = "Take the baby to yoga",
                Description = "Take the baby to yoga, for some mysterious reason",
                Location = "Yoga Studio",
                Contact = "The Baby",
                Url = "",
                Start = DateTime.Now.AddDays(3),
                End = DateTime.Now.AddDays(3).AddHours(2)
            };

            var appointment5 = new Appointment
            {
                CustomerId = 1,
                Title = "Take a nice nap",
                Description = "Soft couch and cold beer",
                Location = "Living room",
                Contact = "None",
                Url = "",
                Start = DateTime.Now.AddHours(2),
                End = DateTime.Now.AddHours(3)
            };

            SaveAppointment(appointment1);
            SaveAppointment(appointment2);
            SaveAppointment(appointment3);
            SaveAppointment(appointment4);
            SaveAppointment(appointment5);
        }

        public void AddAutoIncrement()
        {
            MySqlConnection connection = new MySqlConnection(ConnectionString);
            connection.Open();
            var sql = $"ALTER TABLE appointment MODIFY appointmentId MEDIUMINT NOT NULL AUTO_INCREMENT;";
            try
            {
                MySqlCommand cmd = new MySqlCommand(sql, connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
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