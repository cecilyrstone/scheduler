using MySql.Data.MySqlClient;
using Scheduler.Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace Scheduler.Repository
{
    public class AddressRepository : MySqlRepository
    {
        public User LoggedInUser { get; set; }

        public AddressRepository(User user)
        {
            LoggedInUser = user;
            InitializeCitiesAndCountries();
            PopulateData();
        }

        public Address GetAddress(int id)
        {
            var address = new Address();
            connection.Open();

            var sql = $"SELECT * FROM address WHERE addressid = @id";

            try
            {
                MySqlCommand cmd = new MySqlCommand(sql, connection);
                cmd.Parameters.Add("@Id", MySqlDbType.Int32);
                cmd.Parameters["@Id"].Value = id;
                var reader = cmd.ExecuteReader();
                address = ConstructAddress(reader);
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

            return address;
        }

        public int SaveAddress(Address address)
        {
            connection.Open();
            var sql = $"INSERT INTO address (address, address2, cityId, postalCode, phone, createDate, createdBy, lastUpdate, lastUpdateBy) " +
                      $"VALUES (@Address1, @Address2, @CityId, @PostalCode, @Phone, " +
                      $"CreateDate, @CreatedBy, @LastUpdate, @LastUpdateBy);";
            try
            {
                MySqlCommand cmd = new MySqlCommand(sql, connection);
                cmd.Parameters.Add("@Address1", MySqlDbType.VarChar);
                cmd.Parameters.Add("@Address2", MySqlDbType.VarChar);
                cmd.Parameters.Add("@CityId", MySqlDbType.Int32);
                cmd.Parameters.Add("@PostalCode", MySqlDbType.VarChar);
                cmd.Parameters.Add("@Phone", MySqlDbType.VarChar);
                cmd.Parameters.Add("@CreateDate", MySqlDbType.VarChar);
                cmd.Parameters.Add("@CreatedBy", MySqlDbType.VarChar);
                cmd.Parameters.Add("@LastUpdate", MySqlDbType.VarChar);
                cmd.Parameters.Add("@LastUpdateBy", MySqlDbType.VarChar);

                cmd.Parameters["@Address1"].Value = address.Address1;
                cmd.Parameters["@Address2"].Value = address.Address2;
                cmd.Parameters["@CityId"].Value = address.CityId;
                cmd.Parameters["@PostalCode"].Value = address.PostalCode;
                cmd.Parameters["@Phone"].Value = address.Phone;
                cmd.Parameters["@CreateDate"].Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                cmd.Parameters["@CreatedBy"].Value = LoggedInUser.UserName;
                cmd.Parameters["@LastUpdate"].Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                cmd.Parameters["@LastUpdateBy"].Value = LoggedInUser.UserName;

                cmd.ExecuteNonQuery();
                return Convert.ToInt32(cmd.LastInsertedId);
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

        public void UpdateAddress(Address address)
        {
            connection.Open();
            var sql = $"UPDATE address " +
                      $"SET address = @Address1, " +
                      $"address2 = @Address2, " +
                      $"cityId = @CityId, " +
                      $"postalCode = @PostalCode, " +
                      $"phone = @Phone, " +
                      $"lastUpdate = @LastUpdate, " +
                      $"lastUpdateBy = @LastUpdateBy WHERE addressId = @AddressId;";
            try
            {
                MySqlCommand cmd = new MySqlCommand(sql, connection);
                cmd.Parameters.Add("@AddressId", MySqlDbType.Int32);
                cmd.Parameters.Add("@Address1", MySqlDbType.VarChar);
                cmd.Parameters.Add("@Address2", MySqlDbType.VarChar);
                cmd.Parameters.Add("@CityId", MySqlDbType.Int32);
                cmd.Parameters.Add("@PostalCode", MySqlDbType.VarChar);
                cmd.Parameters.Add("@Phone", MySqlDbType.VarChar);
                cmd.Parameters.Add("@LastUpdate", MySqlDbType.VarChar);
                cmd.Parameters.Add("@LastUpdateBy", MySqlDbType.VarChar);

                cmd.Parameters["@AddressId"].Value = address.Id;
                cmd.Parameters["@Address1"].Value = address.Address1;
                cmd.Parameters["@Address2"].Value = address.Address2;
                cmd.Parameters["@CityId"].Value = address.CityId;
                cmd.Parameters["@PostalCode"].Value = address.PostalCode;
                cmd.Parameters["@Phone"].Value = address.Phone;
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

        public void DeleteAddress(int id)
        {
            connection.Open();

            try
            {
                var sql = $"DELETE FROM address WHERE addressId = @Id;";
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

        public List<City> GetAllCities()
        {
            var cities = new List<City>();
            connection.Open();

            var sql = $"SELECT * FROM city;";

            try
            {
                MySqlCommand cmd = new MySqlCommand(sql, connection);
                var reader = cmd.ExecuteReader();
                cities = ConstructCities(reader);
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

            return cities;
        }

        public List<Country> GetAllCountries()
        {
            var countries = new List<Country>();
            connection.Open();

            var sql = $"SELECT * FROM country;";

            try
            {
                MySqlCommand cmd = new MySqlCommand(sql, connection);
                var reader = cmd.ExecuteReader();
                countries = ConstructCountries(reader);
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

            return countries;
        }

        public void InitializeCitiesAndCountries()
        {
            var cities = new List<City>()
            {
                new City{ CountryId = 1, Name = "Nashville"},
                new City{ CountryId = 1, Name = "Los Angeles"},
                new City{ CountryId = 1, Name = "New York"},
                new City{ CountryId = 2, Name = "Oaxaca"},
                new City{ CountryId = 2, Name = "Ciudad de Mexico"}
            };

            var countries = new List<Country>()
            {
                new Country { Name="USA" },
                new Country { Name = "Mexico" }
            };

            SaveCitiesAndCountries(cities, countries);
        }

        public void SaveCitiesAndCountries(List<City> cities, List<Country> countries)
        {
            connection.Open();

            try
            {
                foreach (var country in countries)
                {
                    var sql = $"INSERT INTO country (country, createDate, createdBy, lastUpdate, lastUpdateBy) " +
                          $"VALUES (@Country, @CreateDate, @CreatedBy, @LastUpdate, @LastUpdateBy);";

                    MySqlCommand cmd = new MySqlCommand(sql, connection);
                    cmd.Parameters.Add("@Country", MySqlDbType.VarChar);
                    cmd.Parameters.Add("@CreateDate", MySqlDbType.VarChar);
                    cmd.Parameters.Add("@CreatedBy", MySqlDbType.VarChar);
                    cmd.Parameters.Add("@LastUpdate", MySqlDbType.VarChar);
                    cmd.Parameters.Add("@LastUpdateBy", MySqlDbType.VarChar);

                    cmd.Parameters["@Country"].Value = country.Name;
                    cmd.Parameters["@CreateDate"].Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    cmd.Parameters["@CreatedBy"].Value = LoggedInUser.UserName;
                    cmd.Parameters["@LastUpdate"].Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    cmd.Parameters["@LastUpdateBy"].Value = LoggedInUser.UserName;
                    cmd.ExecuteNonQuery();
                }

                foreach (var city in cities)
                {
                    var sql = $"INSERT INTO city (city, countryId, createDate, createdBy, lastUpdate, lastUpdateBy) " +
                          $"VALUES (@City, @CountryId, @CreateDate, @CreatedBy, @LastUpdate, @LastUpdateBy);";

                    MySqlCommand cmd = new MySqlCommand(sql, connection);
                    cmd.Parameters.Add("@City", MySqlDbType.VarChar);
                    cmd.Parameters.Add("@CountryId", MySqlDbType.VarChar);
                    cmd.Parameters.Add("@CreateDate", MySqlDbType.VarChar);
                    cmd.Parameters.Add("@CreatedBy", MySqlDbType.VarChar);
                    cmd.Parameters.Add("@LastUpdate", MySqlDbType.VarChar);
                    cmd.Parameters.Add("@LastUpdateBy", MySqlDbType.VarChar);

                    cmd.Parameters["@City"].Value = city.Name;
                    cmd.Parameters["@CountryId"].Value = city.CountryId;
                    cmd.Parameters["@CreateDate"].Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    cmd.Parameters["@CreatedBy"].Value = LoggedInUser.UserName;
                    cmd.Parameters["@LastUpdate"].Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    cmd.Parameters["@LastUpdateBy"].Value = LoggedInUser.UserName;
                    cmd.ExecuteNonQuery();
                }
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

        private Address ConstructAddress(MySqlDataReader reader)
        {
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    return new Address
                    {
                        Id = reader.GetInt32("addressId"),
                        Address1 = reader.GetString("address"),
                        Address2 = reader.GetString("address2"),
                        CityId = reader.GetInt32("cityId"),
                        PostalCode = reader.GetString("postalCode"),
                        Phone = reader.GetString("phone")
                    };
                }
            }

            return null;
        }

        private List<City> ConstructCities(MySqlDataReader reader)
        {
            if (reader.HasRows)
            {
                var cities = new List<City>();

                while (reader.Read())
                {
                    var city = new City
                    {
                        Id = reader.GetInt32("cityId"),
                        Name = reader.GetString("city"),
                        CountryId = reader.GetInt32("countryId")
                    };
                    cities.Add(city);
                }
                return cities;
            }

            return null;
        }

        private List<Country> ConstructCountries(MySqlDataReader reader)
        {
            if (reader.HasRows)
            {
                var countries = new List<Country>();

                while (reader.Read())
                {
                    var country = new Country
                    {
                        Id = reader.GetInt32("countryId"),
                        Name = reader.GetString("country")
                    };
                    countries.Add(country);
                }
                return countries;
            }

            return null;
        }

        public override void PopulateData()
        {
            var address1 = new Address()
            {
                Address1 = "123 Wildlife Trl",
                Address2 = "",
                CityId = 1,
                PostalCode = "37082",
                Phone = "(555) 456-7890"
            };

            var address2 = new Address()
            {
                Address1 = "123 Ranch St",
                Address2 = "",
                CityId = 2,
                PostalCode = "90210",
                Phone = "(555) 867-5309"
            };

            SaveAddress(address1);
            SaveAddress(address2);
        }
    }
}
