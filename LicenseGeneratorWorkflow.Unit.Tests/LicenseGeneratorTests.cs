using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CryptoLicenseGenerator.Unit.Tests
{

    public class LicenseGeneratorTests
    {
	    [Fact]
	    public void ShouldDoSomething()
	    {
		    var cryptoLicenseGeneratorWrapper = new CryptoLicenseGeneratorWrapper();
		    var licenseEmailGenerator = new LicenseEmailGenerator();
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
