using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;

namespace GTChallenge.Code
{
      /// <summary>
      ///       Implementation of file record manager service as a singleton service with concurrency enabled
      /// </summary>
      [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
      public class ChallengeRecordsManager : IChallengeRecordsManager
      {
            private static List<RecordItem> _records;
            private static List<int> _delimiters;

            public ChallengeRecordsManager()
            {
                  _records = new List<RecordItem>();
                  _delimiters = new List<int>();
            }

            public ChallengeRecordsManager(params char[] delimiters)
            {
                  _records = new List<RecordItem>();
                  _delimiters = new List<int>();
                  foreach (var delimiter in delimiters)
                        _delimiters.Add(delimiter);
            }

            /// <summary>
            ///       Append new record to existing records list
            /// </summary>
            /// <param name="dataline"></param>
            /// <returns>true if operation succeed, else false</returns>
            public bool AppendRecord(string dataline)
            {
                  var splitarray = dataline.Split(_delimiters.Select(c => Convert.ToChar(c)).ToArray());
                  _records.Add(new RecordItem(splitarray));

                  return true;
            }

            /// <summary>
            ///       Get the records collected so far in record  list
            /// </summary>
            /// <returns>collection of records</returns>
            public List<RecordItem> GetRecords()
            {
                  return _records;
            }

            /// <summary>
            ///       Append new record to existing records list
            /// </summary>
            /// <param name="filename">name of file record was extracted from</param>
            /// <param name="delimiter">delimiter contained in raw record string</param>
            public void AppendRecord(string filename, char delimiter)
            {
                  if (!File.Exists(filename))
                        throw new FileNotFoundException();
                  _delimiters.Add(delimiter);
                  using (var textreader = new StreamReader(filename))
                  {
                        while (textreader.Peek() >= 0)
                        {
                              var record = textreader.ReadLine();
                              if (record != null) _records.Add(new RecordItem(record.Split(delimiter)));
                        }
                  }
            }
            /// <summary>
            /// Print records to outputstream
            /// </summary>
            /// <param name="outstream">Output stream to which records are written</param>
            public void PrintRecords(Stream outstream)
            {
                  using (var standardOutput = new StreamWriter(outstream))
                  {
                        standardOutput.AutoFlush = true;
                        var records =
                              GetRecords().OrderBy(x => x.Gender).ThenBy(y => y.Firstname).ThenBy(c => c.Lastname);
                        PrintToOutStream(standardOutput,
                              "Output 1 – sorted by gender (females before males) then by last name ascending.", records);
                        records = records.OrderBy(x => x.Dateofbirth);
                        PrintToOutStream(standardOutput, "Output 2 – sorted by birth date, ascending", records);
                        records = records.OrderByDescending(x => x.Dateofbirth);
                        PrintToOutStream(standardOutput, "Output 3 – sorted by last name, descending", records);
                  }
            }

            /// <summary>
            ///       Print the individual records contained in the record list
            /// </summary>
            /// <param name="outstream">OutStream to which records are written</param>
            /// <param name="headertitle">Title line for printed records</param>
            /// <param name="records">List of records</param>
            private static void PrintToOutStream(StreamWriter outstream, string headertitle,  IEnumerable<RecordItem> records)
            {
                  outstream.WriteLine(headertitle);
                  outstream.WriteLine();
                  foreach (var recorditem in records)
                        outstream.WriteLine(recorditem.Print());
                  outstream.WriteLine();
                  outstream.WriteLine();
            }
      }
}