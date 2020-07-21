using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core;

public class _MongoDatabase : Node
{

    private IMongoDatabase database;
    private String addonPath;

    public void LoadDatabase(IMongoDatabase db, String path)
    {
        database = db;
        addonPath = path;
    }

    public Godot.Collections.Array GetCollectionsList()
    {
        return Converters.ConvertBsonListToArray(database.ListCollections().ToList());
    }
    public Godot.Collections.Array GetCollectionsNameList()
    {
        return Converters.ConvertStringListToArray(database.ListCollectionNames().ToList());
    }

    public _MongoCollection GetCollection(String collectionName)
    {
        var collection = database.GetCollection<BsonDocument>(collectionName);
        _MongoCollection CollectionScene = null;
        if (!HasNode(collectionName)) {
            var MongoCollectionScene = (PackedScene) ResourceLoader.Load(addonPath+"MongoCollection/MongoCollection.tscn");
            CollectionScene = (_MongoCollection)MongoCollectionScene.Instance();
            AddChild(CollectionScene);
            CollectionScene.LoadCollection(collection, addonPath);
            CollectionScene.Name = collectionName;
        }
        else {
            CollectionScene = (_MongoCollection) GetNode(collectionName);
        }
        return CollectionScene;
    }

    public _MongoCollection CreateCollection(String collectionName)
    {
        try
        {
            database.CreateCollection(collectionName);
        }
        catch (System.Exception)
        {
            GD.PrintErr("[MongoDB Bridge] >> A collection named '"+collectionName+"' already exists in the current database. The existing one is returned");
        }
        return GetCollection(collectionName);
    }

    public void DropCollection(String collectionName)
    {
        database.DropCollection(collectionName);
    }

}
