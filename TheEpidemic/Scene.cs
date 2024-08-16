namespace TheEpidemic
{
    // Scene은 공통적으로 ,Render, Input, Update함수를 가지고 있고, 씬의 역할이 다하면, FinishScene을 true시켜 
    // Main함수에서 전달 받아 다음씬으로 이동.
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

    // Player의 정보(클래스)가 필요한 씬을 위해서 따로 인터페이스를 빼놔 Player 클래스 사용.
    public interface IAwake
    {
        public void Awake(Player player );
    }

    //첫번째 씬
    public class FirstScene : Scene
    {
        //메인화면씬
        public override void Render()
        {
            Console.Clear();
            Console.WriteLine("#######################################################################");
            Console.WriteLine("#                           전염병 주식회사                           #");
            Console.WriteLine("#                                                                     #");
            Console.WriteLine("#######################################################################");
            Console.WriteLine("# 게임설명:                                                           #");
            Console.WriteLine("# 질병을 전세계에 퍼트리세요!                                         #");
            Console.WriteLine("# 치료제가 완성되기 전에 전세계의 인구를 멸종시키면 당신의 승리입니다.#");
            Console.WriteLine("#######################################################################");
            Console.Write("(게임을 하실거면 엔터를 눌러주세요.: )");

        }

        // 엔터 시 게임시작
        public override void Input()
        {
            Console.ReadLine();
        }

        // 현재씬 종료
        public override void Update()
        {
            FinishScene = true;
        }

    }

    //두번째 씬
    public class ChoiceScene : Scene, IAwake
    {
        private Player _player;
        private int _numEpidemic;

        // 메인함수에서 Player의 정보를 가져옴.
        public void Awake(Player player)
        {
            _player = player;
        }
        //전염병 선택씬
        public override void Render()
        {
            Console.Clear();
            Console.WriteLine("#######################################################");
            Console.WriteLine("#                     전염병 선택                     #");
            Console.WriteLine("#######################################################");
            Console.WriteLine($"#1. 박테리아                                          #");
            Console.WriteLine($"#전염률: 3                                            #");
            Console.WriteLine($"#치사률: 2                                            #");
            Console.WriteLine($"#스킬:  이틀동안 치사율 두 배 상승                    #");
            Console.WriteLine($"#쿨타임: 4                                            #");
            Console.WriteLine($"#지속시간:2                                           #");
            Console.WriteLine("#######################################################");
            Console.WriteLine($"#2. 바이러스                                          #");
            Console.WriteLine($"#전염률: 5                                            #");
            Console.WriteLine($"#치사률: 1                                            #");
            Console.WriteLine($"#스킬:  이틀동안 전염률 두 배 상승                    #");
            Console.WriteLine($"#쿨타임: 4                                            #");
            Console.WriteLine($"#지속시간:2                                           #");
            Console.WriteLine("#######################################################");
            Console.WriteLine($"#3. 코로나                                            #");
            Console.WriteLine($"#전염률: 3                                            #");
            Console.WriteLine($"#치사률: 3                                            #");
            Console.WriteLine($"#스킬:  이틀동안 치사율,전염률 랜덤상승 or 감소       #");
            Console.WriteLine($"#쿨타임: 3                                            #");
            Console.WriteLine($"#지속시간:2                                           #");
            Console.WriteLine("#######################################################");
            Console.WriteLine("원하는 전염병을 선택해주세요.(잘못입력시 재입력)");
        }

        // 원하는 전염병 선택
        public override void Input()
        {
            do
            { } while (int.TryParse(Console.ReadLine(), out _numEpidemic) == false || _numEpidemic < 1 || _numEpidemic > 3);
        }

        //EpidemicFactory(팩토리 메서드) 을 FactoryManager(싱글톤)의 딕셔너리에 따로 저장하여
        // 원하는 전염병 선택 후 Datamanager의 딕셔너리Player가 사용할 전염병 저장.
        public override void Update()
        {
            IEpidemicFactory epidemicFactory;
            _player.GetEpidemic(FactoryManager.Instance.CreateEpidemic((EpidemicType)_numEpidemic));
            FinishScene = true;
        }

    }

    //게임씬
    public class GameScene : Scene, IAwake
    {
        private Player _player;
        private Global _global;
        private GameManager _gameManager;
        private int _numInput;
        //생성자를 통해 Global클래스 및 게임매니저 사용
        public GameScene()
        {
            _global = new Global();
            _gameManager = GameManager.Instance;
        }
        // 전염병 선택한 Player정보 가져옴.
        public void Awake(Player player)
        {
            _player = player;
        }

        //게임씬 맵, 세계맵 그린 후, 전염병 발견 소식 및 게임메니저를 통해 감염자,죽은자,생존자, 날짜 등 정보 출력.
        public override void Render()
        {
            Console.Clear();
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 30; j++)
                {
                    if(_gameManager.Map[i, j] == 0)
                    {
                        Console.Write("□");
                    }
                    else if (_gameManager.Map[i, j] == 1)
                    {
                        Console.Write("■");
                    }
                    else if (_gameManager.Map[i, j] == 2)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("■");
                        Console.ResetColor();
                    }
                    else if (_gameManager.Map[i, j] == 3)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("■");
                        Console.ResetColor();
                    }
                }
                Console.WriteLine();
            }

            _global.FindEpidemic();
            _gameManager.Show();
            _gameManager.Reset();

        }

        //Player 원하는 행동 선택. 1. 전염률증가, 2. 치사율증가, 3. 전염병버프활용 4. 다음 날짜로 넘어가기
        public override void Input()
        {
            do
            {
                Console.WriteLine("---------------------------------------------------------------------------------");
                Console.WriteLine($"하고 싶은 행동을 고르세요. (잘못입력시 재입력)       보유골드: {_gameManager.Gold}G");
                Console.WriteLine($"1. 전염률 증가({_gameManager.UpgradeGoldForInfect}G): ");
                Console.WriteLine($"2. 치사율 증가({_gameManager.UpgradeGoldForFatality}G): ");
                Console.WriteLine($"3. 버프 사용 (쿨타임 {_player.Epidemic.BuffWaitTime}일 남았습니다.)");
                Console.WriteLine("4. 다음 날로 넘어가기");
                Console.WriteLine("---------------------------------------------------------------------------------");
            } while (int.TryParse(Console.ReadLine(), out _numInput) == false || _numInput < 0 || _numInput > 4);
        }

        // 원하는 선택 입력에 따라 행동 실천.
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
            // 행동 후 게임메니저에 정보 업데이트 콜백함수 활용(옵저버 패턴)
            _gameManager.StartUpdate();
            GameFinish();
        }

        //치료제(cure)이 100% 넘었거나, 생존자가 없다면 게임 종료.
        public void GameFinish()
        {

            if (_gameManager.Cure >= 100)
            {
                Render();
                Console.WriteLine("인구가 치료제를 완성시켰습니다. 패배.");
                FinishScene = true;
            }
            else if(_gameManager.Survivor == 0)
            {
                Render();
                Console.WriteLine("인류를 멸종시켰습니다. 승리!");
                FinishScene = true;
            }

        }

    }
}
