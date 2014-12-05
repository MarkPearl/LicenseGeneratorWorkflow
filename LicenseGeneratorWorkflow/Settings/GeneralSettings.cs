namespace LicenseGeneratorWorkflow.Settings
{
    public class GeneralSettings
    {
        public GeneralSettings()
        {
            SmtpSettings = new SmtpSettings();
            CryptoLicenseSettings = new CryptoLicenseSettings();
            EmailSettings = new EmailSettings();
            PayPalSettings = new PayPalSettings();
            ProductProfileSettings = new ProductProfileSettings();
        }

        public ProductProfileSettings ProductProfileSettings { get; private set; }
        public PayPalSettings PayPalSettings { get; private set; }
        public EmailSettings EmailSettings { get; private set; }
        public CryptoLicenseSettings CryptoLicenseSettings { get; private set; }
        public SmtpSettings SmtpSettings { get; private set; }
    }
}