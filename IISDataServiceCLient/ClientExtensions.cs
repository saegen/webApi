using System;
using System.Collections.Generic;
using System.Linq;
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
                    if (client.State == System.ServiceModel.CommunicationState.Opened)
                    {
                        client.Close();
                    }
                    else
                    {
                        client.Abort();
                    }                    
                }
                catch (Exception)
                {
                    client.Abort();
                }
            }
        }
    }
}
