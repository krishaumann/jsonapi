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

    public static class Ranges
    {
        private static string connectionString = System.Environment.GetEnvironmentVariable("mongoDBConnectionString", EnvironmentVariableTarget.User);
        public class RangeList
        {
            public string RangeName { get; set; }
            public string RangeDescription { get; set; }
            public string[] RangeValues { get; set; }
            public string UserName { get; set; }
            public RangeList(string rangeName, string rangeDescription, string[] rangeValues)
            {
                RangeName = rangeName;
                RangeDescription = rangeDescription;
                RangeValues = rangeValues;
                UserName = Users.currentUser;
            }
        }

        public static void NewRangeDocument(string rangeName, string rangeDescription, string[] rangeValues)
        {
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("JSONAPI");
            var docNewTestSuite = database.GetCollection<BsonDocument>("colRangeList");

            var doc = new BsonDocument
            {
                {"RangeName", rangeName},
                {"RangeDescription", rangeDescription},
                {"UserName", Users.currentUser}
            };
            var rangeValuesArr = new BsonArray(rangeValues);
            doc.Add("RangeValues", rangeValuesArr);
            docNewTestSuite.InsertOne(doc);
        }

        public static void DeleteRangeDocument(string rangeName)
        {
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("JSONAPI");
            var docSearchForElement = database.GetCollection<BsonDocument>("colRangeList");

            try
            {
                var filter = Builders<BsonDocument>.Filter.Eq("RangeName", rangeName) & Builders<BsonDocument>.Filter.Eq("UserName", Users.currentUser);
                docSearchForElement.DeleteOne(filter);
            }
            catch (Exception e)
            {
                Utilities.WriteLogItem("DeleteRangeDocument failed; " + e.ToString(), TraceEventType.Error);
            }
        }

        public static void UpdateRangeValues(string oldRangeName, string newRangeName, string rangeDescription, string[] rangeValues)
        {
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("JSONAPI");
            var docSearchForElement = database.GetCollection<BsonDocument>("colRangeList");

            try
            {
                var filter = Builders<BsonDocument>.Filter.Eq("RangeName", oldRangeName) & Builders<BsonDocument>.Filter.Eq("UserName", Users.currentUser);
                var update = Builders<BsonDocument>.Update.Set("RangeValues", rangeValues).Set("RangeName", newRangeName).Set("RangeDescription", rangeDescription);
                docSearchForElement.UpdateOne(filter, update);
            }
            catch (Exception e)
            {
                Utilities.WriteLogItem("UpdateRangeValues failed; " + e.ToString(), TraceEventType.Error);
            }
        }

        public static List<RangeList> GetRangeLists()
        {
            List<RangeList> returnList = new List<RangeList>();
            RangeList arrayOfStrings;
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("JSONAPI");
            var docSearchForElement = database.GetCollection<BsonDocument>("colRangeList");
            try
            {
                var filter = Builders<BsonDocument>.Filter.Eq("UserName", Users.currentUser);
                var docs = docSearchForElement.Find(filter).Project("{_id: 0}").ToList();
                docs.ForEach(doc =>
                {
                    string tempString = doc.AsBsonValue.ToJson();
                    arrayOfStrings = JsonConvert.DeserializeObject<RangeList>(tempString);

                    returnList.Add(arrayOfStrings);
                });
            }
            catch (Exception e)
            {
                Utilities.WriteLogItem("GetRangeLists failed; " + e.ToString(), TraceEventType.Error);
            }
            return returnList;
        }

        public static string[] GetRangeListValues(string rangeName)
        {
            string[] returnListObj = new string[100];
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("JSONAPI");
            var testRangeCollection = database.GetCollection<RangeList>("colRangeList");
            string colName = "";
            bool multipleColumnFlag = false;
            if (rangeName.Contains("."))
            {
                var tempArray = rangeName.Split(".");
                rangeName = tempArray[0];
                colName = tempArray[1];
                multipleColumnFlag = true;
            }

            var tcBuilder = Builders<BsonDocument>.Filter;
            var rangeFilter = Builders<RangeList>.Filter.Eq(u => u.RangeName, rangeName) & Builders<RangeList>.Filter.Eq("UserName", Users.currentUser);
            // projection stage
            var simpleProjection = Builders<RangeList>.Projection
                .Include(t => t.RangeValues)
                .Exclude("_id");
            try
            {
                var docs = testRangeCollection.Find(rangeFilter).Project(simpleProjection).ToList();
                //var docs = testListCollection.Find<BsonDocument>(tcFilter).Project<TestRunList>(simpleProjection).ToList();
                docs.ForEach(doc =>
                {
                    // First see if it is a JArray
                    
                    string tempString = doc.AsBsonValue.ToJson();
                    int c = 0;
                    int h = 0;
                    var values = JsonConvert.DeserializeObject<Dictionary<string, object>>(tempString);
                    foreach (KeyValuePair<string, object> d in values)
                    {
                        // if (d.Value.GetType().FullName.Contains("Newtonsoft.Json.Linq.JObject"))
                        // 
                        if (d.Value is JArray)
                        {
                            string[] lineArray = d.Value.ToString().Split(
                                        new string[] { "\r\n", "\r", "\n" },
                                        StringSplitOptions.None
                            );
                            int startRow;
                            if (lineArray[0].Length < 2) startRow = 1;
                            else startRow = 0;
                            string[] headerArray = lineArray[startRow].Split(",");
                            for (int e = 0; e < headerArray.Length; e++)
                            {
                                if (headerArray[e] == colName)
                                    c = e;
                            }
                            for (int f = startRow+1; f < lineArray.Length; f++)
                            {
                                string[] colArray = lineArray[f].Split(",");
                                for (int g = 0; g < colArray.Length; g++)
                                {
                                    if (g == c & colArray[g].Length >=1)
                                    {
                                        returnListObj[h] = colArray[g].Replace("\"", "").Replace("\\", "").Replace("{", "").Replace("}", "").Replace("[", "").Replace("]", "");
                                        h++;
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (!multipleColumnFlag)
                            {
                                tempString = tempString.Replace("\"", "").Replace("{", "").Replace("}", "").Replace("[", "").Replace("]", "");
                                returnListObj = tempString.Split(",".ToCharArray());
                            }
                        }
                    }
                    //{ \"RangeValues\" : [\"DK\", \"US\", \"UK\", \"ZA\", \"DE\", \"AU\", \"NZ\", \"IT\", \"FR\", \"NL\", \"BE\", \"CZ\"] }"
                    //var Json = JsonConvert.DeserializeObject(tempString);
                    //returnValue1 = tempString.Split(",");
                    //returnValue1 = Json.Select(x => x.ToString()).ToArray();
                    //returnListObj = JsonConvert.DeserializeObject<string[]>(tempString);
                    //var userObj = JObject.Parse(doc.ToString());
                    //tempValue = Convert.ToString(userObj["RangeValues"]);
                    //returnValue1[counter] = tempValue;
                });
            }
            catch (Exception e)
            {
                Utilities.WriteLogItem("GetRangeListValues failed; " + e.ToString(), TraceEventType.Error);
            }
            string[] returnValue2 = new string[returnListObj.Length];
            for (int d = 0; d < returnListObj.Length; ++d)
            {
                if (returnListObj[d] != null) returnListObj[d] = returnListObj[d].Trim();
            }

            returnValue2 = returnListObj;
            return returnValue2;
        }

        public static RangeList GetRangeListDetails(string rangeName)
        {
            RangeList returnValue = null;
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("JSONAPI");
            var testListCollection = database.GetCollection<RangeList>("colRangeList");

            var rangeFilter = Builders<RangeList>.Filter.Eq(u => u.RangeName, rangeName) & Builders<RangeList>.Filter.Eq("UserName", Users.currentUser);
            // projection stage
            var simpleProjection = Builders<RangeList>.Projection
                .Exclude("_id");
            try
            {
                var docs = testListCollection.Find(rangeFilter).Project(simpleProjection).ToList();
                //var docs = testListCollection.Find<BsonDocument>(tcFilter).Project<TestRunList>(simpleProjection).ToList();
                docs.ForEach(doc =>
                {
                    string tempString = doc.AsBsonValue.ToJson();
                    returnValue = JsonConvert.DeserializeObject<RangeList>(tempString);
                });
            }
            catch (Exception e)
            {
                Utilities.WriteLogItem("GetRangeListDetails failed; " + e.ToString(), TraceEventType.Error);
            }
            return returnValue;
        }
    }
}
