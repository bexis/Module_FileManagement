using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BExIS.Modules.Fmt.UI.Controllers
{
    public class PostDocFilesController : BaseFileManagementController
    {
        public ActionResult Index(string node_id = "")
        {
            return RedirectToAction("Show", "PostDocFiles", new { area = "FMT", viewTitle = "PostDoc Documents", viewName = "PostDocFiles", rootMenu = "PostDoc", node_id = node_id.Replace("\\", "_") });
        }
    }
}