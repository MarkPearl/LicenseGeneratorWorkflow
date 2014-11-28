using Crypto = LogicNP.CryptoLicensing;

namespace CryptoLicenseGenerator
{
	public class CryptoLicenseGeneratorWrapper : LicenseGenerator
	{
		private Crypto.CryptoLicenseGenerator _cryptoLicenseGenerator;
		private bool _settingsLicenseFileSet;

		public CryptoLicenseGeneratorWrapper()
		{
			_settingsLicenseFileSet = false;
		}

		public void LoadLicenseFile(string fileName)
		{
			_cryptoLicenseGenerator = new Crypto.CryptoLicenseGenerator(fileName);
			_settingsLicenseFileSet = true;
		}

		public void SetLicenseCode(string licenseCode)
		{
			_cryptoLicenseGenerator.SetLicenseCode(licenseCode);
		}

		public void SetActiveProfile(string profileName)
		{
			EnsureLicenseFileSet();
			_cryptoLicenseGenerator.SetActiveProfile(profileName);
		}

		public string UserData
		{
			get
			{
				EnsureLicenseFileSet();
				return _cryptoLicenseGenerator.UserData;
			}
			set
			{
				EnsureLicenseFileSet();
				_cryptoLicenseGenerator.UserData = value;
			}
		}

		public string Generate()
		{
			EnsureLicenseFileSet();
			return _cryptoLicenseGenerator.Generate();
		}

		public string[] LicenseCodes
		{
			get
			{
				EnsureLicenseFileSet();
				return _cryptoLicenseGenerator.LicenseCodes;
			}
		}

		public short NumberOfUsers
		{
			get
			{
				EnsureLicenseFileSet();
				return _cryptoLicenseGenerator.NumberOfUsers;
			}
			set
			{
				EnsureLicenseFileSet();
				_cryptoLicenseGenerator.NumberOfUsers = value;
			}
		}


		private void EnsureLicenseFileSet()
		{
			if (!_settingsLicenseFileSet) throw new LicenseFileNotSetException();
		}
	}
}