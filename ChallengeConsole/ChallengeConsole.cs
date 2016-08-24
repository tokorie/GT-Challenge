using System;
using System.Collections.Generic;
using System.IO;
using GTChallenge.Code;

//****************************************************************************************************************************************
//ASSUMPTIONS
//---------------
//1.  the delimiters (commas, pipes and spaces) do not appear anywhere in the data values themselves. 
//2. No where in files are multiple delimiters used
//3. Each record is separated from the next using line end
//4. No particular rule on the handling of empty records and empty fields
//5. No particular rules given on when and how the REST service should be able to communicate with console app to obtain records read in from files
//6. No Specifics given on how the delimiter information will be known of each file provided
//7. No specifics given on the order or details of processing of individual input file
//****************************************************************************************************************************************

namespace GTChallenge.ConsoleApp
{
      /// <summary>
      /// Console application exposes a service with which it can communicate with other applications such as the REST service 
      /// </summary>
      public static class ChallengeConsole
      {
            /// <summary>
            ///       Program entry point
            /// </summary>
            internal static void Main()
            {
                  var host = new ChallengeServiceHost();
                  try
                  {
                        IDictionary<string, int> inputfiles = new Dictionary<string, int>();
                        host = new ChallengeServiceHost();
                        GetFileInput(inputfiles);
                        var manager = new ChallengeRecordsManager();
                        host.Start(manager); 
                        foreach (var item in inputfiles)
                              manager.AppendRecord(item.Key, (char) item.Value);
                        manager.PrintRecords(Console.OpenStandardOutput());
                  }
                  catch (Exception)
                  {
                        host.Stop();
                  }
                  finally
                  {
                        host.Stop();
                  }
            }
            /// <summary>
            /// Retrieve information on the input files to the system
            /// </summary>
            /// <param name="inputfiles"></param>
            internal static void GetFileInput(IDictionary<string, int> inputfiles)
            {
                  var maxfilecount = 1;
                  while (maxfilecount <= 3)
                  {
                        Console.Write("{0}. Enter file name:  ", maxfilecount);
                        var filename = Console.ReadLine();
                        //verify that the file name provided is not a duplicate entry
                        if (inputfiles.Keys.Contains(filename))
                        {
                              Console.WriteLine( "The File name provided has already been specified. Please provide a correct filename");
                              continue;
                        }
                        //confirm that the provided file exists in the directory specified
                        if (!File.Exists(filename))
                        {
                              Console.WriteLine(  "The File does not exist in directory specified. Please provide a correct filename");
                              continue;
                        }
                        Console.Write(" Please provide  the record delimiter for  file {0}  :", filename);
                        var delimiter = Console.Read();
                        inputfiles[filename] = delimiter; //add the filename and associated delimiter to the dictionary
                        maxfilecount++;
                  } //end while loop 
                  //begin hosting of service
            }
      }
}