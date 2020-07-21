<img src="addons/MongoDB/mongoDB-icon.png" align="left" width="64" height="64">

# MongoDB Bridge
**A MongoDB bridge written in C# for Godot Engine mono projects.**

## How does it work?
*MongoDB Bridge* is a collection of scripts written in C# which interface MongoDB APIs to work with a MongoDB database even with GDScript. 
To make this plugin work:   
1. [install MongoDB](https://www.mongodb.com/try/download/community) on your local machine (or remote machine),  
2. [install nuget](https://www.nuget.org/downloads),  
3. install this repository as a common Godot Engine addon, using the AssetLibrary or just cloning this repo in your **mono** project,  
4. edit your `.csproj`, adding after the line including `<Reference Include="System" />` the following:
```
<ItemGroup>
  [...]
  <PackageReference Include="MongoDB.Driver">
    <Version>2.10.4</Version>
  </PackageReference>
  <PackageReference Include="MongoDB.Driver.Core">
    <Version>2.10.4</Version>
  </PackageReference>
  <PackageReference Include="MongoDB.Bson">
    <Version>2.10.4</Version>
  </PackageReference>
  [...]
</ItemGroup>
```
5. use the command `nuget restore` via a command line at your choice
