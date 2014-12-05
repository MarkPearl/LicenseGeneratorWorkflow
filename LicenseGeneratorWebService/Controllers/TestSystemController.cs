using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using LicenseGeneratorWorkflow.Settings;

namespace LicenseGeneratorWebService.Controllers
{
    public class SystemResults
    {
        public string SettingsFileLocation { get; set; }
        public GeneralSettings Settings { get; set; }
    }

    public class TestSystemController : Controller
    {
        [System.Web.Http.HttpGet]
        public ActionResult Index()
        {
            var data = new SystemResults();
            data.SettingsFileLocation = System.Web.Configuration.WebConfigurationManager.AppSettings["SettingsFileLocation"];
            data.Settings = new SettingsRepository(System.Web.Configuration.WebConfigurationManager.AppSettings["SettingsFileLocation"]).Load();
            

            return View("Index", data );
        }
    }
}
