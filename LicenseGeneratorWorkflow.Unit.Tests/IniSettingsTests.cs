using LicenseGeneratorWorkflow.Settings;
using Xunit;

namespace LicenseGeneratorWorkflow.Unit.Tests
{
    public class IniSettingsTests
    {
		[Fact]
        public void GenerateIniSettingsFile()
        {
			var settingsRepositoryCreator = new SettingsRepositoryCreator();
	        var generalSettings = new GeneralSettings();
			
			// --------- Set General Settings Here -------------------
	        // generalSettings.PayPalSettings.IpnValidationUrl = "123";
			// generalSettings.SmtpSettings.Password = "Secret";

	        settingsRepositoryCreator.Write(generalSettings, @"d:\test.ini");
        }

	    [Fact]
	    public void LoadIniSettingsFile()
	    {
			//const string iniFileLocation = @"D:\GitHub\LicenseGeneratorWorkflow\LicenseGeneratorWorkflowDataFiles\LicenseGeneratorWorkflow.ini";
			//var settings = new SettingsRepository(iniFileLocation);
			//var values = settings.Load();
	    }
    }
}