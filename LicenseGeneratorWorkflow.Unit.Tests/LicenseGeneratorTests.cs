using System;
using CryptoLicenseGenerator;
using IniParser;
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

			var smtpServer = parsedData["SMTP"].GetKeyData("smtpServer").Value;
			var smtpPort = Convert.ToInt32(parsedData["SMTP"].GetKeyData("smtpPort").Value);
			var smtpUseSsl = Convert.ToBoolean(parsedData["SMTP"].GetKeyData("smtpUseSsl").Value);
			var smtpUsername = parsedData["SMTP"].GetKeyData("smtpUsername").Value;
			var smtpPassword = parsedData["SMTP"].GetKeyData("smtpPassword").Value;

			var cryptoLicenseCode = parsedData["CryptoLicensing"].GetKeyData("LicenseCode").Value;
		    var licenseFileLocation = parsedData["CryptoLicensing"].GetKeyData("LicenseFileLocation").Value;

		    var emailSubject = parsedData["Email"].GetKeyData("Subject").Value;
		    var emailTemaplateFileLocation = parsedData["Email"].GetKeyData("TemaplateFileLocation").Value;
		    var emailProductName = parsedData["Email"].GetKeyData("ProductName").Value;
		    var emailFrom = parsedData["Email"].GetKeyData("From").Value;

		    var cryptoLicenseGeneratorWrapper = new CryptoLicenseGeneratorWrapper(licenseFileLocation, cryptoLicenseCode);
			var emailSender = new EmailSender(smtpServer, smtpPort, smtpUseSsl, smtpUsername, smtpPassword);
		    var licenseEmail = new LicenseEmail(emailFrom, emailProductName, emailSubject, emailTemaplateFileLocation);
		    var licenseWorkflow = new LicenseWorkflow(cryptoLicenseGeneratorWrapper, emailSender, licenseEmail);
		    
			var payPalInfo = new PayPalInfo(
				"web_accept", 
				"SKU/item code of your product", 1, 101, 1, "mark@maximasoftware.co.za",
			    "VERIFIED", 
				"dummyclient", "markpearl@gmail.com", "dummy company");

			licenseWorkflow.Run(payPalInfo, "Test");
		    Assert.True(true);
	    }
    }
}
