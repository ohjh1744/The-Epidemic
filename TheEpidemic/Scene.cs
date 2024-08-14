namespace TheEpidemic
{
    public abstract class Scene
    {
        private bool _finishScene;

        public Scene()
        {
            _finishScene = false;
        }
        public abstract void Render();
        public abstract void Input();
        public abstract void Update();
        public bool FinishScene { get { return _finishScene; } set { _finishScene = value; } }

    }

    public interface IAwake
    {
        public void Awake(Player player );
    }

    public class FirstScene : Scene
    {
        public override void Render()
        {
            Console.Clear();
            Console.WriteLine("#######################################");
            Console.WriteLine("#        전염병 퍼트리기              #");
            Console.WriteLine("#                                     #");
            Console.WriteLine("#######################################");
            Console.WriteLine("게임설명: ");
            Console.WriteLine("질병을 전세계에 퍼트리세요!");
            Console.WriteLine("치료제가 완성하기전에 전세계의 인구를 멸종시키면 당신의 승리입니다.");
            Console.Write("(게임을 하실거면 엔터를 눌러주세요.: )");

        }

        public override void Input()
        {
            Console.ReadLine();
        }

        public override void Update()
        {
            FinishScene = true;
        }

    }

    public class ChoiceScene : Scene, IAwake
    {
        private Player _player;
        private int _numEpidemic;

        public void Awake(Player player)
        {
            _player = player;
        }
        public override void Render()
        {
            Console.Clear();
            Console.WriteLine("#####################################################");
            Console.WriteLine("#                     전염병 선택                   #");
            Console.WriteLine("#####################################################");
            Console.WriteLine($"#1. 박테리아                                         #");
            Console.WriteLine($"#전염률: 3                                           #");
            Console.WriteLine($"#치사률: 2                                           #");
            Console.WriteLine($"#스킬:  이틀동안 치사율 두 배 상승                   #");
            Console.WriteLine($"#쿨타임: 4                                           #");
            Console.WriteLine($"#지속시간:2                                          #");
            Console.WriteLine("#####################################################");
            Console.WriteLine($"#2. 바이러스                                         #");
            Console.WriteLine($"#전염률: 5                                           #");
            Console.WriteLine($"#치사률: 1                                           #");
            Console.WriteLine($"#스킬:  이틀동안 전염률 두 배 상승                   #");
            Console.WriteLine($"#쿨타임: 4                                           #");
            Console.WriteLine($"#지속시간:2                                          #");
            Console.WriteLine("#####################################################");
            Console.WriteLine("원하는 전염병을 선택해주세요.(잘못입력시 재입력)");
        }

        public override void Input()
        {
            do
            { } while (int.TryParse(Console.ReadLine(), out _numEpidemic) == false || _numEpidemic < 1 || _numEpidemic > 2);
        }

        public override void Update()
        {
            IEpidemicFactory epidemicFactory;
            switch (_numEpidemic)
            {
                case 1:
                    epidemicFactory = new BacteriaFactory();
                    _player.GetEpidemic(epidemicFactory.Create());
                    break;
                case 2:
                    epidemicFactory = new VirusFactory();
                    _player.GetEpidemic(epidemicFactory.Create());
                    break;
                default:
                    break;
            }
            FinishScene = true;
        }

    }

    public class GameScene : Scene, IAwake
    {
        private Player _player;
        private Global _global;
        private int _numInput;

        public GameScene()
        {
            _global = new Global();
        }
        public void Awake(Player player)
        {
            _player = player;
        }

        public override void Render()
        {
            Console.Clear();
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 30; j++)
                {
                    if(GameManager.Instance.Map[i, j] == 0)
                    {
                        Console.Write("□");
                    }
                    else if (GameManager.Instance.Map[i, j] == 1)
                    {
                        Console.Write("■");
                    }
                    else if (GameManager.Instance.Map[i, j] == 2)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("■");
                        Console.ResetColor();
                    }
                    else if (GameManager.Instance.Map[i, j] == 3)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("■");
                        Console.ResetColor();
                    }
                }
                Console.WriteLine();
            }

            _global.FindEpidemic();
            GameManager.Instance.Show();
            GameManager.Instance.Reset();

        }

        public override void Input()
        {
            do
            {
                Console.WriteLine("---------------------------------------------------------------------------------");
                Console.WriteLine($"하고 싶은 행동을 고르세요. (잘못입력시 재입력)       보유골드: {GameManager.Instance.Gold}G");
                Console.WriteLine($"1. 전염률 증가({GameManager.Instance.UpgradeGoldForInfect}G): ");
                Console.WriteLine($"2. 치사율 증가({GameManager.Instance.UpgradeGoldForFatality}G): ");
                Console.WriteLine($"3. 스킬 사용 (쿨타임 {_player.Epidemic.BuffWaitTime}일 남았습니다.)");
                Console.WriteLine("4. 다음 날로 넘어가기");
                Console.WriteLine("---------------------------------------------------------------------------------");
            } while (int.TryParse(Console.ReadLine(), out _numInput) == false || _numInput < 0 || _numInput > 4);
        }

        public override void Update()
        {
            switch (_numInput)
            {
                case 1:
                    _player.UpdateInfectRate();
                    break;
                case 2:
                    _player.UpdateFatalityRate();
                    break;
                case 3:
                    _player.UseSkill();
                    break;
                case 4:                    
                    _player.Next();
                    _global.DevelopRemedy();
                    break;
            }
            GameManager.Instance.StartUpdate();
            GameFinish();
        }

        public void GameFinish()
        {

            if (GameManager.Instance.Cure >= 100)
            {
                Render();
                Console.WriteLine("인구가 치료제를 완성시켰습니다. 패배.");
                FinishScene = true;
            }
            else if(GameManager.Instance.Survivor == 0)
            {
                Render();
                Console.WriteLine("인류를 멸종시켰습니다. 승리!");
                FinishScene = true;
            }

        }

    }
}
