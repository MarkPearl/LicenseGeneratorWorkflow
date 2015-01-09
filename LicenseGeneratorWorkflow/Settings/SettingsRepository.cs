using System;
using System.Linq;
using IniParser;
using IniParser.Model;
using LicenseGeneratorWorkflow.Settings.Constants;

namespace LicenseGeneratorWorkflow.Settings
{
	public class SettingsRepository
	{
		private readonly string _settingsFileLocation;
		private IniData _parsedData;

		public SettingsRepository(string settingsFileLocation)
		{
			_settingsFileLocation = settingsFileLocation;
		}

		public GeneralSettings Load()
		{
			var parser = new FileIniDataParser();
			_parsedData = parser.ReadFile(_settingsFileLocation);
			var generalSettings = new GeneralSettings();

			LoadPaypalSectionSettings(generalSettings);
			LoadSmtpSectionSettings(generalSettings);
			LoadCryptoSectionSettings(generalSettings);
			LoadEmailSectionSettings(generalSettings);
			LoadProductProfileSectionsSettings(generalSettings);

			return generalSettings;
		}

		private void LoadProductProfileSectionsSettings(GeneralSettings generalSettings)
		{
			var productProfiles = LoadSection(IniFileSections.ProductLicenseProfiles);
			for (var i = 0; i < productProfiles.Count; i++)
			{
				var item = productProfiles.ElementAt(i);
				generalSettings.ProductProfileSettings.Add(item.KeyName, item.Value);
			}
		}

		private void LoadEmailSectionSettings(GeneralSettings generalSettings)
		{
			var emailSettings = generalSettings.EmailSettings;
			emailSettings.Subject = GetSectionFieldValue(IniFileSections.EmailSection, IniEmail.Subject);
			emailSettings.EndUserEmailTemaplateFileLocation = GetSectionFieldValue(IniFileSections.EmailSection, IniEmail.Enduseremailtemaplatefilelocation);
			emailSettings.AdminEmailTemaplateFileLocation = GetSectionFieldValue(IniFileSections.EmailSection, IniEmail.Adminemailtemaplatefilelocation);
			emailSettings.ProductName = GetSectionFieldValue(IniFileSections.EmailSection, IniEmail.Productname);
			emailSettings.From = GetSectionFieldValue(IniFileSections.EmailSection, IniEmail.FromField);
			emailSettings.Bcc = GetSectionFieldValue(IniFileSections.EmailSection, IniEmail.Bcc);
		}

		private void LoadCryptoSectionSettings(GeneralSettings generalSettings)
		{
			var cryptoSection = LoadSection(IniFileSections.CryptoLicensing);
			var cryptoLicenseSettings = generalSettings.CryptoLicenseSettings;
			cryptoLicenseSettings.LicenseCode = cryptoSection.GetKeyData(IniCrypto.LicenseCode).Value;
			cryptoLicenseSettings.LicenseFileLocation = cryptoSection.GetKeyData(IniCrypto.Licensefilelocation).Value;
		}

		private void LoadSmtpSectionSettings(GeneralSettings generalSettings)
		{
			var smtpSettings = generalSettings.SmtpSettings;
			smtpSettings.Server = GetSectionFieldValue(IniFileSections.SmtpSection, IniSmtp.Smtpserver);
			smtpSettings.Port = Convert.ToInt32(GetSectionFieldValue(IniFileSections.SmtpSection, IniSmtp.Smtpport));
			smtpSettings.UseSsl = Convert.ToBoolean(GetSectionFieldValue(IniFileSections.SmtpSection, IniSmtp.Smtpusessl));
			smtpSettings.Username = GetSectionFieldValue(IniFileSections.SmtpSection, IniSmtp.Smtpusername);
			smtpSettings.Password = GetSectionFieldValue(IniFileSections.SmtpSection, IniSmtp.Smtppassword);
		}

		private void LoadPaypalSectionSettings(GeneralSettings generalSettings)
		{
			var payPalSection = LoadSection(IniFileSections.PayPalSection);
			var payPalSettings = generalSettings.PayPalSettings;
			payPalSettings.IpnReceiverEmail = payPalSection.GetKeyData(IniPayPal.IpnReceiverEmail).Value;
			payPalSettings.IpnValidationUrl = payPalSection.GetKeyData(IniPayPal.IpnValidationUrl).Value;
		}

		private string GetSectionFieldValue(string sectionName, string fieldName)
		{
			var emailSection = LoadSection(sectionName);
			return emailSection.GetKeyData(fieldName).Value;
		}

		private KeyDataCollection LoadSection(string sectionName)
		{
			return _parsedData[sectionName];
		}

	}
}
