//2013.12.17 FSWW, Ramesh M Added For CR#61411  For Order Note Null to be pushed to cloud DB
//2014.01.23  Ramesh M Added For CR#61759 Added ShipToID
// 2014.01.30  Ramesh M Added For CR#62038 Added AllowDriversToChangeMultiBOL
// 2014.03.17  Ramesh M Added For CR#62668 to get home terminal details
// 05-20-2014  MadhuVenkat k - Added for CR 63346 - PO & Priority No to Load Information Screen 
// 09-18-2014  MadhuVenkat k - Added for CR 65002 - Application not allowing to change the From SiteID on BOL processing.
using System;

namespace DeliveryStreamCustomerWinServ.DataAccess
{

    /// <summary>
    /// DAL
    /// Partial class for data access layer
    /// </summary>
    public partial class DAL
    {
        partial class DestNoteDataTable
        {
        }

        partial class UpdatedInSiteUserDataTable
        {
        }

        partial class OrderDetailsDataTable
        {
        }

        partial class LoadDetailsDataTable
        {
        }

        partial class UpdatedDriversDataTable
        {
        }

        /// <summary>
        /// DestNoteRow
        /// Partial DestNote row class to get and set datamembers
        /// </summary>
        partial class DestNoteRow
        {
            /// <summary>
            /// DestID_Ext
            /// Properties for DestID datamember
            /// </summary>
            public Int32 DestID_Ext
            {
                get
                {
                    return this.DestID;
                }
                set
                {
                    this.DestID = value;
                }
            }

            /// <summary>
            /// DestNote_Ext
            /// Properties for DestNote datamember
            /// </summary>
            public String DestNote_Ext
            {
                get
                {
                    return this.Note;
                }
                set
                {
                    this.Note = value;
                }
            }
        }

        /// <summary>
        /// VehicleCompartmentsRow
        /// Partial VehicleCompartments Row class to get and set datamembers
        /// </summary>
        partial class VehicleCompartmentsRow
        {
            /// <summary>
            /// CompartmentID_Ext
            /// Properties for CompartmentID datamember
            /// </summary>
            public Int32 CompartmentID_Ext
            {
                get
                {
                    return this.CompartmentID;
                }
                set
                {
                    this.CompartmentID = value;
                }
            }

            /// <summary>
            /// VehicleID_Ext
            /// Properties for VehicleID datamember
            /// </summary>
            public Int32 VehicleID_Ext
            {
                get
                {
                    return this.VehicleID;
                }
                set
                {
                    this.VehicleID = value;
                }
            }

            /// <summary>
            /// Capacity_Ext
            /// Properties for Capacity datamember
            /// </summary>
            public Int32 Capacity_Ext
            {
                get
                {
                    return this.Capacity;
                }
                set
                {
                    this.Capacity = value;
                }
            }


            /// <summary>
            /// ClientID_Ext
            /// Properties for ClientID datamember
            /// </summary>
            public Int32 ClientID_Ext
            {
                get
                {
                    return this.ClientID;
                }
                set
                {
                    this.ClientID = value;
                }
            }


            /// <summary>
            /// Code_Ext
            /// Properties for Code datamember
            /// </summary>
            public String Code_Ext
            {
                get
                {
                    return this.Code;
                }
                set
                {
                    this.Code = value;
                }
            }
        }


        partial class SupplierRow
        {
            /// <summary>
            /// CompartmentID_Ext
            /// Properties for CompartmentID datamember
            /// </summary>
            public Int32 SupplierID_Ext
            {
                get
                {
                    return this.SupplierID;
                }
                set
                {
                    this.SupplierID = value;
                }
            }

            /// <summary>
            /// SystrxNo_Ext
            /// Properties for SystrxNo datamember
            /// </summary>
            public String Code_Ext
            {
                get
                {
                    return this.Code;
                }
                set
                {
                    this.Code = value;
                }
            }

            /// <summary>
            /// Capacity_Ext
            /// Properties for Capacity datamember
            /// </summary>
            public String Descr_Ext
            {
                get
                {
                    return this.Descr;
                }
                set
                {
                    this.Descr = value;
                }
            }


            /// <summary>
            /// ClientID_Ext
            /// Properties for ClientID datamember
            /// </summary>
            public DateTime? LastModifiedDtTm_Ext
            {
                get
                {
                    if (this.IsLastModifiedDtTmNull())
                    {
                        return null;
                    }
                    return this.LastModifiedDtTm;
                }
                set
                {
                    if (value == null)
                    {
                        this.IsLastModifiedDtTmNull();
                    }
                    else
                    {
                        this.LastModifiedDtTm = Convert.ToDateTime(value);
                    }
                }
            }

            /// <summary>
            /// ClientID_Ext
            /// Properties for ClientID datamember
            /// </summary>
            public int CompanyID_Ext
            {
                get
                {
                    return this.ClientID;
                }
                set
                {
                    this.ClientID = value;
                }
            }



        }

        partial class SupplierSupplyPtRow
        {
            /// <summary>
            /// CompartmentID_Ext
            /// Properties for CompartmentID datamember
            /// </summary>
            public Int32 SupplierSupplyPtID_Ext
            {
                get
                {
                    return this.SupplierSupplyPtID;
                }
                set
                {
                    this.SupplierSupplyPtID = value;
                }
            }

            /// <summary>
            /// SystrxNo_Ext
            /// Properties for SystrxNo datamember
            /// </summary>
            public Int32 SupplierID_Ext
            {
                get
                {
                    return this.SupplierID;
                }
                set
                {
                    this.SupplierID = value;
                }
            }

            /// <summary>
            /// Capacity_Ext
            /// Properties for Capacity datamember
            /// </summary>
            public String SupplierSupplyPtCode_Ext
            {
                get
                {
                    return this.SupplierSupplyPtCode;
                }
                set
                {
                    this.SupplierSupplyPtCode = value;
                }
            }

            /// <summary>
            /// Capacity_Ext
            /// Properties for Capacity datamember
            /// </summary>
            public String SupplierSupplyPtDescr_Ext
            {
                get
                {
                    //return this.SupplierSupplyPtDescr;
                    return this.IsSupplierSupplyPtDescrNull() ? null : this.SupplierSupplyPtDescr;
                }
                set
                {
                    this.SupplierSupplyPtDescr = value;
                }
            }


            /// <summary>
            /// ClientID_Ext
            /// Properties for ClientID datamember
            /// </summary>
            public DateTime? LastModifiedDtTm_Ext
            {
                //get
                //{
                //    return this.LastModifiedDtTm;
                //}
                //set
                //{
                //    this.LastModifiedDtTm = value;
                //}

                get
                {
                    if (this.IsLastModifiedDtTmNull())
                    {
                        return null;
                    }
                    return this.LastModifiedDtTm;
                }
                set
                {
                    if (value == null)
                    {
                        this.IsLastModifiedDtTmNull();
                    }
                    else
                    {
                        this.LastModifiedDtTm = Convert.ToDateTime(value);
                    }
                }

            }

            /// <summary>
            /// ClientID_Ext
            /// Properties for ClientID datamember
            /// </summary>
            public int CompanyID_Ext
            {
                get
                {
                    return this.ClientID;
                }
                set
                {
                    this.ClientID = value;
                }
            }



        }
        partial class Cloud_GetOrderDispatchHistoryRow
        {
            /// <summary>
            /// SysTrxNo_Ext
            /// Properties for SysTrxNo datamember
            /// </summary>
            public Decimal SysTrxNo_Ext
            {
                get
                {
                    return this.SysTrxNo;
                }
                set
                {
                    this.SysTrxNo = value;
                }
            }
            /// <summary>
            /// DefDriverID_Ext
            /// Properties for NoteNo datamember
            /// </summary>
            public Int32 DefDriverID_Ext
            {
                get
                {
                    return this.DefDriverID;
                }
                set
                {
                    this.DefDriverID = value;
                }
            }
            /// <summary>
            /// DefVehicleID_Ext
            /// Properties for NoteTypeID datamember
            /// </summary>
            public Int32 DefVehicleID_Ext
            {
                get
                {
                    return this.DefVehicleID;

                }
                set
                {
                    this.DefVehicleID = value;
                }
            }

            /// <summary>
            /// OldDriverID_Ext
            /// Properties for Note datamember
            /// </summary>
            public Int32 OldDriverID_Ext
            {
                get
                {
                    return this.OldDriverID;
                }
                set
                {
                    this.OldDriverID = value;
                }
            }
            /// <summary>
            /// OldVehicleID_Ext
            /// Properties for PrintOn datamember
            /// </summary>
            public Int32 OldVehicleID_Ext
            {
                get
                {
                    return this.OldVehicleID;
                }
                set
                {
                    this.OldVehicleID = value;
                }
            }

        }

        partial class ProductRow
        {
            /// <summary>
            /// CompartmentID_Ext
            /// Properties for CompartmentID datamember
            /// </summary>
            public Int32 SupplierID_Ext
            {
                get
                {
                    return this.SupplierID;
                }
                set
                {
                    this.SupplierID = value;
                }
            }

            /// <summary>
            /// CompartmentID_Ext
            /// Properties for CompartmentID datamember
            /// </summary>
            public Int32 SupplierSupplyPtID_Ext
            {
                get
                {
                    return this.SupplierSupplyPtID;
                }
                set
                {
                    this.SupplierSupplyPtID = value;
                }
            }



            /// <summary>
            /// CompartmentID_Ext
            /// Properties for CompartmentID datamember
            /// </summary>
            public Int32 SupplyPtID_Ext
            {
                get
                {
                    return this.SupplyPtID;
                }
                set
                {
                    this.SupplyPtID = value;
                }
            }



            /// <summary>
            /// CompartmentID_Ext
            /// Properties for CompartmentID datamember
            /// </summary>
            public Int32 PurchRackid_Ext
            {
                get
                {
                    return this.PurchRackid;
                }
                set
                {
                    this.PurchRackid = value;
                }
            }
            /// <summary>
            /// SystrxNo_Ext
            /// Properties for SystrxNo datamember
            /// </summary>
            public String ProdcutCode_Ext
            {
                get
                {
                    return this.ProdcutCode;
                }
                set
                {
                    this.ProdcutCode = value;
                }
            }

            /// <summary>
            /// Capacity_Ext
            /// Properties for Capacity datamember
            /// </summary>
            public String ProdcutDescr_Ext
            {
                get
                {
                    return this.ProdcutDescr;
                }
                set
                {
                    this.ProdcutDescr = value;
                }
            }


            /// <summary>
            /// ClientID_Ext
            /// Properties for ClientID datamember
            /// </summary>
            public DateTime? LastModifiedDtTm_Ext
            {
                get
                {
                    if (this.IsLastModifiedDtTmNull())
                    {
                        return null;
                    }
                    return this.LastModifiedDtTm;
                }
                set
                {
                    if (value == null)
                    {
                        this.IsLastModifiedDtTmNull();
                    }
                    else
                    {
                        this.LastModifiedDtTm = Convert.ToDateTime(value);
                    }
                }
            }

            /// <summary>
            /// ClientID_Ext
            /// Properties for ClientID datamember
            /// </summary>
            public int CompanyID_Ext
            {
                get
                {
                    return this.ClientID;
                }
                set
                {
                    this.ClientID = value;
                }
            }



        }

        partial class INSiteRow
        {
            /// <summary>
            /// CompartmentID_Ext
            /// Properties for CompartmentID datamember
            /// </summary>
            public Int32 SiteID_Ext
            {
                get
                {
                    return this.SiteID;
                }
                set
                {
                    this.SiteID = value;
                }
            }


            /// <summary>
            /// Capacity_Ext
            /// Properties for Capacity datamember
            /// </summary>
            public String Code_Ext
            {
                get
                {
                    return this.Code;
                }
                set
                {
                    this.Code = value;
                }
            }


            /// <summary>
            /// ClientID_Ext
            /// Properties for ClientID datamember
            /// </summary>
            public String LongDescr_Ext
            {
                get
                {
                    return this.LongDescr;
                }
                set
                {
                    this.LongDescr = value;
                }
            }

            /// <summary>
            /// ClientID_Ext
            /// Properties for ClientID datamember
            /// </summary>
            public DateTime? LastModifiedDtTm_Ext
            {
                get
                {
                    if (this.IsLastModifiedDtTmNull())
                    {
                        return null;
                    }
                    return this.LastModifiedDtTm;
                }
                set
                {
                    if (value == null)
                    {
                        this.IsLastModifiedDtTmNull();
                    }
                    else
                    {
                        this.LastModifiedDtTm = Convert.ToDateTime(value);
                    }
                }
            }


            /// <summary>
            /// CompartmentID_Ext
            /// Properties for CompartmentID datamember
            /// </summary>
            public Int32 CustomerID_Ext
            {
                get
                {
                    return this.ClientID;
                }
                set
                {
                    this.ClientID = value;
                }
            }
        }

        partial class Cloud_TW_VehicleTypeRow
        {
            /// <summary>
            /// VehicleTypeID_Ext
            /// Properties for VehicleTypeID datamember
            /// </summary>
            public Int32 VehicleTypeID_Ext
            {
                get
                {
                    return this.VehicleTypeID;
                }
                set
                {
                    this.VehicleTypeID = value;
                }
            }


            /// <summary>
            /// Descr_Ext
            /// Properties for Descr datamember
            /// </summary>
            public String Descr_Ext
            {
                get
                {
                    return this.Descr;
                }
                set
                {
                    this.Descr = value;
                }
            }


            /// <summary>
            /// ClientID_Ext
            /// Properties for ClientID datamember
            /// </summary>
            public Int32 ClientID_Ext
            {
                get
                {
                    return this.ClientID;
                }
                set
                {
                    this.ClientID = value;
                }
            }
        }
        //2013.12.17 FSWW, Ramesh M Added For CR#61411 For Order Note Null to be pushed to cloud DB
        partial class OrderNoteRow
        {
            /// <summary>
            /// SysTrxNo_Ext
            /// Properties for SysTrxNo datamember
            /// </summary>
            public Decimal SysTrxNo_Ext
            {
                get
                {
                    return this.SysTrxNo;
                }
                set
                {
                    this.SysTrxNo = value;
                }
            }
            /// <summary>
            /// NoteNo_Ext
            /// Properties for NoteNo datamember
            /// </summary>
            public Int32 NoteNo_Ext
            {
                get
                {
                    return this.NoteNo;
                }
                set
                {
                    this.NoteNo = value;
                }
            }
            /// <summary>
            /// NoteTypeID_Ext
            /// Properties for NoteTypeID datamember
            /// </summary>
            public Int32 NoteTypeID_Ext
            {
                get
                {
                    return this.NoteTypeID;

                }
                set
                {
                    this.NoteTypeID = value;
                }
            }

            /// <summary>
            /// Note_Ext
            /// Properties for Note datamember
            /// </summary>
            public String Note_Ext
            {
                get
                {
                    return this.IsNoteNull() ? null : this.Note;
                }
                set
                {
                    this.Note = value;
                }
            }
            /// <summary>
            /// PrintOn_Ext
            /// Properties for PrintOn datamember
            /// </summary>
            public String PrintOn_Ext
            {
                get
                {
                    return this.IsPrintOnNull() ? null : this.PrintOn;
                }
                set
                {
                    this.PrintOn = value;
                }
            }
        }


        /// <summary>
        /// ItemDetailsRow
        /// Partial ItemDetails row class to get and set datamembers
        /// </summary>
        partial class ItemDetailsRow
        {
            /// <summary>
            /// SysTrxLine_Ext
            /// Properties for SysTrxLine datamember
            /// </summary>
            public int SysTrxLine_Ext
            {
                get
                {
                    return this.SysTrxLine;
                }
                set
                {
                    this.SysTrxLine = value;
                }
            }

            /// <summary>
            /// OrderedQty_Ext
            /// Properties for OrderedQty datamember
            /// </summary>
            public Decimal OrderedQty_Ext
            {
                get
                {
                    return this.OrderedQty;
                }
                set
                {
                    this.OrderedQty = value;
                }
            }

            /// <summary>
            /// ProdCode_Ext
            /// Properties for ProdCode datamember
            /// </summary>
            public String ProdCode_Ext
            {
                get
                {
                    return this.IsProdCodeNull() ? null : this.ProdCode;
                }
                set
                {
                    this.ProdCode = value;
                }
            }

            /// <summary>
            /// ProdName_Ext
            /// Properties for ProdName datamember
            /// </summary>
            public String ProdName_Ext
            {
                get
                {
                    return this.IsProdNameNull() ? null : this.ProdName;
                }
                set
                {
                    this.ProdName = value;
                }
            }

            /// <summary>
            /// ProdUOM_Ext
            /// Properties for ProdUOM datamember
            /// </summary>
            public String ProdUOM_Ext
            {
                get
                {
                    return this.IsProdUOMNull() ? null : this.ProdUOM;
                }
                set
                {
                    this.ProdUOM = value;
                }
            }

            /// <summary>
            /// Blend_Ext
            /// Properties for Blend datamember
            /// </summary>
            public String Blend_Ext
            {
                get
                {
                    return this.IsBlendNull() ? null : this.Blend;
                }
                set
                {
                    this.Blend = value;
                }
            }

            /// <summary>
            /// SupplierName_Ext
            /// Properties for SupplierName datamember
            /// </summary>
            public String SupplierName_Ext
            {
                get
                {
                    return this.IsSupplierNameNull() ? null : this.SupplierName;
                }
                set
                {
                    this.SupplierName = value;
                }
            }

            /// <summary>
            /// SupplierCode_Ext
            /// Properties for SupplierCode datamember
            /// </summary>
            public String SupplierCode_Ext
            {
                get
                {
                    return this.IsSupplierCodeNull() ? null : this.SupplierCode;
                }
                set
                {
                    this.SupplierCode = value;
                }
            }

            /// <summary>
            /// SupplyPointName_Ext
            /// Properties for SupplyPointName datamember
            /// </summary>
            public String SupplyPointName_Ext
            {
                get
                {
                    return this.IsSupplyPointNameNull() ? null : this.SupplyPointName;
                }
                set
                {
                    this.SupplyPointName = value;
                }
            }

            /// <summary>
            /// SupplyPointCode_Ext
            /// Properties for SupplyPointCode datamember
            /// </summary>
            public String SupplyPointCode_Ext
            {
                get
                {
                    return this.IsSupplyPointCodeNull() ? null : this.SupplyPointCode;
                }
                set
                {
                    this.SupplyPointCode = value;
                }
            }

            /// <summary>
            /// SupplyPointAddress1_Ext
            /// Properties for SupplyPointAddress1 datamember
            /// </summary>
            public String SupplyPointAddress1_Ext
            {
                get
                {
                    return this.IsSupplyPointAddress1Null() ? null : this.SupplyPointAddress1;
                }
                set
                {
                    this.SupplyPointAddress1 = value;
                }
            }

            /// <summary>
            /// SupplyPointAddress2_Ext
            /// Properties for SupplyPointAddress2 datamember
            /// </summary>
            public String SupplyPointAddress2_Ext
            {
                get
                {
                    return this.IsSupplyPointAddress2Null() ? null : this.SupplyPointAddress2;
                }
                set
                {
                    this.SupplyPointAddress2 = value;
                }
            }

            /// <summary>
            /// City_Ext
            /// Properties for City datamember
            /// </summary>
            public String City_Ext
            {
                get
                {
                    return this.IsCityNull() ? null : this.City;
                }
                set
                {
                    this.City = value;
                }
            }

            /// <summary>
            /// State_Ext
            /// Properties for State datamember
            /// </summary>
            public String State_Ext
            {
                get
                {
                    return this.IsStateNull() ? null : this.State;
                }
                set
                {
                    this.State = value;
                }
            }

            /// <summary>
            /// Zip_Ext
            /// Properties for Zip datamember
            /// </summary>
            public String Zip_Ext
            {
                get
                {
                    return this.IsZipNull() ? null : this.Zip;
                }
                set
                {
                    this.Zip = value;
                }
            }

            /// <summary>
            /// Country_Ext
            /// Properties for Country datamember
            /// </summary>
            public String Country_Ext
            {
                get
                {
                    return this.IsCountryNull() ? null : this.Country;
                }
                set
                {
                    this.Country = value;
                }
            }

            /// <summary>
            /// Country_Ext
            /// Properties for Country datamember
            /// </summary>
            public String Notes_Ext
            {
                get
                {
                    return this.IsNotesNull() ? null : this.Notes;
                }
                set
                {
                    this.Country = value;
                }
            }

            public int ARShipToTankId_Ext
            {
                get
                {
                    return this.IsARShipToTankIDNull() ? 0 : this.ARShipToTankID;
                }
                set
                {
                    this.ARShipToTankID = value;
                }
            }

            public String ARShipToTankCode_Ext
            {
                get
                {
                    return this.IsARShipToTankCodeNull() ? null : this.ARShipToTankCode;
                }
                set
                {
                    this.ARShipToTankCode = value;
                }
            }


        }

        /// <summary>
        /// OrderDetailsRow
        /// Partial OrderDetails row class to get and set datamembers
        /// </summary>
        partial class OrderDetailsRow
        {
            /// <summary>
            /// OrderNo_Ext
            /// Properties for OrderNo datamember
            /// </summary>
            public String OrderNo_Ext
            {
                get
                {
                    return this.OrderNo;
                }
                set
                {
                    this.OrderNo = value;
                }
            }

            /// <summary>
            /// SysTrxNo_Ext
            /// Properties for SysTrxNo datamember
            /// </summary>
            public Decimal SysTrxNo_Ext
            {
                get
                {
                    return this.SysTrxNo;
                }
                set
                {
                    this.SysTrxNo = value;
                }
            }

            /// <summary>
            /// PromisedDateTime_Ext
            /// Properties for PromisedDateTime datamember
            /// </summary>
            public DateTime PromisedDateTime_Ext
            {
                get
                {
                    return this.IsPromisedDtTmNull() ? DateTime.Now : this.PromisedDtTm;
                }
                set
                {
                    this.PromisedDtTm = value;
                }
            }

            /// <summary>
            /// DestAddress1_Ext
            /// Properties for DestAddress1 datamember
            /// </summary>
            public String DestAddress1_Ext
            {
                get
                {
                    return this.IsDestAddr1Null() ? null : this.DestAddr1;
                }
                set
                {
                    this.DestAddr1 = value;
                }
            }

            /// <summary>
            /// DestAddress2_Ext
            /// Properties for DestAddress2 datamember
            /// </summary>
            public String DestAddress2_Ext
            {
                get
                {
                    return this.IsDestAddr2Null() ? null : this.DestAddr2;
                }
                set
                {
                    this.DestAddr2 = value;
                }
            }

            /// <summary>
            /// City_Ext
            /// Properties for City datamember
            /// </summary>
            public String City_Ext
            {
                get
                {
                    return this.IsCityNull() ? null : this.City;
                }
                set
                {
                    this.City = value;
                }
            }

            /// <summary>
            /// State_Ext
            /// Properties for State datamember
            /// </summary>
            public String State_Ext
            {
                get
                {
                    return this.IsStatesNull() ? null : this.States;
                }
                set
                {
                    this.States = value;
                }
            }

            /// <summary>
            /// Zip_Ext
            /// Properties for Zip datamember
            /// </summary>
            public String Zip_Ext
            {
                get
                {
                    return this.IsZipNull() ? null : this.Zip;
                }
                set
                {
                    this.Zip = value;
                }
            }

            /// <summary>
            /// DestID_Ext
            /// Properties for DestID datamember
            /// </summary>
            public Int32 DestID_Ext
            {
                get
                {
                    return this.IsDestIDNull() ? 0 : this.DestID;
                }
                set
                {
                    this.DestID = value;
                }
            }

            /// <summary>
            /// DestSite_Ext
            /// Properties for DestSite datamember
            /// </summary>
            public String DestSite_Ext
            {
                get
                {
                    return this.IsDestSiteNull() ? null : this.DestSite;
                }
                set
                {
                    this.DestSite = value;
                }
            }

            /// <summary>
            /// Country_Ext
            /// Properties for Country datamember
            /// </summary>
            public String Country_Ext
            {
                get
                {
                    return this.IsCountryNull() ? null : this.Country;
                }
                set
                {
                    this.Country = value;
                }
            }
            //2013.12.02  Fsww Ramesh M, Added for  RequestedDtTm CR#61274 
            /// <summary>
            /// RequestedDtTm_Ext
            /// Properties for RequestedDtTm datamember
            /// </summary>
            public DateTime RequestedDtTm_Ext
            {
                get
                {
                    return this.IsRequestedDtTmNull() ? DateTime.Now : this.RequestedDtTm;
                }
                set
                {
                    this.RequestedDtTm = value;
                }
            }

            public String PONo_Ext
            {
                get
                {
                    return this.IsPONoNull() ? null : this.PONo;
                }
                set
                {
                    this.PONo = value;
                }
            }


            public String PriorityNo_Ext
            {
                get
                {
                    return this.IsPriorityNoNull() ? null : this.PriorityNo;
                }
                set
                {
                    this.PriorityNo = value;
                }
            }
        }

        /// <summary>
        /// Cloud_GetUndispatchLoadRow
        /// Partial LoadDetails row class to get and set datamembers
        /// </summary>
        partial class Cloud_GetUndispatchLoadRow
        {
            /// <summary>
            /// LoadNo_Ext
            /// Properties for LoadNo datamember
            /// </summary>
            public string LoadNo_Ext
            {
                get
                {
                    return this.LoadNo;
                }
                set
                {
                    this.LoadNo = value;
                }
            }

            /// <summary>
            /// DriverID_Ext
            /// Properties for DriverID datamember
            /// </summary>
            public int DriverID_Ext
            {
                get
                {
                    return this.IsDriverIDNull() ? 0 : this.DriverID;
                }
                set
                {
                    this.DriverID = value;
                }
            }

            /// <summary>
            /// Code_Ext
            /// Properties for Code datamember
            /// </summary>
            public string Code_Ext
            {
                get
                {
                    return this.Code;
                }
                set
                {
                    this.Code = value;
                }
            }

            /// <summary>
            /// VehicleID_Ext
            /// Properties for VehicleID datamember
            /// </summary>
            public int VehicleID_Ext
            {
                get
                {
                    return this.IsDefVehicleIDNull() ? 0 : this.DefVehicleID;
                }
                set
                {
                    this.DefVehicleID = value;
                }
            }

            /// <summary>
            /// VehicleCode_Ext
            /// Properties for VehicleCode datamember
            /// </summary>
            public string VehicleCode_Ext
            {
                get
                {
                    return this.IsVehicleCodeNull() ? string.Empty : this.VehicleCode;
                }
                set
                {
                    this.VehicleCode = value;
                }
            }
        }

        /// <summary>
        /// LoadDetailsRow
        /// Partial LoadDetails row class to get and set datamembers
        /// </summary>
        partial class LoadDetailsRow
        {
            /// <summary>
            /// LoadNo_Ext
            /// Properties for LoadNo datamember
            /// </summary>
            public string LoadNo_Ext
            {
                get
                {
                    return this.LoadNo;
                }
                set
                {
                    this.LoadNo = value;
                }
            }

            /// <summary>
            /// DriverID_Ext
            /// Properties for DriverID datamember
            /// </summary>
            public int DriverID_Ext
            {
                get
                {
                    return this.IsDriverIDNull() ? 0 : this.DriverID;
                }
                set
                {
                    this.DriverID = value;
                }
            }

            /// <summary>
            /// Code_Ext
            /// Properties for Code datamember
            /// </summary>
            public string Code_Ext
            {
                get
                {
                    return this.Code;
                }
                set
                {
                    this.Code = value;
                }
            }

            /// <summary>
            /// VehicleID_Ext
            /// Properties for VehicleID datamember
            /// </summary>
            public int VehicleID_Ext
            {
                get
                {
                    return this.IsDefVehicleIDNull() ? 0 : this.DefVehicleID;
                }
                set
                {
                    this.DefVehicleID = value;
                }
            }

            /// <summary>
            /// VehicleCode_Ext
            /// Properties for VehicleCode datamember
            /// </summary>
            public string VehicleCode_Ext
            {
                get
                {
                    return this.IsVehicleCodeNull() ? string.Empty : this.VehicleCode;
                }
                set
                {
                    this.VehicleCode = value;
                }
            }

            /// <summary>
            /// SleeperRig_Ext
            /// Properties for SleeperRig datamember
            /// </summary>                
            public Boolean SleeperRig_Ext
            {
                get
                {
                    if (this.IsSleeperRigNull())
                    {
                        return false;
                    }
                    return this.SleeperRig;
                }
                set
                {
                    this.SleeperRig = value;
                }
            }

            /// <summary>
            /// UserName_Ext
            /// Properties for UserName datamember
            /// </summary>
            public string UserName_Ext
            {
                get
                {
                    return this.IsUserNameNull() ? string.Empty : this.UserName;
                }
                set
                {
                    this.UserName = value;
                }
            }

            /// <summary>
            /// Password_Ext
            /// Properties for Password datamember
            /// </summary>
            public string Password_Ext
            {
                get
                {
                    return this.IsPasswordNull() ? string.Empty : this.Password;
                }
                set
                {
                    this.Password = value;
                }
            }

            /// <summary>
            /// Email_Ext
            /// Properties for Email datamember
            /// </summary>
            public string Email_Ext
            {
                get
                {
                    return this.IsEmailNull() ? string.Empty : this.Email;
                }
                set
                {
                    this.Email = value;
                }
            }

            /// <summary>
            /// FirstName_Ext
            /// Properties for FirstName datamember
            /// </summary>
            public string FirstName_Ext
            {
                get
                {
                    return this.IsFirstNameNull() ? string.Empty : this.FirstName;
                }
                set
                {
                    this.FirstName = value;
                }
            }

            /// <summary>
            /// MiddleName_Ext
            /// Properties for MiddleName datamember
            /// </summary>
            public string MiddleName_Ext
            {
                get
                {
                    return this.IsMiddleNameNull() ? string.Empty : this.MiddleName;
                }
                set
                {
                    this.MiddleName = value;
                }
            }

            /// <summary>
            /// LastName_Ext
            /// Properties for LastNam datamember
            /// </summary>
            public string LastName_Ext
            {
                get
                {
                    return this.IsLastNameNull() ? string.Empty : this.LastName;
                }
                set
                {
                    this.LastName = value;
                }
            }

            /// <summary>
            /// BOLWaitTime_Enabled_Ext
            /// Properties for BOLWaitTime_Enabled datamember
            /// </summary>                
            public Boolean BOLWaitTime_Enabled_Ext
            {
                get
                {
                    if (this.IsBOLWaitTime_EnabledNull())
                    {
                        return false;
                    }
                    return this.BOLWaitTime_Enabled;
                }
                set
                {
                    this.BOLWaitTime_Enabled = value;
                }
            }

            /// <summary>
            /// SiteWaitTim_Enablede_Ext
            /// Properties for SiteWaitTime_Enabled datamember
            /// </summary>
            public Boolean SiteWaitTime_Enabled_Ext
            {
                get
                {
                    if (this.IsSiteWaitTime_EnabledNull())
                    {
                        return false;
                    }
                    return this.SiteWaitTime_Enabled;
                }
                set
                {
                    this.SiteWaitTime_Enabled = value;
                }
            }

            /// <summary>
            /// SplitLoad_Enabled_Ext
            /// Properties for SplitLoad datamember
            /// </summary>                
            public Boolean SplitLoad_Enabled_Ext
            {
                get
                {
                    if (this.IsSplitLoad_EnabledNull())
                    {
                        return false;
                    }
                    return this.SplitLoad_Enabled;
                }
                set
                {
                    this.SplitLoad_Enabled = value;
                }
            }

            /// <summary>
            /// SplitDrop_Enabled_Ext
            /// Properties for SplitDrop_Enabled datamember
            /// </summary>                
            public Boolean SplitDrop_Enabled_Ext
            {
                get
                {
                    if (this.IsSplitDrop_EnabledNull())
                    {
                        return false;
                    }
                    return this.SplitDrop_Enabled;
                }
                set
                {
                    this.SplitDrop_Enabled = value;
                }
            }

            /// <summary>
            /// PumpOut_Enabled_Ext
            /// Properties for PumpOut_Enabled datamember
            /// </summary>                
            public Boolean PumpOut_Enabled_Ext
            {
                get
                {
                    if (this.IsPumpOut_EnabledNull())
                    {
                        return false;
                    }
                    return this.PumpOut_Enabled;
                }
                set
                {
                    this.PumpOut_Enabled = value;
                }
            }

            /// <summary>
            /// Diversion_Enabled_Ext
            /// Properties for Diversion_Enabled datamember
            /// </summary>                
            public Boolean Diversion_Enabled_Ext
            {
                get
                {
                    if (this.IsDiversion_EnabledNull())
                    {
                        return false;
                    }
                    return this.Diversion_Enabled;
                }
                set
                {
                    this.Diversion_Enabled = value;
                }
            }

            /// <summary>
            /// MinLoad_Enabled_Ext
            /// Properties for MinLoad_Enabled datamember
            /// </summary>                
            public Boolean MinLoad_Enabled_Ext
            {
                get
                {
                    if (this.IsMinLoad_EnabledNull())
                    {
                        return false;
                    }
                    return this.MinLoad_Enabled;
                }
                set
                {
                    this.MinLoad_Enabled = value;
                }
            }

            /// <summary>
            /// Other_Enabled_Ext
            /// Properties for Other_Enabled datamember
            /// </summary>               
            public Boolean Other_Enabled_Ext
            {
                get
                {
                    if (this.IsOther_EnabledNull())
                    {
                        return false;
                    }
                    return this.Other_Enabled;
                }
                set
                {
                    this.Other_Enabled = value;
                }
            }

            /// <summary>
            /// BOLWaitTime_Enabled_Reason_Ext
            /// Properties for BOLWaitTime_Enabled_Reason datamember
            /// </summary>                
            public Boolean BOLWaitTime_Enabled_Reason_Ext
            {
                get
                {
                    if (this.IsBOLWaitTime_Enabled_ReasonNull())
                    {
                        return false;
                    }
                    return this.BOLWaitTime_Enabled_Reason;
                }
                set
                {
                    this.BOLWaitTime_Enabled_Reason = value;
                }
            }

            /// <summary>
            /// SiteWaitTim_Enablede_Reason_Ext
            /// Properties for SiteWaitTime_Enabled_Reason datamember
            /// </summary>
            public Boolean SiteWaitTime_Enabled_Reason_Ext
            {
                get
                {
                    if (this.IsSiteWaitTime_Enabled_ReasonNull())
                    {
                        return false;
                    }
                    return this.SiteWaitTime_Enabled_Reason;
                }
                set
                {
                    this.SiteWaitTime_Enabled_Reason = value;
                }
            }

            /// <summary>
            /// SplitLoad_Enabled_Reason_Ext
            /// Properties for SplitLoad_Reason datamember
            /// </summary>                
            public Boolean SplitLoad_Enabled_Reason_Ext
            {
                get
                {
                    if (this.IsSplitLoad_Enabled_ReasonNull())
                    {
                        return false;
                    }
                    return this.SplitLoad_Enabled_Reason;
                }
                set
                {
                    this.SplitLoad_Enabled_Reason = value;
                }
            }

            /// <summary>
            /// SplitDrop_Enabled_Reason_Ext
            /// Properties for SplitDrop_Enabled_Reason datamember
            /// </summary>                
            public Boolean SplitDrop_Enabled_Reason_Ext
            {
                get
                {
                    if (this.IsSplitDrop_Enabled_ReasonNull())
                    {
                        return false;
                    }
                    return this.SplitDrop_Enabled_Reason;
                }
                set
                {
                    this.SplitDrop_Enabled_Reason = value;
                }
            }

            /// <summary>
            /// PumpOut_Enabled_Reason_Ext
            /// Properties for PumpOut_Enabled_Reason datamember
            /// </summary>                
            public Boolean PumpOut_Enabled_Reason_Ext
            {
                get
                {
                    if (this.IsPumpOut_Enabled_ReasonNull())
                    {
                        return false;
                    }
                    return this.PumpOut_Enabled_Reason;
                }
                set
                {
                    this.PumpOut_Enabled_Reason = value;
                }
            }

            /// <summary>
            /// Diversion_Enabled_Reason_Ext
            /// Properties for Diversion_Enabled_Reason datamember
            /// </summary>                
            public Boolean Diversion_Enabled_Reason_Ext
            {
                get
                {
                    if (this.IsDiversion_Enabled_ReasonNull())
                    {
                        return false;
                    }
                    return this.Diversion_Enabled_Reason;
                }
                set
                {
                    this.Diversion_Enabled_Reason = value;
                }
            }

            /// <summary>
            /// MinLoad_Enabled_Reason_Ext
            /// Properties for MinLoad_Reason_Enabled datamember
            /// </summary>                
            public Boolean MinLoad_Enabled_Reason_Ext
            {
                get
                {
                    if (this.IsMinLoad_Enabled_ReasonNull())
                    {
                        return false;
                    }
                    return this.MinLoad_Enabled_Reason;
                }
                set
                {
                    this.MinLoad_Enabled_Reason = value;
                }
            }

            /// <summary>
            /// Other_Enabled_Reason_Ext
            /// Properties for Other_Enabled_Reason datamember
            /// </summary>               
            public Boolean Other_Enabled_Reason_Ext
            {
                get
                {
                    if (this.IsOther_Enabled_ReasonNull())
                    {
                        return false;
                    }
                    return this.Other_Enabled_Reason;
                }
                set
                {
                    this.Other_Enabled_Reason = value;
                }
            }

            /// <summary>
            /// OrderLoadReviewEnabled_Ext
            /// Properties for OrderLoadReviewEnabled datamember
            /// </summary>               
            public string OrderLoadReviewEnabled_Ext
            {
                get
                {
                    return this.OrderLoadReviewEnabled;
                }
                set
                {
                    this.OrderLoadReviewEnabled = value;
                }
            }


            /// <summary>
            /// QtyTolerance_Ext
            /// Properties for QtyTolerance datamember
            /// </summary>               
            public decimal? QtyTolerance_Ext
            {
                get
                {
                    return IsQtyToleranceNull() ? (decimal?)null : this.QtyTolerance;
                }
                set
                {
                    this.QtyTolerance = value.Value;
                }
            }

            /// <summary>
            /// PercentTolerance_Ext
            /// Properties for PercentTolerance datamember
            /// </summary>               
            public decimal? PercentTolerance_Ext
            {
                get
                {
                    return IsPercentToleranceNull() ? (decimal?)null : this.PercentTolerance;
                }
                set
                {
                    this.PercentTolerance = value.Value;
                }
            }
            /// <summary>
            /// LoadType_Ext
            /// Properties for LoadType datamember
            /// </summary>
            public string LoadType_Ext
            {
                get
                {
                    return this.IsLoadTypeNull() ? string.Empty : this.LoadType;
                }
                set
                {
                    this.LoadType = value;
                }
            }
            // 2014.01.23  Ramesh M Added For CR#61759 Added ShipToID.
            // 09-18-2014  MadhuVenkat k - Added for CR 65002 - Application not allowing to change the From SiteID on BOL processing.
            public int ShipToID_Ext
            {
                get
                {
                    return this.IsToSiteIDNull() ? 0 : this.ToSiteID;
                }
                set
                {
                    this.ToSiteID = value;
                }
            }

        }

        /// <summary>
        /// UpdatedDriversRow
        /// Partial UpdatedDrivers row class to get and set datamembers
        /// </summary>
        partial class UpdatedDriversRow
        {
            /// <summary>
            /// DriverID_Ext
            /// Properties for DriverID datamember
            /// </summary>
            public int DriverID_Ext
            {
                get
                {
                    return this.DriverID;
                }
                set
                {
                    this.DriverID = value;
                }
            }

            /// <summary>
            /// UserName_Ext
            /// Properties for UserName datamember
            /// </summary>
            public string UserName_Ext
            {
                get
                {
                    return this.IsUserNameNull() ? string.Empty : this.UserName;
                }
                set
                {
                    this.UserName = value;
                }
            }

            /// <summary>
            /// Password_Ext
            /// Properties for Password datamember
            /// </summary>
            public string Password_Ext
            {
                get
                {
                    return this.IsPasswordNull() ? string.Empty : this.Password;
                }
                set
                {
                    this.Password = value;
                }
            }

            /// <summary>
            /// Email_Ext
            /// Properties for Email datamember
            /// </summary>
            public string Email_Ext
            {
                get
                {
                    return this.IsEmailNull() ? string.Empty : this.Email;
                }
                set
                {
                    this.Email = value;
                }
            }

            /// <summary>
            /// FirstName_Ext
            /// Properties for FirstName datamember
            /// </summary>
            public string FirstName_Ext
            {
                get
                {
                    return this.IsFirstNameNull() ? string.Empty : this.FirstName;
                }
                set
                {
                    this.FirstName = value;
                }
            }

            /// <summary>
            /// MiddleName_Ext
            /// Properties for MiddleName datamember
            /// </summary>
            public string MiddleName_Ext
            {
                get
                {
                    return this.IsMiddleNameNull() ? string.Empty : this.MiddleName;
                }
                set
                {
                    this.MiddleName = value;
                }
            }

            /// <summary>
            /// LastName_Ext
            /// Properties for LastName datamember
            /// </summary>
            public string LastName_Ext
            {
                get
                {
                    return this.IsLastNameNull() ? string.Empty : this.LastName;
                }
                set
                {
                    this.LastName = value;
                }
            }
            /// <summary>
            /// UserType_Ext
            /// Properties for UserType datamember
            /// </summary>
            public string UserType_Ext
            {
                get
                {
                    return this.UserType;
                }
                set
                {
                    this.UserType = value;
                }
            }
            // 2014.03.17  Ramesh M Added For CR#62668 to get home terminal details
            public String Co_Name_Ext
            {
                get
                {
                    return this.IsCo_NameNull() ? null : this.Co_Name;
                }
                set
                {
                    this.Co_Name = value;
                }
            }
            public String Co_Addr1_Ext
            {
                get
                {
                    return this.IsCo_Addr1Null() ? null : this.Co_Addr1;
                }
                set
                {
                    this.Co_Addr1 = value;
                }
            }
            public String Co_City_Ext
            {
                get
                {
                    return this.IsCo_CityNull() ? null : this.Co_City;
                }
                set
                {
                    this.Co_City = value;
                }
            }
            public String Co_State_Ext
            {
                get
                {
                    return this.IsCo_StateNull() ? null : this.Co_State;
                }
                set
                {
                    this.Co_State = value;
                }
            }
            public String Co_Zip_Ext
            {
                get
                {
                    return this.IsCo_ZipNull() ? null : this.Co_Zip;
                }
                set
                {
                    this.Co_Zip = value;
                }
            }
            public String HT_Descr_Ext
            {
                get
                {
                    return this.IsHT_DescrNull() ? null : this.HT_Descr;
                }
                set
                {
                    this.HT_Descr = value;
                }
            }
            public String HT_Addr1_Ext
            {
                get
                {
                    return this.IsHT_Addr1Null() ? null : this.HT_Addr1;
                }
                set
                {
                    this.HT_Addr1 = value;
                }
            }
            public String HT_City_Ext
            {
                get
                {
                    return this.IsHT_CityNull() ? null : this.HT_City;
                }
                set
                {
                    this.HT_City = value;
                }
            }
            public String HT_State_Ext
            {
                get
                {
                    return this.IsHT_StateNull() ? null : this.HT_State;
                }
                set
                {
                    this.HT_State = value;
                }
            }
            public String HT_Zip_Ext
            {
                get
                {
                    return this.IsHT_ZipNull() ? null : this.HT_Zip;
                }
                set
                {
                    this.HT_Zip = value;
                }
            }
            public DateTime HazMatDate_Ext
            {
                get
                {
                    return this.IsHazMatDateNull() ? DateTime.Now : this.HazMatDate;
                }
                set
                {
                    this.HazMatDate = value;
                }
            }
        }


        /// <summary>
        /// UpdatedVehicleRow
        /// Partial UpdatedVehicle row class to get and set datamembers
        /// </summary>
        partial class UpdatedVehicleRow
        {
            /// <summary>
            /// VehicleID_Ext
            /// Properties for VehicleID datamember
            /// </summary>
            public int VehicleID_Ext
            {
                get
                {
                    return this.VehicleID;
                }
                set
                {
                    this.VehicleID = value;
                }
            }

            /// <summary>
            /// Code_Ext
            /// Properties for Code datamember
            /// </summary>
            public string Code_Ext
            {
                get
                {
                    return this.Code;
                }
                set
                {
                    this.Code = value;
                }
            }

            /// <summary>
            /// SleeperRig_Ext
            /// Properties for SleeperRig datamember
            /// </summary>
            public Boolean SleeperRig_Ext
            {
                get
                {
                    return this.SleeperRig;
                }
                set
                {
                    this.SleeperRig = value;
                }
            }

            /// <summary>
            /// VehicleType_Ext
            /// Properties for VehicleType datamember
            /// </summary>
            public int VehicleType_Ext
            {
                get
                {
                    return this.VehicleType;
                }
                set
                {
                    this.VehicleType = value;
                }
            }

            /// <summary>
            /// OverShortSiteID_Ext
            /// Properties for OverShortSiteID datamember
            /// </summary>
            public int OverShortSiteID_Ext
            {
                get
                {
                    return this.OverShortSiteID;
                }
                set
                {
                    this.OverShortSiteID = value;
                }
            }
        }

        partial class GetUpdatedSysParamsRow
        {

            /// <summary>
            /// AllowDriversToChangeFromSite_Ext
            /// Properties for AllowDriversToChangeFromSite datamember
            /// </summary>
            public string AllowDriversToChangeFromSite_Ext
            {
                get
                {
                    return this.AllowDriversToChangeFromSite;
                }
                set
                {
                    this.AllowDriversToChangeFromSite = value;
                }
            }
            /// <summary>
            /// ClientID_Ext
            /// Properties for ClientID datamember
            /// </summary>
            public string ClientID_Ext
            {
                get
                {
                    return this.ClientID;
                }
                set
                {
                    this.ClientID = value;
                }
            }

            // 2014.01.30  Ramesh M Added For CR#62038 Added AllowDriversToChangeMultiBOL
            /// <summary>
            /// AllowDriversToChangeMultiBOL_Ext
            /// Properties for AllowDriversToChangeMultiBOL datamember
            /// </summary>
            public string AllowDriversToChangeMultiBOL_Ext
            {
                get
                {
                    return this.AllowDriversToChangeMultiBOL;
                }
                set
                {
                    this.AllowDriversToChangeMultiBOL = value;
                }
            }

        }

        partial class GetUpdatedOEDefSuppSupplyPtRow
        {

            /// <summary>
            /// ShipToID
            /// Properties for ShiptoID datamember
            /// </summary>
            public Int32? ShipToID_Ext
            {
                get
                {
                    return this.ShipToID;
                }
                set
                {
                    this.ShipToID = value;
                }
            }
            /// <summary>
            /// SupplierSupplyPtID_Ext
            /// Properties for SupplierSupplyPtID datamember
            /// </summary>
            public Int32 SupplierSupplyPtID_Ext
            {
                get
                {
                    return this.SupplierSupplyPtID;
                }
                set
                {
                    this.SupplierSupplyPtID = value;
                }
            }
            /// <summary>
            /// 
            /// </summary>
            public Int32 SupplierID_Ext
            {
                get
                {
                    return this.SupplierID;
                }
                set
                {
                    this.SupplierID = value;
                }
            }
            /// <summary>
            /// 
            /// </summary>
            public Int32 SupplyPtID_Ext
            {
                get
                {
                    return this.SupplyPtID;
                }
                set
                {
                    this.SupplierID = value;
                }
            }
            /// <summary>
            /// SupplierDescr_Ext
            /// Properties for SupplierDescr datamember
            /// </summary>           
            public string SupplierDescr_Ext
            {
                get
                {
                    return this.SupplierDescr;
                }
                set
                {
                    this.SupplierDescr = value;
                }
            }

            /// <summary>
            /// SupplyPtDescr_Ext
            /// Properties for SupplyPtDescr datamember
            /// </summary>
            public string SupplyPtDescr_Ext
            {
                get
                {
                    return this.SupplyPtDescr;
                }
                set
                {
                    this.SupplyPtDescr = value;
                }
            }
            /// <summary>
            /// Address1_Ext
            /// Properties for Address1 datamember
            /// </summary>
            public string Address1_Ext
            {
                get
                {
                    return this.Address1;
                }
                set
                {
                    this.Address1 = value;
                }
            }
            /// <summary>
            /// Address2_Ext
            /// Properties for Address1 datamember
            /// </summary>
            public string Address2_Ext
            {
                get
                {
                    return this.Address2;
                }
                set
                {
                    this.Address2 = value;
                }
            }
            /// <summary>
            /// ClientID_Ext
            /// Properties for ClientID datamember
            /// </summary>
            public string ClientID_Ext
            {
                get
                {
                    return this.ClientID;
                }
                set
                {
                    this.ClientID = value;
                }
            }

            /// <summary>
            /// 
            /// </summary>
            public string SupplierCode_Ext
            {
                get
                {
                    return this.IsSupplierCodeNull() ? string.Empty : this.SupplierCode;
                }
                set
                {
                    this.SupplierCode = value;
                }
            }
            /// <summary>
            /// 
            /// </summary>
            public string SupplyPtCode_Ext
            {
                get
                {
                    return this.IsSupplyPtCodeNull() ? string.Empty : this.SupplyPtCode;
                }
                set
                {
                    this.SupplyPtCode = value;
                }
            }
        }



        /// <summary>
        /// OrderItemComponentRow
        /// Partial OrderItemComponent row class to get and set datamembers
        /// </summary>
        partial class OrderItemComponentRow
        {
            /// <summary>
            /// ComponentNo_Ext
            /// Properties for ComponentNo datamember
            /// </summary>
            public int ComponentNo_Ext
            {
                get
                {
                    return this.ComponentNo;
                }
                set
                {
                    this.ComponentNo = value;
                }
            }

            /// <summary>
            /// OrderedQty_Ext
            /// Properties for OrderedQty datamember
            /// </summary>
            public Decimal OrderedQty_Ext
            {
                get
                {
                    return this.IsQtyNull() ? 0.0M : this.Qty;
                }
                set
                {
                    this.Qty = value;
                }
            }

            /// <summary>
            /// ProdCode_Ext
            /// Properties for ProdCode datamember
            /// </summary>
            public String ProdCode_Ext
            {
                get
                {
                    return this.IsProdCodeNull() ? null : this.ProdCode;
                }
                set
                {
                    this.ProdCode = value;
                }
            }

            /// <summary>
            /// ProdName_Ext
            /// Properties for ProdName datamember
            /// </summary>
            public String ProdName_Ext
            {
                get
                {
                    return this.IsProdNameNull() ? null : this.ProdName;
                }
                set
                {
                    this.ProdName = value;
                }
            }

            /// <summary>
            /// ProdUOM_Ext
            /// Properties for ProdUOM datamember
            /// </summary>
            public String ProdUOM_Ext
            {
                get
                {
                    return this.IsProdUOMNull() ? null : this.ProdUOM;
                }
                set
                {
                    this.ProdUOM = value;
                }
            }

            /// <summary>
            /// SupplierName_Ext
            /// Properties for SupplierName datamember
            /// </summary>
            public String SupplierName_Ext
            {
                get
                {
                    return this.IsSupplierNameNull() ? null : this.SupplierName;
                }
                set
                {
                    this.SupplierName = value;
                }
            }

            /// <summary>
            /// SupplierCode_Ext
            /// Properties for SupplierCode datamember
            /// </summary>
            public String SupplierCode_Ext
            {
                get
                {
                    return this.IsSupplierCodeNull() ? null : this.SupplierCode;
                }
                set
                {
                    this.SupplierCode = value;
                }
            }

            /// <summary>
            /// SupplyPointName_Ext
            /// Properties for SupplyPointName datamember
            /// </summary>
            public String SupplyPointName_Ext
            {
                get
                {
                    return this.IsSupplyPointNameNull() ? null : this.SupplyPointName;
                }
                set
                {
                    this.SupplyPointName = value;
                }
            }

            /// <summary>
            /// SupplyPointCode_Ext
            /// Properties for SupplyPointCode datamember
            /// </summary>
            public String SupplyPointCode_Ext
            {
                get
                {
                    return this.IsSupplyPointCodeNull() ? null : this.SupplyPointCode;
                }
                set
                {
                    this.SupplyPointCode = value;
                }
            }

            /// <summary>
            /// SupplyPointAddress1_Ext
            /// Properties for SupplyPointAddress1s datamember
            /// </summary>
            public String SupplyPointAddress1_Ext
            {
                get
                {
                    return this.IsSupplyPointAddress1Null() ? null : this.SupplyPointAddress1;
                }
                set
                {
                    this.SupplyPointAddress1 = value;
                }
            }

            /// <summary>
            /// SupplyPointAddress2_Ext
            /// Properties for SupplyPointAddress2 datamember
            /// </summary>
            public String SupplyPointAddress2_Ext
            {
                get
                {
                    return this.IsSupplyPointAddress2Null() ? null : this.SupplyPointAddress2;
                }
                set
                {
                    this.SupplyPointAddress2 = value;
                }
            }

            /// <summary>
            /// City_Ext
            /// Properties for City datamember
            /// </summary>
            public String City_Ext
            {
                get
                {
                    return this.IsCityNull() ? null : this.City;
                }
                set
                {
                    this.City = value;
                }
            }

            /// <summary>
            /// State_Ext
            /// Properties for State datamember
            /// </summary>
            public String State_Ext
            {
                get
                {
                    return this.IsStateNull() ? null : this.State;
                }
                set
                {
                    this.State = value;
                }
            }

            /// <summary>
            /// Zip_Ext
            /// Properties for Zip datamember
            /// </summary>
            public String Zip_Ext
            {
                get
                {
                    return this.IsZipNull() ? null : this.Zip;
                }
                set
                {
                    this.Zip = value;
                }
            }

            /// <summary>
            /// Country_Ext
            /// Properties for Country datamember
            /// </summary>
            public String Country_Ext
            {
                get
                {
                    return this.IsCountryNull() ? null : this.Country;
                }
                set
                {
                    this.Country = value;
                }
            }

            /// <summary>
            /// FromCSTankID_Ext
            /// Properties for Country datamember
            /// </summary>
            public int FromCSTankID_Ext
            {
                get
                {
                    return this.IsFromCSTankIDNull() ? 0 : this.FromCSTankID;
                }
                set
                {
                    this.FromCSTankID = value;
                }
            }

            /// <summary>
            /// ToCSTankID_Ext
            /// Properties for Country datamember
            /// </summary>
            public int ToCSTankID_Ext
            {
                get
                {
                    return this.IsToCSTankIDNull() ? 0 : this.ToCSTankID;
                }
                set
                {
                    this.ToCSTankID = value;
                }
            }

            /// <summary>
            /// FromCSTankCode_Ext
            /// Properties for Country datamember
            /// </summary>
            public String FromCSTankCode_Ext
            {
                get
                {
                    return this.IsFromCSTankCodeNull() ? null : this.FromCSTankCode;
                }
                set
                {
                    this.FromCSTankCode = value;
                }
            }

            /// <summary>
            /// ToCSTankCode_Ext
            /// Properties for Country datamember
            /// </summary>
            public String ToCSTankCode_Ext
            {
                get
                {
                    return this.IsToCSTankCodeNull() ? null : this.ToCSTankCode;
                }
                set
                {
                    this.ToCSTankCode = value;
                }
            }


        }
        /// <summary>
        /// UpdatedInSiteUserRow
        ///  Partial UpdatedInSiteUser row class to get and set datamembers
        /// </summary>
        partial class UpdatedInSiteUserRow
        {
            /// <summary>
            /// SiteID_Ext
            /// Properties for SiteID datamember
            /// </summary>
            public int SiteID_Ext
            {
                get
                {
                    return this.SiteID;
                }
                set
                {
                    this.SiteID = value;
                }
            }
            /// <summary>
            /// SiteCode_Ext
            /// Properties for SiteCode datamember
            /// </summary>
            public string SiteCode_Ext
            {
                get
                {
                    return this.SiteCode;
                }
                set
                {
                    this.SiteCode = value;
                }
            }
            /// <summary>
            /// UserName_Ext
            /// Properties for UserName datamember
            /// </summary>
            public string UserName_Ext
            {
                get
                {
                    return this.UserName;
                }
                set
                {
                    this.UserName = value;
                }
            }

            /// <summary>
            /// Password_Ext
            /// Properties for Password datamember
            /// </summary>
            public string Password_Ext
            {
                get
                {
                    return this.Password;
                }
                set
                {
                    this.Password = value;
                }
            }
            /// <summary>
            /// UserType_Ext
            /// Properties for UserType datamember
            /// </summary>
            public string UserType_Ext
            {
                get
                {
                    return this.UserType;
                }
                set
                {
                    this.UserType = value;
                }
            }
        }
    }
}


namespace DeliveryStreamCustomerWinServ.DataAccess.DALTableAdapters
{
    partial class DestNoteTableAdapter
    {
    }

    partial class GetUpdatedOEDefSuppSupplyPtTableAdapter
    {
    }

    partial class UpdatedInSiteUserTableAdapter
    {
    }

    partial class UpdatedDriversTableAdapter
    {
    }


    public partial class OrderItemComponentTableAdapter
    {
    }
}
