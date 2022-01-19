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
    using System.Security.Cryptography;
    using System.Net.Mail;

    public static class Users
    {
        private static string connectionString = System.Environment.GetEnvironmentVariable("mongoDBConnectionString", EnvironmentVariableTarget.User);
        public static string currentUser = System.Environment.GetEnvironmentVariable("jsonapiuserName", EnvironmentVariableTarget.User);
        public class User
        {
            public string UserName { get; set; }
            public string UserPassword { get; set; }
            public string UserEmail { get; set; }
            public string UserHome { get; set; }
            public bool UserStatus { get; set; }
            public string UserRole { get; set; }
            public bool ReceiveNotifications { get; set; }

            public string ActivationCode { get; set; }
            public User(string userName, string userPassword, string userEmail, string userHome, string activationCode)
            {
                UserName = userName;
                UserPassword = userPassword;
                UserEmail = userEmail;
                if (userHome.Length == 0) UserHome = "C:\\JSONAPI\\";
                else UserHome = userHome;
                UserStatus = false;
                UserRole = "User";
                ReceiveNotifications = true;
                ActivationCode = activationCode;
            }
            public User()
            {
                UserName = "";
                UserPassword = "";
                UserEmail = "";
                UserRole = "";
                UserHome = "C:\\JSONAPI\\";
                UserStatus = false;
                ReceiveNotifications = true;
            }
        }

        public static bool NewUser(string userName, string userPassword, string userEmail, string userHome)
        {
            bool returnValue = false;
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("JSONAPI");
            userPassword = Encrypt(userPassword);
            var docSearchForElement = database.GetCollection<BsonDocument>("colUsers");
            Guid activationGuid = Guid.NewGuid();
            User userAccount = new User(userName, userPassword, userEmail, userHome, activationGuid.ToString());
            try
            {
                docSearchForElement.InsertOne(userAccount.ToBsonDocument());
                string[] emails = new string[1];
                emails[0] = userEmail;
                SendEmail(emails, "Please activate your new JSON API User Account", "Hi " + userName + ",<br>" + "You have successfully created a user account for the JSON API Application. Please login to the application and use the following:" + "<br>" + "<b>" + activationGuid.ToString() + "</b>" + "<br>" + "Warm regards," + "<br>" + "JSON API Support Team");
                returnValue = true;
            }
            catch (Exception e)
            {
                Utilities.WriteLogItem("Add new User failed; " + e.ToString(), TraceEventType.Error);
                returnValue = false;
            }
            return returnValue;
        }

        public static bool ValidateUser(string userName, string password)
        {
            bool returnValue = false;
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("JSONAPI");
            var testListCollection = database.GetCollection<User>("colUsers");

            var tcBuilder = Builders<BsonDocument>.Filter;
            var variableFilter = Builders<User>.Filter
                                                        .Eq(u => u.UserName, userName);
            // projection stage
            var simpleProjection = Builders<User>.Projection
                .Exclude("_id");

            var docs = testListCollection.Find(variableFilter).Project(simpleProjection).ToList();
            //var docs = testListCollection.Find<BsonDocument>(tcFilter).Project<TestRunList>(simpleProjection).ToList();
            docs.ForEach(doc =>
            {
                string tempString = doc.AsBsonValue.ToJson();
                var userObj = JsonConvert.DeserializeObject<User>(tempString);
                var tempPassword = userObj.UserPassword;
                if (Decrypt(tempPassword) == password && userObj.UserStatus == true) returnValue = true;
            });
            return returnValue;
        }

        public static User GetUser(string userName)
        {
            User returnValue = new User();
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("JSONAPI");
            var testListCollection = database.GetCollection<User>("colUsers");

            var tcBuilder = Builders<BsonDocument>.Filter;
            var variableFilter = Builders<User>.Filter.Eq(u => u.UserName, userName) & Builders<User>.Filter.Eq(u => u.UserStatus, true);
            // projection stage
            var simpleProjection = Builders<User>.Projection
                .Exclude("_id");

            var docs = testListCollection.Find(variableFilter).Project(simpleProjection).ToList();
            //var docs = testListCollection.Find<BsonDocument>(tcFilter).Project<TestRunList>(simpleProjection).ToList();
            docs.ForEach(doc =>
            {
                string tempString = doc.AsBsonValue.ToJson();
                returnValue = JsonConvert.DeserializeObject<User>(tempString);
            });
            return returnValue;
        }

        public static bool UserExists(string userName)
        {
            bool returnValue = true;
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("JSONAPI");
            var testListCollection = database.GetCollection<User>("colUsers");

            var tcBuilder = Builders<BsonDocument>.Filter;
            var variableFilter = Builders<User>.Filter.Eq(u => u.UserName, userName) & Builders<User>.Filter.Eq(u => u.UserStatus, true);
            // projection stage
            var simpleProjection = Builders<User>.Projection
                .Exclude("_id");

            var docs = testListCollection.Find(variableFilter).Project(simpleProjection).ToList();
            //var docs = testListCollection.Find<BsonDocument>(tcFilter).Project<TestRunList>(simpleProjection).ToList();
            if (docs.Count == 0) returnValue = false;
            return returnValue;
        }

        public static User GetNewUser(string userName)
        {
            User returnValue = new User();
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("JSONAPI");
            var testListCollection = database.GetCollection<User>("colUsers");

            var tcBuilder = Builders<BsonDocument>.Filter;
            var variableFilter = Builders<User>.Filter.Eq(u => u.UserName, userName);
            // projection stage
            var simpleProjection = Builders<User>.Projection
                .Exclude("_id");

            var docs = testListCollection.Find(variableFilter).Project(simpleProjection).ToList();
            //var docs = testListCollection.Find<BsonDocument>(tcFilter).Project<TestRunList>(simpleProjection).ToList();
            docs.ForEach(doc =>
            {
                string tempString = doc.AsBsonValue.ToJson();
                returnValue = JsonConvert.DeserializeObject<User>(tempString);
            });
            return returnValue;
        }

        public static List<string> GetUserEmails()
        {
            User returnValue = new User();
            List<string> emailList = new List<string>();
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("JSONAPI");
            var testListCollection = database.GetCollection<User>("colUsers");

            var variableFilter = Builders<User>.Filter.Eq(u => u.ReceiveNotifications, true) & Builders<User>.Filter.Eq(u => u.UserStatus, true);
            // projection stage
            var simpleProjection = Builders<User>.Projection
                .Exclude("_id");

            var docs = testListCollection.Find(variableFilter).Project(simpleProjection).ToList();
            //var docs = testListCollection.Find<BsonDocument>(tcFilter).Project<TestRunList>(simpleProjection).ToList();
            docs.ForEach(doc =>
            {
                string tempString = doc.AsBsonValue.ToJson();
                returnValue = JsonConvert.DeserializeObject<User>(tempString);
                emailList.Add(returnValue.UserEmail);
            });
            return emailList;
        }

        public static bool UserNameEmailUnique(string userName, string email)
        {
            bool returnValue = true;
            List<string> emailList = new List<string>();
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("JSONAPI");
            var testListCollection = database.GetCollection<User>("colUsers");

            var variableFilter = Builders<User>.Filter.Eq(u => u.UserName, userName);
            // projection stage

            var docs = testListCollection.Find(variableFilter).CountDocuments();
            if (docs == 1) returnValue = true;
            else returnValue = false;
        
            return returnValue;
        }

        public static List<User> GetUsers()
        {
            User tempReturnValue = new User();
            List<User> returnValue = new List<User>();

            var settings = MongoClientSettings.FromConnectionString(connectionString);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("JSONAPI");
            var testListCollection = database.GetCollection<BsonDocument>("colUsers");

            // projection stage
            var simpleProjection = Builders<BsonDocument>.Projection
                .Exclude("_id");

            var docs = testListCollection.Find(new BsonDocument()).Project(simpleProjection).ToList();
            //var docs = testListCollection.Find<BsonDocument>(tcFilter).Project<TestRunList>(simpleProjection).ToList();
            docs.ForEach(doc =>
            {
                string tempString = doc.AsBsonValue.ToJson();
                tempReturnValue = JsonConvert.DeserializeObject<User>(tempString);
                returnValue.Add(tempReturnValue);
            });
            return returnValue;
        }

        public static bool UpdatePassword(string userName, string password)
        {
            bool returnValue = true;
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("JSONAPI");
            var testUserCollection = database.GetCollection<User>("colUsers");
            try
            {
                var filter = Builders<User>.Filter.Eq("UserName", userName);
                var update = Builders<User>.Update.Set("UserPassword", Encrypt(password));
                testUserCollection.UpdateOne(filter, update);
            }
            catch (Exception e)
            {
                Utilities.WriteLogItem("UpdatePassword failed; " + e.ToString(), TraceEventType.Error);
                returnValue = false;
            }
            return returnValue;
        }

        public static void DeactivateUser(string userName)
        {
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("JSONAPI");
            var testUserCollection = database.GetCollection<User>("colUsers");
            try
            {
                var filter = Builders<User>.Filter.Eq("UserName", userName);
                var update = Builders<User>.Update.Set("UserStatus", false);
                testUserCollection.UpdateOne(filter, update);
            }
            catch (Exception e)
            {
                Utilities.WriteLogItem("DeactivateUser failed; " + e.ToString(), TraceEventType.Error);
            }
        }

        public static void ActivateUser(string userName)
        {
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("JSONAPI");
            var testUserCollection = database.GetCollection<User>("colUsers");
            try
            {
                var filter = Builders<User>.Filter.Eq("UserName", userName);
                var update = Builders<User>.Update.Set("UserStatus", true);
                testUserCollection.UpdateOne(filter, update);
            }
            catch (Exception e)
            {
                Utilities.WriteLogItem("DeactivateUser failed; " + e.ToString(), TraceEventType.Error);
            }
        }

        public static bool UpdateEmailAddress(string userName, string email)
        {
            bool returnValue = true;
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("JSONAPI");
            var testUserCollection = database.GetCollection<User>("colUsers");
            try
            {
                var filter = Builders<User>.Filter.Eq("UserName", userName);
                var update = Builders<User>.Update.Set("UserEmail", email);
                testUserCollection.UpdateOne(filter, update);
            }
            catch (Exception e)
            {
                Utilities.WriteLogItem("UpdateEmailAddress failed; " + e.ToString(), TraceEventType.Error);
                returnValue = false;
            }
            return returnValue;
        }

        public static bool UpdateUserRole(string userName, string userRole)
        {
            bool returnValue = true;
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("JSONAPI");
            var testUserCollection = database.GetCollection<User>("colUsers");
            try
            {
                var filter = Builders<User>.Filter.Eq("UserName", userName);
                var update = Builders<User>.Update.Set("UserRole", userRole);
                testUserCollection.UpdateOne(filter, update);
            }
            catch (Exception e)
            {
                Utilities.WriteLogItem("UpdateUserRole failed; " + e.ToString(), TraceEventType.Error);
                returnValue = false;
            }
            return returnValue;
        }

        public static bool UpdateUserName(string oldUserName, string newUserName)
        {
            bool returnValue = true;
            if (oldUserName != newUserName)
            {
                var settings = MongoClientSettings.FromConnectionString(connectionString);
                var client = new MongoClient(settings);
                var database = client.GetDatabase("JSONAPI");
                var testUserCollection = database.GetCollection<User>("colUsers");
                try
                {
                    var filter = Builders<User>.Filter.Eq("UserName", oldUserName);
                    var update = Builders<User>.Update.Set("UserName", newUserName);
                    testUserCollection.UpdateOne(filter, update);
                }
                catch (Exception e)
                {
                    Utilities.WriteLogItem("UpdateUserName failed; " + e.ToString(), TraceEventType.Error);
                    returnValue = false;
                }
            }
            return returnValue;
        }

        public static void DeleteUser(string userName)
        {
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("JSONAPI");
            var testUserCollection = database.GetCollection<User>("colUsers");

            try
            {
                var filter = Builders<User>.Filter.Eq("UserName", userName);
                testUserCollection.DeleteOne(filter);
            }
            catch (Exception e)
            {
                Utilities.WriteLogItem("DeleteUser failed; " + e.ToString(), TraceEventType.Error);
            }
        }
        public static void RemoveNotifications(string userName)
        {
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("JSONAPI");
            var testUserCollection = database.GetCollection<User>("colUsers");
            try
            {
                var filter = Builders<User>.Filter.Eq("UserName", userName);
                var update = Builders<User>.Update.Set("ReceiveNotifications", false);
                testUserCollection.UpdateOne(filter, update);
            }
            catch (Exception e)
            {
                Utilities.WriteLogItem("RemoveNotifications failed; " + e.ToString(), TraceEventType.Error);
            }
        }
        public static void AddNotifications(string userName)
        {
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("JSONAPI");
            var testUserCollection = database.GetCollection<User>("colUsers");
            try
            {
                var filter = Builders<User>.Filter.Eq("UserName", userName);
                var update = Builders<User>.Update.Set("ReceiveNotifications", true);
                testUserCollection.UpdateOne(filter, update);
            }
            catch (Exception e)
            {
                Utilities.WriteLogItem("AddNotifications failed; " + e.ToString(), TraceEventType.Error);
            }
        }


        public static bool UpdateUserHome(string userName, string userHome)
        {
            bool returnValue = true;
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("JSONAPI");
            var testUserCollection = database.GetCollection<User>("colUsers");
            try
            {
                var filter = Builders<User>.Filter.Eq("UserName", userName);
                var update = Builders<User>.Update.Set("UserHome", userHome);
                testUserCollection.UpdateOne(filter, update);
            }
            catch (Exception e)
            {
                Utilities.WriteLogItem("UpdateEmailAddress failed; " + e.ToString(), TraceEventType.Error);
                returnValue = false;
            }
            if (returnValue) System.Environment.SetEnvironmentVariable("workfolder", userHome, EnvironmentVariableTarget.User);
            return returnValue;
        }

        public static string Encrypt(string clearText)
        {
            string EncryptionKey = "JSONAPI";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }
        public static string Decrypt(string cipherText)
        {
            string EncryptionKey = "JSONAPI";
            cipherText = cipherText.Replace(" ", "+");
            try
            {
                byte[] cipherBytes = Convert.FromBase64String(cipherText);
                using (Aes encryptor = Aes.Create())
                {
                    Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                    encryptor.Key = pdb.GetBytes(32);
                    encryptor.IV = pdb.GetBytes(16);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(cipherBytes, 0, cipherBytes.Length);
                            cs.Close();
                        }
                        cipherText = Encoding.Unicode.GetString(ms.ToArray());
                    }
                }
            }
            catch (Exception e)
            {
                Utilities.WriteLogItem("Decrypt failed; " + e.ToString(), TraceEventType.Error);
            }
            return cipherText;
        }

        public static bool IsValidEmail(string email)
        {
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false; // suggested by @TK-421
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }

        private static void SendEmail(string[] emails, string subject, string body)
        {
            MailMessage message = new MailMessage();
            string passwordS = "[YX: dfU}up`@v3h=";
            // Add a recipient
            for (int c = 0; c < emails.Length; c++)
            {
                message.To.Add(emails[c]);
            }
            // Add a message subject
            message.Subject = subject;
            // Add a message body
            message.Body = body;
            message.IsBodyHtml = true;
            message.From = new MailAddress("jsonapi.unmanned@gmail.com", "JSON API Support");
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("jsonapi.unmanned@gmail.com", passwordS);
            smtp.Send(message);
        }
    }
}