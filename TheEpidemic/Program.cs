namespace TheEpidemic
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IGameManager gameManager = new GameManager();
            IEpidemicFactory epidemicFactory;

            ISceneFactory sceneFactory = new FirstSceneFactory();
            IScene scene1 = sceneFactory.CreateScene(1);

            int sceneNum = 0;
            while(sceneNum < 3)
            {

            }


        }
    }
}
