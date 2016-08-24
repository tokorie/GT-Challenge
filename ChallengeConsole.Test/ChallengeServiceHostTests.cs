using System.ServiceModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GTChallenge.Code.Test
{
      /// <summary>
      /// Unit Test class for ChallengeService Host Class Validation
      /// </summary>
      [TestClass]
      public class ChallengeServiceHostTests
      {
            private ChallengeServiceHost _serviceHost;

            [TestInitialize]
            public void Initialize()
            {
                  _serviceHost = new ChallengeServiceHost();
            }

            [TestMethod]
            public void OrderedTests()
            {
                  when_challenge_service_start_and_opened_then_reference_to_instancecontext_is_not_null_test();
                  when_challenge_service_stopped_and_state_is_closed_then_access_to_instanceconstext_is_null_test();
            }

            private void when_challenge_service_start_and_opened_then_reference_to_instancecontext_is_not_null_test()
            {
                  var expected = new ChallengeRecordsManager();
                  _serviceHost.Start(expected);
                  var result = _serviceHost.SingletonInstance;
                  Assert.AreSame(expected, result);
            }

            private void when_challenge_service_stopped_and_state_is_closed_then_access_to_instanceconstext_is_null_test()
            {
                  var result = _serviceHost.SingletonInstance;
                  _serviceHost.Stop();
                  result = _serviceHost.State == CommunicationState.Closed ? null : result;
                  Assert.AreSame(null, result);
            }

            [TestCleanup]
            public void Cleanup()
            {
                  if(_serviceHost !=null)
                        _serviceHost.Stop();
            }
      }
}