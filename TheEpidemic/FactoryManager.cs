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
    //원하는 인덱스에 따라 원하는Factory를 불러 객체를 생성해주게 도와주는 역할
    public class FactoryManager
    {
        private static FactoryManager _instance;
        private List<EpidemicType> _epidemicKeys;
        private List<IEpidemicFactory> _epidemicValues;
        private List<SceneType> _sceneKeys;
        private List<ISceneFactory> _sceneValues;
        private Dictionary<EpidemicType, IEpidemicFactory> _epidemicFactoryType;
        private Dictionary<SceneType, ISceneFactory> _sceneFactoryType;

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

        public Epidemic CreateEpidemic(EpidemicType epidemicType)
        {
            if (_epidemicFactoryType.TryGetValue(epidemicType, out IEpidemicFactory factory))
            {
                return factory.Create(); 
            }
            return null;
        }

        public Scene CreateScene(SceneType sceneType)
        {
            if (_sceneFactoryType.ContainsKey(sceneType))
            {
                Console.WriteLine("키가 들어잇습니다");
            }
            else
            {
                Console.WriteLine("안들어있습니다.");
            }
            if (_sceneFactoryType.TryGetValue(sceneType, out ISceneFactory factory))
            {
                return factory.Create();
            }
            
            return null;
        }



    }
}
