using BExIS.Security.Entities.Objects;
using BExIS.Security.Services.Objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Vaiona.Utils.Cfg;
using Vaiona.Web.Mvc.Modularity;

namespace BExIS.Modules.Fmt.UI.Helper
{
    public class FmtSeedDataGenerator : IModuleSeedDataGenerator
    {
        public void GenerateSeedData()
        {
            OperationManager operationManager = new OperationManager();
            FeatureManager featureManager = new FeatureManager();

            try
            {
                List<Feature> features = featureManager.FeatureRepository.Get().ToList();

                Feature FileManagement = features.FirstOrDefault(f => f.Name.Equals("File Management"));
                if (FileManagement == null)
                    FileManagement = featureManager.Create("File Management", "File Management");

                List<string> controllers = GetController();
                foreach(string c in controllers)
                {
                    Feature feature = features.FirstOrDefault(f => f.Name.Equals(c));
                    if (feature == null)
                        feature = featureManager.Create(c, c, FileManagement);

                    operationManager.Create("FMT", c, "*", feature);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                operationManager.Dispose();
                featureManager.Dispose();
            }
        }

        private List<string> GetController()
        {
            List<string> controllers = new List<string>();

            string path = Path.Combine(AppConfiguration.AppRoot, @"Areas\FMT\Controllers");
            string[] fileEntries = Directory.GetFiles(path);

            foreach (string r in fileEntries)
            {
                string file = Path.GetFileName(r);
                if (file.StartsWith("Base"))
                    continue;
                else
                {
                    string[] temp = Regex.Split(file, "Controller");
                    controllers.Add(temp[0]);
                }

            }
            return controllers;
        }

        public void Dispose()
        {
        }
    }
}