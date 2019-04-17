using BExIS.Security.Entities.Objects;
using BExIS.Security.Services.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BExIS.Modules.Fmt.UI.Helper
{
    public class FmtSeedDataGenerator
    {
        public void GenerateSeedData()
        {
            OperationManager operationManager = new OperationManager();
            FeatureManager featureManager = new FeatureManager();

            try
            {
                List<Feature> features = featureManager.FeatureRepository.Get().ToList();

                Feature BeoInformation = features.FirstOrDefault(f => f.Name.Equals("Beo Information"));
                if (BeoInformation == null)
                    BeoInformation = featureManager.Create("Beo Information", "Beo Information");

                operationManager.Create("FMT", "BEOInformation", "*", BeoInformation);
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
    }
}