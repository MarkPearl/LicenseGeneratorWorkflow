using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LicenseGeneratorWorkflow
{
    public class IndexedKeyValuePair : IEnumerable<Tuple<string, string>>
    {
        private readonly List<Tuple<string, string>> _collection;

        public IndexedKeyValuePair()
        {
            _collection = new List<Tuple<string, string>>();
        }

        public void Add(string key, string value)
        {
            _collection.Add(new Tuple<string, string>(key, value));
        }

        public IEnumerator<Tuple<string, string>> GetEnumerator()
        {
            return _collection.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class PayPalInfo
    {
        private const string PayPalAccountEmail = "Mark@MaximaSoftware.co.za";
        private const string ItemNumber = "";
        private const decimal ItemCostPerUnit = 100;

        private readonly string _receiverEmail;
        private readonly string _receiverId;
        private readonly string _residenceCountry;
        private readonly string _testIpn;
        private readonly string _transactionSubject;
        private readonly string _txnId;
        private readonly string _txnType;
        private readonly decimal _mcGross;
        private readonly string _paymentDate;
        private readonly decimal _paymentFee;
        private readonly decimal _paymentGross;
        private readonly string _paymentStatus;
        private readonly string _paymentType;
        private readonly string _protectionEligibility;
        private readonly short _quantity;
        private readonly decimal _shipping;
        private readonly decimal _tax;
        private readonly string _notifyVersion;
        private readonly string _charset;
        private readonly string _verifySign;
        private decimal _ipnTax;
        private readonly string _payerUserName;
        private readonly string _payerStatus;
        private readonly string _firstName;
        private readonly string _lastName;
        private readonly string _addressCity;
        private readonly string _addressCountry;
        private readonly string _addressCountryCode;
        private readonly string _addressName;
        private readonly string _addressState;
        private readonly string _addressStatus;
        private readonly string _addressStreet;
        private readonly string _addressZip;
        private readonly string _custom;
        private readonly decimal _handlingAmount;
        private readonly string _itemName;
        private readonly string _itemNumber;
        private readonly string _mcCurrency;
        private readonly decimal _mcFee;
        private readonly string _payerEmail;
        private readonly string _payerCompany;


        public PayPalInfo(
            string receiverEmail,
            string receiverId,
            string residenceCountry,
            string testIpn,
            string transactionSubject,
            string txnId,
            string txnType,
            string payerEmail,
            string payerUserName,
            string payerStatus,
            string firstName,
            string lastName,
            string addressCity,
            string addressCountry,
            string addressCountryCode,
            string addressName,
            string addressState,
            string addressStatus,
            string addressStreet,
            string addressZip,
            string custom,
            decimal handlingAmount,
            string itemName,
            string itemNumber,
            string mcCurrency,
            decimal mcFee,
            decimal mcGross,
            string paymentDate,
            decimal paymentFee,
            decimal paymentGross,
            string paymentStatus,
            string paymentType,
            string protectionEligibility,
            short quantity,
            decimal shipping,
            decimal tax,
            string notifyVersion,
            string charset,
            string verifySign
            )
        {
            _receiverEmail = receiverEmail;
            _receiverId = receiverId;
            _residenceCountry = residenceCountry;
            _testIpn = testIpn;
            _transactionSubject = transactionSubject;
            _txnId = txnId;
            _txnType = txnType;
            _payerEmail = payerEmail;
            _payerUserName = payerUserName;
            _payerStatus = payerStatus;
            _firstName = firstName;
            _lastName = lastName;
            _addressCity = addressCity;
            _addressCountry = addressCountry;
            _addressCountryCode = addressCountryCode;
            _addressName = addressName;
            _addressState = addressState;
            _addressStatus = addressStatus;
            _addressStreet = addressStreet;
            _addressZip = addressZip;
            _custom = custom;
            _handlingAmount = handlingAmount;
            _itemName = itemName;
            _itemNumber = itemNumber;
            _mcCurrency = mcCurrency;
            _mcFee = mcFee;
            _mcGross = mcGross;
            _paymentDate = paymentDate;
            _paymentFee = paymentFee;
            _paymentGross = paymentGross;
            _paymentStatus = paymentStatus;
            _paymentType = paymentType;
            _protectionEligibility = protectionEligibility;
            _quantity = quantity;
            _shipping = shipping;
            _tax = tax;
            _notifyVersion = notifyVersion;
            _charset = charset;
            _verifySign = verifySign;
        }

        public PayPalInfo(
            string ipnTxnType,
            string ipnItemNumber,
            short ipnQuantity,
            Decimal mcGross,
            Decimal ipnTax,
            string ipnReceiverEmail,
            string payerUserName,
            string payerEmail,
            string payerCompany
            )
        {
            _txnType = ipnTxnType;
            _itemNumber = ipnItemNumber;
            _quantity = ipnQuantity;
            _mcGross = mcGross;
            _ipnTax = ipnTax;
            _receiverEmail = ipnReceiverEmail;
            _payerUserName = payerUserName;
            _payerEmail = payerEmail;
            _payerCompany = payerCompany;

            VerifyTransactionType();
            VerifyProductNumber();
            VerifyCostTotals();
            VerifyReceiverEmail();
        }

        public string IpnReceiverEmail
        {
            get { return _receiverEmail; }
        }

        public decimal IpnTax
        {
            get { return _ipnTax; }
            set { _ipnTax = value; }
        }

        public decimal McGross
        {
            get { return _mcGross; }
        }

        public short IpnQuantity
        {
            get { return _quantity; }
        }

        public string IpnItemNumber
        {
            get { return _itemNumber; }
        }

        public string IpnTxnType
        {
            get { return _txnType; }
        }

        public string PayerEmail
        {
            get { return _payerEmail; }
        }

        public string PayerUserName
        {
            get { return _payerUserName; }
        }

        public string PayerCompany
        {
            get { return _payerCompany; }
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
            get { return _receiverId; }
        }

        public string ResidenceCountry
        {
            get { return _residenceCountry; }
        }

        public string TestIpn
        {
            get { return _testIpn; }
        }

        public string TransactionSubject
        {
            get { return _transactionSubject; }
        }

        public string TxnId
        {
            get { return _txnId; }
        }

        public string PaymentDate
        {
            get { return _paymentDate; }
        }

        public decimal PaymentFee
        {
            get { return _paymentFee; }
        }

        public decimal PaymentGross
        {
            get { return _paymentGross; }
        }

        public string PaymentStatus
        {
            get { return _paymentStatus; }
        }

        public string PaymentType
        {
            get { return _paymentType; }
        }

        public string ProtectionEligibility
        {
            get { return _protectionEligibility; }
        }

        public decimal Shipping
        {
            get { return _shipping; }
        }

        public decimal Tax
        {
            get { return _tax; }
        }

        public string NotifyVersion
        {
            get { return _notifyVersion; }
        }

        public string Charset
        {
            get { return _charset; }
        }

        public string VerifySign
        {
            get { return _verifySign; }
        }

        public string PayerStatus
        {
            get { return _payerStatus; }
        }

        public string FirstName
        {
            get { return _firstName; }
        }

        public string LastName
        {
            get { return _lastName; }
        }

        public string AddressCity
        {
            get { return _addressCity; }
        }

        public string AddressCountry
        {
            get { return _addressCountry; }
        }

        public string AddressCountryCode
        {
            get { return _addressCountryCode; }
        }

        public string AddressName
        {
            get { return _addressName; }
        }

        public string AddressState
        {
            get { return _addressState; }
        }

        public string AddressStatus
        {
            get { return _addressStatus; }
        }

        public string AddressStreet
        {
            get { return _addressStreet; }
        }

        public string AddressZip
        {
            get { return _addressZip; }
        }

        public string Custom
        {
            get { return _custom; }
        }

        public decimal HandlingAmount
        {
            get { return _handlingAmount; }
        }

        public string ItemName
        {
            get { return _itemName; }
        }

        public string McCurrency
        {
            get { return _mcCurrency; }
        }

        public decimal McFee
        {
            get { return _mcFee; }
        }

        private void VerifyTransactionType()
        {
            if (_txnType != "web_accept")
                throw new Exception("ipn_txn_type is not 'web_accept'");
        }

        private void VerifyProductNumber()
        {
            if (_itemNumber != ItemNumber)
                throw new Exception("ipn_item_number not equal to defined item number");
        }

        private void VerifyCostTotals()
        {
            var checksumTotal = _quantity * ItemCostPerUnit + _ipnTax;
            if (checksumTotal != _mcGross)
                throw new Exception("Cost does not match");
        }

        private void VerifyReceiverEmail()
        {
            if (_receiverEmail.ToUpper() != PayPalAccountEmail.ToUpper())
                throw new Exception();
        }

        public string SerializedMessage()
        {
            var data = new IndexedKeyValuePair();
            data.Add("mc_gross", _mcGross.ToString());
            data.Add("protection_eligibility", _protectionEligibility);
            data.Add("address_status", _addressStatus);
            data.Add("payer_id", _payerUserName);
            data.Add("tax", _tax.ToString("F"));
            data.Add("address_street", _addressStreet);
            data.Add("payment_date", _paymentDate);
            data.Add("payment_status", _paymentStatus);
            data.Add("charset", _charset);
            data.Add("address_zip", _addressZip);
            data.Add("first_name", _firstName);
            data.Add("mc_fee", _mcFee.ToString("F"));
            data.Add("address_country_code", _addressCountryCode);
            data.Add("address_name", _addressName);
            data.Add("notify_version", _notifyVersion);
            data.Add("custom", _custom);
            data.Add("payer_status", _payerStatus);
            data.Add("address_country", _addressCountry);
            data.Add("address_city", _addressCity);
            data.Add("quantity", _quantity.ToString());
            data.Add("verify_sign", _verifySign);
            data.Add("payer_email", _payerEmail);
            data.Add("txn_id", _txnId);
            data.Add("payment_type", _paymentType);
            data.Add("last_name", _lastName);
            data.Add("address_state", _addressState);
            data.Add("receiver_email", _receiverEmail);
            data.Add("payment_fee", _paymentFee.ToString("F"));
            data.Add("receiver_id", _receiverId);
            data.Add("txn_type", _txnType);
            data.Add("item_name", _itemName);
            data.Add("mc_currency", _mcCurrency);
            data.Add("item_number", _itemNumber);
            data.Add("residence_country", _residenceCountry);
            data.Add("test_ipn", _testIpn);
            data.Add("handling_amount", _handlingAmount.ToString("F"));
            data.Add("transaction_subject", _transactionSubject);
            data.Add("payment_gross", _paymentGross.ToString("F"));
            data.Add("shipping", _shipping.ToString("F"));

            return CollateData(data);
        }

        private string CollateData(IndexedKeyValuePair data)
        {
            var result = string.Empty;

            for (var i = 0; i < data.Count() - 1; i++)
            {
                var item = data.ElementAt(i);
                result += MergeData(item) + "&";
            }

            var endItem = data.ElementAt(data.Count() - 1);
            result += MergeData(endItem);
            return result;
        }

        private string MergeData(string field, string value)
        {
            return string.Format("{0}={1}", field, HttpUtility.UrlEncode(value));
        }

        private string MergeData(Tuple<string,string> entry)
        {
            return MergeData(entry.Item1, entry.Item2);
        }

    }
}