namespace LicenseGeneratorWorkflow
{
    public interface LicenseWorkflow
    {
        void Run(PayPalInfo payPalInfo, string licenseTypeProfile);
    }
}