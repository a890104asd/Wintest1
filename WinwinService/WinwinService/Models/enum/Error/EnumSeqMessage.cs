namespace WinwinService.Models
{
    public enum EnumSeqMessage
{
        //public SeqMessage
        Cant_Be_Null = 9999,
        Type_Is_Wrong = 9998,
        Length_Is_Wrong = 9997,
        Is_Duplicated = 9996,
        Is_Invalid = 9995,
        Value_Formate_Is_wrong=9994,
        //System Error(For developer) 9000-9099
        Request_Is_Null = 9000,
        Tool_Token_Is_Invalid = 9001,
        Tool_Token_Is_Required = 9002,
        Exception_Problem =9003,
        Update_DB_Failed=9004,
        Data_Is_Not_Existed =9005,
        ErpData_Update_failed=9006,
        Track_Update_failed=9007,
        Data_Limited = 9008,
        Service_Permission_Is_Closed = 9009,
        Get_Live_Label_failed = 9010,
        //Tool-SXA
        //秤重 9200 - 9219
        OverWeight = 9200,
        IsScanned = 9201,
        NoRead= 9202,
        UnKnow=9203,
        Process_has_been_Responsed=9204,
        Processing = 9205,
        //receiving_item 9220-9229
        NoPackingRecords = 9220,
        PackingAlreadyScanned = 9221,
        HoldItem=9222,
        //HoldRequest
        Storage_Download_Failed=9300,
        Stroage_connecting_failed=9301,
        Storage_Upload_Failed=9302,
        //tool-Erp
        HoldRequestAlreadyExisted=9500,
    }
}
