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

    public static class Databases
    {
        private static string connectionString = System.Environment.GetEnvironmentVariable("mongoDBConnectionString", EnvironmentVariableTarget.User);
        public class DatabaseList
        {
            public string DatabaseName { get; set; }
            public string DatabaseDescription { get; set; }
            public string ConnectionString { get; set; }
            public string SQL { get; set; }
            public string UserName { get; set; }
            public DatabaseList(string databaseName, string databaseDescription, string connectionString, string sql)
            {
                DatabaseName = databaseName;
                DatabaseDescription = databaseDescription;
                ConnectionString = connectionString;
                UserName = Users.currentUser;
                SQL = sql;
             }
        }

        public static void NewDBDocument(string dbName, string dbDescription, string connectionString, string sql)
        {
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("JSONAPI");
            var docNewTestSuite = database.GetCollection<BsonDocument>("colDatabaseList");

            var doc = new BsonDocument
            {
                {"DatabaseName", dbName},
                {"DatabaseDescription", dbDescription},
                {"ConnectionString", connectionString},
                {"SQL", sql},
                {"UserName", Users.currentUser}
            };
            docNewTestSuite.InsertOne(doc);
        }

        public static void DeleteDBDocument(string dbName)
        {
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("JSONAPI");
            var docSearchForElement = database.GetCollection<BsonDocument>("colDatabaseList");

            try
            {
                var filter = Builders<BsonDocument>.Filter.Eq("DatabaseName", dbName) & Builders<BsonDocument>.Filter.Eq("UserName", Users.currentUser);
                docSearchForElement.DeleteOne(filter);
            }
            catch (Exception e)
            {
                Utilities.WriteLogItem("DeleteDBDocument failed; " + e.ToString(), TraceEventType.Error);
            }
        }

        public static void UpdateDBValues(string oldDBName, string newDBName, string dbName, string dbDescription, string connectionString, string sql)
        {
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("JSONAPI");
            var docSearchForElement = database.GetCollection<BsonDocument>("colDatabaseList");

            try
            {
                var filter = Builders<BsonDocument>.Filter.Eq("DatabaseName", oldDBName) & Builders<BsonDocument>.Filter.Eq("UserName", Users.currentUser);
                var update = Builders<BsonDocument>.Update.Set("ConnectionString", connectionString).Set("SQL", sql).Set("DatabaseDescription", dbDescription);
                docSearchForElement.UpdateOne(filter, update);
            }
            catch (Exception e)
            {
                Utilities.WriteLogItem("UpdateDBValues failed; " + e.ToString(), TraceEventType.Error);
            }
        }

        public static List<DatabaseList> GetDBLists()
        {
            List<DatabaseList> returnList = new List<DatabaseList>();
            DatabaseList arrayOfStrings;
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("JSONAPI");
            var docSearchForElement = database.GetCollection<BsonDocument>("colDatabaseList");
            try
            {
                var filter = Builders<BsonDocument>.Filter.Eq("UserName", Users.currentUser);
                var docs = docSearchForElement.Find(filter).Project("{_id: 0}").ToList();
                docs.ForEach(doc =>
                {
                    string tempString = doc.AsBsonValue.ToJson();
                    arrayOfStrings = JsonConvert.DeserializeObject<DatabaseList>(tempString);

                    returnList.Add(arrayOfStrings);
                });
            }
            catch (Exception e)
            {
                Utilities.WriteLogItem("GetDBLists failed; " + e.ToString(), TraceEventType.Error);
            }
            return returnList;
        }

        public static DatabaseList GetDBListDetails(string dbName)
        {
            DatabaseList returnValue = null;
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("JSONAPI");
            var testListCollection = database.GetCollection<DatabaseList>("colDatabaseList");

            var DatabaseFilter = Builders<DatabaseList>.Filter.Eq(u => u.DatabaseName, dbName) & Builders<DatabaseList>.Filter.Eq("UserName", Users.currentUser);
            // projection stage
            var simpleProjection = Builders<DatabaseList>.Projection
                .Exclude("_id");
            try
            {
                var docs = testListCollection.Find(DatabaseFilter).Project(simpleProjection).ToList();
                //var docs = testListCollection.Find<BsonDocument>(tcFilter).Project<TestRunList>(simpleProjection).ToList();
                docs.ForEach(doc =>
                {
                    string tempString = doc.AsBsonValue.ToJson();
                    returnValue = JsonConvert.DeserializeObject<DatabaseList>(tempString);
                });
            }
            catch (Exception e)
            {
                Utilities.WriteLogItem("GetDBListDetails failed; " + e.ToString(), TraceEventType.Error);
            }
            return returnValue;
        }
    }
}

