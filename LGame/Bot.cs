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
        SortedDictionary<ulong, int> priority = new SortedDictionary<ulong, int>(); // less - better
        Game LocalGame = new Game();
        public enum Difficulties
        {
            Player = 0,
            Random = 1,
            Easy = 2, // if can finish game play, otherwise random
            Medium = 3, // if can finish game play, otherwise not lose in 1 move random
            Hard = 4, // perfect attack, perfect protect
            //Perfect = 5, // theory of game, perfect attack and protect
        }
        public Bot()
        {
            /*
            int [,] Field2 = new int[4, 4];
            Field = new int[4, 4] {
                { 0, 0, 3, 0 },
                { 2, 2, 2, 0 },
                { 2, 1, 3, 0 },
                { 0, 1, 1, 1 }
            };
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    Field2[i, j] = Field[i, j];

            bool DBG = false;
            //Console.WriteLine("LosePositions.Add({0});", Hash());

            // Rotate'n'Hash
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    Field[i, j] = Field2[3 - j, i];
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    Field2[i, j] = Field[i, j];
            Console.WriteLine("LosePositions.Add({0});", Hash());
            if (DBG)
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                        Console.Write(Field[i, j]);
                    Console.WriteLine();
                }
            }
            //
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    Field[i, j] = Field2[3 - j, i];
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    Field2[i, j] = Field[i, j];
            Console.WriteLine("LosePositions.Add({0});", Hash());
            if (DBG)
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                        Console.Write(Field[i, j]);
                    Console.WriteLine();
                }
            }
            //
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    Field[i, j] = Field2[3 - j, i];
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    Field2[i, j] = Field[i, j];
            Console.WriteLine("LosePositions.Add({0});", Hash());
            if (DBG)
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                        Console.Write(Field[i, j]);
                    Console.WriteLine();
                }
            }
            //
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    Field[i, j] = Field2[3 - j, i];
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    Field2[i, j] = Field[i, j];
            Console.WriteLine("LosePositions.Add({0});", Hash());
            if (DBG)
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                        Console.Write(Field[i, j]);
                    Console.WriteLine();
                }
            }
            // Mirror
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    Field[i, j] = Field2[i, 3 - j];
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    Field2[i, j] = Field[i, j];
            Console.WriteLine("LosePositions.Add({0});", Hash());
            if (DBG)
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                        Console.Write(Field[i, j]);
                    Console.WriteLine();
                }
            }
            // Rotate'n'Hash
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    Field[i, j] = Field2[3 - j, i];
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    Field2[i, j] = Field[i, j];
            Console.WriteLine("LosePositions.Add({0});", Hash());
            if (DBG)
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                        Console.Write(Field[i, j]);
                    Console.WriteLine();
                }
            }
            //
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    Field[i, j] = Field2[3 - j, i];
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    Field2[i, j] = Field[i, j];
            Console.WriteLine("LosePositions.Add({0});", Hash());
            if (DBG)
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                        Console.Write(Field[i, j]);
                    Console.WriteLine();
                }
            }
            //
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    Field[i, j] = Field2[3 - j, i];
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    Field2[i, j] = Field[i, j];
            Console.WriteLine("LosePositions.Add({0});", Hash());
            if (DBG)
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                        Console.Write(Field[i, j]);
                    Console.WriteLine();
                }
            }
            //*/
            //1
            LosePositions.Add(11257908949540);priority[11257908949540] = 1;
            LosePositions.Add(20571111125230);priority[20571111125230] = 1;
            LosePositions.Add(5580434013772);priority[5580434013772] = 1;
            LosePositions.Add(6901765930882);priority[6901765930882] = 1;
            LosePositions.Add(5732698056430);priority[5732698056430] = 1;
            LosePositions.Add(19784049666340);priority[19784049666340] = 1;
            LosePositions.Add(11105644906882);priority[11105644906882] = 1;
            LosePositions.Add(7688827389772); priority[7688827389772] = 1;
            //1
            LosePositions.Add(10967241917458); priority[10967241917458] = 1;
            LosePositions.Add(6334361039056);priority[6334361039056] = 1;
            LosePositions.Add(7864246401790);priority[7864246401790] = 1;
            LosePositions.Add(6895851252256);priority[6895851252256] = 1;
            LosePositions.Add(5738615211856);priority[5738615211856] = 1;
            LosePositions.Add(7866701386258);priority[7866701386258] = 1;
            LosePositions.Add(11064135876256);priority[11064135876256] = 1;
            LosePositions.Add(5654158171390);priority[5654158171390] = 1;
            //1
            LosePositions.Add(16501726788226);priority[16501726788226] = 1;
            LosePositions.Add(5567164130950);priority[5567164130950] = 1;
            LosePositions.Add(7579786294510);priority[7579786294510] = 1;
            LosePositions.Add(10579369669450);priority[10579369669450] = 1;
            LosePositions.Add(7593593114950);priority[7593593114950] = 1;
            LosePositions.Add(5830111644226);priority[5830111644226] = 1;
            LosePositions.Add(15038546058250);priority[15038546058250] = 1;
            LosePositions.Add(11186432744110);priority[11186432744110] = 1;
            //2
            LosePositions.Add(11008765779040);priority[11008765779040] = 2;
            LosePositions.Add(6334358568430);priority[6334358568430] = 2;
            LosePositions.Add(5829577184272);priority[5829577184272] = 2;
            LosePositions.Add(21138518487682);priority[21138518487682] = 2;
            LosePositions.Add(5774207093230);priority[5774207093230] = 2;
            LosePositions.Add(7576034355040);priority[7576034355040] = 2;
            LosePositions.Add(11064135870082);priority[11064135870082] = 2;
            LosePositions.Add(19896842701072);priority[19896842701072] = 2;
            //2
            LosePositions.Add(10967241924640);priority[10967241924640] = 2;
            LosePositions.Add(6334358568574);priority[6334358568574] = 2;
            LosePositions.Add(5835509163472);priority[5835509163472] = 2;
            LosePositions.Add(7186500989074);priority[7186500989074] = 2;
            LosePositions.Add(7767352450174);priority[7767352450174] = 2;
            LosePositions.Add(7576051649440);priority[7576051649440] = 2;
            LosePositions.Add(11064135869074);priority[11064135869074] = 2;
            LosePositions.Add(5654160641872);priority[5654160641872] = 2;
            //2
            LosePositions.Add(7769610603112);priority[7769610603112] = 2;
            LosePositions.Add(11068050207250);priority[11068050207250] = 2;
            LosePositions.Add(5843981757160);priority[5843981757160] = 2;
            LosePositions.Add(5543424701950);priority[5543424701950] = 2;
            LosePositions.Add(5539481679250);priority[5539481679250] = 2;
            LosePositions.Add(12323676449512);priority[12323676449512] = 2;
            LosePositions.Add(6334089505150);priority[6334089505150] = 2;
            LosePositions.Add(7186546278760);priority[7186546278760] = 2;
            //3
            LosePositions.Add(11008765779922);priority[11008765779922] = 3;
            LosePositions.Add(20571111118030);priority[20571111118030] = 3;
            LosePositions.Add(5580434014654);priority[5580434014654] = 3;
            LosePositions.Add(21138518480482);priority[21138518480482] = 3;
            LosePositions.Add(5774204623630);priority[5774204623630] = 3;
            LosePositions.Add(19784049666322);priority[19784049666322] = 3;
            LosePositions.Add(11105642437282);priority[11105642437282] = 3;
            LosePositions.Add(19896842701054);priority[19896842701054] = 3;
            //3
            LosePositions.Add(10967241925522);priority[10967241925522] = 3;
            LosePositions.Add(20571111118174);priority[20571111118174] = 3;
            LosePositions.Add(5586365993854);priority[5586365993854] = 3;
            LosePositions.Add(7186500981874);priority[7186500981874] = 3;
            LosePositions.Add(7767349980574);priority[7767349980574] = 3;
            LosePositions.Add(19784066960722);priority[19784066960722] = 3;
            LosePositions.Add(11105642436274);priority[11105642436274] = 3;
            LosePositions.Add(5654160641854);priority[5654160641854] = 3;
            //3
            LosePositions.Add(11160443492794);priority[11160443492794] = 3;
            LosePositions.Add(20576962631128);priority[20576962631128] = 3;
            LosePositions.Add(5871664314742);priority[5871664314742] = 3;
            LosePositions.Add(5543422937560);priority[5543422937560] = 3;
            LosePositions.Add(5539481658328);priority[5539481658328] = 3;
            LosePositions.Add(21818799351994);priority[21818799351994] = 3;
            LosePositions.Add(11105096460760);priority[11105096460760] = 3;
            LosePositions.Add(7006569191542);priority[7006569191542] = 3;
            //3
            LosePositions.Add(10967241918340);priority[10967241918340] = 3;
            LosePositions.Add(20571113588656);priority[20571113588656] = 3;
            LosePositions.Add(7615103232172);priority[7615103232172] = 3;
            LosePositions.Add(6895851245056);priority[6895851245056] = 3;
            LosePositions.Add(5738612742256);priority[5738612742256] = 3;
            LosePositions.Add(20074716697540);priority[20074716697540] = 3;
            LosePositions.Add(11105642443456);priority[11105642443456] = 3;
            LosePositions.Add(5654158171372);priority[5654158171372] = 3;
            //3
            LosePositions.Add(10967241918466);priority[10967241918466] = 3;
            LosePositions.Add(20577043098256);priority[20577043098256] = 3;
            LosePositions.Add(5871101044846);priority[5871101044846] = 3;
            LosePositions.Add(6895833957856);priority[6895833957856] = 3;
            LosePositions.Add(5732683232656);priority[5732683232656] = 3;
            LosePositions.Add(21818718884866);priority[21818718884866] = 3;
            LosePositions.Add(11105659730656);priority[11105659730656] = 3;
            LosePositions.Add(5654158171246);priority[5654158171246] = 3;
            //4
            LosePositions.Add(10967241924514);priority[10967241924514] = 4;
            LosePositions.Add(6328429058974);priority[6328429058974] = 4;
            LosePositions.Add(7579511350798);priority[7579511350798] = 4;
            LosePositions.Add(7186518276274);priority[7186518276274] = 4;
            LosePositions.Add(7773281959774);priority[7773281959774] = 4;
            LosePositions.Add(5832049462114);priority[5832049462114] = 4;
            LosePositions.Add(11064118581874);priority[11064118581874] = 4;
            LosePositions.Add(5654160641998);priority[5654160641998] = 4;
            //4
            LosePositions.Add(7091387654968);priority[7091387654968] = 4;
            LosePositions.Add(11062394937616);priority[11062394937616] = 4;
            LosePositions.Add(5559287059768);priority[5559287059768] = 4;
            LosePositions.Add(5834090919232);priority[5834090919232] = 4;
            LosePositions.Add(7574148542416);priority[7574148542416] = 4;
            LosePositions.Add(10289307000568);priority[10289307000568] = 4;
            LosePositions.Add(6332135237632);priority[6332135237632] = 4;
            LosePositions.Add(7089659755768);priority[7089659755768] = 4;
            //4
            LosePositions.Add(7091387647768);priority[7091387647768] = 4;
            LosePositions.Add(11062394938498);priority[11062394938498] = 4;
            LosePositions.Add(19796039609368);priority[19796039609368] = 4;
            LosePositions.Add(5584947749614);priority[5584947749614] = 4;
            LosePositions.Add(19782163853698);priority[19782163853698] = 4;
            LosePositions.Add(10330813567768);priority[10330813567768] = 4;
            LosePositions.Add(6332135237614);priority[6332135237614] = 4;
            LosePositions.Add(7089657286168);priority[7089657286168] = 4;
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
                /*
                for (int i = 0; i < 4; i++)
                    for (int j = 0; j < 4; j++)
                        Field[i, j] = Form1.SelfRef.game.Field[i, j];
                Console.WriteLine(Hash());//*/
                List<Tuple<int, int, int, int, int, int>> goodList = new List<Tuple<int, int, int, int, int, int>>();
                Player newPosition = new Player();
                int x, y, f, element = -1, bestPriority = 123;
                ulong hash;
                // Search move for win; save all correct positions
                for (x = 0; x < 4; x++)
                    for (y = 0; y < 4; y++)
                        for (int i = 0; i < 8; i++)
                        {
                            LocalGame = new Game(1, Form1.SelfRef.game.player[1 - curStep], Form1.SelfRef.game.player[curStep], Form1.SelfRef.game.stone[0], Form1.SelfRef.game.stone[1]);
                            for (int j = 0; j < 4; j++)
                                newPosition[j] = new Point(LocalGame.LForms[i][j].X + x, LocalGame.LForms[i][j].Y + y);
                            if (LocalGame.CheckCorrect(newPosition))
                            {
                                for (int stoneID = 0; stoneID < 2; stoneID++)
                                    for (int stoneX = 0; stoneX < 4; stoneX++)
                                        for (int stoneY = 0; stoneY < 4; stoneY++)
                                        {
                                            //LocalGame = new Game(curStep, Form1.SelfRef.game.player[curStep], Form1.SelfRef.game.player[1 - curStep], Form1.SelfRef.game.stone[0], Form1.SelfRef.game.stone[1]);
                                            LocalGame = new Game(1, Form1.SelfRef.game.player[1 - curStep], Form1.SelfRef.game.player[curStep], Form1.SelfRef.game.stone[0], Form1.SelfRef.game.stone[1]);
                                            LocalGame.Play(newPosition);
                                            if (LocalGame.CheckStone(stoneID, new Point(stoneX, stoneY)))
                                            {
                                                LocalGame.Play(stoneID, new Point(stoneX, stoneY));
                                                list.Add(new Tuple<int, int, int, int, int, int>(x, y, i, stoneID, stoneX, stoneY));
                                                for (int i1 = 0; i1 < 4; i1++)
                                                    for (int j1 = 0; j1 < 4; j1++)
                                                        Field[i1, j1] = LocalGame.Field[i1, j1];
                                                if (LocalGame.IsFinish())
                                                {
                                                    Form1.SelfRef.game.Play(newPosition, stoneID, new Point(stoneX, stoneY));
                                                    //Console.WriteLine(Hash());
                                                    //goodList.Add(new Tuple<int, int, int, int, int, int>(x, y, i, stoneID, stoneX, stoneY));
                                                    return;
                                                }
                                                hash = Hash();
                                                if (LosePositions.Contains(hash))
                                                {
                                                    if (priority[hash] < bestPriority)
                                                    {
                                                        bestPriority = priority[hash];
                                                        goodList.Add(new Tuple<int, int, int, int, int, int>(x, y, i, stoneID, stoneX, stoneY));
                                                        //element = list.Count - 1;
                                                        element = goodList.Count - 1;
                                                    }
                                                    //Form1.SelfRef.game.Play(newPosition, stoneID, new Point(stoneX, stoneY));
                                                    //Console.WriteLine(Hash());
                                                    //goodList.Add(new Tuple<int, int, int, int, int, int>(x, y, i, stoneID, stoneX, stoneY));
                                                    //return;
                                                }
                                            }
                                        }
                            }
                        }
                if (element != -1)
                {
                    for (int j = 0; j < 4; j++)
                        newPosition[j] = new Point(LocalGame.LForms[goodList[element].Item3][j].X + goodList[element].Item1, LocalGame.LForms[goodList[element].Item3][j].Y + goodList[element].Item2);
                    Form1.SelfRef.game.Play(newPosition, goodList[element].Item4, new Point(goodList[element].Item5, goodList[element].Item6));
                    return;
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
                    Point stone1, stone2;
                    bool isGood = true;
                    LocalGame = new Game(0, Form1.SelfRef.game.player[curStep], Form1.SelfRef.game.player[1 - curStep], Form1.SelfRef.game.stone[0], Form1.SelfRef.game.stone[1]);
                    for (int j = 0; j < 4; j++)
                        newPosition[j] = new Point(LocalGame.LForms[list[element].Item3][j].X + list[element].Item1, LocalGame.LForms[list[element].Item3][j].Y + list[element].Item2);
                    LocalGame.Play(newPosition, list[element].Item4, new Point(list[element].Item5, list[element].Item6));
                    //step = (LocalGame.IsStep(0) ? 0 : 1);
                    player1 = LocalGame.player[0];
                    player2 = LocalGame.player[1];
                    stone1 = LocalGame.stone[0];
                    stone2 = LocalGame.stone[1];
                    for (x = 0; x < 4; x++)
                        for (y = 0; y < 4; y++)
                            for (int i = 0; i < 8; i++)
                            {
                                LocalGame = new Game(1, player1, player2, stone1, stone2);
                                for (int j = 0; j < 4; j++)
                                    newPosition[j] = new Point(LocalGame.LForms[i][j].X + x, LocalGame.LForms[i][j].Y + y);
                                if (LocalGame.CheckCorrect(newPosition))
                                {
                                    //LocalGame.Play(newPosition);
                                    for (int stoneID = 0; stoneID < 2; stoneID++)
                                        for (int stoneX = 0; stoneX < 4; stoneX++)
                                            for (int stoneY = 0; stoneY < 4; stoneY++)
                                            {
                                                LocalGame = new Game(1, player1, player2, stone1, stone2);
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
                        //break;
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
