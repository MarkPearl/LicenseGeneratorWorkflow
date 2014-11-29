using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LicenseGeneratorWorkflow;

namespace CryptoLicenseGenerator
{
	public class LicenseWorkflow
	{
		private readonly LicenseGenerator _licenseGenerator;
		private readonly EmailSender _emailSender;
		private readonly LicenseEmail _licenseEmail;

		public LicenseWorkflow(
			LicenseGenerator licenseGenerator,
			EmailSender emailSender,
			LicenseEmail licenseEmail)
		{
			_licenseGenerator = licenseGenerator;
			_emailSender = emailSender;
			_licenseEmail = licenseEmail;
		}

		public void Run(PayPalInfo payPalInfo, string licenseTypeProfile)
		{
			var licenseCode = GenerateLicenseCode(payPalInfo, licenseTypeProfile);
			var message = _licenseEmail.ConstructEmail(payPalInfo.PayerEmail, licenseCode);
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
