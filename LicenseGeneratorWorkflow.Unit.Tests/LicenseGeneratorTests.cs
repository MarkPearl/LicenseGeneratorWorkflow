﻿using System;
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
            emailSettings.EndUserEmailTemaplateFileLocation = parsedData["Email"].GetKeyData("EndUserEmailTemaplateFileLocation").Value;
            emailSettings.ProductName = parsedData["Email"].GetKeyData("ProductName").Value;
            emailSettings.From = parsedData["Email"].GetKeyData("From").Value;

            var cryptoLicenseGeneratorWrapper = new CryptoLicenseGeneratorWrapper(cryptoLicenseSettings);
			var emailSender = new EmailSender(smtpSettings);
		    var userLicenseEmail = new UserLicenseEmail(emailSettings, new TemplateToMessageConverter(), new EmailTemplatePlaceholders());
		    var adminLicenseEmail = new AdminLicenseEmail(emailSettings, new TemplateToMessageConverter(), new EmailTemplatePlaceholders());
	        var paypalSettings = new PayPalSettings();
	        var paypalValidation = new PaypalIpnValidation(paypalSettings);
	        var productProfileSettings = new ProductProfileSettings();

		    var licenseWorkflow = new PaypalLicenseWorkflow(
                cryptoLicenseGeneratorWrapper, 
                emailSender, 
                userLicenseEmail,
                paypalValidation,
                productProfileSettings);

	        var payPalInfo = new PayPalInfo();

            licenseWorkflow.Run(payPalInfo);
		    Assert.True(true);
	    }
    }
}
