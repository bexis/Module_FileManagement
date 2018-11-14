using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using Vaiona.Utils.Cfg;
using BExIS.IO;
using System.Xml;
using System.Xml.Linq;
using BExIS.Xml.Helpers;
using BExIS.Modules.FMT.UI.Helper;
using System.IO;
using System.Web.Mvc;
using Telerik.Web.Mvc;
using BExIS.Modules.FMT.UI.Models;
using Vaiona.Utils.IO;

namespace BExIS.Modules.FMT.UI.Controllers
{
    public class InfoPageController : Controller
    {
       
        public ActionResult Index(string rootMenu = "")
        {
            MenuHelper menuHelper = new MenuHelper();
            var menus = menuHelper.GetMenu(rootMenu);
            if (string.IsNullOrEmpty(rootMenu))
                ViewBag.UseLayout = true;
            return View("InfoPage", menus);
        }


        public ActionResult SubmitFile(HttpPostedFileBase[] SelectFileUploaders, string filePath)
        {
            foreach (var SelectFileUploader in SelectFileUploaders)
            {
                string newFilePath = Path.Combine(AppConfiguration.DataPath, filePath, SelectFileUploader.FileName);
                SelectFileUploader.SaveAs(newFilePath);
            }
            return RedirectToAction("Index");
        }

        public ActionResult GetFileUploader(string FMTMenuItemPath)
        {
            ViewBag.FilePath = FMTMenuItemPath;
            return PartialView("_uploadFile");
        }

        public ActionResult GetFileLists(string FMTMenuItemPath)
        {
            var fileModelList = FileModel.GetFileModelList(FMTMenuItemPath);
            return PartialView("_fileList", fileModelList);
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
                des = deletedFilePath + "\\"+new Random().Next(1,100)+"_" + Path.GetFileName(filePath);
            Vaiona.Utils.IO.FileHelper.MoveAndReplace(Path.Combine(AppConfiguration.DataPath, filePath),des);
            return null;
        }
    }


}