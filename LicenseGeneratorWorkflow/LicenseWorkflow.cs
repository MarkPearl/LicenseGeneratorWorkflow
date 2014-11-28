using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoLicenseGenerator
{
	public class LicenseWorkflow
	{
		private const string emailAddressFrom = "Support@MaximaSoftware.co.za";
		private const string LicenseFileLocation = @"..\..\..\LicenseGeneratorWorkflowDataFiles\ExampleLicenseCode.netlicproj";
		private readonly LicenseGenerator _licenseGenerator;
		private readonly LicenseEmailGenerator _licenseEmailGenerator;

		public LicenseWorkflow(
			LicenseGenerator licenseGenerator,
			LicenseEmailGenerator licenseEmailGenerator)
		{
			_licenseGenerator = licenseGenerator;
			_licenseEmailGenerator = licenseEmailGenerator;
		}

		public void GenerateLicense(
			PayPalInfo payPalInfo, 
			string licenseTypeProfile)
		{
			_licenseGenerator.LoadLicenseFile(LicenseFileLocation);
			_licenseGenerator.SetActiveProfile(licenseTypeProfile);

			_licenseGenerator.NumberOfUsers = payPalInfo.IpnQuantity;
			_licenseGenerator.UserData = payPalInfo.UserData;
			var licenseCode = _licenseGenerator.Generate();

			_licenseEmailGenerator.SendEmail(payPalInfo.PayerEmail, licenseCode, emailAddressFrom);
		}
	}
}
