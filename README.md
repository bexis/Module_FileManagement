# File Management Module


1. [Features](#Features)
2. [How to use / Workflow](#how_to)
3. [Installtion](#install)
4. [Dependencies](#depend)



## Installation <a name="install"></a>
- edit XML file in Workspace FMT/Menu-Config.xml which describes the dirctory struture

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

- adjust file ... and ...

## Dependencies to other Modules

BExIS.IO ...
