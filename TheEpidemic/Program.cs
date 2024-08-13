namespace TheEpidemic
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 초기화.
            IGameManager gameManager = new GameManager();
            IEpidemicFactory epidemicFactory = new BacteriaFactory();
            IEpidemic epidemic = epidemicFactory.Create(1);

            ISceneFactory sceneFactory = new FirstSceneFactory();
            IScene scene1 = sceneFactory.CreateScene();

            ISceneFactory sceneFactory2 = new ChoiceSceneFactory();
            IScene scene2 = sceneFactory.CreateScene();

            ISceneFactory sceneFactory3 = new GameSceneFactory();
            IScene scene3 = sceneFactory.CreateScene();


            IScene[] scenes = new IScene[3];
            scenes[0] = scene1;
            scenes[1] = scene2;
            scenes[2] = scene3;

            int sceneNum = 1;
            while(sceneNum <= 3)
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
