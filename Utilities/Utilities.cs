namespace JSONAPI.Utilities
{
    using CsvHelper;
    using Microsoft.Office.Interop.Excel;
    using MongoDB.Driver;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using OpenQA.Selenium.Support.UI;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Net.Http;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;
    using System.Xml;

    public static class Utilities
    {
        public static string[,] selectedRanges = new string[50, 100];
        public static int selectedRangeNameCounter = 0;
        public static int selectedRangeValueCounter = 0;
        static IWebDriver driver = null;
        public static string GenerateDetail(string beginStr, bool batchMode = false, bool firstBatchRun = false)
        {
            string endStr = "";
            string origStr = beginStr;
            int execCounter = 0;
            int tagIndex = 0;
            string errorStr = "";
            string beginStrFromTagIndex = "";
            string operationString = "";
            int tagEndIndex = 0;
            int endTagIndex = 0;
            string parameterStr = "";
            bool foundFlag = false;
            bool replaceFlag = false;
            int foundCounter = 0;
            string[] operation1Array = new string[] { "[NUMBER(", "[TEXT(", "[VARIABLE(", "[YESTERDAY(", "[TODAY(", "[TOMORROW(", "[GUID", "[RANGE(" };
            String newBeginStr = beginStr;
            while (newBeginStr.IndexOf("[") >= 0)
            {
                for (int i = 0; i < operation1Array.Length; i++)
                {
                    if (newBeginStr.IndexOf(operation1Array[i]) >= 0 && !foundFlag)
                    {
                        foundFlag = true;
                        foundCounter = i;
                    }
                }
                if (foundFlag)
                {
                    string anotherString = "";
                    foundFlag = false;
                    replaceFlag = false;

                    operationString = operation1Array[foundCounter];
                    var matchesArray = newBeginStr.Split(operationString.ToCharArray());
                    //MessageBox.Show("newBeginStr: " + newBeginStr);
                    for (int d = 0; d < matchesArray.Length - 1; ++d)
                    {
                        if (newBeginStr.IndexOf(operationString) > 0)
                        {
                            anotherString = newBeginStr.Substring(0, newBeginStr.IndexOf(operationString));
                            newBeginStr = newBeginStr.Substring(newBeginStr.IndexOf(operationString));
                        }
                        if (newBeginStr.IndexOf(operationString) == 0)
                        {
                            tagIndex = newBeginStr.IndexOf(operationString) + operationString.Length;
                            beginStrFromTagIndex = newBeginStr.Substring(tagIndex, newBeginStr.Length - tagIndex);
                            tagEndIndex = beginStrFromTagIndex.IndexOf(")");
                            endTagIndex = newBeginStr.IndexOf("]") + 1;
                            newBeginStr = newBeginStr.Substring(endTagIndex);
                            if (tagEndIndex >= 0) parameterStr = beginStrFromTagIndex.Substring(0, tagEndIndex);
                            else parameterStr = "";
                        }
                        //MessageBox.Show("newBeginStr;" + newBeginStr);
                        var generator = new RandomGenerator();
                        int numDigits = 0;
                        if (foundCounter <= 2)
                        {
                            try
                            {
                                numDigits = Int32.Parse(parameterStr);
                            }
                            catch (Exception pe)
                            {
                                errorStr = operationString + "):Invalid number of digits specified: " + parameterStr;
                                WriteLogItem("Parsing issue:" + pe.ToString(), TraceEventType.Error);
                            }
                        }
                        switch (foundCounter)
                        {
                            case 0:
                                int rMin = 0;
                                int rMax = 0;
                                if (errorStr.Length == 0)
                                {
                                    switch (numDigits)
                                    {
                                        case 1:
                                            rMin = 0;
                                            rMax = 9;
                                            break;
                                        case 2:
                                            rMin = 10;
                                            rMax = 99;
                                            break;
                                        case 3:
                                            rMin = 100;
                                            rMax = 999;
                                            break;
                                        case 4:
                                            rMin = 1000;
                                            rMax = 9999;
                                            break;
                                        default:
                                            rMin = 10000;
                                            rMax = 99999;
                                            break;
                                    }
                                }
                                var randomNumber = generator.RandomNumber(rMin, rMax);
                                errorStr = "";
                                beginStr = ReplaceWholeWord(beginStr, operationString, randomNumber.ToString(), parameterStr);
                                //MessageBox.Show("Number: "+ beginStr);
                                if (anotherString.Length > 0)
                                {
                                    newBeginStr = anotherString + newBeginStr;
                                    anotherString = "";
                                }
                                break;
                            case 1:
                                if (errorStr.Length == 0)
                                {
                                    var randomString = generator.RandomString(int.Parse(parameterStr));
                                    beginStr = ReplaceWholeWord(beginStr, operationString, randomString, parameterStr);
                                    //MessageBox.Show("String: " + beginStr);
                                    if (anotherString.Length > 0)
                                    {
                                        newBeginStr = anotherString + newBeginStr;
                                        anotherString = "";
                                    }
                                }
                                break;
                            case 2:
                                var variableValue = Variables.GetSavedValue(parameterStr);
                                if (variableValue.Length > 0)
                                {
                                    beginStr = ReplaceWholeWord(beginStr, operationString, variableValue, parameterStr);
                                    if (anotherString.Length > 0)
                                    {
                                        newBeginStr = anotherString + newBeginStr;
                                        anotherString = "";
                                    }
                                    errorStr = "";
                                }
                                else
                                {
                                    errorStr = operationString + "):No saved value found for parameter: " + parameterStr;
                                }
                                break;
                            case 3:
                                string yesterdayValue = "";
                                try
                                {
                                    yesterdayValue = DateTime.Now.AddDays(-1).ToString(parameterStr);
                                    errorStr = "";
                                }
                                catch (Exception de)
                                {
                                    errorStr = operationString + "):Invalid date format specified: " + parameterStr;
                                    WriteLogItem("Date format parsing error:" + de.ToString(), TraceEventType.Error);
                                }
                                if (errorStr.Length == 0)
                                {
                                    beginStr = ReplaceWholeWord(beginStr, operationString, yesterdayValue, parameterStr);
                                    if (anotherString.Length > 0)
                                    {
                                        newBeginStr = anotherString + newBeginStr;
                                        anotherString = "";
                                    }
                                }
                                break;
                            case 4:
                                string todayValue = "";
                                try
                                {
                                    todayValue = DateTime.Now.ToString(parameterStr);
                                    errorStr = "";
                                }
                                catch (Exception de)
                                {
                                    errorStr = operationString + "):Invalid date format specified: " + parameterStr;
                                    WriteLogItem("Date format parsing error:" + de.ToString(), TraceEventType.Error);
                                }
                                if (errorStr.Length == 0)
                                {
                                    beginStr = ReplaceWholeWord(beginStr, operationString, todayValue, parameterStr);
                                    if (anotherString.Length > 0)
                                    {
                                        newBeginStr = anotherString + newBeginStr;
                                        anotherString = "";
                                    }
                                }
                                break;
                            case 5:
                                string tomorrowValue = "";
                                try
                                {
                                    tomorrowValue = DateTime.Now.AddDays(1).ToString(parameterStr);
                                    errorStr = "";
                                }
                                catch (Exception de)
                                {
                                    errorStr = operationString + "):Invalid date format specified: ";
                                    WriteLogItem("Date format parsing error:" + de.ToString(), TraceEventType.Error);
                                }
                                if (errorStr.Length == 0)
                                {
                                    beginStr = ReplaceWholeWord(beginStr, operationString, tomorrowValue, parameterStr);
                                    if (anotherString.Length > 0)
                                    {
                                        newBeginStr = anotherString + newBeginStr;
                                        anotherString = "";
                                    }
                                }
                                break;
                            case 6:
                                Guid guid = Guid.NewGuid();
                                beginStr = ReplaceWholeWord(beginStr, operationString, guid.ToString());
                                if (anotherString.Length > 0)
                                {
                                    newBeginStr = anotherString + newBeginStr;
                                    anotherString = "";
                                }
                                break;
                            case 7:
                                if (batchMode == false)
                                {
                                    string[] rangeValue = Ranges.GetRangeListValues(parameterStr);
                                    selectedRangeNameCounter = 0;
                                    for (int c = 0; c < rangeValue.Length; c++)
                                    {
                                        selectedRanges[selectedRangeNameCounter, c] = rangeValue[c];
                                    }
                                    if (rangeValue.Length > 0)
                                    {
                                        if (firstBatchRun == false)
                                        {
                                            beginStr = ReplaceWholeWord(beginStr, operationString, rangeValue[0], parameterStr);
                                        }
                                        else
                                        {
                                            beginStr = ReplaceWholeWord(beginStr, operationString, "RANGE" + selectedRangeValueCounter.ToString(), parameterStr);
                                        }

                                        if (anotherString.Length > 0)
                                        {
                                            newBeginStr = anotherString + newBeginStr;
                                            anotherString = "";
                                        }
                                        errorStr = "";
                                    }
                                    else
                                    {
                                        errorStr = operationString + "):No saved value found for range: " + parameterStr;
                                    }
                                }
                                //In normal mode only first value in the range will be calculated.  In batch mode, all values used to generate multiple detail messages
                                else
                                {
                                    beginStr = ReplaceWholeWord(beginStr, operationString, "RANGE", parameterStr);
                                    if (anotherString.Length > 0)
                                    {
                                        newBeginStr = anotherString + newBeginStr;
                                        anotherString = "";
                                    }
                                    errorStr = "";
                                    selectedRangeValueCounter++;
                                    if (selectedRangeValueCounter == 99)
                                    {
                                        selectedRangeValueCounter = 0;
                                        selectedRangeNameCounter++;
                                    }
                                    else
                                    {
                                        try
                                        {
                                            if (selectedRanges[selectedRangeNameCounter, selectedRangeValueCounter + 1] == null)
                                                selectedRangeNameCounter++;
                                        }
                                        catch (Exception e)
                                        {

                                        }
                                    }
                                }

                                break;
                        }

                        if (errorStr.Length > 0)
                        {
                            newBeginStr = ReplaceWholeWord(newBeginStr, operationString, errorStr);
                        }
                        replaceFlag = true;
                    }
                    if (replaceFlag) endStr += beginStr;
                    execCounter++;
                }
                else endStr = beginStr;
            }
            return endStr;
        }

        public static string ValidateDetail(string beginStr, string actualResult)
        {
            bool interimResult = true;
            string origStr = beginStr;
            int execCounter = 0;
            int tagIndex = 0;
            string errorStr = "";
            string beginStrFromTagIndex = "";
            string operationString = "";
            int tagEndIndex = 0;
            int endTagIndex = 0;
            string parameterStr = "";
            string lineStr = "";
            bool foundFlag = false;
            string[] operation1Array = new string[] { "[ISNUMBER", "[ISTEXT", "[ISDATE", "[VARIABLE(", "[GUID", "[RANGE(" };
            for (int i = 0; i < operation1Array.Length; i++)
            {
                if (beginStr.IndexOf(operation1Array[i]) >= 0)
                {
                    foundFlag = true;
                }
            }
            if (foundFlag)
            {
                String newBeginStr = beginStr;
                string anotherString = "";
                while (newBeginStr.IndexOf("[") >= 0)
                {
                    for (int i = 0; i < operation1Array.Length; i++)
                    {
                        operationString = operation1Array[i];
                        var matchesArray = newBeginStr.Split(operationString.ToCharArray());
                        //MessageBox.Show("newBeginStr: " + newBeginStr);
                        for (int d = 0; d < matchesArray.Length - 1; ++d)
                        {
                            if (newBeginStr.IndexOf(operationString) > 0)
                            {
                                anotherString = newBeginStr.Substring(0, newBeginStr.IndexOf(operationString));
                                newBeginStr = newBeginStr.Substring(newBeginStr.IndexOf(operationString));
                            }
                            if (newBeginStr.IndexOf(operationString) == 0)
                            {
                                tagIndex = newBeginStr.IndexOf(operationString) + operationString.Length;
                                beginStrFromTagIndex = newBeginStr.Substring(tagIndex, newBeginStr.Length - tagIndex);
                                tagEndIndex = beginStrFromTagIndex.IndexOf(")");
                                endTagIndex = newBeginStr.IndexOf("]") + 1;
                                newBeginStr = newBeginStr.Substring(endTagIndex);
                                if (tagEndIndex >= 0) parameterStr = beginStrFromTagIndex.Substring(0, tagEndIndex);
                                else parameterStr = "";
                            }

                            switch (i)
                            {
                                case 0:
                                    var variableValue = Variables.GetSavedValue(parameterStr);
                                    if (actualResult == "JSONAPI") variableValue = "Test";
                                    if (variableValue.Length > 0)
                                    {
                                        if (variableValue.ToLower() == actualResult)
                                        {
                                            if (interimResult == false) interimResult = false;
                                            else interimResult = true;
                                        }
                                        else
                                        {
                                            interimResult = true;
                                        }
                                        if (anotherString.Length > 0)
                                        {
                                            newBeginStr = anotherString + newBeginStr;
                                            anotherString = "";
                                        }
                                        errorStr = "";
                                    }
                                    else
                                    {
                                        errorStr = "FAIL for Variable: " + parameterStr;
                                        interimResult = false;
                                    }
                                    break;
                                case 1:
                                    //Is Numeric
                                    int numericValue;
                                    if (actualResult == "JSONAPI") actualResult = "123";
                                    bool isNumber = int.TryParse(actualResult, out numericValue);
                                    if (isNumber)
                                    {
                                        if (interimResult == false) interimResult = false;
                                        else interimResult = true;
                                        if (anotherString.Length > 0)
                                        {
                                            newBeginStr = anotherString + newBeginStr;
                                            anotherString = "";
                                        }
                                        errorStr = "";
                                    }
                                    else
                                    {
                                        errorStr = "FAIL for IsNumeric: " + actualResult;
                                        interimResult = false;
                                    }
                                    break;
                                case 2:
                                    DateTime dateValue;
                                    if (actualResult == "JSONAPI") actualResult = "22/12/2021";
                                    string[] formats = {"M/d/yyyy h:mm:ss tt", "M/d/yyyy h:mm tt",
                                       "MM/dd/yyyy hh:mm:ss", "M/d/yyyy h:mm:ss", "M/d/yyyy", "MM/d/yyyy",
                                       "M/d/yyyy hh:mm tt", "M/d/yyyy hh tt","M/dd/yyyy", "MM/dd/yyyy",
                                       "M/d/yyyy h:mm", "M/d/yyyy h:mm","d/M/yyyy", "d/MM/yyyy",
                                       "MM/dd/yyyy hh:mm", "M/dd/yyyy hh:mm","dd/M/yyyy", "dd/MM/yyyy",
                                       "d/M/yyyy h:mm:ss tt", "d/M/yyyy h:mm tt",
                                       "dd/M/yyyy hh:mm:ss", "dd/M/yyyy h:mm:ss",
                                       "d/M/yyyy hh:mm tt", "d/M/yyyy hh tt",
                                       "d/M/yyyy h:mm", "d/M/yyyy h:mm",
                                       "dd/MM/yyyy hh:mm", "dd/MM/yyyy hh tt", "dd/MM/yyyy hh:mm:ss"};
                                    if (DateTime.TryParseExact(actualResult, formats, new CultureInfo("en-US"), DateTimeStyles.None, out dateValue))
                                    {
                                        if (interimResult == false) interimResult = false;
                                        else interimResult = true;
                                        if (anotherString.Length > 0)
                                        {
                                            newBeginStr = anotherString + newBeginStr;
                                            anotherString = "";
                                        }
                                        errorStr = "";
                                    }
                                    else
                                    {
                                        errorStr = "FAIL for IsDate: " + actualResult;
                                        interimResult = false;
                                    }
                                    break;
                                case 3:
                                    if (actualResult == "JSONAPI") actualResult = "abc";
                                    isNumber = int.TryParse(actualResult, out numericValue);
                                    if (!isNumber)
                                    {
                                        if (interimResult == false) interimResult = false;
                                        else interimResult = true;
                                        if (anotherString.Length > 0)
                                        {
                                            newBeginStr = anotherString + newBeginStr;
                                            anotherString = "";
                                        }
                                        errorStr = "";
                                    }
                                    else
                                    {
                                        errorStr = "FAIL for IsText: " + actualResult;
                                        interimResult = false;
                                    }
                                    break;
                                case 4:
                                    Guid guidValue;
                                    if (actualResult == "JSONAPI") actualResult = "e9427c6c-4c3f-4773-a322-94ebe586c4c2";
                                    bool isGuid = Guid.TryParse(actualResult, out guidValue);
                                    if (isGuid)
                                    {
                                        if (interimResult == false) interimResult = false;
                                        else interimResult = true;
                                        if (anotherString.Length > 0)
                                        {
                                            newBeginStr = anotherString + newBeginStr;
                                            anotherString = "";
                                        }
                                        errorStr = "";
                                    }
                                    else
                                    {
                                        errorStr = "FAIL for IsGuid: " + actualResult;
                                        interimResult = false;
                                    }
                                    break;
                                case 5:
                                    string[] rangeValue = Ranges.GetRangeListValues(parameterStr);
                                    bool foundRangeFlag = false;
                                    if (rangeValue != null)
                                    {
                                        if (actualResult == "JSONAPI") actualResult = rangeValue[0];
                                        for (int c = 0; c < rangeValue.Length; c++)
                                        {
                                            if (rangeValue[c] != null & foundRangeFlag == false)
                                            {
                                                if (rangeValue[c] == actualResult)
                                                {
                                                    if (interimResult == false) interimResult = false;
                                                    else interimResult = true;
                                                    foundRangeFlag = true;
                                                }
                                            }
                                        }
                                        if (!foundRangeFlag) interimResult = false;
                                        if (anotherString.Length > 0)
                                        {
                                            newBeginStr = anotherString + newBeginStr;
                                            anotherString = "";
                                        }
                                        errorStr = "";
                                    }
                                    else
                                    {
                                        errorStr = "FAIL for Range: " + parameterStr;
                                        interimResult = false;
                                    }
                                    break;
                            }
                            if (errorStr.Length > 0)
                            {
                                if (lineStr.Length == 0)
                                {
                                    lineStr = errorStr + "\n";
                                }
                                else lineStr = lineStr + errorStr + "\n";
                                errorStr = "";
                            }
                        }
                    }
                    execCounter++;
                }
            }
            else
            {
                if (actualResult == "JSONAPI") actualResult = beginStr;
                if (beginStr == actualResult)
                {
                    interimResult = true;
                }
                else
                {
                    lineStr = "Fail";
                }
            }

            if (!interimResult)
            {
                if (lineStr.Length == 0) lineStr = "Fail";
                return lineStr;
            }
            else
            {
                return "Pass";
            }
        }

        public static List<string> GenerateBatchDetail(string beginStr)
        {
            List<string> returnValue = new List<string>();
            if (beginStr.Contains("RANGE"))
            {
                //First replace all other reserved keywords
                string newString = GenerateDetail(beginStr.Trim(), false, true);
                string tempString = "";
                if (selectedRanges[0, 0] != null)
                {
                    for (int d = 0; d < 50; d++)
                    {
                        for (int e = 0; e < 100; e++)
                        {
                            if (selectedRanges[d, e] != null)
                            {
                                newString = GenerateDetail(beginStr.Trim(), true, false);
                                tempString = ReplaceWholeWord(newString, "RANGE", selectedRanges[d, e]);
                                returnValue.Add(tempString);
                            }
                        }
                    }
                }
                else
                {
                    returnValue.Add(newString);
                }
            }
            else
            {
                returnValue.Add(beginStr);
            }
            return returnValue;
        }

        public static Dictionary<string, string> BuildHttpHeaderInformation(string message)
        {
            Dictionary<string, string> returnDictionary = new Dictionary<string, string>();
            string tempValue = "";
            // Get Token if applicable
            string token = Variables.GetSavedValue("token");
            if (token == null) token = "";
            //First get authentication
            if (System.Environment.GetEnvironmentVariable("useBearerToken", EnvironmentVariableTarget.User) == "yes")
            {
                if (System.Environment.GetEnvironmentVariable("bearerToken", EnvironmentVariableTarget.User) != "none" & token == "")
                {
                    returnDictionary.Add("Authorization", "Bearer " + System.Environment.GetEnvironmentVariable("bearerToken", EnvironmentVariableTarget.User));
                    //MessageBox.Show("Setting Bearer token:  Bearer " + System.Environment.GetEnvironmentVariable("bearerToken", EnvironmentVariableTarget.User));
                }
                else
                {
                    if (token != "") returnDictionary.Add("Authorization", "Bearer " + token);
                }
            }

            if (System.Environment.GetEnvironmentVariable("useBasicToken", EnvironmentVariableTarget.User) == "yes")
            {
                if (System.Environment.GetEnvironmentVariable("basicToken", EnvironmentVariableTarget.User) != "none" & token == "")
                {
                    returnDictionary.Add("Authorization", "basic " + System.Environment.GetEnvironmentVariable("basicToken", EnvironmentVariableTarget.User));
                }
                else
                {
                    if (token != "") returnDictionary.Add("Authorization", "basic " + token);
                }
            }

            if (System.Environment.GetEnvironmentVariable("useAPIKey", EnvironmentVariableTarget.User) == "yes")
            {
                if (System.Environment.GetEnvironmentVariable("apiKey", EnvironmentVariableTarget.User) != "none")
                    returnDictionary.Add("Authorization", "apikey " + System.Environment.GetEnvironmentVariable("apiKey", EnvironmentVariableTarget.User));
            }
            if (System.Environment.GetEnvironmentVariable("useCustomKey", EnvironmentVariableTarget.User) == "yes")
            {
                if (System.Environment.GetEnvironmentVariable("customKey", EnvironmentVariableTarget.User) != "none")
                    returnDictionary.Add("Authorization", System.Environment.GetEnvironmentVariable("customKey", EnvironmentVariableTarget.User));
            }

            int counter = 1;
            //MessageBox.Show("Header file; " + path);
            if (message.Length > 0)
            {
                string[] httpAttrs = new string[2];
                string[] headerArray = message.Split(Environment.NewLine.ToCharArray());
                string[] headerArray2 = message.Split("\n".ToCharArray());
                if (headerArray2.Length < headerArray.Length) headerArray = headerArray2;
                for (int i = 0; i < headerArray.Length; i++)
                {
                    headerArray[i] = headerArray[i].Replace("\r", "");
                    if (counter == 1)
                    {
                        returnDictionary.Add("Url", headerArray[i]);
                    }
                    if (counter == 2)
                    {
                        returnDictionary.Add("Type", headerArray[i]);
                    }
                    if (counter == 3)
                    {
                        if (!returnDictionary.ContainsKey("Authorization"))
                            returnDictionary.Add("Authorization", headerArray[i]);
                    }
                    if (counter > 3)
                    {
                        httpAttrs = headerArray[i].Split(',');
                        try
                        {
                            if (!returnDictionary.ContainsKey(httpAttrs[0]))
                                returnDictionary.Add(httpAttrs[0], httpAttrs[1]);
                        }
                        catch (Exception ae)
                        {
                            WriteLogItem("Header file not correct:" + ae.ToString(), TraceEventType.Error);
                        }
                    }
                    counter++;
                }
            }
            //Auto-Add
            List<Variables.VariableList> savedVariableList = Variables.GetVariableDocuments();
            for (int j = 0; j < savedVariableList.Count; j++)
            {
                if (savedVariableList[j].SavedValue.Length > 0 && savedVariableList[j].VariableName.ToLower() != "token")
                {
                    if (returnDictionary.TryGetValue(savedVariableList[j].SearchForElement, out tempValue))
                    {
                        if (savedVariableList[j].PartialSave.ToString().ToLower() == "first")
                        {
                            int numChars = savedVariableList[j].NumChars;
                            if (numChars > 0 & numChars <= savedVariableList[j].SavedValue.Length)
                            {
                                string tempString = savedVariableList[j].SavedValue;
                                tempString = tempString.Substring(0, numChars);
                                returnDictionary[savedVariableList[j].SearchForElement] = tempString;
                            }
                        }
                        if (savedVariableList[j].PartialSave.ToString().ToLower() == "last")
                        {
                            int numChars = savedVariableList[j].NumChars;
                            if (numChars > 0 & numChars <= savedVariableList[j].SavedValue.Length)
                            {
                                string tempString = savedVariableList[j].SavedValue;
                                tempString = tempString.Substring(tempString.Length - numChars);
                                returnDictionary[savedVariableList[j].SearchForElement] = tempString;
                            }
                        }
                        if (savedVariableList[j].PartialSave.ToString().ToLower() == "all")
                        {
                            returnDictionary[savedVariableList[j].SearchForElement] = savedVariableList[j].SavedValue;
                        }
                    }
                    else
                    {
                        returnDictionary.Add(savedVariableList[j].SearchForElement, savedVariableList[j].SavedValue);
                    }
                }
            }
            return returnDictionary;

        }

        public static Dictionary<string, string> GetHttpHeaderInformation(string testName)
        {
            Dictionary<string, string> returnDictionary = new Dictionary<string, string>();
            string path = "";
            string headerContents = "";
            if (testName.Length == 0)
            {
                try
                {
                    path = System.Environment.GetEnvironmentVariable("editFile", EnvironmentVariableTarget.User).ToString();
                }
                catch (Exception e)
                {
                    WriteLogItem("Issue with file. Nothing will be displayed:" + e.ToString(), TraceEventType.Error);
                }
                string[] tempPath = path.Split(".".ToCharArray());
                path = tempPath[0] + ".h";
                if (File.Exists(path))
                {
                    headerContents = System.IO.File.ReadAllText(path);
                }
                else
                {
                    headerContents = "";
                }
                
            }
            else
            {
                headerContents = TestSuite.GetTestHeaderInput(testName);
            }
            string tempValue = "";
            // Get Token if applicable
            string token = Variables.GetSavedValue("token");
            if (token == null) token = "";
            //First get authentication
            if (System.Environment.GetEnvironmentVariable("useBearerToken", EnvironmentVariableTarget.User) == "yes")
            {
                if (System.Environment.GetEnvironmentVariable("bearerToken", EnvironmentVariableTarget.User) != "none" & token == "")
                {
                    returnDictionary.Add("Authorization", "Bearer " + System.Environment.GetEnvironmentVariable("bearerToken", EnvironmentVariableTarget.User));
                    //MessageBox.Show("Setting Bearer token:  Bearer " + System.Environment.GetEnvironmentVariable("bearerToken", EnvironmentVariableTarget.User));
                }
                else
                {
                    if (token != "") returnDictionary.Add("Authorization", "Bearer " + token);
                }
            }

            if (System.Environment.GetEnvironmentVariable("useBasicToken", EnvironmentVariableTarget.User) == "yes")
            {
                if (System.Environment.GetEnvironmentVariable("basicToken", EnvironmentVariableTarget.User) != "none" & token == "")
                {
                    returnDictionary.Add("Authorization", "basic " + System.Environment.GetEnvironmentVariable("basicToken", EnvironmentVariableTarget.User));
                }
                else
                {
                    if (token != "") returnDictionary.Add("Authorization", "basic " + token);
                }
            }

            if (System.Environment.GetEnvironmentVariable("useAPIKey", EnvironmentVariableTarget.User) == "yes")
            {
                if (System.Environment.GetEnvironmentVariable("apiKey", EnvironmentVariableTarget.User) != "none")
                    returnDictionary.Add("Authorization", "apikey " + System.Environment.GetEnvironmentVariable("apiKey", EnvironmentVariableTarget.User));
            }
            if (System.Environment.GetEnvironmentVariable("useCustomKey", EnvironmentVariableTarget.User) == "yes")
            {
                if (System.Environment.GetEnvironmentVariable("customKey", EnvironmentVariableTarget.User) != "none")
                    returnDictionary.Add("Authorization", System.Environment.GetEnvironmentVariable("customKey", EnvironmentVariableTarget.User));
            }

            int counter = 1;
            //MessageBox.Show("Header file; " + path);
            if (headerContents.Length > 0)
            {
                string[] httpAttrs = new string[2];
                string[] headerArray = headerContents.Split(Environment.NewLine.ToCharArray());
                string[] headerArray2 = headerContents.Split("\n".ToCharArray());
                if (headerArray2.Length < headerArray.Length) headerArray = headerArray2;
                for (int i = 0; i < headerArray.Length; i++)
                {
                    headerArray[i] = headerArray[i].Replace("\r", "");
                    if (counter == 1)
                    {
                        returnDictionary.Add("Url", headerArray[i]);
                    }
                    if (counter == 2)
                    {
                        returnDictionary.Add("Type", headerArray[i]);
                    }
                    if (counter == 3)
                    {
                        if (!returnDictionary.ContainsKey("Authorization"))
                            returnDictionary.Add("Authorization", headerArray[i]);
                    }
                    if (counter > 3)
                    {
                        httpAttrs = headerArray[i].Split(',');
                        try
                        {
                            if (!returnDictionary.ContainsKey(httpAttrs[0]))
                                returnDictionary.Add(httpAttrs[0], httpAttrs[1]);
                        }
                        catch (Exception ae)
                        {
                            WriteLogItem("Header file not correct:" + ae.ToString(), TraceEventType.Error);
                        }
                    }
                    counter++;
                }
            }
            //Auto-Add
            List<Variables.VariableList> savedVariableList = Variables.GetVariableDocuments();
            for (int j = 0; j < savedVariableList.Count; j++)
            {
                if (savedVariableList[j].SavedValue.Length > 0 && savedVariableList[j].VariableName.ToLower() != "token")
                {
                    if (returnDictionary.TryGetValue(savedVariableList[j].SearchForElement, out tempValue))
                    {
                        if (savedVariableList[j].PartialSave.ToString().ToLower() == "first")
                        {
                            int numChars = savedVariableList[j].NumChars;
                            if (numChars > 0 & numChars <= savedVariableList[j].SavedValue.Length)
                            {
                                string tempString = savedVariableList[j].SavedValue;
                                tempString = tempString.Substring(0, numChars);
                                returnDictionary[savedVariableList[j].SearchForElement] = tempString;
                            }
                        }
                        if (savedVariableList[j].PartialSave.ToString().ToLower() == "last")
                        {
                            int numChars = savedVariableList[j].NumChars;
                            if (numChars > 0 & numChars <= savedVariableList[j].SavedValue.Length)
                            {
                                string tempString = savedVariableList[j].SavedValue;
                                tempString = tempString.Substring(tempString.Length - numChars);
                                returnDictionary[savedVariableList[j].SearchForElement] = tempString;
                            }
                        }
                        if (savedVariableList[j].PartialSave.ToString().ToLower() == "all")
                        {
                            returnDictionary[savedVariableList[j].SearchForElement] = savedVariableList[j].SavedValue;
                        }
                    }
                    else
                    {
                        returnDictionary.Add(savedVariableList[j].SearchForElement, savedVariableList[j].SavedValue);
                    }
                }
            }
            return returnDictionary;
        }

        public static Dictionary<string, string> SendHttpRequest(string bodyText, int incrementCounter, string testSuiteName = "", string testName = "", string tempValue = "")
        {
            HttpClient client = new HttpClient();
            HttpRequestMessage requestMessage = new HttpRequestMessage();
            StringContent httpContent = new StringContent(bodyText);
            Stream streamResponse = null;
            StreamReader streamReader = null;
            HttpResponseMessage httpResponseClient = null;
            bool continueFlag = true;
            if (IsValidJson(bodyText)) System.Environment.SetEnvironmentVariable("fileType", "json", EnvironmentVariableTarget.User);
            else System.Environment.SetEnvironmentVariable("fileType", "xml", EnvironmentVariableTarget.User);
            Dictionary<string, string> returnDict = new Dictionary<string, string>();
            Dictionary<string, string> headerDict = GetHttpHeaderInformation(testName);
            if (System.Environment.GetEnvironmentVariable("execMode", EnvironmentVariableTarget.User) != "batch")
                bodyText = GenerateDetail(bodyText);
            string dictionaryString = "{";
            if (testSuiteName.Length > 0)
            {
                foreach (KeyValuePair<string, string> keyValues in headerDict)
                {
                    dictionaryString += keyValues.Key + " : " + keyValues.Value + ", ";
                }
                dictionaryString = dictionaryString.TrimEnd(',', ' ');
            }
            returnDict.Add("HeaderRequest", dictionaryString + "}");
            Dictionary<string, string> responseDict = new Dictionary<string, string>();
            string url = "";
            string url2 = url;
            if (headerDict.TryGetValue("Url", out url))
            {
                client.BaseAddress = new Uri(url);
                client.Timeout = TimeSpan.FromMinutes(10);
                client.DefaultRequestHeaders.Connection.Add("Keep-Alive");
                WriteLogItem("url: " + url, TraceEventType.Information);
                if (url == "") continueFlag = false;
                headerDict.Remove("Url");
            }
            if (continueFlag)
            {           
                if (headerDict.ContainsKey("Type"))
                {
                    if (headerDict.TryGetValue("Type", out url))
                    {
                        try
                        {
                            if (url == "") continueFlag = false;
                            else requestMessage.Method = new HttpMethod(url);
                            headerDict.Remove("Type");
                        }
                        catch (Exception typeException)
                        {
                            WriteLogItem("Invalid HTTP Type: " + typeException.Message, TraceEventType.Information);
                            continueFlag = false;
                        }
                    }
                }

                if (headerDict.ContainsKey("Accept"))
                {
                    url = "";
                    if (headerDict.TryGetValue("Accept", out url))
                    {
                        if (url != "")
                        {
                            try
                            {
                                requestMessage.Headers.Add("Accept", url);
                                headerDict.Remove("Accept");
                            }
                            catch (Exception acceptException)
                            {
                                WriteLogItem("Invalid HTTP Accept: " + acceptException.Message, TraceEventType.Information);
                                continueFlag = false;
                            }
                        }
                    }
                }
                if (continueFlag)
                {
                    foreach (string key in headerDict.Keys)
                    {
                        try
                        {
                            if (key != "")
                                requestMessage.Headers.Add(key, headerDict[key]);
                        }
                        catch (Exception fr)
                        {
                            WriteLogItem("Unable to read Header:" + fr.ToString(), TraceEventType.Error);
                        }
                    }

                    try
                    {
                        if (System.Environment.GetEnvironmentVariable("fileType", EnvironmentVariableTarget.User) == "json")
                        {
                            var content = new StringContent(bodyText, Encoding.UTF8, "application/json");
                            requestMessage.Content = content;
                            httpResponseClient = client.Send(requestMessage);
                        }
                        else
                        {
                            var xmlContent = new StringContent(bodyText, Encoding.UTF8, "text/xml");
                            requestMessage.Content = xmlContent;

                            httpResponseClient = client.Send(requestMessage);
                        }

                        if (httpResponseClient.IsSuccessStatusCode)
                        {
                            returnDict.Add("HttpResponse", httpResponseClient.StatusCode.ToString() + "-" + httpResponseClient.ReasonPhrase.ToString());
                            try
                            {
                                streamResponse = httpResponseClient.Content.ReadAsStream();
                                streamReader = new StreamReader(streamResponse);
                                returnDict.Add("Body", streamReader.ReadToEnd());
                                continueFlag = true;
                            }
                            finally
                            {
                                streamReader.Close();
                                streamResponse.Close();
                            }
                        }
                        else
                        {
                            returnDict.Add("HttpResponse", httpResponseClient.StatusCode.ToString());
                            returnDict.Add("Body", "No response");
                            continueFlag = false;
                            WriteLogItem("Exception with HTTP Response Code:" + httpResponseClient.StatusCode.ToString(), TraceEventType.Error);
                        }
                    }
                    catch (Exception e)
                    {
                        returnDict.Add("HttpResponse", "Error");
                        returnDict.Add("Body", e.ToString());
                        continueFlag = false;
                        WriteLogItem("Exception with HTTP Response:" + e.ToString(), TraceEventType.Error);
                        //MessageBox.Show("Exception on getting HTTP Response:" + e.ToString());
                    }
                }
                if (continueFlag)
                {
                    try 
                    {        
                        List<Variables.VariableList> savedVariableList = Variables.GetVariableDocuments();
                        foreach (var httpResponse in httpResponseClient.Headers)
                        {
                            returnDict.Add("Header_" + httpResponse.Key, httpResponse.Value.ToString());
                            for (int j = 0; j < savedVariableList.Count; j++)
                            {
                                if (savedVariableList[j].SearchForElement.Contains(httpResponse.Key))
                                {
                                    if (savedVariableList[j].ReplaceWhere.ToLower() != "static" && savedVariableList[j].ReplaceWhere.ToLower() != "body")
                                    {
                                        savedVariableList[j].SavedValue = httpResponse.Value.ToString();
                                        Variables.UpdateVariableSavedValue(httpResponse.Key, httpResponse.Value.ToString());
                                    }
                                }
                            }
                            //Read Token from header based on the Token Element Specified in Options screen
                            if (httpResponse.Key.ToString().ToLower().Contains(System.Environment.GetEnvironmentVariable("tokenElement", EnvironmentVariableTarget.User).ToString().ToLower()))
                            {
                                if (System.Environment.GetEnvironmentVariable("useBearerToken", EnvironmentVariableTarget.User) == "yes")
                                {
                                    System.Environment.SetEnvironmentVariable("bearerToken", httpResponse.Value.ToString(), EnvironmentVariableTarget.User);
                                }

                                if (System.Environment.GetEnvironmentVariable("useBasicToken", EnvironmentVariableTarget.User) == "yes")
                                {
                                    System.Environment.SetEnvironmentVariable("basicToken", httpResponse.Value.ToString(), EnvironmentVariableTarget.User);
                                }

                                if (System.Environment.GetEnvironmentVariable("useAPIKey", EnvironmentVariableTarget.User) == "yes")
                                {
                                    System.Environment.SetEnvironmentVariable("apiKey", httpResponse.Value.ToString(), EnvironmentVariableTarget.User);
                                }
                                if (System.Environment.GetEnvironmentVariable("useCustomKey", EnvironmentVariableTarget.User) == "yes")
                                {
                                    System.Environment.SetEnvironmentVariable("customKey", httpResponse.Value.ToString(), EnvironmentVariableTarget.User);
                                }
                            }
                            else
                            {
                                if (httpResponse.Key.ToString().ToLower().Contains("authorization"))
                                {
                                    switch (httpResponse.Value.ToString().ToLower())
                                    {
                                        case "bearer":
                                            System.Environment.SetEnvironmentVariable("bearerToken", httpResponse.Value.ToString(), EnvironmentVariableTarget.User);
                                            break;
                                        case "api_key":
                                            System.Environment.SetEnvironmentVariable("apiKey", httpResponse.Value.ToString(), EnvironmentVariableTarget.User);
                                            break;
                                        case "api-key":
                                            System.Environment.SetEnvironmentVariable("apiKey", httpResponse.Value.ToString(), EnvironmentVariableTarget.User);
                                            break;
                                        case "basic":
                                            System.Environment.SetEnvironmentVariable("basicToken", httpResponse.Value.ToString(), EnvironmentVariableTarget.User);
                                            break;
                                        default:
                                            System.Environment.SetEnvironmentVariable("customKey", httpResponse.Value.ToString(), EnvironmentVariableTarget.User);
                                            break;
                                    }
                                }
                            }
                        }
                        if (System.Environment.GetEnvironmentVariable("fileType", EnvironmentVariableTarget.User) == "json")
                        {
                            buildJsonHttpResponseDict(returnDict);
                            System.Environment.GetEnvironmentVariable("testRunName", EnvironmentVariableTarget.User);
                            SaveExecutionResult(returnDict, System.Environment.GetEnvironmentVariable("testRunName", EnvironmentVariableTarget.User), System.Environment.GetEnvironmentVariable("testSuiteName", EnvironmentVariableTarget.User), System.Environment.GetEnvironmentVariable("testName", EnvironmentVariableTarget.User), incrementCounter);
                        }
                        else
                        {
                            buildXMLHttpResponseDict(returnDict);
                            SaveExecutionResultXML(returnDict, System.Environment.GetEnvironmentVariable("testRunName", EnvironmentVariableTarget.User), System.Environment.GetEnvironmentVariable("testSuiteName", EnvironmentVariableTarget.User), System.Environment.GetEnvironmentVariable("testName", EnvironmentVariableTarget.User), incrementCounter);
                        }
                    }
                    finally
                    {
                        client.Dispose();
                        if (streamReader != null)
                            streamReader.Dispose();
                        if (httpResponseClient != null)
                            httpResponseClient.Dispose();
                    }
                }
                else
                {
                    try
                    {
                        returnDict.Add("HttpResponse", "Error");
                        returnDict.Add("Body", "No HTTP Method specified");
                    }
                    catch (Exception e)
                    {
                        Logs.NewLogItem("Error adding error response: " + e.Message, TraceEventType.Information);
                    }
                }
            }
            else
            {
                try
                {
                    returnDict.Add("HttpResponse", "Error");
                    returnDict.Add("Body", "No URL specified");
                }
                catch (Exception e)
                {
                    Logs.NewLogItem("Error adding error response: " + e.Message, TraceEventType.Information);
                }
            }
            return returnDict;
        }

        public static void SendPerformanceTestMessage(string headerText, string detailText)
        {
            HttpClient client = new HttpClient();
            HttpRequestMessage requestMessage = new HttpRequestMessage();
            StringContent httpContent = new StringContent(detailText);
            Stream streamResponse = null;
            StreamReader streamReader = null;
            HttpResponseMessage httpResponseClient = null;
            bool continueFlag = true;
            if (IsValidJson(detailText)) System.Environment.SetEnvironmentVariable("fileType", "json", EnvironmentVariableTarget.User);
            else System.Environment.SetEnvironmentVariable("fileType", "xml", EnvironmentVariableTarget.User);
            Dictionary<string, string> headerDict = BuildHttpHeaderInformation(headerText);
            string dictionaryString = "{";
            //returnDict.Add("HeaderRequest", dictionaryString + "}");
            Dictionary<string, string> responseDict = new Dictionary<string, string>();
            string url = "";
            string url2 = url;
            if (headerDict.TryGetValue("Url", out url))
            {
                client.BaseAddress = new Uri(url);
                client.Timeout = TimeSpan.FromMinutes(10);
                client.DefaultRequestHeaders.Connection.Add("Keep-Alive");
                WriteLogItem("url: " + url, TraceEventType.Information);
                if (url == "") continueFlag = false;
                headerDict.Remove("Url");
            }
            if (continueFlag)
            {
                if (headerDict.ContainsKey("Type"))
                {
                    if (headerDict.TryGetValue("Type", out url))
                    {
                        try
                        {
                            if (url == "") continueFlag = false;
                            else requestMessage.Method = new HttpMethod(url);
                            headerDict.Remove("Type");
                        }
                        catch (Exception typeException)
                        {
                            WriteLogItem("Invalid HTTP Type: " + typeException.Message, TraceEventType.Information);
                            continueFlag = false;
                        }
                    }
                }

                if (headerDict.ContainsKey("Accept"))
                {
                    url = "";
                    if (headerDict.TryGetValue("Accept", out url))
                    {
                        if (url != "")
                        {
                            try
                            {
                                requestMessage.Headers.Add("Accept", url);
                                headerDict.Remove("Accept");
                            }
                            catch (Exception acceptException)
                            {
                                WriteLogItem("Invalid HTTP Accept: " + acceptException.Message, TraceEventType.Information);
                                continueFlag = false;
                            }
                        }
                    }
                }
                if (continueFlag)
                {
                    foreach (string key in headerDict.Keys)
                    {
                        try
                        {
                            if (key != "")
                                requestMessage.Headers.Add(key, headerDict[key]);
                        }
                        catch (Exception fr)
                        {
                            WriteLogItem("Unable to read Header:" + fr.ToString(), TraceEventType.Error);
                        }
                    }

                    try
                    {
                        if (System.Environment.GetEnvironmentVariable("fileType", EnvironmentVariableTarget.User) == "json")
                        {
                            var content = new StringContent(detailText, Encoding.UTF8, "application/json");
                            requestMessage.Content = content;
                            httpResponseClient = client.Send(requestMessage);
                        }
                        else
                        {
                            var xmlContent = new StringContent(detailText, Encoding.UTF8, "text/xml");
                            requestMessage.Content = xmlContent;

                            httpResponseClient = client.Send(requestMessage);
                        }

                        if (httpResponseClient.IsSuccessStatusCode)
                        {
                            // returnDict.Add("HttpResponse", httpResponseClient.StatusCode.ToString() + "-" + httpResponseClient.ReasonPhrase.ToString());
                            try
                            {
                                streamResponse = httpResponseClient.Content.ReadAsStream();
                                streamReader = new StreamReader(streamResponse);
                                //returnDict.Add("Body", streamReader.ReadToEnd());
                                continueFlag = true;
                            }
                            finally
                            {
                                streamReader.Close();
                                streamResponse.Close();
                            }
                        }
                        else
                        {
                            //returnDict.Add("HttpResponse", httpResponseClient.StatusCode.ToString());
                            //returnDict.Add("Body", "No response");
                            continueFlag = false;
                            WriteLogItem("Exception with HTTP Response Code:" + httpResponseClient.StatusCode.ToString(), TraceEventType.Error);
                        }
                    }
                    catch (Exception e)
                    {
                        //returnDict.Add("HttpResponse", "Error");
                        //returnDict.Add("Body", e.ToString());
                        continueFlag = false;
                        WriteLogItem("Exception with HTTP Response:" + e.ToString(), TraceEventType.Error);
                        //MessageBox.Show("Exception on getting HTTP Response:" + e.ToString());
                    }
                }
                if (continueFlag)
                {
                    try
                    {
                        List<Variables.VariableList> savedVariableList = Variables.GetVariableDocuments();
                        foreach (var httpResponse in httpResponseClient.Headers)
                        {
                            //returnDict.Add("Header_" + httpResponse.Key, httpResponse.Value.ToString());
                            for (int j = 0; j < savedVariableList.Count; j++)
                            {
                                if (savedVariableList[j].SearchForElement.Contains(httpResponse.Key))
                                {
                                    if (savedVariableList[j].ReplaceWhere.ToLower() != "static" && savedVariableList[j].ReplaceWhere.ToLower() != "body")
                                    {
                                        savedVariableList[j].SavedValue = httpResponse.Value.ToString();
                                        Variables.UpdateVariableSavedValue(httpResponse.Key, httpResponse.Value.ToString());
                                    }
                                }
                            }
                            //Read Token from header based on the Token Element Specified in Options screen
                            if (httpResponse.Key.ToString().ToLower().Contains(System.Environment.GetEnvironmentVariable("tokenElement", EnvironmentVariableTarget.User).ToString().ToLower()))
                            {
                                if (System.Environment.GetEnvironmentVariable("useBearerToken", EnvironmentVariableTarget.User) == "yes")
                                {
                                    System.Environment.SetEnvironmentVariable("bearerToken", httpResponse.Value.ToString(), EnvironmentVariableTarget.User);
                                }

                                if (System.Environment.GetEnvironmentVariable("useBasicToken", EnvironmentVariableTarget.User) == "yes")
                                {
                                    System.Environment.SetEnvironmentVariable("basicToken", httpResponse.Value.ToString(), EnvironmentVariableTarget.User);
                                }

                                if (System.Environment.GetEnvironmentVariable("useAPIKey", EnvironmentVariableTarget.User) == "yes")
                                {
                                    System.Environment.SetEnvironmentVariable("apiKey", httpResponse.Value.ToString(), EnvironmentVariableTarget.User);
                                }
                                if (System.Environment.GetEnvironmentVariable("useCustomKey", EnvironmentVariableTarget.User) == "yes")
                                {
                                    System.Environment.SetEnvironmentVariable("customKey", httpResponse.Value.ToString(), EnvironmentVariableTarget.User);
                                }
                            }
                            else
                            {
                                if (httpResponse.Key.ToString().ToLower().Contains("authorization"))
                                {
                                    switch (httpResponse.Value.ToString().ToLower())
                                    {
                                        case "bearer":
                                            System.Environment.SetEnvironmentVariable("bearerToken", httpResponse.Value.ToString(), EnvironmentVariableTarget.User);
                                            break;
                                        case "api_key":
                                            System.Environment.SetEnvironmentVariable("apiKey", httpResponse.Value.ToString(), EnvironmentVariableTarget.User);
                                            break;
                                        case "api-key":
                                            System.Environment.SetEnvironmentVariable("apiKey", httpResponse.Value.ToString(), EnvironmentVariableTarget.User);
                                            break;
                                        case "basic":
                                            System.Environment.SetEnvironmentVariable("basicToken", httpResponse.Value.ToString(), EnvironmentVariableTarget.User);
                                            break;
                                        default:
                                            System.Environment.SetEnvironmentVariable("customKey", httpResponse.Value.ToString(), EnvironmentVariableTarget.User);
                                            break;
                                    }
                                }
                            }
                        }
                        /*if (System.Environment.GetEnvironmentVariable("fileType", EnvironmentVariableTarget.User) == "json")
                        {
                            buildJsonHttpResponseDict(returnDict);
                            System.Environment.GetEnvironmentVariable("testRunName", EnvironmentVariableTarget.User);
                            SaveExecutionResult(returnDict, System.Environment.GetEnvironmentVariable("testRunName", EnvironmentVariableTarget.User), System.Environment.GetEnvironmentVariable("testSuiteName", EnvironmentVariableTarget.User), System.Environment.GetEnvironmentVariable("testName", EnvironmentVariableTarget.User), incrementCounter);
                        }
                        else
                        {
                            buildXMLHttpResponseDict(returnDict);
                            SaveExecutionResultXML(returnDict, System.Environment.GetEnvironmentVariable("testRunName", EnvironmentVariableTarget.User), System.Environment.GetEnvironmentVariable("testSuiteName", EnvironmentVariableTarget.User), System.Environment.GetEnvironmentVariable("testName", EnvironmentVariableTarget.User), incrementCounter);
                        }*/
                    }
                    finally
                    {
                        client.Dispose();
                        if (streamReader != null)
                            streamReader.Dispose();
                        if (httpResponseClient != null)
                            httpResponseClient.Dispose();
                    }
                }
                /*else
                {
                    try
                    {
                        returnDict.Add("HttpResponse", "Error");
                        returnDict.Add("Body", "No HTTP Method specified");
                    }
                    catch (Exception e)
                    {
                        Logs.NewLogItem("Error adding error response: " + e.Message, TraceEventType.Information);
                    }
                }*/
            }
            /*else
            {
                try
                {
                    returnDict.Add("HttpResponse", "Error");
                    returnDict.Add("Body", "No URL specified");
                }
                catch (Exception e)
                {
                    Logs.NewLogItem("Error adding error response: " + e.Message, TraceEventType.Information);
                }
            }
            return returnDict;*/
        }

        static void buildXMLHttpResponseDict(Dictionary<string, string> returnDict)
        {
            XmlDocument xml = new XmlDocument();
            string tempResponse = returnDict["HttpResponse"].ToString();
            if (!tempResponse.ToLower().Contains("error"))
            {
                xml.LoadXml(returnDict["Body"]);
                XmlNodeList resources = xml.SelectNodes("*");
                //SortedDictionary<string, string> dictionary = new SortedDictionary<string, string>();
                List<Variables.VariableList> savedVariableList = Variables.GetVariableDocuments();
                try
                {
                    foreach (XmlNode node in resources)
                    {
                        if (node.HasChildNodes)
                        {
                            foreach (XmlNode childNode in node.ChildNodes)
                            {
                                if (childNode.HasChildNodes)
                                {
                                    foreach (XmlNode grandChildNode in childNode.ChildNodes)
                                    {
                                        if (grandChildNode.HasChildNodes)
                                        {
                                            foreach (XmlNode greatGrandChildNode in grandChildNode.ChildNodes)
                                            {
                                                if (greatGrandChildNode.InnerText.Length > 0)
                                                {
                                                    string key = grandChildNode.Name.ToString();
                                                    string value = "";
                                                    if (greatGrandChildNode.Value == null) value = greatGrandChildNode.InnerText;
                                                    else value = greatGrandChildNode.Value.ToString();
                                                    if (key != null & value != null)
                                                    {
                                                        for (int j = 0; j < savedVariableList.Count; j++)
                                                        {
                                                            if (savedVariableList[j].SearchForElement.Contains(key))
                                                            {
                                                                if (savedVariableList[j].ReplaceWhere.ToLower() != "static" && savedVariableList[j].ReplaceWhere.ToLower() != "header")
                                                                {
                                                                    Variables.UpdateVariableSavedValue(savedVariableList[j].VariableName, value);
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (grandChildNode.InnerText.Length > 0)
                                            {
                                                string key = childNode.Name.ToString();
                                                string value = "";
                                                if (grandChildNode.Value == null) value = grandChildNode.InnerText;
                                                else value = grandChildNode.Value.ToString();
                                                if (key != null & value != null)
                                                {
                                                    for (int j = 0; j < savedVariableList.Count; j++)
                                                    {
                                                        if (savedVariableList[j].SearchForElement.Contains(key))
                                                        {
                                                            if (savedVariableList[j].ReplaceWhere.ToLower() != "static" && savedVariableList[j].ReplaceWhere.ToLower() != "header")
                                                            {
                                                                Variables.UpdateVariableSavedValue(savedVariableList[j].VariableName, value);
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (childNode.InnerText.Length > 0)
                                    {
                                        string key = node.Name.ToString();
                                        string value = "";
                                        if (childNode.Value == null) value = childNode.InnerText;
                                        else value = childNode.Value.ToString();
                                        if (key != null & value != null)
                                        {
                                            for (int j = 0; j < savedVariableList.Count; j++)
                                            {
                                                if (savedVariableList[j].SearchForElement.Contains(key))
                                                {
                                                    if (savedVariableList[j].ReplaceWhere.ToLower() != "static" && savedVariableList[j].ReplaceWhere.ToLower() != "header")
                                                    {
                                                        Variables.UpdateVariableSavedValue(savedVariableList[j].VariableName, value);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (node.InnerText.Length > 0)
                            {
                                string key = node.Name.ToString();
                                string value = "";
                                if (node.Value == null) value = node.InnerText;
                                else value = node.Value.ToString();
                                if (key != null & value != null)
                                {
                                    for (int j = 0; j < savedVariableList.Count; j++)
                                    {
                                        if (savedVariableList[j].SearchForElement.Contains(node.Name.ToString()))
                                        {
                                            if (savedVariableList[j].ReplaceWhere.ToLower() != "static" && savedVariableList[j].ReplaceWhere.ToLower() != "header")
                                            {
                                                Variables.UpdateVariableSavedValue(savedVariableList[j].VariableName, value);
                                            }
                                        }
                                    }
                                }
                            }
                        }

                    }
                }
                catch (Exception pe)
                {
                    WriteLogItem("XML Issue; " + pe.Message, TraceEventType.Error);
                }
            }
        }

        static void buildJsonHttpResponseDict(Dictionary<string, string> returnDict)
        {
            try
            {
                string tempResponse = returnDict["HttpResponse"].ToString();
                if (!tempResponse.ToLower().Contains("error"))
                {
                    List<Variables.VariableList> savedVariableList = Variables.GetVariableDocuments();
                    var values = JsonConvert.DeserializeObject<Dictionary<string, object>>(returnDict["Body"].ToString());
                    foreach (KeyValuePair<string, object> d in values)
                    {
                        // if (d.Value.GetType().FullName.Contains("Newtonsoft.Json.Linq.JObject"))
                        if (d.Value is JObject)
                        {
                            foreach (JProperty x in (JToken)d.Value)
                            { // if 'obj' is a JObject
                                if (x.Value is JObject)
                                {
                                    foreach (JProperty y in (JToken)x.Value)
                                    {
                                        if (y.Name.ToString().ToLower().Contains(System.Environment.GetEnvironmentVariable("tokenElement", EnvironmentVariableTarget.User).ToString().ToLower()))
                                        {
                                            if (y.Value.ToString().Contains("bearer"))
                                            {
                                                var tempArray = y.Value.ToString().Split(" ".ToCharArray());
                                                System.Environment.SetEnvironmentVariable("bearerToken", tempArray[1], EnvironmentVariableTarget.User);
                                            }
                                            else
                                            {
                                                System.Environment.SetEnvironmentVariable("bearerToken", y.Value.ToString(), EnvironmentVariableTarget.User);
                                            }

                                            for (int j = 0; j < savedVariableList.Count; j++)
                                            {
                                                if (savedVariableList[j].SearchForElement.Contains(y.Name.ToString()))
                                                {
                                                    if (savedVariableList[j].ReplaceWhere.ToLower() != "static" && savedVariableList[j].ReplaceWhere.ToLower() != "header")
                                                    {
                                                        savedVariableList[j].SavedValue = y.Value.ToString();
                                                        Variables.UpdateVariableSavedValue(savedVariableList[j].VariableName, y.Value.ToString());
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (x.Value.ToString().Contains("bearer") | x.Name.ToString().ToLower().Contains(System.Environment.GetEnvironmentVariable("tokenElement", EnvironmentVariableTarget.User).ToString().ToLower()) )
                                    {
                                        var tempArray = x.Value.ToString().Split(" ".ToCharArray());
                                        if (tempArray.Count() > 1)
                                        {
                                            System.Environment.SetEnvironmentVariable("bearerToken", tempArray[1], EnvironmentVariableTarget.User);
                                        }
                                        else
                                        {
                                            System.Environment.SetEnvironmentVariable("bearerToken", x.Value.ToString(), EnvironmentVariableTarget.User);
                                        }
                                    }

                                    for (int j = 0; j < savedVariableList.Count; j++)
                                    {
                                        if (savedVariableList[j].SearchForElement.Contains(x.Name.ToString()))
                                        {
                                            if (savedVariableList[j].ReplaceWhere.ToLower() != "static" && savedVariableList[j].ReplaceWhere.ToLower() != "header")
                                            {
                                                savedVariableList[j].SavedValue = x.Value.ToString();
                                                Variables.UpdateVariableSavedValue(savedVariableList[j].VariableName, x.Value.ToString());
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (d.Value.ToString().Contains("bearer"))
                            {
                                var tempArray = d.Value.ToString().Split(" ".ToCharArray());
                                System.Environment.SetEnvironmentVariable("bearerToken", tempArray[1], EnvironmentVariableTarget.User);
                            }
                            else
                            { 
                                //System.Environment.SetEnvironmentVariable("bearerToken", d.Value.ToString(), EnvironmentVariableTarget.User);
                            }
                            for (int j = 0; j < savedVariableList.Count; j++)
                            {
                                if (savedVariableList[j].SearchForElement.Contains(d.Value.ToString()))
                                {
                                    if (savedVariableList[j].ReplaceWhere.ToLower() != "static" && savedVariableList[j].ReplaceWhere.ToLower() != "header")
                                    {
                                        savedVariableList[j].SavedValue = d.Value.ToString();
                                        Variables.UpdateVariableSavedValue(savedVariableList[j].VariableName, d.Value.ToString());
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception pe)
            {
                WriteLogItem("Issue with buiding JSON Response dictionary:" + pe.ToString(), TraceEventType.Error);
            }
        }

        public static void SaveExecutionResult(Dictionary<string, string> returnDict, string testRunName, string testSuiteName, string testName, int incrementNr)
        {
            try
            {
                string tempResponse = returnDict["HttpResponse"].ToString();
                List<TestSuite.FieldExpectedResult> testList = TestSuite.GetTestExpectedResults(testName);
                string fieldName = "";
                string fieldValue = "";
                string expectedResult = "";
                string testResult = "Fail";
                if (!tempResponse.ToLower().Contains("error"))
                {             
                    var values = JsonConvert.DeserializeObject<Dictionary<string, object>>(returnDict["Body"].ToString());
                    foreach (KeyValuePair<string, object> d in values)
                    {
                        // if (d.Value.GetType().FullName.Contains("Newtonsoft.Json.Linq.JObject"))
                        if (d.Value is JObject)
                        {
                            foreach (JProperty x in (JToken)d.Value)
                            { // if 'obj' is a JObject
                                if (x.Value is JObject)
                                {
                                    foreach (JProperty y in (JToken)x.Value)
                                    {
                                        fieldName = y.Name.ToString();
                                        fieldValue = y.Value.ToString();
                                        for (int c = 0; c < testList.Count; c++)
                                        {
                                            if (testList[c].FieldName == fieldName)
                                            {
                                                if (testList[c].ExpectedResult != null)
                                                    expectedResult = testList[c].ExpectedResult;
                                            }
                                        }
                                        testResult = ValidateDetail(fieldValue, expectedResult);
                                        ExecutionResult.AddTestResult(testRunName, testSuiteName, testName, incrementNr, fieldName, expectedResult, fieldValue, testResult);
                                    }
                                }
                                else
                                {
                                    fieldName = x.Name.ToString();
                                    fieldValue = x.Value.ToString();
                                    for (int c = 0; c < testList.Count; c++)
                                    {
                                        if (testList[c].FieldName == fieldName)
                                        {
                                            if (testList[c].ExpectedResult != null)
                                                expectedResult = testList[c].ExpectedResult;
                                        }
                                    }
                                    testResult = ValidateDetail(fieldValue, expectedResult);
                                    ExecutionResult.AddTestResult(testRunName, testSuiteName, testName, incrementNr, fieldName, expectedResult, fieldValue, testResult);
                                }
                            }
                        }
                        else
                        {
                            fieldName = d.Key.ToString();
                            fieldValue = d.Value.ToString();
                            for (int c = 0; c < testList.Count; c++)
                            {
                                if (testList[c].FieldName == fieldName)
                                {
                                    if (testList[c].ExpectedResult != null)
                                        expectedResult = testList[c].ExpectedResult;
                                }
                            }
                            testResult = ValidateDetail(fieldValue, expectedResult);
                            ExecutionResult.AddTestResult(testRunName, testSuiteName, testName, incrementNr, fieldName, expectedResult, fieldValue, testResult);
                        }
                    }
                }
            }
            catch (Exception pe)
            {
                WriteLogItem("Issue with Adding Test Results:" + pe.ToString(), TraceEventType.Error);
            }
        }

        public static void SaveExecutionResultXML(Dictionary<string, string> returnDict, string testRunName, string testSuiteName, string testName, int incrementNr)
        {
            try
            {
                XmlDocument xml = new XmlDocument();
                string tempResponse = returnDict["HttpResponse"].ToString();
                List<TestSuite.FieldExpectedResult> testList = TestSuite.GetTestExpectedResults(testName);
                string expectedResult = "";
                string testResult = "Fail";
                if (!tempResponse.ToLower().Contains("error"))
                {
                    xml.LoadXml(returnDict["Body"]);
                    XmlNodeList resources = xml.SelectNodes("*");
                    foreach (XmlNode node in resources)
                    {
                        if (node.HasChildNodes)
                        {
                            foreach (XmlNode childNode in node.ChildNodes)
                            {
                                if (childNode.HasChildNodes)
                                {
                                    foreach (XmlNode grandChildNode in childNode.ChildNodes)
                                    {
                                        if (grandChildNode.HasChildNodes)
                                        {
                                            foreach (XmlNode greatGrandChildNode in grandChildNode.ChildNodes)
                                            {
                                                if (greatGrandChildNode.InnerText.Length > 0)
                                                {
                                                    string key = grandChildNode.Name.ToString();
                                                    string value = "";
                                                    if (greatGrandChildNode.Value == null) value = greatGrandChildNode.InnerText;
                                                    else value = greatGrandChildNode.Value.ToString();
                                                    if (key != null & value != null)
                                                    {
                                                        for (int c = 0; c < testList.Count; c++)
                                                        {
                                                            if (testList[c].FieldName == key)
                                                            {
                                                                if (testList[c].ExpectedResult != null)
                                                                    expectedResult = testList[c].ExpectedResult;
                                                            }
                                                        }
                                                        testResult = ValidateDetail(key, expectedResult);
                                                        ExecutionResult.AddTestResult(testRunName, testSuiteName, testName, incrementNr, key, expectedResult, value, testResult);
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (grandChildNode.InnerText.Length > 0)
                                            {
                                                string key = childNode.Name.ToString();
                                                string value = "";
                                                if (grandChildNode.Value == null) value = grandChildNode.InnerText;
                                                else value = grandChildNode.Value.ToString();
                                                if (key != null & value != null)
                                                {
                                                    for (int c = 0; c < testList.Count; c++)
                                                    {
                                                        if (testList[c].FieldName == key)
                                                        {
                                                            if (testList[c].ExpectedResult != null)
                                                                expectedResult = testList[c].ExpectedResult;
                                                        }
                                                    }
                                                    testResult = ValidateDetail(key, expectedResult);
                                                    ExecutionResult.AddTestResult(testRunName, testSuiteName, testName, incrementNr, key, expectedResult, value, testResult);
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (childNode.InnerText.Length > 0)
                                    {
                                        string key = node.Name.ToString();
                                        string value = "";
                                        if (childNode.Value == null) value = childNode.InnerText;
                                        else value = childNode.Value.ToString();
                                        if (key != null & value != null)
                                        {
                                            for (int c = 0; c < testList.Count; c++)
                                            {
                                                if (testList[c].FieldName == key)
                                                {
                                                    if (testList[c].ExpectedResult != null)
                                                        expectedResult = testList[c].ExpectedResult;
                                                }
                                            }
                                            testResult = ValidateDetail(key, expectedResult);
                                            ExecutionResult.AddTestResult(testRunName, testSuiteName, testName, incrementNr, key, expectedResult, value, testResult);
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (node.InnerText.Length > 0)
                            {
                                string key = node.Name.ToString();
                                string value = "";
                                if (node.Value == null) value = node.InnerText;
                                else value = node.Value.ToString();
                                if (key != null & value != null)
                                {
                                    for (int c = 0; c < testList.Count; c++)
                                    {
                                        if (testList[c].FieldName == key)
                                        {
                                            if (testList[c].ExpectedResult != null)
                                                expectedResult = testList[c].ExpectedResult;
                                        }
                                    }
                                    testResult = ValidateDetail(key, expectedResult);
                                    ExecutionResult.AddTestResult(testRunName, testSuiteName, testName, incrementNr, key, expectedResult, value, testResult);
                                }
                            }
                        }

                    }
                }
            }
            catch (Exception pe)
            {
                WriteLogItem("Issue with Adding Test Results:" + pe.ToString(), TraceEventType.Error);
            }
        }

        public static List<TestSuite.FieldExpectedResult> BuildExpectedResultList(string testName, string inputJson)
        {
            List<TestSuite.FieldExpectedResult> returnValue = new List<TestSuite.FieldExpectedResult>();
            TestSuite.FieldExpectedResult newExpectedResult = null;
            try
            {   
                string fieldName = "";
                string fieldValue = "";
                TestSuite.Test newTest = new TestSuite.Test(testName);
                var values = JsonConvert.DeserializeObject<Dictionary<string, object>>(inputJson);
                foreach (KeyValuePair<string, object> d in values)
                {
                    // if (d.Value.GetType().FullName.Contains("Newtonsoft.Json.Linq.JObject"))
                    if (d.Value is JObject)
                    {
                        foreach (JProperty x in (JToken)d.Value)
                        { // if 'obj' is a JObject
                            if (x.Value is JObject)
                            {
                                foreach (JProperty y in (JToken)x.Value)
                                {
                                    fieldName = y.Name.ToString();
                                    fieldValue = y.Value.ToString();
                                    newExpectedResult = new TestSuite.FieldExpectedResult(fieldName, fieldValue);
                                    returnValue.Add(newExpectedResult);
                                }
                            }
                            else
                            {
                                fieldName = x.Name.ToString();
                                fieldValue = x.Value.ToString();
                                newExpectedResult = new TestSuite.FieldExpectedResult(fieldName, fieldValue);
                                returnValue.Add(newExpectedResult);
                            }
                        }
                    }
                    else
                    {
                        fieldName = d.Key.ToString();
                        fieldValue = d.Value.ToString();
                        newExpectedResult = new TestSuite.FieldExpectedResult(fieldName, fieldValue);
                        returnValue.Add(newExpectedResult);
                    }
                }
            } 
            catch (Exception pe)
            {
                WriteLogItem("Issue with Expected Result List:" + pe.ToString(), TraceEventType.Error);
            }
            return returnValue;
        }

        private static Worksheet FindSheet(Workbook workbook, string sheet_name)
        {
            foreach (Worksheet sheet in workbook.Sheets)
            {
                if (sheet.Name == sheet_name) return sheet;
            }
            return null;
        }

        private static void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                WriteLogItem("Exception occurrec while releasing object:" + ex.ToString(), TraceEventType.Error);
            }
            finally
            {
                GC.Collect();
            }
        }

        public static string ReplaceWholeWord(this string text, string word, string bywhat)
        {
            /*static bool IsWordChar(char c) => char.IsLetterOrDigit(c) || c == '_';
            StringBuilder sb = null;
            int p = 0, j = 0;
            while (j < text.Length && (j = text.IndexOf(word, j, StringComparison.Ordinal)) >= 0)
                if ((j == 0 || !IsWordChar(text[j - 1])) &&
                    (j + word.Length == text.Length || !IsWordChar(text[j + word.Length])))
                {
                    sb ??= new StringBuilder();
                    sb.Append(text, p, j - p);
                    sb.Append(bywhat);
                    j += word.Length;
                    p = j;
                }
                else j++;
            if (sb == null) return text;
            sb.Append(text, p, text.Length - p);*/
            string result = "";
            if (text.Length > 0)
            {
                text = text.Replace("[", "").Replace("]", "").Replace("(", "").Replace(")", "");
                word = word.Replace("[", "").Replace("]", "").Replace("(", "").Replace(")", ""); ;
                //string pattern = @"\b" + ToAlphaOnly(word) + "\b";
                //result = Regex.Replace(text, pattern, bywhat);
                result = text.Replace(word, bywhat);
                //MessageBox.Show(result);
            }
            else
            {
                result = "";
            }
            return result;
        }

        public static string ReplaceWholeWord(this string text, string word, string bywhat, string parameterStr)
        {
            string result = "";
            if (text.Length > 0)
            {
                // only replace part as applicable
                string beginStr = "";
                int operationToBeReplaceStart = text.IndexOf(word);
                if (operationToBeReplaceStart > 0)
                {
                    beginStr = text.Substring(0, operationToBeReplaceStart);
                    text = text.Substring(operationToBeReplaceStart);
                }
                int endText = text.IndexOf(parameterStr);
                int newLength = endText + parameterStr.Length + 2;
                if (newLength > text.Length) newLength = text.Length;
                string replaceText1 = text.Substring(0, newLength);
                string replaceText2 = text.Substring(newLength);
                replaceText1 = replaceText1.Replace("[", "").Replace("]", "").Replace("(", "").Replace(")", "");
                word = word.Replace("[", "").Replace("]", "").Replace("(", "").Replace(")", "");
                parameterStr = ToAlphaNumericOnly(parameterStr);
                //string pattern = @"\b" + ToAlphaOnly(word) + "\b";
                //result = Regex.Replace(text, pattern, bywhat);
                result = replaceText1.Replace(word + parameterStr, bywhat);
                if (parameterStr.Length > 0)
                { 
                    result = result.Replace(parameterStr, "");
                }
                result = beginStr + result + replaceText2;
                //MessageBox.Show(result);
            }
            else
            {
                result = "";
            }
            return result;
        }

        public static string ToAlphaNumericOnly(this string input)
        {
            Regex rgx = new Regex("[^a-zA-Z0-9]\\.");
            return rgx.Replace(input, "");
        }

        public static string ToAlphaOnly(this string input)
        {
            Regex rgx = new Regex("[^a-zA-Z]");
            return rgx.Replace(input, "");
        }

        public static string ToNumericOnly(this string input)
        {
            Regex rgx = new Regex("[^0-9]");
            return rgx.Replace(input, "");
        }
        public class RandomGenerator
        {
            // Instantiate random number generator.  
            // It is better to keep a single Random instance 
            // and keep using Next on the same instance.  
            private readonly Random _random = new Random();

            // Generates a random number within a range.      
            public int RandomNumber(int min, int max)
            {
                return _random.Next(min, max);
            }

            // Generates a random string with a given size.    
            public string RandomString(int size, bool lowerCase = false)
            {
                var builder = new StringBuilder(size);

                // Unicode/ASCII Letters are divided into two blocks
                // (Letters 65–90 / 97–122):   
                // The first group containing the uppercase letters and
                // the second group containing the lowercase.  

                // char is a single Unicode character  
                char offset = lowerCase ? 'a' : 'A';
                const int lettersOffset = 26; // A...Z or a..z: length = 26  

                for (var i = 0; i < size; i++)
                {
                    var @char = (char)_random.Next(offset, offset + lettersOffset);
                    builder.Append(@char);
                }

                return lowerCase ? builder.ToString().ToLower() : builder.ToString();
            }

            // Generates a random password.  
            // 4-LowerCase + 4-Digits + 2-UpperCase  
            public string Date(string dateFormat)
            {
                var passwordBuilder = new StringBuilder();

                // 4-Letters lower case   
                passwordBuilder.Append(RandomString(4, true));

                // 4-Digits between 1000 and 9999  
                passwordBuilder.Append(RandomNumber(1000, 9999));

                // 2-Letters upper case  
                passwordBuilder.Append(RandomString(2));
                return passwordBuilder.ToString();
            }
        }

        public static void WriteLogItem(string message, TraceEventType logType)
        {
            /*TraceSource ts = new TraceSource("Logger");
            ts.TraceEvent(logType, 1, message);
            ts.Flush();
            ts.Close();*/
            Logs.NewLogItem(message, logType);
        }

        public static void ClearSessionVariables()
        {
            System.Environment.SetEnvironmentVariable("customKey", "none", EnvironmentVariableTarget.User);
            System.Environment.SetEnvironmentVariable("apiKey", "none", EnvironmentVariableTarget.User);
            System.Environment.SetEnvironmentVariable("basicToken", "none", EnvironmentVariableTarget.User);
            System.Environment.SetEnvironmentVariable("bearerToken", "none", EnvironmentVariableTarget.User);
        }

        public static bool CheckIfFormIsOpen(string formname)
        {

            //FormCollection fc = Application.OpenForms;
            //foreach (Form frm in fc)
            //{
            //    if (frm.Name == formname)
            //    {
            //        return true;
            //    }
            //}
            //return false;

            bool formOpen = System.Windows.Forms.Application.OpenForms.Cast<Form>().Any(form => form.Name == formname);

            return formOpen;
        }

        public static Boolean ExportGrid(DataGridView GridView1, string fileName)
        {
            bool returnValue = true;
            StringBuilder sb = new StringBuilder();
            FileStream stream = null;
            try
            {
                stream = new FileStream(fileName, FileMode.OpenOrCreate);
                using (StreamWriter writer = new StreamWriter(stream, Encoding.UTF8))
                { 
                    for (int k = 0; k < GridView1.Columns.Count; k++)
                    {
                        //add separator
                        if (!GridView1.Columns[k].HeaderText.Contains(".."))
                            sb.Append(GridView1.Columns[k].HeaderText + ",");
                    }
                    sb.AppendLine();

                    for (int i = 0; i < GridView1.Rows.Count; i++)
                    {
                        for (int k = 0; k < GridView1.Columns.Count; k++)
                        {
                            var tempValue = GridView1.Rows[i].Cells[k].Value;
                            string tempValueStr = "";
                            if (tempValue == null) tempValueStr = "";
                            else tempValueStr = GridView1.Rows[i].Cells[k].Value.ToString();
                            if (tempValueStr != "")
                                sb.Append(tempValueStr.Replace("\r\n", "").Replace("\n", "").Replace("\r", "").Replace(",", "").Replace(":", "-") + ",");
                        }
                        sb.AppendLine();
                    }
                    writer.WriteLine(sb.ToString());
                }
                if (stream != null)
                    stream.Dispose();
            }
            catch (Exception e)
            {
                Utilities.WriteLogItem("Grid Excel Export failed with: " + e.ToString(), TraceEventType.Error);
                returnValue = false;
            }
            return returnValue;
        }

        public static string[] GetGridValuesAsCSV(DataGridView GridView1)
        {
            string[] returnValue = new string[GridView1.Rows.Count + 1];
            int counter = 1;
            string tempValue = "";
            try
            {               
                for (int k = 0; k < GridView1.Columns.Count; k++)
                {
                    //add separator
                    returnValue[0] += GridView1.Columns[k].HeaderText + ",";
                }

                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    for (int k = 0; k < GridView1.Columns.Count; k++)
                    {                     
                        var tempValue2 = GridView1.Rows[i].Cells[k].Value;
                        if (tempValue2 != null) tempValue = tempValue2.ToString();
                        if (returnValue[counter] == null) returnValue[counter] = "";
                        if (returnValue[counter].Length > 0)
                            returnValue[counter] += "," + tempValue.Replace("\r\n", "").Replace("\n", "").Replace("\r", "").Replace(",", "").Replace(":", "-");
                        else returnValue[counter] = tempValue.Replace("\r\n", "").Replace("\n", "").Replace("\r", "").Replace(",", "").Replace(":", "-");
                    }     
                    counter++;
                }

            }
            catch (Exception e)
            {
                Utilities.WriteLogItem("Grid Excel screen scrape failed with: " + e.ToString(), TraceEventType.Error);
            }
            return returnValue;
        }

        private static bool IsValidJson(string strInput)
        {
            if (string.IsNullOrWhiteSpace(strInput)) { return false; }
            strInput = strInput.Trim();
            if ((strInput.StartsWith("{") && strInput.EndsWith("}")) || //For object
                (strInput.StartsWith("[") && strInput.EndsWith("]"))) //For array
            {
                try
                {
                    var obj = JToken.Parse(strInput);
                    return true;
                }
                catch (JsonReaderException jex)
                {
                    //Exception in parsing json
                    WriteLogItem(jex.Message, TraceEventType.Error);
                    return false;
                }
                catch (Exception ex) //some other exception
                {
                    WriteLogItem(ex.Message, TraceEventType.Warning);
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public static bool ExecuteJavaScript(string javaScriptText)
        {
            bool returnValue = true;
            try
            {

                Jint.Engine js = new Jint.Engine();
                var result = js.Execute(javaScriptText).GetCompletionValue();
            }
            catch (Exception e)
            {
                Logs.NewLogItem("ExecuteJavaScript failed: " + e.Message, TraceEventType.Error);
                returnValue = false;
            }
            return returnValue;
        }

        public static bool ExecuteGUITest(string testSuiteName, string testRunName, string testName)
        {
            bool returnValue = true;
            int incrementNr = 1;
            try
            {
                List<TestSuite.Test> testList = TestSuite.GetGUITests(testName);
                foreach (TestSuite.Test test in testList)
                { 
                    string url = test.URL;
                    string xpath = test.XPath;
                    string operation = test.Operation;
                    string testData = test.Input;
                    string fieldName = "";
                    string attribute = "";
                    string comparitor = "";
                    string expectedValue = "";
                    string actualValue = "";
                    var fieldExpectedR = test.FieldExpectedResultList;
                    foreach(TestSuite.FieldExpectedResult f in fieldExpectedR)
                    {
                        fieldName = f.FieldName;
                        var expectedArray = f.ExpectedResult.Split(",");
                        attribute = expectedArray[0];
                        comparitor = expectedArray[1];
                        expectedValue = expectedArray[2];
                    }
                    
                    bool passed = Uri.TryCreate(url, UriKind.Absolute, out Uri uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
                    if (!passed)
                    {
                        returnValue = false;
                    }
                    else
                    {
                        try
                        {
                            System.Xml.XPath.XPathExpression expr = System.Xml.XPath.XPathExpression.Compile(xpath);
                            returnValue = true;
                        }
                        catch (System.Xml.XPath.XPathException)
                        {
                            returnValue = false;
                        }
                        if (returnValue)
                        {
                            ChromeOptions options = new ChromeOptions();
                            options.AddArguments("test-type");
                            options.AddArgument("--disable-popup-blocking");
                            options.AddArgument("--ignore-certificate-errors");
                            driver = new ChromeDriver(GetBasePath, options);
                            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
                            driver.Navigate().GoToUrl(url);
                            IWebElement element = null;
                            bool continueFlag = true;
                            try
                            {
                                wait.Until(driver => driver.FindElement(By.XPath(xpath)));
                                element = driver.FindElement(By.XPath(xpath));
                            }
                            catch (NoSuchElementException te)
                            {
                                Logs.NewLogItem("Element with XPath= " + xpath + " not found. " + te.Message, TraceEventType.Error);
                                continueFlag = false;
                            }
                            catch (Exception e)
                            {
                                Logs.NewLogItem("Unhadled exception with Element with XPath= " + xpath + " not found. " + e.Message, TraceEventType.Error);
                                continueFlag = false;
                            }
                            if (continueFlag)
                            {
                                switch (operation)
                                {
                                    case "Click":
                                        element.Click();
                                        break;
                                    case "Type":
                                        element.SendKeys(testData);
                                        break;
                                    case "Select":
                                        SelectElement selectElement = new SelectElement(element);
                                        selectElement.SelectByText(testData);
                                        break;
                                    case "Validate":
                                        if (comparitor.Length < 3)
                                        {
                                            if (attribute.ToLower() != "none") actualValue = element.GetAttribute(attribute);
                                            else actualValue = element.GetAttribute("value");
                                            returnValue = Compare(comparitor, actualValue, expectedValue);
                                        }
                                        else
                                        {
                                            switch (comparitor)
                                            {
                                                case "Exist":
                                                    returnValue = element.Displayed;
                                                    break;
                                                case "DoesNotExist":
                                                    returnValue = !element.Displayed;
                                                    break;
                                                case "EqualToVariableValue":
                                                    actualValue = Variables.GetSavedValue(testData);
                                                    returnValue = Compare("=", actualValue, expectedValue);
                                                    break;
                                                case "EqualToRangeValue":
                                                    actualValue = "To do";
                                                    returnValue = Compare("=", actualValue, expectedValue);
                                                    break;
                                                case "EqualToValue":
                                                    actualValue = element.GetAttribute("value");
                                                    returnValue = Compare("=", testData, actualValue);
                                                    break;
                                                case "EqualToRegEx":
                                                    actualValue = element.GetAttribute("value");
                                                    var reg = new Regex(testData);
                                                    if (reg.IsMatch(actualValue)) returnValue = true;
                                                    else returnValue = false;
                                                    break;
                                            }
                                        }
                                        break;

                                }
                            }
                            string testResult = "";
                            if (returnValue) testResult = "Pass";
                            else testResult = "False";
                            ExecutionResult.AddTestResult(testRunName, testSuiteName, testName, incrementNr, fieldName, expectedValue, actualValue, testResult);
                            incrementNr++;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logs.NewLogItem("ExecuteGUITest failed: " + e.Message, TraceEventType.Error);
                returnValue = false;
            }
            if (driver != null)
            {
                driver.Quit();
                driver = null;
            }
            return returnValue;
        }

        public static void ExtractResource(string path)
        {
            byte[] bytes = Properties.Resources.chromedriver;
            try
            {
                String tempPath = Path.GetFullPath(path).Split("chromedriver.exe")[0];
                System.Environment.SetEnvironmentVariable("webdriver.chrome.driver", tempPath, EnvironmentVariableTarget.User);
                File.WriteAllBytes(path, bytes);
            }
            catch (Exception e)
            {
                Logs.NewLogItem("ChromeDriver already loaded: " + e.Message, TraceEventType.Information);
            }
        }

        public static string GetBasePath
        {
            get
            {
                var basePath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                
                return basePath;
            }
        }


        public static bool Compare(string sign, string actualValue, string expectedValue)
        {
            switch (sign)
            {
                case "<": return decimal.Parse(actualValue) < decimal.Parse(expectedValue);
                case ">": return decimal.Parse(actualValue) > decimal.Parse(expectedValue);
                case "<=": return decimal.Parse(actualValue) <= decimal.Parse(expectedValue);
                case ">=": return decimal.Parse(actualValue) >= decimal.Parse(expectedValue);
                case "=": return actualValue == expectedValue;
                case "!=": return actualValue != expectedValue;
                default: return false;
            }
        }

        public static string[] JsonToCsv(string jsonContent)
        {
            string fieldName = "";
            string fieldValue = "";
            int detailCounter = 1;
            string[] returnValue = new string[100000];
            string lastColValue = "";
            string prevColValue = "";
            var values = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonContent);
            foreach (KeyValuePair<string, object> d in values)
            {
                // if (d.Value.GetType().FullName.Contains("Newtonsoft.Json.Linq.JObject"))            
                if (d.Value is JArray)
                {
                    JArray JArr = (JArray)d.Value;
                    int c = 0;
                    if (returnValue[c] == null) returnValue[c] = "";
                    foreach (JObject parsedObject in JArr.Children<JObject>())
                    {
                        c++;
                        if (returnValue[c] == null) returnValue[c] = "";
                        foreach (JProperty parsedProperty in parsedObject.Properties())
                        {
                            fieldName = parsedProperty.Name;
                            fieldValue = parsedProperty.Value.ToString();
                            
                            if (returnValue[0].Contains(fieldName))
                            {
                                if (returnValue[c].Length == 0) returnValue[c] = fieldValue;
                                else returnValue[c] += "," + fieldValue;
                            }
                            else
                            {
                                if (returnValue[0].Length == 0) returnValue[0] = fieldName;
                                else returnValue[0] += "," + fieldName;
                                if (returnValue[c].Length == 0) returnValue[c] = fieldValue;
                                else returnValue[c] += "," + fieldValue;
                            }
                        }
                    }
                }
                else
                {
                    if (d.Value is JObject)
                    {
                        foreach (JProperty x in (JToken)d.Value)
                        { // if 'obj' is a JObject
                            if (x.Value is JObject)
                            {
                                foreach (JProperty y in (JToken)x.Value)
                                {
                                    fieldName = y.Name.ToString();
                                    fieldValue = y.Value.ToString();
                                }
                            }
                            else
                            {
                                fieldName = x.Name.ToString();
                                fieldValue = x.Value.ToString();
                            }
                        }
                    }
                    else
                    {
                        fieldName = d.Key.ToString();
                        fieldValue = d.Value.ToString();
                    }
                    if (returnValue[0] == null) returnValue[0] = "";
                    if (returnValue[detailCounter] == null) returnValue[detailCounter] = "";
                    if (returnValue[0].Contains(fieldName))
                    {
                        detailCounter++;
                        if (returnValue[detailCounter] == null | returnValue[detailCounter].Length == 0) returnValue[detailCounter] = fieldValue;
                        else returnValue[detailCounter] += "," + fieldValue;
                    }
                    else
                    {
                        if (returnValue[0].Length == 0) returnValue[0] = fieldName;
                        else returnValue[0] += "," + fieldName;
                        if (returnValue[detailCounter].Length == 0) returnValue[detailCounter] = fieldValue;
                        else returnValue[detailCounter] += "," + fieldValue;
                    }
                }
            }
            
            int count = 0;
            for (int g = 0; g < returnValue.Length; g++)
            {
                if (returnValue[g] != null)
                    count++;
            }
            string[] finalReturnValue = new string[count];
            for (int f = 0; f < count; f++)
            {
                if (returnValue[f] != null)
                    finalReturnValue[f] = returnValue[f];
            }
                
            return finalReturnValue;
        }
    }
}