using System;
using System.Collections.Specialized;
using System.Web;

namespace LicenseGeneratorWorkflow
{
    public class PayPalInfo
    {
        private readonly NameValueCollection _nameValueCollection;

        public PayPalInfo()
        {
            _nameValueCollection = new NameValueCollection();
        }

        public PayPalInfo(NameValueCollection nameValueCollection)
        {
            _nameValueCollection = nameValueCollection;
        }

        public string IpnReceiverEmail
        {
            get { return _nameValueCollection["receiver_email"]; }
        }

        public decimal McGross
        {
            get { return Convert.ToDecimal(_nameValueCollection["mc_gross"]); }
        }

        public short IpnQuantity
        {
            get { return Convert.ToInt16(_nameValueCollection["quantity"]); }
        }

        public string IpnItemNumber
        {
            get { return _nameValueCollection["item_number"]; }
        }

        public string IpnTxnType
        {
            get { return _nameValueCollection["txn_type"]; }
        }

        public string PayerEmail
        {
            get { return _nameValueCollection["payer_email"]; }
        }

        public string PayerUserName
        {
            get { return _nameValueCollection["payer_id"]; }
        }

        public string PayerCompany
        {
            get { return string.Empty; }
        }

        public string UserData
        {
            get
            {
                return PayerUserName + "#" + PayerCompany + "#" + PayerEmail;
            }
        }

        public string ReceiverId
        {
            get { return _nameValueCollection["receiver_id"]; }
        }

        public string ResidenceCountry
        {
            get { return _nameValueCollection["residence_country"]; }
        }

        public string TestIpn
        {
            get { return _nameValueCollection["test_ipn"]; }
        }

        public string TransactionSubject
        {
            get { return _nameValueCollection["transaction_subject"]; }
        }

        public string TxnId
        {
            get { return _nameValueCollection["txn_id"]; }
        }

        public string PaymentDate
        {
            get { return _nameValueCollection["payment_date"]; }
        }

        public decimal PaymentFee
        {
            get { return Convert.ToDecimal(_nameValueCollection["payment_fee"]); }
        }

        public decimal PaymentGross
        {
            get { return Convert.ToDecimal(_nameValueCollection["payment_gross"]); }
        }

        public string PaymentStatus
        {
            get { return _nameValueCollection["payment_status"]; }
        }

        public string PaymentType
        {
            get { return _nameValueCollection["payment_type"]; }
        }

        public string ProtectionEligibility
        {
            get { return _nameValueCollection["protection_eligibility"]; }
        }

        public decimal Shipping
        {
            get { return Convert.ToDecimal(_nameValueCollection["shipping"]); }
        }

        public decimal Tax
        {
            get { return Convert.ToDecimal(_nameValueCollection["tax"]); }
        }

        public string NotifyVersion
        {
            get { return _nameValueCollection["notify_version"]; }
        }

        public string Charset
        {
            get { return _nameValueCollection["charset"]; }
        }

        public string VerifySign
        {
            get { return _nameValueCollection["verify_sign"]; }
        }

        public string PayerStatus
        {
            get { return _nameValueCollection["payer_status"]; }
        }

        public string FirstName
        {
            get { return _nameValueCollection["first_name"]; }
        }

        public string LastName
        {
            get { return _nameValueCollection["last_name"]; }
        }

        public string AddressCity
        {
            get { return _nameValueCollection["address_city"]; }
        }

        public string AddressCountry
        {
            get { return _nameValueCollection["address_country"]; }
        }

        public string AddressCountryCode
        {
            get { return _nameValueCollection["address_country_code"]; }
        }

        public string AddressName
        {
            get { return _nameValueCollection["address_name"]; }
        }

        public string AddressState
        {
            get { return _nameValueCollection["address_state"]; }
        }

        public string AddressStatus
        {
            get { return _nameValueCollection["address_status"]; }
        }

        public string AddressStreet
        {
            get { return _nameValueCollection["address_street"]; }
        }

        public string AddressZip
        {
            get { return _nameValueCollection["address_zip"]; }
        }

        public string Custom
        {
            get { return _nameValueCollection["custom"]; }
        }

        public decimal HandlingAmount
        {
            get { return Convert.ToDecimal(_nameValueCollection["handling_amount"]); }
        }

        public string ItemName
        {
            get { return _nameValueCollection["item_name"]; }
        }

        public string McCurrency
        {
            get { return _nameValueCollection["mc_currency"]; }
        }

        public decimal McFee
        {
            get { return Convert.ToDecimal(_nameValueCollection["mc_fee"]); }
        }

        public NameValueCollection NameValueCollection
        {
            get { return _nameValueCollection; }
        }


        public string SerializedMessage()
        {
            return CollateData(_nameValueCollection);
        }

        private string CollateData(NameValueCollection data)
        {
            var result = string.Empty;

            for (var i = 0; i < data.Count; i++)
            {
                var key = data.GetKey(i);
                var value = data.Get(i);

                result += MergeData(key, value);
                if (i < data.Count - 1) result += "&";
            }

            return result;
        }

        private string MergeData(string field, string value)
        {
            return string.Format("{0}={1}", field, HttpUtility.UrlEncode(value));
        }
    }
}