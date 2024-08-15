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
        //전염병 이름
        private string _name;
        //전염률
        private int _infectRate;
        //치사율
        private int _fatalityRate;
        //버프쿨타임
        private int _buffWaitTime;
        //버프지속시간
        private int _buffDuration;
        //버프활용했는지 체크
        private bool _isBuff;


        public string Name { get { return _name; } set { _name = value; } }
        public int InfectRate { get { return _infectRate; } set { _infectRate = value; } }
        public int FatalityRate { get { return _fatalityRate; } set { _fatalityRate = value; } }
        public int BuffWaitTime { get { return _buffWaitTime; } set { _buffWaitTime = value; } }
        public int BuffDuration { get { return _buffDuration; } set { _buffDuration = value; } }
        public bool IsBuff { get { return _isBuff; } set { _isBuff = value; } }

        //버프 on함수
        public abstract void Buff();

        //버프 off함수
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
        
        // 바이러스 버프는 전염률 2배 증가
        private void EnforceSkill()
        {
            InfectRate *= 2;
        }
        
        // 버프 쿨타임 4일
        public override void Buff()
        {
            IsBuff = true;
            EnforceSkill();
            BuffWaitTime = 4;
        }

        // 버프 지속시간 2일
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
            BuffWaitTime = 0;
            BuffDuration = 2;
            IsBuff = false;
        }

        // 박테리아 버프는 치사율 2배 증가
        private void EnforceSkill()
        {
            FatalityRate *= 2;
        }

        //버프 쿨타임 4일
        public override void Buff()
        {
            IsBuff = true;
            EnforceSkill();
            BuffWaitTime = 4;
        }

        //버프 지속시간 2일
        public override void DeBuff()
        {
            IsBuff = false;
            FatalityRate /= 2;
            BuffDuration = 2;
        }

    }

    public class Corona : Epidemic
    {
        int randomInfect;
        int randomFatality;
        public Corona()
        {
            Name = "코로나";
            InfectRate = 3;
            FatalityRate = 3;
            BuffWaitTime = 0;
            BuffDuration = 2;
            IsBuff = false;
        }

        // 코로나 버프는 능력치 랜덤 조정 마이너스가 될수도, 플러스가 될수도
        private void EnforceSkill()
        {
            Random random = new Random();
            randomInfect = random.Next(-10, 21);
            randomFatality = random.Next(-10, 21);
            InfectRate += randomInfect;
            FatalityRate += randomFatality;
        }

        //버프 쿨타임 4일
        public override void Buff()
        {
            IsBuff = true;
            EnforceSkill();
            BuffWaitTime = 4;
        }

        //버프 지속시간 2일
        public override void DeBuff()
        {
            IsBuff = false;
            InfectRate -= randomInfect;
            FatalityRate -= randomFatality;
            BuffDuration = 2;
        }

    }







}
