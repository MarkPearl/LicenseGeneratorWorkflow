using System;
using CryptoLicenseGenerator;
using IniParser;
using LicenseGeneratorWorkflow.Settings;
using Xunit;

namespace LicenseGeneratorWorkflow.Unit.Tests
{

    public class LicenseGeneratorTests
    {
	    [Fact]
	    public void ShouldDoSomething()
	    {
			var parser = new FileIniDataParser();
			var parsedData = parser.ReadFile(@"../../../LicenseGeneratorWorkflowDataFiles/LicenseGeneratorWorkflow.ini");

	        var smtpSettings = new SmtpSettings();
            smtpSettings.Server = parsedData["SMTP"].GetKeyData("smtpServer").Value;
            smtpSettings.Port = Convert.ToInt32(parsedData["SMTP"].GetKeyData("smtpPort").Value);
            smtpSettings.UseSsl = Convert.ToBoolean(parsedData["SMTP"].GetKeyData("smtpUseSsl").Value);
            smtpSettings.Username = parsedData["SMTP"].GetKeyData("smtpUsername").Value;
            smtpSettings.Password = parsedData["SMTP"].GetKeyData("smtpPassword").Value;

	        var cryptoLicenseSettings = new CryptoLicenseSettings();
            cryptoLicenseSettings.LicenseCode = parsedData["CryptoLicensing"].GetKeyData("LicenseCode").Value;
		    cryptoLicenseSettings.LicenseFileLocation = parsedData["CryptoLicensing"].GetKeyData("LicenseFileLocation").Value;

	        var emailSettings = new EmailSettings();
            emailSettings.Subject = parsedData["Email"].GetKeyData("Subject").Value;
            emailSettings.TemaplateFileLocation = parsedData["Email"].GetKeyData("TemaplateFileLocation").Value;
            emailSettings.ProductName = parsedData["Email"].GetKeyData("ProductName").Value;
            emailSettings.From = parsedData["Email"].GetKeyData("From").Value;

            var cryptoLicenseGeneratorWrapper = new CryptoLicenseGeneratorWrapper(cryptoLicenseSettings);
			var emailSender = new EmailSender(smtpSettings);
		    var licenseEmail = new LicenseEmail(emailSettings);
	        var paypalSettings = new PayPalSettings();

		    var licenseWorkflow = new PaypalLicenseWorkflow(
                cryptoLicenseGeneratorWrapper, 
                emailSender, 
                licenseEmail, 
                paypalSettings);
		    
			var payPalInfo = new PayPalInfo(
				"web_accept", 
				"SKU/item code of your product", 1, 101, 1, "mark@maximasoftware.co.za", 
				"dummyclient", "markpearl@gmail.com", "dummy company");

			licenseWorkflow.Run(payPalInfo, "Test");
		    Assert.True(true);
	    }
    }
}
