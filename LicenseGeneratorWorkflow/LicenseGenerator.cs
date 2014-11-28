namespace CryptoLicenseGenerator
{
	public interface LicenseGenerator
	{
		void SetActiveProfile(string profileName);
		string UserData { get; set; }
		string Generate();
		string[] LicenseCodes { get; }
		short NumberOfUsers { get; set; }
		void LoadLicenseFile(string fileName);
	}
}