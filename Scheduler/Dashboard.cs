using Scheduler.Enum;
using Scheduler.Models;
using Scheduler.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Scheduler
{
    public partial class Dashboard : Form
    {
        public CustomerRepository CustomerRepo { get; set; }
        public AppointmentRepository AppointmentRepo { get; set; }
        public AddressRepository AddressRepo { get; set; }
        public User LoggedInUser { get; set; }
        public List<Appointment> Appointments { get; set; }
        public List<Customer> Customers { get; set; }

        public Dashboard(User user)
        {
            AddressRepo = new AddressRepository(user);
            CustomerRepo = new CustomerRepository(user, AddressRepo);
            AppointmentRepo = new AppointmentRepository(user);
            
            InitializeComponent();
            BindGrids();
        }

        public void BindGrids()
        {
            Customers = CustomerRepo.GetAllCustomers();
            dgCustomers.DataSource = Customers;
            // It is not a listed requirement to display address in data grid. Hiding address id column.
            dgCustomers.Columns["Address"].Visible = false;
            dgCustomers.Columns["AddressId"].Visible = false;
            Appointments = AppointmentRepo.GetAllAppointments();
            dgAppointments.DataSource = Appointments;
        }

        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            var addCustomerForm = new CustomerDetail(DetailMode.Add, this, AddressRepo, CustomerRepo);
            addCustomerForm.Show();
        }

        private void btnModifyCustomer_Click(object sender, EventArgs e)
        {
            var selectedCustomer = (Customer)dgCustomers.SelectedRows[0].DataBoundItem;
            var modifyCustomerForm = new CustomerDetail(DetailMode.Modify, this, AddressRepo, CustomerRepo, selectedCustomer);
            modifyCustomerForm.Show();
        }

        private void btnDeleteCustomer_Click(object sender, EventArgs e)
        {
            var selectedCustomer = (Customer)dgCustomers.SelectedRows[0].DataBoundItem;
            AddressRepo.DeleteAddress(selectedCustomer.AddressId);
            CustomerRepo.DeleteCustomer(selectedCustomer.Id);
            BindGrids();
        }

        private void btnAddAppointment_Click(object sender, EventArgs e)
        {
            var addAppointmentForm = new AppointmentDetail(DetailMode.Add, this, AppointmentRepo, CustomerRepo, AddressRepo);
            addAppointmentForm.Show();
        }

        private void btnModifyAppointment_Click(object sender, EventArgs e)
        {
            var selectedAppointment = (Appointment)dgAppointments.SelectedRows[0].DataBoundItem;
            var modifyAppointmentForm = new AppointmentDetail(DetailMode.Modify, this, AppointmentRepo, CustomerRepo, AddressRepo, selectedAppointment);
            modifyAppointmentForm.Show();
        }

        private void btnDeleteAppointment_Click(object sender, EventArgs e)
        {
            var selectedAppointment = (Appointment)dgAppointments.SelectedRows[0].DataBoundItem;
            AppointmentRepo.DeleteAppointment(selectedAppointment.Id);
            BindGrids();
        }

        private void Dashboard_Click(Object sender, EventArgs e)
        {
            MessageBox.Show("You are in the Control.GotFocus event.");
        }

        private void reminder_Tick(object sender, EventArgs e)
        {
            var upcomingAppointment = CheckForUpcomingAppointment();
            MessageBox.Show("Alert! Upcoming Appointment");
        }

        // In the database that the assignment says we cannot alter, appointments are not tied to users. There is no persisted user id.
        // I cannot base this off of the login in absence of an fk relationship - checking all appointments instead.
        private bool CheckForUpcomingAppointment()
        {
            var beginReminderInterval = DateTime.Now;
            var endReminderInterval = DateTime.Now.AddMinutes(15);

            foreach (var appointment in Appointments)
            {
                if (appointment.Start > beginReminderInterval && appointment.Start < endReminderInterval)
                {
                    return true;
                }
            }
            return false;
        }

        private void btnTypesReport_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Types are not currently persisted in unalterable database schema.  Appointment Types by Month available in next update.");
        }

        private void btnMasterSchedule_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Appointments are not currently tied to users in unalterable database schema.  Master Schedule available in next update.");
        }

        private void btnCustomersByCity_Click(object sender, EventArgs e)
        {
            var cities = AddressRepo.GetAllCities();
            var report = "";

            foreach (var city in cities)
            {
                var customersInThisCity = Customers.Where(c => c.Address.CityId == city.Id).ToList();
                report += $"{city.Name}: {customersInThisCity.Count} customers \n";
            }

            MessageBox.Show(report);
        }

        private void btnWeekly_Click(object sender, EventArgs e)
        {
            var weekly = new Weekly();
            weekly.Show();
        }
    }
}
