using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheEpidemic
{
    // 전염률 업글 돈 및 돈 건드는가
    public interface ITouchGoldForInfect
    {
        public void UseGoldForInfect();
        public void UpgradeGoldForInfect();
    }

}
