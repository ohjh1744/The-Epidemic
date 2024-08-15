using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheEpidemic.ITouchStates;

namespace TheEpidemic 
{
    // Player와 상대하는 클래스로, 질병이 발견되면 치료제를 개발하는 클래스

    public class Global :ITouchCure
    {
        // 치료제 개발율
        private int _cure = 0;
        // 개발 속도
        private int _developRate = 0;
        // 질병 발견했는지 확인
        private bool _isFindEpidemic = false;

        public int Cure { get { return _cure; } set { _cure = value; } }
        public int DevelopRate { get { return _developRate; } set { _developRate = value; } }

        // 치료제 개발 -> 개발속도에 맞춰서
        public void UpdateCure()
        {
            Cure += _developRate;
            GameManager.Instance.Cure = Cure;
        }

        // 전염병이 발견되면 치료제 개발 시작. 개발속도는 매일 랜덤
        public void DevelopRemedy()
        {
            if (_isFindEpidemic)
            {
                Random random = new Random();
                _developRate = random.Next(1, 7);
                GameManager.Instance.Update += UpdateCure;
            }
        }

        // 전염병이 발견되면(전염병에 의해 죽은 사람이 발생하여 게임메니저에 상태가 Update되면) 소식 출력
        public void FindEpidemic( )
        {
            if (GameManager.Instance.Death > 0)
            {
                Console.WriteLine($"----------------------------------------------------------");
                Console.WriteLine("세계 정부가 질병을 발견했습니다.! 치료제 개발을 시작합니다.");
                _isFindEpidemic = true;
            }
        }



    }
}
