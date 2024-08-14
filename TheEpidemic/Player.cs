namespace TheEpidemic
{
    public interface IPlayer
    {
        void UpInfectRate();
        void UpFatalityRate();
        void UseSkill();
        void Next();
        void FindSurvivor();

    }


    public class Player : IPlayer
    {
        IGameManager _gameManager;
        public Player(IGameManager gameManager)
        {
            _gameManager = gameManager;
        }
        public void UpInfectRate()
        {
            if (_gameManager.Gold >= _gameManager.UpgradeGoldForInfect)
            {
                _gameManager.Epidemic.InfectRate += 5;
                _gameManager.Gold -= _gameManager.UpgradeGoldForInfect;
                _gameManager.UpgradeGoldForInfect += 20;
            }
            else
            {
                Console.WriteLine("돈이 부족해 강화를 할수가 없습니다.");
            }
        }

        public void UpFatalityRate( )
        {
            if (_gameManager.Gold >= _gameManager.UpgradeGoldForFatality)
            {
                _gameManager.Epidemic.FatalityRate += 5;
                _gameManager.Gold -= _gameManager.UpgradeGoldForFatality;
                _gameManager.UpgradeGoldForFatality += 20;
            }
            else
            {
                Console.WriteLine("돈이 부족해 강화를 할수가 없습니다.");
            }
        }

        public void UseSkill( )
        {
            if(_gameManager.Epidemic.BuffWaitTime == 0)
            {
                _gameManager.Epidemic.Buff();
            }
            else
            {
                Console.WriteLine($"아직 {_gameManager.Epidemic.BuffWaitTime}일 지나야합니다.");
            }
        }
        public void Next( )
        {
            FindSurvivor();
            _gameManager.Day++;
            if(_gameManager.Epidemic.IsBuff == true)
            {
                if (_gameManager.Epidemic.BuffDuration > 0)
                {
                    _gameManager.Epidemic.BuffDuration--;
                }
                else if (_gameManager.Epidemic.BuffDuration == 0)
                {
                    _gameManager.Epidemic.DeBuff();
                }
            }
            if (_gameManager.Epidemic.BuffWaitTime != 0){
                _gameManager.Epidemic.BuffWaitTime--;
            }

        }


        public void FindSurvivor( )
        {
            int[,] map = _gameManager.Map;
            bool[,] visited = new bool[20, 30];

            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 30; j++)
                {
                    visited[i, j] = false;
                }
            }

            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 30; j++)
                {
                    if (map[i, j] == 2 && visited[i, j] == false)
                    {
                        InfectOrKill(i, j, 2, map, visited, _gameManager.Epidemic.FatalityRate);
                    }
                }
            }

            for (int i = 0; i < 20; i++)  
            {
                for (int j = 0; j < 30; j++)
                {
                    visited[i, j] = false;
                }
            }

            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 30; j++)
                {
                    if (map[i, j] == 1 && visited[i, j] == false)
                    {
                        InfectOrKill(i, j, 1, map, visited, _gameManager.Epidemic.InfectRate);
                    }
                }
            }


        }

        public void InfectOrKill(int y, int x, int visitNum, int[,] map, bool[,] visited, int rate)
        {
            visited[y, x] = true;
            int[] dx = new int[4] { 1, -1, 0, 0 };
            int[] dy = new int[4] { 0, 0, -1, 1 };
            Queue<(int, int)> q = new Queue<(int, int)>();
            q.Enqueue((y, x));

            Random random = new Random();
            int randomNumber = random.Next(0, 101);
            if (rate >= randomNumber)
            {
                map[y, x] = visitNum+1;
                visited[y, x] = true;
                _gameManager.Gold += 1;
            }

            while (q.Count != 0)
            {
                int cx = q.Peek().Item2;
                int cy = q.Peek().Item1;
                q.Dequeue();


                for (int i = 0; i < 4; i++)
                {
                    int nx = cx + dx[i];
                    int ny = cy + dy[i];
                    if (nx < 0 || ny < 0 || nx >= 30|| ny >= 20)
                    {
                        continue;
                    }
                    if (visited[ny,nx] == false && map[ny, nx] == visitNum)
                    {
                        random = new Random();
                        randomNumber = random.Next(0, 101);
                        if(rate >= randomNumber)
                        {
                            map[ny, nx] = visitNum+1;
                            visited[ny, nx] = true;
                            _gameManager.Gold += 1;
                        }
                    }
                }
            }
        }




    }
}
