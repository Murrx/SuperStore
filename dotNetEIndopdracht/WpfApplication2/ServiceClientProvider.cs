using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApplication2.SuperStoreServiceReference;
using System.ServiceModel;

namespace WpfApplication2
{
    public class ServiceClientProvider
    {
        private static SuperStoreServiceInterfaceClient client;

        public static SuperStoreServiceInterfaceClient GetInstance(){
            if (client == null || client.State.Equals(CommunicationState.Faulted) || client.State.Equals(CommunicationState.Closed) || client.State.Equals(CommunicationState.Closing))
            {
                client = new SuperStoreServiceInterfaceClient();
            }
            
            return client;
        }

        public static void CloseClient(){
            client.Close();
        }
    }
}
