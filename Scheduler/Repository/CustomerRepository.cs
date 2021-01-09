using MySql.Data.MySqlClient;
using Scheduler.Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace Scheduler.Repository
{
    public class CustomerRepository : MySqlRepository
    {
        public User LoggedInUser { get; set; }
        public AddressRepository AddressRepo { get; set; }

        public CustomerRepository(User user, AddressRepository addressRepo)
        {
            LoggedInUser = user;
            PopulateData();
            AddressRepo = addressRepo;
        }

        public Customer GetCustomer(int id)
        {
            var customer = new Customer();
            connection.Open();

            var sql = $"SELECT * FROM customer WHERE customerId = @Id;";

            try
            {
                MySqlCommand cmd = new MySqlCommand(sql, connection);
                cmd.Parameters.Add("@Id", MySqlDbType.VarChar);
                cmd.Parameters["@Id"].Value = id;

                var reader = cmd.ExecuteReader();
                customer = ConstructCustomer(reader);
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

            return customer;
        }

        public void SaveCustomer(Customer customer)
        {
            connection.Open();
            var sql = $"INSERT INTO customer (customerName, addressId, active, createDate, createdBy, lastUpdate, lastUpdateBy) " +
                      $"VALUES (@CustomerName, @AddressId, @Active, " +
                      $"@CreateDate, @CreatedBy, @LastUpdate, @LastUpdateBy); SELECT LAST_INSERT_ID() AS id;";
            try
            {
                MySqlCommand cmd = new MySqlCommand(sql, connection);
                cmd.Parameters.Add("@CustomerName", MySqlDbType.VarChar);
                cmd.Parameters.Add("@AddressId", MySqlDbType.Int32);
                cmd.Parameters.Add("@Active", MySqlDbType.VarChar);
                cmd.Parameters.Add("@CreateDate", MySqlDbType.VarChar);
                cmd.Parameters.Add("@CreatedBy", MySqlDbType.VarChar);
                cmd.Parameters.Add("@LastUpdate", MySqlDbType.VarChar);
                cmd.Parameters.Add("@LastUpdateBy", MySqlDbType.VarChar);

                cmd.Parameters["@CustomerName"].Value = customer.CustomerName;
                cmd.Parameters["@AddressId"].Value = customer.AddressId;
                cmd.Parameters["@Active"].Value = (customer.Active) ? "1" : "0";
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

        public void UpdateCustomer(Customer customer)
        {
            connection.Open();
            var sql = $"UPDATE customer " +
                      $"SET customerName = @CustomerName, " +
                      $"addressId = @AddressId, " +
                      $"active = @Active, " +
                      $"lastUpdate = @LastUpdate, " +
                      $"lastUpdateBy = LastUpdateBy WHERE customerId = @CustomerId;";
            try
            {
                MySqlCommand cmd = new MySqlCommand(sql, connection);
                cmd.Parameters.Add("@CustomerId", MySqlDbType.Int32);
                cmd.Parameters.Add("@CustomerName", MySqlDbType.VarChar);
                cmd.Parameters.Add("@AddressId", MySqlDbType.Int32);
                cmd.Parameters.Add("@Active", MySqlDbType.VarChar);
                cmd.Parameters.Add("@LastUpdate", MySqlDbType.VarChar);
                cmd.Parameters.Add("@LastUpdateBy", MySqlDbType.VarChar);

                cmd.Parameters["@CustomerId"].Value = customer.Id;
                cmd.Parameters["@CustomerName"].Value = customer.CustomerName;
                cmd.Parameters["@AddressId"].Value = customer.AddressId;
                cmd.Parameters["@Active"].Value = (customer.Active) ? "1" : "0";
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

        public void DeleteCustomer(int id)
        {
            connection.Open();

            try
            {
                var sql = $"DELETE FROM customer WHERE customerId = @Id;";
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

        public List<Customer> GetAllCustomers()
        {
            var customers = new List<Customer>();
            connection.Open();

            var sql = $"SELECT * FROM customer;";

            try
            {
                MySqlCommand cmd = new MySqlCommand(sql, connection);
                var reader = cmd.ExecuteReader();
                customers = ConstructCustomers(reader);
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

            foreach (var customer in customers)
            {
                try
                {
                    customer.Address = AddressRepo.GetAddress(customer.AddressId);
                }
                catch (Exception e)
                {

                }
            }

            return customers;
        }

        private Customer ConstructCustomer(MySqlDataReader reader)
        {
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    return new Customer
                    {
                        Id = reader.GetInt32("customerId"),
                        CustomerName = reader.GetString("customerName"),
                        AddressId = reader.GetInt32("addressId"),
                        Active = reader.GetBoolean("active"),
                    };
                }
            }

            return null;
        }

        private List<Customer> ConstructCustomers(MySqlDataReader reader)
        {
            if (reader.HasRows)
            {
                var customers = new List<Customer>();

                while (reader.Read())
                {
                    var customer = new Customer
                    {
                        Id = reader.GetInt32("customerId"),
                        CustomerName = reader.GetString("customerName"),
                        AddressId = reader.GetInt32("addressId"),
                        Active = reader.GetBoolean("active"),
                    };

                    customers.Add(customer);
                }
                return customers;
            }

            return null;
        }

        public override void PopulateData()
        {
            var customer1 = new Customer
            {
                CustomerName = "The Baby",
                AddressId = 2,
                Active = true
            };

            var customer2 = new Customer
            {
                CustomerName = "The Julie",
                AddressId = 2,
                Active = true
            };

            var customer3 = new Customer
            {
                CustomerName = "The Dog",
                AddressId = 2,
                Active = true
            };

            SaveCustomer(customer1);
            SaveCustomer(customer2);
            SaveCustomer(customer3);
        }
    }
}
