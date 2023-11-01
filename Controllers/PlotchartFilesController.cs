using System.Web.Mvc;

namespace BExIS.Modules.Fmt.UI.Controllers
{
    public class PlotchartFilesController : BaseFileManagementController
    {
        public ActionResult Index(string node_id = "")
        {
            return RedirectToAction("Show", "PlotchartFiles", new { area = "FMT", viewName = "PlotchartFiles", viewTitle = "Plotchart Documents", rootMenu = "PlotchartPackages", node_id = node_id.Replace("\\", "_") });
        }
    }
}