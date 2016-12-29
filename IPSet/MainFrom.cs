using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IPSet.Models;
using IPSet.UserControls;

namespace IPSet
{
    public partial class MainFrom : Form
    {
        public MainFrom()
        {
            InitializeComponent();
        }

        private void MainFrom_Load(object sender, EventArgs e)
        {

            var sets = NetworkManager.GetNetworkInterfaces();

            if (sets != null)
            {

                var pts = ControlMod.CartesianCtrl(pnlHolder.Width, new Size(560, 140), sets.Count);

                var i = 0;

                foreach (var networkInterface in sets)
                {
                    var control = new NetworkInterfaceUc()
                    {
                        Location = pts[i],
                        BackColor = Color.White,
                        Name = "networkInterfaceUC" + i,
                        Size = new Size(546, 126),
                        TabIndex = i
                    };

                    control.Fill(networkInterface);
                    control.Deleted += NetworkInterfaceUC_Deleted;
                    control.ActivationStarted += Control_ActivationStarted;
                    control.ActivationCompleted += Control_ActivationCompleted;

                    pnlHolder.Controls.Add(control);

                    i++;

                }

                ValidatePositions();

            }

        }

        private void Control_ActivationCompleted(object sender, EventArgs e)
        {
            pnlHolder.Enabled = true;
        }

        private void Control_ActivationStarted(object sender, EventArgs e)
        {
            pnlHolder.Enabled = false;
        }

        private void NetworkInterfaceUC_Deleted(object sender, EventArgs e)
        {
            var control = (NetworkInterfaceUc)sender;
            this.Controls.Remove(control);
            control.Dispose();
            ValidatePositions();
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            using (var form = new SettingsForm())
            {
                form.ShowDialog();
            }
        }

        private void btnAddInterface_Click(object sender, EventArgs e)
        {

            var count = pnlHolder.Controls.Count + 1;

            var pt = ControlMod.CartesianCtrl(pnlHolder.Width, new Size(560, 140), count).Last();

            var control = new NetworkInterfaceUc()
            {
                Location = pt,
                BackColor = Color.White,
                Name = "networkInterfaceUC" + count,
                Size = new Size(546, 126),
                TabIndex = count
            };

            control.Deleted += NetworkInterfaceUC_Deleted;
            control.ActivationStarted += Control_ActivationStarted;
            control.ActivationCompleted += Control_ActivationCompleted;

            pnlHolder.Controls.Add(control);

            ValidatePositions();

        }

        private void MainFrom_SizeChanged(object sender, EventArgs e)
        {
            ValidatePositions();
        }

        private void ValidatePositions()
        {
            if (pnlHolder.Controls.Count > 0)
            {

                var pt = ControlMod.CartesianCtrl(pnlHolder.Width, new Size(560, 140), pnlHolder.Controls.Count);

                var i = 0;

                foreach (Control ctl in pnlHolder.Controls)
                {
                    ctl.Location = pt[i++];
                }

            }
        }

        private void MainFrom_FormClosing(object sender, FormClosingEventArgs e)
        {

            var sets = pnlHolder.Controls
                .OfType<NetworkInterfaceUc>()
                .Select(uc => uc.GetNetworkInterface()).ToList();

            NetworkManager.SaveNetworkInterfaces(sets);

        }
    }
}
