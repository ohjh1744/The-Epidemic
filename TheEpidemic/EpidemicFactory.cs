using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheEpidemic
{
    public interface IEpidemicFactory
    {
        IEpidemic Create(int num);
    }

    public class VirusFactory : IEpidemicFactory
    {
        public IEpidemic Create(int num)
        {
            if(num == 1)
            {
                return new Virus();
            }
            else
            {
                throw new InvalidOperationException("숫자 1만 반환합니다.");
            }
        }
    }

    public class BacteriaFactory: IEpidemicFactory
    {
        public IEpidemic Create(int num)
        {
            if (num == 2)
            {
                return new Bacteria();
            }
            else
            {
                throw new InvalidOperationException("숫자 2만 반환합니다.");
            }
        }
    }
}
