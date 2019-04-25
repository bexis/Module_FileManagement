using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vaiona.Utils.Cfg;
using Vaiona.Utils.IO;

namespace BExIS.Modules.Fmt.UI.Controllers
{
    public class BaseAdminFileManagementController : Controller
    {
        public ActionResult GetFileUploader(string menuItemPath, string cont)
        {
            ViewBag.FilePath = menuItemPath;
            Session["Controller"] = cont;
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
    }
}