using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core;

public class MongoAPI : Node
{
    [Export]
    private String host = "mongodb://127.0.0.1:27017";
    private String addonPath = "res://addons/MongoDB/";
    public _MongoClient Connect(String hostIp){
        return this.Connect(hostIp, true);
    }
    public _MongoClient Connect(String hostIp, bool checkSslCertificate)
    {
        host = hostIp;
        var MongoClientScene = (PackedScene) ResourceLoader.Load(addonPath+"MongoClient/MongoClient.tscn");
        var ClientScene = MongoClientScene.Instance() as _MongoClient;
        AddChild(ClientScene);
        ClientScene.LoadClient(new MongoClient(host), addonPath, checkSslCertificate);
        ClientScene.Name = host;
        return ClientScene;
    }

}
