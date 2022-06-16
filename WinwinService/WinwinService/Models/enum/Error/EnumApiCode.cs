namespace WinwinService.Models
{
    public enum EnumApiCode
    {
        //System 000-099
        Getlocalization = 000,
        CreateScanPrintLog = 001,
        GetScanPrintLog = 002,
        GetFilteredScanPrintLog = 003,
        GetLiveLabel = 004,
        SystemUpdate = 005,
        //Account Service 100-199
        Account_Login = 100,
        Account_GetUserInfo = 101,
        Account_GetUsers = 102,
        //Stock in 200-299
        StockIn_Weight = 200,
        StockIn_ScanLogSearch = 201,
        StockIn_ReceivingBox = 202,
        StockIn_ReceivingItem = 203,
        StockIn_ReceivingItemLog = 204,
        StockIn_PackingListExport = 205,
        StockIn_PackingListExportUs = 206,
        StockIn_PackingListExportAu = 207,
        StockIn_OffReceivingItem = 208,
        //HoldRequest
        HoldRequest_GetSyncHoldList = 300,
        HoldRequest_ConfirmHold = 301,
        HoldRequest_DownloadFile = 302,
        HoldRequest_Bulk_ConfirmHold = 303,
        HoldRequest_Create = 304,
        HoldRequest_Update = 305,
        HoldRequest_Bulk_Create = 306,
        //Stock Out 400-450
        StockOut_CreateBox = 400,
        StockOut_DeleteBox = 401,
        StockOut_UpdateBox = 402,
        StockOut_CreatePackingItem = 403,
        StockOut_DeletePackingItem = 404,
        StockOut_GetBoxes = 405,
        StockOut_GetBoxesPackingList = 406,
        StockOut_GetBoxRemark = 407,
        StockOut_GetSysTime = 408,
        StockOut_GetWareHouseList = 409,
        StockOut_GetAllWareHouse = 410,
        StockOut_PackingWeightSearch = 411,

        //Outbound Inbound 451-499
        Outbound_GetBoxes = 451,
        Outbound_CreateBox = 452,
        Outbound_UpdateBox = 453,
        Outbound_DeleteBox = 454,
        Outbound_CreatePackingItem = 455,
        Outbound_DeletePackingItem = 456,
        Outbound_GetBoxPackingList = 457,
        Outbound_GetUserPackingTypePermission = 458,
        Outbound_SearchItemList = 459,
        Inbound_ArrivalScan = 476,

        //Return 500-599
        Return_Weight = 500,
        Return_ScanLogSearch = 501

    }
}
