using GTChallenge.REST.Test.ChallengeRecordClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GTChallenge.REST.Test
{
      /// <summary>
      ///       Test Class to test the operations exposed by the ChallengeRecordsService which is a REST service
      /// </summary>
      [TestClass]
      public class GtChallengeRestTest
      {
            private static bool IsValidJson(string input)
            {
                  var temptest = input.Trim();
                  if (temptest.StartsWith("{") && temptest.EndsWith("}"))
                        return true;
                  return temptest.StartsWith("[") && temptest.EndsWith("]");
            }

            #region "POST file records"

            [TestMethod]
            public void when_POST_comma_separated_JSON_records_test()
            {
                  var commadelimitedstring = "Day,Tammy,Female,5/23/2005,3543304771032277";
                  var challengerrecordclient = new ChallengeRecordsServiceClient();
                  var result = challengerrecordclient.PostRecord(commadelimitedstring);
                  Assert.IsTrue(result);
            }

            [TestMethod]
            public void when_POST_space_separated_JSON_records_test()
            {
                  var spacedelimitedpost = "Grant Annie Female 2/20/2011 36223621727383";
                  var challengerrecordclient = new ChallengeRecordsServiceClient();
                  var result = challengerrecordclient.PostRecord(spacedelimitedpost);
                  Assert.IsTrue(result);
            }

            [TestMethod]
            public void when_POST_pipe_separated_JSON_records_test()
            {
                  var pipedelimitedpost = "Reynolds|Diana|Female|23/7/1976|36194439642397";
                  var challengerrecordclient = new ChallengeRecordsServiceClient();
                  var result = challengerrecordclient.PostRecord(pipedelimitedpost);
                  Assert.IsTrue(result);
            }

            [TestMethod]
            public void when_POST_empty_record_JSON_record_test()
            {
                  var challengerrecordclient = new ChallengeRecordsServiceClient();
                  var result = challengerrecordclient.PostRecord(string.Empty);
                  Assert.IsTrue(result);
            } 
            #endregion 

            #region "GET file records" 
            [TestMethod]
            public void when_get_records_sorted_by_gender_then_JSON_records_Out_test()
            {
                  var challengerrecordclient = new ChallengeRecordsServiceClient();
                  var result = challengerrecordclient.GetRecordsByGender();
                  Assert.IsNotNull(result);
            }

            [TestMethod]
            public void when_get_records_sorted_by_name_then_sorted_JSON_records_out_test()
            {
                  var challengerrecordclient = new ChallengeRecordsServiceClient();
                  var result = challengerrecordclient.GetRecordsByName();
                  Assert.IsNotNull(result);
            }

            [TestMethod]
            public void when_get_records_sorted_by_birthdate_then_sorted_JSON_records_Out_test()
            {
                  var challengerrecordclient = new ChallengeRecordsServiceClient();
                  var result = challengerrecordclient.GetRecordsByBirthDate().ToString();
                  Assert.IsTrue(IsValidJson(result));
            } 
            #endregion
      }
}