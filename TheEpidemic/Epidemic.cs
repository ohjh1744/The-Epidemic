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
        private bool _isSkill;

        
        public string Name { get { return _name; } set { _name = value; } }
        public int InfectRate { get { return _infectRate; } set { _infectRate = value; } }
        public int FatalityRate { get { return _fatalityRate; } set { _fatalityRate = value; } }
        public bool IsSkill { get { return _isSkill; } set { _isSkill = value; } }

        public abstract void UseSkill();


    }


    public class Virus : Epidemic
    {

        public Virus()
        {
            Name = "바이러스";
            InfectRate = 20;
            FatalityRate = 5;
            IsSkill = false;
        }
        

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

    public class Bacteria : Epidemic
    {
        public Bacteria()
        {
            Name = "박테리아";
            InfectRate = 10;
            FatalityRate = 10;
            IsSkill = false;
        }


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
