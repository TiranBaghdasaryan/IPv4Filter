namespace Ipv4Filter.Models
{
    public class Ipv4LogResultModel
    {
        public Options FilterOptios { get; set; }
        public IEnumerable<RequestCount> Logs { get; set; }
    }

    public class RequestCount
    {
        public string IpAddress { get; set; }
        public uint Count { get; set; }
    }
}
