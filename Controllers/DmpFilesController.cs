<<<<<<< Updated upstream
using System;
=======
﻿using System;
>>>>>>> Stashed changes
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BExIS.Modules.Fmt.UI.Controllers
{
    public class DmpFilesController : BaseFileManagementController
    {
        public ActionResult Index(string node_id = "")
        {
            return RedirectToAction("Show", "DmpFiles", new { area = "FMT", viewName = "DmpFiles", viewTitle = "Dmp Documents", rootMenu = "DMPs", node_id = node_id.Replace("\\", "_") });
        }
    }
}