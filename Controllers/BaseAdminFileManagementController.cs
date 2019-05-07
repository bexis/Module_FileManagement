using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vaiona.Utils.Cfg;

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
            foreach (var SelectFileUploader in SelectFileUploaders)
            {
                string newFilePath = Path.Combine(AppConfiguration.DataPath, filePath, SelectFileUploader.FileName);
                SelectFileUploader.SaveAs(newFilePath);
            }
            string controller = Session["Controller"].ToString();
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
  
            // Create directory for deleted files if not exists
            var deletedFilePath = Path.Combine(AppConfiguration.DataPath, "FMT\\Deleted Files");
            BExIS.IO.FileHelper.CreateDicrectoriesIfNotExist(deletedFilePath);

            var des = deletedFilePath + "\\" + Path.GetFileName(filePath);
            
            // Check if file already exists in the "Deleted Files" folder and rename the file if yes.
            if (System.IO.File.Exists(des)){
                des = deletedFilePath + "\\" + new Random().Next(1, 1000) + "_" + Path.GetFileName(filePath);
            }
            
            // Move file from source to "Deleted Files" folder
            System.IO.File.Move(Path.Combine(AppConfiguration.DataPath, filePath), des);

            return null;
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

            return File(Path.Combine(AppConfiguration.DataPath, path), mimeType, title);
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