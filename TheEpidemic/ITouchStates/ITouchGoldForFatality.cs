using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheEpidemic
{
    //치사율 업글위한 돈 및 돈을 건드는가
    public interface ITouchGoldForFatality
    {
        public void UseGoldForFatality();
        public void UpgradeGoldForFatality();

    }
}
