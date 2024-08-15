using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheEpidemic
{
    //전염병 버프를 사용하는가?
    public interface ITouchBuff
    {
        public void OnOffBuff();
        public void UpdateBuffDuration();
        public void UpdateBuffWaitTime();
    }
}
