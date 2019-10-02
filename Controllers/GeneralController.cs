﻿
using BExIS.Modules.Fmt.UI.Controllers;
using System.Web.Mvc;

namespace BExIS.Modules.FMT.UI.Controllers
{
    public class GeneralController : BaseFileManagementController
    {
        public ActionResult Index(string node_id = "")
        {
            return RedirectToAction("Show", "General", new {area="FMT", viewName= "General", rootMenu= "BeoInformation", node_id = node_id.Replace("\\", "_") });
        }
    }


}