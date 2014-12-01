using System.Net;
using System.Net.Mail;
using LicenseGeneratorWorkflow.Settings;

namespace LicenseGeneratorWorkflow
{
	public class EmailSender
	{
		readonly string _emailSmtpServer = string.Empty;
		private readonly int _emailSmtpPort;
		private readonly bool _emailUseSsl;
		private readonly string _emailSmtpUsername;
		private readonly string _emailSmtpPassword;

		public EmailSender(SmtpSettings smtpSettings)
		{
            _emailSmtpServer = smtpSettings.Server;
            _emailSmtpPort = smtpSettings.Port;
            _emailUseSsl = smtpSettings.UseSsl;
            _emailSmtpUsername = smtpSettings.Username;
            _emailSmtpPassword = smtpSettings.Password;
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