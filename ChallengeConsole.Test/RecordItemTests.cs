using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GTChallenge.Code.Test
{
      /// <summary>
      /// Unit Test for The RecordItem DataContract handling and mapping of record String
      /// </summary>
      [TestClass]
      public class RecordItemTest
      {
            private Dictionary<string, string[]> _testdatabed;
            public TestContext TestContext { get; set; }

            [TestInitialize]
            public void Init()
            {
                  _testdatabed = new Dictionary<string, string[]>();
                  _testdatabed["MissingGenderFieldValue"] = new[] { "Barrett", "Glenn", "", "10/20/2013 ", "BambooTrees" };
                  _testdatabed["MissingFavoriteFieldValue"] = new[] {"LeSage", "Buck", "Male", "1-24-1977 ", ""};
                  _testdatabed["MissingLastNameFieldValue"] = new[] {"", "Randy", "Male", "01-22-1980 ", "Tatoos"};
                  _testdatabed["MissingFirstNameFieldValue"] = new[] {"Small", "", "Male", "08/24/2000 ", "Tatoos"};
                  _testdatabed["MissingDateOfBirthFieldValue"] = new[] {"Cook", "Mike", "Male", "", "Tatoos"};
                  _testdatabed["CompleteFieldDataSet"] = new[] {"RummerField", "Pat", "Male", "March 08 2016", "Tatoos"};
                  _testdatabed["BadBirthddateFormat"] = new[] {"Rochlitzer", "Tim", "Male", "8241977 ", "Tatoos"};
                  _testdatabed["excesscountofvalues"] = new[]{"Marble", "Celeste", "Female", "3.11.2013 ", "Ikea-Kallax", "Motocycle", "Barbeque", "MiniBikes" };
                  _testdatabed["incorrectcountofvalues"] = new[] {"Lauren", "Sophia", "Female", "3/12/1956"};
                  _testdatabed["norecord"] = new string[] {};
                  _testdatabed["emptyfieldvalues"] = new[] {"", "", "", "", ""};
            }

            #region "RecordItem output based on input"

            [TestMethod]
            public void when_print_comma_delimited_record_string_then_output_formatted_as_expected_test()
            {
                  var recorditem = new RecordItem(_testdatabed["CompleteFieldDataSet"]);
                  var result = recorditem.Print();
                  const string expected = " RummerField   Pat   Male   3/08/2016   Tatoos   ";
                  Debug.WriteLine(result);
                  Assert.AreEqual(expected, result);
            }

            [TestMethod]
            public void when_print_comma_delimited_record_string_with_gender_field_value_missing_then_output_formatted_as_expected_test()
            {
                  var recorditem = new RecordItem(_testdatabed["MissingGenderFieldValue"]);
                  var result = recorditem.Print();
                  const string expected = " Barrett   Glenn      10/20/2013   BambooTrees   ";
                  Assert.AreEqual(expected, result);
            }

            [TestMethod]
            public void when_print_comma_delimited_record_string_and_all_field_values_are_empty_then_output_is_empty_string_as_expected_test()
            {
                  var recorditem = new RecordItem(_testdatabed["emptyfieldvalues"]);
                  var result = recorditem.Print();
                  Assert.IsNull(result);
            }

            [TestMethod]
            public void when_print_record_string_specifying_no_record_then_output_does_not_cause_error_as_expected_test()
            {
                  var recorditem = new RecordItem(_testdatabed["norecord"]);
                  var result = recorditem.Print();
                  Assert.IsNull(result);
            }

            [TestMethod]
            public void when_print_comma_delimited_record_string_with_incorrect_count_of_values_to_fields_then_output_formatted_as_expected_test()
            {
                  var recorditem = new RecordItem(_testdatabed["incorrectcountofvalues"]);
                  var result = recorditem.Print();
                  const string expected = " Lauren   Sophia   Female   3/12/1956      ";
                  Debug.WriteLine(result);
                  Assert.AreEqual(expected, result);
            }

            [TestMethod]
            public void when_print_comma_delimited_record_string_with_excess_count_of_values_to_fields_then_output_formatted_as_expected_test()
            {
                  var recorditem = new RecordItem(_testdatabed["excesscountofvalues"]);
                  var result = recorditem.Print();
                  const string expected = " Marble   Celeste   Female   3/11/2013   Ikea-Kallax   ";
                  Debug.WriteLine(result);
                  Assert.AreEqual(expected, result);
            }

            [TestMethod]
            public void when_print_comma_delimited_record_string_with_dateof_birth_field_value_empty_then_output_formatted_as_expected_test()
            {
                  var recorditem = new RecordItem(_testdatabed["MissingDateOfBirthFieldValue"]);
                  var result = recorditem.Print();
                  const string expected = " Cook   Mike   Male      Tatoos   ";
                  Assert.AreEqual(expected, result);
            }

            [TestMethod]
            public void when_print_comma_delimited_record_string_with_favorite_field_value_empty_then_output_formatted_as_expected_test()
            {
                  var recorditem = new RecordItem(_testdatabed["MissingFavoriteFieldValue"]);
                  var result = recorditem.Print();
                  const string expected = " LeSage   Buck   Male   1/24/1977      ";
                  Debug.WriteLine(result);
                  Assert.AreEqual(expected, result);
            }

            [TestMethod]
            public void when_print_comma_delimited_record_string_with_lastname_field_value_empty_then_output_formatted_as_expected_test()
            {
                  var recorditem = new RecordItem(_testdatabed["MissingLastNameFieldValue"]);
                  var result = recorditem.Print();
                  const string expected = "    Randy   Male   1/22/1980   Tatoos   ";
                  Debug.WriteLine(result);
                  Assert.AreEqual(expected, result);
            }

            [TestMethod]
            public void when_print_comma_delimited_record_string_with_firstname_field_value_empty_then_output_formatted_as_expected_test()
            {
                  var recorditem = new RecordItem(_testdatabed["MissingFirstNameFieldValue"]);
                  var result = recorditem.Print();
                  const string expected = " Small      Male   8/24/2000   Tatoos   ";
                  Debug.WriteLine(result);
                  Assert.AreEqual(expected, result);
            }

            [TestMethod]
            public void when_print_comma_delimited_record_string_with_bad_format_dateofbirth_field_value_empty_then_output_formatted_as_expected_test()
            {
                  var recorditem = new RecordItem(_testdatabed["BadBirthddateFormat"]);
                  var result = recorditem.Print();
                  const string expected = " Rochlitzer   Tim   Male      Tatoos   ";
                  Debug.WriteLine(result);
                  Assert.AreEqual(expected, result);
            }

            #endregion "RecordItem output based on input"

            #region "Record Date Formatting Tests"

            [TestMethod]
            public void when_dateofbirth_is_Shortdate_then_date_string_parsing_pass_test()
            {
                  const string dateofbirthstring = "3/9/2008";
                  var expected = "3/09/2008";
                  var result = RecordItem.FormatDateString(dateofbirthstring);
                  Assert.AreEqual(expected, result);
            }

            [TestMethod]
            public void when_dateofbirth_is_baddate_string_then_date_string_parsing_pass_test()
            {
                  const string dateofbirthstring = "17.3.1956";
                  var expected = string.Empty;
                  var result = RecordItem.FormatDateString(dateofbirthstring);
                  Assert.AreEqual(expected, result);
            }
             
            [TestMethod]
            public void when_dateofbirth_is_Shortdate_random_string_then_date_string_parsing_pass_test()
            {
                  const string dateofbirthstring = "March 08 2016";
                  const string expected = "3/08/2016";
                  var result = RecordItem.FormatDateString(dateofbirthstring);
                  Assert.AreEqual(expected, result);
            }

            [TestMethod]
            public void when_dateofbirth_is_Shortdatetime_string_then_date_string_parsing_pass_test()
            {
                  const string dateofbirthstring = "3/9/2008";
                  var expected = "3/09/2008";
                  var result = RecordItem.FormatDateString(dateofbirthstring);
                  Assert.AreEqual(expected, result);
            }

            [TestMethod]
            public void when_dateofbirth_is_Shortdateshorttime_string_then_date_string_parsing_pass_test()
            {
                  const string dateofbirthstring = "3/9/2008 4:05 PM";
                  var expected = "3/09/2008";
                  var result = RecordItem.FormatDateString(dateofbirthstring);
                  Assert.AreEqual(expected, result);
            }

            [TestMethod]
            public void when_dateofbirth_is_Shortdatelongtime_string_then_date_parsing_pass_test()
            {
                  const string dateofbirthstring = "3/9/2008 4:05:07 PM";
                  var expected = "3/09/2008";
                  var result = RecordItem.FormatDateString(dateofbirthstring);
                  Debug.WriteLine(result);
                  Assert.AreEqual(expected, result);
            }

            [TestMethod]
            public void when_dateofbirth_is_longdate_string_then_date_string_parsing_pass_test()
            {
                  const string dateofbirthstring = "Sunday, March 09, 2008";
                  const string expected = "3/09/2008";
                  var result = RecordItem.FormatDateString(dateofbirthstring);
                  Debug.WriteLine(result);
                  Assert.AreEqual(expected, result);
            }

            [TestMethod]
            public void when_dateofbirth_is_longdateshorttime_string_then_date_string_parsing_pass_test()
            {
                  const string dateofbirthstring = "Sunday, March 09, 2008 4:05 PM";
                  const string expected = "3/09/2008";
                  var result = RecordItem.FormatDateString(dateofbirthstring);
                  Debug.WriteLine(result);
                  Assert.AreEqual(expected, result);
            }

            [TestMethod]
            public void when_dateofbirth_is_fulldatetime_string_then_date_string_parsing_pass_test()
            {
                  const string dateofbirthstring = "Sunday, March 09, 2008 4:05:07 PM";
                  const string expected = "3/09/2008";
                  var result = RecordItem.FormatDateString(dateofbirthstring);
                  Debug.WriteLine(result);
                  Assert.AreEqual(expected, result);
            }

            [TestMethod]
            public void when_dateofbirth_is_monthday_string_then_date_string_parsing_pass_test()
            {
                  const string dateofbirthstring = "March 09";
                  const string expected = "3/09/2016";
                  var result = RecordItem.FormatDateString(dateofbirthstring);
                  Debug.WriteLine(result);
                  Assert.AreEqual(expected, result);
            }

            [TestMethod]
            public void when_dateofbirth_is_monthyear_string_then_date_string_parsing_test()
            {
                  const string dateofbirthstring = "March, 1997";
                  const string expected = "3/01/1997";
                  var result = RecordItem.FormatDateString(dateofbirthstring);
                  Debug.WriteLine(result);
                  Assert.AreEqual(expected, result);
            }

            [TestMethod]
            public void when_dateofbirth_is_RFC123_string_then_date_string_parsing_pass_test()
            {
                  const string dateofbirthstring = "Sun, 09 Mar 2010 16:05:07 GMT";
                  var expected = string.Empty;
                  var result = RecordItem.FormatDateString(dateofbirthstring);
                  Assert.AreEqual(expected, result);
            }

            [TestMethod]
            public void when_dateofbirth_is_sortabledatetime_string_then_date_string_parsing_pass_test()
            {
                  const string dateofbirthstring = "2000-11-09T16:05:07";
                  const string expected = "11/09/2000";
                  var result = RecordItem.FormatDateString(dateofbirthstring);
                  Assert.AreEqual(expected, result);
            }

            [TestMethod]
            public void when_dateofbirth_is_universalsortabledatetime_string_then_date_string_parsing_pass_test()
            {
                  const string dateofbirthstring = "1996-01-31 16:05:07Z";
                  const string expected = "1/31/1996";
                  var result = RecordItem.FormatDateString(dateofbirthstring);
                  Assert.AreEqual(expected, result);
            }

            #endregion  
      }
}