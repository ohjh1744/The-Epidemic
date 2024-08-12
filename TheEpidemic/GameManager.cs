using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheEpidemic
{
    public interface IGameManager
    {
        IEpidemic Epidemic { get; set; }
        IGlobal Global { get; set; }
        int[,] Map { get; set; }
        int Infected { get; set; }
        int Death { get; set; }
        void Show();

    }

    public class GameManager : IGameManager
    {

        private IEpidemic _epidemic;
        private IGlobal _global;
        private int[,] _map;
        private int _infected;
        private int _death;

        public IEpidemic Epidemic { get { return _epidemic; } set { _epidemic = value; } }
        public IGlobal Global { get { return _global; } set { _global = value; } }
        public int[,] Map { get { return _map; } set { _map = value; } }
        public int Infected { get { return _infected; } set { _infected = value; } }
        public int Death { get { return _death; } set { _death = value; } }
        public void Show()
        {

        }
    }
}
