using System.Collections.Generic;

namespace LicenseGeneratorWorkflow.Settings
{
    public class ProductProfileSettings
    {
        private Dictionary<string, string> _productProfiles;

        public ProductProfileSettings()
        {
            _productProfiles = new Dictionary<string, string>();
        }

        public string GetValue(string key)
        {
            return _productProfiles[key];
        }

        public void Add(string key, string value)
        {
            _productProfiles.Add(key, value);
        }

        public string GetValueOrDefault(string key)
        {
            string temp;
            if (_productProfiles.TryGetValue(key, out temp))
            {
                return _productProfiles[key];
            }
            return _productProfiles["Default"];
        }

    }

    public class PayPalSettings
    {
        public string IpnReceiverEmail { get; set; }
        public string IpnValidationUrl { get; set; }
    }
}