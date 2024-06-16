using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LGame
{
    [SupportedOSPlatform("windows")]
    public partial class GameForm : Form
    {
        public readonly int CellSize = 150;
        public Game game = new Game();

        private LShape selected = new LShape();
        private int SelectedStone = -1;
        private Phase StepPhase = Phase.MovePlayer;
        private bool isMouseDown = false;

        public enum Phase
        {
            MovePlayer = 0,
            SelectStone = 1,
            MoveStone = 2,
            Finished = 3,
            WaitBot = 4,
        }
        public GameForm()
        {            
            InitializeComponent();
            selected.Clear();
            game.InterruptBot = true;
            //game.RegisterBot(0, Bot.Difficulties.Medium);
            //game.RegisterBot(1, Bot.Difficulties.Medium);
            game.RegisterBot(1, Bot.Difficulties.Hard);
            if (game.IsBotStep())
            {
                StepPhase = Phase.WaitBot;
                timer1.Interval = 10;
                timer1.Start();
            }
            
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!game.BotStep())
            {
                StepPhase = Phase.MovePlayer;
                timer1.Stop();
            }
            if (game.IsFinish())
            {
                StepPhase = Phase.Finished;
                timer1.Stop();
            }
            pictureBox1.Refresh();
            /*timer1.Stop();
            StepPhase = Phase.WaitBot;
            while (game.BotStep())
            {
                pictureBox1.Refresh();
            }
            pictureBox1.Refresh();*/
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            /*LShape tmp = new LShape();
            tmp[0] = new Point(2, 0);
            tmp[1] = new Point(2, 1);
            tmp[2] = new Point(2, 2);
            tmp[3] = new Point(3, 2);
            game.Play(tmp);*/
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            System.Threading.Monitor.Enter(e.Graphics);
            // Draw Map
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    switch (game.Field[i, j])
                    {
                        case FieldTile.None:
                            e.Graphics.FillRectangle(Brushes.White, i * CellSize, j * CellSize, CellSize, CellSize);
                            break;
                        case FieldTile.BluePlayer:
                            e.Graphics.FillRectangle(Brushes.Blue, i * CellSize, j * CellSize, CellSize, CellSize);
                            break;
                        case FieldTile.RedPlayer:
                            e.Graphics.FillRectangle(Brushes.Red, i * CellSize, j * CellSize, CellSize, CellSize);
                            break;
                        case FieldTile.YellowStone:
                            if (SelectedStone == 0)
                            {
                                if (game.IsPlayerStep(0))
                                    e.Graphics.FillRectangle(Brushes.LightBlue, i * CellSize, j * CellSize, CellSize, CellSize);
                                else
                                    e.Graphics.FillRectangle(Brushes.Pink, i * CellSize, j * CellSize, CellSize, CellSize);
                            }
                            else
                                e.Graphics.FillRectangle(Brushes.White, i * CellSize, j * CellSize, CellSize, CellSize);
                            e.Graphics.FillEllipse(Brushes.Yellow, i * CellSize, j * CellSize, CellSize, CellSize);
                            break;
                        case FieldTile.GreenStone:
                            if (SelectedStone == 1)
                            {
                                if (game.IsPlayerStep(0))
                                    e.Graphics.FillRectangle(Brushes.LightBlue, i * CellSize, j * CellSize, CellSize, CellSize);
                                else
                                    e.Graphics.FillRectangle(Brushes.Pink, i * CellSize, j * CellSize, CellSize, CellSize);
                            }
                            else
                                e.Graphics.FillRectangle(Brushes.White, i * CellSize, j * CellSize, CellSize, CellSize);
                            e.Graphics.FillEllipse(Brushes.Green, i * CellSize, j * CellSize, CellSize, CellSize);
                            break;
                    }
                    e.Graphics.DrawRectangle(Pens.Black, i * CellSize, j * CellSize, CellSize, CellSize);
                }
            }
            // Draw Selected
            for (int i = 0; i < 4; i++)
            {
                if (0 <= selected[i].X && selected[i].X < 4 && 0 <= selected[i].Y && selected[i].Y < 4)
                {
                    if (game.IsPlayerStep(0))
                    {
                        e.Graphics.FillRectangle(Brushes.LightBlue, selected[i].X * CellSize, selected[i].Y * CellSize, CellSize, CellSize);
                        e.Graphics.DrawRectangle(Pens.Black, selected[i].X * CellSize, selected[i].Y * CellSize, CellSize, CellSize);
                    }
                    if (game.IsPlayerStep(1))
                    {
                        e.Graphics.FillRectangle(Brushes.Pink, selected[i].X * CellSize, selected[i].Y * CellSize, CellSize, CellSize);
                        e.Graphics.DrawRectangle(Pens.Black, selected[i].X * CellSize, selected[i].Y * CellSize, CellSize, CellSize);
                    }
                }
            }
            // Congratulations
            if (StepPhase == Phase.Finished)
            {
                if (game.IsStep(0))
                    e.Graphics.DrawString("Red Won!", new Font("Arial", 32), Brushes.Pink, pictureBox1.Width / 2 - 120, pictureBox1.Height / 2 - 24);
                if (game.IsStep(1))
                    e.Graphics.DrawString("Blue Won!", new Font("Arial", 32), Brushes.LightBlue, pictureBox1.Width / 2 - 120, pictureBox1.Height / 2 - 24);
            }
            System.Threading.Monitor.Exit(e.Graphics);
        }

        private bool DrawNewPosition(Point newPoint) // Return true if need update screen
        {
            if (0 > newPoint.X || newPoint.X >= 4 || 0 > newPoint.Y || newPoint.Y >= 4)
                return false;
            for (int i = 0; i < 4; i++)
            {
                if (selected[i] == newPoint)
                    return false;
                if (selected[i].X == -1 && selected[i].Y == -1)
                {
                    selected[i] = newPoint;
                    return true;
                }
            }
            selected.Clear();
            return true;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            /*if (game.IsWait)
                game.NextStep();*/
            Point newPoint;
            switch (StepPhase)
            {
                case Phase.MovePlayer:
                    isMouseDown = true;
                    newPoint = new Point(e.X / CellSize, e.Y / CellSize);
                    if (DrawNewPosition(newPoint))
                        pictureBox1.Refresh();
                    break;
                case Phase.SelectStone:
                    //newPoint = new Point(e.X / CellSize, e.Y / CellSize);

                    break;
                case Phase.MoveStone:

                    break;
            }
        }
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown)
            {
                Point newPoint = new Point(e.X / CellSize, e.Y / CellSize);
                if (DrawNewPosition(newPoint))
                    pictureBox1.Refresh();
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            Point newPoint;
            switch (StepPhase)
            {
                case Phase.MovePlayer:
                    if (game.Play(selected))
                        StepPhase = Phase.SelectStone;
                    selected.Clear();
                    isMouseDown = false;
                    pictureBox1.Refresh();
                    break;
                case Phase.SelectStone:
                    newPoint = new Point(e.X / CellSize, e.Y / CellSize);
                    if (0 > newPoint.X || newPoint.X >= 4 || 0 > newPoint.Y || newPoint.Y >= 4)
                        return;
                    if ((int)game.Field[newPoint.X, newPoint.Y] > 2)
                    {
                        SelectedStone = (int)game.Field[newPoint.X, newPoint.Y] - 3;
                        StepPhase = Phase.MoveStone;
                    }
                    pictureBox1.Refresh();
                    break;
                case Phase.MoveStone:
                    newPoint = new Point(e.X / CellSize, e.Y / CellSize);
                    if (0 > newPoint.X || newPoint.X >= 4 || 0 > newPoint.Y || newPoint.Y >= 4)
                        return;
                    if (game.Play(SelectedStone, newPoint))
                    {
                        SelectedStone = -1;
                        pictureBox1.Refresh();
                        StepPhase = Phase.WaitBot;
                        while (game.BotStep())
                        {
                            pictureBox1.Refresh();
                        }
                        StepPhase = Phase.MovePlayer;
                        if (game.IsFinish())
                            StepPhase = Phase.Finished;
                    }
                    pictureBox1.Refresh();
                    break;
            }
        }
    }
}
