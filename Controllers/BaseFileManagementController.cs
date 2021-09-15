using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BExIS.Modules.FMT.UI.Models;
using BExIS.Modules.FMT.UI.Helper;
using BExIS.Security.Services.Authorization;
using BExIS.Security.Services.Subjects;
using BExIS.Security.Entities.Objects;
using BExIS.Security.Services.Objects;
using Vaiona.Utils.Cfg;
using System.IO;
using Vaiona.Web.Mvc.Models;
using Vaiona.Web.Extensions;

namespace BExIS.Modules.Fmt.UI.Controllers
{
    public class BaseFileManagementController : Controller
    {

        public ActionResult Show(string viewName, string rootMenu, string viewTitle )
        {
            ViewBag.Title = PresentationModel.GetViewTitleForTenant(viewTitle, this.Session.GetTenant());
            bool hasAdminRights = false;
            using (UserManager userManager = new UserManager())
            using (FeaturePermissionManager featurePermissionManager = new FeaturePermissionManager())
            using (FeatureManager featureManager = new FeatureManager())
            {
                var user = userManager.FindByNameAsync(HttpContext.User.Identity.Name).Result;
                var feature = featureManager.FindByName(viewName + "Admin");
                hasAdminRights =  featurePermissionManager.HasAccess(user.Id, feature.Id);
            }

            if (String.IsNullOrEmpty(rootMenu))
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
                    ModelState.AddModelError("Error", "No access rights for this menu and this page!");
                else
                   menus = menuHelper.GetMenu(rootMenu, userName);


            //if (string.IsNullOrEmpty(rootMenu))
            ViewBag.UseLayout = true;
            ViewData["AdminRights"] = hasAdminRights;
            

            return View(viewName, menus);
        }


        public ActionResult GetFileLists(string menuItemPath, string contollerName)
        {
            //string menuItem = new DirectoryInfo(menuItemPath).Name;

            bool hasDeleteRights = false;
            //check user permissions for delete
            using (var featurePermissionManager = new FeaturePermissionManager())
            using (var featureManager = new FeatureManager())
            using (UserManager userManager = new UserManager())
            {

                var userTask = userManager.FindByNameAsync(GetUsernameOrDefault());
                userTask.Wait();
                var user = userTask.Result;
                List<Feature> features = featureManager.FeatureRepository.Get().ToList();
                Feature feature = features.FirstOrDefault(f => f.Name.Equals(contollerName + "Admin"));
                if (feature != null)
                {
                    if (featurePermissionManager.HasAccess(user.Id, feature.Id))
                    {
                        hasDeleteRights = true;
                    }
                }

                var fileModelList = FileModel.GetFileModelList(menuItemPath, hasDeleteRights);
                fileModelList.ForEach(a => a.controllerName = contollerName);


                return PartialView("~/Areas/FMT/Views/Shared/_fileList.cshtml", fileModelList);
            }
            
        }

        public ActionResult DownloadFile(string path, string mimeType)
        {

            string title = path.Split('\\').Last();
            string message = string.Format("file was downloaded");
            string userName = GetUsernameOrDefault();
            Vaiona.Logging.LoggerFactory.LogCustom(message);

            string message_mail = $"Dataset <b>\"{title}\"</b> with id was downloaded";


            if (!string.IsNullOrEmpty(userName))
            {
                message_mail += $" by <b>{userName}</b>";
            }

            message_mail = message_mail + ".";

            //var es = new Security.Services.Utilities.EmailService();
            //    es.Send(MessageHelper.GetDownloadDatasetHeader(),message_mail, ConfigurationManager.AppSettings["SystemEmail"]);

            string folderpath = "";
            folderpath = BExIS.Modules.FMT.UI.Helper.Settings.get("SourcePathToFiles").ToString();
            if (String.IsNullOrEmpty(path))
                folderpath = AppConfiguration.DataPath;

            return File(Path.Combine(folderpath, path), mimeType, title);
        }

        public string GetUsernameOrDefault()
        {
            var username = string.Empty;
            try
            {
                username = HttpContext.User.Identity.Name;
            }
            catch { }

            return !string.IsNullOrWhiteSpace(username) ? username : "DEFAULT";
        }
    }
}