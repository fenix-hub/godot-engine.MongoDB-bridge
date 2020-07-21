<img src="addons/MongoDB/mongodb-bridge-icon.png" align="left" width="64" height="64">

# Godot Engine - MongoDB Bridge
**A MongoDB bridge written in C# for Godot Engine mono projects.**

## How does it work?
*MongoDB Bridge* is a collection of scripts written in C# which interface MongoDB APIs to work with a MongoDB database even with GDScript. 
To make this plugin work:   
1. [install MongoDB](https://www.mongodb.com/try/download/community) on your local machine (or remote machine),  
2. [install NuGet](https://www.nuget.org/downloads),  
3. install this repository as a common Godot Engine addon, using the AssetLibrary or just cloning this repo in your **mono** project,  
4. edit your `.csproj`, adding after the line including `<Reference Include="System" />` the following:
```
<PackageReference Include="MongoDB.Driver">
  <Version>2.10.4</Version>
</PackageReference>
<PackageReference Include="MongoDB.Driver.Core">
  <Version>2.10.4</Version>
</PackageReference>
<PackageReference Include="MongoDB.Bson">
  <Version>2.10.4</Version>
</PackageReference>
```
5. use the command `nuget restore` via a command line at your choice  
<br/>
<img src="https://cdn.iconscout.com/icon/free/png-512/mongodb-4-1175139.png" align="left" width="32" height="32">

## What is MongoDB?
*MongoDB* is a document database, which means it stores data in JSON-like documents called **BSON**.
[BSON](https://docs.mongodb.com/manual/reference/bson-types/) is a binary serialization format used to store documents and make remote procedure calls in MongoDB. The BSON specification is located at [bsonspec.org](bsonspec.org).
> Read more aboute MongoDB [here](https://docs.mongodb.com/manual/introduction/).  
<br/>
<img src="https://upload.wikimedia.org/wikipedia/commons/thumb/2/25/NuGet_project_logo.svg/512px-NuGet_project_logo.svg.png" align="left" width="32" height="32">

## What is NuGet and why do I need it?
*NuGet* is the package manager for .NET. The NuGet client tools provide the ability to produce and consume packages. The NuGet Gallery is the central package repository used by all package authors and consumers.  
NuGet is required to correctly download and implement dipendencies in C# projects just referencing them in the `.csproj` file with a `<PackageReference>`.
<br/>
## Why can I only use this plugin with the Mono version?
Even though Godot Engine currently supports [Cross-Language Scripting](https://docs.godotengine.org/it/stable/getting_started/scripting/cross_language_scripting.html) without the need of a .mono project, .cs files in this addon intensively rely on `MongoDB` Packages.  
In order to compile these packages and build the project, a mono project is required. Otherwise, a C++ module implementing MongoDB APIs should be compiled, but that's not this case and it is not what I'm currently working on.  
But **note**, this doesn't mean you need to write your whole project in C# if you want to use MongoDB APIs: this is why I made this brigde, indeed.  
*MongoDB Bridge* lets you write your whole project in GDScript without caring too much about C# stuff, and still access to all main functionalities you would have directly working with [MongoDB C# Drivers](https://docs.mongodb.com/drivers/).


