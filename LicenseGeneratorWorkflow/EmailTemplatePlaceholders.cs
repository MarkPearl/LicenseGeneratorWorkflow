using System.Collections.Generic;

namespace LicenseGeneratorWorkflow
{
	public class EmailTemplatePlaceholders
    {
        public Dictionary<string, string> Populate(string licenseCode, PayPalInfo payPalInfo)
        {
            var placeholders = new Dictionary<string, string>();
            placeholders.Clear();
            AddLicensePlaceholders(licenseCode, placeholders);
            AddPayPalInfoPlaceholders(placeholders, payPalInfo);
            return placeholders;
        }

        private static void AddLicensePlaceholders(string licenseCode, Dictionary<string, string> placeholders)
        {
            placeholders.Add("license", licenseCode);
        }

        private void AddPayPalInfoPlaceholders(Dictionary<string, string> placeholders, PayPalInfo payPalInfo)
        {
            placeholders.Add("IpnItemNumber", payPalInfo.IpnItemNumber);
            placeholders.Add("LastName", payPalInfo.LastName);
            placeholders.Add("PayerCompany", payPalInfo.PayerCompany);
            placeholders.Add("IpnTxnType", payPalInfo.IpnTxnType);
            placeholders.Add("IpnItemNumber", payPalInfo.IpnItemNumber);
            placeholders.Add("McCurrency", payPalInfo.McCurrency);
            placeholders.Add("PaymentDate", payPalInfo.PaymentDate);
            placeholders.Add("PaymentType", payPalInfo.PaymentType);
            placeholders.Add("ResidenceCountry", payPalInfo.ResidenceCountry);
            placeholders.Add("TestIpn", payPalInfo.TestIpn);
            placeholders.Add("TransactionSubject", payPalInfo.TransactionSubject);
            placeholders.Add("HandlingAmount", payPalInfo.HandlingAmount.ToString());
            placeholders.Add("IpnQuantity", payPalInfo.IpnQuantity.ToString());
            placeholders.Add("McFee", payPalInfo.McFee.ToString());
            placeholders.Add("McGross", payPalInfo.McGross.ToString());
            placeholders.Add("PaymentFee", payPalInfo.PaymentFee.ToString());
            placeholders.Add("PaymentGross", payPalInfo.PaymentGross.ToString());
            placeholders.Add("Shipping", payPalInfo.Shipping.ToString());
            placeholders.Add("Tax", payPalInfo.Tax.ToString());
        }
    }
}