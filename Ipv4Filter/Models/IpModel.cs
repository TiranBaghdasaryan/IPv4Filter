using System.Net;

namespace Ipv4Filter.Models
{
    public record IpModel
    {
        public DateTime RequestTime { get; private set; }
        public int IpAddressInt { get; private set; }
        public bool IsValid { get; private set; } = true;
        public int Count { get; set; }

        public IpModel(string address, string time)
        {
            var validAddress = IPAddress.TryParse(address, out var ipAddress);
            if (validAddress && ipAddress != null)
            {
                IpAddressInt = BitConverter.ToInt32(ipAddress.GetAddressBytes().Reverse().ToArray());
            }
            else
            {
                IsValid = false;
            }

            var validTime = DateTime.TryParse(time, out var dateTime);
            if (validTime)
            {
                RequestTime = dateTime;
            }
            else
            {
                IsValid = false;
            }
        }
    }
}
