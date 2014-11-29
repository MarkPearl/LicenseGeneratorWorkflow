using System.Collections.Generic;

namespace LicenseGeneratorWorkflow
{
	public interface LicenseGenerator
	{
		void Initialize();
		void SetActiveProfile(string profileName);
		string UserData { get; set; }
		string Generate();
		IEnumerable<string> LicenseCodes { get; }
		short NumberOfUsers { get; set; }
	}
}