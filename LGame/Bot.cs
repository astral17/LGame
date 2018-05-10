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
                List<Tuple<int, int, int, int, int, int>> list = new List<Tuple<int, int, int, int, int, int>>();
                List<Tuple<int, int, int, int, int, int>> goodList = new List<Tuple<int, int, int, int, int, int>>();
                Player newPosition = new Player();
                int x, y, f, element;
                // Search move for win; save all correct positions
                for (x = 0; x < 4; x++)
                    for (y = 0; y < 4; y++)
                        for (int i = 0; i < 8; i++)
                        {
                            LocalGame = new Game((Form1.SelfRef.game.IsStep(0) ? 0 : 1), Form1.SelfRef.game.player[0], Form1.SelfRef.game.player[1], Form1.SelfRef.game.stone[0], Form1.SelfRef.game.stone[1]);
                            for (int j = 0; j < 4; j++)
                                newPosition[j] = new Point(LocalGame.LForms[i][j].X + x, LocalGame.LForms[i][j].Y + y);
                            if (LocalGame.CheckCorrect(newPosition))
                            {
                                for (int stoneID = 0; stoneID < 2; stoneID++)
                                    for (int stoneX = 0; stoneX < 4; stoneX++)
                                        for (int stoneY = 0; stoneY < 4; stoneY++)
                                        {
                                            LocalGame = new Game((Form1.SelfRef.game.IsStep(0) ? 0 : 1), Form1.SelfRef.game.player[0], Form1.SelfRef.game.player[1], Form1.SelfRef.game.stone[0], Form1.SelfRef.game.stone[1]);
                                            LocalGame.Play(newPosition);
                                            if (LocalGame.CheckStone(stoneID, new Point(stoneX, stoneY)))
                                            {
                                                list.Add(new Tuple<int, int, int, int, int, int>(x, y, i, stoneID, stoneX, stoneY));
                                            }
                                        }
                            }
                        }

                element = rnd.Next(0, list.Count - 1);
                for (int j = 0; j < 4; j++)
                    newPosition[j] = new Point(LocalGame.LForms[list[element].Item3][j].X + list[element].Item1, LocalGame.LForms[list[element].Item3][j].Y + list[element].Item2);
                Form1.SelfRef.game.Play(newPosition, list[element].Item4, new Point(list[element].Item5, list[element].Item6));
                return;
            }
            if (Difficulty == Difficulties.Easy)
            {
                List<Tuple<int, int, int, int, int, int>> list = new List<Tuple<int, int, int, int, int, int>>();
                Player newPosition = new Player();
                int x, y, f, element;
                // Search move for win; save all correct positions
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
                                                list.Add(new Tuple<int, int, int, int, int, int>(x, y, i, stoneID, stoneX, stoneY));
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
                element = rnd.Next(0, list.Count - 1);
                for (int j = 0; j < 4; j++)
                    newPosition[j] = new Point(LocalGame.LForms[list[element].Item3][j].X + list[element].Item1, LocalGame.LForms[list[element].Item3][j].Y + list[element].Item2);
                Form1.SelfRef.game.Play(newPosition, list[element].Item4, new Point(list[element].Item5, list[element].Item6));
                return;
            }
            if (Difficulty == Difficulties.Medium)
            {
                List<Tuple<int, int, int, int, int, int>> list = new List<Tuple<int, int, int, int, int, int>>();
                List<Tuple<int, int, int, int, int, int>> goodList = new List<Tuple<int, int, int, int, int, int>>();
                Player newPosition = new Player();
                int x, y, f, element;
                // Search move for win; save all correct positions
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
                                for (int stoneID = 0; stoneID < 2; stoneID++)
                                    for (int stoneX = 0; stoneX < 4; stoneX++)
                                        for (int stoneY = 0; stoneY < 4; stoneY++)
                                        {
                                            LocalGame = new Game((Form1.SelfRef.game.IsStep(0) ? 0 : 1), Form1.SelfRef.game.player[0], Form1.SelfRef.game.player[1], Form1.SelfRef.game.stone[0], Form1.SelfRef.game.stone[1]);
                                            LocalGame.Play(newPosition);
                                            if (LocalGame.CheckStone(stoneID, new Point(stoneX, stoneY)))
                                            {
                                                list.Add(new Tuple<int, int, int, int, int, int>(x, y, i, stoneID, stoneX, stoneY));
                                                LocalGame.Play(stoneID, new Point(stoneX, stoneY));
                                                if (LocalGame.IsFinish())
                                                {
                                                    Form1.SelfRef.game.Play(newPosition, stoneID, new Point(stoneX, stoneY));
                                                    return;
                                                }
                                            }
                                        }
                            }
                        }
                // Search move for not lose in 1 step; save all good positions
                //*
                for (element = 0; element < list.Count; element++)
                {
                    Player player1, player2;
                    int step = 0;
                    Point stone1, stone2;
                    bool isGood = true;
                    LocalGame = new Game((Form1.SelfRef.game.IsStep(0) ? 0 : 1), Form1.SelfRef.game.player[0], Form1.SelfRef.game.player[1], Form1.SelfRef.game.stone[0], Form1.SelfRef.game.stone[1]);
                    for (int j = 0; j < 4; j++)
                        newPosition[j] = new Point(LocalGame.LForms[list[element].Item3][j].X + list[element].Item1, LocalGame.LForms[list[element].Item3][j].Y + list[element].Item2);
                    LocalGame.Play(newPosition, list[element].Item4, new Point(list[element].Item5, list[element].Item6));
                    player1 = LocalGame.player[0];
                    player2 = LocalGame.player[1];
                    step = (LocalGame.IsStep(0) ? 0 : 1);
                    stone1 = LocalGame.stone[0];
                    stone2 = LocalGame.stone[1];
                    for (x = 0; x < 4; x++)
                        for (y = 0; y < 4; y++)
                            for (int i = 0; i < 8; i++)
                            {
                                LocalGame = new Game(step, player1, player2, stone1, stone2);
                                for (int j = 0; j < 4; j++)
                                    newPosition[j] = new Point(LocalGame.LForms[i][j].X + x, LocalGame.LForms[i][j].Y + y);
                                if (LocalGame.CheckCorrect(newPosition))
                                {
                                    //LocalGame.Play(newPosition);
                                    for (int stoneID = 0; stoneID < 2; stoneID++)
                                        for (int stoneX = 0; stoneX < 4; stoneX++)
                                            for (int stoneY = 0; stoneY < 4; stoneY++)
                                            {
                                                LocalGame = new Game(step, player1, player2, stone1, stone2);
                                                LocalGame.Play(newPosition);
                                                if (LocalGame.CheckStone(stoneID, new Point(stoneX, stoneY)))
                                                {
                                                    LocalGame.Play(stoneID, new Point(stoneX, stoneY));
                                                    if (LocalGame.IsFinish())
                                                    {
                                                        isGood = false;
                                                        goto finish;
                                                    }
                                                }
                                            }
                                }
                            }
                finish:
                    if (isGood)
                    {
                        goodList.Add(list[element]);
                        //break;
                    }
                }//*/

                if (goodList.Count > 0)
                {
                    element = rnd.Next(0, goodList.Count - 1);
                    //element = 0;
                    for (int j = 0; j < 4; j++)
                        newPosition[j] = new Point(LocalGame.LForms[goodList[element].Item3][j].X + goodList[element].Item1, LocalGame.LForms[goodList[element].Item3][j].Y + goodList[element].Item2);
                    Form1.SelfRef.game.Play(newPosition, goodList[element].Item4, new Point(goodList[element].Item5, goodList[element].Item6));
                    return;
                }
                element = rnd.Next(0, list.Count - 1);
                for (int j = 0; j < 4; j++)
                    newPosition[j] = new Point(LocalGame.LForms[list[element].Item3][j].X + list[element].Item1, LocalGame.LForms[list[element].Item3][j].Y + list[element].Item2);
                Form1.SelfRef.game.Play(newPosition, list[element].Item4, new Point(list[element].Item5, list[element].Item6));

                return;
            }
        }
    }
}
