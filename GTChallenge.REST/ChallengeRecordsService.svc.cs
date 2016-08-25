using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using GTChallenge.REST.ChallengeRecordManagerClient;

namespace GTChallenge.REST
{
      /// <summary>
      ///       Implementation of IChallengeRecordService service contract -- This is a REST service
      /// </summary>
      public class ChallengeRecordsService : IChallengeRecordsService
      {
            private static ChallengeRecordsManagerClient _challengerecordsmanagerClient; 
             static ChallengeRecordsService()
            {
                  _challengerecordsmanagerClient = new ChallengeRecordsManagerClient();
            }

            private static ChallengeRecordsManagerClient  GetInstance()
             {
                   if (_challengerecordsmanagerClient == null)
                         _challengerecordsmanagerClient = new ChallengeRecordsManagerClient();
                                       return _challengerecordsmanagerClient;
             }
            public bool PostRecord(string dataline)
            {
                  bool result;
                  try
                  {
                        var managerclient = GetInstance();
                        result = managerclient.AppendRecord(dataline);
                  }
                  catch (Exception e)
                  {
                        throw new FaultException<RecordFault>(new RecordFault
                        {
                              RecordLine = e.Message,
                              ErrorMessage = "Record POST failed"
                        });
                  }
                  return result;
            }

            public List<RecordItem> GetRecordsByGender()
            {
                  List<RecordItem> gendersortedrecords;
                  try
                  {
                        var manager =  GetInstance();
                        gendersortedrecords =
                              manager.GetRecords()
                                    .AsEnumerable()
                                    .OrderBy(x => x.Gender)
                                    .ThenBy(y => y.Firstname)
                                    .ThenBy(c => c.Lastname)
                                    .ToList();
                  }
                  catch (Exception e)
                  {
                        throw new FaultException<RecordFault>(new RecordFault
                        {
                              RecordLine = e.Message,
                              ErrorMessage = "Sort Records by Gender Failed"
                        });
                  }
                  return gendersortedrecords;
            }

            public List<RecordItem> GetRecordsByBirthDate()
            {
                  List<RecordItem> birthdatesortedresults;
                  try
                  {
                        var managerclient = GetInstance();
                        birthdatesortedresults = managerclient.GetRecords().OrderBy(x => x.Dateofbirth).ToList();
                  }
                  catch (Exception e)
                  {
                        throw new FaultException<RecordFault>(new RecordFault
                        {
                              RecordLine = e.Message,
                              ErrorMessage = "Sort Records by Birthdate Failed"
                        });
                  }
                  return birthdatesortedresults;
            }

            public List<RecordItem> GetRecordsByName()
            {
                  List<RecordItem> nameSortedRecords;
                  try
                  {
                        var managerclient =  GetInstance();
                        nameSortedRecords =
                              managerclient.GetRecords()
                                    .AsQueryable()
                                    .OrderByDescending(x => x.Firstname)
                                    .ThenBy(y => y.Lastname).ToList();
                  }
                  catch (Exception e)
                  {
                        throw new FaultException<RecordFault>(new RecordFault
                        {
                              RecordLine = e.Message,
                              ErrorMessage = "Sort Records by Name Failed"
                        });
                  }
                  return nameSortedRecords;
            }
      }
}