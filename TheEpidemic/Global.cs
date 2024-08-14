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
        
        void DevelopRemedy();
        void FindEpidemic(IGameManager gameManager);
    }

    public class Global : IGlobal
    {
        int _cure = 0;
        int _developRate = 0;
        bool _isFindEpidemic = false;

        public int Cure { get { return _cure; } set { _cure = value; } }
        public int DevelopRate { get { return _developRate; } set { _developRate = value; } }

        public void DevelopRemedy()
        {
            if (_isFindEpidemic)
            {
                Random random = new Random();
                _developRate = random.Next(0, 10);
                Cure += _developRate;
            }
        }

        public void FindEpidemic(IGameManager gameManager)
        {
            if (gameManager.Death > 0)
            {
                Console.WriteLine("세계 정부가 질병을 발견했습니다.! 치료제 개발을 시작합니다.");
                _isFindEpidemic = true;
            }
        }


    }
}
