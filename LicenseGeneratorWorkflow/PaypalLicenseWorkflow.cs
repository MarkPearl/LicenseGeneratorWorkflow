using System;
using System.Net;
using System.Text;
using System.Web.Services.Protocols;
using CryptoLicenseGenerator;
using LicenseGeneratorWorkflow.Settings;

namespace LicenseGeneratorWorkflow
{
    public class PaypalIpnValidation
    {
        private readonly PayPalSettings _payPalSettings;

        public PaypalIpnValidation(PayPalSettings payPalSettings)
        {
            _payPalSettings = payPalSettings;
        }

        public void EnsureValid(PayPalInfo payPalInfo)
        {
            //string ipnReceiverEmail = payPalInfo.IpnReceiverEmail;

            //// Does not match our paypal login id
            //if (ipnReceiverEmail != _payPalSettings.IpnReceiverEmail)
            //{
            //    //TODO: throw exception
            //}

            string cmd = "cmd = _notify-validate";

            //foreach (string paramName in base.Request.Form)
            //{
            //    string paramValue = this.Encode(base.Request.Form[paramName]);
            //    cmd = cmd + string.Format("&{0}={1}", paramName, paramValue);
            //}
            var client = new WebClient();
            client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
            var bytes = Encoding.UTF8.GetBytes(cmd);

            // Send HTTP POST to verify IPN
            var responseBuffer = client.UploadData(_payPalSettings.IpnValidationUrl, "POST", bytes);
            var responseMessage = Encoding.UTF8.GetString(responseBuffer);

            if (responseMessage != "VERIFIED") 
                throw new Exception("IPN verification failed.");
        }
    }

    public class PaypalLicenseWorkflow : LicenseWorkflow
    {
		private readonly LicenseGenerator _licenseGenerator;
		private readonly EmailSender _emailSender;
		private readonly LicenseEmail _licenseEmail;
        private readonly PayPalSettings _payPalSettings;

        public PaypalLicenseWorkflow(
			LicenseGenerator licenseGenerator,
			EmailSender emailSender,
			LicenseEmail licenseEmail,
            PayPalSettings payPalSettings)
		{
			_licenseGenerator = licenseGenerator;
			_emailSender = emailSender;
			_licenseEmail = licenseEmail;
		    _payPalSettings = payPalSettings;
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
