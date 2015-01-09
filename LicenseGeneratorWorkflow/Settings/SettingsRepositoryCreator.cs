using IniParser;
using IniParser.Model;
using LicenseGeneratorWorkflow.Settings.Constants;

namespace LicenseGeneratorWorkflow.Settings
{
	public class SettingsRepositoryCreator
	{
		private IniData _iniData;

		public void Write(GeneralSettings generalSettings, string iniFileLocation)
		{
			var parser = new FileIniDataParser();
			_iniData = new IniData();

			CreateCryptoSectionSettings(generalSettings);
			CreatePayPalSectionAndFields(generalSettings);
			CreateSmtpSectionAndFields(generalSettings);
			CreateEmailSectionAndFields(generalSettings);
			CreateProductProfilesSectionAndFields(generalSettings);

			parser.WriteFile(iniFileLocation, _iniData);
		}

		private void CreateProductProfilesSectionAndFields(GeneralSettings generalSettings)
		{
			ProductProfileSettings productProfiles	 = generalSettings.ProductProfileSettings;
			AddSection(IniFileSections.ProductLicenseProfiles);

			foreach (var key in productProfiles.Keys())
			{
				var value = productProfiles.GetValue(key);
				AddSectionField(IniFileSections.ProductLicenseProfiles, key, value);				
			}
		}

		private void CreateCryptoSectionSettings(GeneralSettings generalSettings)
		{
			var cryptoLicenseSettings = generalSettings.CryptoLicenseSettings;
			AddSectionField(IniFileSections.CryptoLicensing, IniCrypto.LicenseCode, cryptoLicenseSettings.LicenseCode);
			AddSectionField(IniFileSections.CryptoLicensing, IniCrypto.Licensefilelocation, cryptoLicenseSettings.LicenseFileLocation);
		}

		private void CreateSmtpSectionAndFields(GeneralSettings generalSettings)
		{
			var smtpSettings = generalSettings.SmtpSettings;
			AddSectionField(IniFileSections.SmtpSection, IniSmtp.Smtpserver, smtpSettings.Server);
			AddSectionField(IniFileSections.SmtpSection, IniSmtp.Smtpport, smtpSettings.Port.ToString());
			AddSectionField(IniFileSections.SmtpSection, IniSmtp.Smtpusessl, smtpSettings.UseSsl.ToString());
			AddSectionField(IniFileSections.SmtpSection, IniSmtp.Smtpusername, smtpSettings.Username);
			AddSectionField(IniFileSections.SmtpSection, IniSmtp.Smtppassword, smtpSettings.Password);
		}

		private void CreatePayPalSectionAndFields(GeneralSettings generalSettings)
		{
			AddSectionField(IniFileSections.PayPalSection, IniPayPal.IpnValidationUrl, generalSettings.PayPalSettings.IpnValidationUrl);
			AddSectionField(IniFileSections.PayPalSection, IniPayPal.IpnReceiverEmail, generalSettings.PayPalSettings.IpnReceiverEmail);
		}

		private void CreateEmailSectionAndFields(GeneralSettings generalSettings)
		{
			var emailSettings = generalSettings.EmailSettings;
			AddSectionField(IniFileSections.EmailSection, IniEmail.Subject, emailSettings.Subject);
			AddSectionField(IniFileSections.EmailSection, IniEmail.Enduseremailtemaplatefilelocation, emailSettings.EndUserEmailTemaplateFileLocation);
			AddSectionField(IniFileSections.EmailSection, IniEmail.Adminemailtemaplatefilelocation, emailSettings.AdminEmailTemaplateFileLocation);
			AddSectionField(IniFileSections.EmailSection, IniEmail.Productname, emailSettings.ProductName);
			AddSectionField(IniFileSections.EmailSection, IniEmail.FromField, emailSettings.From);
			AddSectionField(IniFileSections.EmailSection, IniEmail.Bcc, emailSettings.Bcc);
		}

		private void AddSectionField(string sectionName, string fieldName, string fieldValue)
		{
			if (!_iniData.Sections.ContainsSection(sectionName))
			{
				AddSection(sectionName);
			}

			_iniData[sectionName].AddKey(fieldName);
			_iniData[sectionName].GetKeyData(fieldName).Value = fieldValue;
		}

		private void AddSection(string sectionName)
		{
			_iniData.Sections.AddSection(sectionName);
		}
	}
}