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
            return RedirectToAction("Index", controller, new { area =""});
        }


        public ActionResult DeleteFile(string filePath)
        {
            //should to set last path in file attribute
            // File.SetAttributes(filePath, FileAttributes.Normal);
            var deletedFilePath = Path.Combine(AppConfiguration.DataPath, "FMT\\Deleted Files");
            Directory.CreateDirectory(deletedFilePath);

            var des = deletedFilePath + "\\" + Path.GetFileName(filePath);
            //to prevent of error and rewriting I add something to file name if it exists
            if (FileHelper.CanReadFile(deletedFilePath + "\\" + Path.GetFileName(filePath)))
                des = deletedFilePath + "\\" + new Random().Next(1, 100) + "_" + Path.GetFileName(filePath);
            string path = AppConfiguration.DataPath + @"\" + filePath;
            path = path.Replace(@"\\", @"\");
            Vaiona.Utils.IO.FileHelper.MoveAndReplace(path, des);
            return null;
        }
    }
}