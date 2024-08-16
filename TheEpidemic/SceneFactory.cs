namespace TheEpidemic
{
    //씬 팩토리 메서드
    public interface ISceneFactory
    {
        Scene Create();
    }

    public class FirstSceneFactory : ISceneFactory
    {
        public Scene Create()
        {
            return new FirstScene();
        }
    }

    public class ChoiceSceneFactory : ISceneFactory
    {
        public Scene Create()
        {
            return new ChoiceScene();
        }
    }

    public class GameSceneFactory : ISceneFactory
    {
        public Scene Create()
        {
            return new GameScene();
        }
    }
}
