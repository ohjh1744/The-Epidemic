using TheEpidemic;
using System.Text;

namespace TheEpidemic
{
    //게임의 모든 데이터들을 관리하고 그 데이터를 보여주는 권한을 가진 관리자역할.

    public class GameManager 
    {
        // 싱글톤 패턴 을 위한 정적변수
        private static GameManager _instance;
        // Player가 선택한 전염병 이름
        private string _name;
        //현재 전염률
        private int _infectRate;
        //현재 치사율
        private int _fatalityRate;
        //현재 치료제 개발율
        private int _cure;
        // 세계 지도 맵
        private int[,] _map;
        // 감염자
        private int _infected;
        // 사망자
        private int _death;
        // 총 인구수
        private int _human;
        // 생존자
        private int _survivor;
        // 날짜
        private int _day;
        // 현재 골드
        private int _gold;
        // 전염률 증가를 위해 필요한 골드
        private int _upgradeGoldForInfect;
        // 치사율 증가를 위해 필요한 골드
        private int _upgradeGoldForFatality;

        // Player와 Global에서 업데이트된 내용에 따라 GameManager의 데이터들도 Update하기위한 콜백함수. -> 옵저버패턴
        public event Action Update;

        // 초기화
        public GameManager()
        {      
            _name = "";
            _infectRate = 0;
            _fatalityRate = 0;
            _cure = 0;
            _map = new int[,]
            {
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0 },
                {0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 1, 1, 1, 1, 0 },
                {0, 0, 0, 1, 1, 1, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1 },
                {0, 0, 1, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1 },
                {1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1 },
                {0, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 0 },
                {0, 0, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 0, 0 },
                {0, 0, 0, 1, 1, 1, 1, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 0, 0 },
                {0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0 },
                {0, 0, 0, 0, 1, 0, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0 },
                {0, 0, 0, 1, 1, 0, 0, 1, 0, 0, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                {0, 0, 1, 1, 1, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 0, 0 },
                {0, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 0, 0 },
                {0, 0, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 0 },
                {0, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0 },
                {0, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0 },
                {0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0 },
                {0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0 },
                {0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0 },
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0 },
            };
            _infected = 0;
            _death = 0;
            _human = 0;

            for(int i = 0; i < 20; i++)
            {
                for(int j = 0; j < 30; j++)
                {
                    if (Map[i, j] == 1)
                    {
                        _human++;
                    }
                }
            }
            _survivor = _human;
            _day = 1;
            _gold = 0;
            _upgradeGoldForInfect = 20;
            _upgradeGoldForFatality = 20;

        }

        // 게임 메니저 싱글톤 객체로 사용.
        public static GameManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameManager();
                }
                return _instance;
            }
        }
        public string Name { get { return _name; } set { _name = value; } }
        public int InfectRate { get { return _infectRate; } set { _infectRate = value; } }
        public int FatalityRate { get { return _fatalityRate; } set { _fatalityRate = value; } }
        public int Cure { get { return _cure; } set { _cure = value; } }
        public int[,] Map { get { return _map; } set { _map = value; } }
        public int Infected { get { return _infected; } set { _infected = value; } }
        public int Death { get { return _death; } set { _death = value; } }
        public int Human { get { return _human; } set { _human = value; } }
        public int Survivor { get { return _survivor; } set { _survivor = value; } }
        public int Day { get { return _day; } set { _day = value; } }
        public int Gold { get { return _gold; } set { _gold = value; } }
        public int UpgradeGoldForInfect { get { return _upgradeGoldForInfect; } set { _upgradeGoldForInfect = value; } }
        public int UpgradeGoldForFatality { get { return _upgradeGoldForFatality; } set { _upgradeGoldForFatality = value; } }
       
        // 현재 정보 출력
        public void Show()
        {
            Console.WriteLine($"----------------------------------------------------------");
            Console.WriteLine($"Day: {_day}");
            Console.WriteLine($"전염병: {_name}        생존자: {_survivor}");
            Console.WriteLine($"전염률: {_infectRate}               전염수: {_infected}");
            Console.WriteLine($"치사율: {_fatalityRate}               사망수: {_death}");
            Console.WriteLine($"치료제 개발율: {_cure}");
            Console.WriteLine($"----------------------------------------------------------");
        }
        // 콜백함수 옵저버 패턴. Player와 Global에서 각자 상태를 Update함에 따라 게임메니저에도 전달되어 상태 저장.
        public void StartUpdate()
        {
            Update?.Invoke();
            Update = null;
        }

        public void Reset()
        {
            _infected = 0;
            _death = 0;
            _survivor = Human;
        }


    }
}
