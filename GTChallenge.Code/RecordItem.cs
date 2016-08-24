using System;
using System.Runtime.Serialization;

namespace GTChallenge.Code
{
      /// <summary>
      ///       Record item data contract implementation
      /// </summary>
      [DataContract]
      public class RecordItem : IRecordItem
      {
            /// <summary>
            ///       Constructor
            /// </summary>
            /// <param name="fieldvalues">values of fields in record</param>
            public RecordItem(string[] fieldvalues)
            {
                  MapFieldValues(fieldvalues);
            }
 
            [DataMember]
            public string Lastname { get; set; }

            [DataMember]
            public string Firstname { get; set; }

            [DataMember]
            public string Gender { get; set; }

            [DataMember]
            public string Dateofbirth { get; set; }

            [DataMember]
            public string Favorite { get; set; }

            /// <summary>
            ///       Mapping of values to record item fields
            /// </summary>
            /// <param name="fieldvalues">values to map to individual fields</param>
            private void MapFieldValues(string[] fieldvalues)
            {
                  if (fieldvalues == null)
                        throw new ArgumentNullException();
                  Action<string>[] fieldmappings =
                  {
                        x => Lastname = x ?? string.Empty,
                        x => Firstname = x ?? string.Empty,
                        x => Gender = x ?? string.Empty,
                        x => Dateofbirth = x != null ? FormatDateString(x) : string.Empty,
                        x => Favorite = x ?? string.Empty
                  };
                  for (var i = 0; i < fieldmappings.Length; i++)
                        if (i >= fieldvalues.Length)
                              break;
                        else
                              fieldmappings[i](fieldvalues[i]);
            }

            /// <summary>
            ///       Parse and format date field
            /// </summary>
            /// <param name="datestring">string representation of date field to be formatted</param>
            /// <returns></returns>
            internal static string FormatDateString(string datestring)
            {
                  DateTime c;
                  return DateTime.TryParse(datestring, out c) ? string.Format("{0:M/dd/yyyy}", c) : string.Empty;
            }

            /// <summary>
            ///       Format record detail for printing
            /// </summary>
            /// <returns>String detail to output</returns>
            public string Print()
            {
                  var result = string.Format(" {0}   {1}   {2}   {3}   {4}   ", Lastname, Firstname, Gender, Dateofbirth,
                        Favorite);
                  return string.IsNullOrWhiteSpace(result) ? null : result;
            }
      }
}