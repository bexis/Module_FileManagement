
using BExIS.Modules.Fmt.UI.Controllers;
using System.Web.Mvc;

namespace BExIS.Modules.FMT.UI.Controllers
{
    public class GeneralFilesController : BaseFileManagementController
    {
        public ActionResult Index(string node_id = "")
        {
            return RedirectToAction("Show", "GeneralFiles", new {area="FMT", viewName= "GeneralFiles", rootMenu= "BeoInformation", node_id = node_id.Replace("\\", "_") });
        }
    }


}