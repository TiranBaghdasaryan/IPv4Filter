using Ipv4Filter.Interfaces;
using Ipv4Filter.Models;

namespace Ipv4Filter.Implementations
{
    public class Ipv4FilterBuilder : IIpv4FilterBuilder
    {
        private LinkedList<Func<Options, IpModel, bool>> filters = new LinkedList<Func<Options, IpModel, bool>>();

        public IIpv4FilterBuilder AddCondition(Func<Options, IpModel, bool> filter)
        {
            filters.AddLast(filter);

            return this;
        }

        public IIpv4Filter Build() => new Ipv4Filter(filters);
    }
}
