using System;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace GTChallenge.Code
{
      public class ChallengeServiceHost
      {
            private readonly Uri _tcpBaseAddress;
            private ServiceHost _svchost;
            /// <summary>
            /// Service Base URI
            /// </summary>
            public Uri BaseAddress { get; set; } 
            /// <summary>
            /// Instance of running service(Singleton instance)
            /// </summary>
            public ChallengeRecordsManager SingletonInstance
            {
                  get { return (ChallengeRecordsManager)_svchost.SingletonInstance; }
            }
            /// <summary>
            /// State of Service Host
            /// </summary>
            public CommunicationState State
            {
                  get
                  {
                        if (_svchost == null) return CommunicationState.Closed;
                        return _svchost.State;
                  }
            }
            /// <summary>
            ///       Service Host Constructor
            /// </summary>
            public ChallengeServiceHost()
            {
                  _tcpBaseAddress = new Uri("net.tcp://localhost:8020/");
                  BaseAddress = _tcpBaseAddress;
            }

            /// <summary>
            ///       1.Create a ServiceHost for the IChallengeRecordsManager type and  provide the base address.
            ///       2.Open the ServiceHostBase to create listeners and start listening for messages.
            /// </summary>
            /// <param name="manager">instance of service to host</param>
            public void Start(IChallengeRecordsManager manager)
            {
                  Console.WriteLine("Starting Console Service");
                  
                  _svchost = new ServiceHost(manager, _tcpBaseAddress);
                  _svchost.AddServiceEndpoint(typeof(IChallengeRecordsManager), new NetTcpBinding(), "");
                  var metadataBehavior = _svchost.Description.Behaviors.Find<ServiceMetadataBehavior>();
                  if (metadataBehavior == null)
                  {
                        metadataBehavior = new ServiceMetadataBehavior();
                        _svchost.Description.Behaviors.Add(metadataBehavior);
                  }

                  _svchost.AddServiceEndpoint(ServiceMetadataBehavior.MexContractName,
                        MetadataExchangeBindings.CreateMexTcpBinding(), "mex");
                  _svchost.Open();
            }

            /// <summary>
            ///       Close the service host
            /// </summary>
            public void Stop()
            {
                  Console.WriteLine("Stopping Console Service");
                  if (_svchost == null) return;
                  _svchost.Close();
                  _svchost = null;
            }
      }
}