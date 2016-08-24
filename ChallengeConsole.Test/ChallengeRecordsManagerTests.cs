using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GTChallenge.Code.Test
{
      /// <summary>
      /// Test class for console app exposed functionality which includes a service(ChallengeRecordsManager) and ServiceHost(ChallengeServiceHost)
      /// </summary>
      [TestClass]
      public class ChallengeRecordsManagerTests
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
                  _testFileLog[Path.Combine(_testfilePath, "commadelimitedrecords_02.txt")] = Commaelimiter;
                  _testFileLog[Path.Combine(_testfilePath, "commadelimitedrecords.txt")] = Commaelimiter;
                  _testFileLog[Path.Combine(_testfilePath, "spaceDelimitedRecords.txt")] = Spacedelimiter;
                  _testFileLog[Path.Combine(_testfilePath, "spaceDelimitedRecordsMultidateFormat.txt")] = Spacedelimiter;
                  _testFileLog[Path.Combine(_testfilePath, "PipeDelimitedRecords.txt")] = Pipedelimiter;
                  _testFileLog[Path.Combine(_testfilePath, "PipeDelimitedRecordsMultidateFormat.txt")] = Pipedelimiter;
            }

            #region  compare single record import and print output
            [TestMethod]
            public void when_appending_single_space_delimited_record__and_getRecords_called_then_record_correctly_formatted_success_test()
            { 
                   var manager = new ChallengeRecordsManager(' '); 
                  const string post = "Martin Janet Female 02/15/2000 3589427281916450";
                  manager.AppendRecord(post);
                  var record = manager.GetRecords().FirstOrDefault();
                  var result = record == null ? string.Empty : record.Print();
                  const string expected = " Martin   Janet   Female   2/15/2000   3589427281916450   ";
                  Assert.AreEqual(expected, result);
            }
             
            [TestMethod]
            public void when_appending_single_comma_delimited_record_and_getRecords_called_then_record_correctly_formatted_success_test()
            {
                  var manager = new ChallengeRecordsManager(',');
                  const string post = "Wagner,Jane,Female,12/4/1977,5602231883971808";
                  manager.AppendRecord(post);
                  var record = manager.GetRecords().FirstOrDefault();
                  var result = record == null ? string.Empty : record.Print();
                  const string expected = " Wagner   Jane   Female   12/04/1977   5602231883971808   ";
                  Assert.AreEqual(expected, result);
            }
             
            [TestMethod]
            public void when_appending_single_pipe_delimited_file_records__and_getRecords_called_then_record_correctly_formatted_success_test()
            {
                  var manager = new ChallengeRecordsManager('|');
                  const string post = "Reid|Brian|Male|13.3.1972|4026644554390283";
                  manager.AppendRecord(post);
                  var record = manager.GetRecords().FirstOrDefault();
                  var result = record == null ? string.Empty : record.Print();
                  const string expected = " Reid   Brian   Male      4026644554390283   ";
                  Assert.AreEqual(expected, result);
            }
#endregion

            #region  File record imports

            [TestMethod]
            public void when_importing_space_delimited_file_records_then_record_import_success_test()
            {
                  var manager = new ChallengeRecordsManager();
                  var testfile = Path.Combine(_testfilePath, "spaceDelimitedRecords.txt");
                  manager.AppendRecord(testfile, _testFileLog[testfile]);
                  var record = manager.GetRecords().Count();
                  Debug.WriteLine(record);
                  Assert.IsTrue(record > 10);
            }
             
            [TestMethod]
            public void when_import_commma_delimited_file_records_then_record_import_success_test()
            {
                  var manager = new ChallengeRecordsManager();
                  var testfile = Path.Combine(_testfilePath, "commadelimitedrecords.txt");
                  manager.AppendRecord(testfile, _testFileLog[testfile]);
                  var record = manager.GetRecords().Count();
                  Debug.WriteLine(record);
                  Assert.IsTrue(record > 10);
            }

            [TestMethod]
            public void when_importing_pipe_delimited_file_records_then_record_import_success_test()
            {
                  var manager = new ChallengeRecordsManager();
                  var testfile = Path.Combine(_testfilePath, "PipeDelimitedRecords.txt");
                  manager.AppendRecord(testfile, _testFileLog[testfile]);
                  var record = manager.GetRecords().Count();
                  Debug.WriteLine(record);
                  Assert.IsTrue(record > 8);
            }
            #endregion

            #region Posting  multiple records

            [TestMethod]
            public void when_append_records__and_get_records_called_then_correct_record_count_return_success_test()
            {
                  var manager = new ChallengeRecordsManager('|', ' ', ',');
                  const string post0 = "Stewart|Albert|Male|1969/11/26|3572200444859209";
                  const string post1 = "Tucker,Andrew,Male,7/4/2012,3532356280584530";
                  const string post2 = "Martin Janet Female 02/15/2000 3589427281916450";
                  manager.AppendRecord(post0);
                  manager.AppendRecord(post1);
                  manager.AppendRecord(post2);
                  var result = manager.GetRecords().Count();
                  const int expected = 3;
                  Assert.AreEqual(expected, result);
            }

            [TestMethod]
            public void when_append_records_with_one_empty_record_included_and_get_records_called_then_correct_record_count_return_success_test()
            {
                  var manager = new ChallengeRecordsManager('|', ' ', ',');
                  const string post0 = "Burke,Dorothy,Female,4 / 19 / 1999,337941438151721";
                  const string post1 = "Patterson,Laura,Female,1/26/1998,3552861342552511";
                  const string post2 = "Martin Janet Female 02/15/2000 3589427281916450";
                  const string post3 = "    ";
                  manager.AppendRecord(post0);
                  manager.AppendRecord(post1);
                  manager.AppendRecord(post2);
                  manager.AppendRecord(post3);
                  var result = manager.GetRecords().Count();
                  const int expected = 4;
                  Assert.AreEqual(expected, result);
            }
#endregion
      }
}