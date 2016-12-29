using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;

namespace IPSet.Models
{
    public class NetworkManager
    {

        private const string InternetSettingskey = "Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings";

        public const string SetsFileName = "data\\data.set";

        /// <summary>
        /// Set's a new IP Address and it's Submask of the local machine
        /// </summary>
        /// <param name="ipAddress">The IP Address</param>
        /// <param name="subnetMask">The Submask IP Address</param>
        /// <remarks>Requires a reference to the System.Management namespace</remarks>
        public void SetIp(string ipAddress, string subnetMask)
        {
            ManagementClass objMC = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection objMOC = objMC.GetInstances();

            foreach (ManagementObject objMO in objMOC)
            {
                if ((bool)objMO["IPEnabled"])
                {
                    try
                    {
                        ManagementBaseObject setIP;
                        ManagementBaseObject newIP =
                            objMO.GetMethodParameters("EnableStatic");

                        newIP["IPAddress"] = new string[] { ipAddress };
                        newIP["SubnetMask"] = new string[] { subnetMask };

                        setIP = objMO.InvokeMethod("EnableStatic", newIP, null);
                    }
                    catch (Exception)
                    {
                        throw;
                    }


                }
            }
        }
        
        /// <summary>
        /// Set's a new Gateway address of the local machine
        /// </summary>
        /// <param name="gateway">The Gateway IP Address</param>
        /// <remarks>Requires a reference to the System.Management namespace</remarks>
        public void SetGateway(string gateway)
        {
            ManagementClass objMC = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection objMOC = objMC.GetInstances();

            foreach (ManagementObject objMO in objMOC)
            {
                if ((bool)objMO["IPEnabled"])
                {
                    try
                    {
                        ManagementBaseObject setGateway;
                        ManagementBaseObject newGateway =
                            objMO.GetMethodParameters("SetGateways");

                        newGateway["DefaultIPGateway"] = new string[] { gateway };
                        newGateway["GatewayCostMetric"] = new int[] { 1 };

                        setGateway = objMO.InvokeMethod("SetGateways", newGateway, null);
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
            }
        }
        
        /// <summary>
        /// Set's the dns Server of the local machine
        /// </summary>
        /// <param name="nic">nic address</param>
        /// <param name="dns">dns server address</param>
        /// <remarks>Requires a reference to the System.Management namespace</remarks>
        public void SetDns(string nic, string dns)
        {
            ManagementClass objMC = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection objMOC = objMC.GetInstances();

            foreach (ManagementObject objMO in objMOC)
            {
                if ((bool)objMO["IPEnabled"])
                {
                    // if you are using the System.Net.NetworkInformation.NetworkInterface you'll need to change this line to if (objMO["Caption"].ToString().Contains(nic)) and pass in the Description property instead of the name 
                    if (objMO["Caption"].Equals(nic))
                    {
                        try
                        {
                            ManagementBaseObject newDNS =
                                objMO.GetMethodParameters("SetDNSServerSearchOrder");
                            newDNS["DNSServerSearchOrder"] = dns.Split(',');
                            ManagementBaseObject setDNS =
                                objMO.InvokeMethod("SetDNSServerSearchOrder", newDNS, null);
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Set's WINS of the local machine
        /// </summary>
        /// <param name="nic">nic Address</param>
        /// <param name="priWins">Primary WINS server address</param>
        /// <param name="secWins">Secondary WINS server address</param>
        /// <remarks>Requires a reference to the System.Management namespace</remarks>
        public void SetWins(string nic, string priWins, string secWins)
        {
            ManagementClass objMc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection objMoc = objMc.GetInstances();

            foreach (var o in objMoc)
            {
                var objMo = (ManagementObject) o;
                if ((bool)objMo["IPEnabled"])
                {
                    if (objMo["Caption"].Equals(nic))
                    {
                        try
                        {
                            ManagementBaseObject wins =
                            objMo.GetMethodParameters("SetWINSServer");
                            wins.SetPropertyValue("WINSPrimaryServer", priWins);
                            wins.SetPropertyValue("WINSSecondaryServer", secWins);

                            var setWins = objMo.InvokeMethod("SetWINSServer", wins, null);
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Enables proxy settings
        /// </summary>
        /// <param name="proxy">Proxy IP or domain</param>
        /// <param name="port">Proxy port; 80 if empty</param>
        public void EnableProxy(string proxy, string port)
        {
            var registryKey = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings", true);
            if (registryKey != null)
            {

                if (!string.IsNullOrWhiteSpace(proxy))
                {
                    if (string.IsNullOrWhiteSpace(port))
                    {
                        port = "80";
                    }

                    registryKey.SetValue("ProxyServer", string.Concat(proxy, ":", port)); 
                }

                registryKey.SetValue("ProxyEnable", 1);
                registryKey.Close();

            }
        }

        /// <summary>
        /// Disables proxy settings
        /// </summary>
        public void DisableProxy()
        {
            var registryKey = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings", true);
            if (registryKey != null)
            {
                registryKey.SetValue("ProxyEnable", 0);
                registryKey.Close();
            }
        }

        public static List<NetworkInterface> GetNetworkInterfaces()
        {

            DefaultValues.ValidateDataDirectory();

            var filePath = Path.Combine(Application.StartupPath, SetsFileName);

            if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
            {
                return new List<NetworkInterface>();
            }

            return ControlMod.loadBinary(filePath) as List<NetworkInterface>;
        }

        public static void SaveNetworkInterfaces(List<NetworkInterface> settings)
        {
            DefaultValues.ValidateDataDirectory();

            ControlMod.saveBinary(settings, Path.Combine(Application.StartupPath, SetsFileName));
        }

    }
}
