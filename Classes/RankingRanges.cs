using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Omega6.Classes
{
    public class RankingRanges
    {
        #region Ranking
        public static int Rank1Start = 2000;
        public static int Rank1Demo = 1800;

        public static int Rank2Promo = 950;
        public static int Rank2Start = 700;
        public static int Rank2Demo = 550;

        public static int Rank3Promo = 600;
        public static int Rank3Start = 400;
        public static int Rank3Demo = 250;

        public static int Rank4Promo = 300;
        public static int Rank4Start = 100;

        public static int BaseMMR = 15;

        public static int MinMMRGain = 3;

        public static bool CanQueue = true;
        #endregion

        #region Roles
        public static ulong Rank1Role = 1110727923419328583;
        public static ulong Rank2Role = 1110728008249126942;
        public static ulong Rank3Role = 1110728104751665174;
        public static ulong Rank4Role = 1110728142076788748;
        public static ulong ModRole = 1110246096791425024;
        public static ulong BanRole = 1119414449636380712;
        #endregion

        #region Channels
        public static ulong LeaderboardCHannelID = 1112887345537101884;
        public static ulong ModChannelID = 1112281476306829414;
        public static ulong RankCheckID = 1110718883112947772;
        public static ulong RulesChannelID = 1110245890259697767;
        public static ulong Rank1Category = 1112295020771414057;
        public static ulong Rank2Category = 1112295140585898076;
        public static ulong Rank3Category = 1112295213147377756;
        public static ulong Rank4Category = 1112295317661044787;
        public static ulong RankUniversalCategory = 1112878711465836665;
        public static ulong RankCheckTicket = 1115089037468901488;
        public static ulong AnnouncementsChannelID = 1110245890259697768;
        #endregion

        public static DiscordEmbedBuilder LeaderboardEmbed = null;
        public static int WaitTime = 14;

        public static string[] RankNames = { "Rank S", "Rank A", "Rank B", "Rank C" };


        public static DiscordMessage Leaderboard = null;
        public static DateTime LastUpdated = DateTime.Now.AddHours(5);

        /// <summary>
        /// gets the rank name
        /// </summary>
        /// <param name="rank"></param>
        /// <returns></returns>
        public static string GetRankString(int rank)
        {
            return RankNames[rank-1];
        }
    }
}
