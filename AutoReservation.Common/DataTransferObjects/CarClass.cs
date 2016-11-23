using System.Runtime.Serialization;

namespace CarReservation.Common.DataTransferObjects
{
    [DataContract]
    public enum CarClass
    {
        [EnumMember]
        Luxury = 0,
        [EnumMember]
        MidRange = 1,
        [EnumMember]
        Standard = 2
    }
}
