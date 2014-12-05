using System;
using System.Net;
using System.Text;
using LicenseGeneratorWorkflow.Settings;

namespace LicenseGeneratorWorkflow
{
    public class PaypalIpnValidation
    {
        private const string ItemNumber = "";
        private const decimal ItemCostPerUnit = 100;

        private readonly PayPalSettings _payPalSettings;

        public PaypalIpnValidation(PayPalSettings payPalSettings)
        {
            _payPalSettings = payPalSettings;
        }

        public bool EnsureValid(PayPalInfo payPalInfo)
        {
            DataDumper.Dump(payPalInfo.SerializedMessage());

            var cmd = "cmd = _notify-validate &" + payPalInfo.SerializedMessage();
            var client = new WebClient();
            client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

            DataDumper.Dump(cmd);
            var bytes = Encoding.UTF8.GetBytes(cmd);

            // Send HTTP POST to verify IPN
            var responseBuffer = client.UploadData(_payPalSettings.IpnValidationUrl, "POST", bytes);
            var responseMessage = Encoding.UTF8.GetString(responseBuffer);

            DataDumper.Dump(responseMessage);
            if (responseMessage != "VERIFIED") return false;

            //try
            //{
            //    //VerifyTransactionType(payPalInfo);
            //    //VerifyProductNumber(payPalInfo);
            //    //VerifyCostTotals(payPalInfo);
            //    //VerifyReceiverEmail(payPalInfo);
            //}
            //catch (Exception)
            //{
            //    return false;
            //}

            return true;
        }

        private void VerifyTransactionType(PayPalInfo payPalInfo)
        {
            if (payPalInfo.IpnTxnType != "web_accept")
                throw new Exception("ipn_txn_type is not 'web_accept'");
        }

        private void VerifyProductNumber(PayPalInfo payPalInfo)
        {
            if (payPalInfo.IpnItemNumber != ItemNumber)
                throw new Exception("ipn_item_number not equal to defined item number");
        }

        private void VerifyCostTotals(PayPalInfo payPalInfo)
        {
            var checksumTotal = payPalInfo.IpnQuantity * ItemCostPerUnit + payPalInfo.Tax;
            if (checksumTotal != Convert.ToDecimal(payPalInfo.McGross))
                throw new Exception("Cost does not match");
        }

        private void VerifyReceiverEmail(PayPalInfo payPalInfo)
        {
            if (payPalInfo.IpnReceiverEmail.ToUpper() != _payPalSettings.IpnReceiverEmail.ToUpper())
                throw new Exception();
        }
    }
}