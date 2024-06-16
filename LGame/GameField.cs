using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LGame
{
    /*public enum Tiles
    {
        Empty = 0,
        First,
        Second,

    }*/
    public class LShape : ICloneable
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
        public bool Equals(LShape p)
        {
            for (int i = 0; i < 4; i++)
                if (Tiles[i] != p[i])
                    return false;
            return true;
        }

        public object Clone()
        {
            return new LShape
            {
                Tiles = (Point[])Tiles.Clone(),
            };
        }
    }
    public enum FieldTile
    {
        None = 0,
        BluePlayer,
        RedPlayer,
        YellowStone,
        GreenStone,
    }
    public class GameField : ICloneable
    {
        public static LShape[] LForms = new LShape[8];
        // TODO: Convert to enum
        public FieldTile[,] Field = new FieldTile[4, 4];
        public LShape[] player = new LShape[2];
        public Point[] stone = new Point[2];
        public int PlayerStep;
        static GameField()
        {
            LForms[0] = new LShape();
            LForms[0][0] = new Point(0, 1);
            LForms[0][1] = new Point(0, 0);
            LForms[0][2] = new Point(1, 0);
            LForms[0][3] = new Point(2, 0);
            LForms[0].Sort();

            LForms[1] = new LShape();
            LForms[1][0] = new Point(2, 1);
            LForms[1][1] = new Point(2, 0);
            LForms[1][2] = new Point(1, 0);
            LForms[1][3] = new Point(0, 0);
            LForms[1].Sort();

            LForms[2] = new LShape();
            LForms[2][0] = new Point(0, 0);
            LForms[2][1] = new Point(0, 1);
            LForms[2][2] = new Point(1, 1);
            LForms[2][3] = new Point(2, 1);
            LForms[2].Sort();

            LForms[3] = new LShape();
            LForms[3][0] = new Point(2, 0);
            LForms[3][1] = new Point(2, 1);
            LForms[3][2] = new Point(1, 1);
            LForms[3][3] = new Point(0, 1);
            LForms[3].Sort();
            //
            LForms[4] = new LShape();
            LForms[4][0] = new Point(1, 0);
            LForms[4][1] = new Point(0, 0);
            LForms[4][2] = new Point(0, 1);
            LForms[4][3] = new Point(0, 2);
            LForms[4].Sort();

            LForms[5] = new LShape();
            LForms[5][0] = new Point(0, 0);
            LForms[5][1] = new Point(1, 0);
            LForms[5][2] = new Point(1, 1);
            LForms[5][3] = new Point(1, 2);
            LForms[5].Sort();

            LForms[6] = new LShape();
            LForms[6][0] = new Point(0, 0);
            LForms[6][1] = new Point(0, 1);
            LForms[6][2] = new Point(0, 2);
            LForms[6][3] = new Point(1, 2);
            LForms[6].Sort();

            LForms[7] = new LShape();
            LForms[7][0] = new Point(1, 0);
            LForms[7][1] = new Point(1, 1);
            LForms[7][2] = new Point(1, 2);
            LForms[7][3] = new Point(0, 2);
            LForms[7].Sort();
        }
        
        public GameField()
        {
            Restart();
        }
        public GameField(int playerStep, LShape player1, LShape player2, Point stone1, Point stone2)
        {
            player[0] = new LShape();
            player[1] = new LShape();
            for (int i = 0; i < 4; i++)
            {
                player[0][i] = player1[i];
                Field[player[0][i].X, player[0][i].Y] = FieldTile.BluePlayer;
            }
            for (int i = 0; i < 4; i++)
            {
                player[1][i] = player2[i];
                Field[player[1][i].X, player[1][i].Y] = FieldTile.RedPlayer;
            }
            stone[0] = stone1;
            Field[stone[0].X, stone[0].Y] = FieldTile.YellowStone;
            stone[1] = stone2;
            Field[stone[1].X, stone[1].Y] = FieldTile.GreenStone;
            PlayerStep = playerStep;
        }

        public bool IsStep(int player)
        {
            if (player < 0 || player > 1)
                return false;
            return PlayerStep == player;
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

            player[0] = new LShape();
            player[0][0] = new Point(1, 0);
            player[0][1] = new Point(2, 0);
            player[0][2] = new Point(2, 1);
            player[0][3] = new Point(2, 2);

            /*player[0][0] = new Point(0, 1);
            player[0][1] = new Point(0, 2);
            player[0][2] = new Point(1, 2);
            player[0][3] = new Point(2, 2);*/
            player[0].Sort();
            for (int i = 0; i < 4; i++)
                Field[player[0][i].X, player[0][i].Y] = FieldTile.BluePlayer;

            player[1] = new LShape();
            player[1][0] = new Point(2, 3);
            player[1][1] = new Point(1, 3);
            player[1][2] = new Point(1, 2);
            player[1][3] = new Point(1, 1);

            /*player[1][0] = new Point(1, 1);
            player[1][1] = new Point(2, 1);
            player[1][2] = new Point(3, 1);
            player[1][3] = new Point(3, 2);*/
            player[1].Sort();
            for (int i = 0; i < 4; i++)
                Field[player[1][i].X, player[1][i].Y] = FieldTile.RedPlayer;

            stone[0] = new Point(0, 0);
            Field[stone[0].X, stone[0].Y] = FieldTile.YellowStone;
            stone[1] = new Point(3, 3);
            Field[stone[1].X, stone[1].Y] = FieldTile.GreenStone;
        }
        public void NextStep()
        {
            PlayerStep = 1 - PlayerStep;
        }
        public bool CheckCorrect(LShape _newPosition)
        {
            LShape newPosition = new LShape();
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
                    if (Field[newPosition[i].X, newPosition[i].Y] != (FieldTile)(PlayerStep + 1))
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
            if (Field[newStone.X, newStone.Y] == 0 || Field[newStone.X, newStone.Y] == (FieldTile)(StoneID + 3))
                return true;
            return false;
        }
        public bool Play(LShape _newPosition)
        {
            LShape newPosition = new LShape();
            for (int i = 0; i < 4; i++)
                newPosition[i] = _newPosition[i];
            newPosition.Sort();
            if (!CheckCorrect(newPosition))
                return false;
            for (int i = 0; i < 4; i++)
                Field[player[PlayerStep][i].X, player[PlayerStep][i].Y] = 0;
            for (int i = 0; i < 4; i++)
                Field[newPosition[i].X, newPosition[i].Y] = (FieldTile)(PlayerStep + 1);
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
            Field[stone[StoneID].X, stone[StoneID].Y] = (FieldTile)(StoneID + 3);
            NextStep();
            return true;
        }
        public bool Play(LShape newPosition, int StoneID, Point newStone)
        {
            if (!Play(newPosition))
                return false;
            return Play(StoneID, newStone);
        }
        public bool IsFinish()
        {
            LShape newPosition = new LShape();
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

        public object Clone()
        {
            return new GameField(PlayerStep, player[0], player[1], stone[0], stone[1]);
        }
    }
}
