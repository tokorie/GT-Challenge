using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using GTChallenge.REST.ChallengeRecordManagerClient;

namespace GTChallenge.REST
{ 
      /// <summary>
      /// REST service Contract IChallengeRecordsService
      /// </summary>
      [ServiceContract]
      public interface IChallengeRecordsService
      {
            [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, UriTemplate = "/records")]
            [OperationContract]
            [FaultContract(typeof(RecordFault))]
            bool PostRecord(string dataline);
             
            [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/records/gender")]
            [OperationContract]
            [FaultContract(typeof(RecordFault))]
            List<RecordItem> GetRecordsByGender(); 

            [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/records/birthdate")]
            [OperationContract]
            [FaultContract(typeof(RecordFault))]
            List<RecordItem> GetRecordsByBirthDate();
             
            [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/records/name")]
            [OperationContract]
            [FaultContract(typeof(RecordFault))]
            List<RecordItem> GetRecordsByName();
      }
}