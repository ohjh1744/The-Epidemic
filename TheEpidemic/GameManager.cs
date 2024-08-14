using TheEpidemic;

namespace TheEpidemic
{
    //게임의 모든 데이터들을 관리하고 보여주는 관리자 인터페이스 
    public interface IGameManager
    {
        //전염병 종류
        List<Epidemic> Epidemics { get; set; }
        // Player가 고른 전염병
        Epidemic Epidemic { get; set; }
        //Global
        IGlobal Global { get; set; }
        //맵 상태 1: 전염, 2: 죽음
        int[,] Map { get; set; }
        // 현재 전염수
        int Infected { get; set; }
        //세계인구수
        int Human { get; set; }
        // 현재 죽음수
        int Death { get; set; }
        //현재 살아남은 인구 수
        int Survivor { get; set; }
        // Day날짜
        int Day { get; set; }
        int Gold { get; set; }
        int UpgradeGoldForInfect { get; set; }
        int UpgradeGoldForFatality { get; set; }
        // 상태 출력
        void Show();
        void Reset();

    }

    public class GameManager : IGameManager
    {
        private List<Epidemic> _epidemics;
        private Epidemic _epidemic;
        private IGlobal _global;
        private int[,] _map;
        private int _infected;
        private int _death;
        private int _human;
        private int _survivor;
        private int _day;
        private int _gold;
        private int _upgradeGoldForInfect;
        private int _upgradeGoldForFatality;



        public GameManager()
        {
            _epidemics = new List<Epidemic>();
            _epidemic = new Virus();
            _global = new Global();
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

        public List<Epidemic> Epidemics { get { return _epidemics; } set { _epidemics = value; } }
        public Epidemic Epidemic { get { return _epidemic; } set { _epidemic = value; } }
        public IGlobal Global { get { return _global; } set { _global = value; } }
        public int[,] Map { get { return _map; } set { _map = value; } }
        public int Infected { get { return _infected; } set { _infected = value; } }
        public int Death { get { return _death; } set { _death = value; } }
        public int Human { get { return _human; } set { _human = value; } }
        public int Survivor { get { return _survivor; } set { _survivor = value; } }
        public int Day { get { return _day; } set { _day = value; } }
        public int Gold { get { return _gold; } set { _gold = value; } }
        public int UpgradeGoldForInfect { get { return _upgradeGoldForInfect; } set { _upgradeGoldForInfect = value; } }
        public int UpgradeGoldForFatality { get { return _upgradeGoldForFatality; } set { _upgradeGoldForFatality = value; } }
        public void Show()
        {
            Console.WriteLine($"----------------------------------------------------------");
            Console.WriteLine($"Day: {_day}");
            Console.WriteLine($"전염병: {_epidemic.Name}        생존자: {_survivor}");
            Console.WriteLine($"전염률: {_epidemic.InfectRate}               전염수: {_infected}");
            Console.WriteLine($"치사율: {_epidemic.FatalityRate}               사망수: {_death}");
            Console.WriteLine($"치료제 개발율: {_global.Cure}");
            Console.WriteLine($"----------------------------------------------------------");
        }

        public void Reset()
        {
            _infected = 0;
            _death = 0;
            _survivor = Human;
        }


    }
}
