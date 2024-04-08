using Ipv4Filter.Interfaces;
using Ipv4Filter.Models;

namespace Ipv4Filter
{
    public class Ipv4Filter : IIpv4Filter
    {
        private readonly LinkedList<Func<Options, IpModel, bool>> _filters = new LinkedList<Func<Options, IpModel, bool>>();

        public Ipv4Filter(LinkedList<Func<Options, IpModel, bool>> filters)
        {
                _filters = filters;
        }

        public bool Execute(IpModel model, Options options) => _filters.All(filter => filter(options, model));
    }
}
