using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Extensions;
using DSharpPlus.Net.Models;
using Microsoft.VisualBasic;
using MongoDB.Bson;
using MongoDB.Driver.Core.Operations;
using Omega6.Classes.Exceptions;
using Omega6.Commands;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Omega6.Classes
{
    public class Game
    {
        public static List<Game> PreGames = new List<Game>();
        public static List<Game> OnGoingGames = new List<Game>();

        private static List<Player> PlayersInQ_NA_1 = new List<Player>();
        private static List<Player> PlayersInQ_NA_2 = new List<Player>();
        private static List<Player> PlayersInQ_NA_3 = new List<Player>();
        private static List<Player> PlayersInQ_NA_4 = new List<Player>();
        private static List<Player> PlayersInQ_EU_1 = new List<Player>();
        private static List<Player> PlayersInQ_EU_2 = new List<Player>();
        private static List<Player> PlayersInQ_EU_3 = new List<Player>();
        private static List<Player> PlayersInQ_EU_4 = new List<Player>();
        private static List<Player> PlayersInQ_UN_NA = new List<Player>();
        private static List<Player> PlayersInQ_UN_EU = new List<Player>();


        public Game(string id, List<Player> players, bool universal = false)
        {
            ID = id;
            Players = new List<Player>();
            foreach (Player player in players)
            {
                Players.Add(player);
            }
            Team1 = new List<Player> { };
            Team2 = new List<Player> { };
            StartTime = DateTime.Now;
            WinningTeam = string.Empty;
            Universal = universal;
        }

        public Game(string id, List<Player> players, List<Player> team1, List<Player> team2, DateTime start, DateTime end, string winning_team, bool universal = false)
        {
            ID = id;
            Players = new List<Player>();
            foreach (Player player in players)
            {
                Players.Add(player);
            }
            Team1 = new List<Player> { };
            Team2 = new List<Player> { };
            StartTime = DateTime.Now;
            WinningTeam = string.Empty;
            Universal = universal;
        }

        #region Variables
        public string ID { get; set; }
        public List<Player> Players { get; set; }
        public List<Player> Team1 { get; set; }
        public List<Player> Team2 { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string WinningTeam { get; set; }
        public System.Threading.Timer timer { get; set; }
        public bool Universal { get; set; }
        public string LobbyPass { get; set; }
        #endregion


        /// <summary>
        /// adds the given player to the q, if q is full it returns the list of players
        /// </summary>
        /// <param name="player"></param>
        public static Game? AddPlayerToQ(Player player, string region, int rank)
        {
            if (region == "NA")
            {
                if (rank  == 1)
                {
                    if (PlayerExistsInList(PlayersInQ_NA_1, player))
                    {
                        throw new UserAlreadyInQueueException("Already in queue!");
                    }
                    PlayersInQ_NA_1.Add(player);
                    if (PlayersInQ_NA_1.Count == 6)
                    {
                        string id = "";
                        return new Game(id, PlayersInQ_NA_1);
                    }
                    else
                    {
                        return null;
                    }
                }
                else if (rank == 2)
                {
                    if (PlayerExistsInList(PlayersInQ_NA_2, player))
                    {
                        throw new UserAlreadyInQueueException("Already in queue!");
                    }
                    PlayersInQ_NA_2.Add(player);
                    if (PlayersInQ_NA_2.Count == 6)
                    {
                        string id = "";
                        return new Game(id, PlayersInQ_NA_2);
                    }
                    else
                    {
                        return null;
                    }
                }
                else if (rank == 3)
                {
                    if (PlayerExistsInList(PlayersInQ_NA_3, player))
                    {
                        throw new UserAlreadyInQueueException("Already in queue!");
                    }
                    PlayersInQ_NA_3.Add(player);
                    if (PlayersInQ_NA_3.Count == 6)
                    {
                        string id = "";
                        return new Game(id, PlayersInQ_NA_3);
                    }
                    else
                    {
                        return null;
                    }
                }
                else if (rank == 4)
                {
                    if (PlayerExistsInList(PlayersInQ_NA_4, player))
                    {
                        throw new UserAlreadyInQueueException("Already in queue!");
                    }
                    PlayersInQ_NA_4.Add(player);
                    if (PlayersInQ_NA_4.Count == 6)
                    {
                        string id = "";
                        return new Game(id, PlayersInQ_NA_4);
                    }
                    else
                    {
                        return null;
                    }
                }
                else if (rank == 0)
                {
                    if (PlayerExistsInList(PlayersInQ_UN_NA, player))
                    {
                        throw new UserAlreadyInQueueException("Already in queue!");
                    }
                    PlayersInQ_UN_NA.Add(player);
                    if (PlayersInQ_UN_NA.Count == 6)
                    {
                        string id = "";
                        return new Game(id, PlayersInQ_UN_NA);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            else if(region == "EU")
            {
                if (rank == 1)
                {
                    if (PlayerExistsInList(PlayersInQ_EU_1, player))
                    {
                        throw new UserAlreadyInQueueException("Already in queue!");
                    }
                    PlayersInQ_EU_1.Add(player);
                    if (PlayersInQ_EU_1.Count == 6)
                    {
                        string id = "";
                        return new Game(id, PlayersInQ_EU_1);
                    }
                    else
                    {
                        return null;
                    }
                }
                else if (rank == 2)
                {
                    if (PlayerExistsInList(PlayersInQ_EU_2, player))
                    {
                        throw new UserAlreadyInQueueException("Already in queue!");
                    }
                    PlayersInQ_EU_2.Add(player);
                    if (PlayersInQ_EU_2.Count == 6)
                    {
                        string id = "";
                        return new Game(id, PlayersInQ_EU_2);
                    }
                    else
                    {
                        return null;
                    }
                }
                else if (rank == 3)
                {
                    if (PlayerExistsInList(PlayersInQ_EU_3, player))
                    {
                        throw new UserAlreadyInQueueException("Already in queue!");
                    }
                    PlayersInQ_EU_3.Add(player);
                    if (PlayersInQ_EU_3.Count == 6)
                    {
                        string id = "";
                        return new Game(id, PlayersInQ_EU_3);
                    }
                    else
                    {
                        return null;
                    }
                }
                else if (rank == 4)
                {
                    if (PlayerExistsInList(PlayersInQ_EU_4, player))
                    {
                        throw new UserAlreadyInQueueException("Already in queue!");
                    }
                    PlayersInQ_EU_4.Add(player);
                    if (PlayersInQ_EU_4.Count == 6)
                    {
                        string id = "";
                        return new Game(id, PlayersInQ_EU_4);
                    }
                    else
                    {
                        return null;
                    }
                }
                else if (rank == 0)
                {
                    if (PlayerExistsInList(PlayersInQ_UN_EU, player))
                    {
                        throw new UserAlreadyInQueueException("Already in queue!");
                    }
                    PlayersInQ_UN_EU.Add(player);
                    if (PlayersInQ_UN_EU.Count == 6)
                    {
                        string id = "";
                        return new Game(id, PlayersInQ_UN_EU);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// removes player from the q
        /// </summary>
        /// <param name="player"></param>
        public static void RemovePlayerFromQ(Player player, string region, int rank)
        {
            if (region == "NA")
            {
                if (rank == 1)
                {
                    if (!PlayerExistsInList(PlayersInQ_NA_1, player))
                    {
                        throw new UserNotInQueueException();
                    }
                    PlayersInQ_NA_1 = RemovePlayerFromlist(PlayersInQ_NA_1, player);
                }
                else if (rank == 2)
                {
                    if (!PlayerExistsInList(PlayersInQ_NA_2, player))
                    {
                        throw new UserNotInQueueException();
                    }
                    PlayersInQ_NA_2 = RemovePlayerFromlist(PlayersInQ_NA_2, player);
                }
                else if (rank == 3)
                {
                    if (!PlayerExistsInList(PlayersInQ_NA_3, player))
                    {
                        throw new UserNotInQueueException();
                    }
                    PlayersInQ_NA_3 = RemovePlayerFromlist(PlayersInQ_NA_3, player);
                }
                else if (rank == 4)
                {
                    if (!PlayerExistsInList(PlayersInQ_NA_4, player))
                    {
                        throw new UserNotInQueueException();
                    }
                    PlayersInQ_NA_4 = RemovePlayerFromlist(PlayersInQ_NA_4, player);
                }
                else if (rank == 0)
                {
                    if (!PlayerExistsInList(PlayersInQ_UN_NA, player))
                    {
                        throw new UserNotInQueueException();
                    }
                    PlayersInQ_UN_NA = RemovePlayerFromlist(PlayersInQ_UN_NA, player);
                }
            }
            else if (region == "EU")
            {
                if (rank == 1)
                {
                    if (!PlayerExistsInList(PlayersInQ_EU_1, player))
                    {
                        throw new UserNotInQueueException();
                    }
                    PlayersInQ_EU_1 = RemovePlayerFromlist(PlayersInQ_EU_1, player);
                }
                else if (rank == 2)
                {
                    if (!PlayerExistsInList(PlayersInQ_EU_2, player))
                    {
                        throw new UserNotInQueueException();
                    }
                    PlayersInQ_EU_2 = RemovePlayerFromlist(PlayersInQ_EU_2, player);
                }
                else if (rank == 3)
                {
                    if (!PlayerExistsInList(PlayersInQ_EU_3, player))
                    {
                        throw new UserNotInQueueException();
                    }
                    PlayersInQ_EU_3 = RemovePlayerFromlist(PlayersInQ_EU_3, player);
                }
                else if (rank == 4)
                {
                    if (!PlayerExistsInList(PlayersInQ_EU_4, player))
                    {
                        throw new UserNotInQueueException();
                    }
                    PlayersInQ_EU_4 = RemovePlayerFromlist(PlayersInQ_EU_4, player);
                }
                else if (rank == 0)
                {
                    if (!PlayerExistsInList(PlayersInQ_UN_EU, player))
                    {
                        throw new UserNotInQueueException();
                    }
                    PlayersInQ_UN_EU = RemovePlayerFromlist(PlayersInQ_UN_EU, player);
                }
            }
        }

        /// <summary>
        /// gets the numbner of players int he q
        /// </summary>
        /// <param name="region"></param>
        /// <param name="rank"></param>
        /// <returns></returns>
        public static int GetNumberPlayersInQ(string region, int rank)
        {
            if (region == "NA")
            {
                if(rank == 1)
                {
                    return PlayersInQ_NA_1.Count;
                }
                else if (rank == 2)
                {
                    return PlayersInQ_NA_2.Count;
                }
                else if (rank == 3)
                {
                    return PlayersInQ_NA_3.Count;
                }
                else if (rank == 4)
                {
                    return PlayersInQ_NA_4.Count;
                }
                else if (rank == 0)
                {
                    return PlayersInQ_UN_NA.Count;
                }
            }
            else if(region == "EU")
            {
                if (rank == 1)
                {
                    return PlayersInQ_EU_1.Count;
                }
                else if (rank == 2)
                {
                    return PlayersInQ_EU_2.Count;
                }
                else if (rank == 3)
                {
                    return PlayersInQ_EU_3.Count;
                }
                else if (rank == 4)
                {
                    return PlayersInQ_EU_4.Count;
                }
                else if (rank == 0)
                {
                    return PlayersInQ_UN_EU.Count;
                }
            }
            return 0;
        }

        /// <summary>
        /// returns the list of players in the q
        /// </summary>
        /// <param name="region"></param>
        /// <param name="rank"></param>
        /// <returns></returns>
        public static List<Player>? GetPlayersInQ(string region, int rank)
        {
            if (region == "NA")
            {
                if (rank == 1)
                {
                    return PlayersInQ_NA_1;
                }
                else if (rank == 2)
                {
                    return PlayersInQ_NA_2;
                }
                else if (rank == 3)
                {
                    return PlayersInQ_NA_3;
                }
                else if (rank == 4)
                {
                    return PlayersInQ_NA_4;
                }
                else if (rank == 0)
                {
                    return PlayersInQ_UN_NA;
                }
            }
            else if (region == "EU")
            {
                if (rank == 1)
                {
                    return PlayersInQ_EU_1;
                }
                else if (rank == 2)
                {
                    return PlayersInQ_EU_2;
                }
                else if (rank == 3)
                {
                    return PlayersInQ_EU_3;
                }
                else if (rank == 4)
                {
                    return PlayersInQ_EU_4;
                }
                else if (rank == 0)
                {
                    return PlayersInQ_UN_EU;
                }
            }
            return null;
        }

        /// <summary>
        /// crerates a match
        /// </summary>
        /// <param name="region"></param>
        /// <param name="rank"></param>
        /// <returns></returns>
        public static async void CreateMatch(CommandContext ctx, string region, int rank)
        {
            Game game = null;

            DiscordChannel channel = null;

            if (region == "NA")
            {
                if (rank == 1)
                {
                    List<Player> captains = PickCaptains(PlayersInQ_NA_1);
                    game = MyMongoDB.CreateGame(PlayersInQ_NA_1, captains[0], captains[1]);
                    PlayersInQ_NA_1.Clear();
                    channel = CustomCommands.GetChannel(RankingRanges.Rank1Category);
                }
                else if (rank == 2)
                {
                    List<Player> captains = PickCaptains(PlayersInQ_NA_2);
                    game = MyMongoDB.CreateGame(PlayersInQ_NA_2, captains[0], captains[1]);
                    PlayersInQ_NA_2.Clear();
                    channel = CustomCommands.GetChannel(RankingRanges.Rank2Category);
                }
                else if (rank == 3)
                {
                    List<Player> captains = PickCaptains(PlayersInQ_NA_3);
                    game = MyMongoDB.CreateGame(PlayersInQ_NA_3, captains[0], captains[1]);
                    PlayersInQ_NA_3.Clear();
                    channel = CustomCommands.GetChannel(RankingRanges.Rank3Category);
                }
                else if (rank == 4)
                {
                    List<Player> captains = PickCaptains(PlayersInQ_NA_4);
                    game = MyMongoDB.CreateGame(PlayersInQ_NA_4, captains[0], captains[1]);
                    PlayersInQ_NA_4.Clear();
                    channel = CustomCommands.GetChannel(RankingRanges.Rank4Category);
                }
                else if (rank == 0)
                {
                    List<Player> captains = PickCaptains(PlayersInQ_UN_NA);
                    game = MyMongoDB.CreateGame(PlayersInQ_UN_NA, captains[0], captains[1], true);
                    PlayersInQ_UN_NA.Clear();
                    channel = CustomCommands.GetChannel(RankingRanges.RankUniversalCategory);
                }
            }
            else if (region == "EU")
            {
                if (rank == 1)
                {
                    List<Player> captains = PickCaptains(PlayersInQ_EU_1);
                    game = MyMongoDB.CreateGame(PlayersInQ_EU_1, captains[0], captains[1]);
                    PlayersInQ_EU_1.Clear();
                    channel = CustomCommands.GetChannel(RankingRanges.Rank1Category);
                }
                else if (rank == 2)
                {
                    List<Player> captains = PickCaptains(PlayersInQ_EU_2);
                    game = MyMongoDB.CreateGame(PlayersInQ_EU_2, captains[0], captains[1]);
                    PlayersInQ_EU_2.Clear();
                    channel = CustomCommands.GetChannel(RankingRanges.Rank2Category);
                }
                else if (rank == 3)
                {
                    List<Player> captains = PickCaptains(PlayersInQ_EU_3);
                    game = MyMongoDB.CreateGame(PlayersInQ_EU_3, captains[0], captains[1]);
                    PlayersInQ_EU_3.Clear();
                    channel = CustomCommands.GetChannel(RankingRanges.Rank3Category);
                }
                else if(rank == 4)
                {
                    List<Player> captains = PickCaptains(PlayersInQ_EU_4);
                    game = MyMongoDB.CreateGame(PlayersInQ_EU_4, captains[0], captains[1]);
                    PlayersInQ_EU_4.Clear();
                    channel = CustomCommands.GetChannel(RankingRanges.Rank4Category);
                }
                else if (rank == 0)
                {
                    List<Player> captains = PickCaptains(PlayersInQ_UN_EU);
                    game = MyMongoDB.CreateGame(PlayersInQ_UN_EU, captains[0], captains[1], true);
                    PlayersInQ_UN_EU.Clear();
                    channel = CustomCommands.GetChannel(RankingRanges.RankUniversalCategory);
                }
            }

            PreGames.Add(game);

            List<Player> playersLeft = new List<Player>();

            foreach (Player player in game.Players)
            {
                playersLeft.Add(player);
            }

            await ctx.Channel.SendMessageAsync(new DiscordEmbedBuilder
            {
                Color = DiscordColor.Green,
                Title = "Lobby " + game.ID + " has been created!",
                Description = "The team captains for this game are:\nTeam 1: " + game.Team1[0].Account.Mention + "\nTeam 2: " + game.Team2[0].Account.Mention +
                                "\nTeam captains are now choosing their teams."
            });

            foreach (Player player in playersLeft)
            {
                await player.Account.SendMessageAsync(player.Account.Mention + ": your queue has filled. Please be ready to join your team channels after teams are chosen.");
            }

            await game.Team2[0].Account.SendMessageAsync(game.Team2[0].Account.Mention + ": your queue has filled, and you are 2nd captain. Please be ready to pick your teammates.");

            #region Cap 1 Vote

            DiscordEmbedBuilder cap1 = new DiscordEmbedBuilder
            {
                Color = DiscordColor.White,
                Title = "You are first captain. (7 minutes to choose)",
                Description = PrintStats(OrganizePlayers(playersLeft))
            };

            var interactivity = MyMongoDB.storedCTX.Client.GetInteractivity();
            
            var embed = await game.Team1[0].Account.SendMessageAsync(embed: cap1).ConfigureAwait(false);

            var choice1 = DiscordEmoji.FromName(MyMongoDB.storedCTX.Client, ":one:");
            var choice2 = DiscordEmoji.FromName(MyMongoDB.storedCTX.Client, ":two:");
            var choice3 = DiscordEmoji.FromName(MyMongoDB.storedCTX.Client, ":three:");
            var choice4 = DiscordEmoji.FromName(MyMongoDB.storedCTX.Client, ":four:");
               
            await embed.CreateReactionAsync(choice1);
            await embed.CreateReactionAsync(choice2);
            await embed.CreateReactionAsync(choice3);
            await embed.CreateReactionAsync(choice4);


            var result = await interactivity.WaitForReactionAsync(
                x => x.Message == embed &&
                x.User.Id == game.Team1[0].Account.Id &&
                (x.Emoji == choice1 || x.Emoji == choice2 || x.Emoji == choice3 || x.Emoji == choice4), embed, game.Team1[0].Account, TimeSpan.FromMinutes(7)).ConfigureAwait(false);

            if (result.Result == null)
            {
                await ctx.Channel.SendMessageAsync(CancelMatch(game));
                return;
            }

            if (result.Result.Emoji == choice1)
            {
                game.Team1.Add(playersLeft[0]);
                await playersLeft[0].Account.SendMessageAsync("You have been picked to be on " + game.Team1[0].Account.DisplayName + "'s team.");
                playersLeft.Remove(playersLeft[0]);
            }
            else if (result.Result.Emoji == choice2)
            {
                game.Team1.Add(playersLeft[1]);
                await playersLeft[1].Account.SendMessageAsync("You have been picked to be on " + game.Team1[0].Account.DisplayName + "'s team.");
                playersLeft.Remove(playersLeft[1]);
            }
            else if (result.Result.Emoji == choice3)
            {
                game.Team1.Add(playersLeft[2]);
                await playersLeft[2].Account.SendMessageAsync("You have been picked to be on " + game.Team1[0].Account.DisplayName + "'s team.");
                playersLeft.Remove(playersLeft[2]);
            }
            else if (result.Result.Emoji == choice4)
            {
                game.Team1.Add(playersLeft[3]);
                await playersLeft[3].Account.SendMessageAsync("You have been picked to be on " + game.Team1[0].Account.DisplayName + "'s team.");
                playersLeft.Remove(playersLeft[3]);
            }

            await game.Team1[0].Account.SendMessageAsync("You picked " + game.Team1[1].Account.Mention +" to be on your team.").ConfigureAwait(false);


            #endregion

            #region Cap 2 Vote
            DiscordEmbedBuilder cap2 = new DiscordEmbedBuilder
            {
                Color = DiscordColor.White,
                Title = "You are second captain. [Pick 2] (7 minutes to choose)",
                Description = PrintStats(OrganizePlayers(playersLeft))
            };

             embed = await game.Team2[0].Account.SendMessageAsync(embed: cap2).ConfigureAwait(false);

             choice1 = DiscordEmoji.FromName(MyMongoDB.storedCTX.Client, ":one:");
             choice2 = DiscordEmoji.FromName(MyMongoDB.storedCTX.Client, ":two:");
             choice3 = DiscordEmoji.FromName(MyMongoDB.storedCTX.Client, ":three:");

            await embed.CreateReactionAsync(choice1);
            await embed.CreateReactionAsync(choice2);
            await embed.CreateReactionAsync(choice3);

            interactivity = MyMongoDB.storedCTX.Client.GetInteractivity();

            int choices = 0;
            int firstChoice = 3;
            List<Player> playersChosen = new List<Player>();
            while (choices < 2)
            {
                result = await interactivity.WaitForReactionAsync(
                x => x.Message == embed &&
                x.User.Id == game.Team2[0].Account.Id &&
                (x.Emoji == choice1 || x.Emoji == choice2 || x.Emoji == choice3), embed, game.Team2[0].Account, TimeSpan.FromMinutes(7)).ConfigureAwait(false);

                if (result.Result == null)
                {
                    await ctx.Channel.SendMessageAsync(CancelMatch(game));
                    return;
                }

                if (result.Result.Emoji == choice1)
                {
                    if (firstChoice == 0)
                    {
                        await game.Team2[0].Account.SendMessageAsync(AlreadyPicked(playersLeft[0])).ConfigureAwait(false);
                    }
                    else
                    {
                        game.Team2.Add(playersLeft[0]);
                        playersChosen.Add(playersLeft[0]);
                        firstChoice = 0;
                        choices++;

                        if (choices == 1)
                        {
                            cap2.Description = PrintStats(OrganizePlayers(playersLeft), 1);
                            await embed.ModifyAsync((DiscordEmbed)cap2);
                        }

                        await game.Team2[0].Account.SendMessageAsync("You picked " + game.Team2[choices].Account.Mention + " to be on your team.").ConfigureAwait(false);
                        await playersLeft[0].Account.SendMessageAsync("You have been picked to be on " + game.Team2[0].Account.DisplayName + "'s team.");
                    }
                }
                else if (result.Result.Emoji == choice2)
                {
                    if (firstChoice == 1)
                    {
                        await game.Team2[0].Account.SendMessageAsync(AlreadyPicked(playersLeft[1])).ConfigureAwait(false);
                    }
                    else
                    {
                        game.Team2.Add(playersLeft[1]);
                        playersChosen.Add(playersLeft[1]);
                        firstChoice = 1;
                        choices++;

                        if (choices == 1)
                        {
                            cap2.Description = PrintStats(OrganizePlayers(playersLeft), 2);
                            await embed.ModifyAsync((DiscordEmbed)cap2);
                        }

                        await game.Team2[0].Account.SendMessageAsync("You picked " + game.Team2[choices].Account.Mention + " to be on your team.").ConfigureAwait(false);
                        await playersLeft[1].Account.SendMessageAsync("You have been picked to be on " + game.Team2[0].Account.DisplayName + "'s team.");
                    }
                }
                else if (result.Result.Emoji == choice3)
                {
                    if (firstChoice == 2)
                    {
                        await game.Team2[0].Account.SendMessageAsync(AlreadyPicked(playersLeft[2])).ConfigureAwait(false);
                    }
                    else
                    {
                        game.Team2.Add(playersLeft[2]);
                        playersChosen.Add(playersLeft[2]);
                        firstChoice = 2;
                        choices++;
                        if (choices == 1)
                        {
                            cap2.Description = PrintStats(OrganizePlayers(playersLeft), 3);
                            await embed.ModifyAsync((DiscordEmbed)cap2);
                        }

                        await game.Team2[0].Account.SendMessageAsync("You picked " + game.Team2[choices].Account.Mention + " to be on your team.").ConfigureAwait(false);
                        await playersLeft[2].Account.SendMessageAsync("You have been picked to be on " + game.Team2[0].Account.DisplayName + "'s team.");
                    }
                }
            }

            foreach (Player player in playersLeft)
            {
                bool found = false;

                if (player.Account.Id == playersChosen[0].Account.Id)
                {
                    found = true;
                }
                else if (player.Account.Id == playersChosen[1].Account.Id)
                {
                    found = true;
                }

                if (!found)
                {
                    game.Team1.Add(player);
                }
            }

            #endregion

            MyMongoDB.UpdateGame(game);

            //var builder = new DiscordOverwriteBuilder[] { new DiscordOverwriteBuilder(ctx.Guild.GetRole(RankingRanges.Rank3Role)).Allow(Permissions.UseVoice | Permissions.Speak) };

            DiscordChannel team1 = await ctx.Guild.CreateChannelAsync("("+game.ID+") Team 1", ChannelType.Voice, channel, null,null,3, null,null,null,null,null,null, AutoArchiveDuration.Hour, null,  null, null);

            foreach (Player player in game.Team1)
            {
                await team1.AddOverwriteAsync(player.Account, Permissions.Speak | Permissions.UseVoice);
                Thread.Sleep(1000);
            }

            DiscordChannel team2 = await ctx.Guild.CreateChannelAsync("(" + game.ID + ") Team 2", ChannelType.Voice, channel, null, null, 3, null, null, null, null, null, null, AutoArchiveDuration.Hour, null, null, null);

            foreach (Player player in game.Team2)
            {
                await team2.AddOverwriteAsync(player.Account, Permissions.Speak | Permissions.UseVoice);
                Thread.Sleep(1000);
            }

            MyMongoDB.SaveVoiceChannels(game.ID, team1.Id, team2.Id);

            await ctx.Channel.SendMessageAsync(StartMatch(game));

            SendGameInfo(game, team1, team2);
        }

        /// <summary>
        /// starts the game 
        /// </summary>
        /// <param name="game"></param>
        /// <returns></returns>
        public static DiscordEmbedBuilder StartMatch(Game game)
        {
            foreach (Game currentGame in PreGames)
            {
                if (currentGame.ID == game.ID)
                {
                    OnGoingGames.Add(currentGame);
                    PreGames.Remove(currentGame);
                    break;
                }
            }

            return new DiscordEmbedBuilder
            {
                Color = DiscordColor.Green,
                Title = "Start Match " + game.ID,
                Description = PrintGameBanner(game)
            };
        }

        /// <summary>
        /// reports ther outcome of a game
        /// </summary>
        /// <param name="player"></param>
        /// <param name="win"></param>
        /// <returns></returns>
        public static DiscordEmbedBuilder ReportMatch(string gameid, Player player, bool win)
        {
            DiscordEmbedBuilder embed = new DiscordEmbedBuilder
            {
                Color = DiscordColor.Green,
                Description = string.Empty
                
            };

            foreach (Game game in OnGoingGames)
            {
                if (game.ID.Equals(gameid))
                {
                    try
                    {
                        Game newGame = MyMongoDB.UpdateGame(game, player, win);

                        // delete game specific channels

                        //BsonDocument doc = MyMongoDB.GetVoiceChannels(game.ID);

                        //CustomCommands.GetChannel(game.Players[0].Account, ulong.Parse(doc.GetElement("team1VC").Value.ToString())).DeleteAsync();
                        //CustomCommands.GetChannel(game.Players[0].Account, ulong.Parse(doc.GetElement("team2VC").Value.ToString())).DeleteAsync();

                        embed.Title = "Reported Successfully!";
                        embed.Description = "Game "+game.ID+" reported on " +game.EndTime.ToString();
                        return embed;
                    }
                    catch (GameNotFoundException ){ }
                    catch (GameAlreadyReportedException )
                    {
                        embed.Color = DiscordColor.Red;
                        embed.Title = "Game was already reported!";
                        embed.Description = "Please do not report the same games repeatedly.";
                        return embed;
                    }
                    catch (TooEarlyException )
                    {
                        embed.Color = DiscordColor.Red;
                        embed.Title = "Suspicious Report!";
                        embed.Description = "User: "+player.Account.Mention+"\n\nYou are either reporting too early, or trying to report a game that has not been created yet.";
                        return embed;
                    }
                    catch (UniversalMatchException )
                    {
                        embed.Color = DiscordColor.Yellow;
                        embed.Title = "Unnecessary Report!";
                        embed.Description = "User: " + player.Account.Mention + "\n\nYou attempted to report a universal match. Universal matches do not need to be reported since they do not affect mmr.";
                        return embed;
                    }
                    catch (PlayerNotInLobbyException )
                    {
                        embed.Color = DiscordColor.Red;
                        embed.Title = "Suspicious Report!";
                        embed.Description = "User: " + player.Account.Mention + "\n\nYou are trying to report a game you are not in.";
                        return embed;
                    }
                    catch (TooLateException)
                    {
                        embed.Color = DiscordColor.Red;
                        embed.Title = "Suspicious Report!";
                        embed.Description = "User: " + player.Account.Mention + "\n\nYou are trying to report a game that took place over 2 hours ago.";
                        return embed;
                    }
                }
            }

            try
            {
                MyMongoDB.UpdateGame(gameid, player, win);
                //BsonDocument doc = MyMongoDB.GetVoiceChannels(gameid);

                //CustomCommands.GetChannel(player.Account, ulong.Parse(doc.GetElement("team1VC").Value.ToString())).DeleteAsync();
                //CustomCommands.GetChannel(player.Account, ulong.Parse(doc.GetElement("team2VC").Value.ToString())).DeleteAsync();
                embed.Title = "Reported Successfully!";
                embed.Description = "Game " + gameid + " reported on " + DateTime.Now;
                return embed;
            }
            catch (GameNotFoundException) { }
            catch (GameAlreadyReportedException)
            {
                embed.Color = DiscordColor.Red;
                embed.Title = "Game was already reported!";
                embed.Description = "Please do not report the same games repeatedly.";
                return embed;
            }
            catch (TooEarlyException)
            {
                embed.Color = DiscordColor.Red;
                embed.Title = "Suspicious Report!";
                embed.Description = "User: " + player.Account.Mention + "\n\nYou are either reporting too early, or trying to report a game that has not been created yet.";
                return embed;
            }
            catch (UniversalMatchException)
            {
                embed.Color = DiscordColor.Yellow;
                embed.Title = "Unnecessary Report!";
                embed.Description = "User: " + player.Account.Mention + "\n\nYou attempted to report a universal match. Universal matches do not need to be reported since they do not affect mmr.";
                return embed;
            }
            catch (PlayerNotInLobbyException )
            {
                embed.Color = DiscordColor.Red;
                embed.Title = "Suspicious Report!";
                embed.Description = "User: " + player.Account.Mention + "\n\nYou are trying to report a game you are not in.";
                return embed;
            }
            catch (TooLateException)
            {
                embed.Color = DiscordColor.Red;
                embed.Title = "Suspicious Report!";
                embed.Description = "User: " + player.Account.Mention + "\n\nYou are trying to report a game that took place over 2 hours ago.";
                return embed;
            }

            embed.Color = DiscordColor.Red;
            embed.Title = "Could not find lobby with that ID";
            embed.Description = "Make sure you entered the correct ID of the game. If you are sure, then message a moderator.";


            

            return embed;
        }



        /// <summary>
        /// checks if the player is in the list of players
        /// </summary>
        /// <param name="list"></param>
        /// <param name="player"></param>
        /// <returns></returns>
        private static bool PlayerExistsInList(List<Player> list, Player player)
        {
            foreach (Player currentPlayer in list)
            {
                if (currentPlayer.Account.Id == player.Account.Id)
                {
                    currentPlayer.QTime = DateTime.Now;
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// removes the given player from the list
        /// </summary>
        /// <param name="list"></param>
        /// <param name="player"></param>
        /// <returns></returns>
        private static List<Player> RemovePlayerFromlist(List<Player> list, Player player)
        {
            foreach (Player currentPlayer in list)
            {
                if (currentPlayer.Account.Id == player.Account.Id)
                {
                    list.Remove(currentPlayer);
                    break;
                }
            }
            return list;
        }

        /// <summary>
        /// orders the players highest to lowest by mmr
        /// </summary>
        /// <param name="players"></param>
        /// <returns></returns>
        private static List<Player> OrderPlayers(List<Player> players)
        {
            bool changes = true;
            while (changes)
            {
                changes = false;
                for (int i = 0; i < players.Count - 1; i++)
                {
                    if (players[i+1].MMR > players[i].MMR)
                    {
                        Player holder = players[i+1];
                        players[i+1] = players[i];
                        players[i] = holder;
                        changes = true;
                    }
                }
            }
            return players;
        }

        private static List<Player> PickCaptains(List<Player> players)
        {
            bool changes = true;
            while (changes)
            {
                changes = false;
                for (int i = 0; i < players.Count - 1; i++)
                {
                    if (players[i + 1].MMR > players[i].MMR)
                    {
                        Player holder = players[i + 1];
                        players[i + 1] = players[i];
                        players[i] = holder;
                        changes = true;
                    }
                }
            }

            List<Player> top3 = new List<Player>
            {
                players[0],
                players[1],
                players[2]
            };

            Random rand = new Random();

            List<Player> captains = new List<Player>();

            for (int i = 0; i < 2; i++)
            {
                int num = rand.Next(top3.Count);
                captains.Add(top3[num]);
                top3.Remove(top3[num]);
            }
            return captains;
        }

        /// <summary>
        /// returns all the stats in 1 string
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public static string PrintStats(List<Player> players, int firstpick = 0, int secondpick = 0)
        {
            string output = "Please react with the corresponding number of the player:\n\n";
            int playerNum = 1;

            List<Player> flexPlayers = new List<Player>();
            List<Player> forwardPlayers = new List<Player>();
            List<Player> goaliePlayers = new List<Player>();

            foreach (Player player in players)
            {
                if (player.Preference == "flex")
                {
                    flexPlayers.Add(player);
                }
                else if (player.Preference == "goalie")
                {
                    goaliePlayers.Add(player);
                }
                else if (player.Preference == "forward")
                {
                    forwardPlayers.Add(player);
                }
            }

            if (forwardPlayers.Count > 0)
            {
                output += "**FORWARD:**\n";
                foreach (Player player in forwardPlayers)
                {
                    if (playerNum == firstpick || playerNum == secondpick)
                    {
                        output += "~~";
                    }
                    output += "**" + playerNum + "** - " + player.Account.DisplayName +
                    " **Stats:** " + player.TotalWins + "/" + player.TotalLosses +
                    " **Win%:** " + Math.Round(((double)player.TotalWins / ((double)player.TotalWins + (double)player.TotalLosses) * 100), 1) +
                    " **MMR:** " + player.MMR.ToString();
                    if (playerNum == firstpick || playerNum == secondpick)
                    {
                        output += "~~";
                    }
                    playerNum++;
                    output += "\n";
                }
                output += "\n";
            }
            if (flexPlayers.Count > 0)
            {
                output += "**FLEX:**\n";
                foreach (Player player in flexPlayers)
                {
                    if (playerNum == firstpick || playerNum == secondpick)
                    {
                        output += "~~";
                    }
                    output += "**" + playerNum + "** - " + player.Account.DisplayName +
                    " **Stats:** " + player.TotalWins + "/" + player.TotalLosses +
                    " **Win%:** " + Math.Round(((double)player.TotalWins / ((double)player.TotalWins + (double)player.TotalLosses) * 100), 1) +
                    " **MMR:** " + player.MMR.ToString();
                    if (playerNum == firstpick || playerNum == secondpick)
                    {
                        output += "~~";
                    }
                    playerNum++;
                    output += "\n";
                }
                output += "\n";
            }
            if (goaliePlayers.Count > 0)
            {
                output += "**GOALIE:**\n";
                foreach (Player player in goaliePlayers)
                {
                    if (playerNum == firstpick || playerNum == secondpick)
                    {
                        output += "~~";
                    }
                    output += "**" + playerNum + "** - " + player.Account.DisplayName +
                    " **Stats:** " + player.TotalWins + "/" + player.TotalLosses +
                    " **Win%:** " + Math.Round(((double)player.TotalWins / ((double)player.TotalWins + (double)player.TotalLosses) * 100), 1) +
                    " **MMR:** " + player.MMR.ToString();
                    if (playerNum == firstpick || playerNum == secondpick)
                    {
                        output += "~~";
                    }
                    playerNum++;
                    output += "\n";
                }
            }

            return output;

        }

        /// <summary>
        /// prints all the info of the lobby including game id, players, and who is goalie
        /// </summary>
        /// <param name="game"></param>
        /// <returns></returns>
        public static string PrintGameBanner(Game game)
        {
            string output = "Begin your match! The teams are listed below:\n\n";

            output += DisplayTeamRoles(game.Team1, 1);

            output += DisplayTeamRoles(game.Team2, 2);

            return output += "Lobby info will be directly messaged to each of you.";
        }

        /// <summary>
        /// moves players to team 1
        /// </summary>
        /// <param name="player"></param>
        public void MoveToTeam1(Player player)
        {
            bool changed = false;
            for(int i = 0; i < Players.Count; i++)
            {
                if (!changed)
                {
                    if (player.Account.Id == Players[i].Account.Id)
                    {
                        Team1.Add(Players[i]);
                        Players.Remove(Players[i]);
                        changed = true;
                    }
                }
            }
        }

        /// <summary>
        /// moves a player to team 2
        /// </summary>
        /// <param name="player"></param>
        public void MoveToTeam2(Player player)
        {
            bool changed = false;
            for (int i = 0; i < Players.Count; i++)
            {
                if (!changed)
                {
                    if (player.Account.Id == Players[i].Account.Id)
                    {
                        Team2.Add(Players[i]);
                        Players.Remove(Players[i]);
                        changed = true;
                    }
                }
            }
        }


        /// <summary>
        /// generates a password
        /// </summary>
        /// <returns></returns>
        private static string GeneratePassword()
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string randomString = new string(Enumerable.Repeat(chars, 5)
                .Select(s => s[random.Next(s.Length)]).ToArray());

            return randomString;
        }

        /// <summary>
        /// already picked a player
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        private static string AlreadyPicked(Player player)
        {
            return "You already picked " + player.Account.Mention + ".";
        }

        /// <summary>
        /// sends the game info to all the players in the game
        /// </summary>
        /// <param name="game"></param>
        private static async void SendGameInfo(Game game, DiscordChannel team1, DiscordChannel team2)
        {
            string password = GeneratePassword();

            for (int i = 0; i < OnGoingGames.Count; i++)
            {
                if (OnGoingGames[i].ID == game.ID)
                {
                    OnGoingGames[i].LobbyPass = password;
                }
            }

            DiscordEmbedBuilder embedt1 = new DiscordEmbedBuilder();

            embedt1.Color = DiscordColor.White;
            embedt1.Title = "Game " + game.ID + "  Has Started!";
            embedt1.Description = "Begin your match! The teams are listed below:\n\n";
            embedt1.Description += DisplayTeamRoles(game.Team1, 1);
            embedt1.Description += DisplayTeamRoles(game.Team2, 2);
            embedt1.Description += "\nPlease add the captain of your team to your friends list and invite eachother to a party.\n";
            embedt1.Description += "\nWhoever is labeled as **GOALIE** must be goalie, unless discussed and agreed upon by the entire team.\n";
            embedt1.Description += "\n**PASSWORD:** "+password+"\n";
            embedt1.Description += "\nBe sure to join your teams Voice Channel → " + team1.Mention;
            embedt1.Description += "\n\nUnsure about the rules? Head over to " + CustomCommands.GetChannel(RankingRanges.RulesChannelID).Mention + ".";

            DiscordEmbedBuilder embedt2 = new DiscordEmbedBuilder();

            embedt2.Color = DiscordColor.White;
            embedt2.Title = "Game " + game.ID + "  Has Started!";
            embedt2.Description = "Begin your match! The teams are listed below:\n\n";
            embedt2.Description += DisplayTeamRoles(game.Team1, 1);
            embedt2.Description += DisplayTeamRoles(game.Team2, 2);
            embedt2.Description += "\nPlease add the captain of your team to your friends list and invite eachother to a party.\n";
            embedt2.Description += "\nWhoever is labeled as **GOALIE** must be goalie, unless discussed and agreed upon by the entire team.\n";
            embedt2.Description += "\n**PASSWORD:** " + password + "\n";
            embedt2.Description += "\nBe sure to join your teams Voice Channel → " + team2.Mention;
            embedt2.Description += "\n\nUnsure about the rules? Head over to " + CustomCommands.GetChannel(RankingRanges.RulesChannelID).Mention + ".";


            foreach (Player player in game.Team1)
            {
                await player.Account.SendMessageAsync(embedt1);
            }
            foreach (Player player in game.Team2)
            { 
                await player.Account.SendMessageAsync(embedt2);
            }
        }

        /// <summary>
        /// prints the teams info, and assigns them the according roles
        /// </summary>
        /// <param name="players"></param>
        /// <returns></returns>
        private static string DisplayTeamRoles(List<Player> players, int team)
        {
            string output = "Team " + team + ":\n(GOALIE)";

            bool found = false;

            string goalieID = null;
            foreach (Player player in players)
            {
                if (!found)
                {
                    if (player.Preference == "goalie")
                    {
                        output += player.Account.Mention + " ";
                        goalieID = player.Account.Id.ToString();
                        found = true;
                    }
                }
            }

            if (!found)
            {
                foreach (Player player in players)
                {
                    if (!found)
                    {
                        if (player.Preference == "flex")
                        {
                            output += player.Account.Mention + " ";
                            goalieID = player.Account.Id.ToString();
                            found = true;
                        }
                    }
                }
            }

            if (!found)
            {
                output += players[0].Account.Mention + " ";
                goalieID = players[0].Account.Id.ToString(); ;
            }

            foreach (Player player in players)
            {
                if (player.Account.Id.ToString() != goalieID)
                {
                    output += player.Account.Mention + " ";
                }
            }

           return output += "\n\n";
        }

        /// <summary>
        /// organizes the players Forward - Flex - Goalie
        /// </summary>
        /// <param name="players"></param>
        /// <returns></returns>
        private static List<Player> OrganizePlayers(List<Player> players)
        {
            List<Player> forward = new List<Player>();
            List<Player> flex = new List<Player>();
            List<Player> goalie = new List<Player>();

            foreach (Player player in players)
            {
                if (player.Preference == "forward")
                {
                    forward.Add(player);
                }
                else if (player.Preference == "flex")
                {
                    flex.Add(player);
                }
                else if (player.Preference == "goalie")
                {
                    goalie.Add(player);
                }
            }

            players.Clear();

            foreach (Player player in forward)
            {
                players.Add(player);
            }
            foreach(Player player in flex)
            {
                players.Add(player);
            }
            foreach (Player player in goalie)
            {
                players.Add(player);
            }

            return players;
        }

        /// <summary>
        /// sends the cancel match message and allows players to q again
        /// </summary>
        /// <param name="game"></param>
        /// <returns></returns>
        private static DiscordEmbedBuilder CancelMatch(Game game, string description = "Captains took too long to pick players")
        {
            DiscordEmbedBuilder embed = new DiscordEmbedBuilder { Color = DiscordColor.Red, Title = "Game " + game.ID + " has been canceled", Description = description };

            for (int i = 0; i < PreGames.Count; i++)
            {
                if (PreGames[i].ID == game.ID)
                {
                    PreGames.Remove(PreGames[i]);
                }
            }

            foreach (Player player in game.Players)
            {
                player.Account.SendMessageAsync(embed);
            }

            game.Team1[0].Account.SendMessageAsync(embed);
            game.Team2[0].Account.SendMessageAsync(embed);


            return embed;
        }

        /// <summary>
        /// gets the lobby password (for spectating)
        /// </summary>
        /// <param name="LobbyID"></param>
        /// <returns></returns>
        public static string GetLobbyPass(string LobbyID)
        {
            foreach(Game game in OnGoingGames)
            {
                if (game.ID == LobbyID)
                {
                    return game.LobbyPass;
                }
            }
            foreach(Game game in PreGames)
            {
                if (game.ID == LobbyID)
                {
                    throw new TooEarlyException();
                }
            }

            MyMongoDB.FindGame(LobbyID);

            throw new GameAlreadyReportedException();
        }

        /// <summary>
        /// cleans up all the leftover channels that were not deleted
        /// </summary>
        /// <param name="ctx"></param>
        public static async Task CleanUpChannels(CommandContext ctx)
        {
            var channels =  ctx.Guild.GetChannelsAsync().Result;

            foreach (DiscordChannel vc in channels)
            {
                try
                {
                    if (vc.UserLimit == 3)
                    {
                        if (vc.Users.Count == 0)
                        {
                            BsonDocument doc = MyMongoDB.GetVoiceChannels(vc.Id);

                            DateTime now = doc.GetElement("creationTime").Value.AsDateTime;

                            DateTime now2 = DateTime.Now.AddHours(4);

                            int minutes = DateTime.Now.AddHours(4).Subtract(doc.GetElement("creationTime").Value.AsDateTime).Minutes;

                            if (DateTime.Now.AddHours(4).Subtract(doc.GetElement("creationTime").Value.AsDateTime).Minutes > 3)
                            {
                                ctx.Guild.GetChannel(vc.Id).DeleteAsync().Wait();
                            }
                        }
                    }
                }
                catch { }
            }
        }

        /// <summary>
        /// empties all the queues
        /// </summary>
        public static void EmptyQueues()
        {
            PlayersInQ_EU_1.Clear();
            PlayersInQ_EU_2.Clear();
            PlayersInQ_EU_3.Clear();
            PlayersInQ_EU_4.Clear();
            PlayersInQ_NA_1.Clear();
            PlayersInQ_NA_2.Clear();
            PlayersInQ_NA_3.Clear();
            PlayersInQ_NA_4.Clear();
            PlayersInQ_UN_EU.Clear();
            PlayersInQ_UN_NA.Clear();
        }
    }
}
