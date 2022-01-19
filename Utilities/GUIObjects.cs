namespace JSONAPI.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Newtonsoft.Json;
    using Octokit;
    using System.Net;
    using Microsoft.Net.Http.Server;
    using MongoDB.Driver;
    using MongoDB.Bson;
    using System.Linq;
    using MongoDB.Bson.Serialization;
    using OpenXmlPowerTools;
    using System.Threading.Tasks;
    using MongoDB.Driver.Builders;
    using MongoDB.Driver.Linq;
    using System.Windows.Forms;
    using Newtonsoft.Json.Linq;
    using System.Xml;
    using Microsoft.Office.Interop.Excel;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Diagnostics;

    public static class GUIObjects
    {
        private static string connectionString = System.Environment.GetEnvironmentVariable("mongoDBConnectionString", EnvironmentVariableTarget.User);

        public class GUIObjectList
        {
            public string ObjectName { get; set; }
            public string XPath { get; set; }
            public string URL { get; set; }
            public bool IsValidated { get; set; }
            public string UserName { get; set; }

            public GUIObjectList(string objectName, string xpath, string url, bool isValidated)
            {
                ObjectName = objectName;
                XPath = xpath;
                URL = url;
                IsValidated = isValidated;
                UserName = Users.currentUser;
            }
            public GUIObjectList()
            {
                ObjectName = "";
                XPath = "";
                URL = "";
                IsValidated = false;
                UserName = Users.currentUser;
            }
        }

        public static void NewGuiObjectDocument(string objectName, string xpath, string url, bool isValidated)
        {
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("JSONAPI");
            var docSearchForElement = database.GetCollection<BsonDocument>("colGUIObjects");
            var doc = new BsonDocument
            {
                {"ObjectName", objectName},
                {"XPath", xpath},
                {"URL", url},
                {"IsValidated", isValidated},
                {"UserName", Users.currentUser}
            };

            docSearchForElement.InsertOne(doc);
        }

        public static void DeleteGuiObjectDocument(string objectName)
        {
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("JSONAPI");
            var docSearchForElement = database.GetCollection<BsonDocument>("colGUIObjects");

            try
            {
                var filter = Builders<BsonDocument>.Filter.Eq("ObjectName", objectName) & Builders<BsonDocument>.Filter.Eq("UserName", Users.currentUser);
                docSearchForElement.DeleteOne(filter);
            }
            catch (Exception e)
            {
                Utilities.WriteLogItem("DeleteGUIObjectDocument failed; " + e.ToString(), TraceEventType.Error);
            }
        }

        public static void UpdateGUIObjectDocument(string objectNameOldValue, string objectNameNewValue, string xpathOldValue, string xpathNewValue, string urlOldValue, string urlNewValue, bool isValidatedOldValue, bool isValidatedNewValue)
        {
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("JSONAPI");
            var docSearchForElement = database.GetCollection<BsonDocument>("colGUIObjects");

            try
            {
                if (objectNameOldValue != objectNameNewValue)
                {
                    var filter = Builders<BsonDocument>.Filter.Eq("ObjectName", objectNameOldValue) & Builders<BsonDocument>.Filter.Eq("UserName", Users.currentUser);
                    var update = Builders<BsonDocument>.Update.Set("ObjectName", objectNameNewValue);
                    docSearchForElement.UpdateOne(filter, update);
                }
                if (xpathOldValue != xpathNewValue)
                {
                    var filter = Builders<BsonDocument>.Filter.Eq("XPath", xpathOldValue);
                    var update = Builders<BsonDocument>.Update.Set("XPath", xpathNewValue);
                    docSearchForElement.UpdateOne(filter, update);
                }
                if (urlOldValue != urlNewValue)
                {
                    var filter = Builders<BsonDocument>.Filter.Eq("URL", urlOldValue);
                    var update = Builders<BsonDocument>.Update.Set("URL", urlNewValue);
                    docSearchForElement.UpdateOne(filter, update);
                }
                if (isValidatedOldValue != isValidatedNewValue)
                {
                    var filter = Builders<BsonDocument>.Filter.Eq("IsValidated", isValidatedOldValue);
                    var update = Builders<BsonDocument>.Update.Set("IsValidated", isValidatedNewValue);
                    docSearchForElement.UpdateOne(filter, update);
                }
            }
            catch (Exception e)
            {
                Utilities.WriteLogItem("UpdateGUIObjectDocument failed; " + e.ToString(), TraceEventType.Error);
            }
        }



        public static List<GUIObjectList> GetGUIObjectDocuments()
        {
            List<GUIObjectList> returnList = new List<GUIObjectList>();
            GUIObjectList arrayOfStrings;
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("JSONAPI");
            var docSearchForElement = database.GetCollection<BsonDocument>("colGUIObjects");
            try
            {
                var filter = Builders<BsonDocument>.Filter.Eq("UserName", Users.currentUser);
                var docs = docSearchForElement.Find(filter).Project("{_id: 0}").ToList();
                docs.ForEach(doc =>
                {
                    string tempString = doc.AsBsonValue.ToJson();
                    arrayOfStrings = JsonConvert.DeserializeObject<GUIObjectList>(tempString);

                    returnList.Add(arrayOfStrings);
                });
            }
            catch (Exception e)
            {
                Utilities.WriteLogItem("GetGUIObjectsDocuments failed; " + e.ToString(), TraceEventType.Error);
            }
            return returnList;

        }

        public static GUIObjectList GetGUIObjectDocument(string objectName)
        {
            GUIObjectList returnValue = new GUIObjectList();
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("JSONAPI");
            var docSearchForElement = database.GetCollection<BsonDocument>("colGUIObjects");
            try
            {
                var filter = Builders<BsonDocument>.Filter.Eq("UserName", Users.currentUser) & Builders<BsonDocument>.Filter.Eq("ObjectName", objectName);
                var docs = docSearchForElement.Find(filter).Project("{_id: 0}").ToList();
                docs.ForEach(doc =>
                {
                    string tempString = doc.AsBsonValue.ToJson();
                    returnValue = JsonConvert.DeserializeObject<GUIObjectList>(tempString);
                });
            }
            catch (Exception e)
            {
                Utilities.WriteLogItem("GetGUIObjectDocument(sequenceNr) failed; " + e.ToString(), TraceEventType.Error);
            }
            return returnValue;

        }

        public static List<String> GetGUIObjectFromURL(string url)
        {
            List<String> returnValue = new List<String>();
            GUIObjectList tempValue = new GUIObjectList();
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("JSONAPI");
            var docSearchForElement = database.GetCollection<BsonDocument>("colGUIObjects");
            try
            {
       
               var filter = Builders<BsonDocument>.Filter.Eq("UserName", Users.currentUser) & Builders<BsonDocument>.Filter.Eq("URL", url);
                var docs = docSearchForElement.Find(filter).Project("{_id: 0}").ToList();
                docs.ForEach(doc =>
                {
                    string tempString = doc.AsBsonValue.ToJson();
                    tempValue = JsonConvert.DeserializeObject<GUIObjectList>(tempString);
                    if (tempValue.URL.ToLower() == url)
                    {
                        returnValue.Add(tempValue.XPath);  
                    }
                });
            }
            catch (Exception e)
            {
                Utilities.WriteLogItem("GetGUIObjectFromURL(url) failed; " + e.ToString(), TraceEventType.Error);
            }
            return returnValue;

        }

        public static List<String> GetGUIObjectFromURL()
        {
            List<String> returnValue = new List<String>();
            GUIObjectList tempValue = new GUIObjectList();
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("JSONAPI");
            var docSearchForElement = database.GetCollection<BsonDocument>("colGUIObjects");
            try
            {

                var filter = Builders<BsonDocument>.Filter.Eq("UserName", Users.currentUser);
                var docs = docSearchForElement.Find(filter).Project("{_id: 0}").ToList();
                docs.ForEach(doc =>
                {
                    string tempString = doc.AsBsonValue.ToJson();
                    tempValue = JsonConvert.DeserializeObject<GUIObjectList>(tempString);
                    returnValue.Add(tempValue.XPath);
                });
            }
            catch (Exception e)
            {
                Utilities.WriteLogItem("GetGUIObjectFromURL() failed; " + e.ToString(), TraceEventType.Error);
            }
            return returnValue;

        }

        public static List<String> GetGUIURLs()
        {
            List<String> returnValue = new List<String>();
            GUIObjectList tempValue = new GUIObjectList();
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("JSONAPI");
            var docSearchForElement = database.GetCollection<BsonDocument>("colGUIObjects");
            try
            {

                var filter = Builders<BsonDocument>.Filter.Eq("UserName", Users.currentUser);
                var docs = docSearchForElement.Find(filter).Project("{_id: 0}").ToList();
                docs.ForEach(doc =>
                {
                    string tempString = doc.AsBsonValue.ToJson();
                    tempValue = JsonConvert.DeserializeObject<GUIObjectList>(tempString);
                    if (!returnValue.Contains(tempValue.URL))
                        returnValue.Add(tempValue.URL);
                });
            }
            catch (Exception e)
            {
                Utilities.WriteLogItem("GetGUIObjectFromURL() failed; " + e.ToString(), TraceEventType.Error);
            }
            return returnValue;

        }
    }
}
