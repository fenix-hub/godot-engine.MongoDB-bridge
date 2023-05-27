using Godot;
using Godot.Collections;
using System;
using System.Security.Authentication;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core;

public class _MongoClient : Node
{

    private String addonPath;
    private MongoClient client;

    public void LoadClient(MongoClient mongoClient, String path, bool checkSslCertificate)
    {
        client = mongoClient;
        addonPath = path;
       
        if (!checkSslCertificate){
			var settings = client.Settings.Clone();
			settings.SslSettings = new SslSettings() {
			CheckCertificateRevocation = true,
			ServerCertificateValidationCallback = (sender, certificate, chain, errors) => true
			};
			
			client = new MongoClient(settings);
		}
        
    }

    public Godot.Collections.Array<Dictionary> GetDatabaseList()
    {
        var databaseList = client.ListDatabases().ToList();
        var databaseArray = Converters.ConvertBsonListToJsonList(databaseList);
        return databaseArray;
    }

    public Godot.Collections.Array GetDatabaseNameList()
    {
        var databaseNameList = client.ListDatabaseNames().ToList();
        var databaseNameArray = Converters.ConvertStringListToArray(databaseNameList);
        return databaseNameArray;
    }

    public _MongoDatabase GetDatabase(String databaseName)
    {
        var database = client.GetDatabase(databaseName);
        _MongoDatabase DatabaseScene = null;
        if (!HasNode(databaseName))
        {
            var MongoDatabaseScene = (PackedScene) ResourceLoader.Load(addonPath+"MongoDatabase/MongoDatabase.tscn");
            DatabaseScene = MongoDatabaseScene.Instance() as _MongoDatabase;
            AddChild(DatabaseScene);
            DatabaseScene.LoadDatabase(database, addonPath);
            DatabaseScene.Name = databaseName;
        }
        else {
            DatabaseScene = (_MongoDatabase)GetNode(databaseName);
        }

        return DatabaseScene;
    }

}
