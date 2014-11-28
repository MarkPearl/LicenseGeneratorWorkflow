using System;

namespace CryptoLicenseGenerator
{
	public class PayPalInfo
	{
		private const string PayPalAccountEmail = "Mark@MaximaSoftware.co.za";
		private const bool UsePayPalSandBox = true;
		private const string ItemNumber = "SKU/item code of your product";
		private const Decimal ItemCostPerUnit = 100;

		private string _ipnTxnType;
		private string _ipnItemNumber;
		private short _ipnQuantity;
		private Decimal _ipnMcGross;
		private Decimal _ipnTax;
		private string _ipnReceiverEmail;
		private string _message;
		private readonly string _payerUserName;
		private readonly string _payerEmail;
		private readonly string _payerCompany;


		public PayPalInfo(
			string ipnTxnType,
			string ipnItemNumber,
			short ipnQuantity,
			Decimal ipnMcGross,
			Decimal ipnTax,
			string ipnReceiverEmail,
			string message,
			string payerUserName,
			string payerEmail,
			string payerCompany)
		{
			_ipnTxnType = ipnTxnType;
			_ipnItemNumber = ipnItemNumber;
			_ipnQuantity = ipnQuantity;
			_ipnMcGross = ipnMcGross;
			_ipnTax = ipnTax;
			_ipnReceiverEmail = ipnReceiverEmail;
			_message = message;
			_payerUserName = payerUserName;
			_payerEmail = payerEmail;
			_payerCompany = payerCompany;

			VerifyTransactionType();
			VerifyProductNumber();
			VerifyCostTotals();
			VerifyReceiverEmail();
			VerifyMessage();
		}

		public string Message
		{
			get { return _message; }
			set { _message = value; }
		}

		public string IpnReceiverEmail
		{
			get { return _ipnReceiverEmail; }
			set { _ipnReceiverEmail = value; }
		}

		public decimal IpnTax
		{
			get { return _ipnTax; }
			set { _ipnTax = value; }
		}

		public decimal IpnMcGross
		{
			get { return _ipnMcGross; }
			set { _ipnMcGross = value; }
		}

		public short IpnQuantity
		{
			get { return _ipnQuantity; }
			set { _ipnQuantity = value; }
		}

		public string IpnItemNumber
		{
			get { return _ipnItemNumber; }
			set { _ipnItemNumber = value; }
		}

		public string IpnTxnType
		{
			get { return _ipnTxnType; }
			set { _ipnTxnType = value; }
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

		private void VerifyTransactionType()
		{
			if (_ipnTxnType != "web_accept")
				throw new Exception("ipn_txn_type is not 'web_accept'");
		}

		private void VerifyProductNumber()
		{
			if (_ipnItemNumber != ItemNumber)
				throw new Exception("ipn_item_number not equal to defined item number");
		}

		private void VerifyCostTotals()
		{
			var checksumTotal = _ipnQuantity * ItemCostPerUnit + _ipnTax;
			if (checksumTotal != _ipnMcGross)
				throw new Exception("Cost does not match");
		}

		private void VerifyMessage()
		{
			if (_message != "VERIFIED")
				throw new Exception("IPN verification unsuccessful.");
		}

		private void VerifyReceiverEmail()
		{
			if (_ipnReceiverEmail.ToUpper() != PayPalAccountEmail.ToUpper())
				throw new Exception();
		}

	}
}