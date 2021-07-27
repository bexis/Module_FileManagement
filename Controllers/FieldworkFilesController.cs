using System.Web.Mvc;
using Vaiona.Web.Extensions;
using Vaiona.Web.Mvc.Models;

namespace BExIS.Modules.Fmt.UI.Controllers
{
    public class FieldworkFilesController : BaseFileManagementController
    {
        public ActionResult Index(string node_id = "")
        {
            ViewBag.Title = PresentationModel.GetViewTitleForTenant("BExIS - Fieldwork Documents", this.Session.GetTenant());

            return RedirectToAction("Show", "FieldworkFiles", new { area = "FMT", viewName = "FieldworkFiles", viewTitle = "BExIS - Fieldwork Documents", rootMenu = "Fieldwork", node_id = node_id.Replace("\\", "_") });
        }
    }
}