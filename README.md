<p align="right">
  <a href="https://discord.gg/JNrcucg">
    <img src="https://github.com/fenix-hub/ColoredBadges/blob/master/svg/social/discord.svg" alt="discord" style="vertical-align:top; margin:6px 4px">
  </a>
</p>

<img src="addons/MongoDB/mongodb-bridge-icon.png" align="left" width="80" height="80">

# Godot Engine - MongoDB Bridge
**A MongoDB bridge written in C# for Godot Engine mono projects.**  
- [How does it work?](https://github.com/fenix-hub/godot-engine.MongoDB-bridge#how-does-it-work)  
- [What is MongoDB?](https://github.com/fenix-hub/godot-engine.MongoDB-bridge#what-is-mongodb)  
- [What is NuGet and why do I need it?](https://github.com/fenix-hub/godot-engine.MongoDB-bridge#what-is-nuget-and-why-do-i-need-it)  
- [Why can I only use this plugin with the Mono version?](https://github.com/fenix-hub/godot-engine.MongoDB-bridge#why-can-i-only-use-this-plugin-with-the-mono-version?)  
- [Table of Features](https://github.com/fenix-hub/godot-engine.MongoDB-bridge#table-of-features)
- [Some examples](https://github.com/fenix-hub/godot-engine.MongoDB-bridge#some-examples)

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
<img src="https://cdn.iconscout.com/icon/free/png-512/mongodb-4-1175139.png" align="left" width="64" height="64">

## What is MongoDB?
*MongoDB* is a document database, which means it stores data in JSON-like documents called **BSON**.  
[BSON](https://docs.mongodb.com/manual/reference/bson-types/) is a binary serialization format used to store documents and make remote procedure calls in MongoDB.   
The BSON specification is located at [bsonspec.org](bsonspec.org).
> Read more aboute MongoDB [here](https://docs.mongodb.com/manual/introduction/).  
<br/>  

<img src="https://upload.wikimedia.org/wikipedia/commons/thumb/2/25/NuGet_project_logo.svg/512px-NuGet_project_logo.svg.png" align="left" width="64" height="64">

## What is NuGet and why do I need it?
*NuGet* is the package manager for .NET. The NuGet client tools provide the ability to produce and consume packages. The NuGet Gallery is the central package repository used by all package authors and consumers.  
NuGet is required to correctly download and implement dipendencies in C# projects just referencing them in the `.csproj` file with a `<PackageReference>`.
<br/>  
  
## :grey_question: Why can I only use this plugin with the Mono version?
Even though Godot Engine currently supports [Cross-Language Scripting](https://docs.godotengine.org/it/stable/getting_started/scripting/cross_language_scripting.html) without the need of a .mono project, .cs files in this addon intensively rely on `MongoDB` Packages.  
In order to compile these packages and build the project, a mono project is required. Otherwise, a C++ module implementing MongoDB APIs should be compiled, but that's not the case (<ins>even though it is something I'm working on</ins>).  
But **note**, this doesn't mean you need to write your whole project in C# if you want to use MongoDB APIs: this is why I made this bridge, indeed.  
*MongoDB Bridge* lets you write your whole project in GDScript without caring too much about C# stuff, and still access to all main functionalities you would have directly working with [MongoDB C# Drivers](https://docs.mongodb.com/drivers/).  
<br/>
  
<img src="https://cdn.icon-icons.com/icons2/2107/PNG/512/file_type_script_icon_130178.png" align="left" width="64" height="64">

## Table of Features   
| Class | Description |
| ------------- | ------------- |
|`MongoAPI`|Instance of the API to initialize the Bridge and connect to a MongoDB server|
|`MongoClient`|Instance of a client connected to a specific server, containing a set of databases|
|`MongoDatabase`|Instance of a single database containing multiple collections of documents|
|`MongoCollection`|Instance of a collection of BSON/JSON documents|
<br/>  

**MongoAPI**
| Method | Type | Description |
| ------------- | ------------- | ------------- |
|`Connect(hostIp : String)`|`MongoClient`|Connect to a server in order to retrieve a client. You can get the default `hostIp` with `MongoAPI.host`, which is `mongodb://127.0.0.1:27017`|
|`Connect(hostIp : String, checkSslCertificate : Bool)`|`MongoClient`|Connect to a server in order to retrieve a client. If `checkSslCertificate` is set to `false` mongodb will connect without verifying SSL certificates|
<br/>  

**MongoClient**
| Method | Type | Description |
| ------------- | ------------- | ------------- |
|`GetDatabaseList()`|`Array<Dictionary>`|Return an `Array` of different `Dictionary` containing all databases belonging to the `MongoClient` connected|
|`GetDatabaseNameList()`|`Array<String>`|Return an `Array` of different `String` containing all databases' names belonging to the `MongoClient` connected|
|`GetDatabase(database_name : String)`|`MongoDatabase`|Return a specific `MongoDatabase` by its name|
<br/> 

**MongoDatabase**
| Method | Type | Description |
| ------------- | ------------- | ------------- |
|`GetCollectionsList()`|`Array<String>`|Return an `Array` of different `String` representing all collections contained in the specified database|
|`GetCollectionsNameList()`|`Array<String>`|Return an `Array` of different `String` containing all collections' names contained in the specified database|
|`GetCollection(collection_name : String)`|`MongoCollection`|Return a specific `MongoCollection` inside the database by its name|
|`CreateCollection(collection_name : String)`|`MongoCollection`|Create a new `MongoCollection` and return it|
|`DropCollection(collection_name : String)`|`void`|Drop a specific `MongoCollection`|
<br/>  

**MongoCollection**
| Method | Type | Description |
| ------------- | ------------- | ------------- |
|`GetDocuments()`|`Array<Dictionary>`|Return an `Array` of different `Dictionary` representing a single document. The `Dictionary` is a serialization of a `BSON` document parsed to a GDScript `JSON`|
|`InsertDocument(document : Dictionary, _id : String)`|`void`|Insert a `BSON` document in the collection, parsed by a GDScript `Dictionary`. **note:** the \_id is not mandatory, but it always needs to be `null`,`""` or `" "` if you don't want to define an \_id|
|`InsertManyDocuments(document_list : Array<Dictionary>)`|`void`|Insert multiple `BSON` documents in the collection, parsed by an `Array` of GDScript different `Dictionary`|
|`CountDocuments()`|`int`|Count the number of `Documents` in the `MongoCollection`|
|`GetDocument(_id : String)`|`Dictionary`|Return a specific document as a `Dictionary`|
|`FindDocumentsBy(key : String, value : String)`|`Array<Dictionary>`|Return an `Array` of different `Dictionary` representing the documents that respect the query with the specified `key` and `value`|
|`UpdateDocumentBy(key : String, oldValue : String, newValue : String)`|`void`|Update the first document found with a `key:value` query, replacing the `oldValue` with the `newValue`|
|`UpdateDocumentsBy(key : String, oldValue : String, newValue : String)`|`void`|Update all the documents found with a `key:value` query, replacing the `oldValue` with the `newValue` in each one of them|
|`DeleteDocumentBy(key : String, value : String)`|`void`|Delete the first document found with a `key:value` query|
|`DeleteDocumentByID(String id)`|`void`|Delete the first document found with an `_id` query|
|`DeleteDocumentsBy(key : String, value : String)`|`void`|Delete all the documents found with a `key:value` query|
|`ReplaceOne(key : String, value : String, replacement_document : Dictionary)`|`void`| Replace document found with a `key:value` query|
|`ReplaceOneByID(id : String, replacement_document : Dictionary)`|`void`| Replace document found with an `_id` query|
<br/>  
  
## Some examples  
```gdscript
var client : MongoClient = MongoAPI.Connect(MongoAPI.host);
var database_list : Array = client.GetDatabaseList();
var database_namelist : Array = client.GetDatabaseNameList();
var database : MongoDatabase = client.GetDatabase(database_namelist[2]);
var collections_namelist : Array = database.GetCollectionsNameList();
var collection : MongoCollection = database.GetCollection(collections_namelist[0]);
var documents_list : Array = collection.GetDocuments();
var document : Dictionary = documents_list[0];
print(document);
print(document.name);
```
