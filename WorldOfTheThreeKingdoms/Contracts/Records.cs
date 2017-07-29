using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SeasonContracts
{
    [DataContract]
    public class UserIdentity
    {
        [DataMember]
        public string Guid { get; set; }

        [DataMember]
        public int UserID { get; set; }

        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string Contact { get; set; }

        [DataMember]
        public string UserIP { get; set; }

        [DataMember]
        public string DeviceId { get; set; }

        [DataMember]
        public string DeviceInfo { get; set; }

        [DataMember]
        public string Product { get; set; }

        [DataMember]
        public string Platform { get; set; }

        [DataMember]
        public string Channel { get; set; }

        [DataMember]
        public string Version { get; set; }
    }

    public enum GameUserAction
    {
        Info,
        Register,
        Login,
        LoginAuto,
        Modify,
        BuyProduct,
        BuyBattle,
        ChangePass,
        ResetPass,
        ChangeAvatar,
        ConsumeCredit,
        PaidPhone
    }

    public enum RecordType
    {
        Record,
        ClientStatus,
        ErrorLog,
        WebRecord
    }

    public interface iRecord
    {
        int SortID { get; set; }
        int RecordID { get; set; }
        string ResultStatus { get; set; }
        string ResultDetail { get; set; }
        string ErrorMsg { get; set; }
        DateTime? CreateTime { get; set; }
        string RefreshTime { get; set; }
        UserIdentity UserIdentity { get; set; }
    }

    [DataContract]
    [KnownType(typeof(Record))]
    [KnownType(typeof(ClientStatus))]
    [KnownType(typeof(ErrorLog))]
    [KnownType(typeof(UnknownTexts))]
    [KnownType(typeof(WebRecord))]
    public abstract class RecordBase : iRecord
    {
        [DataMember]
        public int SortID { get; set; }
        [DataMember]
        public int RecordID { get; set; }
        [DataMember]
        public string ResultStatus { get; set; }
        [DataMember]
        public string ResultDetail { get; set; }
        [DataMember]
        public string ErrorMsg { get; set; }
        /// <summary>
        /// 記錄開始時間
        /// </summary>
        [DataMember]
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// 記錄開始時間
        /// </summary>
        [DataMember]
        public string RefreshTime { get; set; }
        [DataMember]
        public UserIdentity UserIdentity { get; set; }

        public override string ToString()
        {
            return ResultStatus;
        }
    }

    [DataContract]
    public class Record : RecordBase
    {

    }

    [DataContract]
    public class ClientStatus : RecordBase
    {
        //[DataMember]
        //public RecordType RecordType { get { return RecordType.ClientStatus; } }
        [DataMember]
        public string Requirement { get; set; }
        [DataMember]
        public string DeviceInfo { get; set; }
        [DataMember]
        public string SystemInfo { get; set; }
        [DataMember]
        public string UserInfo { get; set; }

        public override string ToString()
        {
            return ResultStatus + " " + DeviceInfo + " " + UserIdentity.DeviceId + " " + SystemInfo + " " + UserInfo;
        }
    }

    /// <summary>
    /// 事件日志
    /// </summary>
    [DataContract]
    public class ErrorLog : RecordBase
    {
        [DataMember]
        public int? BaseRecordID { get; set; }
        [DataMember]
        public string Type { get; set; }
        [DataMember]
        public string PlatformVer { get; set; }
        [DataMember]
        public string ContentCategory { get; set; }
        [DataMember]
        public string Content { get; set; }
        [DataMember]
        public string ContentBrief { get; set; }
        [DataMember]
        public bool ContentMulti { get; set; }
        [DataMember]
        public int Count { get; set; }
        [DataMember]
        public Dictionary<string, int[]> RelateDate { get; set; }
        [DataMember]
        public string LastTime { get; set; }
        [DataMember]
        public bool Processed { get; set; }
        public override string ToString()
        {
            return ResultStatus + " " + UserIdentity.Platform + UserIdentity.Channel + " " + UserIdentity.Version + " " + ErrorMsg;
        }
    }

    [DataContract]
    public class UnknownTexts : RecordBase
    {
        [DataMember]
        public string Content { get; set; }
    }

    [DataContract]
    public class WebRecord : RecordBase
    {
        [DataMember]
        public string Content { get; set; }
        public override string ToString()
        {
            return ResultStatus + " " + Content;
        }
    }    

    public enum RecordsType
    {
        GameUser,
        SystemRecord,
        RecentGamerRanks,
        RecentChats,
        PayOrder,
        PaidPhones,
        AnalyzeTotal,
        AnalyzeError,
        RecentLogs,
        Summary,
        Market
        //HistoryRecord
    }

    [DataContract]
    [KnownType(typeof(Record))]
    [KnownType(typeof(ClientStatus))]
    [KnownType(typeof(ErrorLog))]
    [KnownType(typeof(UnknownTexts))]
    [KnownType(typeof(WebRecord))]
    public class Records
    {
        [DataMember]
        public RecordsType RecordsType { get; set; }
        [DataMember]
        public string Guid { get; set; }
        [DataMember]
        public int[] UserIds { get; set; }
        [DataMember]
        public string[] UserDevices { get; set; }

        [DataMember]
        public string[] Types = new string[] { };

        [DataMember]
        public string[] Platforms = new string[] { };

        [DataMember]
        public string[] Versions = new string[] { };

        [DataMember]
        public Dictionary<string, string> RecordParams { get; set; }
        [DataMember]
        public List<iRecord> RecordList { get; set; }
    }
    
}