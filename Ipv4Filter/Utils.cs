using System.Net;

namespace Ipv4Filter
{
    public class Utils
    {
        public static uint GetIPRange(int start, int end, out string startIp, out string endIp)
        {
            var range = uint.MaxValue;
            var t = uint.MaxValue;

            range = range >> start;
            startIp = new IPAddress(new IPAddress(~range).GetAddressBytes().Reverse().ToArray()).ToString();

            t = t << 32 - end;

            endIp = new IPAddress(new IPAddress(t).GetAddressBytes().Reverse().ToArray()).ToString();

            return range & t;
        }
    }
}
