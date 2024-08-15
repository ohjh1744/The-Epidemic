using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheEpidemic.ITouchStates
{
    //전염률 건드는가
    public interface ITouchInfectRate
    {
        public void UpdateInfectRate();
    }

}
