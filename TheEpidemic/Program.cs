namespace TheEpidemic
{
    internal class Program
    {
        static void ResetGameManager(IGameManager gameManager)
        {
            IEpidemicFactory epidemicFactory;
            Epidemic epidemic;

            epidemicFactory = new BacteriaFactory();
            epidemic = epidemicFactory.Create();
            gameManager.Epidemics.Add(epidemic);

            epidemicFactory = new VirusFactory();
            epidemic = epidemicFactory.Create();
            gameManager.Epidemics.Add(epidemic);
        }
        
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

        static void PlayGame(Scene[] scenes, IGameManager gameManager)
        {
            int sceneNum = 0;
            while (sceneNum <= 2)
            {
                if (scenes[sceneNum] is IAwake)
                {
                    (scenes[sceneNum] as IAwake).Awake(gameManager);
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
            IGameManager gameManager = new GameManager();
            Scene[] scenes = new Scene[3];

            ResetGameManager(gameManager);
            ResetScenes(scenes);
            PlayGame(scenes, gameManager);
        }
    }
}
