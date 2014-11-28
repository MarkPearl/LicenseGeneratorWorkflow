using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IniParser;
using IniParser.Model;
using Xunit;

namespace CryptoLicenseGenerator.Unit.Tests
{

    public class LicenseGeneratorTests
    {
	    [Fact]
	    public void ShouldDoSomething()
	    {
			var parser = new FileIniDataParser();
			var parsedData = parser.LoadFile(@"../../../LicenseGeneratorWorkflowDataFiles/LicenseGeneratorWorkflow.ini");
			var provider = parsedData["GeneralConfiguration"].GetKeyData("smtpProvider");

		    var cryptoLicenseGeneratorWrapper = new CryptoLicenseGeneratorWrapper();
		    var licenseEmailGenerator = new LicenseEmailGenerator("", 25, false, "", "");
		    var licenseGeneratorWorkflow = new LicenseWorkflow(cryptoLicenseGeneratorWrapper, licenseEmailGenerator);
		    
			var payPalInfo = new PayPalInfo(
				"web_accept", 
				"SKU/item code of your product", 1, 101, 1, "mark@maximasoftware.co.za",
			    "VERIFIED", 
				"dummyclient", "markpearl@gmail.com", "dummy company");

			licenseGeneratorWorkflow.GenerateLicense(payPalInfo, "Test");
		    Assert.True(true);
	    }
    }
}
