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

    public static class ExecutionResult
    {
        private static string connectionString = "mongodb+srv://admin:admin@jsonapi.f7zgt.mongodb.net/myFirstDatabase?keepAlive=true&poolSize=30&autoReconnect=true&socketTimeoutMS=360000&connectTimeoutMS=360000";
        public class TestRun
        {
            public string TestRunName { get; set; }
            public string TestSuiteName { get; set; }
            public string TestName { get; set; }
            public string UserName { get; set; }
            public List<TestResult> TestResultList { get; set; }
            public TestRun(string testRunName, string testSuiteName, string testName, List<TestResult> testResultList)
            {
                TestRunName = testRunName;
                TestSuiteName = testSuiteName;
                TestName = testName;
                TestResultList = testResultList;
                UserName = Users.currentUser;
            }
            public TestRun()
            {
                TestResultList = new List<TestResult>();
            }
        }

        public class TestResult
        {
            public string FieldName { get; set; }
            public string RunNr { get; set; }
            public int IncrementNr { get; set; }
            public string ExpectedValue { get; set; }
            public string ActualValue { get; set; }
            public string Result { get; set; }
            public TestResult(string fieldName, string runNr, int incrementNr, string expectedValue, string actualValue, string result)
            {
                FieldName = fieldName;
                RunNr = runNr;
                IncrementNr = incrementNr;
                ExpectedValue = expectedValue;
                ActualValue = actualValue;
                Result = result;
            }
            public TestResult()
            {

            }
        }

        public class GroupedResult
        {
            public string TestName { get; set; }
            public string UserName { get; set; }
            public string FieldName { get; set; }
            public string ExpectedResult { get; set; }
            public string FieldValue1 { get; set; }
            public string FieldValue2 { get; set; }
            public string FieldValue3 { get; set; }
            public string FieldValue4 { get; set; }
            public string FieldValue5 { get; set; }
            public string FieldValue6 { get; set; }
            public string FieldValue7 { get; set; }
            public string FieldValue8 { get; set; }
            public string FieldValue9 { get; set; }
            public string FieldValue10 { get; set; }

            public GroupedResult(string testName, string fieldName, string expectedResult, string fieldValue1, string fieldValue2, string fieldValue3, string fieldValue4, string fieldValue5, string fieldValue6, string fieldValue7, string fieldValue8, string fieldValue9, string fieldValue10)
            {
                TestName = testName;
                UserName = Users.currentUser;
                FieldName = fieldName;
                ExpectedResult = expectedResult;
                FieldValue1 = fieldValue1;
                FieldValue2 = fieldValue2;
                FieldValue3 = fieldValue3;
                FieldValue4 = fieldValue4;
                FieldValue5 = fieldValue5;
                FieldValue6 = fieldValue6;
                FieldValue7 = fieldValue7;
                FieldValue8 = fieldValue8;
                FieldValue9 = fieldValue9;
                FieldValue10 = fieldValue10;  
            }
            public GroupedResult(string testName)
            {
                TestName = testName;
                UserName = Users.currentUser;
                FieldName = "";
                ExpectedResult = "";
                FieldValue1 = "";
                FieldValue2 = "";
                FieldValue3 = "";
                FieldValue4 = "";
                FieldValue5 = "";
                FieldValue6 = "";
                FieldValue7 = "";
                FieldValue8 = "";
                FieldValue9 = "";
                FieldValue10 = "";
            }
            public GroupedResult()
            {
                TestName = "";
                UserName = "";
                FieldName = "";
                ExpectedResult = "";
                FieldValue1 = "";
                FieldValue2 = "";
                FieldValue3 = "";
                FieldValue4 = "";
                FieldValue5 = "";
                FieldValue6 = "";
                FieldValue7 = "";
                FieldValue8 = "";
                FieldValue9 = "";
                FieldValue10 = "";
            }
        }

        public static void NewTestRun(string testRunName, string testSuiteName, string testName)
        {
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("JSONAPI");
            var docNewTestSuite = database.GetCollection<BsonDocument>("colTestRuns");
            
            TestRun testRunQuery = GetExecutedRunList(testRunName, testSuiteName, testName);
            if (testRunQuery.TestSuiteName == null)
            {
                List<TestResult> testResultList = new List<TestResult>();
                TestRun testRun = new TestRun(testRunName, testSuiteName, testName, testResultList);
                docNewTestSuite.InsertOne(testRun.ToBsonDocument());
            }
        }

        public static List<TestRun> GetExecutedRunList()
        {
            List<TestRun> returnList = new List<TestRun>();
            TestRun arrayOfStrings;
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("JSONAPI");
            var docSearchForElement = database.GetCollection<TestRun>("colTestRuns");
            try
            {
                var filter = Builders<TestRun>.Filter.Eq(u => u.UserName, Users.currentUser);
                var docs = docSearchForElement.Find(filter).Project("{_id: 0}").ToList();
                docs.ForEach(doc =>
                {
                    string tempString = doc.AsBsonValue.ToJson();
                    arrayOfStrings = JsonConvert.DeserializeObject<TestRun>(tempString);
                    returnList.Add(arrayOfStrings);
                });
            }
            catch (Exception e)
            {
                Utilities.WriteLogItem("GetExecutedRunList failed; " + e.ToString(), TraceEventType.Error);
            }
            return returnList;
        }

        public static List<String> GetTestRunNames()
        {
            List<string> returnList = new List<string>();
            TestRun arrayOfStrings;
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("JSONAPI");
            var docSearchForElement = database.GetCollection<TestRun>("colTestRuns");
            try
            {
                var filter = Builders<TestRun>.Filter.Eq(u => u.UserName, Users.currentUser);
                var docs = docSearchForElement.Find(filter).Project("{_id: 0}").ToList();
                docs.ForEach(doc =>
                {
                    string tempString = doc.AsBsonValue.ToJson();
                    arrayOfStrings = JsonConvert.DeserializeObject<TestRun>(tempString);
                    if (!returnList.Contains(arrayOfStrings.TestRunName))
                    returnList.Add(arrayOfStrings.TestRunName);
                });
            }
            catch (Exception e)
            {
                Utilities.WriteLogItem("GetTestRunNames failed; " + e.ToString(), TraceEventType.Error);
            }
            return returnList;
        }

        public static List<TestRun> GetExecutedRunList_testSuite(string testSuiteName)
        {
            List<TestRun> returnList = new List<TestRun>();
            TestRun arrayOfStrings;
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("JSONAPI");
            var docSearchForElement = database.GetCollection<TestRun>("colTestRuns");
            var testSuiteNameFilter = Builders<TestRun>.Filter.Eq(u => u.TestSuiteName, testSuiteName) & Builders<TestRun>.Filter.Eq(u => u.UserName, Users.currentUser);
            try                                          
            {
                var docs = docSearchForElement.Find(testSuiteNameFilter).Project("{_id: 0}").ToList();
                docs.ForEach(doc =>
                {
                    string tempString = doc.AsBsonValue.ToJson();
                    arrayOfStrings = JsonConvert.DeserializeObject<TestRun>(tempString);
                    returnList.Add(arrayOfStrings);
                });
            }
            catch (Exception e)
            {
                Utilities.WriteLogItem("GetExecutedRunList(testSuiteName) failed; " + e.ToString(), TraceEventType.Error);
            }
            return returnList;
        }

        public static List<TestResult> GetFieldResultList(string testRunName, string testName)
        {
            List<TestResult> returnList = new List<TestResult>();
            TestRun arrayOfStrings;
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("JSONAPI");
            var docSearchForElement = database.GetCollection<TestRun>("colTestRuns");
            var testRunFilter = Builders<TestRun>.Filter.Eq(u => u.TestName, testName) & Builders<TestRun>.Filter.Eq(u => u.TestRunName, testRunName) & Builders<TestRun>.Filter.Eq(u => u.UserName, Users.currentUser);
            try
            {
                var docs = docSearchForElement.Find(testRunFilter).Project("{_id: 0}").ToList();
                docs.ForEach(doc =>
                {
                    string tempString = doc.AsBsonValue.ToJson();
                    arrayOfStrings = JsonConvert.DeserializeObject<TestRun>(tempString);
                    returnList = arrayOfStrings.TestResultList;
                });
            }
            catch (Exception e)
            {
                Utilities.WriteLogItem("GetFieldResultList(testRunName,testName) failed; " + e.ToString(), TraceEventType.Error);
            }
            return returnList;
        }

        public static List<TestResult> GetFieldResultList(string testRunName, string testName, int incrementCounter)
        {
            List<TestResult> returnList = new List<TestResult>();
            TestRun arrayOfStrings;
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("JSONAPI");
            var docSearchForElement = database.GetCollection<TestRun>("colTestRuns");
            var testRunFilter = Builders<TestRun>.Filter.Eq(u => u.TestName, testName) & Builders<TestRun>.Filter.Eq(u => u.TestRunName, testRunName);
            try
            {
                var docs = docSearchForElement.Find(testRunFilter).Project("{_id: 0}").ToList();
                docs.ForEach(doc =>
                {
                    string tempString = doc.AsBsonValue.ToJson();
                    arrayOfStrings = JsonConvert.DeserializeObject<TestRun>(tempString);

                    foreach (TestResult testResultItem in arrayOfStrings.TestResultList)
                    {
                        if (testResultItem.IncrementNr == incrementCounter)
                        {
                            returnList.Add(testResultItem);
                        }
                    }   
                });
            }
            catch (Exception e)
            {
                Utilities.WriteLogItem("GetFieldResultList(testRunName,testName) failed; " + e.ToString(), TraceEventType.Error);
            }
            return returnList;
        }


        public static TestRun GetExecutedRunList(string testRunName, string testSuiteName = "", string testName = "")
        {
            TestRun returnValue = new TestRun();
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("JSONAPI");
            var docSearchForElement = database.GetCollection<TestRun>("colTestRuns");
            var simpleProjection = Builders<TestRun>.Projection
            //.Include(t => t.TestSuiteList)
               .Exclude("_id");
            FilterDefinition<TestRun> filter = null;
            if (testRunName.Length > 0)
            {
                if (filter == null)
                {
                    filter = Builders<TestRun>.Filter.Eq("TestRunName", testRunName);
                }
                else
                {
                    filter = filter & Builders<TestRun>.Filter.Eq("TestRunName", testRunName);
                }
            }
            if (testSuiteName.Length > 0)
            {
                if (filter == null)
                {
                    filter = Builders<TestRun>.Filter.Eq("TestSuiteName", testSuiteName);
                }
                else
                {
                    filter = filter & Builders<TestRun>.Filter.Eq("TestSuiteName", testSuiteName);
                }
            }
            if (testName.Length > 0)
            {
                if (filter == null)
                {
                    filter = Builders<TestRun>.Filter.Eq("TestName", testName);
                }
                else
                {
                    filter = filter & Builders<TestRun>.Filter.Eq("TestName", testName);
                }
            }
            try
            {
                filter = filter & Builders<TestRun>.Filter.Eq(u => u.UserName, Users.currentUser);
                var docs = docSearchForElement.Find(filter).Project(simpleProjection).ToList();
                docs.ForEach(doc =>
                {
                    string tempString = doc.AsBsonValue.ToJson();
                    returnValue = JsonConvert.DeserializeObject<TestRun>(tempString);
                });

            }
            catch (Exception e)
            {
                Utilities.WriteLogItem("GetTestRunList(testSuiteName, testName) failed; " + e.ToString(), TraceEventType.Error);
            }
            return returnValue;
        }


        public static List<GroupedResult> GetGroupedResultList(string testRunName, string testSuiteName = "", string testName = "")
        {
            List<GroupedResult> returnValue = new List<GroupedResult>();
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("JSONAPI");
            var docSearchForElement = database.GetCollection<TestRun>("colTestRuns");
            var simpleProjection = Builders<TestRun>.Projection
            //.Include(t => t.TestSuiteList)
               .Exclude("_id");
            FilterDefinition<TestRun> filter = null;
            if (testRunName.Length > 0)
            {
                if (filter == null)
                {
                    filter = Builders<TestRun>.Filter.Eq("TestRunName", testRunName);
                }
                else
                {
                    filter = filter & Builders<TestRun>.Filter.Eq("TestRunName", testRunName);
                }
            }
            if (testSuiteName.Length > 0)
            {
                if (filter == null)
                {
                    filter = Builders<TestRun>.Filter.Eq("TestSuiteName", testSuiteName);
                }
                else
                {
                    filter = filter & Builders<TestRun>.Filter.Eq("TestSuiteName", testSuiteName);
                }
            }
            if (testName.Length > 0)
            {
                if (filter == null)
                {
                    filter = Builders<TestRun>.Filter.Eq("TestName", testName);
                }
                else
                {
                    filter = filter & Builders<TestRun>.Filter.Eq("TestName", testName);
                }
            }
            try
            {
                filter = filter & Builders<TestRun>.Filter.Eq(u => u.UserName, Users.currentUser);
                var docs = docSearchForElement.Find(filter).Project(simpleProjection).ToList();
                if (docs.Count == 0)
                {
                    if (testName.Length != 0)
                    {
                        List<TestSuite.FieldExpectedResult> testExpectedResultList = TestSuite.GetTestExpectedResults(testName);
                        
                        foreach (TestSuite.FieldExpectedResult field in testExpectedResultList)
                        {
                            GroupedResult groupResult = new GroupedResult("");
                            groupResult.TestName = testName;
                            groupResult.FieldName = field.FieldName;
                            groupResult.ExpectedResult = field.ExpectedResult;
                            returnValue.Add(groupResult);
                        }
                    }
                }
                else
                {
                    docs.ForEach(doc =>
                    {
                        string tempString = doc.AsBsonValue.ToJson();
                        TestRun testRun = JsonConvert.DeserializeObject<TestRun>(tempString);
                        List<TestSuite.FieldExpectedResult> expectedResults = TestSuite.GetTestExpectedResults(testRun.TestName);
                        foreach (TestResult result in testRun.TestResultList)
                        {
                            GroupedResult activeTest = returnValue.Find(x => x.TestName == testRun.TestName);
                            bool foundFieldFlag = false;
                            if (activeTest == null)
                            {
                                GroupedResult groupResult = new GroupedResult(testRun.TestName);
                                TestSuite.FieldExpectedResult activeExpectedResult = expectedResults.Find(x => x.FieldName == result.FieldName);
                                if (activeExpectedResult != null)
                                {
                                    groupResult.ExpectedResult = activeExpectedResult.ExpectedResult;
                                }
                                groupResult.FieldName = result.FieldName;
                                groupResult.FieldValue1 = result.ActualValue;
                                returnValue.Add(groupResult);
                            }
                            else
                            {
                                for (int c = 0; c < returnValue.Count(); c++)
                                {
                                    if (returnValue[c].FieldName == result.FieldName)
                                    {
                                        foundFieldFlag = true;
                                        if (returnValue[c].FieldValue1 == null) returnValue[c].FieldValue1 = result.ActualValue;
                                        else if (returnValue[c].FieldValue2 == null) returnValue[c].FieldValue2 = result.ActualValue;
                                        else if (returnValue[c].FieldValue3 == null) returnValue[c].FieldValue3 = result.ActualValue;
                                        else if (returnValue[c].FieldValue4 == null) returnValue[c].FieldValue4 = result.ActualValue;
                                        else if (returnValue[c].FieldValue5 == null) returnValue[c].FieldValue5 = result.ActualValue;
                                        else if (returnValue[c].FieldValue6 == null) returnValue[c].FieldValue6 = result.ActualValue;
                                        else if (returnValue[c].FieldValue7 == null) returnValue[c].FieldValue7 = result.ActualValue;
                                        else if (returnValue[c].FieldValue8 == null) returnValue[c].FieldValue8 = result.ActualValue;
                                        else if (returnValue[c].FieldValue9 == null) returnValue[c].FieldValue9 = result.ActualValue;
                                        else if (returnValue[c].FieldValue10 == null) returnValue[c].FieldValue10 = result.ActualValue;
                                    }
                                }
                                if (!foundFieldFlag)
                                {
                                    GroupedResult groupResult = new GroupedResult(testRun.TestName);
                                    TestSuite.FieldExpectedResult activeExpectedResult = expectedResults.Find(x => x.FieldName == result.FieldName);
                                    if (activeExpectedResult != null)
                                    {
                                        groupResult.ExpectedResult = activeExpectedResult.ExpectedResult;
                                    }
                                    groupResult.FieldName = result.FieldName;
                                    groupResult.FieldValue1 = result.ActualValue;
                                    returnValue.Add(groupResult);
                                }
                            }
                        }
                    //MessageBox.Show(returnValue.ToString());
                });
                }
            }
            catch (Exception e)
            {
                Utilities.WriteLogItem("GetTestRunList(testSuiteName, testName) failed; " + e.ToString(), TraceEventType.Error);
            }
            return returnValue;
        }

        public static void AddTestResult(string testRunName, string testSuiteName, string testName, int incrementNr, string fieldName, string expectedValue, string actualValue, string result)
        {
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("JSONAPI");
            var docTestStatus = database.GetCollection<TestRun>("colTestRuns");
            try
            {
                TestResult newTestList = new TestResult(fieldName, testRunName, incrementNr, expectedValue, actualValue, result);
                TestRun testRun = GetExecutedRunList(testRunName, testSuiteName, testName);
                testRun.TestResultList.Add(newTestList);
                //DeleteTestSuite(testSuiteName);
                //docTestStatus.InsertOne(testRunList);
                var testRunNameFilter = Builders<TestRun>.Filter
                    .Eq(u => u.TestRunName, testRunName);
                var testSuiteNameFilter = Builders<TestRun>.Filter
                    .Eq(u => u.TestSuiteName, testSuiteName);
                var testNameFilter = Builders<TestRun>.Filter
                    .Eq(u => u.TestName, testName);
                var filter = testRunNameFilter & testSuiteNameFilter & testNameFilter & Builders<TestRun>.Filter.Eq(u => u.UserName, Users.currentUser);
                ProjectionDefinition<TestRun, TestRun> projection = Builders<TestRun>.Projection.Exclude("_id");
                var options = new FindOneAndReplaceOptions<TestRun>()
                {
                    Projection = projection,
                    ReturnDocument = ReturnDocument.After
                };
                docTestStatus.FindOneAndReplace(filter, testRun, options);
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.ToString());
                Utilities.WriteLogItem("AddTestResult failed; " + e.ToString(), TraceEventType.Error);
            }
        }

        public static void DeleteTestRun(string testRunName)
        {
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("JSONAPI");
            var docSearchForElement = database.GetCollection<TestRun>("colTestRuns");

            try
            {
                var filter = Builders<TestRun>.Filter.Eq("TestRunName", testRunName) & Builders<TestRun>.Filter.Eq(u => u.UserName, Users.currentUser);
                docSearchForElement.DeleteOne(filter);
            }
            catch (Exception e)
            {
                Utilities.WriteLogItem("DeleteTestRun failed; " + e.ToString(), TraceEventType.Error);
            }
        }
    }
}