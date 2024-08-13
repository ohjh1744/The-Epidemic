using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheEpidemic
{
    public interface IGlobal
    {
        int Cure { get; set; }
        int DevelopRate { get; set; }
    }

    public class Global: IGlobal
    {
        int _cure = 0;
        int _developRate = 0;

        public int Cure { get { return _cure; } set { _cure = value; } }
        public int DevelopRate { get { return _developRate; } set { _developRate = value; } }
    }
}
