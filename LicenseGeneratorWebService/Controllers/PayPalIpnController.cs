using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web;
using System.Web.Http;
using CryptoLicenseGenerator;
using LicenseGeneratorWorkflow;
using LicenseGeneratorWorkflow.Settings;

namespace LicenseGeneratorWebService.Controllers
{
    public class PayPalIpnController : ApiController
    {

        [HttpPost]
        public IHttpActionResult PaypalIpnNotification()
        {
            //TODO: This is a hack - http://stackoverflow.com/questions/11593595/is-there-a-way-to-handle-form-post-data-in-a-web-api-controller
            var httpContext = (HttpContextWrapper)Request.Properties["MS_HttpContext"];
            var requestInfo = httpContext.Request.Form;
            var paypalInfo = GetFromRequest(requestInfo);


            var settingsRepository = new SettingsRepository();
            var settings = settingsRepository.Load();

            var cryptoLicenseGeneratorWrapper = new CryptoLicenseGeneratorWrapper(settings.CryptoLicenseSettings);
            var emailSender = new EmailSender(settings.SmtpSettings);
            var licenseEmail = new LicenseEmail(settings.EmailSettings);
            var licenseWorkflow = new PaypalLicenseWorkflow(cryptoLicenseGeneratorWrapper, emailSender, licenseEmail, settings.PayPalSettings);



            //licenseWorkflow.Run();

            return Ok("Hello");
        }

        private PayPalInfo GetFromRequest(NameValueCollection requestInfo)
        {
            var ipnTxnType = requestInfo["txn_type"];
            var itemNumber = requestInfo["item_number"];
            var quantity = Convert.ToInt16(requestInfo["quantity"]);
            var mcgross = Convert.ToDecimal(requestInfo["mc_gross"]);
            var tax = Convert.ToDecimal(requestInfo["tax"]);
            var receiveremail = requestInfo["receiver_email"];
            var receiverid = requestInfo["receiver_id"];
            var residencecountry = requestInfo["residence_country"];
            var testipn = requestInfo["test_ipn"];
            var transactionsubject = requestInfo["transaction_subject"];
            var payerUserName = requestInfo["payer_id"];
            var payerEmail = requestInfo["payer_email"];
            var payerBusinessName = requestInfo["payer_business_name"];

            return new PayPalInfo(ipnTxnType, itemNumber, quantity, mcgross, tax, receiveremail, payerUserName, payerEmail, payerBusinessName);

        }
    }
}
