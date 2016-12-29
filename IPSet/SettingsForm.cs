using System;
using System.Windows.Forms;
using IPSet.Models;

namespace IPSet
{
    public partial class SettingsForm : Form
    {

        public SettingsForm()
        {
            InitializeComponent();

            var settings = DefaultValues.GetSettings();

            tbGateway.Text = settings.Gateway;
            tbSubnet.Text = settings.Subnet;
            tbProxy.Text = settings.Proxy;
            tbPort.Text = settings.Port;
            tbUsername.Text = settings.Username;
            if (!string.IsNullOrWhiteSpace(settings.Password))
            {
                tbPassword.Text = Encryptor.DecryptString(settings.Password);
            }

        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            var settings = new DefaultValues
            {
                Gateway = tbGateway.Text,
                Subnet = tbSubnet.Text,
                Proxy = tbProxy.Text,
                Port = tbPort.Text,
                Username = tbUsername.Text
            };

            if (!string.IsNullOrWhiteSpace(tbPassword.Text))
            {
                settings.Password = Encryptor.EncryptString(tbPassword.Text);
            }

            DefaultValues.SaveSettings(settings);
            Close();
        }

    }
}
