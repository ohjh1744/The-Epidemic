﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheEpidemic
{
    public interface ISceneFactory
    {
        Scene CreateScene();
    }

    public class FirstSceneFactory : ISceneFactory
    {
        public Scene CreateScene()
        {
            return new FirstScene();
        }
    }

    public class ChoiceSceneFactory : ISceneFactory
    {
        public Scene CreateScene()
        {
            return new ChoiceScene();
        }
    }

    public class GameSceneFactory : ISceneFactory
    {
        public Scene CreateScene()
        {
            return new GameScene();
        }
    }
}
