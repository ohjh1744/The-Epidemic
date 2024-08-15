﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheEpidemic
{
    // Epdemic 전염병 팩토리 메서드
    public interface IEpidemicFactory
    {
        Epidemic Create();
    }

    public class VirusFactory : IEpidemicFactory
    {
        public Epidemic Create()
        {
            return new Virus();
        }
    }

    public class BacteriaFactory: IEpidemicFactory
    {
        public Epidemic Create()
        {
            return new Bacteria();
        }
    }
}
