
using BExIS.Modules.Fmt.UI.Controllers;
using System.Web.Mvc;

namespace BExIS.Modules.FMT.UI.Controllers
{
    public class InfoPageController : BaseFileManagementController
    {
        public ActionResult Index(string node_id = "")
        {
            return RedirectToAction("Show", "InfoPage", new {area="FMT", viewName= "InfoPage", rootMenu= "BeoInformation", node_id = node_id.Replace("\\", "_") });
        }
    }


}