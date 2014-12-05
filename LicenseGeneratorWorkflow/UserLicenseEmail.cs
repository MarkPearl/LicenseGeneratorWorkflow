using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using LicenseGeneratorWorkflow.Settings;

namespace LicenseGeneratorWorkflow
{
    public class UserLicenseEmail
	{
        private readonly TemplateToMessageConverter _templateToMessageConverter;
        private readonly string _emailAddressBcc;
		private readonly string _emailAddressFrom;
		private readonly string _productName;
		private readonly string _subject;
		private readonly string _emailTemplateFile;
        private readonly Dictionary<string, string> _placeholders; 

		public UserLicenseEmail(
            EmailSettings emailSettings, 
            TemplateToMessageConverter templateToMessageConverter)
		{
		    _templateToMessageConverter = templateToMessageConverter;
		    _emailAddressBcc = emailSettings.Bcc;
            _emailAddressFrom = emailSettings.From;
		    _productName = emailSettings.ProductName;
            _subject = emailSettings.Subject;
            _emailTemplateFile = emailSettings.TemaplateFileLocation;
            _placeholders = new Dictionary<string, string>();
		}

		public MailMessage ConstructEmail(string emailToAddress, string licenseCode)
		{
			var message = new MailMessage();
			message.From = new MailAddress(_emailAddressFrom);
			message.Subject = _subject;
			message.To.Add(emailToAddress);
			message.Bcc.Add(_emailAddressBcc);
			message.IsBodyHtml = true;
			message.Body = EmailBody(licenseCode, _productName);
			message.IsBodyHtml = false;

			return message;
		}

		string EmailBody(string licenseCode, string productName)
		{
			InitPlaceHolders(licenseCode, productName);
		    var bodyTemplate = File.ReadAllText(_emailTemplateFile);
		    return _templateToMessageConverter.Convert(bodyTemplate, _placeholders);

		}

		void InitPlaceHolders(string licenseCode, string productName)
		{
            _placeholders.Clear();
            _placeholders.Add("product_name", productName);
            _placeholders.Add("license", licenseCode);
		}

	}
}