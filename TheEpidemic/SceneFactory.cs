using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheEpidemic
{
    public interface ISceneFactory
    {
        IScene CreateScene();
    }

    public class FirstSceneFactory : ISceneFactory
    {
        public IScene CreateScene()
        {
            return new FirstScene();
        }
    }

    public class ChoiceSceneFactory : ISceneFactory
    {
        public IScene CreateScene()
        {
            return new ChoiceScene();
        }
    }

    public class GameSceneFactory : ISceneFactory
    {
        public IScene CreateScene()
        {
            return new GameScene();
        }
    }
}
