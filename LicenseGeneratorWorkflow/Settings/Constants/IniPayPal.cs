namespace LicenseGeneratorWorkflow.Settings.Constants
{
	internal static class IniPayPal
	{
		public const string IpnReceiverEmail = "ipnReceiverEmail";
		public const string IpnValidationUrl = "IpnValidationUrl";
	}

	internal static class IniSmtp
	{
		public const string Smtpusessl = "smtpUseSsl";
		public const string Smtpport = "smtpPort";
		public const string Smtpusername = "smtpUsername";
		public const string Smtppassword = "smtpPassword";
		public const string Smtpserver = "smtpServer";
	}

	internal static class IniCrypto
	{
		public const string LicenseCode = "LicenseCode";
		public const string Licensefilelocation = "LicenseFileLocation";
	}

	internal static class IniEmail
	{
		public const string Subject = "Subject";
		public const string Enduseremailtemaplatefilelocation = "EndUserEmailTemaplateFileLocation";
		public const string Adminemailtemaplatefilelocation = "AdminEmailTemaplateFileLocation";
		public const string Productname = "ProductName";
		public const string FromField = "From";
		public const string Bcc = "Bcc";
	}
}