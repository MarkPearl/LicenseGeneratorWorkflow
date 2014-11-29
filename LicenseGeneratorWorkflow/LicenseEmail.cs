using System.IO;
using System.Net.Mail;

namespace CryptoLicenseGenerator
{
	public class LicenseEmail
	{
		private readonly string _emailAddressFrom;
		private readonly string _productName;
		private readonly string _subject;
		private readonly string _emailTemplateFile;
		private readonly string[] _placeholders = { "%%product_name%%", "%%license%%" }; 
		private string[] _replacements;

		public LicenseEmail(
			string emailAddressFrom, 
			string productName, 
			string subject, 
			string emailTemplateFile)
		{
			_emailAddressFrom = emailAddressFrom;
			_productName = productName;
			_subject = subject;
			_emailTemplateFile = emailTemplateFile;
		}

		public MailMessage ConstructEmail(string emailToAddress, string licenseCode)
		{
			var message = new MailMessage();
			message.From = new MailAddress(_emailAddressFrom);
			message.Subject = _subject;
			message.To.Add(emailToAddress);
			message.IsBodyHtml = true;
			message.Body = EmailBody(licenseCode, _productName);
			message.IsBodyHtml = false;

			return message;
		}

		string EmailBody(string licenseCode, string productName)
		{
			InitPlaceHolders(licenseCode, productName);
			var bodyTemplate = ReadFile(_emailTemplateFile);
			return ReplacePlaceholders(bodyTemplate);
		}

		void InitPlaceHolders(string licenseCode, string productName)
		{
			_replacements = new string[_placeholders.Length];
			_replacements[0] = productName;
			_replacements[1] = licenseCode;
		}

		string ReplacePlaceholders(string text)
		{
			for (int i = 0; i < _placeholders.Length; i++)
			{
				text = text.Replace(_placeholders[i], _replacements[i]);
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