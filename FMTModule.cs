using BExIS.Modules.Fmt.UI.Helper;
using System;
using Vaiona.Logging;
using Vaiona.Web.Mvc.Modularity;

namespace BExIS.Modules.Fmt.UI
{
    public class FMTModule : ModuleBase
    {
        public FMTModule(): base("FMT")
        {
        }


        public override void Install()
        {
            LoggerFactory.GetFileLogger().LogCustom("... start install of FMT ...");
            try
            {
                base.Install();
                using (var fmtSeedDataGenerator = new FmtSeedDataGenerator())
                {
                    fmtSeedDataGenerator.GenerateSeedData();
                }
            }
            catch (Exception e)
            {
                LoggerFactory.GetFileLogger().LogCustom(e.Message);
                LoggerFactory.GetFileLogger().LogCustom(e.StackTrace);
            }

            LoggerFactory.GetFileLogger().LogCustom("... end install of FMT ...");
        }
    }
}
