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

        }

        public void Input()
        {

        }

        public void Update()
        {

        }

       public bool FinishScene{ get { return _finishScene; } set{ _finishScene = value; } }

 
    }

    public class ChoiceScene:IScene, IAwake
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
