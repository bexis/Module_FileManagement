using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BExIS.Modules.Fmt.UI.Controllers
{
    public class PhDFilesController : BaseFileManagementController
    {
        public ActionResult Index(string node_id = "")
        {
            return RedirectToAction("Show", "PhDFiles", new { area = "FMT", viewName = "PhDFiles", viewTitle = "PhD Documents", rootMenu = "PhD", node_id = node_id.Replace("\\", "_") });
        }
    }
}