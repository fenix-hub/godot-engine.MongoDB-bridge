tool
extends EditorPlugin
var _MongoAPI : String = ("res://addons/MongoDB/MongoAPI.cs")
var _MongoClient : String = ("res://addons/MongoDB/MongoClient/_MongoClient.cs")
var _MongoDatabase : String = ("res://addons/MongoDB/MongoDatabase/_MongoDatabase.cs")
var _MongoCollection : String = ("res://addons/MongoDB/MongoCollection/_MongoCollection.cs")


func _enter_tree():
    add_autoload_singleton("MongoAPI",_MongoAPI)
    add_autoload_singleton("MongoClient",_MongoClient)
    add_autoload_singleton("MongoDatabase",_MongoDatabase)
    add_autoload_singleton("MongoCollection",_MongoCollection)

func _exit_tree():
    remove_autoload_singleton("MongoAPI")
    remove_autoload_singleton("MongoClient")
    remove_autoload_singleton("MongoDatabase")
    remove_autoload_singleton("MongoCollection")
