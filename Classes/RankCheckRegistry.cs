using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Omega6.Classes
{
    public class RankCheckRegistry
    {
        public RankCheckRegistry(ulong linkedDiscordUser, string omegaStrikerName, int startrank)
        {
            LinkedDiscordUser = linkedDiscordUser;
            OmegaStrikerName = omegaStrikerName;
            StartingRank = startrank;
        }

        public RankCheckRegistry(ulong linkedDiscordUser, string omegaStrikerName)
        {
            LinkedDiscordUser = linkedDiscordUser;
            OmegaStrikerName = omegaStrikerName;
            StartingRank = 0;
        }

        public ulong LinkedDiscordUser { get; set; }
        public string OmegaStrikerName { get; set; }
        public int StartingRank { get; set; }

        /// <summary>
        /// checks to see if the in game name has already ben used
        /// </summary>
        /// <returns></returns>
        public bool InGameNameAlreadyInUse()
        {
            RankCheckRegistry registry1 = MyMongoDB.GetRankCheck(OmegaStrikerName);
            RankCheckRegistry registry2 = MyMongoDB.GetRankCheck(LinkedDiscordUser);

            if (registry1.Equals(registry2))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
