using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using IISDataServiceCLient.UserService;
namespace IISDataServiceCLient
{
    public static class ClientExtensions
    {
        public static void CloseOrAbort(this UserServiceClient client)
        {
            if (client != null)
            {
                try
                {
                    client.Close();                    
                }
                catch (Exception)
                {
                    client.Abort();
                }
            }
        }
        public static void DisposeService(ICommunicationObject service)
        {

            if (service != null)
            {
                try
                {
                    service.Close();
                }
                catch
                {
                    service.Abort();
                }
            }
        }
    }
}
