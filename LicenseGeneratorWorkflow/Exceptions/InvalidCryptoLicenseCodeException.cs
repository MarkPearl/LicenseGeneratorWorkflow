using System;
using System.Runtime.Serialization;

namespace LicenseGeneratorWorkflow.Exceptions
{
	[Serializable]
	public class InvalidCryptoLicenseCodeException : Exception
	{
		//
		// For guidelines regarding the creation of new exception types, see
		//    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
		// and
		//    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
		//

		public InvalidCryptoLicenseCodeException()
		{
		}

		public InvalidCryptoLicenseCodeException(string message) : base(message)
		{
		}

		public InvalidCryptoLicenseCodeException(string message, Exception inner) : base(message, inner)
		{
		}

		protected InvalidCryptoLicenseCodeException(
			SerializationInfo info,
			StreamingContext context) : base(info, context)
		{
		}

	}
}