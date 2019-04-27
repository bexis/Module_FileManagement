using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Mvc;
using Vaiona.Utils.Cfg;
using BExIS.Modules.FMT.UI.Helper;

namespace BExIS.Modules.FMT.UI.Models

{
    public class InfoPageModel
    {
        public FileModel Files { get; set; }
        public List<FileModel> FileList { get; set;}
       
        

        public InfoPageModel()
        {
          FileList = new List<FileModel>();
          Files = new FileModel();
          
        }
    }

    public class FileModel
    {

        public string Filename { get; set; }
        public string Filepath { get; set; }
        public string MimeType { get; set; }
        public byte[] FileContent { get;set; }
        public DateTime Date { get; set; }
        public int Length{ get; set; }

        public FileModel()
        {
        }

        public FileModel(FileInfo file, string dir, FileContentResult filecontent)
        {
            Filename = file.Name;
            MimeType = MimeMapping.GetMimeMapping(file.Name);
            Filepath = dir + file.Name;
            FileContent = filecontent.FileContents;

            
        }

        internal static List<FileModel> GetFileModelList(String FMTMenuItemPath)
        {
            var fileModels = new List<FileModel>();
            foreach (var file in Directory.GetFiles(Path.Combine(AppConfiguration.DataPath,FMTMenuItemPath)))
            {
                var fileName = Path.GetFileName(file);
                var filepath = FMTMenuItemPath + @"\\" + fileName;

                fileModels.Add(new FileModel() { Filename = fileName, Filepath = filepath, MimeType = MimeMapping.GetMimeMapping(file),Date = File.GetCreationTime(Path.Combine(AppConfiguration.DataPath, filepath)) , Length = file.Length});
            }
            return fileModels;
        }
    }
}