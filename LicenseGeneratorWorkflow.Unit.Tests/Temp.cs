using LicenseGeneratorWorkflow.Settings;
using Xunit;

namespace LicenseGeneratorWorkflow.Unit.Tests
{
    public class Temp
    {
        [Fact]
        public void TestSettings()
        {
            var settings = new SettingsRepository(@"D:\GitHub\LicenseGeneratorWorkflow\LicenseGeneratorWorkflowDataFiles");
            var values = settings.Load();

        }
    }
}