using System.Collections.Generic;
using System.ServiceModel;

namespace GTChallenge.Code
{
      /// <summary>
      ///       File record management service contract
      /// </summary>
      [ServiceContract]
      public interface IChallengeRecordsManager
      {
            [OperationContract]
            List<RecordItem> GetRecords();

            [OperationContract]
            bool AppendRecord(string dataline);
      }
}