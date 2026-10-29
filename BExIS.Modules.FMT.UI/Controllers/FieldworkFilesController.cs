using System.Web.Mvc;

namespace BExIS.Modules.Fmt.UI.Controllers
{
    public class FieldworkFilesController : BaseFileManagementController
    {
        public ActionResult Index(string node_id = "")
        {
            return RedirectToAction("Show", "FieldworkFiles", new { area = "FMT", viewName = "FieldworkFiles", viewTitle = "Fieldwork Documents", rootMenu = "Fieldwork", node_id = node_id.Replace("\\", "_") });
        }
    }
}