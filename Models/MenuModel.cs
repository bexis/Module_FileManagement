using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BExIS.Modules.FMT.UI.Models
{
    public class FMTMenuItem
    {
        public string Title { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public List<FMTMenuItem> MenuItems = new List<FMTMenuItem>();
    }
}