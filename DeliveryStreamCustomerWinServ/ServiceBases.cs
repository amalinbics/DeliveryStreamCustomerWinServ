using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using DeliveryStreamCustomerWinServ.DataAccess;
using System.Diagnostics;

namespace DeliveryStreamCustomerWinServ
{
    /// <summary>
    /// ServiceBases class
    /// </summary>
    public class ServiceBases
    {
        /// <summary>
        /// Default constructor for ServiceBase
        /// </summary>
        static ServiceBases()
        {

        }

        /// <summary>
        /// GetConnectionString
        /// Function to get connection string for app setting
        /// </summary>
        /// <returns>Connection string</returns>
        private static string GetConnectionString()
        {
            ConnectionStringSettingsCollection connectionStrings = ConfigurationManager.ConnectionStrings;

            foreach (ConnectionStringSettings connection in connectionStrings)
            {
                if (connection.Name != ApplicationConstants.Connection.ConnectionString)
                    continue;
                return connection.ConnectionString;
            }

            throw new ApplicationException(String.Format(ApplicationConstants.Errors.ConnectionString, ApplicationConstants.Connection.ConnectionString));
        }

        /// <summary>
        /// GetNewSession
        /// Function to get new session
        /// </summary>
        /// <returns>session - ISession</returns>
        public static ISession GetNewSession()
        {
            ISession session = new Session(GetConnectionString());
            session.Open();           
            return session;
        }

        /// <summary>
        /// Function to close current session
        /// </summary>
        /// <param name="session">Session object</param>
        protected static void CloseSession(ISession session)
        {
            try
            {
                session.Close();
                session = null;
            }
            catch (Exception ex)
            {
                session = null;
                Logging.LogError(ex);
            }

        }
    }
}
