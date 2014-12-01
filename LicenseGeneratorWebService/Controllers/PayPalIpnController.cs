using System.Web.Http;

namespace LicenseGeneratorWebService.Controllers
{
    public class PayPalIpnController : ApiController
    {
        [HttpPost]
        public IHttpActionResult PaypalIpnNotification()
        {

            return Ok("Hello");
        }
    }
}
