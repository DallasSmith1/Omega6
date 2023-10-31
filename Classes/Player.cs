using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Omega6.Classes
{
    public class Player
    {
        public Player(DiscordMember user, int wins, int losses, int mmr, int rank) 
        {
            Account = user;
            TotalWins = wins;
            TotalLosses = losses;
            MMR = mmr;
            Rank = rank;
            Preference = "Flex";
        }

        public Player(DiscordMember user, int wins, int losses, int mmr, int rank, string preference, DateTime qTime)
        {
            Account = user;
            TotalWins = wins;
            TotalLosses = losses;
            MMR = mmr;
            Rank = rank;
            Preference = preference;
            QTime = qTime;
        }

        public DiscordMember Account { get; set; }
        public int TotalWins { get; set; }
        public int TotalLosses { get; set; }
        public int MMR { get; set; }
        public int Rank { get; set; }

        public string Preference { get; set; }

        public DateTime QTime { get; set; }
    }
}
