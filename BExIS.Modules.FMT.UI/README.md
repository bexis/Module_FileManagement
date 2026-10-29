# File Management Module


1. [Features](#Features)
2. [Installtion](#install)
3. [Dependencies](#depend)

## Features <a name="Features"></a>

- Manage files in BEXIS2#
- Upload, download and delete files
- Create your own folder where the files are stored structure
- Use it on different pages

## Installation <a name="install"></a>

For each page, 2 controllers and 1 views are needed in the backend.
To have different user access rights options on different pages with file management you need for every site a 2 controllers and a view. One for download files and one to administrate files (upload and delete). The site Controllers must inherit from BaseFileManagementController or BaseAdminFileManagementController.
This way is chosen because BExIS2 security works only on controller level.

![[Screenshot Controller]](https://github.com/bexis/Module_FileManagement/blob/dev/Docs/Manual/controller_class_diagram.jpg)


- Edit XML file in Workspace/FMT/Menu-Config.xml which describes the dirctory struture
- In the config files it is also possible to assign certain groups to the individual tree branches of the file structure. For this purpose, the BEXIS2 groups are entered comma-separated in the "Group" attribute. 

 ```XML
<Item Name="FileManagement" Title="FileManagement">
  <Items Name="menu1" Title="menu1" Group="groupwithpermission">
    <Items Name="submenu1" Title="submenu1"/> 
    <Items Name="submenu2" Title="submenu2">
       <Items Name="subsubmenu1" Title="subsubmenu1"/>
       <Items Name="subsubmenu2" Title="subsubmenu2"/> 
    </Items>
  </Items>
    <Items Name="menu2" Title="menu2" Group="groupwithpermission2">
     ...
     ...
    </Items>
  <Items Name="menu3" Title="menu3" Group="groupwithpermission3"/>
  ...
  ...
</Items>
 ```
 
 - Set real path to FMT listed files in Workspace/FMT/Fmt.Settings.xml. We need this because sometimes shortcuts to real files in the default Data folder. The system canot read shortcut.
 
  ```XML
<!--Path to source shortcut-->
  <entry key="SourcePathToFiles" value="C:\Data\Temp\FMT_Temp" type="string"/>
  ```
 
 - make sure that for all FMT views the admin folder in Views exsits if you are publish the module
 



## Dependencies to other Modules <a name="depend"></a>

- BExIS.Modules.FMT.UI.Models;
- BExIS.Modules.FMT.UI.Helper;
- BExIS.Security.Services.Authorization;
- BExIS.Security.Services.Subjects;
- BExIS.Security.Entities.Objects;
- BExIS.Security.Services.Objects;
- Vaiona.Utils.Cfg;
- Vaiona.Web.Mvc.Models;
- Vaiona.Web.Extensions
