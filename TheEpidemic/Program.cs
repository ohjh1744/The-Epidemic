namespace TheEpidemic
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IGameManager gameManager;
            IEpidemicFactory epidemicFactory;
            IEpidemic epidemic;
            ISceneFactory sceneFactory;
            IScene scene;
            IScene[] scenes = new IScene[3];

            // 초기화.
            gameManager = new GameManager();

            epidemicFactory = new BacteriaFactory();
            epidemic = epidemicFactory.Create(1);
            gameManager.Epidemics.Add(epidemic);

            epidemicFactory = new VirusFactory();
            epidemic = epidemicFactory.Create(2);
            gameManager.Epidemics.Add(epidemic);

            sceneFactory = new FirstSceneFactory();
            scene = sceneFactory.CreateScene();
            scenes[0] = scene;

            sceneFactory = new ChoiceSceneFactory();
            scene = sceneFactory.CreateScene();
            scenes[1] = scene;

            sceneFactory = new GameSceneFactory();
            scene = sceneFactory.CreateScene();
            scenes[2] = scene;


            int sceneNum = 1;
            while (sceneNum <= 3)
            {
                if (scenes[sceneNum] is IAwake)
                {
                    (scenes[sceneNum] as IAwake).Awake(gameManager, epidemic);
                }
                while (scenes[sceneNum].FinishScene == false)
                {
                    scenes[sceneNum].Render();
                    scenes[sceneNum].Input();
                    scenes[sceneNum].Update();
                }
            }




        }
    }
}
