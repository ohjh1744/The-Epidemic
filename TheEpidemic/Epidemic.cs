using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheEpidemic
{
    public abstract class Epidemic
    {
        private string _name;
        private int _infectRate;
        private int _fatalityRate;
        private int _buffWaitTime;
        private int _buffDuration;
        private bool _isBuff;


        
        public string Name { get { return _name; } set { _name = value; } }
        public int InfectRate { get { return _infectRate; } set { _infectRate = value; } }
        public int FatalityRate { get { return _fatalityRate; } set { _fatalityRate = value; } }
        public int BuffWaitTime { get { return _buffWaitTime; } set { _buffWaitTime = value; } }
        public int BuffDuration { get { return _buffDuration; } set { _buffDuration = value; } }
        public bool IsBuff { get { return _isBuff; } set { _isBuff = value; } }

        public abstract void Buff();

        public abstract void DeBuff();


    }


    public class Virus : Epidemic
    {

        public Virus()
        {
            Name = "바이러스";
            InfectRate = 5;
            FatalityRate = 1;
            BuffWaitTime = 0;
            BuffDuration = 2;
            IsBuff = false;
        }
        

        public void EnforceSkill()
        {
            InfectRate *= 2;
            BuffWaitTime = 4;
        }

        public override void Buff()
        {
            IsBuff = true;
            EnforceSkill();
        }

        public override void DeBuff()
        {
            IsBuff = false;
            InfectRate /= 2;
            BuffDuration = 2;
        }


    }

    public class Bacteria : Epidemic
    {
        public Bacteria()
        {
            Name = "박테리아";
            InfectRate = 3;
            FatalityRate = 2;
            //쿨타임
            BuffWaitTime = 0;
            //지속시간
            BuffDuration = 2;
        }


        public void EnforceSkill()
        {
            FatalityRate *= 2;
            BuffWaitTime = 4;
        }

        public override void Buff()
        {
            IsBuff = true;
            EnforceSkill();
        }

        public override void DeBuff()
        {
            IsBuff = false;
            FatalityRate /= 2;
            BuffDuration = 2;
        }

    }

    


}
