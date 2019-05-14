using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text.RegularExpressions;
using System.Web;


namespace DataServiceTest
{
    public static class Utilities
    {
        public static void CloseOrAbortService(this ICommunicationObject service)
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