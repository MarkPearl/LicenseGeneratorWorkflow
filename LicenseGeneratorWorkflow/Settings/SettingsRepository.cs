using System;
using IniParser;

namespace LicenseGeneratorWorkflow.Settings
{
    public class SettingsRepository
    {
        public GeneralSettings Load()
        {
            var generalSettings = new GeneralSettings();

            var parser = new FileIniDataParser();
            var parsedData = parser.ReadFile(@"../../../LicenseGeneratorWorkflowDataFiles/LicenseGeneratorWorkflow.ini");

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

            return generalSettings;
        }

    }
}
