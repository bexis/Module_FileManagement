using BExIS.IO;
using BExIS.Modules.FMT.UI.Models;
using BExIS.Security.Entities.Subjects;
using BExIS.Security.Services.Subjects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using Vaiona.Utils.Cfg;

namespace BExIS.Modules.FMT.UI.Helper
{
    public class MenuHelper
    {
        private XmlDocument GetMenuXmlDoc()
        {
            string FMTPath = Path.Combine(AppConfiguration.DataPath, "FMT");
            if (!Directory.Exists(FMTPath))
                Directory.CreateDirectory(FMTPath);
            string menuConfigPath = Path.Combine(FMTPath, "Menu-Config.xml");
            XDocument xDoc = null;// 
            XmlDocument xmlDoc = new XmlDocument();
            //Create config file from folders if there isn't
            if (!Vaiona.Utils.IO.FileHelper.CanReadFile(menuConfigPath))
            {
                var menuItemNodes = CreateXmlConfigByDirectories(new DirectoryInfo(FMTPath));
                menuItemNodes.Save(menuConfigPath);
                xDoc = XDocument.Load(menuConfigPath);
                xmlDoc.Load(xDoc.CreateReader());
            }
            else
            {
                //each time create path by config file
                xDoc = XDocument.Load(menuConfigPath);
                xmlDoc.Load(xDoc.CreateReader());
                foreach (XmlNode node in xmlDoc.SelectNodes("/Item"))
                        CreateDirectoriesByXmlConfig(node, FMTPath);
            }

            return xmlDoc;
        }

        internal bool HasUserAccessRights(string root, string userName)
        {
            XmlDocument xmlDoc = GetMenuXmlDoc();
            string roleName;
            try
            {
                roleName = xmlDoc.SelectSingleNode(string.Format("//Items[@Name='{0}']", root)).Attributes.GetNamedItem("Group").Value;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (!String.IsNullOrEmpty(roleName))
            {
                using (UserManager userManager = new UserManager())
                {
                    var userTask = userManager.FindByNameAsync(userName);
                    userTask.Wait();
                    var user = userTask.Result;

                    if (user.Groups.Select(a => a.Name).Contains(roleName))
                        return true;
                    else
                        return false;
                }
            }
            else
                return false;

           
        }

        internal List<FMTMenuItem> GetMenu(string root)
        {
            List<FMTMenuItem> menuItems = null;

            XmlDocument xmlDoc = GetMenuXmlDoc();
            string FMTPath = Path.Combine(AppConfiguration.DataPath, "FMT");

            //create menuItems list
            var xmlNodeList = xmlDoc.SelectNodes(string.Format("//Items[@Name='{0}']", root));
            if (xmlNodeList.Count == 0)
                xmlNodeList = xmlDoc.SelectNodes("/Items");
            if (xmlNodeList.Count > 0)
            {
                menuItems = new List<FMTMenuItem>();
                string path = FMTPath + @"\\" + root;
                foreach (var xmlMenuItem in GetMenuItems(xmlNodeList[0], path).MenuItems)
                {
                    menuItems.Add(xmlMenuItem);
                }
            }
            return menuItems;
        }

        internal void AppendToRootMenu(XElement newXElement)
        {
            string FMTPath = Path.Combine(AppConfiguration.DataPath, "FMT");
            string menuConfigPath = Path.Combine(FMTPath, "Menu-Config.xml");
            var xElement = XElement.Load(menuConfigPath);// 
            //looking for the same item and remove it if exist
            var newItemName = newXElement.Attribute("Name").Value;
            var lastXElement = xElement.Elements().FirstOrDefault(item => item.Attribute("Name").Value == newItemName);
            if (lastXElement != null)
                 lastXElement.Remove();
            xElement.Add(newXElement);
            xElement.Save(menuConfigPath);
        }

        FMTMenuItem GetMenuItems(XmlNode xmlNode, string directory)
        {
            if (xmlNode.Name != "Item" && xmlNode.Name != "Items")
                return null;
            var item = new FMTMenuItem();
            item.Name = xmlNode.Attributes["Name"].Value;
            item.Path = GetFMTMenuItemRelativePath(directory);
            item.Title = Regex.Replace(xmlNode.Attributes["Title"].Value, "((?<=[a-z])[A-Z]|[A-Z](?=[a-z]))", " $1");
            item.MenuItems = new List<FMTMenuItem>();
            foreach (XmlNode childXmlNode in xmlNode.ChildNodes)
                item.MenuItems.Add(GetMenuItems(childXmlNode, directory + @"\\" + childXmlNode.Attributes["Name"].Value));
            return item;
        }

        string GetFMTMenuItemRelativePath(String FMTMenuItemPath)
        {
            var relativePath = FMTMenuItemPath.Replace(AppConfiguration.DataPath, "");
            relativePath = relativePath.TrimStart('\\');
            return relativePath;
        }

        internal XElement CreateXmlConfigByDirectories(DirectoryInfo directory)
        {
            var directories = directory.GetDirectories();
            var xmlNode = new XElement("Item");
            if (directories.Any())
                xmlNode.Name = "Items";
            xmlNode.SetAttributeValue("Name", directory.Name);
            xmlNode.SetAttributeValue("Title", directory.Name);
            foreach (var subDirectory in directories)
                if (subDirectory.Name != "Deleted Files")
                    xmlNode.Add(CreateXmlConfigByDirectories(subDirectory));
            return xmlNode;
        }


        FMTMenuItem CreateDirectoriesByXmlConfig(XmlNode xmlNode, string directory)
        {

            if (xmlNode.Name != "Item" && xmlNode.Name != "Items")
                return null;
            var item = new FMTMenuItem();
            // var newPath = Path.Combine(directory, xmlNode.Attributes["Name"].Value);
            item.Path = GetFMTMenuItemRelativePath(directory);
            item.Name = xmlNode.Attributes["Name"].Value;
            item.Title = xmlNode.Attributes["Title"].Value;

            string filenameOnly = System.IO.Path.GetFileName(directory);

            if (filenameOnly.Length==0)
                Directory.CreateDirectory(directory);

            item.MenuItems = new List<FMTMenuItem>();
            foreach (XmlNode childXmlNode in xmlNode.ChildNodes)
            {
                item.MenuItems.Add(CreateDirectoriesByXmlConfig(childXmlNode, Path.Combine(directory, childXmlNode.Attributes["Name"].Value)));
            }
            return item;
        }
        public XmlElement XElementToXmlElement(XElement el)
        {
            var doc = new XmlDocument();
            doc.Load(el.CreateReader());
            return doc.DocumentElement;
        }
      
    }
    
}
