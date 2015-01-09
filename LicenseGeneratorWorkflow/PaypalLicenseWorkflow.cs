using System.Web.Services.Protocols;
using LicenseGeneratorWorkflow.Settings;

namespace LicenseGeneratorWorkflow
{
    public class PaypalLicenseWorkflow : LicenseWorkflow
    {
		private readonly LicenseGenerator _licenseGenerator;
		private readonly EmailSender _emailSender;
		private readonly UserLicenseEmail _userLicenseEmail;
        private readonly PaypalIpnValidation _paypalIpnValidation;
        private readonly ProductProfileSettings _productProfileSettings;

        public PaypalLicenseWorkflow(
			LicenseGenerator licenseGenerator,
			EmailSender emailSender,
			UserLicenseEmail userLicenseEmail,
            PaypalIpnValidation paypalIpnValidation,
            ProductProfileSettings productProfileSettings)
		{
			_licenseGenerator = licenseGenerator;
			_emailSender = emailSender;
			_userLicenseEmail = userLicenseEmail;
            _paypalIpnValidation = paypalIpnValidation;
            _productProfileSettings = productProfileSettings;
		}

		public void Run(PayPalInfo payPalInfo)
		{
            _paypalIpnValidation.EnsureValid(payPalInfo);
		    var productProfile = _productProfileSettings.GetValueOrDefault(payPalInfo.IpnItemNumber);
            var licenseCode = GenerateLicenseCode(payPalInfo, productProfile);
            var message = _userLicenseEmail.ConstructEmail(payPalInfo, licenseCode);
            _emailSender.SendEmail(message);
		}

		private string GenerateLicenseCode(PayPalInfo payPalInfo, string licenseTypeProfile)
		{
			_licenseGenerator.Initialize();
			_licenseGenerator.SetActiveProfile(licenseTypeProfile);
			_licenseGenerator.NumberOfUsers = payPalInfo.IpnQuantity;
			_licenseGenerator.UserData = payPalInfo.UserData;
			var licenseCode = _licenseGenerator.Generate();
			return licenseCode;
		}
	}
}
