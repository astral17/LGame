using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace LGame
{
    public class Bot
    {
        Random rnd = new Random();
        public int[,] Field = new int[4, 4]; // [x,y] 0 - White; 1 - Blue; 2 - Red; 3 - Yellow; 4 - Green
        //Dictionary<ulong, bool> isWinning; // First(Blue) player Win? ; if second player has access to win pos -> first lose
        Game LocalGame = new Game();
        public enum Difficulties
        {
            Player = 0,
            Random = 1,
            Easy = 2, // if can finish game play, otherwise random
            Medium = 3, // if can finish game play, otherwise not lose random
            //Hard = 4, // 4 step, otherwise not lose random
            //Perfect = 5, // theory of game, 90% of chance be the same as Hard
        }
        public Bot()
        {

        }
        ulong Hash()
        {
            ulong hash = 0;
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    hash = ((hash * 7) % 1000000000000000007 + (ulong)Field[i, j] + 1) % 1000000000000000007;
            return hash;
        }
        public void Run(Difficulties Difficulty = Difficulties.Random)
        {
            if (Difficulty == Difficulties.Random)
            {
                Player newPosition = new Player();
                int x, y, f;
                do
                {
                    f = rnd.Next(0, 7);
                    x = rnd.Next(0, 3);
                    y = rnd.Next(0, 3);
                    for (int i = 0; i < 4; i++)
                        newPosition[i] = new Point(Form1.SelfRef.game.LForms[f][i].X + x, Form1.SelfRef.game.LForms[f][i].Y + y);
                } while (!Form1.SelfRef.game.CheckCorrect(newPosition));
                Form1.SelfRef.game.Play(newPosition);
                do
                {
                    f = rnd.Next(0, 1);
                    x = rnd.Next(0, 3);
                    y = rnd.Next(0, 3);
                } while (!Form1.SelfRef.game.CheckStone(f, new Point(x, y)));
                Form1.SelfRef.game.Play(f, new Point(x, y));
                return;
            }
            if (Difficulty == Difficulties.Easy)
            {
                Player newPosition = new Player();
                int x, y, f;
                for (x = 0; x < 4; x++)
                    for (y = 0; y < 4; y++)
                        for (int i = 0; i < 8; i++)
                        {
                            LocalGame = new Game((Form1.SelfRef.game.IsStep(0) ? 0 : 1), Form1.SelfRef.game.player[0], Form1.SelfRef.game.player[1], Form1.SelfRef.game.stone[0], Form1.SelfRef.game.stone[1]);
                            for (int j = 0; j < 4; j++)
                                newPosition[j] = new Point(LocalGame.LForms[i][j].X + x, LocalGame.LForms[i][j].Y + y);
                            if (LocalGame.CheckCorrect(newPosition))
                            {
                                //LocalGame.Play(newPosition);
                                for (int i1 = 0; i1 < 4; i1++)
                                    for (int j1 = 0; j1 < 4; j1++)
                                        Field[i1, j1] = LocalGame.Field[i1, j1];
                                for (int stoneID = 0; stoneID < 2; stoneID++)
                                    for (int stoneX = 0; stoneX < 4; stoneX++)
                                        for (int stoneY = 0; stoneY < 4; stoneY++)
                                        {
                                            LocalGame = new Game((Form1.SelfRef.game.IsStep(0) ? 0 : 1), Form1.SelfRef.game.player[0], Form1.SelfRef.game.player[1], Form1.SelfRef.game.stone[0], Form1.SelfRef.game.stone[1]);
                                            LocalGame.Play(newPosition);
                                            if (LocalGame.CheckStone(stoneID, new Point(stoneX, stoneY)))
                                            {
                                                LocalGame.Play(stoneID, new Point(stoneX, stoneY));
                                                if (LocalGame.IsFinish())
                                                {
                                                    Form1.SelfRef.game.Play(newPosition, stoneID, new Point(stoneX, stoneY));
                                                    return;
                                                }
                                                for (int i1 = 0; i1 < 4; i1++)
                                                    for (int j1 = 0; j1 < 4; j1++)
                                                        LocalGame.Field[i1, j1] = Field[i1, j1];
                                                LocalGame.NextStep();
                                            }
                                        }
                                for (int i1 = 0; i1 < 4; i1++)
                                    for (int j1 = 0; j1 < 4; j1++)
                                        LocalGame.Field[i1, j1] = Form1.SelfRef.game.Field[i1, j1];
                            }
                        }
                do
                {
                    f = rnd.Next(0, 7);
                    x = rnd.Next(0, 3);
                    y = rnd.Next(0, 3);
                    for (int i = 0; i < 4; i++)
                        newPosition[i] = new Point(Form1.SelfRef.game.LForms[f][i].X + x, Form1.SelfRef.game.LForms[f][i].Y + y);
                } while (!Form1.SelfRef.game.CheckCorrect(newPosition));
                Form1.SelfRef.game.Play(newPosition);
                do
                {
                    f = rnd.Next(0, 1);
                    x = rnd.Next(0, 3);
                    y = rnd.Next(0, 3);
                } while (!Form1.SelfRef.game.CheckStone(f, new Point(x, y)));
                Form1.SelfRef.game.Play(f, new Point(x, y));
                return;
            }
            if (Difficulty == Difficulties.Medium)
            {
                Player newPosition = new Player();
                int x, y, f, cnt = 10000;
                for (x = 0; x < 4; x++)
                    for (y = 0; y < 4; y++)
                        for (int i = 0; i < 8; i++)
                        {
                            LocalGame = new Game((Form1.SelfRef.game.IsStep(0) ? 0 : 1), Form1.SelfRef.game.player[0], Form1.SelfRef.game.player[1], Form1.SelfRef.game.stone[0], Form1.SelfRef.game.stone[1]);
                            for (int j = 0; j < 4; j++)
                                newPosition[j] = new Point(LocalGame.LForms[i][j].X + x, LocalGame.LForms[i][j].Y + y);
                            if (LocalGame.CheckCorrect(newPosition))
                            {
                                //LocalGame.Play(newPosition);
                                for (int i1 = 0; i1 < 4; i1++)
                                    for (int j1 = 0; j1 < 4; j1++)
                                        Field[i1, j1] = LocalGame.Field[i1, j1];
                                for (int stoneID = 0; stoneID < 2; stoneID++)
                                    for (int stoneX = 0; stoneX < 4; stoneX++)
                                        for (int stoneY = 0; stoneY < 4; stoneY++)
                                        {
                                            LocalGame = new Game((Form1.SelfRef.game.IsStep(0) ? 0 : 1), Form1.SelfRef.game.player[0], Form1.SelfRef.game.player[1], Form1.SelfRef.game.stone[0], Form1.SelfRef.game.stone[1]);
                                            LocalGame.Play(newPosition);
                                            if (LocalGame.CheckStone(stoneID, new Point(stoneX, stoneY)))
                                            {
                                                LocalGame.Play(stoneID, new Point(stoneX, stoneY));
                                                if (LocalGame.IsFinish())
                                                {
                                                    Form1.SelfRef.game.Play(newPosition, stoneID, new Point(stoneX, stoneY));
                                                    return;
                                                }
                                                for (int i1 = 0; i1 < 4; i1++)
                                                    for (int j1 = 0; j1 < 4; j1++)
                                                        LocalGame.Field[i1, j1] = Field[i1, j1];
                                                LocalGame.NextStep();
                                            }
                                        }
                                for (int i1 = 0; i1 < 4; i1++)
                                    for (int j1 = 0; j1 < 4; j1++)
                                        LocalGame.Field[i1, j1] = Form1.SelfRef.game.Field[i1, j1];
                            }
                        }
                do
                {
                    f = rnd.Next(0, 7);
                    x = rnd.Next(0, 3);
                    y = rnd.Next(0, 3);
                    for (int i = 0; i < 4; i++)
                        newPosition[i] = new Point(Form1.SelfRef.game.LForms[f][i].X + x, Form1.SelfRef.game.LForms[f][i].Y + y);
                    if (Form1.SelfRef.game.CheckCorrect(newPosition))
                    {
                        LocalGame = new Game((Form1.SelfRef.game.IsStep(0) ? 0 : 1), Form1.SelfRef.game.player[0], Form1.SelfRef.game.player[1], Form1.SelfRef.game.stone[0], Form1.SelfRef.game.stone[1]);
                        LocalGame.Play(newPosition);
                        do
                        {
                            f = rnd.Next(0, 1);
                            x = rnd.Next(0, 3);
                            y = rnd.Next(0, 3);
                        } while (!LocalGame.CheckStone(f, new Point(x, y)));
                        LocalGame.Play(f, new Point(x, y));
                        if (LocalGame.IsFinish())
                        {
                            cnt--;
                            continue;
                        }
                        Form1.SelfRef.game.Play(newPosition, f, new Point(x, y));
                        return;
                    }
                    cnt--;
                } while (cnt > 0);
                // if all bad
                newPosition = new Player();
                do
                {
                    f = rnd.Next(0, 7);
                    x = rnd.Next(0, 3);
                    y = rnd.Next(0, 3);
                    for (int i = 0; i < 4; i++)
                        newPosition[i] = new Point(Form1.SelfRef.game.LForms[f][i].X + x, Form1.SelfRef.game.LForms[f][i].Y + y);
                } while (!Form1.SelfRef.game.CheckCorrect(newPosition));
                Form1.SelfRef.game.Play(newPosition);
                do
                {
                    f = rnd.Next(0, 1);
                    x = rnd.Next(0, 3);
                    y = rnd.Next(0, 3);
                } while (!Form1.SelfRef.game.CheckStone(f, new Point(x, y)));
                Form1.SelfRef.game.Play(f, new Point(x, y));
                return;
            }
        }
    }
}
