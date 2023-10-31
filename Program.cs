using System;
using Omega6.Classes;
using Omega6.Commands;

namespace Omega6
{
    class Program
    { 
        static void Main(string[] args)
        {
            System.Timers.Timer timer = new System.Timers.Timer(new TimeSpan(1,0,0));
            timer.Elapsed += Timer_Elapsed;
            timer.Start();

            Bot bot = new Bot();
            bot.RunAsync().GetAwaiter().GetResult();
        }


        /// <summary>
        /// every hour, update leaderboard and delete leftover channels
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static async void Timer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                MyMongoDB.UpdateRanks();
                CustomCommands.UpdateLeaderboard(MyMongoDB.storedCTX);
                await Game.CleanUpChannels(MyMongoDB.storedCTX);
                CustomCommands.AutoUnbanPlayers();
            }
            catch { }
        }
    }
}