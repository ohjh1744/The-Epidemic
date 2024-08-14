namespace TheEpidemic
{
    internal class Program
    {   
        static void ResetScenes(Scene[] scenes)
        {
            ISceneFactory sceneFactory;
            Scene scene;

            sceneFactory = new FirstSceneFactory();
            scene = sceneFactory.CreateScene();
            scenes[0] = scene;

            sceneFactory = new ChoiceSceneFactory();
            scene = sceneFactory.CreateScene();
            scenes[1] = scene;

            sceneFactory = new GameSceneFactory();
            scene = sceneFactory.CreateScene();
            scenes[2] = scene;

        }

        static void PlayGame(Scene[] scenes, Player player)
        {
            int sceneNum = 0;
            while (sceneNum <= 2)
            {
                if (scenes[sceneNum] is IAwake)
                {
                    (scenes[sceneNum] as IAwake).Awake(player);
                }
                while (scenes[sceneNum].FinishScene == false)
                {
                    scenes[sceneNum].Render();
                    scenes[sceneNum].Input();
                    scenes[sceneNum].Update();
                }
                sceneNum++;
            }
        }

        static void Main(string[] args)
        {
            Player player = new Player();
            Scene[] scenes = new Scene[3];
            ResetScenes(scenes);
            PlayGame(scenes, player);
        }
    }
}
