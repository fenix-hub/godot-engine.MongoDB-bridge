using Godot;
using Godot.Collections;
using System;
using System.Linq;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core;
using MongoDB.Bson.IO;

public class _MongoCollection : Node
{
    private IMongoCollection<BsonDocument> BsonCollection;
    private Dictionary JsonCollection;
    private String addonPath;

    // ---- 

    public Godot.Collections.Array<Dictionary> documents;

    // --- PRIVATE FUNCTIONS : DON'T USE THEM UNLESS DIRECTLY USING C#

    public void LoadCollection(IMongoCollection<BsonDocument> collection, String path)
    {
        BsonCollection = collection;
        addonPath = path;
        AutoRetrieveDocument();
    }

    private Godot.Collections.Array<Dictionary> RetrieveDocuments()
    {
        var filterDB = Builders<BsonDocument>.Filter.Empty;
        var findDocumentDB = BsonCollection.Find(filterDB).ToList();
        Godot.Collections.Array<Dictionary> documents = Converters.ConvertBsonListToJsonList(findDocumentDB);
        return documents;
    }

    private void AutoRetrieveDocument()
    {
        documents = RetrieveDocuments();
    }

    // --------------------------------------------------

    public Godot.Collections.Array<Dictionary> GetDocuments()
    {
        return documents;
    }

    public void InsertDocument(Dictionary document, String id)
    {
        if (id!=null && id!="" && id!=" ")
        {
            document.Add("_id", id);
        }
        BsonCollection.InsertOne(Converters.ConvertDictionaryToBsonDocument(document));
        AutoRetrieveDocument();
    }

    public void InsertManyDocuments(Godot.Collections.Array<Dictionary> documentsList)
    {   
        var documents = Enumerable.Range(0, documentsList.Count).Select(i => Converters.ConvertDictionaryToBsonDocument(documentsList[i]));
        BsonCollection.InsertMany(documents);
        AutoRetrieveDocument();
    }

    public int CountDocuments()
    {
        return (int) BsonCollection.CountDocuments(new BsonDocument());
    }

    public Dictionary GetDocument(String id)
    {
        FilterDefinition<BsonDocument> filter;
        try
        {
            filter = Builders<BsonDocument>.Filter.Eq("_id", id);
        }
        catch (System.Exception)
        {
        }
        finally
        {
            filter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(id));
        }
        var findDocument = BsonCollection.Find(filter).First();
        return Converters.ConvertBsonDocumentToDictionary(findDocument);
    }

    public Godot.Collections.Array<Dictionary> FindDocumentsBy(String key, String value)
    {
        var filter = Builders<BsonDocument>.Filter.Eq(key, value);
        var findDocument = BsonCollection.Find(filter).ToList();
        return Converters.ConvertBsonListToJsonList(findDocument);
    }

    public void UpdateDocumentBy(String key, String oldValue, String newValue)
    {
        var filter = Builders<BsonDocument>.Filter.Eq(key, oldValue);
        var update = Builders<BsonDocument>.Update.Set(key, newValue);
        BsonCollection.UpdateOne(filter, update);
        AutoRetrieveDocument();
    }
    public void UpdateDocumentsBy(String key, String oldValue, String newValue)
    {
        var filter = Builders<BsonDocument>.Filter.Eq(key, oldValue);
        var update = Builders<BsonDocument>.Update.Set(key, newValue);
        BsonCollection.UpdateMany(filter, update);
        AutoRetrieveDocument();
    }

    public void DeleteDocumentBy(String key, String value)
    {
        var filter = Builders<BsonDocument>.Filter.Eq(key, value);
        BsonCollection.DeleteOne(filter);
        AutoRetrieveDocument();
    }
    public void DeleteDocumentsBy(String key, String value)
    {
        var filter = Builders<BsonDocument>.Filter.Eq(key, value);
        BsonCollection.DeleteMany(filter);
        AutoRetrieveDocument();
    }


}
