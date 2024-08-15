namespace TheEpidemic
{
    internal class Program
    {   
        // 전염병 주식회사
        // 게임 설명: 모은 인류를 멸종시키면 승리, 치료제가 나오기 전에 멸종시켜야함.
        // 게임 순서: 총 3개의 씬으로, 메인화면씬 ->  전염병 선택씬 -> 게임씬
        
        //SceneFactory를 활용해 (팩토리 메서드)
        // 메인화면, 전염병 선택신, 게임씬 3개의 씬 생성.
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

        // 3개의 씬을 순서대로 진행.
        // 2번째씬부터는 Player클래스가 필요하므로 IAwake인터페이스를 통해 Player데이터 사용.
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
