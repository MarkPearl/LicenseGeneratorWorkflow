using System.IO;
using System.Net;
using System.Net.Mail;
using LogicNP.CryptoLicensing;

namespace CryptoLicenseGenerator
{
	public class LicenseEmailGenerator
	{

		private string email_template = "Email Template Path";
		string[] placeholders = new string[] { "%%product_name%%", "%%license%%" }; // placeholders used in email template
		string[] replacements; // initialized in InitPlaceHolders method below
		string email_subject = "License information"; // Email subject
		string email_smtp_server = string.Empty; // Server used for sending email
		int email_smtp_port = 25; // Port used for sending email
		bool email_use_ssl = false; // If true, SSL is used for sending email
		string email_smtp_username = string.Empty; // Username for mail server (leave blank if not required)
		string email_smtp_password = string.Empty; // password for mail server (leave blank if not required)
		string product_name = "My Product"; // The name of your product

		public void SendEmail(string emailToAddress, string licenseCode, string emailAddressFrom)
		{
			// Construct message
			var message = new MailMessage(emailAddressFrom, emailToAddress);
			message.Subject = email_subject;
			message.IsBodyHtml = true;
			message.Body = GetEmailBody(licenseCode);

			var client = new SmtpClient(email_smtp_server, email_smtp_port);
			client.EnableSsl = email_use_ssl;

			if (string.IsNullOrEmpty(email_smtp_username))
			{
				client.Credentials = new NetworkCredential();
			}
			else
			{
				client.Credentials = new NetworkCredential(email_smtp_username, email_smtp_password);
			}

			client.DeliveryMethod = SmtpDeliveryMethod.Network;
			client.Send(message);
		}


		string GetEmailBody(string productKey)
		{
			InitPlaceHolders(productKey);
			string body = ReadFile(email_template);
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
			filePath = LicenseService.GetRealFilePath(filePath);
			var reader = new StreamReader(filePath);
			var ret = reader.ReadToEnd();
			reader.Close();
			return ret;
		}

	}
}