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

    public static class Variables
    {
        private static string connectionString = System.Environment.GetEnvironmentVariable("mongoDBConnectionString", EnvironmentVariableTarget.User);
        public class VariableList
        {
            public string VariableName { get; set; }
            public string SearchForElement { get; set; }
            public string SavedValue { get; set; }
            public string ReplaceWhere { get; set; }
            public string PartialSave { get; set; }
            public int NumChars { get; set; }
            public string UserName { get; set; }
            public VariableList(string variableName, string searchForElement, string savedValue, string replaceWhere, string partialSave, int numChars)
            {
                VariableName = variableName;
                SearchForElement = searchForElement;
                SavedValue = savedValue;
                ReplaceWhere = replaceWhere;
                PartialSave = partialSave;
                NumChars = numChars;
                UserName = Users.currentUser;
            }
        }

        public static void NewVariableDocument(string variableName, string searchForElement, string savedValue, string replaceWhere, string partialSave, int numChars)
        {
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("JSONAPI");
            var docSearchForElement = database.GetCollection<BsonDocument>("colVariables");
            var doc = new BsonDocument
            {
                {"VariableName", variableName},
                {"SearchForElement", searchForElement},
                {"SavedValue", savedValue},
                {"ReplaceWhere", replaceWhere},
                {"PartialSave", partialSave},
                {"NumChars", numChars},
                {"UserName", Users.currentUser}
            };

            docSearchForElement.InsertOne(doc);
        }

        public static void DeleteVariableDocument(string variableName)
        {
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("JSONAPI");
            var docSearchForElement = database.GetCollection<BsonDocument>("colVariables");

            try
            {
                var filter = Builders<BsonDocument>.Filter.Eq("VariableName", variableName) & Builders<BsonDocument>.Filter.Eq("UserName", Users.currentUser);
                docSearchForElement.DeleteOne(filter);
            }
            catch (Exception e)
            {
                Utilities.WriteLogItem("DeleteVariableDocument failed; " + e.ToString(), TraceEventType.Error);       
            }
        }

        public static void UpdateVariableDocument(string variableNameOldValue, string variableNameNewValue, string searchForElementOldValue, string searchForElementNewValue, string savedValueOldValue, string savedValueNewValue, string replaceWhereOldValue, string replaceWhereNewValue, string partialSaveOldValue, string partialSaveNewValue, int numCharsOldValue, int numCharsNewValue)
        {
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("JSONAPI");
            var docSearchForElement = database.GetCollection<BsonDocument>("colVariables");

            try
            {
                if (variableNameOldValue != variableNameNewValue)
                {
                    var filter = Builders<BsonDocument>.Filter.Eq("VariableName", variableNameOldValue) & Builders<BsonDocument>.Filter.Eq("UserName", Users.currentUser);
                    var update = Builders<BsonDocument>.Update.Set("VariableName", variableNameNewValue);
                    docSearchForElement.UpdateOne(filter, update);
                }
                if (searchForElementOldValue != searchForElementNewValue)
                {
                    var filter = Builders<BsonDocument>.Filter.Eq("SearchForElement", searchForElementOldValue);
                    var update = Builders<BsonDocument>.Update.Set("SearchForElement", searchForElementNewValue);
                    docSearchForElement.UpdateOne(filter, update);
                }
                if (savedValueOldValue != savedValueNewValue)
                {
                    var filter = Builders<BsonDocument>.Filter.Eq("SavedValue", savedValueOldValue);
                    var update = Builders<BsonDocument>.Update.Set("SavedValue", savedValueNewValue);
                    docSearchForElement.UpdateOne(filter, update);
                }
                if (replaceWhereOldValue != replaceWhereNewValue)
                {
                    var filter = Builders<BsonDocument>.Filter.Eq("ReplaceWhere", replaceWhereOldValue);
                    var update = Builders<BsonDocument>.Update.Set("ReplaceWhere", replaceWhereNewValue);
                    docSearchForElement.UpdateOne(filter, update);
                }
                if (partialSaveOldValue != partialSaveNewValue)
                {
                    var filter = Builders<BsonDocument>.Filter.Eq("PartialSave", partialSaveOldValue);
                    var update = Builders<BsonDocument>.Update.Set("PartialSave", partialSaveNewValue);
                    docSearchForElement.UpdateOne(filter, update);
                }
                if (numCharsOldValue != numCharsNewValue)
                {
                    var filter = Builders<BsonDocument>.Filter.Eq("NumChars", numCharsOldValue);
                    var update = Builders<BsonDocument>.Update.Set("NumChars", numCharsNewValue);
                    docSearchForElement.UpdateOne(filter, update);
                }
            }
            catch (Exception e)
            {
                Utilities.WriteLogItem("UpdateVariableDocument failed; " + e.ToString(), TraceEventType.Error);
            }
        }

        public static void UpdateVariableSavedValue(string variableName, string savedValue)
        {
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("JSONAPI");
            var docSearchForElement = database.GetCollection<BsonDocument>("colVariables");

            try
            {
                var filter = Builders<BsonDocument>.Filter.Eq("VariableName", variableName);
                var update = Builders<BsonDocument>.Update.Set("SavedValue", savedValue);
                docSearchForElement.UpdateOne(filter, update);

            }
            catch (Exception e)
            {
                Utilities.WriteLogItem("UpdateVariableSavedValue failed; " + e.ToString(), TraceEventType.Error);
            }
        }

        public static bool VariableExist(string variableName)
        {
            bool returnValue = false;
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("JSONAPI");
            var testListCollection = database.GetCollection<VariableList>("colVariables");

            var variableFilter = Builders<VariableList>.Filter.Eq(u => u.VariableName, variableName);
            // projection stage
            var simpleProjection = Builders<VariableList>.Projection
                .Exclude("_id");

            var docs = testListCollection.Find(variableFilter).Project(simpleProjection).ToList();
            //var docs = testListCollection.Find<BsonDocument>(tcFilter).Project<TestRunList>(simpleProjection).ToList();
            docs.ForEach(doc =>
            {
                string tempString = doc.AsBsonValue.ToJson();
                var variableObj = JsonConvert.DeserializeObject<VariableList>(tempString);
                var tempVariableName = variableObj.VariableName;
                if (tempVariableName == variableName) returnValue = true;
            });
            return returnValue;
        }

        public static List<VariableList> GetVariableDocuments()
        {
            List<VariableList> returnList = new List<VariableList>();
            VariableList arrayOfStrings;
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("JSONAPI");
            var docSearchForElement = database.GetCollection<BsonDocument>("colVariables");
                try
                {
                    var filter = Builders<BsonDocument>.Filter.Eq("UserName", Users.currentUser);
                    var docs = docSearchForElement.Find(filter).Project("{_id: 0}").ToList();
                    docs.ForEach(doc =>
                    {
                        string tempString = doc.AsBsonValue.ToJson();
                        arrayOfStrings = JsonConvert.DeserializeObject<VariableList>(tempString);

                        returnList.Add(arrayOfStrings);
                    });
                }
                catch (Exception e)
                {
                    Utilities.WriteLogItem("GetVariableDocuments failed; " + e.ToString(), TraceEventType.Error);
                }
            return returnList;

        }

        public static string GetSavedValue(string variableName)
        {
            string returnValue = "";
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("JSONAPI");
            var testListCollection = database.GetCollection<VariableList>("colVariables");
            var variableFilter = Builders<VariableList>.Filter.Eq(u => u.VariableName, variableName) & Builders<VariableList>.Filter.Eq("UserName", Users.currentUser);
            // projection stage
            var simpleProjection = Builders<VariableList>.Projection
                .Include(t => t.SavedValue)
                .Exclude("_id");

            var docs = testListCollection.Find(variableFilter).Project(simpleProjection).ToList();
            //var docs = testListCollection.Find<BsonDocument>(tcFilter).Project<TestRunList>(simpleProjection).ToList();
            docs.ForEach(doc =>
            {
                string tempString = doc.AsBsonValue.ToJson();
                //var returnListObj = JsonConvert.DeserializeObject(tempString);
                var userObj = JObject.Parse(tempString);
                returnValue = Convert.ToString(userObj["SavedValue"]);

            });
            return returnValue;
        }
    }
}
