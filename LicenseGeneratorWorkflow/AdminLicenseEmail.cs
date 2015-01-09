using System.IO;
using System.Net.Mail;
using LicenseGeneratorWorkflow.Settings;

namespace LicenseGeneratorWorkflow
{
    public class AdminLicenseEmail
    {
        private readonly TemplateToMessageConverter _templateToMessageConverter;
        private readonly EmailTemplatePlaceholders _emailTemplatePlaceholders;
        private readonly string _emailAddressBcc;
        private readonly string _emailAddressFrom;
        private readonly string _subject;
        private readonly string _emailTemplateFile;

        public AdminLicenseEmail(
            EmailSettings emailSettings,
            TemplateToMessageConverter templateToMessageConverter,
            EmailTemplatePlaceholders emailTemplatePlaceholders)
        {
            _templateToMessageConverter = templateToMessageConverter;
            _emailTemplatePlaceholders = emailTemplatePlaceholders;
            _emailAddressBcc = emailSettings.Bcc;
            _emailAddressFrom = emailSettings.From;
            _subject = emailSettings.Subject;
            _emailTemplateFile = emailSettings.TemaplateFileLocation;
        }

        public MailMessage ConstructEmail(PayPalInfo payPalInfo, string licenseCode)
        {
            var message = new MailMessage();
            message.From = new MailAddress(_emailAddressFrom);
            message.Subject = _subject;
            message.To.Add(payPalInfo.PayerEmail);
            message.Bcc.Add(_emailAddressBcc);
            message.IsBodyHtml = true;
            message.Body = EmailBody(payPalInfo, licenseCode);
            message.IsBodyHtml = false;

            return message;
        }

        string EmailBody(PayPalInfo payPalInfo, string licenseCode)
        {
            var placeholders = _emailTemplatePlaceholders.Populate(licenseCode, payPalInfo);
            var bodyTemplate = File.ReadAllText(_emailTemplateFile);
            return _templateToMessageConverter.Convert(bodyTemplate, placeholders);
        }
    }
}