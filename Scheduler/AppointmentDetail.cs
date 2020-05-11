using Scheduler.Enum;
using Scheduler.Models;
using Scheduler.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Scheduler
{
    public partial class AppointmentDetail : Form
    {
        public DetailMode Mode { get; set; }
        public Appointment AppointmentToModify { get; set; }
        public Dashboard Dashboard { get; set; }
        public List<Customer> Customers { get; set; }
        public AppointmentRepository AppointmentRepo { get; set; }
        public CustomerRepository CustomerRepo { get; set; }
        public AddressRepository AddressRepo { get; set; }

        public AppointmentDetail(DetailMode mode, 
                                 Dashboard parent, 
                                 AppointmentRepository aptRepo,
                                 CustomerRepository custRepo,
                                 AddressRepository addressRepo,
                                 Appointment appointmentToModify = null)
        {
            AppointmentRepo = aptRepo;
            CustomerRepo = custRepo;
            AddressRepo = addressRepo;
            Mode = mode;
            Dashboard = parent;
            AppointmentToModify = appointmentToModify;
            Customers = CustomerRepo.GetAllCustomers();
            InitializeComponent();

            cmbCustomer.ValueMember = "Id";
            cmbCustomer.DisplayMember = "CustomerName";
            cmbCustomer.DataSource = Customers;

            dtpBegin.Format = DateTimePickerFormat.Custom;
            dtpBegin.CustomFormat = "MM/dd/yyyy hh:mm tt";

            dtpEnd.Format = DateTimePickerFormat.Custom;
            dtpEnd.CustomFormat = "MM/dd/yyyy hh:mm tt";

            lblError.Visible = false;
        }

        private void AppointmentDetail_Load(object sender, EventArgs e)
        {
            if (Mode == DetailMode.Add)
            {
                lblHeader.Text = "Add Appointment";
                lblId.Visible = false;
                lblIdValue.Visible = false;
                btnGoToCustomer.Visible = false;
            }
            else if (Mode == DetailMode.Modify)
            {
                lblHeader.Text = "Modify Appointment";
                lblId.Visible = true;
                lblIdValue.Visible = true;

                LoadData();
            }
            else
            {
                Close();
            }
        }

        private void LoadData()
        {
            // lambda expression for simple retrieval of customer
            cmbCustomer.SelectedIndex = Customers.Where(c => c.Id == AppointmentToModify.CustomerId).FirstOrDefault().Id;
            lblIdValue.Text = AppointmentToModify.Id.ToString();
            txtTitle.Text = AppointmentToModify.Title;
            txtDescription.Text = AppointmentToModify.Description;
            txtLocation.Text = AppointmentToModify.Location;
            txtContact.Text = AppointmentToModify.Contact;
            txtUrl.Text = AppointmentToModify.Url;
            dtpBegin.Value = AppointmentToModify.Start;
            dtpEnd.Value = AppointmentToModify.End;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateForm())
            {
                lblError.Visible = true;
                return;
            }

            if (Mode == DetailMode.Add)
            {
                var appointment = ConstructAppointment();
                AppointmentRepo.SaveAppointment(appointment);
            }
            else if (Mode == DetailMode.Modify)
            {
                var appointment = ConstructAppointment();
                AppointmentRepo.UpdateAppointment(appointment);
            }

            Dashboard.BindGrids();
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnGoToCustomer_Click(object sender, EventArgs e)
        {
            var customer = CustomerRepo.GetCustomer(AppointmentToModify.CustomerId);
            var modifyCustomerForm = new CustomerDetail(DetailMode.Modify, Dashboard, AddressRepo, CustomerRepo, customer);
            modifyCustomerForm.Show();
        }

        private Appointment ConstructAppointment()
        {
            var customer = (Customer)cmbCustomer.SelectedItem;

            return new Appointment()
            {
                CustomerId = customer.Id,
                Title = txtTitle.Text,
                Description = txtDescription.Text,
                Location = txtLocation.Text,
                Contact = txtContact.Text,
                Url = txtUrl.Text,
                Start = dtpBegin.Value,
                End = dtpEnd.Value
            };
        }

        private bool ValidateForm()
        {
            var beginBusinessHours = new TimeSpan(9, 0, 0);
            var endBusinessHours = new TimeSpan(17, 0, 0);
            var appointmentBeginTime = dtpBegin.Value.TimeOfDay;
            var appointmentEndTime = dtpEnd.Value.TimeOfDay;

            if (appointmentBeginTime < beginBusinessHours || appointmentEndTime > endBusinessHours)
            {
                lblError.Text = "Appointment must be during business hours.";
                return false;
            }

            return true;
        }
    }
}
