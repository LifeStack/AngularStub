using System;
using System.Web.Http;
using System.Web.Mvc;
using AngularStub.Web.Areas.HelpPage.ModelDescriptions;
using AngularStub.Web.Areas.HelpPage.Models;

namespace AngularStub.Web.Areas.HelpPage.Controllers
{
    /// <summary>
    ///     The controller that will handle requests for the help page.
    /// </summary>
    public class HelpController : Controller
    {
        private const string ErrorViewName = "Error";

        public HelpController()
            : this(GlobalConfiguration.Configuration)
        {
        }

        public HelpController(HttpConfiguration config)
        {
            Configuration = config;
        }

        public HttpConfiguration Configuration { get; private set; }

        public ActionResult Index()
        {
            ViewBag.DocumentationProvider = Configuration.Services.GetDocumentationProvider();
            return View(Configuration.Services.GetApiExplorer().ApiDescriptions);
        }

        public ActionResult Api(string apiId)
        {
            if (String.IsNullOrEmpty(apiId)) return View(ErrorViewName);
            HelpPageApiModel apiModel = Configuration.GetHelpPageApiModel(apiId);
            return apiModel != null ? View(apiModel) : View(ErrorViewName);
        }

        public ActionResult ResourceModel(string modelName)
        {
            if (String.IsNullOrEmpty(modelName)) return View(ErrorViewName);
            ModelDescriptionGenerator modelDescriptionGenerator = Configuration.GetModelDescriptionGenerator();
            ModelDescription modelDescription;
            return modelDescriptionGenerator.GeneratedModels.TryGetValue(modelName, out modelDescription)
                ? View(modelDescription)
                : View(ErrorViewName);
        }
    }
}