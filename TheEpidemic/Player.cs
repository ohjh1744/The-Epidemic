﻿namespace TheEpidemic
{

    // Player가 할 수 있는 행동 및 사람들에게 전염을 퍼트리는 역할을 하는 클래스
    public class Player
    {
        // 본인이 선택한 전염병
        private Epidemic _epidemic;
        // 게임매니저정보를 많이 활용하기에 따로 내부에서 저장.
        private GameManager _gameManager;

        public Player()
        {
            _gameManager = GameManager.Instance;
        }

        public Epidemic Epidemic { get { return _epidemic; } set { _epidemic = value; } }

        // 2번째 씬에서 원하는 전염병 선택 후 Player에 저장.
        public void GetEpidemic(Epidemic epidemic)
        {
            _epidemic = epidemic;
            _gameManager.Name = epidemic.Name;
            _gameManager.InfectRate = epidemic.InfectRate;
            _gameManager.FatalityRate = epidemic.FatalityRate;

        }
        // 전염률 증가,골드감소, 전염률Upgrade골드 증가 -> 콜백함수를 통해 GameManager의 데이터 Update
        public void UpInfectRate()
        {
            if (_gameManager.Gold >= _gameManager.GoldForInfect)
            {
                _gameManager.Update += UpdateInfectRate;
                _gameManager.Update += UseGoldForInfect;
                _gameManager.Update += UpgradeGoldForInfect;
                Console.WriteLine("hi");
            }
            else
            {
                Console.WriteLine("돈이 부족해 강화를 할수가 없습니다.");
            }
        }
        // 치사율 증가, 골드감소 , 치사율Upgrade골드 증가-> 콜백함수를 통해 GameManager의 데이터 Update
        public void UpFatalityRate()
        {
            if (_gameManager.Gold >= _gameManager.GoldForFatality)
            {
                _gameManager.Update += UpdateFatalityRate;
                _gameManager.Update += UseGoldForFatality;
                _gameManager.Update += UpgradeGoldForFatality;
            }
            else
            {
                Console.WriteLine("돈이 부족해 강화를 할수가 없습니다.");
            }
        }

        // 버프 사용 -> 버프 사용을 통해 Update된 능력치를 콜백함수를 통해 GameManager의 데이터 Update
        public void UseSkill()
        {
            if (_epidemic.BuffWaitTime == 0)
            {
                _epidemic.OnBuff();
                _gameManager.Update += OnOffBuff;
            }
            else
            {
                Console.WriteLine($"아직 {_epidemic.BuffWaitTime}일 지나야합니다.");
            }
        }
        // 다음 날짜로 넘어가기, 넘어갈때 마다 사용한 버프가 있다면 지속시간 및 쿨타임감소, 지속 시간 끝나면 버프해제.
        public void Next()
        {
            FindSurvivor();
            _gameManager.Update += IncreaseDay;
            if (_epidemic.IsBuff == true)
            {
                if (_epidemic.BuffDuration > 0)
                {
                    _gameManager.Update += UpdateBuffDuration;
                }
                else if (_epidemic.BuffDuration == 0)
                {
                    _epidemic.OnDeBuff();
                    _gameManager.Update += OnOffBuff;
                }
            }
            if (_epidemic.BuffWaitTime != 0)
            {
                _gameManager.Update += UpdateBuffWaitTime;
            }

        }

        // 살아있는 사람을 찾아 전염병 퍼트리기 및 감연된 사람은 kill
        public void FindSurvivor()
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

            // 감염자 찾아 죽이는 Bfs탐색 
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 30; j++)
                {
                    if (map[i, j] == 2 && visited[i, j] == false)
                    {
                        InfectOrKill(i, j, 2, map, visited, _epidemic.FatalityRate);
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

            // 감염이 안된 사람을 찾아 감염시키는 Bfs탐색
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 30; j++)
                {
                    if (map[i, j] == 1 && visited[i, j] == false)
                    {
                        InfectOrKill(i, j, 1, map, visited, _epidemic.InfectRate);
                    }
                }
            }

            // GameManager 콜백함수에 저장.
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 30; j++)
                {
                    if (map[i, j] == 2)
                    {
                        _gameManager.Update += UpdateInfected;
                    }
                    else if (map[i, j] == 3)
                    {
                        _gameManager.Update += UpdateSurvivor;
                        _gameManager.Update += UpdateDeath;
                    }
                }
            }


        }

        // 전염률에 따라 전염시키고, 감연된 사람들 중에서도 치사율에 따라 사망자 발생.
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
                map[y, x] = visitNum + 1;
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
                    if (nx < 0 || ny < 0 || nx >= 30 || ny >= 20)
                    {
                        continue;
                    }
                    if (visited[ny, nx] == false && map[ny, nx] == visitNum)
                    {
                        random = new Random();
                        randomNumber = random.Next(0, 101);
                        if (rate >= randomNumber)
                        {
                            map[ny, nx] = visitNum + 1;
                            visited[ny, nx] = true;
                            _gameManager.Gold += 1;
                        }
                    }
                }
            }
        }

        //아래부터는 콜백함수를 위한 함수들
        // Player에 기능이 많기에 책임이 많은 것 같아 처음에는 기능들을 인터페이스로 세분화시켜 나누고나서 상속받았었지만
        // 세분화로 나눈 인터페이스들이 Player말곤 앞으로도 다른 클래스가 사용할 일이 없기에
        // 피드백 통해 다시 제거.
        public void UpdateInfectRate()
        {
            _gameManager.InfectRate += 5;
            _epidemic.InfectRate += 5;
        }

        public void UseGoldForInfect()
        {
            _gameManager.Gold -= _gameManager.GoldForInfect;
        }

        public void UpgradeGoldForInfect()
        {
            _gameManager.GoldForInfect += 20;
        }

        public void UpdateFatalityRate()
        {
            _gameManager.FatalityRate += 5;
            _epidemic.FatalityRate += 5;
        }

        public void UseGoldForFatality()
        {
            _gameManager.Gold -= _gameManager.GoldForFatality;
        }

        public void UpgradeGoldForFatality()
        {
            _gameManager.GoldForFatality += 20;
        }

        public void IncreaseDay()
        {
            _gameManager.Day++;
        }
        public void OnOffBuff()
        {
            _gameManager.InfectRate = _epidemic.InfectRate;
            _gameManager.FatalityRate = _epidemic.FatalityRate;
        }

        //버프 지속시간은 epidemic에서 감소
        public void UpdateBuffDuration()
        {
            _epidemic.BuffDuration--;
        }

        //버프 쿨타임은 epidemic에서 감소
        public void UpdateBuffWaitTime()
        {
            _epidemic.BuffWaitTime--;
        }

        public void UpdateSurvivor()
        {
            _gameManager.Survivor--;
        }

        public void UpdateDeath()
        {
            _gameManager.Death++;
        }

        public void UpdateInfected()
        {
            _gameManager.Infected++;
        }




    }
}
