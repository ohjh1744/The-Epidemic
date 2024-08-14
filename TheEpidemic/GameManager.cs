using TheEpidemic;
using System.Text;

namespace TheEpidemic
{
    //게임의 모든 데이터들을 관리하고 보여주는 관리자 인터페이스 

    public class GameManager 
    {
        private static GameManager _instance;
        private string _name;
        private int _infectRate;
        private int _fatalityRate;
        private int _cure;
        private int[,] _map;
        private int _infected;
        private int _death;
        private int _human;
        private int _survivor;
        private int _day;
        private int _gold;
        private int _upgradeGoldForInfect;
        private int _upgradeGoldForFatality;

        public event Action Update;


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
