using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IPSet.Models;

namespace IPSet.UserControls
{
    public partial class NetworkInterfaceUc : UserControl
    {

        public event EventHandler Deleted;
        public event EventHandler ActivationStarted;
        public event EventHandler ActivationCompleted;

        public NetworkInterfaceUc()
        {
            InitializeComponent();

            var settings = DefaultValues.GetSettings();

            if (settings != null)
            {
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
        }

        public void Fill(NetworkInterface networkInterface)
        {

            if (networkInterface != null)
            {
                tbIP.Text = networkInterface.Ip;
                tbGateway.Text = networkInterface.Gateway;
                tbSubnet.Text = networkInterface.Subnet;
                tbProxy.Text = networkInterface.Proxy;
                tbPort.Text = networkInterface.Port;
                tbUsername.Text = networkInterface.Username;
                if (!string.IsNullOrWhiteSpace(networkInterface.Password))
                {
                    tbPassword.Text = Encryptor.DecryptString(networkInterface.Password);
                }
            }
        }

        public NetworkInterface GetNetworkInterface()
        {
            var networkInterface = new NetworkInterface
            {
                Ip = tbIP.Text,
                Gateway = tbGateway.Text,
                Subnet = tbSubnet.Text,
                Proxy = tbProxy.Text,
                Port = tbPort.Text,
                Username = tbUsername.Text
            };

            if (!string.IsNullOrWhiteSpace(tbPassword.Text))
            {
                networkInterface.Password = Encryptor.EncryptString(tbPassword.Text);
            }

            return networkInterface;

        }

        private void btnActivate_Click(object sender, EventArgs e)
        {
            try
            {
                OnActivationStarted();

                var networkManager = new NetworkManager();

                if (!string.IsNullOrWhiteSpace(tbIP.Text) && !string.IsNullOrWhiteSpace(tbSubnet.Text))
                {
                    networkManager.SetIp(tbIP.Text, tbSubnet.Text);
                }

                if (!string.IsNullOrWhiteSpace(tbGateway.Text))
                {
                    networkManager.SetGateway(tbGateway.Text);
                }

                if (!string.IsNullOrWhiteSpace(tbProxy.Text))
                {
                    networkManager.EnableProxy(tbProxy.Text, tbPort.Text);
                }
                else
                {
                    networkManager.DisableProxy();
                }

            }
            catch (Exception ex)
            {
                ex.PromptMsg();
            }
            finally
            {
                OnActivationCompleted();
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            OnDeleted();
        }

        protected virtual void OnDeleted()
        {
            Deleted?.Invoke(this, EventArgs.Empty);
        }

        private void tbIP_TextChanged(object sender, EventArgs e)
        {

        }

        protected virtual void OnActivationStarted()
        {
            ActivationStarted?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnActivationCompleted()
        {
            ActivationCompleted?.Invoke(this, EventArgs.Empty);
        }
    }
}
