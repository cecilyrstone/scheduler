using Microsoft.VisualStudio.TestTools.UnitTesting;
using Scheduler.Models;
using Scheduler.Services;
using System.Collections.Generic;

namespace Scheduler.Tests
{
    [TestClass]
    public class SearchAppointmentTests
    {
        [TestMethod]
        public void SearchAppointmentsReturnsFullSetWhenSearchTextNull()
        {
            var appointments = SetUpAppointments();
            var results = SearchService.SearchAppointments(appointments, null);

            Assert.AreEqual(appointments.Count, results.Count);
        }

        [TestMethod]
        public void SearchAppointmentsReturnsFullSetWhenSearchTextEmptyString()
        {
            var appointments = SetUpAppointments();
            var results = SearchService.SearchAppointments(appointments, "");

            Assert.AreEqual(appointments.Count, results.Count);
        }

        [TestMethod]
        public void SearchAppointmentsReturnsFullSetWhenSearchTextWhitespace()
        {
            var appointments = SetUpAppointments();
            var results = SearchService.SearchAppointments(appointments, "       ");

            Assert.AreEqual(appointments.Count, results.Count);
        }

        [TestMethod]
        public void SearchAppointmentsReturnsEmptySetWhenNoMatch()
        {
            var appointments = SetUpAppointments();
            var results = SearchService.SearchAppointments(appointments, "description");

            Assert.AreEqual(0, results.Count);
        }

        [TestMethod]
        public void SearchAppointmentsReturnsResultsWhenMatch()
        {
            var appointments = SetUpAppointments();
            var results = SearchService.SearchAppointments(appointments, "Title1");

            Assert.AreEqual(2, results.Count);
        }

        [TestMethod]
        public void SearchAppointmentsIsCaseInsensitive()
        {
            var appointments = SetUpAppointments();
            var results = SearchService.SearchAppointments(appointments, "title1");

            Assert.AreEqual(2, results.Count);
        }

        private List<Appointment> SetUpAppointments()
        {
            var appointments = new List<Appointment>();
            var appointment1 = new Appointment
            {
                Id = 1,
                CustomerId = 1,
                UserId = 1,
                Title = "Title1",
                Description = "Description1",
                Location = "Location1",
                Contact = "Contact1",
                Url = "Url1",
                Type = "Type1"
            };
            var appointment2 = new Appointment
            {
                Id = 2,
                CustomerId = 1,
                UserId = 1,
                Title = "Title2",
                Description = "Description1",
                Location = "Location1",
                Contact = "Contact1",
                Url = "Url1",
                Type = "Type1"
            };
            var appointment3 = new Appointment
            {
                Id = 3,
                CustomerId = 1,
                UserId = 1,
                Title = "title1",
                Description = "Description1",
                Location = "Location1",
                Contact = "Contact1",
                Url = "Url1",
                Type = "Type1"
            };

            appointments.Add(appointment1);
            appointments.Add(appointment2);
            appointments.Add(appointment3);

            return appointments;
        }
    }
}
