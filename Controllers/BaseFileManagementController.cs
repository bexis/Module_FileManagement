using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BExIS.Modules.FMT.UI.Models;
using BExIS.Modules.FMT.UI.Helper;

namespace BExIS.Modules.Fmt.UI.Controllers
{
    public class BaseFileManagementController : Controller
    {

        public ActionResult Index(string viewName, string rootMenu = "")
        {
            MenuHelper menuHelper = new MenuHelper();
            var menus = menuHelper.GetMenu(rootMenu);
            //if (string.IsNullOrEmpty(rootMenu))

            ViewBag.UseLayout = true;

            return View(viewName, menus);
        }


        public ActionResult GetFileLists(string menuItemPath)
        {
            var fileModelList = FileModel.GetFileModelList(menuItemPath);
            return PartialView("~/Areas/FMT/Views/Shared/_fileList.cshtml", fileModelList);
        }


    }
}