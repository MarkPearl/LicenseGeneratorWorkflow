using System.IO;
using System.Net;
using System.Net.Mail;
using LogicNP.CryptoLicensing;

namespace CryptoLicenseGenerator
{
	public class LicenseEmailGenerator
	{
		string _emailSmtpServer = string.Empty; // Server used for sending email
		int _emailSmtpPort = 25; // Port used for sending email
		bool _emailUseSsl = false; // If true, SSL is used for sending email
		string _emailSmtpUsername = string.Empty; // Username for mail server (leave blank if not required)
		string _emailSmtpPassword = string.Empty; // password for mail server (leave blank if not required)
		private const string EmailTemplateFile = @"..\..\..\LicenseGeneratorWorkflowDataFiles\EmailTemplate.txt";

		string[] placeholders = new string[] { "%%product_name%%", "%%license%%" }; // placeholders used in email template
		string[] replacements; // initialized in InitPlaceHolders method below
		string email_subject = "License information"; // Email subject
		string product_name = "My Product"; // The name of your product

		public LicenseEmailGenerator(
			string emailSmtpServer, 
			int emailSmtpPort, 
			bool emailUseSsl, 
			string emailSmtpUsername,
			string emailSmtpPassword)
		{
			_emailSmtpServer = emailSmtpServer;
			_emailSmtpPort = emailSmtpPort;
			_emailUseSsl = emailUseSsl;
			_emailSmtpUsername = emailSmtpUsername;
			_emailSmtpPassword = emailSmtpPassword;
		}

		public void SendEmail(string emailToAddress, string licenseCode, string emailAddressFrom)
		{
			// Construct message
			var message = new MailMessage(emailAddressFrom, emailToAddress);
			message.Subject = email_subject;
			message.IsBodyHtml = true;
			message.Body = GetEmailBody(licenseCode);

			var client = new SmtpClient(_emailSmtpServer, _emailSmtpPort);
			client.EnableSsl = _emailUseSsl;

			if (string.IsNullOrEmpty(_emailSmtpUsername))
			{
				client.Credentials = new NetworkCredential();
			}
			else
			{
				client.Credentials = new NetworkCredential(_emailSmtpUsername, _emailSmtpPassword);
			}

			client.DeliveryMethod = SmtpDeliveryMethod.Network;
			client.Send(message);
		}


		string GetEmailBody(string productKey)
		{
			InitPlaceHolders(productKey);
			string body = ReadFile(EmailTemplateFile);
			return ReplacePlaceholders(body);
		}


		void InitPlaceHolders(string LicenseCode)
		{
			replacements = new string[placeholders.Length];

			replacements[0] = product_name;
			replacements[1] = LicenseCode;
		}


		string ReplacePlaceholders(string text)
		{
			for (int i = 0; i < placeholders.Length; i++)
			{
				text = text.Replace(placeholders[i], replacements[i]);
			}
			return text;
		}


		string ReadFile(string filePath)
		{
			var reader = new StreamReader(filePath);
			var ret = reader.ReadToEnd();
			reader.Close();
			return ret;
		}

	}
}