using System;
using System.Linq;
using IniParser;

namespace LicenseGeneratorWorkflow.Settings
{
    public class SettingsRepository
    {
        private readonly string _settingsFileLocation;

        public SettingsRepository(string settingsFileLocation)
        {
            _settingsFileLocation = settingsFileLocation;
        }

        public GeneralSettings Load()
        {
            var generalSettings = new GeneralSettings();

            var parser = new FileIniDataParser();
            var parsedData = parser.ReadFile(_settingsFileLocation);

            generalSettings.PayPalSettings.IpnReceiverEmail = parsedData["PayPal"].GetKeyData("ipnReceiverEmail").Value;
            generalSettings.PayPalSettings.IpnValidationUrl = parsedData["PayPal"].GetKeyData("ipnValidationUrl").Value;

            generalSettings.SmtpSettings.Server = parsedData["SMTP"].GetKeyData("smtpServer").Value;
            generalSettings.SmtpSettings.Port = Convert.ToInt32(parsedData["SMTP"].GetKeyData("smtpPort").Value);
            generalSettings.SmtpSettings.UseSsl = Convert.ToBoolean(parsedData["SMTP"].GetKeyData("smtpUseSsl").Value);
            generalSettings.SmtpSettings.Username = parsedData["SMTP"].GetKeyData("smtpUsername").Value;
            generalSettings.SmtpSettings.Password = parsedData["SMTP"].GetKeyData("smtpPassword").Value;

            generalSettings.CryptoLicenseSettings.LicenseCode = parsedData["CryptoLicensing"].GetKeyData("LicenseCode").Value;
            generalSettings.CryptoLicenseSettings.LicenseFileLocation = parsedData["CryptoLicensing"].GetKeyData("LicenseFileLocation").Value;

            generalSettings.EmailSettings.Subject = parsedData["Email"].GetKeyData("Subject").Value;
            generalSettings.EmailSettings.TemaplateFileLocation = parsedData["Email"].GetKeyData("TemaplateFileLocation").Value;
            generalSettings.EmailSettings.ProductName = parsedData["Email"].GetKeyData("ProductName").Value;
            generalSettings.EmailSettings.From = parsedData["Email"].GetKeyData("From").Value;
            generalSettings.EmailSettings.Bcc = parsedData["Email"].GetKeyData("Bcc").Value;


            var productProfiles = parsedData["ProductLicenseProfiles"];
            for (var i = 0; i < productProfiles.Count; i++)
            {
                var item = productProfiles.ElementAt(i);
                generalSettings.ProductProfileSettings.Add(item.KeyName, item.Value);
            }

            return generalSettings;
        }

    }
}
