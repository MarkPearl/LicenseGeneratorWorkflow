using System.Net;
using System.Net.Mail;
using LogicNP.CryptoLicensing;

namespace CryptoLicenseGenerator
{
	public class EmailSender
	{
		readonly string _emailSmtpServer = string.Empty;
		private readonly int _emailSmtpPort;
		private readonly bool _emailUseSsl;
		private readonly string _emailSmtpUsername;
		private readonly string _emailSmtpPassword;

		public EmailSender(
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

		public void SendEmail(MailMessage message)
		{
			var client = CreateSmtpClient();
			client.Send(message);
		}

		private SmtpClient CreateSmtpClient()
		{
			var client = new SmtpClient();
			client.Host = _emailSmtpServer;
			client.Port = _emailSmtpPort;
			client.EnableSsl = _emailUseSsl;
			client.Credentials = new NetworkCredential(_emailSmtpUsername, _emailSmtpPassword);
			client.DeliveryMethod = SmtpDeliveryMethod.Network;
			return client;
		}
	}
}