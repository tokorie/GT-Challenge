using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GTChallenge.Code.Test
{
      /// <summary>
      /// Unit Test Class for testing of Console Application  capabilities, particularly loading of records from file
      /// </summary>
      [TestClass]
      public class ConsoleAppTest
      {
            private const char Pipedelimiter = '|';
            private const char Spacedelimiter = ' ';
            private const char Commaelimiter = ',';
            private IDictionary<string, char> _testFileLog;
            private string _testfilePath;

            [TestInitialize]
            public void Initialize()
            {
                  _testfilePath = @"C:\Users\Tracy\OneDrive\C#\Guarantee Trust Code Challenge\ChallengeConsole\TestDataFiles\CompleteRecords";
                  _testFileLog = new Dictionary<string, char>();
                  _testFileLog["commadelimitedrecords_02.txt"] = Commaelimiter;
                  _testFileLog["commadelimitedrecords.txt"] = Commaelimiter;
                  _testFileLog["spaceDelimitedRecords.txt"] = Spacedelimiter;
                  _testFileLog["spaceDelimitedRecordsMultidateFormat.txt"] = Spacedelimiter;
                  _testFileLog["PipeDelimitedRecords.txt"] = Pipedelimiter;
                  _testFileLog["PipeDelimitedRecordsMultidateFormat.txt"] = Pipedelimiter;
            }

            [TestMethod]
            public void when_input_records_in_space_delimited_file_then_record_import_success_test()
            {
                  var manager = new ChallengeRecordsManager();
                  var testfile = Path.Combine(_testfilePath, "spaceDelimitedRecords.txt");
                  manager.AppendRecord(testfile, _testFileLog["spaceDelimitedRecords.txt"]);
                  var record = manager.GetRecords().Count();
                  Debug.WriteLine(record);
                  Assert.IsTrue(record > 10);
            }

            [TestMethod]
            public void when_input_records_in_commma_delimited_file_then_record_import_success_test()
            {
                  var manager = new ChallengeRecordsManager();
                  var testfile = Path.Combine(_testfilePath, "commadelimitedrecords.txt");
                  manager.AppendRecord(testfile, _testFileLog["commadelimitedrecords.txt"]);
                  var record = manager.GetRecords().Count();
                  Debug.WriteLine(record);
                  Assert.IsTrue(record > 10);
            }

            [TestMethod]
            public void when_input_records_in_pipe_delimited_file_then_record_import_success_test()
            {
                  var manager = new ChallengeRecordsManager();
                  var testfile = Path.Combine(_testfilePath, "PipeDelimitedRecords.txt");
                  manager.AppendRecord(testfile, _testFileLog["PipeDelimitedRecords.txt"]);
                  var record = manager.GetRecords().Count();
                  Debug.WriteLine(record);
                  Assert.IsTrue(record > 8);
            }

      }
}