using System.Runtime.Serialization;

namespace GTChallenge.REST
{
      /// <summary>
      /// FaultMessage Contract that is received on client in case of an error on the service end
      /// </summary>
      [DataContract]
      public class RecordFault
      {
            [DataMember]
            public string ErrorMessage { get; set; }
            [DataMember]
            public string RecordLine { get; set; }
      }
}