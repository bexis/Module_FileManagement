using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BExIS.Modules.Fmt.UI.Controllers
{
    public class DrivingDirectionsFilesController : BaseFileManagementController
    {
        // GET: DrivingDirectionsFiles
        public ActionResult Index(string node_id = "")
        {
            return RedirectToAction("Show", "DrivingDirectionsFiles", new { area = "FMT", viewName = "DrivingDirectionsFiles", viewTitle = "Driving Directions Documents", rootMenu = "DrivingDirections", node_id = node_id.Replace("\\", "_") });
        }
    }
}