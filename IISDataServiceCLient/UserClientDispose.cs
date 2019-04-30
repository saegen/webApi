using System;
using System.ServiceModel;

namespace IISDataServiceCLient.UserService
{
    public partial class UserServiceClient : IDisposable
    {
        #region IDisposable implementation

        /// <summary>
        /// IDisposable.Dispose implementation, calls Dispose(true).
        /// </summary>
        void IDisposable.Dispose()
        {
            Dispose(true);
        }

        /// <summary>
        /// Dispose worker method. Handles graceful shutdown of the
        /// client even if it is an faulted state.
        /// </summary>
        /// <param name="disposing">Are we disposing (alternative
        /// is to be finalizing)</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                try
                {
                    if (State != CommunicationState.Faulted)
                    {
                        Close();
                    }
                }
                finally
                {
                    if (State != CommunicationState.Closed)
                    {
                        Abort();
                    }
                }
            }
        }

        /// <summary>
        /// Finalizer.
        /// </summary>
        ~UserServiceClient()
        {
            Dispose(false);
        }

        #endregion
    }
}
