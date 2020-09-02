# File Management Module


1. [Features](#Features)
2. [Installtion](#install)
3. [How to use / Workflow](#how_to)
4. [Dependencies](#depend)



## Installation <a name="install"></a>

- Edit XML file in Workspace/FMT/Menu-Config.xml which describes the dirctory struture

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


## Dependencies to other Modules

BExIS.IO ...
