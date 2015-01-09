namespace LicenseGeneratorWorkflow.Settings
{
    public class EmailSettings
    {
        public string Subject { get; set; }
        public string TemaplateFileLocation { get; set; }
        public string ProductName { get; set; }
        public string From { get; set; }
        public string Bcc { get; set; }
	    public string EndUserEmailTemaplateFileLocation { get; set; }
	    public string AdminEmailTemaplateFileLocation { get; set; }
    }
}