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
    using System.Diagnostics;

    public static class Logs
    {
        private static string connectionString = System.Environment.GetEnvironmentVariable("mongoDBConnectionString", EnvironmentVariableTarget.User);
        
        public class Log
        {
            public string LogId { get; set; }
            public string LogMessage { get; set; }
            public string LogType { get; set; }
            public string LogDateTime { get; set; }
            public string UserName { get; set; }
            public Log(string logMessage, TraceEventType logType)
            {
                LogId = Guid.NewGuid().ToString();
                LogMessage = logMessage;
                LogType = logType.ToString();
                LogDateTime = DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss");
                UserName = Users.currentUser;
            }
        }

        public static bool NewLogItem(string logMessage, TraceEventType logType)
        {
            bool returnValue = false;
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("JSONAPI");

            var docLog = database.GetCollection<Log>("colLogs");
            Log logItem = new Log(logMessage, logType);
            try
            {
                docLog.InsertOne(logItem);
            }
            catch (Exception e)
            {
                Utilities.WriteLogItem("Add new UserLog failed; " + e.ToString(), TraceEventType.Error);
                returnValue = false;
            }
            return returnValue;
        }

        public static void DeleteLog(string logId)
        {
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("JSONAPI");
            var logCollection = database.GetCollection<Log>("colLogs");

            try
            {
                var filter = Builders<Log>.Filter.Eq("LogId", logId);
                logCollection.DeleteOne(filter);
            }
            catch (Exception e)
            {
                Utilities.WriteLogItem("DeleteUserLog failed; " + e.ToString(), TraceEventType.Error);
            }
        }

        public static void DeleteLogs()
        {
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("JSONAPI");
            var logCollection = database.GetCollection<Log>("colLogs");

            try
            {      
                logCollection.DeleteMany(new BsonDocument());
            }
            catch (Exception e)
            {
                Utilities.WriteLogItem("DeleteLogs failed; " + e.ToString(), TraceEventType.Error);
            }
        }

        public static List<Log> GetLogs()
        {
            List<Log> returnList = new List<Log>();

            var settings = MongoClientSettings.FromConnectionString(connectionString);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("JSONAPI");
            var logCollection = database.GetCollection<Log>("colLogs");
            var simpleProjection = Builders<Log>.Projection
                .Exclude("_id");
            try
            {
                var filter = Builders<Log>.Filter.Eq(u => u.UserName, Users.currentUser);
                var docs = logCollection.Find(new BsonDocument()).Project(simpleProjection).ToList();
                docs.ForEach(doc =>
                {
                    string tempString = doc.AsBsonValue.ToJson();
                    var tempValue = JsonConvert.DeserializeObject<Log>(tempString);
                    returnList.Add(tempValue);
                });
            }
            catch (Exception e)
            {
                Utilities.WriteLogItem("GetLogs() failed; " + e.ToString(), TraceEventType.Error);
            }
            return returnList;
        }
    }
}
