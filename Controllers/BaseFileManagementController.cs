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

        public ActionResult Index(string viewName, string rootMenu)
        {

            if(String.IsNullOrEmpty(rootMenu))
                ModelState.AddModelError("Error", "Please enter a menu root to the url!");
            if (String.IsNullOrEmpty(viewName))
                ModelState.AddModelError("Error", "Please enter a view name to the url!");

           
                MenuHelper menuHelper = new MenuHelper();
                string userName = HttpContext.User.Identity.Name;
                List<FMTMenuItem> menus = null;

                    bool hasUserRights = false;
                if (userName != "" && rootMenu != "")
                        hasUserRights = menuHelper.HasUserAccessRights(rootMenu, userName);

                if (!hasUserRights)
                    ModelState.AddModelError("Error", "No access rights for this page!");
                else
                   menus = menuHelper.GetMenu(rootMenu);


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