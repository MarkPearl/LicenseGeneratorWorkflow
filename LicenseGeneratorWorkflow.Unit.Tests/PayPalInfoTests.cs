using System.Collections.Specialized;
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

            var data = new NameValueCollection();

            data.Add("mc_gross", "19.95");
            data.Add("protection_eligibility", "Eligible");
            data.Add("address_status", "confirmed");
            data.Add("payer_id", "LPLWNMTBWMFAY");
            data.Add("tax", "0.00");
            data.Add("address_street", "1 Main St");
            data.Add("payment_date", "20:12:59 Jan 13, 2009 PST");
            data.Add("payment_status", "Completed");
            data.Add("charset", "windows-1252");
            data.Add("address_zip", "95131");
            data.Add("first_name", "Test");
            data.Add("mc_fee", "0.88");
            data.Add("address_country_code", "US");
            data.Add("address_name", "Test User");
            data.Add("notify_version", "2.6");
            data.Add("custom", "");
            data.Add("payer_status", "verified");
            data.Add("address_country", "United States");
            data.Add("address_city", "San Jose");
            data.Add("quantity", "1");
            data.Add("verify_sign", "AtkOfCXbDm2hu0ZELryHFjY-Vb7PAUvS6nMXgysbElEn9v-1XcmSoGtf");
            data.Add("payer_email", "gpmac_1231902590_per@paypal.com");
            data.Add("txn_id", "61E67681CH3238416");
            data.Add("payment_type", "instant");
            data.Add("last_name", "User");
            data.Add("address_state", "CA");
            data.Add("receiver_email", "gpmac_1231902686_biz@paypal.com");
            data.Add("payment_fee", "0.88");
            data.Add("receiver_id", "S8XGHLYDW9T3S");
            data.Add("txn_type", "express_checkout");
            data.Add("item_name", "");
            data.Add("mc_currency", "USD");
            data.Add("item_number", "");
            data.Add("residence_country", "US");
            data.Add("test_ipn", "1");
            data.Add("handling_amount", "0.00");
            data.Add("transaction_subject", "");
            data.Add("payment_gross", "19.95");
            data.Add("shipping", "0.00");

            var payPalInfo = new PayPalInfo(data);
            var result = payPalInfo.SerializedMessage();

            Assert.True(expected.ToUpper().StartsWith(result.ToUpper()));
            Assert.Equal(result.ToUpper(), expected.ToUpper());
        }
    }
}