using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheEpidemic
{
    public interface ISceneFactory
    {
        IScene CreateScene(int num);
    }

    public class FirstSceneFactory : ISceneFactory
    {
        public IScene CreateScene(int num)
        {
            if(num == 1)
            {
                return new FirstScene();
            }
            else
            {
                throw new InvalidOperationException("숫자 1만 반환합니다.");
            }
        }
    }

    public class ChoiceSceneFactory : ISceneFactory
    {
        public IScene CreateScene(int num)
        {
            if (num == 2)
            {
                return new ChoiceScene();
            }
            else
            {
                throw new InvalidOperationException("숫자 2만 반환합니다.");
            }
        }
    }

    public class GameSceneFactory : ISceneFactory
    {
        public IScene CreateScene(int num)
        {
            if (num == 3)
            {
                return new GameScene();
            }
            else
            {
                throw new InvalidOperationException("숫자 3만 반환합니다.");
            }
        }
    }
}
