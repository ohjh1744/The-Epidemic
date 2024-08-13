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
    }

    public class Global: IGlobal
    {
        int _cure = 0;

        public int Cure { get { return _cure; } set { _cure = value; } }
    }
}
