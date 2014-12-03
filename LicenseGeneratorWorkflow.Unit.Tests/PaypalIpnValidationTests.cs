using LicenseGeneratorWorkflow.Settings;
using Xunit;

namespace LicenseGeneratorWorkflow.Unit.Tests
{
    public class PaypalIpnValidationTests
    {
        [Fact]
        public void ShouldValidateIpn()
        {
            var payPalSettings = new PayPalSettings();
            payPalSettings.IpnValidationUrl = "https://www.sandbox.paypal.com/cgi-bin/webscr";

            var paypalIpnValidation = new PaypalIpnValidation(payPalSettings);
            var paypalInfo = new PayPalInfo("web_accept", "", 1, 101, 1, "mark@maximasoftware.co.za", "", "", "");
            paypalIpnValidation.EnsureValid(paypalInfo);
        }

    }
}