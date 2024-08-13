using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheEpidemic
{
    public abstract class IEpidemic
    {

        public int InfectRate { get; set; }
        public int FatalityRate { get; set; }

        public bool IsSkill { get; set; }

        public abstract void UseSkill();

    }


    public class Virus : IEpidemic
    {

        private int _infectRate = 20;
        private int _fatalityRate = 5;
        private bool _isSkill = false;

        public int InfectRate{get{return _infectRate;} set{_infectRate = value;}}
        public int FatalityRate { get { return _fatalityRate; } set { _fatalityRate = value; } }
        public bool IsSkill { get { return _isSkill; } set { _isSkill = value; } }

        public void EnforceSkill()
        {
            if(IsSkill == false)
            {
                InfectRate *= 2;
            }
        }

        public override void UseSkill()
        {
            EnforceSkill();
        }


    }

    public class Bacteria : IEpidemic
    {
        private int _infectRate = 10;
        private int _fatalityRate = 10;
        private bool _isSkill = false;

        public int InfectRate { get { return _infectRate; } set { _infectRate = value; } }
        public int FatalityRate { get { return _fatalityRate; } set { _fatalityRate = value; } }
        public bool IsSkill { get { return _isSkill; } set { _isSkill = value; } }

        public void EnforceSkill()
        {
            if (IsSkill == false)
            {
                this.FatalityRate *= 2;
            }
        }

        public override void UseSkill()
        {
            EnforceSkill();
        }

    }

    


}
