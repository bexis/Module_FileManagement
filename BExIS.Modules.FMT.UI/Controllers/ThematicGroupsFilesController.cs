using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BExIS.Modules.Fmt.UI.Controllers
{
    public class ThematicGroupsFilesController : BaseFileManagementController
    {
        public ActionResult Index(string node_id = "")
        {
            return RedirectToAction("Show", "ThematicGroupsFiles", new { area = "FMT", viewName = "ThematicGroupsFiles", viewTitle = "ThematicGroups Documents", rootMenu = "ThematicGroups", node_id = node_id.Replace("\\", "_") });
        }
    }
}