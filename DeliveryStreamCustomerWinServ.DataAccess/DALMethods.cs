// 2014.01.10  Ramesh M Added For CR#61759 Added to full From Site business rule
// 2014.01.17  Ramesh M Added For CR#61759 to get list of supplier supply point
// 05-20-2014  MadhuVenkat k - Added for CR 63346 - PO & Priority No to Load Information Screen 
// 06-26-2014  MadhuVenkat k - Added for CR 64058  - For update Sync_deliveryStream 
// 06-26-2014  MadhuVenkat k - Added for CR 64058  - For Get OrderDispatchHistoryRow 
// 08-22-2014  MadhuVenkat k - Added for CR 64744 - Destnotes is not updating, if there are more than one notes for the site.
// 02-03-2015 Madhu, Added for CR#66164 ToIncrease Order Items -> Notes size to 500 from 255 column in Service to allow the processing larges notes.
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using DeliveryStreamCustomerWinServ.DataAccess.DALTableAdapters;

namespace DeliveryStreamCustomerWinServ.DataAccess
{
    /// <summary>
    /// DALMethods class
    /// </summary>
    public class DALMethods
    {
        #region Get
        /// <summary>
        /// GetUpdatedVehicles
        /// </summary>
        /// <param name="session">session object</param>
        /// <returns>list of updated vehicles</returns>
        public static List<DAL.UpdatedVehicleRow> GetUpdatedVehicles(ISession session, String VersionNo = "")
        {
            UpdatedVehicleTableAdapter ta = new UpdatedVehicleTableAdapter();
            ta.CurrentConnection = session;
            return (ta.GetUpdatedVehicle().Select().Cast<DAL.UpdatedVehicleRow>().ToList());
        }
        // 2014.01.10  Ramesh M Added For CR#61759 Added to full From Site business rule
        public static List<DAL.GetUpdatedSysParamsRow> UpdatedFromSiteBSRule(ISession session, String VersionNo = "")
        {
            GetUpdatedSysParamsTableAdapter ta = new GetUpdatedSysParamsTableAdapter();
            ta.CurrentConnection = session;
            return (ta.GetFromSiteBSRule().Select().Cast<DAL.GetUpdatedSysParamsRow>().ToList());
        }
        // 2014.01.17  Ramesh M Added For CR#61759 to get list of supplier supply point
        public static List<DAL.GetUpdatedOEDefSuppSupplyPtRow> UpdatedFromSite(ISession session, String VersionNo = "")
        {
            GetUpdatedOEDefSuppSupplyPtTableAdapter ta = new GetUpdatedOEDefSuppSupplyPtTableAdapter();
            ta.CurrentConnection = session;
            return (ta.GetUpdatedFromSites().Select().Cast<DAL.GetUpdatedOEDefSuppSupplyPtRow>().ToList());
        }

        /// <summary>
        /// GetUpdatedDrivers
        /// Function to Get Updated Driver details
        /// </summary>
        /// <param name="session">Session object</param>
        /// <returns>List of Drivers</returns>
        public static List<DAL.UpdatedDriversRow> GetUpdatedDrivers(ISession session, String VersionNo = "")
        {
            UpdatedDriversTableAdapter ta = new UpdatedDriversTableAdapter();
            ta.CurrentConnection = session;
            return (ta.GetUpdatedDrivers().Select().Cast<DAL.UpdatedDriversRow>().ToList());
        }

        public static List<DAL.UpdatedDriversRow> GetDataDriversByID(int DriverID, ISession session, String VersionNo = "")
        {
            UpdatedDriversTableAdapter ta = new UpdatedDriversTableAdapter();
            ta.CurrentConnection = session;
            return (ta.GetDataDriversByID(DriverID).Select().Cast<DAL.UpdatedDriversRow>().ToList());
        }

        // 08-29-2013 Fsww Ramesh M, Added for CR#?.. To Get Updated wareHouseUsers from Insite Table
        /// <summary>
        /// GetUpdatedWareHouseUsers
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public static List<DAL.UpdatedInSiteUserRow> GetUpdatedWareHouseUsers(ISession session, String VersionNo = "")
        {
            UpdatedInSiteUserTableAdapter ta = new UpdatedInSiteUserTableAdapter();
            ta.CurrentConnection = session;
            return (ta.GetUpdatedWareHosueUsers().Select().Cast<DAL.UpdatedInSiteUserRow>().ToList());
        }

        /// <summary>
        /// GetLoadAssignedToDriver
        /// Function to Get Load Assigned To Driver
        /// </summary>
        /// <param name="session">Session object</param>
        /// <returns>List of Load Assigned</returns>
        public static List<DAL.LoadDetailsRow> GetLoadAssignedToDriver(ISession session, String VersionNo = "")
        {
            LoadDetailsTableAdapter ta = new LoadDetailsTableAdapter();
            ta.CurrentConnection = session;
            return (ta.GetNewLoadAssigned().Select().Cast<DAL.LoadDetailsRow>().ToList());
        }

        /// <summary>
        /// GetDeletedLoads
        /// Function to Get Deleted Loads
        /// </summary>
        /// <param name="session">Session object</param>
        /// <returns>List of Load Assigned</returns>
        public static List<DAL.LoadDetailsRow> GetDeletedLoads(ISession session, String VersionNo = "")
        {
            LoadDetailsTableAdapter ta = new LoadDetailsTableAdapter();
            ta.CurrentConnection = session;
            return (ta.GetDeletedLoads().Select().Cast<DAL.LoadDetailsRow>().ToList());
        }

        /// <summary>
        /// GetOrdersAssignedToDriver
        /// Function to Get Orders Assigned To Driver
        /// </summary>
        /// <param name="loadNo">Load number</param>
        /// <param name="session">Session object</param>
        /// <returns>List of Order Assigned</returns>
        public static List<DAL.OrderDetailsRow> GetOrdersAssignedToDriver(String loadNo, ISession session, String VersionNo = "")
        {
            try
            {
                OrderDetailsTableAdapter ta = new OrderDetailsTableAdapter();
                ta.CurrentConnection = session;
                return (ta.GetOrdersAssignedToDriver(loadNo).Select().Cast<DAL.OrderDetailsRow>().ToList());
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        // 02-03-2015 Madhu, Added for CR#66164 ToIncrease Order Items -> Notes size to 500 from 255 column in Service to allow the processing larges notes.
        /// <summary>
        /// GetItemsAssignedToDriver
        /// Function of Get Items Assigned To Driver
        /// </summary>
        /// <param name="sysTrxNo">Decimal Value</param>
        /// <param name="session">Session object</param>
        /// <returns>List of Item Assigned</returns>
        public static List<DAL.ItemDetailsRow> GetItemsAssignedToDriver(Decimal sysTrxNo, ISession session, String VersionNo = "")
        {
            ItemDetailsTableAdapter ta = new ItemDetailsTableAdapter();
            ta.CurrentConnection = session;
            return (ta.GetItemsByOrder(sysTrxNo).Select().Cast<DAL.ItemDetailsRow>().ToList());
        }

        /// <summary>
        /// GetOrderItemsComponentAssignedToDriver
        /// Function of Get Order Items Component Assigned To Driver
        /// </summary>
        /// <param name="sysTrxNo">System transaction number</param>
        /// <param name="sysTrxLine">System transaction line</param>
        /// <param name="session">Session object</param>
        /// <returns>List of Order Items Component Assigned</returns>
        public static List<DAL.OrderItemComponentRow> GetOrderItemsComponentAssignedToDriver(Decimal sysTrxNo, Int32 sysTrxLine, ISession session, String VersionNo = "")
        {
            OrderItemComponentTableAdapter ta = new OrderItemComponentTableAdapter();
            ta.CurrentConnection = session;
            return (ta.GetOrderItemComponent(sysTrxNo, sysTrxLine).Select().Cast<DAL.OrderItemComponentRow>().ToList());
        }

        // 08-22-2014  MadhuVenkat k - Added for CR 64744 - Destnotes is not updating, if there are more than one notes for the site.
        /// <summary>
        /// GetDestinationNotes
        /// Function of Get Destination Notes
        /// </summary>
        /// <param name="destID">Destination ID</param>
        /// <param name="session">Session object</param>
        /// <returns>List of Destination Notes</returns>
        public static List<DAL.DestNoteRow> GetDestinationNotes(Int32 destID, ISession session, String VersionNo = "")
        {
            try
            {
                DestNoteTableAdapter ta = new DestNoteTableAdapter();
                ta.CurrentConnection = session;
                return (ta.GetDataBy(destID).Select().Cast<DAL.DestNoteRow>().ToList());
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// GetOrderNotes
        /// Function to get Order notes
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public static List<DAL.OrderNoteRow> GetOrderNotes(decimal orderID, ISession session, String VersionNo = "")
        {
            OrderNoteTableAdapter ta = new OrderNoteTableAdapter();
            // 1/31/2013, Suresh Madhesan, 
            // Assigned the DB connection session and changed the base class 
            ta.CurrentConnection = session;
            return (ta.GetOrderNotes(orderID).Select().Cast<DAL.OrderNoteRow>().ToList());
        }

        /// <summary>
        /// Get  UpdatedSignatures
        /// </summary>
        /// <param name="session">session</param>
        /// <returns>List of SignatureImageRow</returns>
        public static List<DAL.SignatureImageRow> GetUpdatedSignatures(ISession session, String VersionNo = "")
        {
            SignatureImageTableAdapter ta = new SignatureImageTableAdapter();
            ta.CurrentConnection = session;
            return (ta.GetData().Select().Cast<DAL.SignatureImageRow>().ToList());
        }
        // 06-26-2014  MadhuVenkat k - Added for CR 64058  - For Get OrderDispatchHistoryRow 
        /// <summary>
        /// Get  OrderDispatchHistoryRow
        /// </summary>
        /// <param name="session">session</param>
        /// <returns>List of Cloud_GetOrderDispatchHistoryRow</returns>
        public static List<DAL.Cloud_GetOrderDispatchHistoryRow> GetOrderDispatchHistoryRow(ISession session, String VersionNo = "")
        {
            Cloud_GetOrderDispatchHistoryTableAdapter ta = new Cloud_GetOrderDispatchHistoryTableAdapter();
            ta.CurrentConnection = session;
            return (ta.GetOrderDispatchHistory().Select().Cast<DAL.Cloud_GetOrderDispatchHistoryRow>().ToList());
        }


        public static List<DAL.Cloud_GetUndispatchLoadRow> GetUndispatchLoad(ISession session, String VersionNo = "")
        {
            Cloud_GetUndispatchLoadTableAdapter ta = new Cloud_GetUndispatchLoadTableAdapter();
            ta.CurrentConnection = session;
            return (ta.GetUndispatchLoad().Select().Cast<DAL.Cloud_GetUndispatchLoadRow>().ToList());
        }

        #endregion

        #region Update
        /// <summary>
        /// Update Signature Image
        /// </summary>
        /// <param name="SignatureImage">SignatureImage</param>
        /// <param name="SignatureDateTime">Signature DateTime</param>
        /// <param name="SysTrxNo">SysTrxNo</param>
        /// <param name="session">session</param>
        public static void UpdateSignatureImage(byte[] SignatureImage, DateTime? SignatureDateTime, decimal SysTrxNo, ISession session, String VersionNo = "")
        {
            SignatureImageTableAdapter ta = new SignatureImageTableAdapter();
            ta.CurrentConnection = session;
            ta.UpdateSignatureImage("2", SignatureImage, SignatureDateTime, SysTrxNo);
        }

        ///// <summary>
        ///// Update DeliveryStreamOrderFrt Signature Image
        ///// </summary>
        ///// <param name="status">status</param>
        ///// <param name="SysTrxNo">SysTrxNo</param>
        ///// <param name="session"></param>
        ///// <returns></returns>
        //public static int UpdateDeliveryStreamOrderFrtSignatureImage(string status, decimal SysTrxNo, ISession session)
        //{
        //    DeliveryStreamOrderFrtTableAdapter ta = new DeliveryStreamOrderFrtTableAdapter();
        //    ta.CurrentConnection = session;
        //    return ta.UpdateQuery(status, SysTrxNo);
        //}

        /// <summary>
        /// UpdateLoadStatus
        /// Function to update the load status
        /// </summary>
        /// <param name="session">Session object</param>
        /// <param name="loadNo">Load number</param>
        /// <param name="statusCode">Status code</param>
        public static void UpdateLoadStatus(ISession session, string loadNo, String statusCode, String CustomerID, String VersionNo = "")
        {
            int ClientID = Convert.ToInt32(CustomerID);
            SqlCommand cmd = (SqlCommand)session.CreateCommand();
            cmd.CommandText = "Cloud_UpdateLoadStatus";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@LoadNo", loadNo);
            cmd.Parameters.AddWithValue("@StatusCode", statusCode);
            cmd.Parameters.AddWithValue("@ClientId", ClientID);
            cmd.ExecuteNonQuery();

        }

        public static int UpdateFSDriverLogSched(ISession session, String VersionNo = "")
        {
            int DriverLogSched = 0;
            SqlCommand cmd = (SqlCommand)session.CreateCommand();
            cmd.CommandText = "SELECT DriverLogSched FROM FSRules";
            cmd.CommandType = CommandType.Text;
            DriverLogSched = Convert.ToInt32(cmd.ExecuteScalar().ToString());

            return DriverLogSched;
        }

        // 06-26-2014  MadhuVenkat k - Added for CR 64058  - For update Sync_deliveryStream 
        /// <summary>
        /// Sync_deliveryStreamStatus
        /// Function to update the Sync_deliveryStream status
        /// </summary>
        /// <param name="session">Session object</param>
        /// <param name="SysTrxno">SysTrxno number</param> 
        public static void Sync_deliveryStream(ISession session, Decimal SysTrxno, String VersionNo = "")
        {
            SqlCommand cmd = (SqlCommand)session.CreateCommand();
            cmd.CommandText = "Cloud_updateorderdispatchhistory";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Systrxno", SysTrxno);
            cmd.ExecuteNonQuery();

        }


        /// <summary>
        /// UpdateVehicleFlag
        /// </summary>
        /// <param name="session">session object</param>
        /// <param name="vehicleID">vehicle id</param>
        public static void UpdateVehicleFlag(ISession session, Int32 vehicleID, String VersionNo = "")
        {
            SqlCommand cmd = (SqlCommand)session.CreateCommand();
            cmd.CommandText = "Cloud_UpdateVehicleFlag";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@VehicleID", vehicleID);
            cmd.ExecuteNonQuery();

        }
        // 2014.01.10  Ramesh M Added For CR#61759 Added to full From Site business rule
        /// <summary>
        /// UpdatedFromSiteBSRuleFlag
        /// </summary>
        /// <param name="session"></param>
        /// <param name="ClientID"></param>
        /// <param name="VersionNo"></param>
        public static void UpdateFromSiteBSRuleFlag(ISession session, String ClientID, String VersionNo = "")
        {
            //Have to Get the updated Procedure
            SqlCommand cmd = (SqlCommand)session.CreateCommand();
            cmd.CommandText = "Cloud_UpdateSysParamFlag";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ClientID", Convert.ToInt32(ClientID));
            cmd.ExecuteNonQuery();

        }

        public static void UpdateFromSiteFlag(ISession session, String ClientID, Int32 SupplierSupplyPtID, String VersionNo = "")
        {
            //Have to change the Procedure
            SqlCommand cmd = (SqlCommand)session.CreateCommand();
            cmd.CommandText = "Cloud_UpdateOEDefSuppSupplyPtFlag";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@OEDefSupplierSupplyPtID", SupplierSupplyPtID);
            cmd.ExecuteNonQuery();

        }

        /// <summary>
        /// UpdateDriverFlag
        /// Function to update the driver flag
        /// </summary>
        /// <param name="session">Session object</param>
        /// <param name="driverID">Driver ID</param>
        public static void UpdateDriverFlag(ISession session, Int32 driverID, String VersionNo = "")
        {
            SqlCommand cmd = (SqlCommand)session.CreateCommand();
            cmd.CommandText = "Cloud_UpdateDriverFlag";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@DriverID", driverID);
            cmd.ExecuteNonQuery();

        }
        //08-29-2013 Fsww Ramesh M, Added for CR#?.. To Get Updated wareHouseUsers from Insite Table
        /// <summary>
        /// UpdatedInSiteUserFlag
        /// Function to update InSite Flag
        /// </summary>
        /// <param name="session"></param>
        /// <param name="SiteID"></param>
        public static void UpdatedInSiteUserFlag(ISession session, Int32 SiteID, String VersionNo = "")
        {
            SqlCommand cmd = (SqlCommand)session.CreateCommand();
            cmd.CommandText = "Cloud_UpdatedInSiteUserFlag";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@SiteID", SiteID);
            cmd.ExecuteNonQuery();

        }
        //2013.10.10 FSWW, Ramesh M Added For CR#60620
        public static DataTable OrderStatusMerg(ISession session, String OrderNos, String VersionNo = "")
        {
            DataTable Dt = null;
            Cloud_GetLoadsToSyncStatusTableAdapter ta = new Cloud_GetLoadsToSyncStatusTableAdapter();
            ta.CurrentConnection = session;
            Dt = ta.GetData(OrderNos);
            return Dt;
        }

        public static char UpdateFreightBreakdown(ISession session, String VersionNo = "")
        {
            char FrtBrkdown = 'N';
            SqlCommand cmd = (SqlCommand)session.CreateCommand();
            cmd.CommandText = "Cloud_UpdateFrtBrkDownFlag";
            cmd.CommandType = CommandType.StoredProcedure;
            SqlConnection con = new SqlConnection(session.ConnectionString);
            con.Open();
            SqlDataAdapter sa = new SqlDataAdapter();
            sa.SelectCommand = cmd;
            DataTable dt = new DataTable();
            sa.Fill(dt);
            con.Close();
            if (dt.Rows.Count > 0)
            {
                FrtBrkdown = Convert.ToChar(dt.Rows[0][0].ToString());
            }
            return FrtBrkdown;
        }

        public static DataTable UpdateBOLImageVolumeStartEndBOLRule(ISession session, String VersionNo = "")
        {
            SqlCommand cmd = (SqlCommand)session.CreateCommand();
            cmd.CommandText = "SELECT DeliveryStream_RequireBOLImage as RequireBOLImage, DeliveryStream_RequireDeliveryImage as RequireDeliveryImage, DeliveryStream_RequireBeforeAndEndVolume as DeliveryVolumeBSRule, DeliveryStream_StartAndEndBOLDates as BOLStartEndDateBSRule FROM FSRules WHERE EnableDeliveryStream = 'Y'";
            cmd.CommandType = CommandType.Text;
            SqlConnection con = new SqlConnection(session.ConnectionString);
            con.Open();
            SqlDataAdapter sa = new SqlDataAdapter();
            sa.SelectCommand = cmd;
            DataTable dt = new DataTable();
            sa.Fill(dt);
            con.Close();

            return dt;
        }

        public static char GetDeliveryDateSortFlag(ISession session, String VersionNo = "")
        {
            char DeliveryDateSort = 'N';
            SqlCommand cmd = (SqlCommand)session.CreateCommand();
            cmd.CommandText = "Cloud_GetDeliveryDateSortFlag";
            cmd.CommandType = CommandType.StoredProcedure;
            SqlConnection con = new SqlConnection(session.ConnectionString);
            con.Open();
            SqlDataAdapter sa = new SqlDataAdapter();
            sa.SelectCommand = cmd;
            DataTable dt = new DataTable();
            sa.Fill(dt);
            con.Close();
            if (dt.Rows.Count > 0)
            {
                DeliveryDateSort = Convert.ToChar(dt.Rows[0][0].ToString());
            }
            return DeliveryDateSort;
        }

        public static string GetRemoveOldLoadByDriverFlag(ISession session, String VersionNo = "")
        {
            string RemoveLoadFlag = string.Empty;
            SqlCommand cmd = (SqlCommand)session.CreateCommand();
            cmd.CommandText = "GetDeliveryStreamRemoveLoadFlag";
            cmd.CommandType = CommandType.StoredProcedure;
            SqlConnection con = new SqlConnection(session.ConnectionString);
            con.Open();
            SqlDataAdapter sa = new SqlDataAdapter();
            sa.SelectCommand = cmd;

            RemoveLoadFlag = cmd.ExecuteScalar().ToString();
            if (string.IsNullOrEmpty(RemoveLoadFlag))
            {
                RemoveLoadFlag = "N";
            }
            return RemoveLoadFlag;

        }

        #endregion

        #region TankWagon


        //08-29-2013 Fsww Ramesh M, Added for CR#?.. To Get Updated wareHouseUsers from Insite Table
        /// <summary>
        /// UpdatedInSiteUserFlag
        /// Function to update InSite Flag
        /// </summary>
        /// <param name="session"></param>
        /// <param name="SiteID"></param>
        public static void UpdatedVehicleCompartmentsFlag(ISession session, Int32 SiteID, String VersionNo = "")
        {
            SqlCommand cmd = (SqlCommand)session.CreateCommand();
            cmd.CommandText = "VehicleCompartmentsFlag";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CompartmentID", SiteID);
            cmd.ExecuteNonQuery();

        }

        //08-29-2013 Fsww Ramesh M, Added for CR#?.. To Get Updated wareHouseUsers from Insite Table
        /// <summary>
        /// UpdatedInSiteUserFlag
        /// Function to update InSite Flag
        /// </summary>
        /// <param name="session"></param>
        /// <param name="SiteID"></param>
        public static void UpdatedSuppliersFlag(ISession session, Int32 SupplierID, String VersionNo = "")
        {
            SqlCommand cmd = (SqlCommand)session.CreateCommand();
            cmd.CommandText = "CLOUD_UPDATESuppliersFLAG";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@SupplierID", SupplierID);
            cmd.ExecuteNonQuery();

        }


        //08-29-2013 Fsww Ramesh M, Added for CR#?.. To Get Updated wareHouseUsers from Insite Table
        /// <summary>
        /// UpdatedInSiteUserFlag
        /// Function to update InSite Flag
        /// </summary>
        /// <param name="session"></param>
        /// <param name="SiteID"></param>
        public static void UpdatedSupplierSupplyPtFlag(ISession session, Int32 SupplierSupplyPtID, String VersionNo = "")
        {
            SqlCommand cmd = (SqlCommand)session.CreateCommand();
            cmd.CommandText = "CLOUD_UPDATESupplierSupplyPtFLAG";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@SupplierSupplyPtID", SupplierSupplyPtID);
            cmd.ExecuteNonQuery();

        }


        //08-29-2013 Fsww Ramesh M, Added for CR#?.. To Get Updated wareHouseUsers from Insite Table
        /// <summary>
        /// UpdatedInSiteUserFlag
        /// Function to update InSite Flag
        /// </summary>
        /// <param name="session"></param>
        /// <param name="SiteID"></param>
        public static void UpdatedProductsFlag(ISession session, Int32 PurchRackID, String VersionNo = "")
        {
            SqlCommand cmd = (SqlCommand)session.CreateCommand();
            cmd.CommandText = "CLOUD_UPDATEProductsFLAG";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PurchRackID", PurchRackID);
            cmd.ExecuteNonQuery();

        }

        //08-29-2013 Fsww Ramesh M, Added for CR#?.. To Get Updated wareHouseUsers from Insite Table
        /// <summary>
        /// UpdatedInSiteUserFlag
        /// Function to update InSite Flag
        /// </summary>
        /// <param name="session"></param>
        /// <param name="SiteID"></param>
        public static void UpdatedINSiteFlag(ISession session, Int32 SiteID, String VersionNo = "")
        {
            SqlCommand cmd = (SqlCommand)session.CreateCommand();
            cmd.CommandText = "CLOUD_UPDATEINSiteFLAG";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@SiteID", SiteID);
            cmd.ExecuteNonQuery();

        }


        /// <summary>
        /// GetDestinationNotes
        /// Function of Get Destination Notes
        /// </summary>
        /// <param name="destID">Destination ID</param>
        /// <param name="session">Session object</param>
        /// <returns>List of Destination Notes</returns>
        public static List<DAL.VehicleCompartmentsRow> GetVehicleCompartment(ISession session, String VersionNo = "")
        {
            try
            {
                VehicleCompartmentsTableAdapter ta = new VehicleCompartmentsTableAdapter();
                ta.CurrentConnection = session;
                return (ta.GetData().Select().Cast<DAL.VehicleCompartmentsRow>().ToList());
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        /// <summary>
        /// GetDestinationNotes
        /// Function of Get Destination Notes
        /// </summary>
        /// <param name="destID">Destination ID</param>
        /// <param name="session">Session object</param>
        /// <returns>List of Destination Notes</returns>
        public static List<DAL.SupplierSupplyPtRow> GetSupplierSupplyPt(ISession session, String VersionNo = "")
        {
            try
            {
                SupplierSupplyPtTableAdapter ta = new SupplierSupplyPtTableAdapter();
                ta.CurrentConnection = session;
                return (ta.GetData().Select().Cast<DAL.SupplierSupplyPtRow>().ToList());
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        /// <summary>
        /// GetDestinationNotes
        /// Function of Get Destination Notes
        /// </summary>
        /// <param name="destID">Destination ID</param>
        /// <param name="session">Session object</param>
        /// <returns>List of Destination Notes</returns>
        public static List<DAL.SupplierRow> GetSuppliers(ISession session, String VersionNo = "")
        {
            try
            {
                SupplierTableAdapter ta = new SupplierTableAdapter();
                ta.CurrentConnection = session;
                return (ta.GetData().Select().Cast<DAL.SupplierRow>().ToList());
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// GetDestinationNotes
        /// Function of Get Destination Notes
        /// </summary>
        /// <param name="destID">Destination ID</param>
        /// <param name="session">Session object</param>
        /// <returns>List of Destination Notes</returns>
        public static List<DAL.ProductRow> GetProducts(ISession session, String VersionNo = "")
        {
            try
            {
                ProductTableAdapter ta = new ProductTableAdapter();
                ta.CurrentConnection = session;
                return (ta.GetData().Select().Cast<DAL.ProductRow>().ToList());
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// GetDestinationNotes
        /// Function of Get Destination Notes
        /// </summary>
        /// <param name="destID">Destination ID</param>
        /// <param name="session">Session object</param>
        /// <returns>List of Destination Notes</returns>
        public static List<DAL.INSiteRow> GetINSite(ISession session, String VersionNo = "")
        {
            try
            {
                INSiteTableAdapter ta = new INSiteTableAdapter();
                ta.CurrentConnection = session;
                return (ta.GetDataINSite().Select().Cast<DAL.INSiteRow>().ToList());
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// GetDestinationNotes
        /// Function of Get Destination Notes
        /// </summary>
        /// <param name="destID">Destination ID</param>
        /// <param name="session">Session object</param>
        /// <returns>List of Destination Notes</returns>
        public static List<DAL.Cloud_TW_VehicleTypeRow> GetTWVehicleType(Int32 ClientID, ISession session, String VersionNo = "")
        {
            try
            {
                Cloud_TW_VehicleTypeTableAdapter ta = new Cloud_TW_VehicleTypeTableAdapter();
                ta.CurrentConnection = session;
                return (ta.GetDataTWVehicleType(ClientID).Cast<DAL.Cloud_TW_VehicleTypeRow>().ToList());
            }
            catch (Exception ex)
            {
                throw;
            }
        }



        #endregion
    }
}
