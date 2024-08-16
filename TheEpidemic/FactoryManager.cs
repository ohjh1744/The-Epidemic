using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheEpidemic
{

    public enum EpidemicType { 박테리아 =1, 바이러스=2, 코로나=3 }
    public enum SceneType { 메인화면 = 1, 전염병선택 = 2, 게임화면 = 3}

    //FactoryMethod패턴을 활용하는 팩토리들을 관리해주는 Manager
    //원하는 Factory를 불러 인덱스에 따라 Dictionary에 저장되어있는 값을 가져와 객체를 생성해주게 도와주는 역할
    public class FactoryManager
    {
        // 싱글톤 패턴을 위한 전역변수
        private static FactoryManager _instance;
        //Epidemic 생성관련 딕셔너리의 키
        private List<EpidemicType> _epidemicKeys;
        //Epidemic 생성관련 딕셔너리의 벨류
        private List<IEpidemicFactory> _epidemicValues;
        //씬 생성관련 딕셔너리의 키
        private List<SceneType> _sceneKeys;
        //씬 생성관련 딕셔너리의 벨류
        private List<ISceneFactory> _sceneValues;
        //Epidemic 생성관련 딕셔너리
        private Dictionary<EpidemicType, IEpidemicFactory> _epidemicFactoryType;
        //씬 생성관련 딕셔너리
        private Dictionary<SceneType, ISceneFactory> _sceneFactoryType;

        //싱글톤 패턴 적용
        public static FactoryManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new FactoryManager();
                }
                return _instance;
            }
        }

        // 생성자를 사용해 모든 List와 딕셔너리 값 초기화
        public FactoryManager()
        {
            _epidemicKeys = new List<EpidemicType>();
            _epidemicKeys.Add(EpidemicType.박테리아);
            _epidemicKeys.Add(EpidemicType.바이러스);
            _epidemicKeys.Add(EpidemicType.코로나);
            _epidemicValues = new List<IEpidemicFactory>();
            _epidemicValues.Add(new BacteriaFactory());
            _epidemicValues.Add(new VirusFactory());
            _epidemicValues.Add(new CoronaFactory());
            _sceneKeys = new List<SceneType>();
            _sceneKeys.Add(SceneType.메인화면);
            _sceneKeys.Add(SceneType.전염병선택);
            _sceneKeys.Add(SceneType.게임화면);
            _sceneValues = new List<ISceneFactory>();
            _sceneValues.Add(new FirstSceneFactory());
            _sceneValues.Add(new ChoiceSceneFactory());
            _sceneValues.Add(new GameSceneFactory());
            _epidemicFactoryType = new Dictionary<EpidemicType, IEpidemicFactory>();
            for(int i = 0; i < _epidemicKeys.Count; i++)
            {
                _epidemicFactoryType[_epidemicKeys[i]] = _epidemicValues[i];
            }
            _sceneFactoryType = new Dictionary<SceneType, ISceneFactory>();
            for (int i = 0; i < _sceneKeys.Count; i++)
            {
                _sceneFactoryType[_sceneKeys[i]] = _sceneValues[i];
            }
        }

        // Epidemic 생성함수
        public Epidemic CreateEpidemic(EpidemicType epidemicType)
        {
            if (_epidemicFactoryType.TryGetValue(epidemicType, out IEpidemicFactory factory))
            {
                return factory.Create(); 
            }
            return null;
        }

        //Scene 생성함수
        public Scene CreateScene(SceneType sceneType)
        {

            if (_sceneFactoryType.TryGetValue(sceneType, out ISceneFactory factory))
            {
                return factory.Create();
            }
            
            return null;
        }



    }
}
