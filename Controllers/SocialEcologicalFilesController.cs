using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BExIS.Modules.Fmt.UI.Controllers
{
    public class SocialEcologicalFilesController : BaseFileManagementController
    {
        public ActionResult Index(string node_id = "")
        {
            return RedirectToAction("Show", "SocialEcologicalFiles", new { area = "FMT", viewName = "SocialEcologicalFiles", viewTitle = "SocialEcological Documents", rootMenu = "SocialEcological", node_id = node_id.Replace("\\", "_") });
        }
    }
}