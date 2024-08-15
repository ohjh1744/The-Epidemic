namespace TheEpidemic
{
    //씬 팩토리 메서드
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
