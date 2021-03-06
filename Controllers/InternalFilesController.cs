﻿using System.Web.Mvc;

namespace BExIS.Modules.Fmt.UI.Controllers
{
    public class InternalFilesController : BaseFileManagementController
    {
        public ActionResult Index(string node_id = "")
        {
            return RedirectToAction("Show", "InternalFiles", new { area = "FMT", viewTitle = "Internal Documents",  viewName = "InternalFiles", rootMenu = "Internal", node_id = node_id.Replace("\\", "_") });
        }
    }
}