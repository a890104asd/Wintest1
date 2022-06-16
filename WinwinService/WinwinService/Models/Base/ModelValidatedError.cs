using System.Runtime.Serialization;

namespace WinwinService.Models.Base
{
    [DataContract]
    public class ModelValidatedError
    {
        [DataMember]
        public string Key { get; set; }
        [DataMember]
        public string[] Value { get; set; }
    }


}
