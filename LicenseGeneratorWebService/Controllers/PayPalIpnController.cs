using System.Collections.Specialized;
using System.Web;
using System.Web.Http;
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


            var settingsRepository = new SettingsRepository(System.Web.Configuration.WebConfigurationManager.AppSettings["SettingsFileLocation"]);
            var settings = settingsRepository.Load();

            var cryptoLicenseGeneratorWrapper = new CryptoLicenseGeneratorWrapper(settings.CryptoLicenseSettings);
            var emailSender = new EmailSender(settings.SmtpSettings);
            var licenseEmail = new UserLicenseEmail(settings.EmailSettings);
            var payPalValidation = new PaypalIpnValidation(settings.PayPalSettings);
            var licenseWorkflow = new PaypalLicenseWorkflow(
                cryptoLicenseGeneratorWrapper,
                emailSender,
                licenseEmail,
                payPalValidation,
                settings.ProductProfileSettings);

            licenseWorkflow.Run(paypalInfo);

            return Ok("Success");
        }

        private PayPalInfo GetFromRequest(NameValueCollection requestInfo)
        {
            return new PayPalInfo(requestInfo);
        }
    }
}
