using System.Collections;
using System.Text.Json.Serialization;

namespace Ipv4Filter.Models
{
    public record Options
    {
        [JsonIgnore]
        public string InputFilePath { get; set; }
        [JsonIgnore]
        public string OutputFilePath { get; set; }
        [JsonIgnore]
        public uint AddressRangeUInt { get; set; }
        public string FromIpAddress { get; set; }
        public string ToIpAddress { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
    }
}