using System;
using System.Collections.Generic;
using System.Linq;
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
        public void Awake(IGameManager gameManager, IEpidemic epidemic);
    }

    public class FirstScene : IScene
    {
        private bool _finishScene = false;
        public void Render()
        {
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

        public void Awake(IGameManager gameManager, IEpidemic epidemic)
        {
            _gameManager = gameManager;
            _epidemic = epidemic;
        }
        public void Render()
        {
            Console.WriteLine("#################################");
            Console.WriteLine("#          전염병 선택          #");
            Console.WriteLine("#################################");
            Console.WriteLine("1. 바이러스");
            Console.WriteLine("전염률: 20");
            Console.WriteLine("치사률:  5");
            Console.WriteLine("스킬:  이틀동안 전염률 두 배 상승");
            Console.WriteLine("#################################");
            Console.WriteLine("2. 박테리아");
            Console.WriteLine("전염률: 10");
            Console.WriteLine("치사률: 10");
            Console.WriteLine("스킬:  이틀동안 치사율 두 배 상승");
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
            _gameManager.Epidemic = _gameManager.Epidemics[_numEpidemic];
            FinishScene = true;
        }
        public bool FinishScene { get { return _finishScene; } set { _finishScene = value; } }

    }

    public class GameScene : IScene, IAwake
    {
        private bool _finishScene = false;
        public void Awake(IGameManager gameManager, IEpidemic epidemic)
        {

        }
        public void Render()
        {

        }

        public void Input()
        {

        }

        public void Update()
        {

        }

        public bool FinishScene { get { return _finishScene; } set { _finishScene = value; } }
    }
}
