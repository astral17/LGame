using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace LGame
{
    public class Game : GameField
    {
        public Bot bot = new Bot();
        Bot.Difficulties[] LevelBot = new Bot.Difficulties[2]; // 0 - Player
        public bool InterruptBot = false;

        public Game() : base()
        {
            LevelBot[0] = Bot.Difficulties.Player;
            LevelBot[1] = Bot.Difficulties.Player;
        }

        public bool IsPlayerStep(int player)
        {
            if (player < 0 || player > 1 || LevelBot[player] > 0)
                return false;
            return PlayerStep == player;
        }
        public bool IsBotStep()
        {
            return LevelBot[PlayerStep] > 0;
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

        public bool BotStep() // Return is bot step
        {
            //IsWait = false;
            if (LevelBot[PlayerStep] > 0)
            {
                // Sheduler Bot
                if (IsFinish())
                    return false;
                bot.Run(this, LevelBot[PlayerStep]);
                if (InterruptBot)
                    return LevelBot[PlayerStep] > 0;
                //NextStep();
            }
            return LevelBot[PlayerStep] > 0;
        }
    }
}
