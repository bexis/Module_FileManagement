using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Mvc;
using Vaiona.Utils.Cfg;
using System.Web.UI;
using BExIS.Modules.FMT.UI.Helper;
using Shell32;

namespace BExIS.Modules.FMT.UI.Models

{
    //public class InfoPageModel
    //{
    //    public FileModel Files { get; set; }
    //    public List<FileModel> FileList { get; set;}



    //    public InfoPageModel()
    //    {
    //      FileList = new List<FileModel>();
    //      Files = new FileModel();

    //    }
    //}

    public class FileModel
    {

        public string Filename { get; set; }
        public string Filepath { get; set; }
        public string MimeType { get; set; }
        public byte[] FileContent { get; set; }
        public DateTime Date { get; set; }
        public long Length { get; set; }
        public bool HasRights { get; set; }

        public FileModel()
        {
        }

        public FileModel(FileInfo file, string dir, FileContentResult filecontent, bool hasRights)
        {
            Filename = file.Name;
            MimeType = MimeMapping.GetMimeMapping(file.Name);
            Filepath = dir + file.Name;
            FileContent = filecontent.FileContents;
            HasRights = hasRights;

        }

        internal static List<FileModel> GetFileModelList(String FMTMenuItemPath, bool hasRights)
        {
            string folderpath = "";
            folderpath = Helper.Settings.get("SourcePathToFiles").ToString();
            if (String.IsNullOrEmpty(folderpath))
                folderpath = AppConfiguration.DataPath;

            var fileModels = new List<FileModel>();
            foreach (var file in Directory.GetFiles(Path.Combine(folderpath, FMTMenuItemPath)))
            {
                var fileName = Path.GetFileName(file);
                var filepath = FMTMenuItemPath + @"\\" + fileName;

                fileModels.Add(new FileModel() { Filename = fileName, Filepath = filepath, MimeType = MimeMapping.GetMimeMapping(file), Date = File.GetCreationTime(Path.Combine(folderpath, filepath)), Length = file.Length, HasRights = hasRights });
            }
            return fileModels;
        }

    }
}