using Ipv4Filter.Models;

namespace Ipv4Filter.Interfaces
{
    public interface IIpv4Filter
    {
        bool Execute(IpModel model, Options options);
    }
}