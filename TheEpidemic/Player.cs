﻿namespace TheEpidemic
{
    public interface IPlayer
    {
        int Gold { get; set; }
        int GoldUpInfectRate { get; set; }
        int GoldUpFatalityRate { get; set; }
        void UpInfectRate(IGameManager gameManager);
        void UpFatalityRate(IGameManager gameManager);
        void UseSkill(IGameManager gameManager);
        void Next(IGameManager gameManager);
        void BfsStart(IGameManager gameManager);

    }


    public class Player : IPlayer
    {
        private int _gold;
        private int _goldUpInfectRate;
        private int _glodUpFatalityRate;
  
        public Player()
        {
            _gold = 0;
            _goldUpInfectRate = 20;
            _glodUpFatalityRate = 20;
        }

        public int Gold { get { return _gold; } set { _gold = value; } }
        public int GoldUpInfectRate { get { return _goldUpInfectRate; } set { _goldUpInfectRate = value; } }
        public int GoldUpFatalityRate { get { return _glodUpFatalityRate; } set { _glodUpFatalityRate = value; } }


        public void UpInfectRate(IGameManager gameManager)
        {
            if (Gold >= GoldUpInfectRate)
            {
                gameManager.Epidemic.InfectRate += 5;
                Gold -= GoldUpInfectRate;
                GoldUpInfectRate += 20;
            }
            else
            {
                Console.WriteLine("돈이 부족해 강화를 할수가 없습니다.");
            }
        }

        public void UpFatalityRate(IGameManager gameManager)
        {
            if (Gold >= GoldUpFatalityRate)
            {
                gameManager.Epidemic.FatalityRate += 5;
                Gold -= GoldUpFatalityRate;
                GoldUpFatalityRate += 20;
            }
            else
            {
                Console.WriteLine("돈이 부족해 강화를 할수가 없습니다.");
            }
        }

        public void UseSkill(IGameManager gameManager)
        {
            if(gameManager.Epidemic.BuffWaitTime == 0)
            {
                gameManager.Epidemic.Buff();
            }
            else
            {
                Console.WriteLine($"아직 {gameManager.Epidemic.BuffWaitTime}일 지나야합니다.");
            }
        }
        public void Next(IGameManager gameManager)
        {
            BfsStart(gameManager);
            gameManager.Day++;
            if(gameManager.Epidemic.IsBuff == true)
            {
                if (gameManager.Epidemic.BuffDuration > 0)
                {
                    gameManager.Epidemic.BuffDuration--;
                }
                else if (gameManager.Epidemic.BuffDuration == 0)
                {
                    gameManager.Epidemic.DeBuff();
                }
            }
            if (gameManager.Epidemic.BuffWaitTime != 0){
                gameManager.Epidemic.BuffWaitTime--;
            }

        }


        public void BfsStart(IGameManager gameManager)
        {
            int[,] map = gameManager.Map;
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
                        InfectorKillBfs(i, j, 2, map, visited, gameManager.Epidemic.FatalityRate);
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
                        InfectorKillBfs(i, j, 1, map, visited, gameManager.Epidemic.InfectRate);
                    }
                }
            }


        }

        public void InfectorKillBfs(int y, int x, int visitNum, int[,] map, bool[,] visited, int rate)
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
                Gold += 1;
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
                            Gold += 1;
                        }
                    }
                }
            }
        }




    }
}
