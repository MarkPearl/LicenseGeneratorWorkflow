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
            //var paypalInfo = new PayPalInfo(
            //    "payments@maxcut.co.za",
            //    "MCP",
            //    1,
            //    "1",
            //    "",
            //    "60084167VS558930E",
            //    "web_accept",
            //    "MarkTester-facilitator-1@gmail.com",
            //    "test account's Test Store";
            //var paypalInfo = new PayPalInfo("","",1,1,1,"","","","");


            //paypalIpnValidation.EnsureValid(paypalInfo);
        }

    }
}