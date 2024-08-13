using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheEpidemic
{
    public interface IEpidemicFactory
    {
        IEpidemic Create();
    }

    public class VirusFactory : IEpidemicFactory
    {
        public IEpidemic Create()
        {
            return new Virus();
        }
    }

    public class BacteriaFactory: IEpidemicFactory
    {
        public IEpidemic Create()
        {
            return new Bacteria();
        }
    }
}
