using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BExIS.Modules.Fmt.UI.Controllers
{
    public class FieldworkFilesController : BaseFileManagementController
    {
        public ActionResult Index(string node_id = "")
        {
            return RedirectToAction("Show", "Fieldwork", new { area = "FMT", viewName = "Fieldwork", rootMenu = "Fieldwork", node_id = node_id.Replace("\\", "_") });
        }
    }
}