using System.Collections.Generic;
using CryptoLicenseGenerator;

namespace LicenseGeneratorWorkflow
{
	public class CryptoLicenseGeneratorWrapper : LicenseGenerator
	{
		private readonly string _licenseFileLocation;
		private readonly string _licenseCode;
		private LogicNP.CryptoLicensing.CryptoLicenseGenerator _cryptoLicenseGenerator;
		private bool _settingsLicenseFileSet;

		public CryptoLicenseGeneratorWrapper(string licenseFileLocation, string licenseCode)
		{
			_licenseFileLocation = licenseFileLocation;
			_licenseCode = licenseCode;
			_settingsLicenseFileSet = false;
		}

		public void Initialize()
		{
			LoadLicenseFile(_licenseFileLocation);

			//See for info on valid license files http://www.ssware.com/support/viewtopic.php?t=734
			SetLicenseCode(_licenseCode);
		}

		private void LoadLicenseFile(string fileName)
		{
			_cryptoLicenseGenerator = new LogicNP.CryptoLicensing.CryptoLicenseGenerator(fileName);
			_settingsLicenseFileSet = true;
		}

		private void SetLicenseCode(string licenseCode)
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

		public IEnumerable<string> LicenseCodes
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
