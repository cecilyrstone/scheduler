using Scheduler.Models;
using System.Collections.Generic;
using System.Linq;

namespace Scheduler.Services
{
    public static class SearchService
    {
        public static List<Appointment> SearchAppointments(List<Appointment> appointments, string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
                return appointments;

            return appointments.Where(a => a.Title.ToLower().Contains(searchText.ToLower())).ToList();
        }

        public static List<Customer> SearchCustomers(List<Customer> customers, string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
                return customers;

            return customers.Where(a => a.CustomerName.ToLower().Contains(searchText.ToLower())).ToList();
        }
    }
}
