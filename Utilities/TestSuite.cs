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
    using System.ComponentModel;

    public static class TestSuite
    {
        private static string connectionString = System.Environment.GetEnvironmentVariable("mongoDBConnectionString", EnvironmentVariableTarget.User);
        public class TestRunList
        {
            public string TestSuiteName { get; set; }
            public string LastExecuted { get; set; }
            public string TestStatus { get; set; }
            public string UserName { get; set; }
            public bool IncludeOutputResults { get; set; }
            public string RunOutput { get; set; }
            public List<TestList> TestList { get; set; }
            public TestRunList(string testSuiteName, string lastExecuted, string testStatus, List<TestList> testSuiteList, bool includeOutputResults, string runOutput)
            {
                TestSuiteName = testSuiteName;
                LastExecuted = lastExecuted;
                TestStatus = testStatus;
                TestList = testSuiteList;
                UserName = Users.currentUser;
                IncludeOutputResults = includeOutputResults;
                RunOutput = runOutput;
            }
            public TestRunList()
            {
                TestList = new List<TestList>();
            }
        }

        public class TestList
        {
            public string TestName { get; set; }
            public int IncrementCounter { get; set; }
            public string ExecutionStatus { get; set; }
            public string HeaderRequest { get; set; }
            public string BodyRequest { get; set; }
            public string HeaderResponse { get; set; }
            public string BodyResponse { get; set; }
            //Only for export of testresults
            public string TestSuiteName { get; set; }
            public int ConcurrentUserPercentage { get; set; }
            public List<ExecutionResult.TestResult> TestResultList { get; set; }
            public TestList(string testName, int incrementCounter, string executionStatus, string headerRequest, string bodyRequest, string headerResponse, string bodyResponse, string testSuiteName, int conPerc)
            {
                TestName = testName;
                IncrementCounter = incrementCounter;
                ExecutionStatus = executionStatus;
                HeaderRequest = headerRequest;
                BodyRequest = bodyRequest;
                HeaderResponse = headerResponse;
                BodyResponse = bodyResponse;
                TestSuiteName = testSuiteName;
                ConcurrentUserPercentage = conPerc;
            }
        }

        public class Test
        {
            public string TestName { get; set; }
            public string UserName { get; set; }
            public string Input { get; set; }
            public string HeaderInput { get; set; }

            public string URL { get; set; }
            public string XPath { get; set; }
            public string Operation { get; set; }
            public int Sequence { get; set; }

            public List<FieldExpectedResult> FieldExpectedResultList { get; set; }

            public Test()
            {
                TestName = "";
                UserName = "";
                Input = "";
                HeaderInput = "";
                FieldExpectedResultList = new List<FieldExpectedResult>();
                URL = "";
                XPath = "";
                Operation = "";
                Sequence = 0;
            }

            public Test(string testName)
            {
                TestName = testName;
                Input = "";
                HeaderInput = "";
                UserName = Users.currentUser;
                FieldExpectedResultList = new List<FieldExpectedResult>();
                URL = "";
                XPath = "";
                Operation = "";
                Sequence = 0;
            }

            public Test(string testName, string headerInput, string input)
            {
                TestName = testName;
                Input = input;
                HeaderInput = headerInput;
                UserName = Users.currentUser;
                FieldExpectedResultList = new List<FieldExpectedResult>();
                URL = "";
                XPath = "";
                Operation = "";
                Sequence = 0;
            }

            public Test(string testName, string headerInput, string input, int sequence, string xpath)
            {
                TestName = testName;
                Input = input;
                HeaderInput = headerInput;
                UserName = Users.currentUser;
                FieldExpectedResultList = new List<FieldExpectedResult>();
                URL = "";
                XPath = xpath;
                Operation = "";
                Sequence = sequence;
            }



            public Test(string testName, List<FieldExpectedResult> expectedResultList)
            {
                TestName = testName;
                Input = "";
                HeaderInput = "";
                UserName = Users.currentUser;
                FieldExpectedResultList = expectedResultList;
                URL = "";
                XPath = "";
                Operation = "";
                Sequence = 0;
            }
        }

        public class FieldExpectedResult
        {
            public string FieldName { get; set; }
            public string ExpectedResult { get; set; }

            public FieldExpectedResult(string fieldName, string expectedResult)
            {
                FieldName = fieldName;
                ExpectedResult = expectedResult;
            }
            public FieldExpectedResult()
            {
                FieldName = "";
                ExpectedResult = "";
            }

            public FieldExpectedResult(string fieldName)
            {
                FieldName = fieldName;
                ExpectedResult = "";
            }
        }

        /* ===============================================
         * Test
         * ===============================================*/
        public static void NewExpectedResult(string testName, List<FieldExpectedResult> fieldList)
        {
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("JSONAPI");
            var docNewTest = database.GetCollection<Test>("colTests");
            Test test = new Test(testName, fieldList);

            List<FieldExpectedResult> testList = GetTestExpectedResults(testName);
            if (testList == null)
            {
                docNewTest.InsertOne(test);
            }
            else
            {
                var testNameFilter = Builders<Test>.Filter.Eq(u => u.TestName, testName) & Builders<Test>.Filter.Eq(u => u.UserName, Users.currentUser);

                ProjectionDefinition<Test, Test> projection = Builders<Test>.Projection.Exclude("_id");
                var options = new FindOneAndReplaceOptions<Test>()
                {
                    Projection = projection,
                    ReturnDocument = ReturnDocument.After,
                    IsUpsert = true
                };

                docNewTest.FindOneAndReplace(testNameFilter, test, options);
            }
        }

        public static bool NewTestWithDetail(string testName, string headerInput, string input)
        {
            bool returnValue = true;
            try
            {
                var settings = MongoClientSettings.FromConnectionString(connectionString);
                var client = new MongoClient(settings);
                var database = client.GetDatabase("JSONAPI");
                var docNewTest = database.GetCollection<Test>("colTests");

                if (!IsValidTestName(testName))
                {
                    Test test = new Test(testName, headerInput, input);
                    docNewTest.InsertOne(test);
                }
                else
                {
                    if (headerInput.Length > 0) AddHeaderInput(testName, headerInput);
                    if (input.Length > 0) AddInput(testName, input);
                }
            }
            catch (Exception e)
            {
                Logs.NewLogItem("NewTestWithDetai error:" + e.Message, TraceEventType.Error);
                returnValue = false;
            }
            return returnValue;
        }

        public static bool NewTestWithDetail(string testName, string headerInput, string input, int sequence, string xpath)
        {
            bool returnValue = true;
            try
            {
                var settings = MongoClientSettings.FromConnectionString(connectionString);
                var client = new MongoClient(settings);
                var database = client.GetDatabase("JSONAPI");
                var docNewTest = database.GetCollection<Test>("colTests");

                if (!IsValidTestName(testName, sequence))
                {
                    Test test = new Test(testName, headerInput, input, sequence, xpath);
                    docNewTest.InsertOne(test);
                }
                else
                {
                    if (headerInput.Length > 0) AddHeaderInput(testName, headerInput);
                    if (input.Length > 0) AddInput(testName, input);
                }
            }
            catch (Exception e)
            {
                Logs.NewLogItem("NewTestWithDetai error:" + e.Message, TraceEventType.Error);
                returnValue = false;
            }
            return returnValue;
        }

        public static void AddExpectedResult(string testName, string fieldName, string expectedValue)
        {
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("JSONAPI");
            var docNewTestSuite = database.GetCollection<BsonDocument>("colTests");
            ExecutionResult.TestResult test = new ExecutionResult.TestResult(fieldName, "", 0, expectedValue, "", "");
            List<ExecutionResult.TestResult> testList = new List<ExecutionResult.TestResult>();
            testList.Add(test);
            docNewTestSuite.InsertOne(test.ToBsonDocument());
        }

        public static void AddInput(string testName, string input)
        {
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("JSONAPI");
            var testCollection = database.GetCollection<Test>("colTests");

            // projection stage
            var testNameFilter = Builders<Test>.Filter.Eq(u => u.TestName, testName) & Builders<Test>.Filter.Eq(u => u.UserName, Users.currentUser);

            try
            {
                var arrayUpdate = Builders<Test>.Update.Set("Input", input);
                testCollection.UpdateOne(testNameFilter, arrayUpdate);
            }
            catch (Exception e)
            {
                Utilities.WriteLogItem("AddInput(string testName, string input) failed; " + e.ToString(), TraceEventType.Error);
            }
        }

        public static void AddGUITestAttributes(int sequence, string testName, string oldTestName, string url, string xpath, string operation, string inputData)
        {
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("JSONAPI");
            var testCollection = database.GetCollection<Test>("colTests");

            // projection stage
            var testNameFilter = Builders<Test>.Filter.Eq(u => u.TestName, oldTestName) & Builders<Test>.Filter.Eq(u => u.UserName, Users.currentUser);

            try
            {
                var arrayUpdate = Builders<Test>.Update.Set("URL", url).Set("XPath", xpath).Set("Operation", operation).Set("Sequence", sequence).Set("InputData", inputData).Set("TestName", testName);
                testCollection.UpdateOne(testNameFilter, arrayUpdate);
            }
            catch (Exception e)
            {
                Utilities.WriteLogItem("AddGUITestAttributes(string testName, string input, string url, string xpath, string operation) failed; " + e.ToString(), TraceEventType.Error);
            }
        }

        public static void AddHeaderInput(string testName, string headerInput)
        {
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("JSONAPI");
            var testCollection = database.GetCollection<Test>("colTests");

            // projection stage
            var testNameFilter = Builders<Test>.Filter.Eq(u => u.TestName, testName) & Builders<Test>.Filter.Eq(u => u.UserName, Users.currentUser);

            try
            {
                var arrayUpdate = Builders<Test>.Update.Set("HeaderInput", headerInput);
                testCollection.UpdateOne(testNameFilter, arrayUpdate);
            }
            catch (Exception e)
            {
                Utilities.WriteLogItem("AddHeaderInput(string testName, string headerInput) failed; " + e.ToString(), TraceEventType.Error);
            }
        }

        public static bool TestExist(string testName)
        {
            bool returnValue = false;
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("JSONAPI");
            var testListCollection = database.GetCollection<Test>("colTests");

            var testFilter = Builders<Test>.Filter.Eq(u => u.TestName, testName);
            // projection stage
            var simpleProjection = Builders<Test>.Projection
                .Exclude("_id");

            var docs = testListCollection.Find(testFilter).Project(simpleProjection).ToList();
            //var docs = testListCollection.Find<BsonDocument>(tcFilter).Project<TestRunList>(simpleProjection).ToList();
            docs.ForEach(doc =>
            {
                string tempString = doc.AsBsonValue.ToJson();
                var testObj = JsonConvert.DeserializeObject<Test>(tempString);
                var tempTestName = testObj.TestName;
                if (tempTestName == testName) returnValue = true;
            });
            return returnValue;
        }

        public static string GetTestInput(string testName)
        {
            string returnValue = "";
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("JSONAPI");
            var testListCollection = database.GetCollection<Test>("colTests");
            var inputFilter = Builders<Test>.Filter.Eq(u => u.TestName, testName) & Builders<Test>.Filter.Eq(u => u.UserName, Users.currentUser);

            // projection stage
            var simpleProjection = Builders<Test>.Projection
                //.Include(t => t.TestSuiteList)
                .Exclude("_id");
            try
            {
                var docs = testListCollection.Find(inputFilter).Project(simpleProjection).ToList();
                //var docs = testListCollection.Find<BsonDocument>(tcFilter).Project<TestRunList>(simpleProjection).ToList();
                docs.ForEach(doc =>
                {
                    string tempString = doc.AsBsonValue.ToJson();
                    returnValue = JsonConvert.DeserializeObject<Test>(tempString).Input;
                });
            }
            catch (Exception e)
            {
                Utilities.WriteLogItem("GetTestInput failed; " + e.ToString(), TraceEventType.Error);
            }
            return returnValue;
        }

        public static string GetTestHeaderInput(string testName)
        {
            string returnValue = "";
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("JSONAPI");
            var testListCollection = database.GetCollection<Test>("colTests");
            var inputFilter = Builders<Test>.Filter.Eq(u => u.TestName, testName) & Builders<Test>.Filter.Eq(u => u.UserName, Users.currentUser);

            // projection stage
            var simpleProjection = Builders<Test>.Projection
                //.Include(t => t.TestSuiteList)
                .Exclude("_id");
            try
            {
                var docs = testListCollection.Find(inputFilter).Project(simpleProjection).ToList();
                //var docs = testListCollection.Find<BsonDocument>(tcFilter).Project<TestRunList>(simpleProjection).ToList();
                docs.ForEach(doc =>
                {
                    string tempString = doc.AsBsonValue.ToJson();
                    returnValue = JsonConvert.DeserializeObject<Test>(tempString).HeaderInput;
                });
            }
            catch (Exception e)
            {
                Utilities.WriteLogItem("GetTestHeaderInput failed; " + e.ToString(), TraceEventType.Error);
            }
            return returnValue;
        }

        public static List<Test> GetTests()
        {
            List<Test> returnList = new List<Test>();
            Test arrayOfStrings;
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("JSONAPI");
            var testListCollection = database.GetCollection<Test>("colTests");
            var userNameFilter = Builders<Test>.Filter.Eq(u => u.UserName, Users.currentUser);

            // projection stage
            var simpleProjection = Builders<Test>.Projection
                //.Include(t => t.TestSuiteList)
                .Exclude("_id");
            try
            {
                var docs = testListCollection.Find(userNameFilter).Project(simpleProjection).ToList();
                //var docs = testListCollection.Find<BsonDocument>(tcFilter).Project<TestRunList>(simpleProjection).ToList();
                docs.ForEach(doc =>
                {
                    string tempString = doc.AsBsonValue.ToJson();
                    arrayOfStrings = JsonConvert.DeserializeObject<Test>(tempString);
                    int index = returnList.FindIndex(item => item.TestName == arrayOfStrings.TestName);
                    if (index == -1) returnList.Add(arrayOfStrings);
                });
            }
            catch (Exception e)
            {
                Utilities.WriteLogItem("GetTests failed; " + e.ToString(), TraceEventType.Error);
            }
            return returnList;
        }

        public static List<Test> GetGUITests()
        {
            List<Test> returnList = new List<Test>();
            Test arrayOfStrings;
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("JSONAPI");
            var testListCollection = database.GetCollection<Test>("colTests");
            var userNameFilter = Builders<Test>.Filter.Eq(u => u.UserName, Users.currentUser);

            // projection stage
            var simpleProjection = Builders<Test>.Projection
                //.Include(t => t.TestSuiteList)
                .Exclude("_id");
            try
            {
                var docs = testListCollection.Find(userNameFilter).Project(simpleProjection).ToList();
                //var docs = testListCollection.Find<BsonDocument>(tcFilter).Project<TestRunList>(simpleProjection).ToList();
                docs.ForEach(doc =>
                {
                    string tempString = doc.AsBsonValue.ToJson();
                    arrayOfStrings = JsonConvert.DeserializeObject<Test>(tempString);
                    if (arrayOfStrings.XPath.Length > 0) returnList.Add(arrayOfStrings);
                });
            }
            catch (Exception e)
            {
                Utilities.WriteLogItem("GetGUITests failed; " + e.ToString(), TraceEventType.Error);
            }
            return returnList;
        }
        public static List<Test> GetGUITests(bool returnUnique)
        {
            List<Test> returnList = new List<Test>();
            Test arrayOfStrings;
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("JSONAPI");
            var testListCollection = database.GetCollection<Test>("colTests");
            var userNameFilter = Builders<Test>.Filter.Eq(u => u.UserName, Users.currentUser);

            // projection stage
            var simpleProjection = Builders<Test>.Projection
                //.Include(t => t.TestSuiteList)
                .Exclude("_id");
            try
            {
                var docs = testListCollection.Find(userNameFilter).Project(simpleProjection).ToList();
                //var docs = testListCollection.Find<BsonDocument>(tcFilter).Project<TestRunList>(simpleProjection).ToList();
                docs.ForEach(doc =>
                {
                    string tempString = doc.AsBsonValue.ToJson();
                    arrayOfStrings = JsonConvert.DeserializeObject<Test>(tempString);
                    if (arrayOfStrings.XPath.Length > 0)
                    {
                        int index = returnList.FindIndex(item => item.TestName == arrayOfStrings.TestName);
                        if (index == -1) returnList.Add(arrayOfStrings);
                    }
                });
            }
            catch (Exception e)
            {
                Utilities.WriteLogItem("GetGUITests(returnUnique) failed; " + e.ToString(), TraceEventType.Error);
            }
            return returnList;
        }

        public static bool IsGUITest(string testName)
        {
            bool returnValue = false;
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("JSONAPI");
            var testListCollection = database.GetCollection<Test>("colTests");

            var variableFilter = Builders<Test>.Filter.Eq(u => u.TestName, testName);
            // projection stage
            var simpleProjection = Builders<Test>.Projection
                .Exclude("_id");

            var docs = testListCollection.Find(variableFilter).Project(simpleProjection).ToList();
            //var docs = testListCollection.Find<BsonDocument>(tcFilter).Project<TestRunList>(simpleProjection).ToList();
            docs.ForEach(doc =>
            {
                string tempString = doc.AsBsonValue.ToJson();
                var testObj = JsonConvert.DeserializeObject<Test>(tempString);
                var tempTestName = testObj.TestName;
                var tempXPath = testObj.XPath;
                if (tempTestName == testName && tempXPath.Length > 0) returnValue = true;
            });
            return returnValue;
        }

        public class SortBySequence : IComparer<Test>
        {
            public int Compare(Test x, Test y)
            {
                return x.Sequence.CompareTo(y.Sequence);
            }
        }

        public static List<Test> GetGUITests(string testName)
        {
            List<Test> returnList = new List<Test>();
            Test arrayOfStrings;
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("JSONAPI");
            var testListCollection = database.GetCollection<Test>("colTests");
            var testFilter = Builders<Test>.Filter.Eq(u => u.UserName, Users.currentUser) & Builders<Test>.Filter.Eq(u => u.TestName, testName);

            // projection stage
            var simpleProjection = Builders<Test>.Projection
                //.Include(t => t.TestSuiteList)
                .Exclude("_id");
            try
            {
                var docs = testListCollection.Find(testFilter).Project(simpleProjection).ToList();
                //var docs = testListCollection.Find<BsonDocument>(tcFilter).Project<TestRunList>(simpleProjection).ToList();
                docs.ForEach(doc =>
                {
                    string tempString = doc.AsBsonValue.ToJson();
                    arrayOfStrings = JsonConvert.DeserializeObject<Test>(tempString);
                    if (arrayOfStrings.XPath.Length > 0) returnList.Add(arrayOfStrings);
                });
                SortBySequence sortBySequence = new SortBySequence();
                returnList.Sort(sortBySequence);
            }
            catch (Exception e)
            {
                Utilities.WriteLogItem("GetGUITests(testName) failed; " + e.ToString(), TraceEventType.Error);
            }
            return returnList;
        }

        public static List<FieldExpectedResult> GetTestExpectedResults(string testName)
        {
            List<FieldExpectedResult> returnList = new List<FieldExpectedResult>();
            Test arrayOfStrings;
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("JSONAPI");
            var testListCollection = database.GetCollection<Test>("colTests");

            // projection stage
            var testNameFilter = Builders<Test>.Filter.Eq(u => u.TestName, testName) & Builders<Test>.Filter.Eq(u => u.UserName, Users.currentUser);
            var simpleProjection = Builders<Test>.Projection
                //.Include(t => t.TestSuiteList)
                .Exclude("_id");
            try
            {
                var docs = testListCollection.Find(testNameFilter).Project(simpleProjection).ToList();
                //var docs = testListCollection.Find<BsonDocument>(tcFilter).Project<TestRunList>(simpleProjection).ToList();
                docs.ForEach(doc =>
                {
                    string tempString = doc.AsBsonDocument.ToJson();
                    arrayOfStrings = JsonConvert.DeserializeObject<Test>(tempString);
                    foreach (FieldExpectedResult field in arrayOfStrings.FieldExpectedResultList)
                        returnList.Add(field);
                });
            }
            catch (Exception e)
            {
                Utilities.WriteLogItem("GetTestExpectedResults(string testName) failed; " + e.ToString(), TraceEventType.Error);
            }
            return returnList;
        }

        public static string GetFieldTestExpectedResults(string testName, string fieldName)
        {
            string returnList = "";
            Test arrayOfStrings;
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("JSONAPI");
            var testListCollection = database.GetCollection<Test>("colTests");

            // projection stage
            var testNameFilter = Builders<Test>.Filter.Eq(u => u.TestName, testName) & Builders<Test>.Filter.Eq(u => u.UserName, Users.currentUser);
            var fieldNameFilter = Builders<Test>.Filter.Eq("FieldExpectedResultList.FieldName", fieldName);
            var filter = testNameFilter & fieldNameFilter;
            var simpleProjection = Builders<Test>.Projection
                //.Include(t => t.TestSuiteList)
                .Exclude("_id");
            try
            {
                var docs = testListCollection.Find(filter).Project(simpleProjection).ToList();
                //var docs = testListCollection.Find<BsonDocument>(tcFilter).Project<TestRunList>(simpleProjection).ToList();
                docs.ForEach(doc =>
                {
                    string tempString = doc.AsBsonValue.ToJson();
                    arrayOfStrings = JsonConvert.DeserializeObject<Test>(tempString);
                    foreach (FieldExpectedResult field in arrayOfStrings.FieldExpectedResultList)
                        returnList = field.ExpectedResult;
                });
            }
            catch (Exception e)
            {
                Utilities.WriteLogItem("GetFieldTestExpectedResults(testName, fieldName) failed; " + e.ToString(), TraceEventType.Error);
            }
            return returnList;
        }

        public static void DeleteTest(string testName)
        {
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("JSONAPI");
            var docSearchForElement = database.GetCollection<Test>("colTests");

            try
            {
                var testNameFilter = Builders<Test>.Filter.Eq(u => u.TestName, testName) & Builders<Test>.Filter.Eq(u => u.UserName, Users.currentUser);
                docSearchForElement.DeleteOne(testNameFilter);
            }
            catch (Exception e)
            {
                Utilities.WriteLogItem("DeleteTest(testName); " + e.ToString(), TraceEventType.Error);
            }
        }
        public static void DeleteTest(string testName, int sequenceNr)
        {
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("JSONAPI");
            var docSearchForElement = database.GetCollection<Test>("colTests");

            try
            {
                var testNameFilter = Builders<Test>.Filter.Eq(u => u.TestName, testName) & Builders<Test>.Filter.Eq(u => u.UserName, Users.currentUser) & Builders<Test>.Filter.Eq(u => u.Sequence, sequenceNr);
                docSearchForElement.DeleteOne(testNameFilter);
            }
            catch (Exception e)
            {
                Utilities.WriteLogItem("DeleteTest(testName, sequenceNr); " + e.ToString(), TraceEventType.Error);
            }
        }

        public static void DeleteTestList(string testName, string fieldName)
        {
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("JSONAPI");
            var docSearchForElement = database.GetCollection<Test>("colTests");

            try
            {
                var arrayFilter = Builders<Test>.Filter.Eq("TestName", testName) &
                     Builders<Test>.Filter.Eq("FieldList.FieldName", fieldName) &
                     Builders<Test>.Filter.Eq(u => u.UserName, Users.currentUser);
                docSearchForElement.DeleteOne(arrayFilter);
            }
            catch (Exception e)
            {
                Utilities.WriteLogItem("DeleteTestList(testName, fieldName); " + e.ToString(), TraceEventType.Error);
            }
        }

        public static bool UpdateExpectedResults(string testName, List<FieldExpectedResult> fieldList)
        {
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("JSONAPI");
            var docTestStatus = database.GetCollection<Test>("colTests");
            bool returnValue = false;
            try
            {
                var testNameFilter = Builders<Test>.Filter.Eq(u => u.TestName, testName) & Builders<Test>.Filter.Eq(u => u.UserName, Users.currentUser);

                ProjectionDefinition<Test, Test> projection = Builders<Test>.Projection.Exclude("_id");
                var options = new FindOneAndReplaceOptions<Test>()
                {
                    Projection = projection,
                    ReturnDocument = ReturnDocument.After,
                    IsUpsert = true
                };
                Test testList = new Test(testName, fieldList);
                docTestStatus.FindOneAndReplace(testNameFilter, testList, options);
                returnValue = true;
            }
            catch (Exception e)
            {
                Utilities.WriteLogItem("UpdateTestFields failed; " + e.ToString(), TraceEventType.Error);
                returnValue = false;
            }
            return returnValue;
        }

        public static void UpdateExpectedResult(string testName, string fieldName, string expectedResult)
        {
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("JSONAPI");
            var docTestStatus = database.GetCollection<Test>("colTests");

            try
            {
                var testNameFilter = Builders<Test>.Filter.Eq(u => u.TestName, testName) & Builders<Test>.Filter.Eq(u => u.UserName, Users.currentUser);
                var fieldNameFilter = Builders<Test>.Filter.Eq("FieldList.TestName", fieldName);
                var filter = testNameFilter & fieldNameFilter;
                var arrayUpdate = Builders<Test>.Update.Set("FieldList.$.ExpectedResult", expectedResult);
                docTestStatus.UpdateOne(filter, arrayUpdate);
            }
            catch (Exception e)
            {
                Utilities.WriteLogItem("UpdateTestFields failed; " + e.ToString(), TraceEventType.Error);
            }
        }

        /* ===============================================
        * TestSuite
        * ===============================================*/
        public static void NewTestSuiteDocument(TestRunList testRunList)
        {
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("JSONAPI");
            var docNewTestSuite = database.GetCollection<BsonDocument>("colTestSuites");
            testRunList.UserName = Users.currentUser;
            /*var testRunList = new TestRunList() { TestSuiteName = testSuiteName, LastExecuted = lastExecuted, TestStatus = testStatus };
            foreach (var testItem in testList)
            {
                testRunList.TestSuiteList.Add(testItem);
            }*/

            docNewTestSuite.InsertOne(testRunList.ToBsonDocument());
        }

        public static List<TestRunList> GetTestRunList()
        {
            List<TestRunList> returnList = new List<TestRunList>();
            TestRunList arrayOfStrings;
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("JSONAPI");
            var docSearchForElement = database.GetCollection<TestRunList>("colTestSuites");
            try
            {
                var filter = Builders<TestRunList>.Filter.Eq(u => u.UserName, Users.currentUser);
                var docs = docSearchForElement.Find(filter).Project("{_id: 0}").ToList();
                docs.ForEach(doc =>
                {
                    string tempString = doc.AsBsonValue.ToJson();
                    arrayOfStrings = JsonConvert.DeserializeObject<TestRunList>(tempString);
                    returnList.Add(arrayOfStrings);
                });
            }
            catch (Exception e)
            {
                Utilities.WriteLogItem("GetTestRun() failed; " + e.ToString(), TraceEventType.Error);
            }
            return returnList;
        }

        public static List<String> GetTestSuiteNames()
        {
            List<string> returnList = new List<string>();

            var settings = MongoClientSettings.FromConnectionString(connectionString);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("JSONAPI");
            var docSearchForElement = database.GetCollection<TestRunList>("colTestSuites");
            var simpleProjection = Builders<TestRunList>.Projection
                .Include(t => t.TestSuiteName)
                .Exclude("_id");
            try
            {
                var filter = Builders<TestRunList>.Filter.Eq(u => u.UserName, Users.currentUser);
                var docs = docSearchForElement.Find(filter).Project(simpleProjection).ToList();
                docs.ForEach(doc =>
                {
                    string tempString = doc.AsBsonValue.ToJson();
                    var tempValue = JsonConvert.DeserializeObject<TestRunList>(tempString);
                    if (!returnList.Contains(tempValue.TestSuiteName))
                        returnList.Add(tempValue.TestSuiteName);
                });
            }
            catch (Exception e)
            {
                Utilities.WriteLogItem("GetSuiteNames() failed; " + e.ToString(), TraceEventType.Error);
            }
            return returnList;
        }

        public static TestRunList GetTestSuiteDetails(string testSuiteName)
        {
            TestRunList returnValue = new TestRunList();
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("JSONAPI");
            var docSearchForElement = database.GetCollection<TestRunList>("colTestSuites");
            var simpleProjection = Builders<TestRunList>.Projection
                .Exclude("_id");
            try
            {
                var filter = Builders<TestRunList>.Filter.Eq(u => u.UserName, Users.currentUser) & Builders<TestRunList>.Filter.Eq(u => u.UserName, Users.currentUser);
                var docs = docSearchForElement.Find(filter).Project(simpleProjection).ToList();
                docs.ForEach(doc =>
                {
                    string tempString = doc.AsBsonValue.ToJson();
                    returnValue = JsonConvert.DeserializeObject<TestRunList>(tempString);
                });
            }
            catch (Exception e)
            {
                Utilities.WriteLogItem("GGetTestSuiteDetails(testSuiteName) failed; " + e.ToString(), TraceEventType.Error);
            }
            return returnValue;
        }

        public static bool IsValidTestSuiteName(string testSuiteName)
        {
            bool returnValue = false;

            var settings = MongoClientSettings.FromConnectionString(connectionString);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("JSONAPI");
            var docSearchForElement = database.GetCollection<TestRunList>("colTestSuites");

            try
            {
                var filter = Builders<TestRunList>.Filter.Eq(u => u.TestSuiteName, testSuiteName);
                var docs = docSearchForElement.Find(filter).CountDocuments();
                if (docs == 1) returnValue = true;
            }
            catch (Exception e)
            {
                Utilities.WriteLogItem("IsValidTestSuiteName() failed; " + e.ToString(), TraceEventType.Error);
            }
            return returnValue;
        }

        public static bool IsValidTestName(string testName)
        {
            bool returnValue = false;

            var settings = MongoClientSettings.FromConnectionString(connectionString);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("JSONAPI");
            var docSearchForElement = database.GetCollection<Test>("colTests");

            try
            {
                var filter = Builders<Test>.Filter.Eq(u => u.TestName, testName);
                var docs = docSearchForElement.Find(filter).CountDocuments();
                if (docs == 1) returnValue = true;
            }
            catch (Exception e)
            {
                Utilities.WriteLogItem("IsValidTestName(testName) failed; " + e.ToString(), TraceEventType.Error);
            }
            return returnValue;
        }

        public static bool IsValidTestName(string testName, int sequence)
        {
            bool returnValue = false;

            var settings = MongoClientSettings.FromConnectionString(connectionString);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("JSONAPI");
            var docSearchForElement = database.GetCollection<Test>("colTests");

            try
            {
                var filter = Builders<Test>.Filter.Eq(u => u.TestName, testName) & Builders<Test>.Filter.Eq(u => u.Sequence, sequence);
                var docs = docSearchForElement.Find(filter).CountDocuments();
                if (docs == 1) returnValue = true;
            }
            catch (Exception e)
            {
                Utilities.WriteLogItem("IsValidTestName(testName, sequence) failed; " + e.ToString(), TraceEventType.Error);
            }
            return returnValue;
        }

        public static List<String> GetTestNames()
        {
            List<string> returnList = new List<string>();

            var settings = MongoClientSettings.FromConnectionString(connectionString);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("JSONAPI");
            var docSearchForElement = database.GetCollection<TestRunList>("colTestSuites");
            try
            {
                var filter = Builders<TestRunList>.Filter.Eq(u => u.UserName, Users.currentUser);
                var docs = docSearchForElement.Find(filter).Project("{_id: 0}").ToList();
                docs.ForEach(doc =>
                {
                    string tempString = doc.AsBsonDocument.ToJson();
                    TestRunList arrayOfStrings = JsonConvert.DeserializeObject<TestRunList>(tempString);
                    foreach (TestList tempTestList in arrayOfStrings.TestList)
                    {
                        if (!returnList.Contains(tempTestList.TestName))
                        {
                            returnList.Add(tempTestList.TestName);
                        }
                    }
                });
            }
            catch (Exception e)
            {
                Utilities.WriteLogItem("GetTestNames() failed; " + e.ToString(), TraceEventType.Error);
            }
            return returnList;
        }

        public static List<TestRunList> GetTestRunList(string testSuiteName)
        {
            List<TestRunList> returnList = new List<TestRunList>();
            TestRunList arrayOfStrings;
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("JSONAPI");
            var docSearchForElement = database.GetCollection<TestRunList>("colTestSuites");
            var testSuiteNameFilter = Builders<TestRunList>.Filter.Eq(u => u.TestSuiteName, testSuiteName) & Builders<TestRunList>.Filter.Eq(u => u.UserName, Users.currentUser);
            try
            {
                var docs = docSearchForElement.Find(testSuiteNameFilter).Project("{_id: 0}").ToList();
                docs.ForEach(doc =>
                {
                    string tempString = doc.AsBsonValue.ToJson();
                    arrayOfStrings = JsonConvert.DeserializeObject<TestRunList>(tempString);
                    returnList.Add(arrayOfStrings);
                });
            }
            catch (Exception e)
            {
                Utilities.WriteLogItem("GetTestRunList(testSuiteName,runId) failed; " + e.ToString(), TraceEventType.Error);
            }
            return returnList;
        }

        public static List<TestRunList> GetTestRunList(string testSuiteName, string testName, int incrementCounter)
        {
            List<TestRunList> returnList = new List<TestRunList>();
            TestRunList arrayOfStrings;
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("JSONAPI");
            var docSearchForElement = database.GetCollection<TestRunList>("colTestSuites");
            var testSuiteNameFilter = Builders<TestRunList>.Filter.Eq("TestSuiteName", testSuiteName) & Builders<TestRunList>
                  .Filter.Eq("TestList.TestName", testName) & Builders<TestRunList>
                  .Filter.Eq("TestList.IncrementCounter", incrementCounter) & Builders<TestRunList>.Filter.Eq(u => u.UserName, Users.currentUser);
            var simpleProjection = Builders<TestRunList>.Projection
                //.Include(t => t.TestSuiteList)
                .Exclude("_id");
            try
            {
                var docs = docSearchForElement.Find(testSuiteNameFilter).Project(simpleProjection).ToList();
                docs.ForEach(doc =>
                {
                    string tempString = doc.AsBsonValue.ToJson();
                    arrayOfStrings = JsonConvert.DeserializeObject<TestRunList>(tempString);
                    returnList.Add(arrayOfStrings);
                });
            }
            catch (Exception e)
            {
                Utilities.WriteLogItem("GetTestRunList(testSuiteName, testName,incrementCounter) failed; " + e.ToString(), TraceEventType.Error);
            }
            return returnList;
        }

        public static List<TestRunList> GetTestRunList(string testSuiteName, string testName)
        {
            List<TestRunList> returnList = new List<TestRunList>();
            TestRunList arrayOfStrings;
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("JSONAPI");
            var docSearchForElement = database.GetCollection<TestRunList>("colTestSuites");
            var testSuiteNameFilter = Builders<TestRunList>.Filter.Eq("TestSuiteName", testSuiteName) & Builders<TestRunList>
                  .Filter.Eq("TestList.TestName", testName) & Builders<TestRunList>.Filter.Eq(u => u.UserName, Users.currentUser);
            var simpleProjection = Builders<TestRunList>.Projection
                //.Include(t => t.TestSuiteList)
                .Exclude("_id");
            try
            {
                var docs = docSearchForElement.Find(testSuiteNameFilter).Project(simpleProjection).ToList();
                docs.ForEach(doc =>
                {
                    string tempString = doc.AsBsonValue.ToJson();
                    arrayOfStrings = JsonConvert.DeserializeObject<TestRunList>(tempString);
                    returnList.Add(arrayOfStrings);
                });
            }
            catch (Exception e)
            {
                Utilities.WriteLogItem("GetTestRunList(testSuiteName,runId,testName) failed; " + e.ToString(), TraceEventType.Error);
            }
            return returnList;
        }

        public static List<TestList> GetTestList(string testSuiteName)
        {
            List<TestList> returnList = new List<TestList>();
            TestRunList arrayOfStrings;
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("JSONAPI");
            var testListCollection = database.GetCollection<TestRunList>("colTestSuites");

            var testSuiteNameFilter = Builders<TestRunList>.Filter.Eq(u => u.TestSuiteName, testSuiteName) & Builders<TestRunList>.Filter.Eq(u => u.UserName, Users.currentUser);
            // projection stage
            var simpleProjection = Builders<TestRunList>.Projection
                //.Include(t => t.TestSuiteList)
                .Exclude("_id");
            try
            {
                var docs = testListCollection.Find(testSuiteNameFilter).Project(simpleProjection).ToList();
                //var docs = testListCollection.Find<BsonDocument>(tcFilter).Project<TestRunList>(simpleProjection).ToList();
                docs.ForEach(doc =>
                {
                    string tempString = doc.AsBsonValue.ToJson();
                    arrayOfStrings = JsonConvert.DeserializeObject<TestRunList>(tempString);
                    for (int i = 0; i < arrayOfStrings.TestList.Count; i++)
                    {
                        returnList.Add(arrayOfStrings.TestList[i]);
                    }
                });
            }
            catch (Exception e)
            {
                Utilities.WriteLogItem("GetTestList(testSuiteName, runId) failed; " + e.ToString(), TraceEventType.Error);

            }
            return returnList;
        }

        public static List<TestList> GetTestList(string testSuiteName, string testName, int incrementCounter)
        {
            List<TestList> returnList = new List<TestList>();
            TestRunList arrayOfStrings;
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("JSONAPI");
            var testListCollection = database.GetCollection<TestRunList>("colTestSuites");

            var testSuiteNameFilter = Builders<TestRunList>.Filter.Eq(u => u.TestSuiteName, testSuiteName) & Builders<TestRunList>.Filter.Eq(u => u.UserName, Users.currentUser);

            var testNameFilter = Builders<TestRunList>.Filter
                                     .ElemMatch(trList => trList.TestList, Builders<TestList>.Filter.Eq(x => x.TestName, testName));
            var incrementFilter = Builders<TestRunList>.Filter
                                     .ElemMatch(trList => trList.TestList, Builders<TestList>.Filter.Eq(x => x.IncrementCounter, incrementCounter));

            var filter = testSuiteNameFilter & testNameFilter & incrementFilter;
            // projection stage
            var simpleProjection = Builders<TestRunList>.Projection
                .Exclude("_id");
            try
            {
                var docs = testListCollection.Find(filter).Project(simpleProjection).ToList();
                //var docs = testListCollection.Find<BsonDocument>(tcFilter).Project<TestRunList>(simpleProjection).ToList();
                docs.ForEach(doc =>
                {
                    string tempString = doc.AsBsonDocument.ToJson();
                    arrayOfStrings = JsonConvert.DeserializeObject<TestRunList>(tempString);
                    foreach (TestList tempTestList in arrayOfStrings.TestList)
                    {
                        if (tempTestList.TestName == testName & tempTestList.IncrementCounter == incrementCounter)
                        {
                            returnList.Add(tempTestList);
                        }
                    }
                });
            }
            catch (Exception e)
            {
                Utilities.WriteLogItem("GetTestList(testSuiteName, testName, incrementCounter) failed; " + e.ToString(), TraceEventType.Error);

            }
            return returnList;
        }

        public static int GetConcurrentUsers(string testSuiteName, string testName, int incrementCounter)
        {
            int returnList = 100;
            TestRunList arrayOfStrings;
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("JSONAPI");
            var testListCollection = database.GetCollection<TestRunList>("colTestSuites");

            var testSuiteNameFilter = Builders<TestRunList>.Filter.Eq(u => u.TestSuiteName, testSuiteName) & Builders<TestRunList>.Filter.Eq(u => u.UserName, Users.currentUser);

            var testNameFilter = Builders<TestRunList>.Filter
                                     .ElemMatch(trList => trList.TestList, Builders<TestList>.Filter.Eq(x => x.TestName, testName));
            var incrementFilter = Builders<TestRunList>.Filter
                                     .ElemMatch(trList => trList.TestList, Builders<TestList>.Filter.Eq(x => x.IncrementCounter, incrementCounter));

            var filter = testSuiteNameFilter & testNameFilter & incrementFilter;
            // projection stage
            var simpleProjection = Builders<TestRunList>.Projection
                .Exclude("_id");
            try
            {
                var docs = testListCollection.Find(filter).Project(simpleProjection).ToList();
                //var docs = testListCollection.Find<BsonDocument>(tcFilter).Project<TestRunList>(simpleProjection).ToList();
                docs.ForEach(doc =>
                {
                    string tempString = doc.AsBsonDocument.ToJson();
                    arrayOfStrings = JsonConvert.DeserializeObject<TestRunList>(tempString);
                    foreach (TestList tempTestList in arrayOfStrings.TestList)
                    {
                        if (tempTestList.TestName == testName & tempTestList.IncrementCounter == incrementCounter)
                        {
                            returnList = tempTestList.ConcurrentUserPercentage;
                        }
                    }
                });
            }
            catch (Exception e)
            {
                Utilities.WriteLogItem("GetConcurrentUsers(testSuiteName, testName, incrementCounter) failed; " + e.ToString(), TraceEventType.Error);

            }
            return returnList;
        }

        public static List<TestList> GetTestList()
        {
            List<TestList> returnList = new List<TestList>();
            TestRunList arrayOfStrings;
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("JSONAPI");
            var testListCollection = database.GetCollection<TestRunList>("colTestSuites");

            // projection stage
            var simpleProjection = Builders<TestRunList>.Projection
                //.Include(t => t.TestSuiteList)
                .Exclude("_id");
            try
            {
                var filter = Builders<TestRunList>.Filter.Eq(u => u.UserName, Users.currentUser);
                var docs = testListCollection.Find(filter).Project(simpleProjection).ToList();
                //var docs = testListCollection.Find<BsonDocument>(tcFilter).Project<TestRunList>(simpleProjection).ToList();
                docs.ForEach(doc =>
                {
                    string tempString = doc.AsBsonValue.ToJson();
                    arrayOfStrings = JsonConvert.DeserializeObject<TestRunList>(tempString);
                    for (int i = 0; i < arrayOfStrings.TestList.Count; i++)
                    {
                        arrayOfStrings.TestList[i].TestSuiteName = arrayOfStrings.TestSuiteName;
                        returnList.Add(arrayOfStrings.TestList[i]);
                    }
                });
            }
            catch (Exception e)
            {
                Utilities.WriteLogItem("GetTestList failed; " + e.ToString(), TraceEventType.Error);
            }
            return returnList;
        }


        public static void UpdateTestStatus(string testSuiteName, string testName, string testStatus, string headerRequest, string bodyRequest, string headerResponse, string bodyResponse, int incrementCounter)
        {
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("JSONAPI");
            var docTestStatus = database.GetCollection<TestRunList>("colTestSuites");

            try
            {
                var arrayFilter = Builders<TestRunList>.Filter.Eq("TestSuiteName", testSuiteName) & Builders<TestRunList>
                .Filter.Eq("TestList.TestName", testName) & Builders<TestRunList>
                .Filter.Eq("TestList.IncrementCounter", incrementCounter) & Builders<TestRunList>.Filter.Eq(u => u.UserName, Users.currentUser);
                var arrayUpdate = Builders<TestRunList>.Update.Set("TestList.$.ExecutionStatus", testStatus).Set("TestList.$.HeaderRequest", headerRequest).Set("TestList.$.BodyRequest", bodyRequest).Set("TestList.$.HeaderResponse", headerResponse).Set("TestList.$.BodyResponse", bodyResponse).Set("TestList.$.IncrementCounter", incrementCounter);
                docTestStatus.UpdateOne(arrayFilter, arrayUpdate);
            }
            catch (Exception e)
            {
                Utilities.WriteLogItem("UpdateTestStatus failed; " + e.ToString(), TraceEventType.Error);
            }
        }

        public static void UpdateTestStatus2(string testSuiteName, string runId, string testName, string testStatus, string headerResponse, string bodyResponse, int incrementCounter)
        {
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("JSONAPI");
            var docTestStatus = database.GetCollection<TestRunList>("colTestSuites");

            try
            {
                var arrayFilter = Builders<TestRunList>.Filter.Eq("TestSuiteName", testSuiteName) & Builders<TestRunList>.Filter.Eq("RunId", runId) & Builders<TestRunList>
                      .Filter.Eq("TestList.TestName", testName) & Builders<TestRunList>
                      .Filter.Eq("TestList.IncrementCounter", incrementCounter) & Builders<TestRunList>.Filter.Eq(u => u.UserName, Users.currentUser);
                var arrayUpdate = Builders<TestRunList>.Update.Set("TestList.$.ExecutionStatus", testStatus).Set("TestList.$.HeaderResponse", headerResponse).Set("TestList.$.BodyResponse", bodyResponse).Set("TestList.$.IncrementCounter", incrementCounter);

                docTestStatus.UpdateOne(arrayFilter, arrayUpdate);
            }
            catch (Exception e)
            {
                Utilities.WriteLogItem("UpdateTestStatus2 failed; " + e.ToString(), TraceEventType.Error);
            }
        }

        public static void UpdateTestStatus_NewInstance(string testSuiteName, string testName, string testStatus, string headerRequest, string bodyRequest, string headerResponse, string bodyResponse, int incrementCounter, int concPerc)
        {
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("JSONAPI");
            var docTestStatus = database.GetCollection<TestRunList>("colTestSuites");
            //try
            //{
            List<TestRunList> existingTestRunList = GetTestRunList(testSuiteName, testName);
            List<TestList> existingTestList = GetTestList(testSuiteName, testName, incrementCounter);
            bool findFlag = false;
            bool insertFlag = true;

            if (existingTestRunList.Count() > 0)
            {
                foreach (TestRunList testRunList in existingTestRunList)
                {
                    if (existingTestList.Count > 0)
                    {
                        foreach (TestList testList in testRunList.TestList)
                        {
                            if (testList.TestSuiteName == testSuiteName & testList.TestSuiteName == testSuiteName & testList.TestName == testName & testList.IncrementCounter == incrementCounter)
                            {
                                var testSuiteNameFilter = Builders<TestRunList>.Filter
                                                        .Eq(u => u.TestSuiteName, testSuiteName) & Builders<TestRunList>.Filter.Eq(u => u.UserName, Users.currentUser);

                                var testNameFilter = Builders<TestRunList>.Filter
                                                                     .ElemMatch(trList => trList.TestList, Builders<TestList>.Filter.Eq(x => x.TestName, testName));
                                var incrementFilter = Builders<TestRunList>.Filter
                                                                     .ElemMatch(trList => trList.TestList, Builders<TestList>.Filter.Eq(x => x.IncrementCounter, incrementCounter));

                                var filter = testSuiteNameFilter & testNameFilter & incrementFilter;
                                var updateDefinition = Builders<TestRunList>.Update.Set("TestList.$.ExecutionStatus", testStatus)
                                                                                   .Set("TestList.$.HeaderRequest", headerRequest)
                                                                                   .Set("TestList.$.BodyRequest", bodyRequest)
                                                                                   .Set("TestList.$.HeaderResponse", headerResponse)
                                                                                   .Set("TestList.$.BodyResponse", bodyResponse);

                                ProjectionDefinition<TestRunList, TestRunList> projection = Builders<TestRunList>.Projection.Exclude("_id");
                                var options = new FindOneAndUpdateOptions<TestRunList>()
                                {
                                    Projection = projection,
                                    ReturnDocument = ReturnDocument.After,
                                    IsUpsert = true
                                };
                                docTestStatus.FindOneAndUpdate(filter, updateDefinition, options);
                                findFlag = true;
                                insertFlag = false;
                            }
                        }
                    }
                    else
                    {
                        if (insertFlag)
                        {
                            if (!findFlag)
                            {
                                TestList newTestList = new TestList(testName, incrementCounter, testStatus, headerRequest, bodyRequest, headerResponse, bodyResponse, testSuiteName, concPerc);
                                testRunList.TestList.Add(newTestList);
                                //DeleteTestSuite(testSuiteName);
                                //docTestStatus.InsertOne(testRunList);
                                var testSuiteNameFilter = Builders<TestRunList>.Filter
                                    .Eq(u => u.TestSuiteName, testSuiteName) & Builders<TestRunList>.Filter.Eq(u => u.UserName, Users.currentUser);
                                var testNameFilter = Builders<TestRunList>.Filter
                                                                    .ElemMatch(trList => trList.TestList, Builders<TestList>.Filter.Eq(x => x.TestName, testName));
                                var filter = testSuiteNameFilter & testNameFilter;
                                ProjectionDefinition<TestRunList, TestRunList> projection = Builders<TestRunList>.Projection.Exclude("_id");
                                var options = new FindOneAndReplaceOptions<TestRunList>()
                                {
                                    Projection = projection,
                                    ReturnDocument = ReturnDocument.After,
                                    IsUpsert = true
                                };
                                docTestStatus.FindOneAndReplace(filter, testRunList, options);
                                findFlag = true;
                            }
                            insertFlag = false;
                        }
                    }
                }
            }
            //}
            //catch (Exception e)
            //{
            //MessageBox.Show(e.ToString());
            //Utilities.WriteLogItem("UpdateTestStatus_NewInstance failed; " + e.ToString(), TraceEventType.Error);
            //}
        }

        public static void UpdateTestSuite(string testSuiteName, string outputStr, bool includeResults, List<TestList> testRunList)
        {
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("JSONAPI");
            var docTestSuite = database.GetCollection<TestRunList>("colTestSuites");
            try
            {
                var filter = Builders<TestRunList>.Filter
                            .Eq(u => u.TestSuiteName, testSuiteName) & Builders<TestRunList>.Filter.Eq(u => u.UserName, Users.currentUser);


                var updateDefinition = Builders<TestRunList>.Update.Set("TestList", testRunList)
                                                                   .Set("RunOutput", outputStr)
                                                                   .Set("IncludeOutputResults", includeResults);

                ProjectionDefinition<TestRunList, TestRunList> projection = Builders<TestRunList>.Projection.Exclude("_id");
                var options = new FindOneAndUpdateOptions<TestRunList>()
                {
                    Projection = projection,
                    ReturnDocument = ReturnDocument.After,
                    IsUpsert = true
                };
                docTestSuite.FindOneAndUpdate(filter, updateDefinition, options);
            }
            catch (Exception uts)
            {
                Utilities.WriteLogItem("Error with UpdateTestSuite(testSuiteName, outputStr, includeResults,testList); " + uts.ToString(), TraceEventType.Error);
            }
        }

        public static void UpdateTestSuiteStatus(string testSuiteName, string testStatus)
        {
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("JSONAPI");
            var docSearchForElement = database.GetCollection<TestRunList>("colTestSuites");

            try
            {
                var datetimeString = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
                var filter = Builders<TestRunList>.Filter.Eq("TestSuiteName", testSuiteName) & Builders<TestRunList>.Filter.Eq(u => u.UserName, Users.currentUser);
                var update = Builders<TestRunList>.Update.Set("LastExecuted", datetimeString).Set("TestStatus", testStatus);
                //set update
                docSearchForElement.UpdateOne(filter, update);
            }
            catch (Exception e)
            {
                Utilities.WriteLogItem("UpdateTestSuiteStatus failed; " + e.ToString(), TraceEventType.Error);
            }
        }

        public static void UpdateRunOutput(string testSuiteName, string runOutput)
        {
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("JSONAPI");
            var docSearchForElement = database.GetCollection<TestRunList>("colTestSuites");

            try
            {
   
                var filter = Builders<TestRunList>.Filter.Eq("TestSuiteName", testSuiteName) & Builders<TestRunList>.Filter.Eq(u => u.UserName, Users.currentUser);
                var update = Builders<TestRunList>.Update.Set("RunOutput", runOutput);
                //set update
                docSearchForElement.UpdateOne(filter, update);
            }
            catch (Exception e)
            {
                Utilities.WriteLogItem("UpdateRunOutput failed; " + e.ToString(), TraceEventType.Error);
            }
        }

        public static void DeleteTestSuite(string testSuiteName)
        {
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("JSONAPI");
            var docSearchForElement = database.GetCollection<TestRunList>("colTestSuites");

            try
            {
                var filter = Builders<TestRunList>.Filter.Eq("TestSuiteName", testSuiteName) & Builders<TestRunList>.Filter.Eq(u => u.UserName, Users.currentUser);
                docSearchForElement.DeleteOne(filter);
            }
            catch (Exception e)
            {
                Utilities.WriteLogItem("DeleteTestSuite failed; " + e.ToString(), TraceEventType.Error);
            }
        }

        public static void DeleteTestList(string testSuiteName, string testName, int incrementCounter)
        {
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("JSONAPI");
            var docSearchForElement = database.GetCollection<TestRunList>("colTestSuites");

            try
            {
                var arrayFilter = Builders<TestRunList>.Filter.Eq("TestSuiteName", testSuiteName) &
                    Builders<TestRunList>.Filter.Eq("TestList.TestName", testName) &
                    Builders<TestRunList>.Filter.Eq("TestList.IncrementCounter", incrementCounter) &
                    Builders<TestRunList>.Filter.Eq(u => u.UserName, Users.currentUser);
                docSearchForElement.DeleteOne(arrayFilter);
            }
            catch (Exception e)
            {
                Utilities.WriteLogItem("DeleteTestList; " + e.ToString(), TraceEventType.Error);
            }
        }

        public static string ExportTestResultsToCsv(DataGridView dgv, string filename)
        {
            //========Data from textbox==========//        
            string stOutput = "";
            string stLine = "";
            string sHeaders = "";
            string sTestList = "";
            string resultString = "success";
            int lineCount = 0;

            for (int j = 0; j < dgv.Columns.Count; j++)
                if (!dgv.Columns[j].HeaderText.ToLower().Contains("select") && !dgv.Columns[j].HeaderText.ToLower().Contains("tests..") && !dgv.Columns[j].HeaderText.ToLower().Contains("history..") && !dgv.Columns[j].HeaderText.ToLower().Contains("export results") && !dgv.Columns[j].HeaderText.ToLower().Contains("output"))
                    sHeaders = sHeaders.ToString() + Convert.ToString(dgv.Columns[j].HeaderText) + ",";
            sHeaders = sHeaders.ToString() + "Test Name,Increment,Header Request,Body Request,Header Response,Body Response,Execution Status";
            stOutput += sHeaders + "\r\n";
            lineCount++;
            //MessageBox.Show("Headers Set:"+stOutput);
            List<TestList> listTestList = GetTestList();
            // Export data.  
            int rowCounter = 1;
            for (int i = 0; i < dgv.RowCount; i++)
            {
                if ((Boolean)((DataGridViewCheckBoxCell)dgv.Rows[i].Cells["colSelected"]).FormattedValue)
                {
                    for (int k = 0; k < dgv.Rows[i].Cells.Count; k++)
                    {
                        if (!dgv.Columns[k].HeaderText.ToLower().Contains("select") && !dgv.Columns[k].HeaderText.ToLower().Contains("tests..") && !dgv.Columns[k].HeaderText.ToLower().Contains("history..") && !dgv.Columns[k].HeaderText.ToLower().Contains("export results") && !dgv.Columns[k].HeaderText.ToLower().Contains("output"))
                            stLine = stLine + Convert.ToString(dgv.Rows[i].Cells[k].Value) + ",";
                    }
                    for (int j = 0; j < dgv.Rows[i].Cells.Count; j++)
                    {
                        if (dgv.Columns[j].HeaderText.ToLower().Contains("test suite name"))
                        {
                            foreach (TestList item in listTestList)
                            {
                                if (dgv.Rows[i].Cells[j].Value != null)
                                {
                                    if (item.TestSuiteName.ToLower() == dgv.Rows[i].Cells[j].Value.ToString().ToLower())
                                    {
                                        sTestList = stLine + item.TestName + "," + item.IncrementCounter + "," + item.HeaderRequest.Replace("\r\n", "").Replace("\n", "").Replace("\r", "").Replace(",", "").Replace(":", "-") + "," + item.BodyRequest.Replace("\r\n", "").Replace("\n", "").Replace("\r", "").Replace(",", "").Replace(":", "-") + "," + item.HeaderResponse.Replace("\r\n", "").Replace("\n", "").Replace("\r", "").Replace(",", "").Replace(":", "-") + "," + item.BodyResponse.Replace("\r\n", "").Replace("\n", "").Replace("\r", "").Replace(",", "").Replace(":", "-") + "," + item.ExecutionStatus + ",";
                                        //MessageBox.Show(rowCounter.ToString());
                                        ++rowCounter;
                                        if (sTestList.Length > 0)
                                        {
                                            stOutput += sTestList + "\r\n";
                                            //MessageBox.Show("Row: " + i.ToString() +" :" + stOutput);
                                            lineCount++;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    sTestList = "";
                    stLine = "";
                }
            }
            System.Text.UnicodeEncoding utf16 = new UnicodeEncoding();
            FileStream fs = null;
            BinaryWriter bw = null;
            byte[] output = utf16.GetBytes(stOutput);
            try
            {
                fs = new FileStream(filename, System.IO.FileMode.Create);
                bw = new BinaryWriter(fs);
                bw.Write(output, 0, output.Length); //write the encoded file
            }
            catch (Exception e)
            {
                resultString = "Export failed with error: " + e.ToString();
                Utilities.WriteLogItem("Export failed; " + e.ToString(), TraceEventType.Error);
            }
            finally
            {
                bw.Flush();
                bw.Close();
                fs.Close();
            }
            return resultString;
        }

        public static string ExportTestResultsToCsv(string testSuiteName, string filename)
        {
            //========Data from textbox==========//        
            string stOutput = "";
            string stLine = "";
            string sHeaders = "";
            string sTestList = "";
            string resultString = "success";
            int lineCount = 0;

            List<TestRunList> testRunList = GetTestRunList(testSuiteName);
            sHeaders = "TestSuite Name,Last Executed,Test Status,Test Name,Increment,Header Request,Body Request,Header Response,Body Response,Execution Status";
            stOutput += sHeaders + "\r\n";
            lineCount++;
            //MessageBox.Show("Headers Set:"+stOutput);
            List<TestList> listTestList = GetTestList();
            // Export data.  
            int rowCounter = 1;
            foreach (TestRunList testRun in testRunList)
            {
                stLine = testRun.TestSuiteName + "," + testRun.LastExecuted + "," + testRun.TestStatus + ",";
                foreach (TestList item in testRun.TestList)
                {
                    sTestList = stLine + item.TestName + "," + item.IncrementCounter + "," + item.HeaderRequest.Replace("\r\n", "").Replace("\n", "").Replace("\r", "").Replace(",", "").Replace(":", "-") + "," + item.BodyRequest.Replace("\r\n", "").Replace("\n", "").Replace("\r", "").Replace(",", "").Replace(":", "-") + "," + item.HeaderResponse.Replace("\r\n", "").Replace("\n", "").Replace("\r", "").Replace(",", "").Replace(":", "-") + "," + item.BodyResponse.Replace("\r\n", "").Replace("\n", "").Replace("\r", "").Replace(",", "").Replace(":", "-") + "," + item.ExecutionStatus + ",";
                    //MessageBox.Show(rowCounter.ToString());
                    ++rowCounter;
                    if (sTestList.Length > 0)
                    {
                        stOutput += sTestList + "\r\n";
                        //MessageBox.Show("Row: " + i.ToString() +" :" + stOutput);
                        lineCount++;
                    }
                    sTestList = "";        
                }
                stLine = "";
            }
            System.Text.UnicodeEncoding utf16 = new UnicodeEncoding();
            FileStream fs = null;
            BinaryWriter bw = null;
            byte[] output = utf16.GetBytes(stOutput);
            try
            {
                fs = new FileStream(filename, System.IO.FileMode.Create);
                bw = new BinaryWriter(fs);
                bw.Write(output, 0, output.Length); //write the encoded file
            }
            catch (Exception e)
            {
                resultString = "Export failed with error: " + e.ToString();
                Utilities.WriteLogItem("Export failed; " + e.ToString(), TraceEventType.Error);
            }
            finally
            {
                bw.Flush();
                bw.Close();
                fs.Close();
            }
            return resultString;
        }

        public static void HandleTestSuiteOutput(string testSuiteName, string fileName)
        {
            string tempReplacestr = "";
            int replaceCounter = 0;
            bool replaceFlag = true;
            TestRunList testSuite = GetTestSuiteDetails(testSuiteName);
            if (testSuite.IncludeOutputResults)
            {
                ExportTestResultsToCsv(testSuiteName, fileName);
            }
            if (testSuite.RunOutput.Length > 0)
            {
                tempReplacestr = testSuite.RunOutput;
                List<Variables.VariableList> savedVariableList = Variables.GetVariableDocuments();
                while (tempReplacestr.Contains("REPLACE("))
                {
                    var tempArray = tempReplacestr.Split(Environment.NewLine.ToCharArray());
                    for (int c = 0; c < tempArray.Length; c++)
                    {
                        if (tempArray[c].Length > 2)
                        {
                            string variableName = tempArray[c].Split(":".ToCharArray())[0].Trim().Replace("}","").Replace("{", "").Replace("\n", "").Replace("\"","");
                            replaceFlag = false;
                            for (int j = 0; j < savedVariableList.Count; j++)
                            {
                                int intValue = 0;
                                bool boolValue = true;
                                if (!replaceFlag)
                                {
                                    if (savedVariableList[j].SearchForElement.Contains(variableName))
                                    {
                                        string variableValue = savedVariableList[j].SavedValue;
                                        if (int.TryParse(variableValue, out intValue) | bool.TryParse(variableValue, out boolValue))
                                        {
                                            tempReplacestr = tempReplacestr.Replace("REPLACE(" + replaceCounter.ToString() + ")", variableValue);
                                            replaceFlag = true;
                                        }
                                        else
                                        {
                                            variableValue = "\"" + variableValue + "\"";
                                            tempReplacestr = tempReplacestr.Replace("REPLACE(" + replaceCounter.ToString() + ")", variableValue);
                                            replaceFlag = true;
                                        }
                                        replaceCounter++;
                                    }
                                }
                            }
                        }
                    }
                }
                File.WriteAllText(Directory.GetCurrentDirectory() + "\\output.csv", tempReplacestr);
                //UpdateRunOutput(testSuiteName, tempReplacestr);
            }
        }

        public static void ExportTestRunListData(string testSuiteName, string fileName)
        {
            HashSet<TestRunList> fields = new HashSet<TestRunList>();
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("JSONAPI");
            var docTestRunList = database.GetCollection<BsonDocument>("colTestSuites");

            var filter = Builders<BsonDocument>.Filter.Eq("TestSuiteName", testSuiteName) & Builders<BsonDocument>.Filter.Eq("UserName", Users.currentUser);
            var result = docTestRunList.Find(filter).ToListAsync().Result;

            // Populate fields with all unique fields, see below for examples how.
            var csv = new StringBuilder();
            string headerLine = string.Join(",", fields);
            csv.AppendLine(headerLine);

            result.ForEach(element =>
            {
                string line = null;
                foreach (var field in fields)
                {
                    BsonValue value;
                    value = element.GetElement(field.ToString()).Value;
                    // Example deserialize to string
                    switch (value.BsonType)
                    {
                        case BsonType.ObjectId:
                            line = line + value.ToString();
                            break;
                        case BsonType.String:
                            line = line + value.ToString();
                            break;
                        case BsonType.Int32:
                            line = line + value.AsInt32.ToString();
                            break;
                    }
                    line = line + ",";
                }
                csv.AppendLine(line);
            });       
            File.WriteAllText(Directory.GetCurrentDirectory() + "\\" + fileName +".csv", csv.ToString());
        }

        public static void RunTestSuite(string testSuiteName)
        {
            int incrementCounter = 0;
            bool testStatus = true;
            bool testRunStatus = true;
            string tempValue = "";
            string testRunName = DateTime.Now.ToString("yyyyMMddhh24mmss");
            System.Environment.SetEnvironmentVariable("testRunName", testRunName, EnvironmentVariableTarget.User);
            List<TestList> testList = GetTestList(testSuiteName);
            //string exePath = ".\\chromeDriver.exe";
            //Utilities.ExtractResource(exePath);
            for (var i = 0; i < testList.Count; i++)
            {
                testStatus = true;
                incrementCounter = 1;
                string testName = testList[i].TestName;
                string bodyResponse = "No response";
                string headerResponse = "No response";

                string inputString = GetTestInput(testName);
                ExecutionResult.NewTestRun(testRunName, testSuiteName, testName);
                System.Environment.SetEnvironmentVariable("testRunName", testRunName, EnvironmentVariableTarget.User);
                System.Environment.SetEnvironmentVariable("testSuiteName", testSuiteName, EnvironmentVariableTarget.User);
                System.Environment.SetEnvironmentVariable("testName", testName, EnvironmentVariableTarget.User);
                Boolean executedFlag = false;
                if (IsGUITest(testName))
                {
                    testStatus = Utilities.ExecuteGUITest(testSuiteName, testRunName, testName);
                    string testR = "";
                    if (testStatus) testR = "Pass";
                    else testR = "Fail";
                    UpdateTestStatus_NewInstance(testSuiteName, testName, testR, "", "", "", "", 1,100);
                    executedFlag = true;
                }
                if (testName.Contains(".js"))
                {
                    var result = Utilities.ExecuteJavaScript(inputString);
                    
                    if (Variables.VariableExist(testName))
                    {
                        Variables.UpdateVariableSavedValue(testName, result.ToString());
                    }
                    testStatus = true;
                    UpdateTestStatus_NewInstance(testSuiteName, testName, "Executed", "", "", "", "", 1,100);
                }
                else
                {
                    if (!executedFlag)
                    {
                    System.Environment.SetEnvironmentVariable("jsonFlag", "yes", EnvironmentVariableTarget.User);
                    //Generate Details to send

                    List<string> detailMessages = Utilities.GenerateBatchDetail(inputString);
                        foreach (string generatedDetailMessage in detailMessages)
                        {
                            Dictionary<string, string> responseDict = Utilities.SendHttpRequest(generatedDetailMessage, incrementCounter, testSuiteName, testName, "In Progress");
                            if (responseDict.TryGetValue("Response", out tempValue))
                            {
                                if (tempValue.ToLower() == "ok")
                                {
                                    testStatus = true;
                                }
                                else
                                {
                                    testStatus = false;
                                    if (testRunStatus != false)
                                    {
                                        testRunStatus = true;
                                    }
                                    else
                                    {
                                        testRunStatus = false;
                                    }
                                }
                            }
                            foreach (string key in responseDict.Keys)
                            {
                                if (key.ToLower().Contains("header_"))
                                {
                                    headerResponse += "\"" + key.Substring(7) + "\"" + ":" + "\"" + responseDict[key] + "\"" + ",";
                                }
                            }
                            if (responseDict.TryGetValue("Body", out tempValue))
                            {
                                bodyResponse = responseDict["Body"].ToString();
                            }
                            string headerRequest = "";
                            if (responseDict.TryGetValue("HeaderRequest", out tempValue))
                            {
                                headerRequest = responseDict["HeaderRequest"].ToString();
                            }
                            if (testStatus) { tempValue = "Pass"; }
                            if (!testStatus) { tempValue = "Fail"; }
                            UpdateTestStatus_NewInstance(testSuiteName, testName, tempValue, headerRequest, generatedDetailMessage, headerResponse, bodyResponse, incrementCounter, GetConcurrentUsers(testSuiteName, testName, incrementCounter));
                            incrementCounter++;
                        }
                    }
                }
                if (testRunStatus) { tempValue = "Pass"; }
                if (!testRunStatus) { tempValue = "Fail"; }
                TestSuite.UpdateTestSuiteStatus(testSuiteName, tempValue);
                testStatus = true;
            }
            HandleTestSuiteOutput(testSuiteName, "AutoGeneratedTestResults.csv");

           //File.Delete(exePath);
        }
    }
}