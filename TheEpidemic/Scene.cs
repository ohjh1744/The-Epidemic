using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TheEpidemic
{
    public interface IScene
    {
        public void Render();
        public  void Input();
        public void Update();

        public bool FinishScene { get; set; }

    }

    public interface IAwake
    {
        public void Awake(IGameManager gameManager);
    }

    public class FirstScene : IScene
    {
        private bool _finishScene = false;
        public void Render()
        {
            Console.Clear();
            Console.WriteLine("#######################################");
            Console.WriteLine("#        전염병 퍼트리기              #");
            Console.WriteLine("#                                     #");
            Console.WriteLine("#######################################");
            Console.WriteLine("게임설명: 질병을 전세계에 퍼트리세요!");
            Console.WriteLine("게임설명: 치료제가 완성하기전에 전세계의 인구를 멸종시키면 당신의 승리입니다.");
            Console.Write("게임을 하실거면 엔터를 눌러주세요.: ");

        }

        public void Input()
        {
            Console.ReadLine();
        }

        public void Update()
        {
            FinishScene = true;
        }

       public bool FinishScene{ get { return _finishScene; } set{ _finishScene = value; } }

 
    }

    public class ChoiceScene:IScene, IAwake
    {
        private bool _finishScene = false;
        private IGameManager _gameManager;
        private int _numEpidemic;

        public void Awake(IGameManager gameManager)
        {
            _gameManager = gameManager;
        }
        public void Render()
        {
            Console.Clear();
            Console.WriteLine("#################################");
            Console.WriteLine("#          전염병 선택          #");
            Console.WriteLine("#################################");
            Console.WriteLine("1. 박테리아");
            Console.WriteLine("전염률: 3");
            Console.WriteLine("치사률: 2");
            Console.WriteLine("스킬:  이틀동안 치사율 두 배 상승, 쿨타임: 4일");
            Console.WriteLine("#################################");
            Console.WriteLine("2. 바이러스");
            Console.WriteLine("전염률: 5");
            Console.WriteLine("치사률: 1");
            Console.WriteLine("스킬:  이틀 동안 전염률 두 배 상승, 쿨타임: 4일");
            Console.WriteLine("#################################");
            Console.WriteLine("원하는 전염병을 선택해주세요.(잘못입력시 재입력)");
        }

        public void Input()
        {
            do
            {

            } while (int.TryParse(Console.ReadLine(), out _numEpidemic) == false || _numEpidemic < 1 || _numEpidemic > 2);
        }

        public void Update()
        {
            _gameManager.Epidemic = _gameManager.Epidemics[_numEpidemic-1];
            FinishScene = true;
        }
        public bool FinishScene { get { return _finishScene; } set { _finishScene = value; } }

    }

    public class GameScene : IScene, IAwake
    {
        private bool _finishScene = false;
        private IGameManager _gameManager;
        private int _numInput;
        private IPlayer _player;
        private IGlobal _global;

        public void Awake(IGameManager gameManager)
        {
            _gameManager = gameManager;
            _player = new Player();
            _global = gameManager.Global;
        }
        public void Render()
        {
            Console.Clear();
            _gameManager.Reset();
            for (int i = 0; i < 20; i++)
            {
                for(int j = 0; j < 30; j++)
                {
                    if (_gameManager.Map[i,j] == 0)
                    {
                        Console.Write("□");
                    }
                    else if (_gameManager.Map[i,j] == 1)
                    {
                        Console.Write("■");
                    }
                    else if (_gameManager.Map[i, j] == 2)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("■");
                        Console.ResetColor();
                        _gameManager.Infected++;
                    }
                    else if (_gameManager.Map[i, j] == 3)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("■");
                        Console.ResetColor();
                        _gameManager.Death++;
                        _gameManager.Survivor--;
                    }
                }
                Console.WriteLine();
            }
            _global.FindEpidemic(_gameManager);
            _gameManager.Show();

        }

        public void Input()
        {
            do
            {
                Console.WriteLine("---------------------------------------------------------------------------------");
                Console.WriteLine($"하고 싶은 행동을 고르세요. (잘못입력시 재입력)       보유골드: {_player.Gold}G");
                Console.WriteLine($"1. 전염률 증가({_player.GoldUpInfectRate}G): ");
                Console.WriteLine($"2. 치사율 증가({_player.GoldUpFatalityRate}G): ");
                Console.WriteLine($"3. 스킬 사용 (쿨타임 {_gameManager.Epidemic.BuffWaitTime}일 남았습니다.)");
                Console.WriteLine("4. 다음 날로 넘어가기");
                Console.WriteLine("---------------------------------------------------------------------------------");
            } while (int.TryParse(Console.ReadLine(), out _numInput) == false || _numInput < 0 || _numInput > 4);
        }

        public void Update()
        {
            switch (_numInput)
            {
                case 1:
                    _player.UpInfectRate(_gameManager);
                    break;
                case 2:
                    _player.UpFatalityRate(_gameManager);
                    break;
                case 3:
                    _player.UseSkill(_gameManager);
                    break;
                case 4:
                    _player.Next(_gameManager);
                    _global.UpCure();
                    break;
            }
            GameFinish();
        }

        public void GameFinish()
        {
           if(_global.Cure >= 100)
           {
                if (_gameManager.Survivor <= 0)
                {
                    Console.WriteLine("전 세계 인구를 멸종시켰습니다. 축하드립니다!");
                }
                else
                {
                    Console.WriteLine("인구가 치료제를 완성시켰습니다. 패배했습니다.");
                }
                FinishScene = true;
            }
        }

        public bool FinishScene { get { return _finishScene; } set { _finishScene = value; } }
    }
}
