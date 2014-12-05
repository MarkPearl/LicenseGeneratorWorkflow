using System;
using System.Runtime.Serialization;

namespace LicenseGeneratorWorkflow.Exceptions
{
	[Serializable]
	public class LicenseFileNotSetException : Exception
	{
		//
		// For guidelines regarding the creation of new exception types, see
		//    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
		// and
		//    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
		//

		public LicenseFileNotSetException()
		{
		}

		public LicenseFileNotSetException(string message) : base(message)
		{
		}

		public LicenseFileNotSetException(string message, Exception inner) : base(message, inner)
		{
		}

		protected LicenseFileNotSetException(
			SerializationInfo info,
			StreamingContext context) : base(info, context)
		{
		}
	}
}