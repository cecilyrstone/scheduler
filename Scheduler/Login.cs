using Scheduler.Repository;
using Scheduler.Services;
using System;
using System.Reflection;
using System.Resources;
using System.Windows.Forms;

namespace Scheduler
{
    public partial class Login : Form
    {
        public LoginRepository Repo { get; set; }
        public LocationService locationService { get; set; }

        public Login()
        {
            InitializeComponent();
            Repo = new LoginRepository();
            locationService = new LocationService();
            btnLogin.Enabled = false;
            lblError.Visible = false;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            var result = Repo.TryLogin(txtUsername.Text, txtPassword.Text);
            if (result.Successful)
            {
                var dashboard = new Dashboard(result.User);
                dashboard.Show();
                this.Hide();
            }
            else
            {
                lblError.Visible = true;
                lblError.Text = result.Message;
                txtUsername.Text = "";
                txtPassword.Text = "";
            }
        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtUsername.Text) && !string.IsNullOrEmpty(txtPassword.Text))
                btnLogin.Enabled = true;
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtUsername.Text) && !string.IsNullOrEmpty(txtPassword.Text))
                btnLogin.Enabled = true;
        }

        private void Login_Load(object sender, EventArgs e)
        {
            ResourceManager spanishResources = new ResourceManager("Scheduler.Repository.Spanish", Assembly.GetExecutingAssembly());

            if (locationService.CurrentCountryName == "Mexico")
            {
                lblUsername.Text = spanishResources.GetString("lblUsername");
                lblPassword.Text = spanishResources.GetString("lblPassword");
                lblWelcome.Text = spanishResources.GetString("lblWelcome");
                btnLogin.Text = spanishResources.GetString("btnLogin");
            }
        }
    }
}
