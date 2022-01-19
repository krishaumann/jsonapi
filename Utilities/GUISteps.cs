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

    public static class GUISteps
    {
        private static string connectionString = System.Environment.GetEnvironmentVariable("mongoDBConnectionString", EnvironmentVariableTarget.User);
        public class GUIStepList
        {
            public int SequenceNr { get; set; }
            public string XPath { get; set; }
            public string OperationDesc { get; set; }
            public string OperationParameter { get; set; }
            public string ValueDesc { get; set; }
            public string Value { get; set; }
            public string UserName { get; set; }

            public GUIStepList(int sequenceNr, string xpath, string operationDesc, string operationParameter, string valueDesc, string value)
            {
                SequenceNr = sequenceNr;
                XPath = xpath;
                OperationDesc = operationDesc;
                OperationParameter = operationParameter;
                ValueDesc = valueDesc;
                Value = value;
                UserName = Users.currentUser;
            }
            public GUIStepList()
            {
                SequenceNr = 0;
                XPath = "";
                OperationDesc = "";
                OperationParameter = "";
                ValueDesc = "";
                Value = "";
                UserName = Users.currentUser;
            }
        }

        public static void NewGuiStepDocument(int sequenceNr, string xpath, string operationDesc, string operationParameter, string valueDesc, string value)
        {
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("JSONAPI");
            var docSearchForElement = database.GetCollection<BsonDocument>("colGUISteps");
            var doc = new BsonDocument
            {
                {"SequenceNr", sequenceNr},
                {"XPath", xpath},
                {"OperationDesc", operationDesc},
                {"OperationParameter", operationParameter},
                {"ValueDesc", valueDesc},
                {"Value", value},
                {"UserName", Users.currentUser}
            };

            docSearchForElement.InsertOne(doc);
        }

        public static void DeleteGuiStepDocument(int sequenceNr)
        {
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("JSONAPI");
            var docSearchForElement = database.GetCollection<BsonDocument>("colGUISteps");

            try
            {
                var filter = Builders<BsonDocument>.Filter.Eq("SequenceNr", sequenceNr) & Builders<BsonDocument>.Filter.Eq("UserName", Users.currentUser);
                docSearchForElement.DeleteOne(filter);
            }
            catch (Exception e)
            {
                Utilities.WriteLogItem("DeleteGUIStepDocument failed; " + e.ToString(), TraceEventType.Error);
            }
        }

        public static void UpdateGUIStepDocument(int sequenceNrOldValue, int sequenceNrNewValue, string xpathOldValue, string xpathNewValue, string operationDescOldValue, string operationDescNewValue, string operationParameterOldValue, string operationParameterNewValue, string valueDescOldValue, string valueDescNewValue, string valueOldValue, string valueNewValue)
        {
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("JSONAPI");
            var docSearchForElement = database.GetCollection<BsonDocument>("colGUISteps");

            try
            {
                if (sequenceNrOldValue != sequenceNrNewValue)
                {
                    var filter = Builders<BsonDocument>.Filter.Eq("SequenceNr", sequenceNrOldValue) & Builders<BsonDocument>.Filter.Eq("UserName", Users.currentUser);
                    var update = Builders<BsonDocument>.Update.Set("SequenceNr", sequenceNrNewValue);
                    docSearchForElement.UpdateOne(filter, update);
                }
                if (xpathOldValue != xpathNewValue)
                {
                    var filter = Builders<BsonDocument>.Filter.Eq("XPath", xpathOldValue);
                    var update = Builders<BsonDocument>.Update.Set("XPath", xpathNewValue);
                    docSearchForElement.UpdateOne(filter, update);
                }
                if (operationDescOldValue != operationDescNewValue)
                {
                    var filter = Builders<BsonDocument>.Filter.Eq("OperationDesc", operationDescOldValue);
                    var update = Builders<BsonDocument>.Update.Set("OperationDesc", operationDescNewValue);
                    docSearchForElement.UpdateOne(filter, update);
                }
                if (operationParameterOldValue != operationParameterNewValue)
                {
                    var filter = Builders<BsonDocument>.Filter.Eq("OperationParameter", operationParameterOldValue);
                    var update = Builders<BsonDocument>.Update.Set("OperationParameter", operationParameterNewValue);
                    docSearchForElement.UpdateOne(filter, update);
                }
                if (valueDescOldValue != valueDescNewValue)
                {
                    var filter = Builders<BsonDocument>.Filter.Eq("ValueDesc", valueDescOldValue);
                    var update = Builders<BsonDocument>.Update.Set("ValueDesc", valueDescNewValue);
                    docSearchForElement.UpdateOne(filter, update);
                }
                if (valueOldValue != valueNewValue)
                {
                    var filter = Builders<BsonDocument>.Filter.Eq("Value", valueOldValue);
                    var update = Builders<BsonDocument>.Update.Set("Value", valueNewValue);
                    docSearchForElement.UpdateOne(filter, update);
                }
            }
            catch (Exception e)
            {
                Utilities.WriteLogItem("UpdateGUIStepsDocument failed; " + e.ToString(), TraceEventType.Error);
            }
        }



        public static List<GUIStepList> GetGUIStepsDocuments()
        {
            List<GUIStepList> returnList = new List<GUIStepList>();
            GUIStepList arrayOfStrings;
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("JSONAPI");
            var docSearchForElement = database.GetCollection<BsonDocument>("colGUISteps");
            try
            {
                var filter = Builders<BsonDocument>.Filter.Eq("UserName", Users.currentUser);
                var docs = docSearchForElement.Find(filter).Project("{_id: 0}").ToList();
                docs.ForEach(doc =>
                {
                    string tempString = doc.AsBsonValue.ToJson();
                    arrayOfStrings = JsonConvert.DeserializeObject<GUIStepList>(tempString);

                    returnList.Add(arrayOfStrings);
                });
            }
            catch (Exception e)
            {
                Utilities.WriteLogItem("GetGUIStepsDocuments failed; " + e.ToString(), TraceEventType.Error);
            }
            return returnList;

        }

        public static GUIStepList GetGUIStepsDocument(int sequenceNr)
        {
            GUIStepList returnValue = new GUIStepList();
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("JSONAPI");
            var docSearchForElement = database.GetCollection<BsonDocument>("colGUISteps");
            try
            {
                var filter = Builders<BsonDocument>.Filter.Eq("UserName", Users.currentUser) & Builders<BsonDocument>.Filter.Eq("SequenceNr", sequenceNr);
                var docs = docSearchForElement.Find(filter).Project("{_id: 0}").ToList();
                docs.ForEach(doc =>
                {
                    string tempString = doc.AsBsonValue.ToJson();
                    returnValue = JsonConvert.DeserializeObject<GUIStepList>(tempString);
                });
            }
            catch (Exception e)
            {
                Utilities.WriteLogItem("GetGUIStepsDocument*sequenceNr) failed; " + e.ToString(), TraceEventType.Error);
            }
            return returnValue;

        }
    }
}
