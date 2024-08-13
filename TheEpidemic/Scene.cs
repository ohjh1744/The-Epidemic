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
            Console.WriteLine("전염률: 10");
            Console.WriteLine("치사률: 10");
            Console.WriteLine("스킬:  이틀동안 치사율 두 배 상승");
            Console.WriteLine("#################################");
            Console.WriteLine("2. 바이러스");
            Console.WriteLine("전염률: 20");
            Console.WriteLine("치사률:  5");
            Console.WriteLine("스킬:  이틀동안 전염률 두 배 상승");
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
        private int _numEpidemic;
        private IPlayer player;

        public void Awake(IGameManager gameManager)
        {
            _gameManager = gameManager;
            player = new Player(gameManager.Epidemic);
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
                    }
                }
                Console.WriteLine();
            }
            _gameManager.Show();

        }

        public void Input()
        {
            do
            {
                Console.WriteLine("하고 싶은 행동을 고르세요. (잘못입력시 재입력)");
                Console.WriteLine("1. 전염률 증가: ");
                Console.WriteLine("2. 치사율 증가: ");
                Console.WriteLine("3. 다음 날로 넘어가기");
            } while (int.TryParse(Console.ReadLine(), out _numEpidemic) == false || _numEpidemic < 0 || _numEpidemic > 3);
        }

        public void Update()
        {
            switch (_numEpidemic)
            {
                case 1:
                    player.UpInfectRate();
                    break;
                case 2:
                    player.UpFatalityRate();
                    break;
                case 3:
                    player.Next(_gameManager);
                    break;
                default:
                    break;
            }
        }

        public bool FinishScene { get { return _finishScene; } set { _finishScene = value; } }
    }
}
