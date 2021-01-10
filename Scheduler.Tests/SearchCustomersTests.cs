using Microsoft.VisualStudio.TestTools.UnitTesting;
using Scheduler.Models;
using Scheduler.Services;
using System.Collections.Generic;

namespace Scheduler.Tests
{
    [TestClass]
    public class SearchCustomersTests
    {
        [TestMethod]
        public void SearchCustomersReturnsFullSetWhenSearchTextNull()
        {
            var customers = SetUpCustomers();
            var results = SearchService.SearchCustomers(customers, null);

            Assert.AreEqual(customers.Count, results.Count);
        }

        [TestMethod]
        public void SearchCustomersReturnsFullSetWhenSearchTextEmptyString()
        {
            var customers = SetUpCustomers();
            var results = SearchService.SearchCustomers(customers, "");

            Assert.AreEqual(customers.Count, results.Count);
        }

        [TestMethod]
        public void SearchCustomersReturnsFullSetWhenSearchTextWhitespace()
        {
            var customers = SetUpCustomers();
            var results = SearchService.SearchCustomers(customers, "       ");

            Assert.AreEqual(customers.Count, results.Count);
        }

        [TestMethod]
        public void SearchCustomersReturnsEmptySetWhenNoMatch()
        {
            var customers = SetUpCustomers();
            var results = SearchService.SearchCustomers(customers, "description");

            Assert.AreEqual(0, results.Count);
        }

        [TestMethod]
        public void SearchCustomersIsCaseInsensitive()
        {
            var customers = SetUpCustomers();
            var results = SearchService.SearchCustomers(customers, "name1");

            Assert.AreEqual(2, results.Count);
        }

        [TestMethod]
        public void SearchCustomersReturnsResultsWhenMatch()
        {
            var customers = SetUpCustomers();
            var results = SearchService.SearchCustomers(customers, "Name1");

            Assert.AreEqual(2, results.Count);
        }

        private List<Customer> SetUpCustomers()
        {
            var customers = new List<Customer>();
            var customer1 = new Customer
            {
                Id = 1,
                CustomerName = "Name1"
            };
            var customer2 = new Customer
            {
                Id = 2,
                CustomerName = "name1"
            };
            var customer3 = new Customer
            {
                Id = 3,
                CustomerName = "Name2"
            };

            customers.Add(customer1);
            customers.Add(customer2);
            customers.Add(customer3);

            return customers;
        }
    }
}
