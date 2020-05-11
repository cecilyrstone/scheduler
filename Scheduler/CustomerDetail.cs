using Scheduler.Enum;
using Scheduler.Models;
using Scheduler.Repository;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;

namespace Scheduler
{
    public partial class CustomerDetail : Form
    {
        public DetailMode Mode { get; set; }
        public Customer CustomerToModify { get; set; }
        public Address AddressToModify { get; set; }
        public List<City> Cities { get; set; }
        public Dashboard Dashboard { get; set; }
        public CustomerRepository CustomerRepo { get; set; }
        public AddressRepository AddressRepo { get; set; }

        public CustomerDetail(DetailMode mode, 
                              Dashboard parent, 
                              AddressRepository addressRepo,
                              CustomerRepository custRepo,
                              Customer customerToModify = null)
        {
            CustomerRepo = custRepo;
            AddressRepo = addressRepo;

            Mode = mode;
            Dashboard = parent;
            if (customerToModify != null)
            {
                CustomerToModify = customerToModify;
                AddressToModify = AddressRepo.GetAddress(customerToModify.AddressId);
            }
            
            Cities = AddressRepo.GetAllCities();

            InitializeComponent();

            lblError.Visible = false;
            cmbCity.ValueMember = "Id";
            cmbCity.DisplayMember = "Name";
            cmbCity.DataSource = Cities;
        }

        private void CustomerDetail_Load(object sender, EventArgs e)
        {
            if (Mode == DetailMode.Add)
            {
                lblHeader.Text = "Add Customer";
                lblId.Visible = false;
                lblCustId.Visible = false;
            }
            else if (Mode == DetailMode.Modify)
            {
                lblHeader.Text = "Modify Customer";
                lblId.Visible = true;
                lblCustId.Visible = true;

                LoadData();
            }
            else
            {
                Close();
            }
        }

        private void LoadData()
        {
            lblId.Text = CustomerToModify.Id.ToString();
            txtName.Text = CustomerToModify.CustomerName;

            txtAddress1.Text = AddressToModify.Address1;
            txtAddress2.Text = AddressToModify.Address2;
            txtPhone.Text = AddressToModify.Phone;
            txtPostalCode.Text = AddressToModify.PostalCode;
            // lambda expression for simple retrieval of city
            cmbCity.SelectedIndex = Cities.Where(c => c.Id == AddressToModify.CityId).FirstOrDefault().Id;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateForm())
            {
                lblError.Visible = true;
                lblError.Text = "Please fill out all customer data.";
                return;
            }
                

            if (Mode == DetailMode.Add)
            {
                var address = ConstructAddress();
                var addressId = AddressRepo.SaveAddress(address);
                var customer = ConstructCustomer(addressId);
                CustomerRepo.SaveCustomer(customer);
            }
            else if (Mode == DetailMode.Modify)
            {
                var address = ConstructAddress();
                AddressRepo.UpdateAddress(address);
                var customer = ConstructCustomer(address.Id);
                CustomerRepo.UpdateCustomer(customer);
            }

            Dashboard.BindGrids();
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private Customer ConstructCustomer(int addressId)
        {
            return new Customer()
            {
                CustomerName = txtName.Text,
                AddressId = addressId
            };
        }

        private Address ConstructAddress()
        {
            var city = (City)cmbCity.SelectedItem;

            var address = new Address()
            {
                Address1 = txtAddress1.Text,
                Address2 = txtAddress2.Text,
                CityId = city.Id,
                PostalCode = txtPostalCode.Text,
                Phone = txtPhone.Text
            };

            if (Mode == DetailMode.Modify)
                address.Id = AddressToModify.Id;

            return address;
        }

        private bool ValidateForm()
        {
            if (string.IsNullOrEmpty(txtName.Text))
                return false;

            if (string.IsNullOrEmpty(txtAddress1.Text))
                return false;

            if (string.IsNullOrEmpty(txtPostalCode.Text))
                return false;

            if (string.IsNullOrEmpty(txtPhone.Text))
                return false;

            return true;
        }
    }
}
