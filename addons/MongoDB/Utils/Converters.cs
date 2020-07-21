using Godot;
using Godot.Collections;
using System;
using System.Linq;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Driver;
using MongoDB.Driver.Core;

public static class Converters
{

    public static String ConvertBsonDocumentToString(BsonDocument BsonDocument)
    {
        return BsonDocument.ToString();
    } 

    public static Dictionary ConvertBsonDocumentToDictionary(BsonDocument BsonDocument)
    {
        var jsonWriterSettings = new JsonWriterSettings { OutputMode = JsonOutputMode.Strict };
        var jsonStringDocument = BsonDocument.ToJson(jsonWriterSettings);
        var jsonDocument = JSON.Parse(jsonStringDocument).Result;
        return (Dictionary) jsonDocument;
    }

    public static BsonDocument ConvertDictionaryToBsonDocument(Dictionary dictionary)
    {
        var documentString = JSON.Print(dictionary);
        var BSONdocument = BsonDocument.Parse(documentString);
        return BSONdocument;
    }

    public static Godot.Collections.Array ConvertBsonListToArray(List<BsonDocument> bsonList)
    {
        var array = new Godot.Collections.Array();
        foreach (BsonDocument bson in bsonList)
        {
            array.Add(ConvertBsonDocumentToString(bson));
        }
        return array; 
    }

    public static Godot.Collections.Array<Dictionary> ConvertBsonListToJsonList(List<BsonDocument> bsonList)
    {
        var array = new Godot.Collections.Array<Dictionary>();
        foreach (BsonDocument bson in bsonList)
            array.Add(ConvertBsonDocumentToDictionary(bson));
        return array; 
    }

    public static Godot.Collections.Array ConvertStringListToArray(List<String> stringList)
    {
        var array = new Godot.Collections.Array();
        foreach (String _string in stringList)
        {
            array.Add(_string);
        }
        return array; 
    }


}