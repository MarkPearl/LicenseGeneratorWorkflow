using System.Collections.Generic;
using System.Linq;

namespace LicenseGeneratorWorkflow
{
    public class TemplateToMessageConverter
    {
        private const string PlaceHolderToken = "%%";

        public string Convert(string templateData, Dictionary<string, string> placeholders )
        {
            return ReplacePlaceholders(templateData, placeholders);
        }

        string ReplacePlaceholders(string textTemplate, Dictionary<string, string> placeholders )
        {
            for (var i = 0; i < placeholders.Count; i++)
            {
                var item = placeholders.ElementAt(i);
                textTemplate = textTemplate.Replace(PlaceHolderToken + item.Key + PlaceHolderToken, item.Value);
            }
            return textTemplate;
        }
    }
}