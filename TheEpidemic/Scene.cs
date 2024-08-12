using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheEpidemic
{
    public interface IScene
    {
        public void Awake();
        public void Render();
        public  void Input();
        public void Update();
    }

    public interface IAwake
    {
        public void Start(IGameManager gameManager, IEpidemic epidemic);
    }

    public class FirstScene: IScene
    {
        public void Awake()
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
    }

    public class ChoiceScene:IScene, IAwake
    {
        public void Awake()
        {

        }
        public void Start(IGameManager gameManager, IEpidemic epidemic)
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
    }

    public class GameScene : IScene, IAwake
    {
        public void Awake()
        {

        }
        public void Start(IGameManager gameManager, IEpidemic epidemic)
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
    }
}
