using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace LGame
{
    /*public enum Tiles
    {
        Empty = 0,
        First,
        Second,

    }*/
    public class Player
    {
        private Point[] Tiles = new Point[4];
        public Point this[int index]
        {
            get
            {
                return Tiles[index];
            }
            set
            {
                Tiles[index] = value;
            }
        }
        public void Sort()
        {
            //Array.Sort(Tiles, PointComparer);
            Tiles = Tiles.OrderBy(p => p.X).ThenBy(p => p.Y).ToArray();
        }
        public void Clear()
        {
            for (int i = 0; i < 4; i++)
                Tiles[i] = new Point(-1, -1);
        }
        public bool Equals(Player p)
        {
            for (int i = 0; i < 4; i++)
                if (Tiles[i] != p[i])
                    return false;
            return true;
        }
    }
    public class Game
    {
        public int[,] Field = new int[4, 4]; // [x,y] 0 - White; 1 - Blue; 2 - Red; 3 - Yellow; 4 - Green
        Bot.Difficulties[] LevelBot = new Bot.Difficulties[2]; // 0 - Player
        public bool AutoNext = false;
        public bool IsWait = false;
        public Player[] LForms = new Player[8];
        public Player[] player = new Player[2];
        public Point[] stone = new Point[2];
        int PlayerStep;
        public void Init()
        {
            LevelBot[0] = Bot.Difficulties.Player;
            LevelBot[1] = Bot.Difficulties.Player;

            LForms[0] = new Player();
            LForms[0][0] = new Point(0, 1);
            LForms[0][1] = new Point(0, 0);
            LForms[0][2] = new Point(1, 0);
            LForms[0][3] = new Point(2, 0);
            LForms[0].Sort();

            LForms[1] = new Player();
            LForms[1][0] = new Point(2, 1);
            LForms[1][1] = new Point(2, 0);
            LForms[1][2] = new Point(1, 0);
            LForms[1][3] = new Point(0, 0);
            LForms[1].Sort();

            LForms[2] = new Player();
            LForms[2][0] = new Point(0, 0);
            LForms[2][1] = new Point(0, 1);
            LForms[2][2] = new Point(1, 1);
            LForms[2][3] = new Point(2, 1);
            LForms[2].Sort();

            LForms[3] = new Player();
            LForms[3][0] = new Point(2, 0);
            LForms[3][1] = new Point(2, 1);
            LForms[3][2] = new Point(1, 1);
            LForms[3][3] = new Point(0, 1);
            LForms[3].Sort();
            //
            LForms[4] = new Player();
            LForms[4][0] = new Point(1, 0);
            LForms[4][1] = new Point(0, 0);
            LForms[4][2] = new Point(0, 1);
            LForms[4][3] = new Point(0, 2);
            LForms[4].Sort();

            LForms[5] = new Player();
            LForms[5][0] = new Point(0, 0);
            LForms[5][1] = new Point(1, 0);
            LForms[5][2] = new Point(1, 1);
            LForms[5][3] = new Point(1, 2);
            LForms[5].Sort();

            LForms[6] = new Player();
            LForms[6][0] = new Point(0, 0);
            LForms[6][1] = new Point(0, 1);
            LForms[6][2] = new Point(0, 2);
            LForms[6][3] = new Point(1, 2);
            LForms[6].Sort();

            LForms[7] = new Player();
            LForms[7][0] = new Point(1, 0);
            LForms[7][1] = new Point(1, 1);
            LForms[7][2] = new Point(1, 2);
            LForms[7][3] = new Point(0, 2);
            LForms[7].Sort();
        }
        public Game()
        {
            Init();
            Restart();
        }
        public Game(int PlayerStep, Player player1, Player player2, Point stone1, Point stone2)
        {
            Init();
            player[0] = new Player();
            player[1] = new Player();
            for (int i = 0; i < 4; i++)
            {
                player[0][i] = player1[i];
                Field[player[0][i].X, player[0][i].Y] = 1;
            }
            for (int i = 0; i < 4; i++)
            {
                player[1][i] = player2[i];
                Field[player[1][i].X, player[1][i].Y] = 2;
            }
            stone[0] = stone1;
            Field[stone[0].X, stone[0].Y] = 3;
            stone[1] = stone2;
            Field[stone[1].X, stone[1].Y] = 4;
            this.PlayerStep = PlayerStep;

        }
        public bool IsPlayerStep(int player)
        {
            if (player < 0 || player > 1 || LevelBot[player] > 0)
                return false;
            return PlayerStep == player;
        }
        public bool IsBotStep()
        {
            return (LevelBot[PlayerStep] > 0);
        }
        public bool IsStep(int player)
        {
            if (player < 0 || player > 1)
                return false;
            return PlayerStep == player;
        }
        public bool RegisterBot(int player, Bot.Difficulties difficulty = Bot.Difficulties.Medium)
        {
            if (player < 0 || player > 1)
                return false;
            if (LevelBot[player] != Bot.Difficulties.Player)
                return false;
            
            LevelBot[player] = difficulty;
            return true;
        }

        public void Restart()
        {
            PlayerStep = 0;
            for (int i = 0; i < 4; i++)
            { 
                for (int j = 0; j < 4; j++)
                {
                    Field[i, j] = 0;
                }
            }

            player[0] = new Player();
            player[0][0] = new Point(1, 0);
            player[0][1] = new Point(2, 0);
            player[0][2] = new Point(2, 1);
            player[0][3] = new Point(2, 2);
            player[0].Sort();
            for (int i = 0; i < 4; i++)
                Field[player[0][i].X, player[0][i].Y] = 1;

            player[1] = new Player();
            player[1][0] = new Point(2, 3);
            player[1][1] = new Point(1, 3);
            player[1][2] = new Point(1, 2);
            player[1][3] = new Point(1, 1);
            player[1].Sort();
            for (int i = 0; i < 4; i++)
                Field[player[1][i].X, player[1][i].Y] = 2;

            stone[0] = new Point(0, 0);
            Field[stone[0].X, stone[0].Y] = 3;
            stone[1] = new Point(3, 3);
            Field[stone[1].X, stone[1].Y] = 4;
        }
        public bool NextStep(bool Init = false)
        {
            IsWait = false;
            if (LevelBot[PlayerStep] > 0)
            {
                // Sheduler Bot
                if (IsFinish())
                    return false;
                Form1.SelfRef.bot.Run(LevelBot[PlayerStep], PlayerStep);
                PlayerStep = (PlayerStep + 1) % 2;
            }
            else
                if (!Init)
                    PlayerStep = (PlayerStep + 1) % 2;
            return (LevelBot[PlayerStep] > 0);
        }
        public bool CheckCorrect(Player _newPosition)
        {
            Player newPosition = new Player();
            for (int i = 0; i < 4; i++)
                newPosition[i] = _newPosition[i];
            newPosition.Sort();
            // Check Change Position and Availability
            int cnt = 0;
            bool canStep = true;
            int MinX = 123, MinY = 123;
            for (int i = 0; i < 4; i++)
            {
                if (0 > newPosition[i].X || newPosition[i].X >= 4 || 0 > newPosition[i].Y || newPosition[i].Y >= 4)
                    return false;
                if (Field[newPosition[i].X, newPosition[i].Y] > 0)
                {
                    cnt++;
                    if (Field[newPosition[i].X, newPosition[i].Y] != PlayerStep + 1)
                    {
                        canStep = false;
                        break;
                    }
                }
                MinX = Math.Min(MinX, newPosition[i].X);
                MinY = Math.Min(MinY, newPosition[i].Y);
            }
            if (cnt >= 4 || !canStep)
                return false;
            // Check L Form
            for (int i = 0; i < 4; i++)
                newPosition[i] = new Point(newPosition[i].X - MinX, newPosition[i].Y - MinY);

            for (int i = 0; i < 8; i++)
                if (LForms[i].Equals(newPosition))
                    return true;
            return false;
        }
        public bool CheckStone(int StoneID, Point newStone)
        {
            if (StoneID < 0 || StoneID > 1 || 0 > newStone.X || newStone.X >= 4 || 0 > newStone.Y || newStone.Y >= 4)
                return false;
            if (Field[newStone.X, newStone.Y] == 0 || Field[newStone.X, newStone.Y] == StoneID + 3)
                return true;
            return false;
        }
        public bool Play(Player _newPosition)
        {
            Player newPosition = new Player();
            for (int i = 0; i < 4; i++)
                newPosition[i] = _newPosition[i];
            newPosition.Sort();
            if (!CheckCorrect(newPosition))
                return false;
            for (int i = 0; i < 4; i++)
                Field[player[PlayerStep][i].X, player[PlayerStep][i].Y] = 0;
            for (int i = 0; i < 4; i++)
                Field[newPosition[i].X, newPosition[i].Y] = PlayerStep + 1;
            player[PlayerStep] = newPosition;
            //NextStep();
            return true;
        }
        public bool Play(int StoneID, Point newStone)
        {
            if (!CheckStone(StoneID, newStone))
                return false;
            Field[stone[StoneID].X, stone[StoneID].Y] = 0;
            stone[StoneID] = newStone;
            Field[stone[StoneID].X, stone[StoneID].Y] = StoneID + 3;
            if (!AutoNext)
                NextStep();
            else
                IsWait = true;
            return true;
        }
        public bool Play(Player newPosition, int StoneID, Point newStone)
        {
            if (!Play(newPosition))
                return false;
            return Play(StoneID, newStone);
            /*if (!CheckStone(StoneID, newStone) || !CheckCorrect(newPosition))
                return false;
            for (int i = 0; i < 4; i++)
                Field[player[PlayerStep][i].X, player[PlayerStep][i].Y] = 0;
            for (int i = 0; i < 4; i++)
                Field[newPosition[i].X, newPosition[i].Y] = PlayerStep + 1;
            player[PlayerStep] = newPosition;
            NextStep();
            return true;*/
        }
        public bool IsFinish()
        {
            Player newPosition = new Player();
            for (int x = 0; x < 4; x++)
                for (int y = 0; y < 4; y++)
                    for (int i = 0; i < 8; i++)
                    {
                        for (int j = 0; j < 4; j++)
                            newPosition[j] = new Point(LForms[i][j].X + x, LForms[i][j].Y + y);

                        if (CheckCorrect(newPosition))
                            return false;
                    }
            return true;
        }
    }
}
