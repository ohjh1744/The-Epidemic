namespace TheEpidemic
{
    // Epdemic 전염병 팩토리 메서드
    public interface IEpidemicFactory
    {
        Epidemic Create();
    }

    public class VirusFactory : IEpidemicFactory
    {
        public Epidemic Create()
        {
            return new Virus();
        }
    }

    public class BacteriaFactory: IEpidemicFactory
    {
        public Epidemic Create()
        {
            return new Bacteria();
        }
    }
    public class CoronaFactory : IEpidemicFactory
    {
        public Epidemic Create()
        {
            return new Corona();
        }
    }
}
