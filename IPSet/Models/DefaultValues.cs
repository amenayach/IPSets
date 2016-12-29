using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IPSet.Models
{
    [Serializable]
    public class DefaultValues
    {

        const string SettingsFileName = "data\\settings.set";

        public string Subnet { get; set; }

        public string  Gateway { get; set; }

        public string Proxy { get; set; }

        public string Port { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public static void ValidateDataDirectory()
        {
            var dataPath = Path.Combine(Application.StartupPath, "data");

            if (!Directory.Exists(dataPath))
            {
                Directory.CreateDirectory(dataPath);
            }
        }

        public static DefaultValues GetSettings()
        {
            ValidateDataDirectory();

            var filePath = Path.Combine(Application.StartupPath, SettingsFileName);

            if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
            {
                return new DefaultValues();
            }

            return ControlMod.loadBinary(filePath) as DefaultValues;
        }

        public static void SaveSettings(DefaultValues settings)
        {
            ValidateDataDirectory();

            ControlMod.saveBinary(settings, Path.Combine(Application.StartupPath, SettingsFileName));
        }

    }
}
