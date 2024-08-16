namespace TheEpidemic
{

    // 개인적으로 아쉬운 부분
    // Choice Scene Render부분에서 직접 문자를 쳐서 출력하지 않고
    // 따로 정보를 저장해 출력하는식으로 표현하면 좋지 않았을까.. 하는 생각이 듭니다.
    // choice Scene render 부분에서 수정하지 않고 epidemic을 확장할수 있도록.
    // 그 외에 피드백도 해주시면 감사하겠습니다..!
    internal class Program
    {
        // 전염병 주식회사
        // 게임 설명: 모은 인류를 멸종시키면 승리, 치료제가 나오기 전에 멸종시켜야함.
        // 게임 순서: 총 3개의 씬으로, 메인화면씬 ->  전염병 선택씬 -> 게임씬

        //FactoryManager(싱글톤패턴)에서 SceneFactory(팩토리메서드)를 사용해
        // 씬 생성 후 저장.
        static void ResetScenes(List<Scene> scenes)
        {
            for(int i = 1; i <= 3; i++)
            {
                scenes.Add(FactoryManager.Instance.CreateScene((SceneType)i));
            }
        }

        // 3개의 씬을 순서대로 진행.
        // 2번째씬부터는 Player클래스가 필요하므로 다운캐스팅을 하여IAwake인터페이스를 통해 Player데이터 사용.
        static void PlayGame(List<Scene> scenes, Player player)
        {
            int sceneNum = 0;
            Console.WriteLine(scenes.Count);
            while (sceneNum < scenes.Count)
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
            List<Scene> scenes = new List<Scene>();

            ResetScenes(scenes);
            PlayGame(scenes, player);
        }
    }
}
