using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vaiona.Utils.Cfg;
using Vaiona.Web.Mvc.Modularity;

namespace BExIS.Modules.Fmt.UI.Controllers
{
    public class BaseAdminFileManagementController : Controller
    {
        public ActionResult GetFileUploader(string menuItemPath, string contollerName)
        {
            ViewBag.FilePath = menuItemPath;
            Session["Controller"] = contollerName;
            return PartialView("~/Areas/FMT/Views/Shared/_uploadFile.cshtml");
        }


        public ActionResult SubmitFile(HttpPostedFileBase[] SelectFileUploaders, string filePath)
        {
            string folderpath = "";
            var settings = ModuleManager.GetModuleSettings("fmt");
            folderpath = settings.GetValueByKey("SourcePathToFiles").ToString();

            if (String.IsNullOrEmpty(folderpath))
                folderpath = AppConfiguration.DataPath;

            foreach (var SelectFileUploader in SelectFileUploaders)
            {
                string newFilePath = Path.Combine(folderpath, filePath, SelectFileUploader.FileName);
                SelectFileUploader.SaveAs(newFilePath);
            }
            string controller = Session["Controller"].ToString();
            controller = controller.Replace("Admin", "");
            Session["Controller"] = null;

            return RedirectToAction("Index", controller, new { node_id = filePath });
        }

        /// <summary>
        /// Delete file from the folder, but peserve (move) it to the folder "Deleted Files". The moved file will be renamed, if a file with the same name already exists.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public ActionResult DeleteFile(string filePath)
        {
            string folderpath = "";
            var settings = ModuleManager.GetModuleSettings("fmt");
            folderpath = settings.GetValueByKey("SourcePathToFiles").ToString();
            if (String.IsNullOrEmpty(folderpath))
                folderpath = AppConfiguration.DataPath;


            // Create directory for deleted files if not exists
            var deletedFilePath = Path.Combine(folderpath, "FMT\\Deleted Files");

            BExIS.IO.FileHelper.CreateDicrectoriesIfNotExist(deletedFilePath);

            var des = deletedFilePath + "\\" + Path.GetFileName(filePath);
            
            // Check if file already exists in the "Deleted Files" folder and rename the file if yes.
            if (System.IO.File.Exists(des)){
                des = deletedFilePath + "\\" + new Random().Next(1, 1000) + "_" + Path.GetFileName(filePath);
            }
            
            // Move file from source to "Deleted Files" folder
            System.IO.File.Move(Path.Combine(folderpath, filePath), des);

            return null;
        }

      

        public string GetUsernameOrDefault()
        {
            string username = string.Empty;
            try
            {
                username = HttpContext.User.Identity.Name;
            }
            catch { }

            return !string.IsNullOrWhiteSpace(username) ? username : "DEFAULT";
        } 
    }
}