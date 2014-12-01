namespace LicenseGeneratorWorkflow.Settings
{
    public class GeneralSettings
    {
        public GeneralSettings()
        {
            SmtpSettings = new SmtpSettings();
            CryptoLicenseSettings = new CryptoLicenseSettings();
            EmailSettings = new EmailSettings();
        }

        public EmailSettings EmailSettings { get; private set; }
        public CryptoLicenseSettings CryptoLicenseSettings { get; private set; }
        public SmtpSettings SmtpSettings { get; private set; }
    }
}