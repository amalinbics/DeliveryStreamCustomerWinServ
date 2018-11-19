//2013.12.17 Ramesh M Modified For CR#61411
//2014.01.10  Ramesh M Added For CR#61759 Added to full From Site business rule
//2014.01.16  Ramesh M Added For CR#61759 added for getting supplier supplypoint 
//2014.01.23  Ramesh M Added For CR#61759 to get supplier supplypoint list
//2014.01.23  Ramesh M Added For CR#61759 Added ShipToID
//2014.01.30  Ramesh M Added For CR#62038 Added AllowDriversToChangeMultiBOL
//2014.02.14  Ramesh M Added For getting vehicle code from customer CR#62289
//2014.03.04  Ramesh M Commented For CR#62032 Loading order item component for all the orders
//2014.03.17  Ramesh M Added For CR#62668 to get home terminal details
//05-20-2014  MadhuVenkat k - Added for CR 63346 - PO & Priority No to Load Information Screen 
//06-26-2014  MadhuVenkat k - Added for CR 64058  - For Add orderDispatchHistory 
//08-26-2014  MadhuVenkat K - Added for CR 64761 -  Order process queue exception handling
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using DeliveryStreamCustomerWinServ.Cloud;
using DeliveryStreamCustomerWinServ.DataAccess;
using System.Diagnostics;
using System.Data;
using System.Reflection;

namespace DeliveryStreamCustomerWinServ
{
    /// <summary>
    /// WebServices class
    /// </summary>
    public class WebServices : ServiceBases
    {
        /// <summary>
        /// CallWebService
        /// Function to Call Web Service
        /// This gets the new orders assigned to driver and post these orders to colud server
        /// </summary>
        /// <param name="endpointConfigName">End point configuration name</param>

        public static String VersionNo = "";
        public static void CallWebService()
        {
            VersionNo = Assembly.GetEntryAssembly().GetName().Version.ToString().Replace(".0", "");
            UpdateVehicles();
            UpdateDrivers();
            UpdateWareHouseUsers();
            UpdateLoads();
            UpdateDeletedLoads();
            UpdateUndispatchLoads();
            AddOrderDispatchHistory();
            LoadStatusMerge();
            UpdateFromSiteBSRule();
            UpdateShiptoFromSite();
            FSDriverLogSched();
            FSFreightBreakdown();
            UpdateFSDeliveryDateSort();
            UpdateAllowDriverstoRemoveOldLoadsRule();
            UpdateBOLImageVolumeStartEndBOLRule();

            String ConfiguredSpeedLimit = ConfigurationManager.AppSettings["IsTankwagonEnabled"].ToString();
            //Tankwagon functions
            if (ConfiguredSpeedLimit.Trim().ToUpper() == "TRUE")
            {
                UpdateCompartment();
                UpdateSupplier();
                UpdateSupplierSupplyPt();
                UpdateProducts();
                UpdateINSite();
                UpdateTWVehicleType();
            }
        }

        /// <summary>
        /// CallWebService
        /// Function to Call Web Service
        /// This gets the new orders assigned to driver and post these orders to colud server
        /// </summary>
        /// <param name="endpointConfigName">End point configuration name</param>
        public static void CallImageWebService()
        {
            ISession session = null;
            string CustomerID = ConfigurationManager.AppSettings["CustomerID"];
            try
            {
                CloudServiceClient client = new CloudServiceClient();

                string Password = ConfigurationManager.AppSettings["Password"];

                session = ServiceBases.GetNewSession();
                List<DAL.SignatureImageRow> rows = DALMethods.GetUpdatedSignatures(session, VersionNo);
                foreach (DAL.SignatureImageRow row in rows)
                {
                    try
                    {
                        //session = ServiceBases.GetNewSession();
                        SignatureImage si = client.GetSignatureImage(CustomerID, Password, row.SysTrxNo, VersionNo);
                        
                        if (si != null && si.Image != null)
                        {
                            DALMethods.UpdateSignatureImage(si.Image, si.DateTime, row.ShipDocSysTrxNo, session, VersionNo);
                            // DALMethods.UpdateDeliveryStreamOrderFrtSignatureImage("2", row.SysTrxNo, session);
                            client.UpdateSignatureStatus(CustomerID, Password, row.SysTrxNo, VersionNo);
                        }
                    }
                    catch (Exception ex)
                    {
                        Logging.LogError(ex);
                        //  12-06-2013 FSWW Ramesh M Added Error log file For CR#?..
                        Logging.WriteToErrorLogFile("Error in SysTrxNo - " + row.SysTrxNo.ToString() + " for the Customer ID - " + CustomerID.ToString());
                    }

                }

                CloseSession(session);

            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                //  12-06-2013 FSWW Ramesh M Added Error log file For CR#?..
                Logging.WriteToErrorLogFile("Error in GetUpdatedSignatures method for the Customer ID -" + CustomerID.ToString());

                CloseSession(session);
            }
            finally
            {
                if (session != null)
                {
                    CloseSession(session);
                }
            }

        }

        /// <summary>
        /// UpdateDrivers
        /// Function to update drivers
        /// </summary>
        private static void UpdateVehicles()
        {
            ISession session = null;
            string CustomerID = ConfigurationManager.AppSettings["CustomerID"];
            try
            {
                CloudServiceClient client = new CloudServiceClient();

                string Password = ConfigurationManager.AppSettings["Password"];

                session = ServiceBases.GetNewSession();
                List<DAL.UpdatedVehicleRow> rows = DALMethods.GetUpdatedVehicles(session, VersionNo);

                foreach (DAL.UpdatedVehicleRow row in rows)
                {
                    try
                    {
                        if (client.UpdateVehicle(CustomerID, Password, row.VehicleID_Ext, row.Code_Ext, row.VehicleType_Ext, row.OverShortSiteID_Ext, VersionNo))
                        {
                            DALMethods.UpdateVehicleFlag(session, row.VehicleID_Ext, VersionNo);
                        }
                    }
                    catch (Exception ex)
                    {
                        // Logging.LogError(ex);
                        //   continue;
                        //  12-06-2013 FSWW Ramesh M Added Error log file For CR#?..
                        Logging.WriteToErrorLogFile("Error in UpdateVehicles  VehicleID - " + row.VehicleID_Ext.ToString() + " for the Customer ID - " + CustomerID.ToString());
                        Logging.WriteLog(String.Format("Error UpdateVehicles in customer {0} , VehicleID {1} and Exception {2} ", CustomerID.ToString(), row.VehicleID_Ext.ToString(), ex.ToString()), EventLogEntryType.Error);
                    }

                }

                CloseSession(session);

            }
            catch (Exception ex)
            {
                //  Logging.LogError(ex);
                //  12-06-2013 FSWW Ramesh M Added Error log file For CR#?..
                Logging.WriteToErrorLogFile("Error in GetUpdatedVehicles method for the Customer ID - " + CustomerID.ToString());
                Logging.WriteLog(String.Format("Error UpdateVehicles in customer {0} and  Exception {1} ", CustomerID.ToString(), ex.ToString()), EventLogEntryType.Error);
                CloseSession(session);
            }
            finally
            {
                if (session != null)
                {
                    CloseSession(session);
                }
            }
        }

        // 06-26-2014  MadhuVenkat k - Added for CR 64058  - For Add orderDispatchHistory 
        /// <summary>
        /// AddOrderDispatchHistory
        /// Function to AddOrderDispatchHistory                                       
        /// </summary>
        private static void AddOrderDispatchHistory()
        {
            ISession session = null;
            string CustomerID = ConfigurationManager.AppSettings["CustomerID"];
            try
            {
                CloudServiceClient client = new CloudServiceClient();

                string Password = ConfigurationManager.AppSettings["Password"];

                session = ServiceBases.GetNewSession();
                List<DAL.Cloud_GetOrderDispatchHistoryRow> rows = DALMethods.GetOrderDispatchHistoryRow(session, VersionNo);

                foreach (DAL.Cloud_GetOrderDispatchHistoryRow row in rows)
                {
                    try
                    {
                        if (client.AddOrderDipatchHistory(row.SysTrxNo_Ext, CustomerID, Password, row.DefDriverID, row.DefVehicleID, row.OldDriverID, row.OldVehicleID, VersionNo))
                        {
                            DALMethods.Sync_deliveryStream(session, row.SysTrxNo_Ext, VersionNo);
                        }
                    }
                    catch (Exception ex)
                    {
                        //Logging.LogError(ex);
                        //   continue;
                        //  12-06-2013 FSWW Ramesh M Added Error log file For CR#?..
                        Logging.WriteToErrorLogFile("Error in VehicleID - " + row.SysTrxNo_Ext.ToString() + " for the Customer ID - " + CustomerID.ToString());
                        Logging.WriteLog(String.Format("Error in customer {0} ,VehicleID {1} and  Exception {2} ", CustomerID.ToString(), row.SysTrxNo_Ext.ToString(), ex.ToString()), EventLogEntryType.Error);
                    }

                }

                CloseSession(session);

            }
            catch (Exception ex)
            {
                //Logging.LogError(ex);
                //  12-06-2013 FSWW Ramesh M Added Error log file For CR#?..
                Logging.WriteToErrorLogFile("Error in GetUpdatedVehicles method for the Customer ID - " + CustomerID.ToString());
                Logging.WriteLog(String.Format("Error in GetUpdatedVehicles method for the customer {0} and  Exception {1} ", CustomerID.ToString(), ex.ToString()), EventLogEntryType.Error);
                CloseSession(session);
            }
            finally
            {
                if (session != null)
                {
                    CloseSession(session);
                }
            }
        }


        ////2013.10.10 FSWW, Ramesh M Added For CR#60620
        private static void LoadStatusMerge()
        {
            ISession session = null;
            string CustomerID = ConfigurationManager.AppSettings["CustomerID"];
            try
            {
                CloudServiceClient client = new CloudServiceClient();

                string Password = ConfigurationManager.AppSettings["Password"];
                String LoadNos = null;

                session = ServiceBases.GetNewSession();

                try
                {
                    LoadNos = client.ListOFLoadNosToMerge(CustomerID, VersionNo);
                    if (LoadNos != null)
                    {
                        DataTable Dt = new DataTable();
                        Dt = DALMethods.OrderStatusMerg(session, LoadNos, VersionNo);
                        foreach (DataRow dr in Dt.Rows)
                        {
                            client.ListOFOrderNosToUpdate(CustomerID, dr["LoadNo"].ToString(), dr["Status"].ToString(), VersionNo);
                        }
                    }

                }
                catch (Exception ex)
                {
                    //Logging.LogError(ex);
                    //   continue;
                    //  12-06-2013 FSWW Ramesh M Added Error log file For CR#?..
                    Logging.WriteToErrorLogFile("Error in LoadNo Status Merging For the Customer ID - " + CustomerID.ToString());
                    Logging.WriteLog(String.Format("Error in LoadNo Status Merging For the Customer ID -  {0}    Exception {1} ", CustomerID.ToString(), ex.ToString()), EventLogEntryType.Error);
                }

                CloseSession(session);

            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                //  12-06-2013 FSWW Ramesh M Added Error log file For CR#?..
                Logging.WriteToErrorLogFile("Error in GetUpdatedVehicles method for the Customer ID - " + CustomerID.ToString());
                Logging.WriteLog(String.Format("Error in GetUpdatedVehicles method For the Customer ID -  {0}    Exception {1} ", CustomerID.ToString(), ex.ToString()), EventLogEntryType.Error);
                CloseSession(session);
            }

            finally
            {
                if (session != null)
                {
                    CloseSession(session);
                }
            }
        }

        // 2014.01.10  Ramesh M Added For CR#61759 Added to full From Site business rule
        private static void UpdateFromSiteBSRule()
        {
            ISession session = null;
            Int32 FromSiteBSRule = 0;
            Int32 MultiBOLBSRule = 0;
            string CustomerID = ConfigurationManager.AppSettings["CustomerID"];
            try
            {
                CloudServiceClient client = new CloudServiceClient();

                string Password = ConfigurationManager.AppSettings["Password"];

                session = ServiceBases.GetNewSession();
                List<DAL.GetUpdatedSysParamsRow> rows = DALMethods.UpdatedFromSiteBSRule(session, VersionNo);

                foreach (DAL.GetUpdatedSysParamsRow row in rows)
                {
                    try
                    {
                        if (row.AllowDriversToChangeFromSite_Ext.ToLower() == "n")
                        {
                            FromSiteBSRule = 0;
                        }
                        else
                        {
                            FromSiteBSRule = 1;
                        }
                        if (row.AllowDriversToChangeMultiBOL_Ext.ToLower() == "n")
                        {
                            MultiBOLBSRule = 0;
                        }
                        else
                        {
                            MultiBOLBSRule = 1;
                        }
                        // 2014.01.30  Ramesh M Added For CR#62038 Added AllowDriversToChangeMultiBOL
                        if (client.UpdateFromSiteBSRule(CustomerID, Password, FromSiteBSRule, MultiBOLBSRule, VersionNo))
                        {
                            DALMethods.UpdateFromSiteBSRuleFlag(session, row.ClientID_Ext, VersionNo);
                        }
                    }
                    catch (Exception ex)
                    {
                        // Logging.LogError(ex);
                        Logging.WriteToErrorLogFile("For the Customer ID - " + CustomerID.ToString());
                        Logging.WriteLog(String.Format("Error in Customer ID -  {0}    Exception {1} ", CustomerID.ToString(), ex.ToString()), EventLogEntryType.Error);
                    }

                }

                CloseSession(session);

            }
            catch (Exception ex)
            {
                // Logging.LogError(ex);
                //  12-06-2013 FSWW Ramesh M Added Error log file 
                Logging.WriteToErrorLogFile("Error in UpdateFromSiteBSRule method for the Customer ID - " + CustomerID.ToString());
                Logging.WriteLog(String.Format("Error in UpdateFromSiteBSRule method for the Customer ID -  {0}    Exception {1} ", CustomerID.ToString(), ex.ToString()), EventLogEntryType.Error);
                CloseSession(session);
            }
            finally
            {
                if (session != null)
                {
                    CloseSession(session);
                }
            }
        }

        // 2014.01.23  Ramesh M Added For CR#61759 to get supplier supplypoint list
        private static void UpdateShiptoFromSite()
        {
            ISession session = null;
            string CustomerID = ConfigurationManager.AppSettings["CustomerID"];

            try
            {
                CloudServiceClient client = new CloudServiceClient();

                string Password = ConfigurationManager.AppSettings["Password"];

                session = ServiceBases.GetNewSession();
                List<DAL.GetUpdatedOEDefSuppSupplyPtRow> rows = DALMethods.UpdatedFromSite(session, VersionNo);

                int count = rows.Count();
                if (count > 0)
                {
                    for (int i = 0; i <= count; i++)
                    {
                        try
                        {
                            if (client.UpdateFromSite(CustomerID, Password, rows[i].ShipToID_Ext, rows[i].SupplierSupplyPtID_Ext, rows[i].SupplierID_Ext, rows[i].SupplyPtID_Ext, rows[i].SupplierDescr_Ext, rows[i].SupplyPtDescr_Ext, rows[i].Address1_Ext, rows[i].Address2_Ext, rows[i].SupplierCode_Ext, rows[i].SupplyPtCode_Ext, VersionNo))
                            {
                                if (i + 1 != count)
                                {

                                    if (rows[i].SupplierSupplyPtID != rows[i + 1].SupplierSupplyPtID)
                                    {
                                        DALMethods.UpdateFromSiteFlag(session, rows[i].ClientID_Ext, rows[i].SupplierSupplyPtID_Ext, VersionNo);
                                    }
                                }
                                else
                                {
                                    DALMethods.UpdateFromSiteFlag(session, rows[i].ClientID_Ext, rows[i].SupplierSupplyPtID_Ext, VersionNo);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Logging.LogError(ex);
                            Logging.WriteToErrorLogFile("For the Customer ID - " + CustomerID.ToString());
                        }
                        count = count - 1;
                    }
                }

                CloseSession(session);

            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                Logging.WriteToErrorLogFile("Error in UpdateShiptoFromSite method for the Customer ID - " + CustomerID.ToString());
                CloseSession(session);
            }
            finally
            {
                if (session != null)
                {
                    CloseSession(session);
                }
            }
        }

        /// <summary>
        /// UpdateDrivers
        /// Function to update drivers
        /// </summary>
        private static void UpdateDrivers()
        {
            ISession session = null;
            string CustomerID = ConfigurationManager.AppSettings["CustomerID"];
            try
            {
                CloudServiceClient client = new CloudServiceClient();

                string Password = ConfigurationManager.AppSettings["Password"];

                session = ServiceBases.GetNewSession();
                List<DAL.UpdatedDriversRow> rows = DALMethods.GetUpdatedDrivers(session, VersionNo);

                foreach (DAL.UpdatedDriversRow row in rows)
                {
                    try
                    {
                        // 2014.03.17  Ramesh M Added For CR#62668 to get home terminal details
                        if (client.UpdateDriver(CustomerID, Password, row.DriverID_Ext, row.UserName_Ext, row.Password_Ext, row.Email_Ext, row.FirstName_Ext, row.MiddleName_Ext,
                            row.LastName_Ext, row.UserType_Ext, row.Co_Name_Ext, row.Co_Addr1_Ext, row.Co_City_Ext, row.Co_State_Ext, row.Co_Zip_Ext, row.HT_Descr_Ext,
                            row.HT_Addr1_Ext, row.HT_City_Ext, row.HT_State_Ext, row.HT_Zip_Ext, row.HazMatDate_Ext, VersionNo))
                        {
                            DALMethods.UpdateDriverFlag(session, row.DriverID_Ext, VersionNo);
                        }
                    }
                    catch (Exception ex)
                    {
                        // Logging.LogError(ex);
                        //   continue;
                        //  12-06-2013 FSWW Ramesh M Added Error log file For CR#?..
                        Logging.WriteToErrorLogFile("Error in DriverID - " + row.DriverID_Ext.ToString() + " for the Customer ID - " + CustomerID.ToString());
                    }

                }

                CloseSession(session);

            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                //  12-06-2013 FSWW Ramesh M Added Error log file For CR#?..
                Logging.WriteToErrorLogFile("Error in GetUpdatedDrivers method for the Customer ID -  " + CustomerID.ToString());
                CloseSession(session);
            }
            finally
            {
                if (session != null)
                {
                    CloseSession(session);
                }
            }
        }

        //2013-08-29 Fsww Ramesh M, Added for To Get Updated wareHouseUsers from Insite Table
        /// <summary>
        /// UpdateWareHouseUsers
        /// Function to UpdateWareHouseUsers
        /// </summary>
        private static void UpdateWareHouseUsers()
        {
            ISession session = null;
            string CustomerID = ConfigurationManager.AppSettings["CustomerID"];
            try
            {
                CloudServiceClient client = new CloudServiceClient();

                string Password = ConfigurationManager.AppSettings["Password"];

                session = ServiceBases.GetNewSession();
                List<DAL.UpdatedInSiteUserRow> rows = DALMethods.GetUpdatedWareHouseUsers(session, VersionNo);

                foreach (DAL.UpdatedInSiteUserRow row in rows)
                {
                    try
                    {
                        if (client.UpdateWareHouseUser(CustomerID, Password, row.SiteID_Ext, row.SiteCode_Ext, row.UserName_Ext, row.Password_Ext, row.UserType_Ext, VersionNo))
                        {
                            DALMethods.UpdatedInSiteUserFlag(session, row.SiteID_Ext, VersionNo);
                        }
                    }
                    catch (Exception ex)
                    {
                        //Logging.LogError(ex);
                        //   continue;
                        //  12-06-2013 FSWW Ramesh M Added Error log file For CR#?..
                        Logging.WriteToErrorLogFile("Error in SiteID - " + row.SiteID_Ext.ToString() + " for the Customer ID - " + CustomerID.ToString());
                        Logging.WriteLog(String.Format("Error in  Customer ID - {0} , SiteID{1} , Exception {2} ", CustomerID.ToString(), row.SiteID_Ext.ToString(), ex.ToString()), EventLogEntryType.Error);
                    }

                }

                CloseSession(session);

            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                //  12-06-2013 FSWW Ramesh M Added Error log file For CR#?..
                Logging.WriteToErrorLogFile("Error in GetUpdatedDrivers method for the Customer ID -  " + CustomerID.ToString());
                CloseSession(session);
            }
            finally
            {
                if (session != null)
                {
                    CloseSession(session);
                }
            }
        }

        private static void FSDriverLogSched()
        {
            ISession session = null;

            string CustomerID = ConfigurationManager.AppSettings["CustomerID"];
            int fsDriverLogSched = 0;
            try
            {
                CloudServiceClient client = new CloudServiceClient();

                string Password = ConfigurationManager.AppSettings["Password"];

                session = ServiceBases.GetNewSession();
                fsDriverLogSched = DALMethods.UpdateFSDriverLogSched(session, VersionNo);

                client.UpdateFSDriverLogSched(CustomerID, Password, fsDriverLogSched, VersionNo);

            }
            catch (Exception ex)
            {

                // 2013-06-12  FSWW Ramesh M Added Error log file For CR#?..
                Logging.WriteToErrorLogFile("Error FSDriverLogSched in FSDriverLogSched method for the Customer ID - " + CustomerID.ToString());
                Logging.WriteLog(String.Format("Error FSDriverLogSched in customer {0}  and Exception {1} ", CustomerID.ToString(), ex.ToString()), EventLogEntryType.Error);
                //Logging.LogError(ex);
                CloseSession(session);
            }
            finally
            {
                if (session != null)
                {
                    CloseSession(session);
                }
            }
        }

        // 2014.01.23  Ramesh M Added For CR#61759 Added ShipToID
        /// <summary>
        /// UpdateLoads
        /// Function to update load
        /// </summary>
        private static void UpdateLoads()
        {

            ISession session = null;
            // 2013-06-12 FSWW Ramesh M changed getting customerID Plcae For CR#?..
            string CustomerID = ConfigurationManager.AppSettings["CustomerID"];
            string OrderStatusUpdation = null;
            try
            {
                CloudServiceClient client = new CloudServiceClient();

                string Password = ConfigurationManager.AppSettings["Password"];

                session = ServiceBases.GetNewSession();
                List<DAL.LoadDetailsRow> rows = DALMethods.GetLoadAssignedToDriver(session, VersionNo);
                Logging.WriteToErrorLogFile("Dinesh UpdateLoads- " + " for the Customer ID - " + rows.Count.ToString());
                foreach (DAL.LoadDetailsRow row in rows)
                {
                    try
                    {
                        Load load = new Load();
                        load.ID = Guid.Empty;
                        load.LoadNo = row.LoadNo_Ext;
                        load.DriverID = row.DriverID_Ext;
                        load.VehicleID = row.VehicleID_Ext;
                        load.CustomerID = CustomerID;
                        load.FSRule_BOLWaitTime = row.BOLWaitTime_Enabled_Ext;
                        load.FSRule_SiteWaitTime = row.SiteWaitTime_Enabled_Ext;
                        load.FSRule_SplitLoad = row.SplitLoad_Enabled_Ext;
                        load.FSRule_SplitDrop = row.SplitDrop_Enabled_Ext;
                        load.FSRule_PumpOut = row.PumpOut_Enabled_Ext;
                        load.FSRule_Diversion = row.Diversion_Enabled_Ext;
                        load.FSRule_MinLoad = row.MinLoad_Enabled_Ext;
                        load.FSRule_Other = row.Other_Enabled_Ext;
                        load.FSRule_BOLWaitTime_Reason = row.BOLWaitTime_Enabled_Reason_Ext;
                        load.FSRule_SiteWaitTime_Reason = row.SiteWaitTime_Enabled_Reason_Ext;
                        load.FSRule_SplitLoad_Reason = row.SplitLoad_Enabled_Reason_Ext;
                        load.FSRule_SplitDrop_Reason = row.SplitDrop_Enabled_Reason_Ext;
                        load.FSRule_PumpOut_Reason = row.PumpOut_Enabled_Reason_Ext;
                        load.FSRule_Diversion_Reason = row.Diversion_Enabled_Reason_Ext;
                        load.FSRule_MinLoad_Reason = row.MinLoad_Enabled_Reason_Ext;
                        load.FSRule_Other_Reason = row.Other_Enabled_Reason_Ext;
                        load.Orders = GetOrdersAssignedToDriver(row.LoadNo, session).ToArray<Order>();
                        load.OrderLoadReviewEnabled = row.OrderLoadReviewEnabled_Ext;
                        load.QtyTolerance = row.QtyTolerance_Ext;
                        load.PercentTolerance = row.PercentTolerance_Ext;
                        load.LoadType = row.LoadType_Ext;
                        // 2014.01.23  Ramesh M Added For CR#61759 Added ShipToID
                        load.ShipToID = row.ShipToID_Ext;
                        // 2014.02.14  Ramesh M Added For getting vehicle code from customer CR#62289
                        load.VehicleCode = row.VehicleCode_Ext;

                        List<DAL.UpdatedDriversRow> Driver_rows = DALMethods.GetDataDriversByID(row.DriverID_Ext, session, VersionNo);
                        string Co_Name = string.Empty;
                        string Co_Addr1 = string.Empty;
                        string Co_City = string.Empty;
                        string Co_State = string.Empty;
                        string Co_Zip = string.Empty;
                        string HT_Descr = string.Empty;
                        string HT_Addr1 = string.Empty;
                        string HT_City = string.Empty;
                        string HT_State = string.Empty;
                        string HT_Zip = string.Empty;
                        DateTime? HazMatDate = null;

                        foreach (DAL.UpdatedDriversRow Driverrow in Driver_rows)
                        {
                            Co_Name = Driverrow.Co_Name_Ext;
                            Co_Addr1 = Driverrow.Co_Addr1_Ext;
                            Co_City = Driverrow.Co_City_Ext;
                            Co_State = Driverrow.Co_State_Ext;
                            Co_Zip = Driverrow.Co_Zip_Ext;
                            HT_Descr = Driverrow.HT_Descr_Ext;
                            HT_Addr1 = Driverrow.HT_Addr1_Ext;
                            HT_City = Driverrow.HT_City_Ext;
                            HT_State = Driverrow.HT_State_Ext;
                            HT_Zip = Driverrow.HT_Zip_Ext;
                            //HazMatDate =null;
                        }
                        //Logging.WriteToErrorLogFile("Dinesh AddLoad- " + " for the Customer ID - " + rows.Count.ToString());
                        // 2013-09-06 FSWW Ramesh M ,For CR#60100 Added LoadType for segregate load type (warehouse,Driver)
                        if (client.AddLoad(CustomerID, Password, load, row.VehicleCode_Ext, row.SleeperRig_Ext, row.UserName_Ext, row.Password_Ext, row.Email_Ext, row.FirstName_Ext, row.MiddleName_Ext, row.LastName_Ext, row.LoadType_Ext, VersionNo))
                        //Co_Name, Co_Addr1, Co_City, Co_State, Co_Zip, HT_Descr, HT_Addr1, HT_City, HT_State, HT_Zip,VersionNo))
                        {
                            //Logging.WriteToErrorLogFile("Dinesh AddLoad 1- " + " for the Customer ID - " + rows.Count.ToString());

                            // 2013-09-19 FSWW Ramesh M ,For CR#59831 Order Status Updation segregation from warehouse loads and vehicle loads
                            if (row.LoadType_Ext.ToLower() == "w")
                            {
                                OrderStatusUpdation = "G";
                            }
                            else
                            {
                                OrderStatusUpdation = "I";
                            }

                            DALMethods.UpdateLoadStatus(session, load.LoadNo, OrderStatusUpdation,CustomerID, VersionNo);
                        }
                    }
                    catch (Exception ex)
                    {
                        //  2013-06-12  FSWW Ramesh M Added Error log file For CR#?..
                        //  2014-08-26  MadhuVenkat K - Added for CR 64761 -  Order process queue exception handling
                        Logging.WriteToErrorLogFile("Error UpdateLoads in LoadNo - " + row.LoadNo.ToString() + " for the Customer ID - " + CustomerID.ToString());
                        Logging.WriteLog(String.Format("Error UpdateLoads in customer {0} , LoadNo {1} and Exception {2} ", CustomerID.ToString(), row.LoadNo.ToString(), ex.ToString()), EventLogEntryType.Error);
                        //   continue;
                        //Logging.LogError(ex);

                    }

                }

                CloseSession(session);

            }
            catch (Exception ex)
            {

                // 2013-06-12  FSWW Ramesh M Added Error log file For CR#?..
                Logging.WriteToErrorLogFile("Error UpdateLoads in GetLoadAssignedToDriver method for the Customer ID - " + CustomerID.ToString());
                Logging.WriteLog(String.Format("Error UpdateLoads in customer {0}  and Exception {1} ", CustomerID.ToString(), ex.ToString()), EventLogEntryType.Error);
                //Logging.LogError(ex);
                CloseSession(session);
            }
            finally
            {
                if (session != null)
                {
                    CloseSession(session);
                }
            }
        }

        /// <summary>
        /// Update Undispatch Loads
        /// </summary>
        private static void UpdateUndispatchLoads()
        {
            ISession session = null;
            string CustomerID = ConfigurationManager.AppSettings["CustomerID"];
            try
            {
                CloudServiceClient client = new CloudServiceClient();

                string Password = ConfigurationManager.AppSettings["Password"];

                session = ServiceBases.GetNewSession();
                List<DAL.Cloud_GetUndispatchLoadRow> rows = DALMethods.GetUndispatchLoad(session);

                foreach (DAL.Cloud_GetUndispatchLoadRow row in rows)
                {
                    try
                    {
                        if (client.UpdateUndispatchLoad(CustomerID, Password, row.LoadNo_Ext, VersionNo))
                        {
                            //Undispatch Pending
                            DALMethods.UpdateLoadStatus(session, row.LoadNo_Ext, "P", CustomerID, VersionNo);
                        }
                        else
                        {
                            //Not Reassigned
                            DALMethods.UpdateLoadStatus(session, row.LoadNo_Ext, "N", CustomerID, VersionNo);
                        }
                    }
                    catch (Exception ex)
                    {
                        //Logging.LogError(ex);
                        //   continue;
                        // 12-06-2013 FSWW Ramesh M Added Error log file For CR#?..
                        Logging.WriteToErrorLogFile("Error in LoadNo - " + row.LoadNo_Ext.ToString() + " for the Customer ID - " + CustomerID.ToString());
                        Logging.WriteLog(String.Format("Error in customer {0} , LoadNo{1} and Exception {2} ", CustomerID.ToString(), row.LoadNo_Ext.ToString(), ex.ToString()), EventLogEntryType.Error);
                    }
                }

                CloseSession(session);
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                // 12-06-2013 FSWW Ramesh M Added Error log file For CR#?..
                Logging.WriteToErrorLogFile("Error in  GetUndispatchLoad method for the Customer ID -" + CustomerID.ToString());
                if (session != null)
                {
                    CloseSession(session);
                }
            }
            finally
            {
                if (session != null)
                {
                    CloseSession(session);
                }
            }
        }

        /// <summary>
        /// UpdateDeletedLoads
        /// Function to updated deleted loads on cloude
        /// </summary>
        private static void UpdateDeletedLoads()
        {
            ISession session = null;
            string CustomerID = ConfigurationManager.AppSettings["CustomerID"];
            try
            {
                CloudServiceClient client = new CloudServiceClient();

                string Password = ConfigurationManager.AppSettings["Password"];

                session = ServiceBases.GetNewSession();
                List<DAL.LoadDetailsRow> rows = DALMethods.GetDeletedLoads(session, VersionNo);

                foreach (DAL.LoadDetailsRow row in rows)
                {
                    Load load = new Load();
                    load.ID = Guid.Empty;
                    load.LoadNo = row.LoadNo_Ext;
                    load.DriverID = row.DriverID_Ext;
                    load.VehicleID = row.VehicleID_Ext;
                    load.CustomerID = CustomerID;
                    load.FSRule_BOLWaitTime = row.BOLWaitTime_Enabled_Ext;
                    load.FSRule_SiteWaitTime = row.SiteWaitTime_Enabled_Ext;
                    load.FSRule_SplitLoad = row.SplitLoad_Enabled_Ext;
                    load.FSRule_SplitDrop = row.SplitDrop_Enabled_Ext;
                    load.FSRule_PumpOut = row.PumpOut_Enabled_Ext;
                    load.FSRule_Diversion = row.Diversion_Enabled_Ext;
                    load.FSRule_MinLoad = row.MinLoad_Enabled_Ext;
                    load.FSRule_Other = row.Other_Enabled_Ext;
                    load.FSRule_BOLWaitTime_Reason = row.BOLWaitTime_Enabled_Reason_Ext;
                    load.FSRule_SiteWaitTime_Reason = row.SiteWaitTime_Enabled_Reason_Ext;
                    load.FSRule_SplitLoad_Reason = row.SplitLoad_Enabled_Reason_Ext;
                    load.FSRule_SplitDrop_Reason = row.SplitDrop_Enabled_Reason_Ext;
                    load.FSRule_PumpOut_Reason = row.PumpOut_Enabled_Reason_Ext;
                    load.FSRule_Diversion_Reason = row.Diversion_Enabled_Reason_Ext;
                    load.FSRule_MinLoad_Reason = row.MinLoad_Enabled_Reason_Ext;
                    load.FSRule_Other_Reason = row.Other_Enabled_Reason_Ext;
                    load.OrderLoadReviewEnabled = row.OrderLoadReviewEnabled_Ext;
                    load.QtyTolerance = row.QtyTolerance_Ext;
                    load.PercentTolerance = row.PercentTolerance_Ext;
                    try
                    {
                        if (client.DeleteLoad(CustomerID, Password, load, row.VehicleCode_Ext, row.SleeperRig_Ext, row.UserName_Ext, row.Password_Ext, row.Email_Ext, row.FirstName_Ext, row.MiddleName_Ext, row.LastName_Ext, VersionNo))
                        {
                            DALMethods.UpdateLoadStatus(session, load.LoadNo, "R", CustomerID,VersionNo);
                        }
                    }
                    catch (Exception ex)
                    {

                        //   continue;
                        // 12-06-2013 FSWW Ramesh M Added Error log file For CR#?..
                        Logging.WriteToErrorLogFile("Error in LoadNo - " + row.LoadNo_Ext.ToString() + " for the Customer ID - " + CustomerID.ToString());
                        Logging.WriteLog(String.Format("Error in customer {0} , LoadNo{1} and Exception {2} ", CustomerID.ToString(), row.LoadNo_Ext.ToString(), ex.ToString()), EventLogEntryType.Error);
                        //Logging.LogError(ex);

                    }

                }

                CloseSession(session);
            }
            catch (Exception ex)
            {

                // 12-06-2013 FSWW Ramesh M Added Error log file For CR#?..
                Logging.WriteToErrorLogFile("Error in  GetDeletedLoads method for the Customer ID -" + CustomerID.ToString());
                Logging.LogError(ex);
                CloseSession(session);
            }
            finally
            {
                if (session != null)
                {
                    CloseSession(session);
                }
            }
        }

        /// <summary>
        /// Get Orders Assigned to Driver/Vehicle from current load
        /// </summary>
        /// <param name="loadNo">load no</param>
        /// <param name="session">session object</param>
        /// <returns>List of Order</returns>
        private static List<Order> GetOrdersAssignedToDriver(String loadNo, ISession session)
        {
            List<Order> lstOrders = new List<Order>();
            List<DAL.OrderDetailsRow> rows = DALMethods.GetOrdersAssignedToDriver(loadNo, session, VersionNo);
            Logging.WriteToErrorLogFile("GetOrdersAssignedToDriver -  " + rows.Count.ToString());
            foreach (DAL.OrderDetailsRow row in rows)
            {
                Order order = new Order();
                order.OrderNo = row.OrderNo_Ext;
                order.SysTrxNo = row.SysTrxNo_Ext;
                order.PromisedDtTm = row.PromisedDateTime_Ext;
                order.DestAddress1 = row.DestAddress1_Ext;
                order.DestAddress2 = row.DestAddress2_Ext;
                order.City = row.City_Ext;
                order.State = row.State_Ext;
                order.Zip = row.Zip_Ext;
                order.Country = row.Country_Ext;
                order.DestSite = row.DestSite_Ext;
                order.DestNotes = GetDestinationNotes(row.DestID_Ext, session);
                order.OrderItems = GetItemsAssignedToDriver(row.SysTrxNo, session).ToArray<OrderItem>();

                // 1/31/2013, Suresh Madhesan, 
                // Added session argument, to send DB connection session 
                order.Notes = GetOrderNotes(row.SysTrxNo, session);
                //2013.12.02  Fsww Ramesh M, Added for  RequestedDtTm CR#61274 
                order.RequestedDtTm = row.RequestedDtTm_Ext;
                order.PONo = row.PONo_Ext;
                order.PriorityNo = row.PriorityNo_Ext;
                lstOrders.Add(order);
            }
            return lstOrders;
        }

        /// <summary>
        /// GetItemsAssignedToDriver
        /// Function to Get Items Assigned To Driver
        /// </summary>
        /// <param name="sysTrxNo">SysTrxNo</param>
        /// <param name="session">session object</param>
        /// <returns>List of Item Assigned to driver</returns>
        private static List<OrderItem> GetItemsAssignedToDriver(Decimal sysTrxNo, ISession session)
        {
            List<OrderItem> lstItems = new List<OrderItem>();
            List<DAL.ItemDetailsRow> rows = DALMethods.GetItemsAssignedToDriver(sysTrxNo, session, VersionNo);
            Logging.WriteToErrorLogFile("Dinesh GetItemsAssignedToDriver loadNo -" + sysTrxNo.ToString() + " - " + rows.Count);
            foreach (DAL.ItemDetailsRow row in rows)
            {
                OrderItem item = new OrderItem();
                item.SysTrxLine = row.SysTrxLine_Ext;
                item.OrderedQty = row.OrderedQty_Ext;
                item.ProdCode = row.ProdCode_Ext;
                item.ProdName = row.ProdName_Ext;
                item.ProdUOM = row.ProdUOM_Ext;
                item.Blend = row.Blend_Ext;
                item.SupplierCode = row.SupplierCode_Ext;
                item.SupplierName = row.SupplierName_Ext;
                item.SupplyPointName = row.SupplyPointName_Ext;
                item.SupplyPointCode = row.SupplyPointCode_Ext;
                item.SupplyPointAddress1 = row.SupplyPointAddress1_Ext;
                item.SupplyPointAddress2 = row.SupplyPointAddress2_Ext;
                item.City = row.City_Ext;
                item.State = row.State_Ext;
                item.Zip = row.Zip_Ext;
                item.Country = row.Country_Ext;
                item.Note = row.Notes_Ext;
                // add by amal to to update ARShipToTankID,ARShipToTankCode               
                item.ARShipToTankID = row.ARShipToTankId_Ext;
                item.ARShipToTankCode = row.ARShipToTankCode_Ext;

                // 2014.03.04  Ramesh M Commented For CR#62032 Loading order item component for all the orders
                //if (item.Blend != "N")
                //{
                item.OrderItemComponent = GetOrderItemsComponentAssignedToDriver(sysTrxNo, item.SysTrxLine, session).ToArray<OrderItemComponent>();
                //}
                lstItems.Add(item);
            }
            return lstItems;
        }

        /// <summary>
        /// GetItemsAssignedToDriver
        /// Function to Get Items Assigned To Driver
        /// </summary>
        /// <param name="sysTrxNo">SysTrxNo</param>
        /// <param name="sysTrxLine">line no</param>
        /// <param name="session">session object</param>
        /// <returns>List of Item Assigned to driver</returns>
        private static List<OrderItemComponent> GetOrderItemsComponentAssignedToDriver(Decimal sysTrxNo, Int32 sysTrxLine, ISession session)
        {
            List<OrderItemComponent> lstItems = new List<OrderItemComponent>();
            List<DAL.OrderItemComponentRow> rows = DALMethods.GetOrderItemsComponentAssignedToDriver(sysTrxNo, sysTrxLine, session, VersionNo);
            Logging.WriteToErrorLogFile("Dinesh GetOrderItemsComponentAssignedToDriver loadNo -" + sysTrxNo.ToString() + " - " + rows.Count);
            foreach (DAL.OrderItemComponentRow row in rows)
            {
                OrderItemComponent item = new OrderItemComponent();
                item.ComponentNo = row.ComponentNo_Ext;
                item.Qty = row.OrderedQty_Ext;
                item.ProdCode = row.ProdCode_Ext;
                item.ProdName = row.ProdName_Ext;
                item.ProdUOM = row.ProdUOM_Ext;
                item.SupplierCode = row.SupplierCode_Ext;
                item.SupplierName = row.SupplierName_Ext;
                item.SupplyPointName = row.SupplyPointName_Ext;
                item.SupplyPointCode = row.SupplyPointCode_Ext;
                item.SupplyPointAddress1 = row.SupplyPointAddress1_Ext;
                item.SupplyPointAddress2 = row.SupplyPointAddress2_Ext;
                item.City = row.City_Ext;
                item.State = row.State_Ext;
                item.Zip = row.Zip_Ext;
                item.Country = row.Country_Ext;
                item.FromCSTankID = row.FromCSTankID_Ext;
                item.ToCSTankID = row.ToCSTankID_Ext;
                item.FromCSTankCode = row.FromCSTankCode_Ext;
                item.ToCSTankCode = row.ToCSTankCode_Ext;

                lstItems.Add(item);
            }
            return lstItems;
        }

        /// <summary>
        /// Get Order Notes
        /// </summary>
        /// <param name="orderNo">order number</param>
        /// <returns>List of order notes</returns>
        private static String GetOrderNotes(decimal sysTranNo, ISession session)
        {
            // 1/31/2013, Suresh Madhesan, 
            // Added session argument, to send DB connection session 
            List<DAL.OrderNoteRow> rows = DALMethods.GetOrderNotes(sysTranNo, session, VersionNo);
            String note = String.Empty;
            Boolean firstFlag = true;

            if (rows != null && rows.Count > 0)
            {
                foreach (DAL.OrderNoteRow row in rows)
                {
                    if (firstFlag)
                    {
                        firstFlag = false;
                        //2013.12.17 FSWW, Ramesh M Modified For CR#61411
                        note = row.Note_Ext;
                    }
                    else
                    {
                        note += "\n" + row.Note;
                    }
                }
            }
            return note;
        }
        /// <summary>
        /// Get Destination notes
        /// </summary>
        /// <param name="destID">destination id</param>
        /// <param name="session">session object</param>
        /// <returns>Destination notes</returns>
        private static String GetDestinationNotes(Int32 destID, ISession session)
        {
            String note = String.Empty;
            Boolean firstFlag = true;
            List<DAL.DestNoteRow> rows = DALMethods.GetDestinationNotes(destID, session, VersionNo);
            foreach (DAL.DestNoteRow row in rows)
            {
                if (firstFlag)
                {
                    note = row.Note;
                    firstFlag = false;
                }
                else
                {
                    note = note + ", " + row.Note;
                }
            }
            return note;
        }

        // 2015-09-30 Added by vinoth for update freight breakdown is Enable or Not
        private static void FSFreightBreakdown()
        {
            ISession session = null;

            string CustomerID = ConfigurationManager.AppSettings["CustomerID"];
            char FrtBrkdown = 'N';
            try
            {
                CloudServiceClient client = new CloudServiceClient();

                string Password = ConfigurationManager.AppSettings["Password"];

                session = ServiceBases.GetNewSession();
                FrtBrkdown = DALMethods.UpdateFreightBreakdown(session, VersionNo);

                client.UpdateFrtBreakdown(CustomerID, Password, FrtBrkdown, VersionNo);

            }
            catch (Exception ex)
            {

                // 2013-06-12  FSWW Ramesh M Added Error log file For CR#?..
                Logging.WriteToErrorLogFile("Error FSFreightBreakdown in FSFreightBreakdown method for the Customer ID - " + CustomerID.ToString() + "-" + FrtBrkdown);
                Logging.WriteLog(String.Format("Error FSFreightBreakdown in customer {0}  and Exception {1} ", CustomerID.ToString(), ex.ToString()), EventLogEntryType.Error);
                //Logging.LogError(ex);
                CloseSession(session);
            }
            finally
            {
                if (session != null)
                {
                    CloseSession(session);
                }
            }
        }

        // 2016-01-06 Added by vinoth for update deliverydatesort is Enable or Not
        private static void UpdateFSDeliveryDateSort()
        {
            ISession session = null;

            string CustomerID = ConfigurationManager.AppSettings["CustomerID"];
            char DeliveryDateSort = 'N';
            try
            {
                CloudServiceClient client = new CloudServiceClient();

                string Password = ConfigurationManager.AppSettings["Password"];

                session = ServiceBases.GetNewSession();
                DeliveryDateSort = DALMethods.GetDeliveryDateSortFlag(session, VersionNo);

                client.UpdateDeliveryDateSort(CustomerID, Password, DeliveryDateSort, VersionNo);

            }
            catch (Exception ex)
            {

                // 2013-06-12  FSWW Ramesh M Added Error log file For CR#?..
                Logging.WriteToErrorLogFile("Error UpdateFSDeliveryDateSort in UpdateFSDeliveryDateSort method for the Customer ID - " + CustomerID.ToString() + " DeliveryDateSort - " + DeliveryDateSort);
                Logging.WriteLog(String.Format("Error UpdateFSDeliveryDateSort in customer {0}  and Exception {1} ", CustomerID.ToString(), ex.ToString()), EventLogEntryType.Error);
                //Logging.LogError(ex);
                CloseSession(session);
            }
            finally
            {
                if (session != null)
                {
                    CloseSession(session);
                }
            }
        }

        // 2016-01-06 Added by vinoth for update deliverydatesort is Enable or Not
        private static void UpdateBOLImageVolumeStartEndBOLRule()
        {
            ISession session = null;

            string CustomerID = ConfigurationManager.AppSettings["CustomerID"];
            DataTable dt = new DataTable();

            try
            {
                CloudServiceClient client = new CloudServiceClient();

                string Password = ConfigurationManager.AppSettings["Password"];

                session = ServiceBases.GetNewSession();
                dt = DALMethods.UpdateBOLImageVolumeStartEndBOLRule(session, VersionNo);

                if (dt.Rows.Count > 0)
                    client.UpdateBOLImageVolumeStartEndBOLRule(CustomerID, Password, Convert.ToChar(dt.Rows[0]["RequireBOLImage"]), Convert.ToChar(dt.Rows[0]["RequireDeliveryImage"]), Convert.ToChar(dt.Rows[0]["DeliveryVolumeBSRule"]), Convert.ToChar(dt.Rows[0]["BOLStartEndDateBSRule"]), VersionNo);

            }
            catch (Exception ex)
            {

                // 2013-06-12  FSWW Ramesh M Added Error log file For CR#?..
                Logging.WriteToErrorLogFile("Error UpdateBOLImageVolumeStartEndBOLRule in method for the Customer ID - " + CustomerID.ToString());
                Logging.WriteLog(String.Format("Error UpdateBOLImageVolumeStartEndBOLRule in customer {0}  and Exception {1} ", CustomerID.ToString(), ex.ToString()), EventLogEntryType.Error);
                //Logging.LogError(ex);
                CloseSession(session);
            }
            finally
            {
                if (session != null)
                {
                    CloseSession(session);
                }
            }
        }

        public static void UpdateAllowDriverstoRemoveOldLoadsRule()
        {
            ISession session = null;

            string CustomerID = ConfigurationManager.AppSettings["CustomerID"];
            DataTable dt = new DataTable();

            try
            {
                CloudServiceClient client = new CloudServiceClient();
                string Password = ConfigurationManager.AppSettings["Password"];
                session = ServiceBases.GetNewSession();
                string isRemoveOldLoadByDriverFlag = DALMethods.GetRemoveOldLoadByDriverFlag(session, VersionNo);
                client.UpdateRemoveLoadFlag(CustomerID, Password, isRemoveOldLoadByDriverFlag, VersionNo);
            }
            catch (Exception ex)
            {
                Logging.WriteToErrorLogFile("Error UpdateAllowDriverstoRemoveOldLoadsRule in method for the Customer ID - " + CustomerID.ToString());
                Logging.WriteLog(String.Format("Error UpdateAllowDriverstoRemoveOldLoadsRule in customer {0}  and Exception {1} ", CustomerID.ToString(), ex.ToString()), EventLogEntryType.Error);                
                CloseSession(session);
            }
            finally
            {
                if (session != null)
                {
                    CloseSession(session);
                }
            }
        }


        #region Tankwagon
        /// <summary>
        /// UpdateDrivers
        /// Function to update drivers
        /// </summary>
        private static void UpdateCompartment()
        {
            ISession session = null;
            string CustomerID = ConfigurationManager.AppSettings["CustomerID"];
            try
            {
                CloudServiceClient client = new CloudServiceClient();

                string Password = ConfigurationManager.AppSettings["Password"];

                session = ServiceBases.GetNewSession();
                List<DAL.VehicleCompartmentsRow> rows = DALMethods.GetVehicleCompartment(session, VersionNo);

                foreach (DAL.VehicleCompartmentsRow row in rows)
                {
                    try
                    {
                        if (client.AddVehicleCompartment(CustomerID, Password, row.VehicleID_Ext, row.CompartmentID_Ext, row.Code_Ext, row.Capacity_Ext, VersionNo))
                        {
                            DALMethods.UpdatedVehicleCompartmentsFlag(session, row.CompartmentID_Ext, VersionNo);
                        }
                    }
                    catch (Exception ex)
                    {
                        // Logging.LogError(ex);
                        //   continue;
                        //  12-06-2013 FSWW Ramesh M Added Error log file For CR#?..
                        Logging.WriteToErrorLogFile("Error in UpdateCompartment  VehicleID - " + row.VehicleID_Ext.ToString() + " for the Customer ID - " + CustomerID.ToString());
                        Logging.WriteLog(String.Format("Error UpdateCompartment in customer {0} , VehicleID {1} and Exception {2} ", CustomerID.ToString(), row.VehicleID_Ext.ToString(), ex.ToString()), EventLogEntryType.Error);
                    }

                }

                CloseSession(session);

            }
            catch (Exception ex)
            {
                //  Logging.LogError(ex);
                //  12-06-2013 FSWW Ramesh M Added Error log file For CR#?..
                Logging.WriteToErrorLogFile("Error in UpdateCompartment method for the Customer ID - " + CustomerID.ToString());
                Logging.WriteLog(String.Format("Error UpdateCompartment in customer {0} and  Exception {1} ", CustomerID.ToString(), ex.ToString()), EventLogEntryType.Error);
                CloseSession(session);
            }
            finally
            {
                if (session != null)
                {
                    CloseSession(session);
                }
            }
        }

        /// <summary>
        /// UpdateDrivers
        /// Function to update drivers
        /// </summary>
        private static void UpdateSupplier()
        {
            ISession session = null;
            string CustomerID = ConfigurationManager.AppSettings["CustomerID"];
            try
            {
                CloudServiceClient client = new CloudServiceClient();



                string Password = ConfigurationManager.AppSettings["Password"];

                session = ServiceBases.GetNewSession();
                List<DAL.SupplierRow> rows = DALMethods.GetSuppliers(session, VersionNo);
                foreach (DAL.SupplierRow row in rows)
                {
                    try
                    {
                        if (client.AddSuppliers(CustomerID, Password, row.SupplierID_Ext, row.Code_Ext, row.Descr_Ext, row.LastModifiedDtTm_Ext, VersionNo))
                        {
                            DALMethods.UpdatedSuppliersFlag(session, row.SupplierID_Ext, VersionNo);

                        }
                    }
                    catch (Exception ex)
                    {
                        Logging.LogError(ex);
                        //   continue;
                        //  12-06-2013 FSWW Ramesh M Added Error log file For CR#?..
                        Logging.WriteToErrorLogFile("Error in UpdateSupplier  VehicleID - " + row.SupplierID_Ext.ToString() + " for the Customer ID - " + CustomerID.ToString());
                        Logging.WriteLog(String.Format("Error UpdateSupplier in customer {0} , VehicleID {1} and Exception {2} ", CustomerID.ToString(), row.SupplierID_Ext.ToString(), ex.ToString()), EventLogEntryType.Error);
                    }

                }

                CloseSession(session);

            }
            catch (Exception ex)
            {
                //  Logging.LogError(ex);
                //  12-06-2013 FSWW Ramesh M Added Error log file For CR#?..
                Logging.WriteToErrorLogFile("Error in UpdateSupplier method for the Customer ID - " + CustomerID.ToString());
                Logging.WriteLog(String.Format("Error UpdateSupplier in customer {0} and  Exception {1} ", CustomerID.ToString(), ex.ToString()), EventLogEntryType.Error);
                CloseSession(session);
            }
            finally
            {
                if (session != null)
                {
                    CloseSession(session);
                }
            }
        }

        /// <summary>
        /// UpdateDrivers
        /// Function to update drivers
        /// </summary>
        private static void UpdateSupplierSupplyPt()
        {
            ISession session = null;
            string CustomerID = ConfigurationManager.AppSettings["CustomerID"];
            try
            {
                CloudServiceClient client = new CloudServiceClient();



                string Password = ConfigurationManager.AppSettings["Password"];

                session = ServiceBases.GetNewSession();
                List<DAL.SupplierSupplyPtRow> rows = DALMethods.GetSupplierSupplyPt(session, VersionNo);
                foreach (DAL.SupplierSupplyPtRow row in rows)
                {
                    try
                    {
                        if (client.AddSupplierSupplyPt(CustomerID, Password, row.SupplierSupplyPtID_Ext, row.SupplierID_Ext, row.LastModifiedDtTm_Ext, row.SupplierSupplyPtCode_Ext, row.SupplierSupplyPtDescr_Ext, VersionNo))
                        {
                            DALMethods.UpdatedSupplierSupplyPtFlag(session, row.SupplierSupplyPtID_Ext, VersionNo);
                        }
                    }
                    catch (Exception ex)
                    {
                        Logging.LogError(ex);
                        //   continue;
                        //  12-06-2013 FSWW Ramesh M Added Error log file For CR#?..
                        Logging.WriteToErrorLogFile("Error in UpdateSupplierSupplyPt  VehicleID - " + row.SupplierSupplyPtID_Ext.ToString() + " for the Customer ID - " + CustomerID.ToString());
                        Logging.WriteLog(String.Format("Error UpdateSupplierSupplyPt in customer {0} , VehicleID {1} and Exception {2} ", CustomerID.ToString(), row.SupplierSupplyPtID_Ext.ToString(), ex.ToString()), EventLogEntryType.Error);
                    }

                }

                CloseSession(session);

            }
            catch (Exception ex)
            {
                //  Logging.LogError(ex);
                //  12-06-2013 FSWW Ramesh M Added Error log file For CR#?..
                Logging.WriteToErrorLogFile("Error in UpdateSupplierSupplyPt method for the Customer ID - " + CustomerID.ToString());
                Logging.WriteLog(String.Format("Error UpdateSupplierSupplyPt in customer {0} and  Exception {1} ", CustomerID.ToString(), ex.ToString()), EventLogEntryType.Error);
                CloseSession(session);
            }
            finally
            {
                if (session != null)
                {
                    CloseSession(session);
                }
            }
        }

        /// <summary>
        /// UpdateDrivers
        /// Function to update drivers
        /// </summary>
        private static void UpdateProducts()
        {
            ISession session = null;
            string CustomerID = ConfigurationManager.AppSettings["CustomerID"];
            try
            {
                CloudServiceClient client = new CloudServiceClient();



                string Password = ConfigurationManager.AppSettings["Password"];

                session = ServiceBases.GetNewSession();
                List<DAL.ProductRow> rows = DALMethods.GetProducts(session, VersionNo);
                foreach (DAL.ProductRow row in rows)
                {
                    try
                    {
                        if (client.AddProducts(row.CompanyID_Ext.ToString(), Password, row.PurchRackid_Ext, row.SupplierSupplyPtID_Ext, row.SupplierID_Ext, row.SupplyPtID_Ext, row.LastModifiedDtTm_Ext, row.ProdcutCode_Ext, row.ProdcutDescr_Ext, VersionNo))
                        {
                            DALMethods.UpdatedProductsFlag(session, row.PurchRackid_Ext, VersionNo);
                        }
                    }
                    catch (Exception ex)
                    {
                        Logging.LogError(ex);
                        //   continue;
                        //  12-06-2013 FSWW Ramesh M Added Error log file For CR#?..
                        Logging.WriteToErrorLogFile("Error in UpdateProducts  VehicleID - " + row.PurchRackid_Ext.ToString() + " for the Customer ID - " + CustomerID.ToString());
                        Logging.WriteLog(String.Format("Error UpdateProducts in customer {0} , VehicleID {1} and Exception {2} ", CustomerID.ToString(), row.PurchRackid_Ext.ToString(), ex.ToString()), EventLogEntryType.Error);
                    }

                }

                CloseSession(session);

            }
            catch (Exception ex)
            {
                //  Logging.LogError(ex);
                //  12-06-2013 FSWW Ramesh M Added Error log file For CR#?..
                Logging.WriteToErrorLogFile("Error in UpdateProducts method for the Customer ID - " + CustomerID.ToString());
                Logging.WriteLog(String.Format("Error UpdateProducts in customer {0} and  Exception {1} ", CustomerID.ToString(), ex.ToString()), EventLogEntryType.Error);
                CloseSession(session);
            }
            finally
            {
                if (session != null)
                {
                    CloseSession(session);
                }
            }
        }


        /// <summary>
        /// UpdateDrivers
        /// Function to update drivers
        /// </summary>
        private static void UpdateINSite()
        {
            ISession session = null;
            string CustomerID = ConfigurationManager.AppSettings["CustomerID"];
            try
            {
                CloudServiceClient client = new CloudServiceClient();



                string Password = ConfigurationManager.AppSettings["Password"];

                session = ServiceBases.GetNewSession();
                List<DAL.INSiteRow> rows = DALMethods.GetINSite(session, VersionNo);
                foreach (DAL.INSiteRow row in rows)
                {
                    try
                    {
                        if (client.AddINSite(row.CustomerID_Ext.ToString(), Password, row.SiteID_Ext, row.Code_Ext, row.LongDescr_Ext, row.LastModifiedDtTm_Ext, VersionNo))
                        {//AddINSite(String companyID, String password, Int32 SiteID, String Code, String LongDescr,  DateTime? LastModifiedDtTm,String CustomerID,  String VersionNo = "")
                            DALMethods.UpdatedINSiteFlag(session, row.SiteID_Ext, VersionNo);
                        }
                    }
                    catch (Exception ex)
                    {
                        Logging.LogError(ex);
                        //   continue;
                        //  12-06-2013 FSWW Ramesh M Added Error log file For CR#?..
                        Logging.WriteToErrorLogFile("Error in UpdateINSite  VehicleID - " + row.SiteID_Ext.ToString() + " for the Customer ID - " + CustomerID.ToString());
                        Logging.WriteLog(String.Format("Error UpdateINSite in customer {0} , VehicleID {1} and Exception {2} ", CustomerID.ToString(), row.SiteID_Ext.ToString(), ex.ToString()), EventLogEntryType.Error);
                    }

                }

                CloseSession(session);

            }
            catch (Exception ex)
            {
                //  Logging.LogError(ex);
                //  12-06-2013 FSWW Ramesh M Added Error log file For CR#?..
                Logging.WriteToErrorLogFile("Error in UpdateINSite method for the Customer ID - " + CustomerID.ToString());
                Logging.WriteLog(String.Format("Error UpdateINSite in customer {0} and  Exception {1} ", CustomerID.ToString(), ex.ToString()), EventLogEntryType.Error);
                CloseSession(session);
            }
            finally
            {
                if (session != null)
                {
                    CloseSession(session);
                }
            }
        }

        /// <summary>
        /// UpdateTWVehicleType
        /// Function to update TankWagon VehicleType
        /// </summary>
        private static void UpdateTWVehicleType()
        {
            ISession session = null;
            string CustomerID = ConfigurationManager.AppSettings["CustomerID"];
            try
            {
                CloudServiceClient client = new CloudServiceClient();



                string Password = ConfigurationManager.AppSettings["Password"];

                session = ServiceBases.GetNewSession();
                List<DAL.Cloud_TW_VehicleTypeRow> rows = DALMethods.GetTWVehicleType(Convert.ToInt32(CustomerID), session, VersionNo);
                foreach (DAL.Cloud_TW_VehicleTypeRow row in rows)
                {
                    try
                    {
                        client.AddTWVehicleType(CustomerID, Password, row.VehicleTypeID_Ext, row.Descr_Ext, row.ClientID_Ext, VersionNo);
                    }
                    catch (Exception ex)
                    {
                        Logging.LogError(ex);
                        //   continue;
                        //  12-06-2013 FSWW Ramesh M Added Error log file For CR#?..
                        Logging.WriteToErrorLogFile("Error in UpdateTWVehicleType  VehicleID - " + row.VehicleTypeID_Ext.ToString() + " for the Customer ID - " + CustomerID.ToString());
                        Logging.WriteLog(String.Format("Error UpdateTWVehicleType in customer {0} , VehicleID {1} and Exception {2} ", CustomerID.ToString(), row.VehicleTypeID_Ext.ToString(), ex.ToString()), EventLogEntryType.Error);
                    }

                }

                CloseSession(session);

            }
            catch (Exception ex)
            {
                //  Logging.LogError(ex);
                //  12-06-2013 FSWW Ramesh M Added Error log file For CR#?..
                Logging.WriteToErrorLogFile("Error in UpdateTWVehicleType method for the Customer ID - " + CustomerID.ToString());
                Logging.WriteLog(String.Format("Error UpdateTWVehicleType in customer {0} and  Exception {1} ", CustomerID.ToString(), ex.ToString()), EventLogEntryType.Error);
                CloseSession(session);
            }
            finally
            {
                if (session != null)
                {
                    CloseSession(session);
                }
            }
        }



        #endregion Tankwagon
    }
}
