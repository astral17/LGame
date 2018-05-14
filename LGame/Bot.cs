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
        SortedSet<ulong> LosePositions = new SortedSet<ulong>(); // Player 1 - lose
        Game LocalGame = new Game();
        public enum Difficulties
        {
            Player = 0,
            Random = 1,
            Easy = 2, // if can finish game play, otherwise random
            Medium = 3, // if can finish game play, otherwise not lose in 1 move random
            Hard = 4, // if can finish game play, otherwise perfect protect
            //Perfect = 5, // theory of game, perfect attack and protect
        }
        public Bot()
        {
            //*
            int [,] Field2 = new int[4, 4];
            Field = new int[4, 4] {
                { 0, 3, 0, 0 },
                { 1, 1, 3, 0 },
                { 1, 2, 2, 2 },
                { 1, 2, 0, 0 }
            };
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    Field2[i, j] = Field[i, j];

            Console.WriteLine("LosePositions.Add({0});", Hash());

            // Rotate'n'Hash
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    Field[i, j] = Field2[3 - j, i];
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    Field2[i, j] = Field[i, j];
            Console.WriteLine("LosePositions.Add({0});", Hash());
            //
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    Field[i, j] = Field2[3 - j, i];
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    Field2[i, j] = Field[i, j];
            Console.WriteLine("LosePositions.Add({0});", Hash());
            //
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    Field[i, j] = Field2[3 - j, i];
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    Field2[i, j] = Field[i, j];
            Console.WriteLine("LosePositions.Add({0});", Hash());
            //
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    Field[i, j] = Field2[3 - j, i];
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    Field2[i, j] = Field[i, j];
            Console.WriteLine("LosePositions.Add({0});", Hash());

            // Mirror
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    Field2[i, j] = Field[i, j];
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    Field[i, j] = Field2[j, i];
            Console.WriteLine("LosePositions.Add({0});", Hash());
            // Rotate'n'Hash
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    Field[i, j] = Field2[3 - j, i];
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    Field2[i, j] = Field[i, j];
            Console.WriteLine("LosePositions.Add({0});", Hash());
            //
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    Field[i, j] = Field2[3 - j, i];
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    Field2[i, j] = Field[i, j];
            Console.WriteLine("LosePositions.Add({0});", Hash());
            //
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    Field[i, j] = Field2[3 - j, i];
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    Field2[i, j] = Field[i, j];
            Console.WriteLine("LosePositions.Add({0});", Hash());
            //*/
            //1
            LosePositions.Add(6901765930882);
            LosePositions.Add(11257908949540);
            LosePositions.Add(20571111125230);
            LosePositions.Add(5580434013772);
            LosePositions.Add(6901765930882);
            LosePositions.Add(7688827389772);
            LosePositions.Add(11257908949540);
            LosePositions.Add(20571111125230);
            LosePositions.Add(5580434013772);
            //1
            LosePositions.Add(6895851252256);
            LosePositions.Add(10967241917458);
            LosePositions.Add(6334361039056);
            LosePositions.Add(7864246401790);
            LosePositions.Add(6895851252256);
            LosePositions.Add(5654158171390);
            LosePositions.Add(10967241917458);
            LosePositions.Add(6334361039056);
            LosePositions.Add(7864246401790);
            //1
            LosePositions.Add(10579369669450);
            LosePositions.Add(16501726788226);
            LosePositions.Add(5567164130950);
            LosePositions.Add(7579786294510);
            LosePositions.Add(10579369669450);
            LosePositions.Add(11186432744110);
            LosePositions.Add(16501726788226);
            LosePositions.Add(5567164130950);
            LosePositions.Add(7579786294510);
            //2
            LosePositions.Add(21138518487682);
            LosePositions.Add(11008765779040);
            LosePositions.Add(6334358568430);
            LosePositions.Add(5829577184272);
            LosePositions.Add(21138518487682);
            LosePositions.Add(19896842701072);
            LosePositions.Add(11008765779040);
            LosePositions.Add(6334358568430);
            LosePositions.Add(5829577184272);
            //2
            LosePositions.Add(7186500989074);
            LosePositions.Add(10967241924640);
            LosePositions.Add(6334358568574);
            LosePositions.Add(5835509163472);
            LosePositions.Add(7186500989074);
            LosePositions.Add(5654160641872);
            LosePositions.Add(10967241924640);
            LosePositions.Add(6334358568574);
            LosePositions.Add(5835509163472);
            //2
            LosePositions.Add(5543424701950);
            LosePositions.Add(7769610603112);
            LosePositions.Add(11068050207250);
            LosePositions.Add(5843981757160);
            LosePositions.Add(5543424701950);
            LosePositions.Add(7186546278760);
            LosePositions.Add(7769610603112);
            LosePositions.Add(11068050207250);
            LosePositions.Add(5843981757160);
            //3
            LosePositions.Add(21138518480482);
            LosePositions.Add(11008765779922);
            LosePositions.Add(20571111118030);
            LosePositions.Add(5580434014654);
            LosePositions.Add(21138518480482);
            LosePositions.Add(19896842701054);
            LosePositions.Add(11008765779922);
            LosePositions.Add(20571111118030);
            LosePositions.Add(5580434014654);
            //3
            LosePositions.Add(7186500981874);
            LosePositions.Add(10967241925522);
            LosePositions.Add(20571111118174);
            LosePositions.Add(5586365993854);
            LosePositions.Add(7186500981874);
            LosePositions.Add(5654160641854);
            LosePositions.Add(10967241925522);
            LosePositions.Add(20571111118174);
            LosePositions.Add(5586365993854);
            //3
            LosePositions.Add(5543422937560);
            LosePositions.Add(11160443492794);
            LosePositions.Add(20576962631128);
            LosePositions.Add(5871664314742);
            LosePositions.Add(5543422937560);
            LosePositions.Add(7006569191542);
            LosePositions.Add(11160443492794);
            LosePositions.Add(20576962631128);
            LosePositions.Add(5871664314742);
            //3
            LosePositions.Add(6895851245056);
            LosePositions.Add(10967241918340);
            LosePositions.Add(20571113588656);
            LosePositions.Add(7615103232172);
            LosePositions.Add(6895851245056);
            LosePositions.Add(5654158171372);
            LosePositions.Add(10967241918340);
            LosePositions.Add(20571113588656);
            LosePositions.Add(7615103232172);
            //3
            LosePositions.Add(6895833957856);
            LosePositions.Add(10967241918466);
            LosePositions.Add(20577043098256);
            LosePositions.Add(5871101044846);
            LosePositions.Add(6895833957856);
            LosePositions.Add(5654158171246);
            LosePositions.Add(10967241918466);
            LosePositions.Add(20577043098256);
            LosePositions.Add(5871101044846);
            //4
            LosePositions.Add(7186518276274);
            LosePositions.Add(10967241924514);
            LosePositions.Add(6328429058974);
            LosePositions.Add(7579511350798);
            LosePositions.Add(7186518276274);
            LosePositions.Add(5654160641998);
            LosePositions.Add(10967241924514);
            LosePositions.Add(6328429058974);
            LosePositions.Add(7579511350798);
            //4
            LosePositions.Add(5834090919232);
            LosePositions.Add(7091387654968);
            LosePositions.Add(11062394937616);
            LosePositions.Add(5559287059768);
            LosePositions.Add(5834090919232);
            LosePositions.Add(7089659755768);
            LosePositions.Add(7091387654968);
            LosePositions.Add(11062394937616);
            LosePositions.Add(5559287059768);
            //4
            LosePositions.Add(5584947749614);
            LosePositions.Add(7091387647768);
            LosePositions.Add(11062394938498);
            LosePositions.Add(19796039609368);
            LosePositions.Add(5584947749614);
            LosePositions.Add(7089657286168);
            LosePositions.Add(7091387647768);
            LosePositions.Add(11062394938498);
            LosePositions.Add(19796039609368);
            //
            LosePositions.Add(19786108266922);
            LosePositions.Add(11880190309450);
            LosePositions.Add(20570753940454);
            LosePositions.Add(5580956930950);
            LosePositions.Add(19786108266922);
            LosePositions.Add(21346142714950);
            LosePositions.Add(11880190309450);
            LosePositions.Add(20570753940454);
            LosePositions.Add(5580956930950);
            //
            LosePositions.Add(19979884640650);
            LosePositions.Add(10523744168554);
            LosePositions.Add(20570742410950);
            LosePositions.Add(5584911584422);
            LosePositions.Add(19979884640650);
            LosePositions.Add(21152366341222);
            LosePositions.Add(10523744168554);
            LosePositions.Add(20570742410950);
            LosePositions.Add(5584911584422);
            //LosePositions.Add(16629706069096);
            //
            LosePositions.Add(11068050207250);
            LosePositions.Add(5843981757160);
            LosePositions.Add(5543424701950);
            LosePositions.Add(7769610603112);
            LosePositions.Add(11068050207250);
            LosePositions.Add(12323676449512);
            LosePositions.Add(5843981757160);
            LosePositions.Add(5543424701950);
            LosePositions.Add(7769610603112);
            //
            /*LosePositions.Add(7575872940658);
            LosePositions.Add(11066072892610);
            LosePositions.Add(5751047517790);
            LosePositions.Add(5733530542126);
            LosePositions.Add(7575872940658);
            LosePositions.Add(6334082210926);
            LosePositions.Add(11066072892610);
            LosePositions.Add(5751047517790);
            LosePositions.Add(5733530542126);*/
        }
        ulong Hash()
        {
            ulong hash = 0;
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                {
                    if (Field[i, j] == 4)
                        hash = hash * 7 + 4;
                    else
                        hash = hash * 7 + (ulong)Field[i, j] + 1;
                }
            return hash;
        }
        public void Run(Difficulties Difficulty = Difficulties.Random, int curStep = 0)
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
            if (Difficulty == Difficulties.Hard)
            {
                List<Tuple<int, int, int, int, int, int>> list = new List<Tuple<int, int, int, int, int, int>>();
                /*for (int i = 0; i < 4; i++)
                    for (int j = 0; j < 4; j++)
                        Field[i, j] = Form1.SelfRef.game.Field[i, j];
                Console.WriteLine(Hash());*/
                List<Tuple<int, int, int, int, int, int>> goodList = new List<Tuple<int, int, int, int, int, int>>();
                Player newPosition = new Player();
                int x, y, f, element;
                // Search move for win; save all correct positions
                for (x = 0; x < 4; x++)
                    for (y = 0; y < 4; y++)
                        for (int i = 0; i < 8; i++)
                        {
                            //LocalGame = new Game((Form1.SelfRef.game.IsStep(0) ? 0 : 1), Form1.SelfRef.game.player[0], Form1.SelfRef.game.player[1], Form1.SelfRef.game.stone[0], Form1.SelfRef.game.stone[1]);
                            if (curStep == 0)
                                LocalGame = new Game((Form1.SelfRef.game.IsStep(0) ? 0 : 1), Form1.SelfRef.game.player[0], Form1.SelfRef.game.player[1], Form1.SelfRef.game.stone[0], Form1.SelfRef.game.stone[1]);
                            else
                                LocalGame = new Game((Form1.SelfRef.game.IsStep(0) ? 1 : 0), Form1.SelfRef.game.player[1], Form1.SelfRef.game.player[0], Form1.SelfRef.game.stone[0], Form1.SelfRef.game.stone[1]);
                            for (int j = 0; j < 4; j++)
                                newPosition[j] = new Point(LocalGame.LForms[i][j].X + x, LocalGame.LForms[i][j].Y + y);
                            if (LocalGame.CheckCorrect(newPosition))
                            {
                                //LocalGame.Play(newPosition);
                                for (int stoneID = 0; stoneID < 2; stoneID++)
                                    for (int stoneX = 0; stoneX < 4; stoneX++)
                                        for (int stoneY = 0; stoneY < 4; stoneY++)
                                        {
                                            if (curStep == 0)
                                                LocalGame = new Game((Form1.SelfRef.game.IsStep(0) ? 0 : 1), Form1.SelfRef.game.player[0], Form1.SelfRef.game.player[1], Form1.SelfRef.game.stone[0], Form1.SelfRef.game.stone[1]);
                                            else
                                                LocalGame = new Game((Form1.SelfRef.game.IsStep(0) ? 1 : 0), Form1.SelfRef.game.player[1], Form1.SelfRef.game.player[0], Form1.SelfRef.game.stone[0], Form1.SelfRef.game.stone[1]);
                                            LocalGame.Play(newPosition);
                                            if (LocalGame.CheckStone(stoneID, new Point(stoneX, stoneY)))
                                            {
                                                LocalGame.Play(stoneID, new Point(stoneX, stoneY));
                                                list.Add(new Tuple<int, int, int, int, int, int>(x, y, i, stoneID, stoneX, stoneY));
                                                for (int i1 = 0; i1 < 4; i1++)
                                                    for (int j1 = 0; j1 < 4; j1++)
                                                        Field[i1, j1] = LocalGame.Field[i1, j1];
                                                if (LocalGame.IsFinish())// || LosePositions.Contains(Hash()))
                                                {
                                                    Form1.SelfRef.game.Play(newPosition, stoneID, new Point(stoneX, stoneY));
                                                    //goodList.Add(new Tuple<int, int, int, int, int, int>(x, y, i, stoneID, stoneX, stoneY));
                                                    return;
                                                }
                                            }
                                        }
                            }
                        }
                /*if (goodList.Count > 0)
                {
                    element = rnd.Next(0, goodList.Count - 1);
                    //element = 0;
                    for (int j = 0; j < 4; j++)
                        newPosition[j] = new Point(LocalGame.LForms[goodList[element].Item3][j].X + goodList[element].Item1, LocalGame.LForms[goodList[element].Item3][j].Y + goodList[element].Item2);
                    Form1.SelfRef.game.Play(newPosition, goodList[element].Item4, new Point(goodList[element].Item5, goodList[element].Item6));
                    return;
                }*/
                /*
                 * for (int i1 = 0; i1 < 4; i1++)
                                                    for (int j1 = 0; j1 < 4; j1++)
                                                        Field[i1, j1] = LocalGame.Field[i1, j1];
                                                if (!LosePositions.Contains(Hash()))
                                                    list.Add(new Tuple<int, int, int, int, int, int>(x, y, i, stoneID, stoneX, stoneY));
                 */
                //
                // Search move for not lose in 1 step; save all good positions
                //*
                for (element = 0; element < list.Count; element++)
                {
                    Player player1, player2;
                    int step = 0;
                    Point stone1, stone2;
                    bool isGood = true;
                    //LocalGame = new Game((Form1.SelfRef.game.IsStep(0) ? 0 : 1), Form1.SelfRef.game.player[0], Form1.SelfRef.game.player[1], Form1.SelfRef.game.stone[0], Form1.SelfRef.game.stone[1]);
                    if (curStep == 0)
                        LocalGame = new Game((Form1.SelfRef.game.IsStep(0) ? 0 : 1), Form1.SelfRef.game.player[0], Form1.SelfRef.game.player[1], Form1.SelfRef.game.stone[0], Form1.SelfRef.game.stone[1]);
                    else
                        LocalGame = new Game((Form1.SelfRef.game.IsStep(0) ? 1 : 0), Form1.SelfRef.game.player[1], Form1.SelfRef.game.player[0], Form1.SelfRef.game.stone[0], Form1.SelfRef.game.stone[1]);
                    for (int j = 0; j < 4; j++)
                        newPosition[j] = new Point(LocalGame.LForms[list[element].Item3][j].X + list[element].Item1, LocalGame.LForms[list[element].Item3][j].Y + list[element].Item2);
                    LocalGame.Play(newPosition, list[element].Item4, new Point(list[element].Item5, list[element].Item6));
                    step = (LocalGame.IsStep(0) ? 0 : 1);
                    if (step == 1)
                    {
                        player1 = LocalGame.player[0];
                        player2 = LocalGame.player[1];
                    }
                    else
                    {
                        player1 = LocalGame.player[1];
                        player2 = LocalGame.player[0];
                    }
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
                                                    for (int i1 = 0; i1 < 4; i1++)
                                                        for (int j1 = 0; j1 < 4; j1++)
                                                            Field[i1, j1] = LocalGame.Field[i1, j1];
                                                    if (LocalGame.IsFinish() || LosePositions.Contains(Hash()))
                                                    {
                                                        isGood = false;
                                                        goto finishHard;
                                                    }
                                                }
                                            }
                                }
                            }
                finishHard:
                    if (isGood)
                    {
                        goodList.Add(list[element]);
                        break;
                    }
                }

                if (goodList.Count > 0)
                {
                    element = rnd.Next(0, goodList.Count - 1);
                    //element = 0;
                    for (int j = 0; j < 4; j++)
                        newPosition[j] = new Point(LocalGame.LForms[goodList[element].Item3][j].X + goodList[element].Item1, LocalGame.LForms[goodList[element].Item3][j].Y + goodList[element].Item2);
                    Form1.SelfRef.game.Play(newPosition, goodList[element].Item4, new Point(goodList[element].Item5, goodList[element].Item6));
                    return;
                }
                //
                element = rnd.Next(0, list.Count - 1);
                for (int j = 0; j < 4; j++)
                    newPosition[j] = new Point(LocalGame.LForms[list[element].Item3][j].X + list[element].Item1, LocalGame.LForms[list[element].Item3][j].Y + list[element].Item2);
                Form1.SelfRef.game.Play(newPosition, list[element].Item4, new Point(list[element].Item5, list[element].Item6));
                return;
            }
        }
    }
}
