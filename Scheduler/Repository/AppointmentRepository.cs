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
            LocationService = new LocationService();
            PopulateData();
        }

        public Appointment GetAppointment(int id)
        {
            var appointment = new Appointment();
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
            connection.Open();
            var sql = $"INSERT INTO appointment (customerId, userId, title, description, location, contact, type, url, start, end, createDate, createdBy, lastUpdate, lastUpdateBy) " +
                      $"VALUES (@CustomerId, @UserId, @Title, " +
                      $"@Description, @Location, @Contact," +
                      $"@Type, @Url, @Start, @End, " +
                      $"@CreateDate, @CreatedBy, @LastUpdate, @LastUpdateBy);";
            try
            {
                MySqlCommand cmd = new MySqlCommand(sql, connection);
                cmd.Parameters.Add("@CustomerId", MySqlDbType.Int32);
                cmd.Parameters.Add("@UserId", MySqlDbType.Int32);
                cmd.Parameters.Add("@Title", MySqlDbType.VarChar);
                cmd.Parameters.Add("@Description", MySqlDbType.VarChar);
                cmd.Parameters.Add("@Location", MySqlDbType.VarChar);
                cmd.Parameters.Add("@Contact", MySqlDbType.VarChar);
                cmd.Parameters.Add("@Url", MySqlDbType.VarChar);
                cmd.Parameters.Add("@Start", MySqlDbType.DateTime);
                cmd.Parameters.Add("@End", MySqlDbType.DateTime);
                cmd.Parameters.Add("@Type", MySqlDbType.VarChar);
                cmd.Parameters.Add("@CreateDate", MySqlDbType.VarChar);
                cmd.Parameters.Add("@CreatedBy", MySqlDbType.VarChar);
                cmd.Parameters.Add("@LastUpdate", MySqlDbType.Timestamp);
                cmd.Parameters.Add("@LastUpdateBy", MySqlDbType.VarChar);

                cmd.Parameters["@CustomerId"].Value = appointment.CustomerId;
                cmd.Parameters["@UserId"].Value = appointment.UserId;
                cmd.Parameters["@Title"].Value = appointment.Title;
                cmd.Parameters["@Description"].Value = appointment.Description;
                cmd.Parameters["@Location"].Value = appointment.Location;
                cmd.Parameters["@Contact"].Value = appointment.Contact;
                cmd.Parameters["@Url"].Value = appointment.Url;
                cmd.Parameters["@Start"].Value = appointment.Start.ToString("yyyy-MM-dd HH:mm:ss");
                cmd.Parameters["@End"].Value = appointment.End.ToString("yyyy-MM-dd HH:mm:ss");
                cmd.Parameters["@Type"].Value = appointment.Type;
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
                      $"end = @End," +
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
                cmd.Parameters.Add("@Start", MySqlDbType.DateTime);
                cmd.Parameters.Add("@End", MySqlDbType.DateTime);
                cmd.Parameters.Add("@LastUpdate", MySqlDbType.Timestamp);
                cmd.Parameters.Add("@LastUpdateBy", MySqlDbType.VarChar);

                cmd.Parameters["@AppointmentId"].Value = appointment.Id;
                cmd.Parameters["@CustomerId"].Value = appointment.CustomerId;
                cmd.Parameters["@UserId"].Value = appointment.UserId;
                cmd.Parameters["@Title"].Value = appointment.Title;
                cmd.Parameters["@Description"].Value = appointment.Description;
                cmd.Parameters["@Location"].Value = appointment.Location;
                cmd.Parameters["@Contact"].Value = appointment.Contact;
                cmd.Parameters["@Url"].Value = appointment.Url;
                cmd.Parameters["@Start"].Value = TimeZoneInfo.ConvertTimeToUtc(appointment.Start);
                cmd.Parameters["@End"].Value = TimeZoneInfo.ConvertTimeToUtc(appointment.End);
                cmd.Parameters["@Type"].Value = appointment.Type;
                cmd.Parameters["@LastUpdate"].Value = DateTime.Now;
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

        public void DeleteAppointmentsForCustomer(int customerId)
        {
            connection.Open();

            try
            {
                var sql = $"DELETE FROM appointment WHERE customerId = @customerId;";
                MySqlCommand cmd = new MySqlCommand(sql, connection);
                cmd.Parameters.Add("@customerId", MySqlDbType.Int32);
                cmd.Parameters["@customerId"].Value = customerId;
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

        public List<Appointment> GetAppointmentsForUser()
        {
            var appointments = new List<Appointment>();

            connection.Open();

            var sql = $"SELECT * FROM appointment where userId = @userId;";

            try
            {
                MySqlCommand cmd = new MySqlCommand(sql, connection);
                cmd.Parameters.Add("@userId", MySqlDbType.Int32);
                cmd.Parameters["@userId"].Value = LoggedInUser.Id;
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
                        UserId = reader.GetInt32("userId"),
                        Title = reader.GetString("title"),
                        Description = reader.GetString("description"),
                        Location = reader.GetString("location"),
                        Contact = reader.GetString("contact"),
                        Url = reader.GetString("url"),
                        Start = reader.GetDateTime("start"),
                        End = reader.GetDateTime("end"),
                        Type = reader.GetString("type")
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
                        UserId = reader.GetInt32("userId"),
                        Title = reader.GetString("title"),
                        Description = reader.GetString("description"),
                        Location = reader.GetString("location"),
                        Contact = reader.GetString("contact"),
                        Url = reader.GetString("url"),
                        Start = reader.GetDateTime("start"),
                        End = reader.GetDateTime("end"),
                        Type = reader.GetString("type")
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
                UserId = 1,
                Title = "Eat bananas with the baby",
                Description = "The baby wants to eat bananas. But she can't do it alone. She has to force feed you first",
                Location = "Home",
                Contact = "The baby",
                Url = "",
                Start = DateTime.Now.AddDays(2),
                End = DateTime.Now.AddDays(2).AddHours(1),
                Type = "parenting"
            };

            var appointment2 = new Appointment
            {
                CustomerId = 2,
                UserId = 1,
                Title = "Go to school program",
                Description = "There is a bug themed kindergarten program. You must handmake a costume somehow",
                Location = "School",
                Contact = "The Julie",
                Url = "",
                Start = DateTime.Now.AddDays(7),
                End = DateTime.Now.AddDays(7).AddHours(4),
                Type = "parenting"
            };

            var appointment3 = new Appointment
            {
                CustomerId = 3,
                UserId = 2,
                Title = "Take the dog to the vet",
                Description = "She won't heat unless you put ketchup on her food. Is she sick or just spoiled?",
                Location = "The Vet",
                Contact = "The Dog",
                Url = "",
                Start = DateTime.Now.AddDays(9),
                End = DateTime.Now.AddDays(9).AddHours(2),
                Type = "dog"
            };

            var appointment4 = new Appointment
            {
                CustomerId = 1,
                UserId = 1,
                Title = "Take the baby to yoga",
                Description = "Take the baby to yoga, for some mysterious reason",
                Location = "Yoga Studio",
                Contact = "The Baby",
                Url = "",
                Start = DateTime.Now.AddDays(3),
                End = DateTime.Now.AddDays(3).AddHours(2),
                Type = "parenting"
            };

            var appointment5 = new Appointment
            {
                CustomerId = 1,
                UserId = 2,
                Title = "Take a nice nap",
                Description = "Soft couch and cold beer",
                Location = "Living room",
                Contact = "None",
                Url = "",
                Start = DateTime.Now.AddHours(2),
                End = DateTime.Now.AddHours(3),
                Type = "restorative"
            };

            SaveAppointment(appointment1);
            SaveAppointment(appointment2);
            SaveAppointment(appointment3);
            SaveAppointment(appointment4);
            SaveAppointment(appointment5);
        }
    }
}