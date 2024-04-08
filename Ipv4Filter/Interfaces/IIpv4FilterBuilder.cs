using Ipv4Filter.Models;

namespace Ipv4Filter.Interfaces
{
    public interface IIpv4FilterBuilder
    {
        IIpv4FilterBuilder AddCondition(Func<Options, IpModel, bool> filter);
        IIpv4Filter Build();
    }
}
