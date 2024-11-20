using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BExIS.Modules.Fmt.UI.Controllers
{
    public class TechniciansFilesController : BaseFileManagementController
    {
        public ActionResult Index(string node_id = "")
        {
            return RedirectToAction("Show", "TechniciansFiles", new { area = "FMT", viewName = "TechniciansFiles", viewTitle = "Technicians Documents", rootMenu = "Technicians", node_id = node_id.Replace("\\", "_") });
        }
    }
}