using Xunit;

namespace LicenseGeneratorWorkflow.Unit.Tests
{
    public class PayPalInfoTests
    {
        [Fact]
        public void ReturnCorrectSerialization()
        {
            //For more info https://developer.paypal.com/docs/classic/ipn/integration-guide/IPNIntro/#id08CKFJ00JYK
            const string expected = "mc_gross=19.95&protection_eligibility=Eligible&address_status=confirmed&payer_id=LPLWNMTBWMFAY&tax=0.00&address_street=1+Main+St&payment_date=20%3A12%3A59+Jan+13%2C+2009+PST&payment_status=Completed&charset=windows-1252&address_zip=95131&first_name=Test&mc_fee=0.88&address_country_code=US&address_name=Test+User&notify_version=2.6&custom=&payer_status=verified&address_country=United+States&address_city=San+Jose&quantity=1&verify_sign=AtkOfCXbDm2hu0ZELryHFjY-Vb7PAUvS6nMXgysbElEn9v-1XcmSoGtf&payer_email=gpmac_1231902590_per%40paypal.com&txn_id=61E67681CH3238416&payment_type=instant&last_name=User&address_state=CA&receiver_email=gpmac_1231902686_biz%40paypal.com&payment_fee=0.88&receiver_id=S8XGHLYDW9T3S&txn_type=express_checkout&item_name=&mc_currency=USD&item_number=&residence_country=US&test_ipn=1&handling_amount=0.00&transaction_subject=&payment_gross=19.95&shipping=0.00";

            var payPalInfo = new PayPalInfo(
                "gpmac_1231902686_biz@paypal.com",
                "S8XGHLYDW9T3S",
                "US",
                "1",
                "",
                "61E67681CH3238416",
                "express_checkout",
                "gpmac_1231902590_per@paypal.com",
                "LPLWNMTBWMFAY",
                "verified",
                "Test",
                "User",
                "San Jose",
                "United States",
                "US",
                "Test User",
                "CA",
                "confirmed",
                "1 Main St",
                "95131",
                "",
                new decimal(0.00),
                "",
                "",
                "USD",
                new decimal(0.88),
                new decimal(19.95),
                "20:12:59 Jan 13, 2009 PST",
                new decimal(0.88),
                new decimal(19.95),
                "Completed",
                "instant",
                "Eligible",
                1,
                new decimal(0.00), 
                new decimal(0.00), 
                "2.6",
                "windows-1252",
                "AtkOfCXbDm2hu0ZELryHFjY-Vb7PAUvS6nMXgysbElEn9v-1XcmSoGtf"
                );

            var result = payPalInfo.SerializedMessage();

            Assert.True(expected.ToUpper().StartsWith(result.ToUpper()));
            Assert.Equal(result.ToUpper(), expected.ToUpper());
        }
    }
}