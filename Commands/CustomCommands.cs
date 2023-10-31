using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Extensions;
using Microsoft.Win32;
using MongoDB.Bson;
using Omega6.Classes;
using Omega6.Classes.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using static MongoDB.Bson.Serialization.Serializers.SerializerHelper;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Omega6.Commands
{
    public class CustomCommands : BaseCommandModule
    {
        #region Lobby Qing 
        [Command("q")]
        [Description("Queues for the game.")]
        public async Task Q(CommandContext ctx, [Description("Your prefered position to play (default is flex)")]string preference = "flex")
        {
            try
            {
                if (RankingRanges.CanQueue)
                {
                    DiscordEmbedBuilder embed = new DiscordEmbedBuilder
                    {
                        Color = DiscordColor.Orange,
                        Title = string.Empty
                    };
                    try
                    {
                        Player player = MyMongoDB.FindPlayer(ctx);

                        CheckGames(player);

                        if (preference.ToLower() == "g" || "goalie".Contains(preference.ToLower()))
                        {
                            player.Preference = "goalie";
                        }
                        else if (preference.ToLower() == "f" || "forward".Contains(preference.ToLower()))
                        {
                            player.Preference = "forward";
                        }
                        else
                        {
                            player.Preference = "flex";
                        }


                        player.QTime = DateTime.Now;


                        List<Player> playersToRemove = new List<Player>();

                        if (ctx.Channel.ToString().Split()[1] == "#na-rank-s")
                        {
                            string region = "NA";
                            string opporegion = "EU";
                            int rank = 1;

                            List<Player> players = Game.GetPlayersInQ(opporegion, rank);

                            foreach (Player currentPlayer in players)
                            {
                                if (currentPlayer.Account.Id == player.Account.Id)
                                {
                                    throw new UserAlreadyInQueueException("Cannot queue EU and NA at the same time.");
                                }
                            }

                            if (Game.GetNumberPlayersInQ(region, rank) < 6)
                            {
                                Game.AddPlayerToQ(player, region, rank);
                            }
                            else
                            {
                                return;
                            }

                            if (Game.GetNumberPlayersInQ(region, rank) == 1)
                            {
                                ctx.Channel.SendMessageAsync(ctx.Guild.GetRole(RankingRanges.Rank1Role).Mention);
                            }
                            else if (Game.GetNumberPlayersInQ(region, rank) == 6)
                            {
                                foreach (Player selectedPlayer in CheckForTimeOuts(region, rank))
                                {
                                    playersToRemove.Add(selectedPlayer);
                                }

                                if (Game.GetNumberPlayersInQ(region, rank) == 6)
                                {
                                    FullQ(ctx);

                                }
                            }
                            if (Game.GetNumberPlayersInQ(region, rank) == 0)
                            {
                                embed.Title = "6 players are in the queue.";
                            }
                            else
                            {
                                embed.Title = Game.GetNumberPlayersInQ(region, rank).ToString() + " players are in the queue.";
                            }
                        }
                        else if (ctx.Channel.ToString().Split()[1] == "#na-rank-a")
                        {
                            string region = "NA";
                            string opporegion = "EU";
                            int rank = 2;

                            List<Player> players = Game.GetPlayersInQ(opporegion, rank);

                            foreach (Player currentPlayer in players)
                            {
                                if (currentPlayer.Account.Id == player.Account.Id)
                                {
                                    throw new UserAlreadyInQueueException("Cannot queue EU and NA at the same time.");
                                }
                            }

                            if (Game.GetNumberPlayersInQ(region, rank) < 6)
                            {
                                Game.AddPlayerToQ(player, region, rank);
                            }
                            else
                            {
                                return;
                            }

                            if (Game.GetNumberPlayersInQ(region, rank) == 1)
                            {
                                ctx.Channel.SendMessageAsync(ctx.Guild.GetRole(RankingRanges.Rank2Role).Mention);
                            }
                            else if (Game.GetNumberPlayersInQ(region, rank) == 6)
                            {
                                foreach (Player selectedPlayer in CheckForTimeOuts(region, rank))
                                {
                                    playersToRemove.Add(selectedPlayer);
                                }

                                if (Game.GetNumberPlayersInQ(region, rank) == 6)
                                {
                                    FullQ(ctx);

                                }
                            }
                            if (Game.GetNumberPlayersInQ(region, rank) == 0)
                            {
                                embed.Title = "6 players are in the queue.";
                            }
                            else
                            {
                                embed.Title = Game.GetNumberPlayersInQ(region, rank).ToString() + " players are in the queue.";
                            }
                        }
                        else if (ctx.Channel.ToString().Split()[1] == "#na-rank-b")
                        {
                            string region = "NA";
                            string opporegion = "EU";
                            int rank = 3;

                            List<Player> players = Game.GetPlayersInQ(opporegion, rank);

                            foreach (Player currentPlayer in players)
                            {
                                if (currentPlayer.Account.Id == player.Account.Id)
                                {
                                    throw new UserAlreadyInQueueException("Cannot queue EU and NA at the same time.");
                                }
                            }

                            if (Game.GetNumberPlayersInQ(region, rank) < 6)
                            {
                                Game.AddPlayerToQ(player, region, rank);
                            }
                            else
                            {
                                return;
                            }

                            if (Game.GetNumberPlayersInQ(region, rank) == 1)
                            {
                                ctx.Channel.SendMessageAsync(ctx.Guild.GetRole(RankingRanges.Rank3Role).Mention);
                            }
                            else if (Game.GetNumberPlayersInQ(region, rank) == 6)
                            {
                                foreach (Player selectedPlayer in CheckForTimeOuts(region, rank))
                                {
                                    playersToRemove.Add(selectedPlayer);
                                }

                                if (Game.GetNumberPlayersInQ(region, rank) == 6)
                                {
                                    FullQ(ctx);

                                }
                            }
                            if (Game.GetNumberPlayersInQ(region, rank) == 0)
                            {
                                embed.Title = "6 players are in the queue.";
                            }
                            else
                            {
                                embed.Title = Game.GetNumberPlayersInQ(region, rank).ToString() + " players are in the queue.";
                            }
                        }
                        else if (ctx.Channel.ToString().Split()[1] == "#na-rank-c")
                        {
                            string region = "NA";
                            string opporegion = "EU";
                            int rank = 4;

                            List<Player> players = Game.GetPlayersInQ(opporegion, rank);

                            foreach (Player currentPlayer in players)
                            {
                                if (currentPlayer.Account.Id == player.Account.Id)
                                {
                                    throw new UserAlreadyInQueueException("Cannot queue EU and NA at the same time.");
                                }
                            }

                            if (Game.GetNumberPlayersInQ(region, rank) < 6)
                            {
                                Game.AddPlayerToQ(player, region, rank);
                            }
                            else
                            {
                                return;
                            }

                            if (Game.GetNumberPlayersInQ(region, rank) == 1)
                            {
                                ctx.Channel.SendMessageAsync(ctx.Guild.GetRole(RankingRanges.Rank4Role).Mention);
                            }
                            else if (Game.GetNumberPlayersInQ(region, rank) == 6)
                            {
                                foreach (Player selectedPlayer in CheckForTimeOuts(region, rank))
                                {
                                    playersToRemove.Add(selectedPlayer);
                                }

                                if (Game.GetNumberPlayersInQ(region, rank) == 6)
                                {
                                    FullQ(ctx);

                                }
                            }
                            if (Game.GetNumberPlayersInQ(region, rank) == 0)
                            {
                                embed.Title = "6 players are in the queue.";
                            }
                            else
                            {
                                embed.Title = Game.GetNumberPlayersInQ(region, rank).ToString() + " players are in the queue.";
                            }
                        }
                        else if (ctx.Channel.ToString().Split()[1] == "#eu-rank-s")
                        {
                            string region = "EU";
                            string opporegion = "NA";
                            int rank = 1;

                            List<Player> players = Game.GetPlayersInQ(opporegion, rank);

                            foreach (Player currentPlayer in players)
                            {
                                if (currentPlayer.Account.Id == player.Account.Id)
                                {
                                    throw new UserAlreadyInQueueException("Cannot queue EU and NA at the same time.");
                                }
                            }

                            if (Game.GetNumberPlayersInQ(region, rank) < 6)
                            {
                                Game.AddPlayerToQ(player, region, rank);
                            }
                            else
                            {
                                return;
                            }

                            if (Game.GetNumberPlayersInQ(region, rank) == 1)
                            {
                                ctx.Channel.SendMessageAsync(ctx.Guild.GetRole(RankingRanges.Rank1Role).Mention);
                            }
                            else if (Game.GetNumberPlayersInQ(region, rank) == 6)
                            {
                                foreach (Player selectedPlayer in CheckForTimeOuts(region, rank))
                                {
                                    playersToRemove.Add(selectedPlayer);
                                }

                                if (Game.GetNumberPlayersInQ(region, rank) == 6)
                                {
                                    FullQ(ctx);

                                }
                            }
                            if (Game.GetNumberPlayersInQ(region, rank) == 0)
                            {
                                embed.Title = "6 players are in the queue.";
                            }
                            else
                            {
                                embed.Title = Game.GetNumberPlayersInQ(region, rank).ToString() + " players are in the queue.";
                            }
                        }
                        else if (ctx.Channel.ToString().Split()[1] == "#eu-rank-a")
                        {
                            string region = "EU";
                            string opporegion = "NA";
                            int rank = 2;

                            List<Player> players = Game.GetPlayersInQ(opporegion, rank);

                            foreach (Player currentPlayer in players)
                            {
                                if (currentPlayer.Account.Id == player.Account.Id)
                                {
                                    throw new UserAlreadyInQueueException("Cannot queue EU and NA at the same time.");
                                }
                            }

                            if (Game.GetNumberPlayersInQ(region, rank) < 6)
                            {
                                Game.AddPlayerToQ(player, region, rank);
                            }
                            else
                            {
                                return;
                            }

                            if (Game.GetNumberPlayersInQ(region, rank) == 1)
                            {
                                ctx.Channel.SendMessageAsync(ctx.Guild.GetRole(RankingRanges.Rank2Role).Mention);
                            }
                            else if (Game.GetNumberPlayersInQ(region, rank) == 6)
                            {
                                foreach (Player selectedPlayer in CheckForTimeOuts(region, rank))
                                {
                                    playersToRemove.Add(selectedPlayer);
                                }

                                if (Game.GetNumberPlayersInQ(region, rank) == 6)
                                {
                                    FullQ(ctx);

                                }
                            }
                            if (Game.GetNumberPlayersInQ(region, rank) == 0)
                            {
                                embed.Title = "6 players are in the queue.";
                            }
                            else
                            {
                                embed.Title = Game.GetNumberPlayersInQ(region, rank).ToString() + " players are in the queue.";
                            }
                        }
                        else if (ctx.Channel.ToString().Split()[1] == "#eu-rank-b")
                        {
                            string region = "EU";
                            string opporegion = "NA";
                            int rank = 3;

                            List<Player> players = Game.GetPlayersInQ(opporegion, rank);

                            foreach (Player currentPlayer in players)
                            {
                                if (currentPlayer.Account.Id == player.Account.Id)
                                {
                                    throw new UserAlreadyInQueueException("Cannot queue EU and NA at the same time.");
                                }
                            }

                            if (Game.GetNumberPlayersInQ(region, rank) < 6)
                            {
                                Game.AddPlayerToQ(player, region, rank);
                            }
                            else
                            {
                                return;
                            }

                            if (Game.GetNumberPlayersInQ(region, rank) == 1)
                            {
                                ctx.Channel.SendMessageAsync(ctx.Guild.GetRole(RankingRanges.Rank3Role).Mention);
                            }
                            else if (Game.GetNumberPlayersInQ(region, rank) == 6)
                            {
                                foreach (Player selectedPlayer in CheckForTimeOuts(region, rank))
                                {
                                    playersToRemove.Add(selectedPlayer);
                                }

                                if (Game.GetNumberPlayersInQ(region, rank) == 6)
                                {
                                    FullQ(ctx);

                                }
                            }
                            if (Game.GetNumberPlayersInQ(region, rank) == 0)
                            {
                                embed.Title = "6 players are in the queue.";
                            }
                            else
                            {
                                embed.Title = Game.GetNumberPlayersInQ(region, rank).ToString() + " players are in the queue.";
                            }
                        }
                        else if (ctx.Channel.ToString().Split()[1] == "#eu-rank-c")
                        {
                            string region = "EU";
                            string opporegion = "NA";
                            int rank = 4;

                            List<Player> players = Game.GetPlayersInQ(opporegion, rank);

                            foreach (Player currentPlayer in players)
                            {
                                if (currentPlayer.Account.Id == player.Account.Id)
                                {
                                    throw new UserAlreadyInQueueException("Cannot queue EU and NA at the same time.");
                                }
                            }

                            if (Game.GetNumberPlayersInQ(region, rank) < 6)
                            {
                                Game.AddPlayerToQ(player, region, rank);
                            }
                            else
                            {
                                return;
                            }

                            if (Game.GetNumberPlayersInQ(region, rank) == 1)
                            {
                                ctx.Channel.SendMessageAsync(ctx.Guild.GetRole(RankingRanges.Rank4Role).Mention);
                            }
                            else if (Game.GetNumberPlayersInQ(region, rank) == 6)
                            {
                                foreach (Player selectedPlayer in CheckForTimeOuts(region, rank))
                                {
                                    playersToRemove.Add(selectedPlayer);
                                }

                                if (Game.GetNumberPlayersInQ(region, rank) == 6)
                                {
                                    FullQ(ctx);

                                }
                            }
                            if (Game.GetNumberPlayersInQ(region, rank) == 0)
                            {
                                embed.Title = "6 players are in the queue.";
                            }
                            else
                            {
                                embed.Title = Game.GetNumberPlayersInQ(region, rank).ToString() + " players are in the queue.";
                            }
                        }
                        else if (ctx.Channel.ToString().Split()[1] == "#na-rank-universal")
                        {
                            string region = "NA";
                            string opporegion = "EU";
                            int rank = 0;

                            List<Player> players = Game.GetPlayersInQ(opporegion, rank);

                            foreach (Player currentPlayer in players)
                            {
                                if (currentPlayer.Account.Id == player.Account.Id)
                                {
                                    throw new UserAlreadyInQueueException("Cannot queue EU and NA at the same time.");
                                }
                            }

                            if (Game.GetNumberPlayersInQ(region, rank) < 6)
                            {
                                Game.AddPlayerToQ(player, region, rank);
                            }
                            else
                            {
                                return;
                            }
                            if (Game.GetNumberPlayersInQ(region, rank) == 6)
                            {
                                foreach (Player selectedPlayer in CheckForTimeOuts(region, rank))
                                {
                                    playersToRemove.Add(selectedPlayer);
                                }

                                if (Game.GetNumberPlayersInQ(region, rank) == 6)
                                {
                                    FullQ(ctx);

                                }
                            }
                            if (Game.GetNumberPlayersInQ(region, rank) == 0)
                            {
                                embed.Title = "6 players are in the queue.";
                            }
                            else
                            {
                                embed.Title = Game.GetNumberPlayersInQ(region, rank).ToString() + " players are in the queue.";
                            }
                        }
                        else if (ctx.Channel.ToString().Split()[1] == "#eu-rank-universal")
                        {
                            string region = "EU";
                            string opporegion = "NA";
                            int rank = 0;

                            List<Player> players = Game.GetPlayersInQ(opporegion, rank);

                            foreach (Player currentPlayer in players)
                            {
                                if (currentPlayer.Account.Id == player.Account.Id)
                                {
                                    throw new UserAlreadyInQueueException("Cannot queue EU and NA at the same time.");
                                }
                            }

                            if (Game.GetNumberPlayersInQ(region, rank) < 6)
                            {
                                Game.AddPlayerToQ(player, region, rank);
                            }
                            else
                            {
                                return;
                            }

                            if (Game.GetNumberPlayersInQ(region, rank) == 6)
                            {
                                foreach (Player selectedPlayer in CheckForTimeOuts(region, rank))
                                {
                                    playersToRemove.Add(selectedPlayer);
                                }

                                if (Game.GetNumberPlayersInQ(region, rank) == 6)
                                {
                                    FullQ(ctx);

                                }
                            }
                            if (Game.GetNumberPlayersInQ(region, rank) == 0)
                            {
                                embed.Title = "6 players are in the queue.";
                            }
                            else
                            {
                                embed.Title = Game.GetNumberPlayersInQ(region, rank).ToString() + " players are in the queue.";
                            }
                        }

                        embed.Description = player.Account.Mention + " has joined the queue as " + player.Preference + ".";

                        if (playersToRemove.Count > 0)
                        {
                            embed.Description += "\n\n";
                            foreach (Player selectedPlayer in playersToRemove)
                            {
                                embed.Description += selectedPlayer.Account.Mention + " ";
                                await selectedPlayer.Account.SendMessageAsync("You have been timed out and taken out of the queue due to being in queue for over 1 hour. If you are still looking for a queue, be sure to requeue.");
                            }
                            embed.Description += "have been timed out from the queue.";
                        }
                    }
                    catch (UserNotRegisteredException e)
                    {
                        embed.Color = DiscordColor.Red;
                        embed.Title = "You have not properly registered!";
                        embed.Description = ctx.Member.Mention + ", go to " + GetChannel(RankingRanges.RankCheckID).Mention + " to register.";
                    }
                    catch (UserAlreadyInQueueException e)
                    {
                        embed.Color = DiscordColor.Yellow;
                        embed.Title = "Already in a game.";
                        if (e.Message == null)
                        {
                            embed.Description = ctx.Member.Mention + " you are already in the queue.\n\n";
                        }
                        else
                        {
                            embed.Description = e.Message;
                        }
                    }
                    catch (UserNotInServerException e)
                    {
                        return;
                    }

                    if (!embed.Title.Equals(string.Empty))
                    {
                        await ctx.Channel.SendMessageAsync(embed).ConfigureAwait(false);
                    }
                }
                else
                {
                    await ctx.Channel.SendMessageAsync(new DiscordEmbedBuilder { Color = DiscordColor.Red, Title = "Queue Disabled", Description = "Queues are currently disabled.\nHead over to "+ctx.Guild.GetChannel(RankingRanges.AnnouncementsChannelID).Mention+" for details."});
                }
            }
            catch { }
        }


        [Command("leave")]
        [Description("Leave the queue you are currently in.")]
        public async Task Leave(CommandContext ctx)
        {
            try
            {
                DiscordEmbedBuilder embed = new DiscordEmbedBuilder
                {
                    Color = DiscordColor.Orange
                };
                try
                {
                    Player player = MyMongoDB.FindPlayer(ctx);

                    if (ctx.Channel.ToString().Split()[1] == "#na-rank-s")
                    {
                        Game.RemovePlayerFromQ(player, "NA", 1);
                        embed.Title = Game.GetNumberPlayersInQ("NA", 1).ToString() + " players are in the queue.";
                    }
                    else if (ctx.Channel.ToString().Split()[1] == "#na-rank-a")
                    {
                        Game.RemovePlayerFromQ(player, "NA", 2);
                        embed.Title = Game.GetNumberPlayersInQ("NA", 2).ToString() + " players are in the queue.";
                    }
                    else if (ctx.Channel.ToString().Split()[1] == "#na-rank-b")
                    {
                        Game.RemovePlayerFromQ(player, "NA", 3);
                        embed.Title = Game.GetNumberPlayersInQ("NA", 3).ToString() + " players are in the queue.";
                    }
                    else if (ctx.Channel.ToString().Split()[1] == "#na-rank-c")
                    {
                        Game.RemovePlayerFromQ(player, "NA", 4);
                        embed.Title = Game.GetNumberPlayersInQ("NA", 4).ToString() + " players are in the queue.";
                    }
                    else if (ctx.Channel.ToString().Split()[1] == "#eu-rank-s")
                    {
                        Game.RemovePlayerFromQ(player, "EU", 1);
                        embed.Title = Game.GetNumberPlayersInQ("EU", 1).ToString() + " players are in the queue.";
                    }
                    else if (ctx.Channel.ToString().Split()[1] == "#eu-rank-a")
                    {
                        Game.RemovePlayerFromQ(player, "EU", 2);
                        embed.Title = Game.GetNumberPlayersInQ("EU", 2).ToString() + " players are in the queue.";
                    }
                    else if (ctx.Channel.ToString().Split()[1] == "#eu-rank-b")
                    {
                        Game.RemovePlayerFromQ(player, "EU", 3);
                        embed.Title = Game.GetNumberPlayersInQ("EU", 3).ToString() + " players are in the queue.";
                    }
                    else if (ctx.Channel.ToString().Split()[1] == "#eu-rank-c")
                    {
                        Game.RemovePlayerFromQ(player, "EU", 4);
                        embed.Title = Game.GetNumberPlayersInQ("EU", 4).ToString() + " players are in the queue.";
                    }
                    else if (ctx.Channel.ToString().Split()[1] == "#eu-rank-universal")
                    {
                        Game.RemovePlayerFromQ(player, "EU", 0);
                        embed.Title = Game.GetNumberPlayersInQ("EU", 0).ToString() + " players are in the queue.";
                    }
                    else if (ctx.Channel.ToString().Split()[1] == "#na-rank-universal")
                    {
                        Game.RemovePlayerFromQ(player, "NA", 0);
                        embed.Title = Game.GetNumberPlayersInQ("NA", 0).ToString() + " players are in the queue.";
                    }
                    embed.Description = player.Account.Mention + " has left the queue.";

                }
                catch (UserNotRegisteredException e)
                {
                    embed.Color = DiscordColor.Red;
                    embed.Title = "You have not properly registered!";
                    embed.Description = ctx.Member.Mention + ", go to " + GetChannel(RankingRanges.RankCheckID).Mention + " to register.";
                }
                catch (UserNotInQueueException e)
                {
                    embed.Color = DiscordColor.Red;
                    embed.Title = "Not in queue!";
                    embed.Description = ctx.Member.Mention + " you are not in the queue.";
                }
                catch (UserNotInServerException e)
                {
                    return;
                }

                await ctx.Channel.SendMessageAsync(embed).ConfigureAwait(false);
            }
            catch { }
        }


        [Command("status")]
        [Description("Returns the status of the current queue.")]
        public async Task Status(CommandContext ctx)
        {
            try
            {
                if (ctx.Member != null)
                {
                    DiscordEmbedBuilder embed = new DiscordEmbedBuilder
                    {
                        Color = DiscordColor.Orange,
                        Title = "Queue Status"
                    };

                    string description = string.Empty;
                    List<Player> players = new List<Player>() { };

                    if (ctx.Channel.ToString().Split()[1] == "#na-rank-s")
                    {
                        description = Game.GetNumberPlayersInQ("NA", 1).ToString();
                        players = Game.GetPlayersInQ("NA", 1);
                    }
                    else if (ctx.Channel.ToString().Split()[1] == "#na-rank-a")
                    {
                        description = Game.GetNumberPlayersInQ("NA", 2).ToString();
                        players = Game.GetPlayersInQ("NA", 2);
                    }
                    else if (ctx.Channel.ToString().Split()[1] == "#na-rank-b")
                    {
                        description = Game.GetNumberPlayersInQ("NA", 3).ToString();
                        players = Game.GetPlayersInQ("NA", 3);
                    }
                    else if (ctx.Channel.ToString().Split()[1] == "#na-rank-c")
                    {
                        description = Game.GetNumberPlayersInQ("NA", 4).ToString();
                        players = Game.GetPlayersInQ("NA", 4);
                    }
                    else if (ctx.Channel.ToString().Split()[1] == "#eu-rank-s")
                    {
                        description = Game.GetNumberPlayersInQ("EU", 1).ToString();
                        players = Game.GetPlayersInQ("EU", 1);
                    }
                    else if (ctx.Channel.ToString().Split()[1] == "#eu-rank-a")
                    {
                        description = Game.GetNumberPlayersInQ("EU", 2).ToString();
                        players = Game.GetPlayersInQ("EU", 2);
                    }
                    else if (ctx.Channel.ToString().Split()[1] == "#eu-rank-b")
                    {
                        description = Game.GetNumberPlayersInQ("EU", 3).ToString();
                        players = Game.GetPlayersInQ("EU", 3);
                    }
                    else if (ctx.Channel.ToString().Split()[1] == "#eu-rank-c")
                    {
                        description = Game.GetNumberPlayersInQ("EU", 4).ToString();
                        players = Game.GetPlayersInQ("EU", 4);
                    }
                    else if (ctx.Channel.ToString().Split()[1] == "#eu-rank-universal")
                    {
                        description = Game.GetNumberPlayersInQ("EU", 0).ToString();
                        players = Game.GetPlayersInQ("EU", 0);
                    }
                    else if (ctx.Channel.ToString().Split()[1] == "#na-rank-universal")
                    {
                        description = Game.GetNumberPlayersInQ("NA", 0).ToString();
                        players = Game.GetPlayersInQ("NA", 0);
                    }

                    description = "There are " + description + " players in the queue.";

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
                        description += "\n**FORWARD:**\n";
                        foreach (Player player in forwardPlayers)
                        {
                            if (DateTime.Now.Subtract(player.QTime).Hours > 0)
                            {
                                description += "(Timed Out)";
                            }
                            description += player.Account.Mention + " ";
                        }
                    }
                    if (flexPlayers.Count > 0)
                    {
                        description += "\n**FLEX:**\n";
                        foreach (Player player in flexPlayers)
                        {
                            if (DateTime.Now.Subtract(player.QTime).Hours > 0)
                            {
                                description += "(Timed Out)";
                            }
                            description += player.Account.Mention + " ";
                        }
                    }
                    if (goaliePlayers.Count > 0)
                    {
                        description += "\n**GOALIE:**\n";
                        foreach (Player player in goaliePlayers)
                        {
                            if (DateTime.Now.Subtract(player.QTime).Hours > 0)
                            {
                                description += "(Timed Out)";
                            }
                            description += player.Account.Mention + " ";
                        }
                    }

                    embed.Description = description;

                    await ctx.Channel.SendMessageAsync(embed).ConfigureAwait(false);
                }
            }
            catch { }
        }


        [Command("report")]
        [Description("Report the end game result")]
        public async Task Report(CommandContext ctx, [Description("The game ID")]string id, [Description("Whether you won (w) or lost (l) the match")]string w_or_l)
        {
            try
            {
                if (ctx.Channel.ToString().Split()[1] == "#report-scores")
                {
                    DiscordEmbedBuilder embed = new DiscordEmbedBuilder {Color = DiscordColor.Green };
                    try
                    {
                        var emoji = DiscordEmoji.FromName(ctx.Client, ":hourglass_flowing_sand:");

                        await ctx.Message.CreateReactionAsync(emoji);

                        if (w_or_l.ToLower() == "w")
                        {
                            embed = Game.ReportMatch(id, MyMongoDB.FindPlayer(ctx), true);
                        }
                        else if (w_or_l.ToLower() == "l")
                        {
                            embed = Game.ReportMatch(id, MyMongoDB.FindPlayer(ctx), false);
                        }
                        else
                        {
                            embed = new DiscordEmbedBuilder { Color = DiscordColor.Red, Title = "Invalid Parameters", Description = "Please specify whether you won (w) or lost (l) the match after giving the id of the game." };
                        }

                        await ctx.Message.DeleteOwnReactionAsync(emoji);
                        if (embed.Title == "Reported Successfully!")
                        {
                            // ↓ sends a message saying it was reported successfully
                            //await ctx.Member.SendMessageAsync(embed).ConfigureAwait(false);
                            await ctx.Message.CreateReactionAsync(DiscordEmoji.FromName(ctx.Client, ":white_check_mark:"));
                        }
                        else if (embed.Title == "Unnecessary Report!")
                        {
                            await ctx.Channel.SendMessageAsync(embed).ConfigureAwait(false);
                            await ctx.Message.CreateReactionAsync(DiscordEmoji.FromName(ctx.Client, ":warning:"));
                        }
                        else
                        {
                            await ctx.Message.CreateReactionAsync(DiscordEmoji.FromName(ctx.Client, ":x:"));
                            await ctx.Channel.SendMessageAsync(embed).ConfigureAwait(false);
                        }
                    }
                    catch (UserNotRegisteredException e)
                    {
                        embed.Color = DiscordColor.Red;
                        embed.Title = "You have not properly registered!";
                        embed.Description = ctx.Member.Mention + ", go to " + GetChannel(RankingRanges.RankCheckID).Mention + " to register.";
                        await ctx.Channel.SendMessageAsync(embed).ConfigureAwait(false);
                    }
                    catch (UserNotInServerException e)
                    {
                        return;
                    }
                }
            }
            catch { }
        }


        [Command("spectate")]
        [Description("Spectate a game.")]
        public async Task Spectate(CommandContext ctx, [Description("The game ID")] string id)
        {
            try
            {
                if (ctx.Member != null)
                {
                    try
                    {
                        string password = Game.GetLobbyPass(id);

                        await ctx.Member.SendMessageAsync(new DiscordEmbedBuilder { Color = DiscordColor.Blue, Title = "Spectate", Description = "You can now spectate Game " + id + ".\n\n**PASSWORD:** "+password+"\n\nYou can spectate by searching for a custom match, selecting 'Join as spectator', then entering the password.\n\nIf you join the game as a player, that is a **BANNABLE** offense." });

                        await ctx.Message.CreateReactionAsync(DiscordEmoji.FromName(ctx.Client, ":white_check_mark:"));

                    }
                    catch (GameNotFoundException)
                    {
                        await ctx.Channel.SendMessageAsync(new DiscordEmbedBuilder
                        {
                            Color = DiscordColor.Red,
                            Title = "Spectate",
                            Description = "Game " + id + " does not exist."
                        }); ;
                    }
                    catch (TooEarlyException)
                    {
                        await ctx.Channel.SendMessageAsync(new DiscordEmbedBuilder
                        {
                            Color = DiscordColor.Red,
                            Title = "Spectate",
                            Description = "Game " + id + " has not started yet."
                        }); ;
                    }
                    catch (GameAlreadyReportedException)
                    {
                        await ctx.Channel.SendMessageAsync(new DiscordEmbedBuilder
                        {
                            Color = DiscordColor.Red,
                            Title = "Spectate",
                            Description = "Game " + id + " has already ended."
                        }); ;
                    }
                }
            }
            catch { }
        }
        #endregion

        #region Personal Account

        [Command("rankcheck_alpha_tester")]
        [Description("Rank check your discord account to allow you to queue in the appropriate rank.")]
        public async Task RankCheckAlpha(CommandContext ctx)
        {
            try
            {
                //await ctx.Message.DeleteAsync().ConfigureAwait(false);
                if (ctx.Channel.ToString().Split()[1] == "#rankcheck")
                {
                    Player player = null;
                    try
                    {
                        player = MyMongoDB.FindPlayer(ctx);

                        await player.Account.SendMessageAsync(new DiscordEmbedBuilder { Color = DiscordColor.Red, Title = "Already Registered!", Description = "You already rankchecked for this Alpha test."}).ConfigureAwait(false);

                        return;

                        // Do actual rank check here
                    }
                    catch (UserNotRegisteredException e)
                    {
                        player =  MyMongoDB.CreatePlayer(ctx, 3);

                        GrantRole(player, ctx, RankingRanges.Rank3Role);
                    }
                    catch (UserNotInServerException e)
                    {
                        return;
                    }

                    player.Rank = 3;

                    DiscordEmbedBuilder embed = new DiscordEmbedBuilder
                    {
                        Color = DiscordColor.Green,
                        Title = "Success!",
                        Description = "You are now in " + RankingRanges.GetRankString(player.Rank)
                    };


                    await player.Account.SendMessageAsync(embed).ConfigureAwait(false);
                }
            }
            catch { }
        }


        [Command("rankcheck")]
        [Description("Rank check your discord account to allow you to queue in the appropriate rank.")]
        public async Task RankCheck(CommandContext ctx, [Description("Your in game name.")]string Omega6Name)
        {
            try
            {
                if (ctx.Channel.ToString().Split()[1] == "#rankcheck")
                {
                    try
                    {
                        if (MyMongoDB.GetRankCheck(ctx.Member.Id) != null)
                        {
                            if (MyMongoDB.GetRankCheck(ctx.Member.Id).OmegaStrikerName == Omega6Name.ToLower())
                            {
                                PerformRankCheck(ctx, new RankCheckRegistry(ctx.Member.Id, Omega6Name.ToLower()), false);
                            }
                            else
                            {
                                throw new AnotherAccountAlreadyInUseException();
                            }
                        }
                        else if (MyMongoDB.GetRankCheck(Omega6Name) != null)
                        {
                            if (MyMongoDB.GetRankCheck(Omega6Name).LinkedDiscordUser == ctx.Member.Id )
                            {
                                PerformRankCheck(ctx, new RankCheckRegistry(ctx.Member.Id, Omega6Name.ToLower()), false);
                            }
                            else
                            {
                                throw new InGameNameAlreadyInUseException();
                            }
                        }
                        else
                        {
                            PerformRankCheck(ctx, new RankCheckRegistry(ctx.Member.Id, Omega6Name.ToLower()), true);
                        }
                    }
                    catch (InGameNameAlreadyInUseException e)
                    {
                        DiscordEmbedBuilder embed = new DiscordEmbedBuilder
                        {
                            Color = DiscordColor.Red,
                            Title = "Omega Striker Account Already In Use!",
                            Description = "**"+Omega6Name+"**\nThis account is already in use by another user. If you believe this is a case of fraud and this is truly your account, " +
                            "react with the green check to report fraud, or react with the red X in case it was a typo."
                        };

                        var interactivity = ctx.Client.GetInteractivity();

                        var message = await ctx.Member.SendMessageAsync(embed);

                        await message.CreateReactionAsync(DiscordEmoji.FromName(ctx.Client, ":white_check_mark:"));
                        await message.CreateReactionAsync(DiscordEmoji.FromName(ctx.Client, ":x:"));

                        var result = await interactivity.WaitForReactionAsync(
                           x => x.Message == message &&
                           x.User == ctx.User &&
                           (x.Emoji == DiscordEmoji.FromName(ctx.Client, ":white_check_mark:") || x.Emoji == DiscordEmoji.FromName(ctx.Client, ":x:")), message, ctx.User, TimeSpan.FromMinutes(7)).ConfigureAwait(false);

                        if (result.Result == null)
                        {
                            return;
                        }

                        if (result.Result.Emoji == DiscordEmoji.FromName(ctx.Client, ":white_check_mark:"))
                        {
                            await ctx.Member.SendMessageAsync("This incident has been reported to moderators. They will be in contact with you soon about the situation.");

                            BsonDocument doc = MyMongoDB.FindPlayer(MyMongoDB.GetRankCheck(Omega6Name).LinkedDiscordUser.ToString());

                            DiscordMember member = ctx.Guild.GetMemberAsync(MyMongoDB.GetRankCheck(Omega6Name).LinkedDiscordUser).Result;

                            await GetChannel(RankingRanges.RankCheckTicket).SendMessageAsync(ctx.Member.Mention + " (ID:"+ctx.Member.Id+") is reporting that someone " +
                                                                                                "has used their Omega Striker account already ("+Omega6Name+"). This account is already linked " +
                                                                                                "to user "+member.Mention+" (ID:"+ MyMongoDB.GetRankCheck(Omega6Name).LinkedDiscordUser+ ").");
                        }
                        else
                        {
                            return;
                        }

                        await ctx.Guild.GetMemberAsync(MyMongoDB.GetRankCheck(Omega6Name).LinkedDiscordUser).Result.SendMessageAsync(new DiscordEmbedBuilder { Color = DiscordColor.Red, Title = "Fraud Report!", Description = "You have been flagged in a possible fraud report. You will be messaged at some point by admins about the ownership of your linked Omega Striker account."});
                    }
                    catch (AnotherAccountAlreadyInUseException e)
                    {
                        DiscordEmbedBuilder embed = new DiscordEmbedBuilder
                        {
                            Color = DiscordColor.Red,
                            Title = "Already Linked An Account!",
                            Description = "You have already linked an account to your discord! You cannot link another account and smurfing is strongly prohibited."
                        };

                        await ctx.Member.SendMessageAsync(embed);
                    }
                }
            }
            catch { }
        }


        [Command("stats")]
        [Description("Retrieve your stats.")]
        public async Task Stats(CommandContext ctx, [Description("Mention the member you would like to recieve the stats from.")]DiscordMember member = null)
        {
            try
            {
                DiscordEmbedBuilder embed = new DiscordEmbedBuilder
                {
                    Color = DiscordColor.Orange
                };

                try
                {
                    Player player = null;

                    if (member != null)
                    {
                        player = MyMongoDB.FindPlayer(ctx, member);
                        embed.Title = member.DisplayName + "'s Stats";
                    }
                    else
                    {
                        player = MyMongoDB.FindPlayer(ctx);
                        embed.Title = player.Account.DisplayName + "'s Stats";
                    }

                    embed.Description = "**Rank:** " + RankingRanges.GetRankString(player.Rank) + "\n" +
                                        "**Total Games:** " + (player.TotalWins + player.TotalLosses).ToString() + "\n" +
                                        "**Wins:** " + player.TotalWins.ToString() + "\n" +
                                        "**Losses:** " + player.TotalLosses.ToString() + "\n" +
                                        "**MMR:** " + player.MMR.ToString();

                    await player.Account.SendMessageAsync(embed);
                }
                catch (UserNotRegisteredException e)
                {
                    if (member == null)
                    {
                        embed.Title = "You have not properly registered!";
                        embed.Description = ctx.Member.Mention + ", go to " + GetChannel(RankingRanges.RankCheckID).Mention + " to register.";
                    }
                    else
                    {
                        embed.Title = "This player is not registered!";
                        embed.Description = member.Mention + " is currently not registerd.";
                    }
                    embed.Color = DiscordColor.Red;
                    await ctx.Channel.SendMessageAsync(embed).ConfigureAwait(false);
                }
                catch (UserNotInServerException e)
                {
                    return;
                }
            }
            catch { }
        }

        [Command("help")]
        [Description("Help about all commands for the bot.")]
        public async Task Help(CommandContext ctx)
        {
            await ctx.Member.SendMessageAsync(new DiscordEmbedBuilder { Color = DiscordColor.Blue, Title = "Help", Description = 
                "*Lobby Queueing Commands*\n" +
                "**!q** Will join you into the current live queue as flex.\n" +
                "**!q f | !q forward** Will join you into the current live queue as forward.\n" +
                "**!q g | !q goalie** Will join you into the current live queue as goalie.\n\n" +
                "**!leave** Will remove you from the queue you are in.\n" +
                "**!status** Will display all users that are currently queueing for a match and their preferred role.\n" +
                "**!spectate [game id]** Will allow you to recieve the game password so you can spectate a match.\n" +
                "**!report [game id] [w or l]** Reports your game. Input W if YOU won the match or L if YOU lost the match.\n\n" +
                "*Profile Commands*\n" +
                "**!rankcheck [Omega Striker name] [the region you play in]** Will rankcheck you into the system and allow you to participate in Omega 6ixes.\n" +
                "**!stats** Will display your current stats within Omega 6ixes.\n" +
                "**!stats [discord member name or id]** Will display that specified members stats within Omega 6ixes.\n"
            });
        }
        #endregion

        #region Admin
        [Command("wipe_games")]
        [Description("Wipes the games in the db")]
        public async Task WipeGames(CommandContext ctx)
        {
            try
            {
                if (ctx.Member.Roles.Contains(ctx.Guild.GetRole(RankingRanges.ModRole)) && ctx.Channel.ToString().Split()[1] == "#database_control")
                {
                    MyMongoDB.ClearGameCache("Games");
                    MyMongoDB.ClearGameCache("VoiceChannels");
                    await ctx.Message.CreateReactionAsync(DiscordEmoji.FromName(ctx.Client, ":white_check_mark:"));
                }
            }
            catch { }
        }

        [Command("wipe_players")]
        [Description("Wipes the Players in the db")]
        public async Task WipePlayers(CommandContext ctx)
        {
            try
            {
                if (ctx.Member.Roles.Contains(ctx.Guild.GetRole(RankingRanges.ModRole)) && ctx.Channel.ToString().Split()[1] == "#database_control")
                {
                    MyMongoDB.ClearGameCache("Player");
                    await ctx.Message.CreateReactionAsync(DiscordEmoji.FromName(ctx.Client, ":white_check_mark:"));
                }
            }
            catch { }
        }

        [Command("startup")]
        [Description("Manually update the leaderboard")]
        public async Task StartupCommand(CommandContext ctx)
        {
            try
            {
                if (ctx.Channel.ToString().Split()[1] == "#database_control")
                {
                    StartupFunctions(ctx);
                    await ctx.Message.CreateReactionAsync(DiscordEmoji.FromName(ctx.Client, ":white_check_mark:"));
                }
            }
            catch { }
        }

        [Command("reverse_outcome")]
        [Description("Manually reverse the outcome of a game.")]
        public async Task ReverseOutcome(CommandContext ctx, [Description("The lobby id of the game to reverse.")]string lobbyID)
        {
            try
            {
                if (ctx.Channel.ToString().Split()[1] == "#database_control")
                {
                    try
                    {
                        MyMongoDB.ReverseOutcome(lobbyID);
                        await ctx.Message.CreateReactionAsync(DiscordEmoji.FromName(ctx.Client, ":white_check_mark:"));
                    }
                    catch (GameNotFoundException e)
                    {
                        await ctx.Message.CreateReactionAsync(DiscordEmoji.FromName(ctx.Client, ":white_check_mark:"));
                        await ctx.Channel.SendMessageAsync("Lobby - " + lobbyID + " does not exist.");
                    }
                }
            }
            catch { }
        }

        [Command("retrieve_game")]
        [Description("Retrieves a game and all its info.")]
        public async Task RetrieveGame (CommandContext ctx, [Description("The lobby id of the game to reverse.")] string lobbyID)
        {
            try
            {
                if (ctx.Channel.ToString().Split()[1] == "#database_control")
                {
                    try
                    {
                        BsonDocument game =  MyMongoDB.FindGame(lobbyID);

                        DiscordEmbedBuilder message = new DiscordEmbedBuilder
                        {
                            Color = DiscordColor.White,
                            Title = "Game " + lobbyID,
                            Description = "Players: "
                        };

                        foreach (var item in game.GetElement("players").Value.AsBsonArray)
                        {
                            message.Description += ctx.Guild.GetMemberAsync(ulong.Parse(item.ToString())).Result.Mention + " ";
                        }

                        message.Description += "\n\nTeam 1: ";

                        try
                        {
                            foreach (var item in game.GetElement("team1").Value.AsBsonArray)
                            {
                                message.Description += ctx.Guild.GetMemberAsync(ulong.Parse(item.ToString())).Result.Mention + " ";
                            }
                        }
                        catch { }

                        message.Description += "\n\nTeam 2: ";

                        try
                        {
                            foreach (var item in game.GetElement("team2").Value.AsBsonArray)
                            {
                                message.Description += ctx.Guild.GetMemberAsync(ulong.Parse(item.ToString())).Result.Mention + " ";
                            }
                        }
                        catch { }


                        int team1MMR = 0;

                        try
                        {
                            foreach (BsonValue player in game.GetElement("team1").Value.AsBsonArray)
                            {
                                BsonDocument playe = MyMongoDB.FindPlayer(player.ToString());
                                team1MMR += (int)playe.GetElement("mmr").Value.AsInt32;
                            }
                        }
                        catch { }

                        int team2MMR = 0;

                        try
                        {
                            foreach (BsonValue player in game.GetElement("team2").Value.AsBsonArray)
                            {
                                BsonDocument playe = MyMongoDB.FindPlayer(player.ToString());
                                team2MMR += (int)playe.GetElement("mmr").Value.AsInt32;
                            }
                        }
                        catch { }

                        double team1PER = Math.Round(((double)team1MMR / ((double)team1MMR + (double)team2MMR) * 100), 1);
                        double team2PER = Math.Round(((double)team2MMR / ((double)team1MMR + (double)team2MMR) * 100), 1);

                        message.Description += "\n\nStart Time: " + game.GetElement("startTime").Value.AsBsonDateTime.ToString() + " UTC\n\n";

                        try
                        {
                            message.Description += "End Time: " + game.GetElement("endTime").Value.AsBsonDateTime.ToString() + " UTC\n\n";
                        }
                        catch
                        {
                            message.Description += "End Time: NaN UTC\n\n";
                        }

                        message.Description += "Winning Team: " + game.GetElement("winningTeam").Value.ToString() + "\n\n";

                        try
                        {
                            message.Description += "+-MMR: " + game.GetElement("mmr+-").Value.ToString() + "\n\n" +
                                              "Team 1 Win%: " + team1PER + "%    Team 2 Win%: " + team2PER + "%\n\n" +
                                              "**Cancel Match** - :no_entry_sign:\n**Reverse Outcome** - :arrows_counterclockwise:\n**Add 2 hour report time** - :stopwatch:";
                        }
                        catch
                        {
                            message.Description += "+-MMR: NaN\n\n" +
                                              "Team 1 Win%: " + team1PER + "%    Team 2 Win%: " + team2PER + "%\n\n" +
                                              "**Cancel Match** - :no_entry_sign:\n**Reverse Outcome** - :arrows_counterclockwise:\n**Add 2 hour report time** - :stopwatch:";
                        }

                                              

                        var interactivity = ctx.Client.GetInteractivity();

                        var embed = await ctx.Channel.SendMessageAsync(embed: message).ConfigureAwait(false);

                        var choice1 = DiscordEmoji.FromName(ctx.Client, ":no_entry_sign:");
                        var choice2 = DiscordEmoji.FromName(ctx.Client, ":arrows_counterclockwise:");
                        var choice3 = DiscordEmoji.FromName(ctx.Client, ":stopwatch:");

                        await embed.CreateReactionAsync(choice1);
                        await embed.CreateReactionAsync(choice2);
                        await embed.CreateReactionAsync(choice3);

                        var result = await interactivity.WaitForReactionAsync(
                            x => x.Message == embed &&
                            x.User == ctx.User &&
                            (x.Emoji == choice1 || x.Emoji == choice2 || x.Emoji == choice3), embed, ctx.User, TimeSpan.FromMinutes(5)).ConfigureAwait(false);

                        if (result.Result == null)
                        {
                            await embed.DeleteAllReactionsAsync();
                            return;
                        }

                        if (result.Result.Emoji == choice1)
                        {
                            MyMongoDB.CancelMatch(lobbyID);
                        }
                        else if (result.Result.Emoji == choice2)
                        {
                            MyMongoDB.ReverseOutcome(lobbyID);
                        }
                        else if (result.Result.Emoji == choice3)
                        {
                            MyMongoDB.AddTime(lobbyID);
                        }

                        await embed.DeleteAllReactionsAsync();
                    }
                    catch (GameNotFoundException e)
                    {
                        await ctx.Channel.SendMessageAsync("Game " + lobbyID + " does not exist.");
                    }
                }
            }
            catch { }
        }

        [Command("retrieve_player")]
        [Description("Retrieves a player and all its info.")]
        public async Task RetrievePlayer(CommandContext ctx, [Description("The lobby id of the game to reverse.")] DiscordMember player)
        {
            try
            {
                if (ctx.Channel.ToString().Split()[1] == "#database_control")
                {
                    try
                    {
                        Player selectedPlayer = MyMongoDB.FindPlayer(ctx, player);

                        RankCheckRegistry registry = MyMongoDB.GetRankCheck(player.Id);

                        DiscordEmbedBuilder message = new DiscordEmbedBuilder
                        {
                            Color = DiscordColor.White,
                            Title = "Player " + player.DisplayName + " - " + player.Id,
                            Description = player.Mention +
                                          "\nTotal Games: " + (selectedPlayer.TotalWins + selectedPlayer.TotalLosses) + "\n\n" +
                                          "Wins: " + selectedPlayer.TotalWins + "\n\n" +
                                          "Losses: " + selectedPlayer.TotalLosses + "\n\n" +
                                          "MMR: " + selectedPlayer.MMR + "\n\n" +
                                          "Rank: " + RankingRanges.GetRankString(selectedPlayer.Rank) + "\n\n" +
                                          "Omega Striker Account: " + registry.OmegaStrikerName + "\n\n" +
                                          "Rank Check: " + RankingRanges.GetRankString(registry.StartingRank) + "\n\n" +
                                          "**Unregister Player:** - :no_pedestrians:"
                        };
                        var interactivity = ctx.Client.GetInteractivity();

                        var embed = await ctx.Channel.SendMessageAsync(embed: message).ConfigureAwait(false);

                        var choice1 = DiscordEmoji.FromName(ctx.Client, ":no_pedestrians:");

                        await embed.CreateReactionAsync(choice1);

                        var result = await interactivity.WaitForReactionAsync(
                            x => x.Message == embed &&
                            x.User == ctx.User &&
                            (x.Emoji == choice1), embed, ctx.User, TimeSpan.FromMinutes(5)).ConfigureAwait(false);

                        if (result.Result == null)
                        {
                            await embed.DeleteAllReactionsAsync();
                            return;
                        }
                        else if (result.Result.Emoji == choice1)
                        {
                            MyMongoDB.UnregisterPlayer(player.Id);
                        }
                      
                        await embed.DeleteAllReactionsAsync();
                    }
                    catch (UserNotRegisteredException e)
                    {
                        await ctx.Channel.SendMessageAsync("Player " + player.DisplayName + " is not registered.");
                    }
                }
            }
            catch { await ctx.Message.CreateReactionAsync(DiscordEmoji.FromName(ctx.Client, ":x:")); }
        }

        [Command("reset_mmr")]
        [Description("Resets everyones MMR to the default of their rank.")]
        //[RequireUserPermissions()]
        public async Task ResetMMR(CommandContext ctx)
        {
            try
            {
                if (ctx.Member.Roles.Contains(ctx.Guild.GetRole(RankingRanges.ModRole)) && ctx.Channel.ToString().Split()[1] == "#database_control")
                {
                    MyMongoDB.ResetMMR();
                    await ctx.Message.CreateReactionAsync(DiscordEmoji.FromName(ctx.Client, ":white_check_mark:"));
                }
            }
            catch { }
        }

        [Command("toggle_q")]
        [Description("Toggles whether players can queue for games or not.")]
        public async Task ToggleQueue ( CommandContext ctx)
        {
            try
            {
                if (ctx.Member.Roles.Contains(ctx.Guild.GetRole(RankingRanges.ModRole)) && ctx.Channel.ToString().Split()[1] == "#database_control")
                {
                    string outcome = string.Empty;
                    if (RankingRanges.CanQueue)
                    {
                        RankingRanges.CanQueue = false;
                        Game.EmptyQueues();
                        outcome = ":lock:";
                    }
                    else
                    {
                        RankingRanges.CanQueue = true;
                        outcome = ":unlock:";
                    }
                    await ctx.Message.CreateReactionAsync(DiscordEmoji.FromName(ctx.Client, ":white_check_mark:"));
                    await ctx.Message.CreateReactionAsync(DiscordEmoji.FromName(ctx.Client, outcome));
                }
            }
            catch { }
        }

        [Command("message")]
        [Description("Sends a message to a specific member")]
        public async Task SendMessage(CommandContext ctx, DiscordMember member, [RemainingText]string message)
        {
            try
            {
                if ( ctx.Channel.ToString().Split()[1] == "#database_control")
                {
                    await member.SendMessageAsync(new DiscordEmbedBuilder { Color = DiscordColor.Purple, Title = "Special Message", Description = message});
                    await ctx.Message.CreateReactionAsync(DiscordEmoji.FromName(ctx.Client, ":white_check_mark:"));
                }
            }
            catch { }
        }

        [Command("promote_to_s")]
        [Description("Promotes a user to S rank")]
        public async Task PromoteToS(CommandContext ctx, DiscordMember member)
        {
            try
            {
                if (ctx.Member.Roles.Contains(ctx.Guild.GetRole(RankingRanges.ModRole)) && ctx.Channel.ToString().Split()[1] == "#database_control")
                {
                    DiscordEmbedBuilder confirmation = new DiscordEmbedBuilder
                    {
                        Color = DiscordColor.White,
                        Title = "Promote To Rank S Confirmation.",
                        Description = "If you are sure you would like to promote " + member.Mention + " to Rank S, press the :white_check_mark:"
                    };

                    var interactivity = ctx.Client.GetInteractivity();

                    var embed = await ctx.Channel.SendMessageAsync(embed: confirmation).ConfigureAwait(false);

                    var choice1 = DiscordEmoji.FromName(ctx.Client, ":x:");
                    var choice2 = DiscordEmoji.FromName(ctx.Client, ":white_check_mark:");

                    await embed.CreateReactionAsync(choice1);
                    await embed.CreateReactionAsync(choice2);

                    var result = await interactivity.WaitForReactionAsync(
                        x => x.Message == embed &&
                        x.User == ctx.User &&
                        (x.Emoji == choice1 || x.Emoji == choice2), embed, ctx.User, TimeSpan.FromMinutes(7)).ConfigureAwait(false);

                    if (result.Result == null || result.Result.Emoji == choice1)
                    {
                        await embed.DeleteAllReactionsAsync();
                        return;
                    }

                    else if (result.Result.Emoji == choice2)
                    {
                        BsonDocument playerDoc = MyMongoDB.FindPlayer(member.Id.ToString());

                        CustomCommands.RevokeRole(member, RankingRanges.Rank2Role);
                        Thread.Sleep(1000);
                        CustomCommands.RevokeRole(member, RankingRanges.Rank3Role);
                        Thread.Sleep(1000);
                        CustomCommands.RevokeRole(member, RankingRanges.Rank4Role);
                        Thread.Sleep(1000);
                        CustomCommands.GrantRole(member, RankingRanges.Rank1Role);
                        Thread.Sleep(1000);

                        MyMongoDB.UpdatePlayer(new BsonDocument { { "id",  playerDoc.GetElement("id").Value.ToString() },
                                                        { "win", (int)playerDoc.GetElement("win").Value },
                                                        { "lose", (int)playerDoc.GetElement("lose").Value },
                                                        { "mmr", RankingRanges.Rank1Start },
                                                        { "rank", 1} });

                        member.SendMessageAsync(new DiscordEmbedBuilder { Title = "Congratulations!", Color = DiscordColor.Gold, Description = "You have been promoted to Rank S" });
                    }
                }
            }
            catch { }
        }

        [Command("delete_messages")]
        [Description("Sends a message to a specific member")]
        public async Task DeleteMessages(CommandContext ctx, int numMessages)
        {
            try
            {
                if (ctx.Member.Roles.Contains(ctx.Guild.GetRole(RankingRanges.ModRole)))
                {
                    var messages = ctx.Channel.GetMessagesBeforeAsync(ctx.Message.Id).Result;

                    for (int i = 0; i < numMessages; i++)
                    {
                        await ctx.Channel.DeleteMessageAsync(messages[i]);
                    }

                    await ctx.Message.DeleteAsync();
                }
            }
            catch { await ctx.Message.DeleteAsync(); }
        }

        [Command("ban")]
        [Description("Bans a user for a given amount of time")]
        public async Task BanUser(CommandContext ctx, DiscordMember member, int days,[RemainingText]string reason)
        {
            try
            {
                if (ctx.Channel.ToString().Split()[1] == "#database_control")
                {
                    DateTime unBan = DateTime.Now.AddDays(days);

                    DiscordEmbedBuilder confirmation = new DiscordEmbedBuilder
                    {
                        Color = DiscordColor.White,
                        Title = "Ban User "+member.DisplayName,
                        Description = "Please Confirm the Ban for member " + member.Mention + ":\n\n**Duration:** "+days+" days\n\n**Reason:** "+reason+"\n\nPress the :white_check_mark: to confirm."
                    };

                    var interactivity = ctx.Client.GetInteractivity();

                    var embed = await ctx.Channel.SendMessageAsync(embed: confirmation).ConfigureAwait(false);

                    var choice1 = DiscordEmoji.FromName(ctx.Client, ":x:");
                    var choice2 = DiscordEmoji.FromName(ctx.Client, ":white_check_mark:");

                    await embed.CreateReactionAsync(choice1);
                    await embed.CreateReactionAsync(choice2);

                    var result = await interactivity.WaitForReactionAsync(
                        x => x.Message == embed &&
                        x.User == ctx.User &&
                        (x.Emoji == choice1 || x.Emoji == choice2), embed, ctx.User, TimeSpan.FromMinutes(7)).ConfigureAwait(false);

                    if (result.Result == null || result.Result.Emoji == choice1)
                    {
                        await embed.DeleteAllReactionsAsync();
                        return;
                    }

                    else if (result.Result.Emoji == choice2)
                    {
                        MyMongoDB.SetBan(new BsonDocument { { "playerID", member.Id.ToString() },
                                                            { "endTime", unBan},
                                                            { "reason", reason},
                                                            { "completed", false},
                                                            { "issuedBy", ctx.User.Id.ToString()} });


                        GrantRole(member, RankingRanges.BanRole);

                        await member.SendMessageAsync(new DiscordEmbedBuilder { Color = DiscordColor.Red, Title = "Banned!", Description = "You have been banned from Omega 6ixes.\n\n**Duration:** "+days+" days\n\n**Reason:** "+reason});
                    }

                    embed.DeleteAllReactionsAsync();
                }
            }
            catch { }
        }

        [Command("get_record")]
        [Description("Gets the ban record of the user")]
        public async Task GetRecord(CommandContext ctx, DiscordMember member)
        {
            try
            {
                if (ctx.Channel.ToString().Split()[1] == "#database_control")
                {
                    List<BsonDocument> bans =  MyMongoDB.GetBans(member.Id.ToString());

                    if (bans.Count == 0)
                    {
                        await ctx.Channel.SendMessageAsync(new DiscordEmbedBuilder { Color = DiscordColor.Green, Title = "Clean!", Description = member.Mention + " has a clean record!" });
                    }
                    else
                    {
                        DiscordEmbedBuilder message = new DiscordEmbedBuilder();
                        message.Color = DiscordColor.Yellow;
                        message.Title = member.DisplayName+"'s Ban History";
                        message.Description = "Total number of bans for "+member.Mention+": "+bans.Count;
                        foreach (BsonDocument ban in bans)
                        {
                            if (ban.GetElement("completed").Value.AsBoolean)
                            {
                                message.Description += "\n\n:man_judge: " + ban.GetElement("reason").Value.ToString();
                                message.Description += "\n**Completed**";
                            }
                            else
                            {
                                message.Description += "\n\n:man_police_officer: " + ban.GetElement("reason").Value.ToString();
                                message.Description += "\nOngoing: " + ban.GetElement("endTime").Value.AsDateTime.ToString()+" UTC";
                            }
                            message.Description += "\nIssued By: " + ctx.Guild.GetMemberAsync(ulong.Parse(ban.GetElement("issuedBy").Value.ToString())).Result.Mention;
                        }
                        await ctx.Channel.SendMessageAsync(message);
                    }
                }
            }
            catch { }
        }

        [Command("unban")]
        [Description("Unbans a user")]
        public async Task UnBanUser(CommandContext ctx, DiscordMember member)
        {
            try
            {
                if (ctx.Channel.ToString().Split()[1] == "#database_control")
                {
                    RevokeRole(member, RankingRanges.BanRole);

                    await member.SendMessageAsync(new DiscordEmbedBuilder { Color = DiscordColor.Green, Title = "Unbanned!", Description = "You have been manually unbanned and you are free to queue Omega 6ixes again." });
                    await ctx.Message.CreateReactionAsync(DiscordEmoji.FromName(ctx.Client, ":white_check_mark:"));
                }
            }
            catch { }
        }

        [Command("register")]
        [Description("Registers a player")]
        public async Task RegisterUser(CommandContext ctx, DiscordMember member, string OmegeStrikerName, int rank)
        {
            try
            {
                if (ctx.Channel.ToString().Split()[1] == "#database_control")
                {
                    try
                    {
                        Player checkedPlayer = MyMongoDB.FindPlayer(ctx, member);

                        await ctx.Channel.SendMessageAsync(new DiscordEmbedBuilder { Color = DiscordColor.Red, Title = "Already Registered", Description = "This user is already registered. If you wish to promote/demote the user, be sure use !retrieve_player to unregister them then proceed to register them again." });
                    }
                    catch (UserNotRegisteredException)
                    {
                        Player player = MyMongoDB.CreatePlayer(member, rank);

                        MyMongoDB.SaveRankCheck(new RankCheckRegistry(member.Id, OmegeStrikerName, rank));
                        if (rank == 2)
                        {
                            RevokeRole(player, ctx, RankingRanges.Rank1Role);
                            Thread.Sleep(1000);
                            RevokeRole(player, ctx, RankingRanges.Rank3Role);
                            Thread.Sleep(1000);
                            RevokeRole(player, ctx, RankingRanges.Rank4Role);
                            Thread.Sleep(1000);
                            GrantRole(player, ctx, RankingRanges.Rank2Role);
                        }
                        else if (rank == 3)
                        {
                            RevokeRole(player, ctx, RankingRanges.Rank1Role);
                            Thread.Sleep(1000);
                            RevokeRole(player, ctx, RankingRanges.Rank2Role);
                            Thread.Sleep(1000);
                            RevokeRole(player, ctx, RankingRanges.Rank4Role);
                            Thread.Sleep(1000);
                            GrantRole(player, ctx, RankingRanges.Rank3Role);
                        }
                        else if (rank == 4)
                        {
                            RevokeRole(player, ctx, RankingRanges.Rank1Role);
                            Thread.Sleep(1000);
                            RevokeRole(player, ctx, RankingRanges.Rank2Role);
                            Thread.Sleep(1000);
                            RevokeRole(player, ctx, RankingRanges.Rank3Role);
                            Thread.Sleep(1000);
                            GrantRole(player, ctx, RankingRanges.Rank4Role);
                        }
                        await member.SendMessageAsync(new DiscordEmbedBuilder
                        {
                            Color = DiscordColor.Green,
                            Title = "Success!",
                            Description = "You are now in " + RankingRanges.GetRankString(player.Rank)
                        });
                    }
                    await ctx.Message.CreateReactionAsync(DiscordEmoji.FromName(ctx.Client, ":white_check_mark:"));
                }
            }
            catch { }
        }

        [Command("mod_help")]
        [Description("Help about all commands for the bot.")]
        public async Task ModHelp(CommandContext ctx)
        {
            try
            {
                if (ctx.Channel.ToString().Split()[1] == "#database_control")
                {
                    await ctx.Member.SendMessageAsync(new DiscordEmbedBuilder
                    {

                        Color = DiscordColor.Cyan,
                        Title = "Mod Help",
                        Description = "*Commands*\n" +
                        "**!update_leaderboard** Will update the leaderboard.\n" +
                        "**!reverse_outcome [game id]** Will reverse the outcome of a game (Not recommended. Do this through !retrieve_game).\n" +
                        "**!retrieve_game [game id]** Will will retrieve all the game details and allow you to make changes to the game.\n" +
                        "**!retrieve_player [discord member name or id]** Will GEt all the players info and allow you to make changes to their account.\n" +
                        "**!message [discord member name or id] [message you want to send]** Will send a message to a user through the bot.\n" +
                        "**!ban [discord member name or id] [number of days] [the reason for the ban]** Will ban a user and send them a message for the reason why.\n" +
                        "**!get_record [discord member name or id]** Will display the users ban record.\n" +
                        "**!unban [discord member name or id]** Will manually unban the user.\n" +
                        "**!register [discord member name or id] [Omega Striker name] [rank they will join (A = 2, B = 3, C = 4)]** Will manually rankcheck a user.\n\n" +
                        "*Admin Commands*\n" +
                        "**!wipe_games** Will delete all the games in the database.\n" +
                        "**!wipe_players** Will delete all players in the database.\n" +
                        "**!reset_mmr** Will reset everyones mmr to their ranks default value.\n" +
                        "**!toggle_q** Will lock or unlock the queues allowing players to queue or not."
                    }); 
                }
            }
            catch { }
        }
        #endregion


        #region Custom Functions
        /// <summary>
        /// revokes  roles for it to be properly reset
        /// </summary>
        /// <param name="player"></param>
        public static async void RevokeRole(Player player, CommandContext ctx, ulong RoleID)
        {
            await player.Account.RevokeRoleAsync(ctx.Guild.GetRole(RoleID));
        }

        /// <summary>
        /// revokes roles for the member
        /// </summary>
        /// <param name="member"></param>
        /// <param name="RoleID"></param>
        public static async void RevokeRole(DiscordMember member,  ulong RoleID)
        {
            await member.RevokeRoleAsync(member.Guild.GetRole(RoleID));
        }

        /// <summary>
        /// grants a role
        /// </summary>
        /// <param name="player"></param>
        /// <param name="ctx"></param>
        /// <param name="RoleID"></param>
        public static async void GrantRole(Player player, CommandContext ctx, ulong RoleID)
        {
            await player.Account.GrantRoleAsync(ctx.Guild.GetRole(RoleID));
        }

        /// <summary>
        /// grants roles to the member
        /// </summary>
        /// <param name="member"></param>
        /// <param name="RoleID"></param>
        public static async void GrantRole(DiscordMember member, ulong RoleID)
        {
            await member.GrantRoleAsync(member.Guild.GetRole(RoleID));
        }
        /// <summary>
        /// when a q fills with 6 players
        /// </summary>
        private async void FullQ(CommandContext ctx)
        {
            string region = string.Empty;
            int rank = 0;

            if (ctx.Channel.ToString().Split()[1] == "#na-rank-s")
            {
                region = "NA";
                rank = 1;
            }
            else if (ctx.Channel.ToString().Split()[1] == "#na-rank-a")
            {
                region = "NA";
                rank = 2;
            }
            else if (ctx.Channel.ToString().Split()[1] == "#na-rank-b")
            {
                region = "NA";
                rank = 3;
            }
            else if (ctx.Channel.ToString().Split()[1] == "#na-rank-c")
            {
                region = "NA";
                rank = 4;
            }
            else if (ctx.Channel.ToString().Split()[1] == "#na-rank-universal")
            {
                region = "NA";
                rank = 0;
            }
            else if (ctx.Channel.ToString().Split()[1] == "#eu-rank-s")
            {
                region = "EU";
                rank = 1;
            }
            else if (ctx.Channel.ToString().Split()[1] == "#eu-rank-a")
            {
                region = "EU";
                rank = 2;
            }
            else if (ctx.Channel.ToString().Split()[1] == "#eu-rank-b")
            {
                region = "EU";
                rank = 3;
            }
            else if (ctx.Channel.ToString().Split()[1] == "#eu-rank-c")
            {
                region = "EU";
                rank = 4;
            }
            else if (ctx.Channel.ToString().Split()[1] == "#eu-rank-universal")
            {
                region = "EU";
                rank = 0;
            }

            if (region != string.Empty)
            {
                try
                {
                    Game.CreateMatch(ctx, region, rank);
                }
                catch (GameExceededTimeException e) 
                {
                    await ctx.Channel.SendMessageAsync(new DiscordEmbedBuilder { Color = DiscordColor.Red, Title = "Game "+e.Message+" Canceled", Description = "Captains took too long to pick."}).ConfigureAwait(false);
                }
            }
        }

        /// <summary>
        /// gets and sets the rank of the player
        /// </summary>
        private async void GetRank(CommandContext ctx)
        {
            //ctx.Member.
        }

        /// <summary>
        /// checks if the player is in a lobby that is currently picking captains
        /// </summary>
        /// <param name="player"></param>
        private void CheckGames(Player player)
        {
            foreach (Game game in Game.PreGames)
            {
                foreach (Player playerGame in game.Players)
                {
                    if (playerGame.Account.Id == player.Account.Id)
                    {
                        playerGame.QTime = DateTime.Now;
                        throw new UserAlreadyInQueueException(player.Account.Mention + " you are already in a game. ("+game.ID+")");
                    }
                }

                if (game.Team1[0].Account.Id == player.Account.Id || game.Team2[0].Account.Id == player.Account.Id)
                {
                    throw new UserAlreadyInQueueException(player.Account.Mention + " you are already in a game. (" + game.ID + ")");
                }
            }
        }

        /// <summary>
        /// updates the leaderboard in the leaderboard channel
        /// </summary>
        public static async void UpdateLeaderboard(CommandContext ctx)
        {
            MyMongoDB.storedCTX = ctx;

            DiscordChannel leaderboardChannel = GetChannel(RankingRanges.LeaderboardCHannelID);

            if (RankingRanges.Leaderboard == null)
            {
                var items = leaderboardChannel.GetMessagesAsync().Result;
                if (items.Count != 0)
                {
                    await leaderboardChannel.DeleteMessagesAsync(items);
                }
                MyMongoDB.GetLeaderboard(ctx);
                RankingRanges.Leaderboard = await leaderboardChannel.SendMessageAsync(embed: RankingRanges.LeaderboardEmbed).ConfigureAwait(false);
            }
            else
            {
                MyMongoDB.GetLeaderboard(ctx);
                await RankingRanges.Leaderboard.ModifyAsync((DiscordEmbed)RankingRanges.LeaderboardEmbed);
            }
                
            RankingRanges.LastUpdated = DateTime.Now;
        }

        /// <summary>
        /// returns channel 
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="channelID"></param>
        /// <returns></returns>
        public static DiscordChannel? GetChannel(ulong channelID)
        {
            int numchannels = MyMongoDB.storedCTX.Guild.Channels.Count;

            DiscordChannel[] ArrayChannels = new List<DiscordChannel>(MyMongoDB.storedCTX.Guild.Channels.Values).ToArray();

            for (int y = 0; y < numchannels; y++)
            {
                if (ArrayChannels[y].Id == channelID)
                {
                    return ArrayChannels[y];
                }
            }

            return null;
        }

        /// <summary>
        /// perform the rankcheck
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="registry"></param>
        /// <param name="isnewuser"></param>
        public static async void PerformRankCheck(CommandContext ctx, RankCheckRegistry registry, bool newregistry)
        {
            if (ctx.Member != null)
            {

                #region Confirm rankcheck
                if (newregistry)
                {
                    DiscordEmbedBuilder embed = new DiscordEmbedBuilder
                    {
                        Color = DiscordColor.Yellow,
                        Title = "Confirm Rankcheck!",
                        Description = "Confirm that you would like to rankcheck, and link your discord to **" + registry.OmegaStrikerName +
                                        "**. \n\n**NOTE:**\nIt is a **BANNABLE** offense to link an account that is not owned by you.\nYou can "
                                        + "only link **1** account to your discord.\n\nReact with the green check to confirm rankcheck, or react " +
                                        "with the red x to cancel."
                    };

                    var interactivity = ctx.Client.GetInteractivity();

                    var message = await ctx.Member.SendMessageAsync(embed);

                    await message.CreateReactionAsync(DiscordEmoji.FromName(ctx.Client, ":white_check_mark:"));
                    await message.CreateReactionAsync(DiscordEmoji.FromName(ctx.Client, ":x:"));

                    var result = await interactivity.WaitForReactionAsync(
                        x => x.Message == message &&
                        x.User == ctx.User &&
                        (x.Emoji == DiscordEmoji.FromName(ctx.Client, ":white_check_mark:") || x.Emoji == DiscordEmoji.FromName(ctx.Client, ":x:")), message, ctx.User, TimeSpan.FromMinutes(7)).ConfigureAwait(false);

                    if (result.Result == null || result.Result.Emoji == DiscordEmoji.FromName(ctx.Client, ":x:"))
                    {
                        return;
                    }
                }
                #endregion


                #region WebRequest

                WebRequest request = null;
                request = WebRequest.Create("https://strikr.gg/pilot/" + registry.OmegaStrikerName);

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                Stream datastream = response.GetResponseStream();

                StreamReader reader = new StreamReader(datastream);

                string data = reader.ReadToEnd();

                datastream.Close();
                reader.Close();
                response.Close();
                #endregion

                #region Rank S
                /*
                if (data.Contains("[Omega]"))
                {
                    registry.StartingRank = 1;
                    if (!newregistry)
                    {
                        Player player = MyMongoDB.FindPlayer(ctx);

                        if (player.Rank != 1 )
                        {
                            RankCheckRegistry regis = MyMongoDB.GetRankCheck(ctx.Member.Id);
                            if (regis.StartingRank >= 1)
                            {
                                await ctx.Member.SendMessageAsync(new DiscordEmbedBuilder
                                {
                                    Color = DiscordColor.Red,
                                    Title = "Denied!",
                                    Description = "Sorry, but you already rankchecked, then proceeded to demote. You cannot rankcheck back into the rank you demoted from."
                                });
                            }
                            else
                            {
                                player.MMR = RankingRanges.Rank1Start;
                                player.Rank = 1;
                                MyMongoDB.UpdatePlayer(player);


                                RevokeRole(player, ctx, RankingRanges.Rank2Role);
                                Thread.Sleep(1000);
                                RevokeRole(player, ctx, RankingRanges.Rank3Role);
                                Thread.Sleep(1000);
                                RevokeRole(player, ctx, RankingRanges.Rank4Role);
                                Thread.Sleep(1000);
                                GrantRole(player, ctx, RankingRanges.Rank1Role);


                                await ctx.Member.SendMessageAsync(new DiscordEmbedBuilder { Color = DiscordColor.Green, Title = "Success!", Description = "You are now in " + RankingRanges.GetRankString(player.Rank) });
                            }
                        }
                        else
                        {
                            await ctx.Member.SendMessageAsync(new DiscordEmbedBuilder { Color = DiscordColor.Yellow, Title = "No Change!", Description = "You are currently in the rank you belong in (" + RankingRanges.GetRankString(player.Rank) + ")" });
                        }
                    }
                    else
                    {
                        Player player = MyMongoDB.CreatePlayer(ctx, 1);

                        MyMongoDB.SaveRankCheck(registry);

                        RevokeRole(player, ctx, RankingRanges.Rank2Role);
                        Thread.Sleep(1000);
                        RevokeRole(player, ctx, RankingRanges.Rank3Role);
                        Thread.Sleep(1000);
                        RevokeRole(player, ctx, RankingRanges.Rank4Role);
                        Thread.Sleep(1000);
                        GrantRole(player, ctx, RankingRanges.Rank1Role);

                        await ctx.Member.SendMessageAsync(new DiscordEmbedBuilder
                        {
                            Color = DiscordColor.Green,
                            Title = "Success!",
                            Description = "You are now in " + RankingRanges.GetRankString(player.Rank)
                        });
                    }
                }
                */
                #endregion

                #region Rank A
                if (data.Contains(">Mid Challenger<") || data.Contains(">High Challenger<") || data.Contains(">Omega<") || data.Contains(">Pro League<"))
                {
                    registry.StartingRank = 2;
                    if (!newregistry)
                    {
                        Player player = MyMongoDB.FindPlayer(ctx);

                        if (player.Rank != 2)
                        {
                            RankCheckRegistry regis = MyMongoDB.GetRankCheck(ctx.Member.Id);
                            if (regis.StartingRank >= 2)
                            {
                                await ctx.Member.SendMessageAsync(new DiscordEmbedBuilder
                                {
                                    Color = DiscordColor.Red,
                                    Title = "Denied!",
                                    Description = "Sorry, but you already rankchecked into your desired rank. You cannot rankcheck back into the rank you demoted from or are currently in."
                                });
                                if (player.Rank == 3)
                                {
                                    GrantRole(player, ctx, RankingRanges.Rank3Role);
                                }
                                else if (player.Rank == 4)
                                {
                                    GrantRole(player, ctx, RankingRanges.Rank4Role);
                                }
                            }
                            else
                            {
                                player.MMR = RankingRanges.Rank2Start;
                                player.Rank = 2;
                                MyMongoDB.UpdatePlayer(player);

                                MyMongoDB.UpdateRankCheck(new BsonDocument { { "PlayerID", registry.LinkedDiscordUser.ToString() },
                                                                             { "Omega6Name", registry.OmegaStrikerName } ,
                                                                             { "StartingRank", registry.StartingRank} });

                                RevokeRole(player, ctx, RankingRanges.Rank1Role);
                                Thread.Sleep(1000);
                                RevokeRole(player, ctx, RankingRanges.Rank3Role);
                                Thread.Sleep(1000);
                                RevokeRole(player, ctx, RankingRanges.Rank4Role);
                                Thread.Sleep(1000);
                                GrantRole(player, ctx, RankingRanges.Rank2Role);


                                await ctx.Member.SendMessageAsync(new DiscordEmbedBuilder { Color = DiscordColor.Green, Title = "Success!", Description = "You are now in " + RankingRanges.GetRankString(player.Rank) });
                            }
                        }
                        else
                        {
                            await ctx.Member.SendMessageAsync(new DiscordEmbedBuilder { Color = DiscordColor.Yellow, Title = "No Change!", Description = "You are currently in the rank you belong in (" + RankingRanges.GetRankString(player.Rank) + ")" });
                            GrantRole(player, ctx, RankingRanges.Rank2Role);
                        }
                    }
                    else
                    {
                        Player player = MyMongoDB.CreatePlayer(ctx, 2);

                        MyMongoDB.SaveRankCheck(registry);

                        RevokeRole(player, ctx, RankingRanges.Rank1Role);
                        Thread.Sleep(1000);
                        RevokeRole(player, ctx, RankingRanges.Rank3Role);
                        Thread.Sleep(1000);
                        RevokeRole(player, ctx, RankingRanges.Rank4Role);
                        Thread.Sleep(1000);
                        GrantRole(player, ctx, RankingRanges.Rank2Role);

                        await ctx.Member.SendMessageAsync(new DiscordEmbedBuilder
                        {
                            Color = DiscordColor.Green,
                            Title = "Success!",
                            Description = "You are now in " + RankingRanges.GetRankString(player.Rank)
                        });
                    }
                }
                #endregion

                #region Rank B
                else if (data.Contains(">Low Diamond<") || data.Contains(">Mid Diamond<") || data.Contains(">High Diamond<") || data.Contains(">Low Challenger<"))
                {
                    registry.StartingRank = 3;
                    if (!newregistry)
                    {
                        Player player = MyMongoDB.FindPlayer(ctx);

                        if (player.Rank != 3)
                        {
                            RankCheckRegistry regis = MyMongoDB.GetRankCheck(ctx.Member.Id);
                            if (regis.StartingRank >= 3)
                            {
                                await ctx.Member.SendMessageAsync(new DiscordEmbedBuilder
                                {
                                    Color = DiscordColor.Red,
                                    Title = "Denied!",
                                    Description = "Sorry, but you already rankchecked into your desired rank. You cannot rankcheck back into the rank you demoted from or are currently in."
                                });

                                if (player.Rank == 4)
                                {
                                    GrantRole(player, ctx, RankingRanges.Rank4Role);
                                }
                            }
                            else
                            {
                                player.MMR = RankingRanges.Rank3Start;
                                player.Rank = 3;
                                MyMongoDB.UpdatePlayer(player);

                                MyMongoDB.UpdateRankCheck(new BsonDocument { { "PlayerID", registry.LinkedDiscordUser.ToString() },
                                                                             { "Omega6Name", registry.OmegaStrikerName } ,
                                                                             { "StartingRank", registry.StartingRank} });

                                RevokeRole(player, ctx, RankingRanges.Rank1Role);
                                Thread.Sleep(1000);
                                RevokeRole(player, ctx, RankingRanges.Rank2Role);
                                Thread.Sleep(1000);
                                RevokeRole(player, ctx, RankingRanges.Rank4Role);
                                Thread.Sleep(1000);
                                GrantRole(player, ctx, RankingRanges.Rank3Role);


                                await ctx.Member.SendMessageAsync(new DiscordEmbedBuilder { Color = DiscordColor.Green, Title = "Success!", Description = "You are now in " + RankingRanges.GetRankString(player.Rank) });
                            }
                        }
                        else
                        {
                            await ctx.Member.SendMessageAsync(new DiscordEmbedBuilder { Color = DiscordColor.Yellow, Title = "No Change!", Description = "You are currently in the rank you belong in (" + RankingRanges.GetRankString(player.Rank) + ")" });
                            GrantRole(player, ctx, RankingRanges.Rank3Role);
                        }
                    }
                    else
                    {
                        Player player = MyMongoDB.CreatePlayer(ctx, 3);

                        MyMongoDB.SaveRankCheck(registry);

                        RevokeRole(player, ctx, RankingRanges.Rank1Role);
                        Thread.Sleep(1000);
                        RevokeRole(player, ctx, RankingRanges.Rank2Role);
                        Thread.Sleep(1000);
                        RevokeRole(player, ctx, RankingRanges.Rank4Role);
                        Thread.Sleep(1000);
                        GrantRole(player, ctx, RankingRanges.Rank3Role);

                        await ctx.Member.SendMessageAsync(new DiscordEmbedBuilder
                        {
                            Color = DiscordColor.Green,
                            Title = "Success!",
                            Description = "You are now in " + RankingRanges.GetRankString(player.Rank)
                        });
                    }
                }
                #endregion

                #region Rank C
                else if (data.Contains(">Low Gold<") || data.Contains(">Mid Gold<") || data.Contains(">High Gold<") || data.Contains(">Low Platinum<") || data.Contains(">Mid Platinum<") || data.Contains(">High Platinum<"))
                {
                    registry.StartingRank = 4;
                    if (!newregistry)
                    {
                        Player player = MyMongoDB.FindPlayer(ctx);

                        if (player.Rank != 4)
                        {
                            RankCheckRegistry regis = MyMongoDB.GetRankCheck(ctx.Member.Id);
                            if (regis.StartingRank >= 4)
                            {
                                await ctx.Member.SendMessageAsync(new DiscordEmbedBuilder
                                {
                                    Color = DiscordColor.Red,
                                    Title = "Denied!",
                                    Description = "Sorry, but you already rankchecked into your desired rank. You cannot rankcheck back into the rank you demoted from or are currently in."
                                });
                            }
                            else
                            {
                                player.MMR = RankingRanges.Rank4Start;
                                player.Rank = 4;
                                MyMongoDB.UpdatePlayer(player);

                                MyMongoDB.UpdateRankCheck(new BsonDocument { { "PlayerID", registry.LinkedDiscordUser.ToString() },
                                                                             { "Omega6Name", registry.OmegaStrikerName } ,
                                                                             { "StartingRank", registry.StartingRank} });

                                RevokeRole(player, ctx, RankingRanges.Rank1Role);
                                Thread.Sleep(1000);
                                RevokeRole(player, ctx, RankingRanges.Rank2Role);
                                Thread.Sleep(1000);
                                RevokeRole(player, ctx, RankingRanges.Rank3Role);
                                Thread.Sleep(1000);
                                GrantRole(player, ctx, RankingRanges.Rank4Role);


                                await ctx.Member.SendMessageAsync(new DiscordEmbedBuilder { Color = DiscordColor.Green, Title = "Success!", Description = "You are now in " + RankingRanges.GetRankString(player.Rank) });
                            }
                        }
                        else
                        {
                            await ctx.Member.SendMessageAsync(new DiscordEmbedBuilder { Color = DiscordColor.Yellow, Title = "No Change!", Description = "You are currently in the rank you belong in (" + RankingRanges.GetRankString(player.Rank) + ")" });
                            GrantRole(player, ctx, RankingRanges.Rank4Role);
                        }
                    }
                    else
                    {
                        Player player = MyMongoDB.CreatePlayer(ctx, 4);

                        MyMongoDB.SaveRankCheck(registry);

                        RevokeRole(player, ctx, RankingRanges.Rank1Role);
                        Thread.Sleep(1000);
                        RevokeRole(player, ctx, RankingRanges.Rank2Role);
                        Thread.Sleep(1000);
                        RevokeRole(player, ctx, RankingRanges.Rank3Role);
                        Thread.Sleep(1000);
                        GrantRole(player, ctx, RankingRanges.Rank4Role);

                        await ctx.Member.SendMessageAsync(new DiscordEmbedBuilder
                        {
                            Color = DiscordColor.Green,
                            Title = "Success!",
                            Description = "You are now in " + RankingRanges.GetRankString(player.Rank)
                        });
                    }
                }
                #endregion



                else
                {
                    DiscordEmbedBuilder embed = new()
                    {
                        Color = DiscordColor.Red,
                        Title = "Unqualified!",
                        Description = "Sorry, but you do not meet the requirements to participate in Omega 6ixes.\n\n" +
                                      "This Omega account will not been linked to your Discord.\n\n" +
                                      "**Reasons for unqualification**\n" +
                                      "- Not a high enough rank\n" +
                                      "- Mistyped username\n" +
                                      "- Not within the top 10k of players\n" +
                                      "- Did not specify a region you are top 10k in\n\n" +
                                      "If you believe you do fit the minimum rank requirement of **Low Gold** then please react with :ballot_box_with_ballot:"
                    };

                    var interactivity = ctx.Client.GetInteractivity();



                    var message = await ctx.Member.SendMessageAsync(embed);

                    await message.CreateReactionAsync(DiscordEmoji.FromName(ctx.Client, ":ballot_box_with_ballot:"));

                    var result = await interactivity.WaitForReactionAsync(
                       x => x.Message == message &&
                       x.User == ctx.User &&
                       (x.Emoji == DiscordEmoji.FromName(ctx.Client, ":ballot_box_with_ballot:")), message, ctx.User, TimeSpan.FromMinutes(10)).ConfigureAwait(false);

                    if (result.Result == null)
                    {
                        return;
                    }

                    if (result.Result.Emoji == DiscordEmoji.FromName(ctx.Client, ":ballot_box_with_ballot:"))
                    {
                        var interactivity2 = ctx.Client.GetInteractivity();

                        var message2 = await ctx.Member.SendMessageAsync(new DiscordEmbedBuilder
                        {
                            Color = DiscordColor.DarkBlue,
                            Title = "Submit Proof",
                            Description = "Please respond to this message within the next 15 minutes with proof of your rank.\n\n" +
                                          "Valid proof must be a screenshot that shows your rank AND your username. (Cannot be a link, must be a file)\n\n" +
                                          "Thank you, I will be patiently waiting for your screenshot :camera:"
                        });

                        var result2 = await interactivity2.WaitForMessageAsync(
                           x => x.Attachments.Count == 1 &&
                       x.Author == ctx.User
                           , TimeSpan.FromMinutes(15)).ConfigureAwait(false);

                        if (result2.Result == null)
                        {
                            await ctx.Member.SendMessageAsync("15 minutes has passed, if you wish to still send rank proof, please repeat the process and submit the proof.");
                            return;
                        }
                        else
                        {
                            await ctx.Member.SendMessageAsync("Thank you, the screenshot has been sent to admins. Please give up to 24 hours for your request to be fullfilled.");

                            await GetChannel(RankingRanges.RankCheckTicket).SendMessageAsync(new DiscordEmbedBuilder { Color = DiscordColor.Wheat, Title = "Rank Check", Description = 
                                "**User:** " + ctx.Member.Mention + "\n" +
                                "**Omega Striker Name:** " + registry.OmegaStrikerName + "\n" +
                                "**Proof:** " + result2.Result.Attachments.Single().Url });
                        }

                    }
                }

                #region Validated Bans
                List<BsonDocument> bans = MyMongoDB.GetBans(ctx.User.Id.ToString());

                if (bans.Count > 0)
                {
                    foreach (BsonDocument ban in bans) 
                    { 
                        if (!ban.GetElement("completed").Value.AsBoolean)
                        {
                            GrantRole(MyMongoDB.FindPlayer(ctx), ctx, RankingRanges.BanRole);
                            break;
                        }
                    }
                }
                #endregion
            }

        }

        /// <summary>
        /// checks for timeouts
        /// </summary>
        /// <param name="region"></param>
        /// <param name="rank"></param>
        private List<Player> CheckForTimeOuts(string region, int rank)
        {
            List<Player> currentPlayers = Game.GetPlayersInQ(region, rank);
            List<Player> playersToRemove = new List<Player>();
            foreach (Player selectedPlayer in currentPlayers)
            {
                TimeSpan time = DateTime.Now.Subtract(selectedPlayer.QTime);
                if (time.Hours >= 1)
                {
                    playersToRemove.Add(selectedPlayer);
                }
            }
            foreach (Player selectedPlayer in playersToRemove)
            {
                Game.RemovePlayerFromQ(selectedPlayer, region, rank);
            }
            return playersToRemove;
        }

        /// <summary>
        /// auto unbans players
        /// </summary>
        public static void AutoUnbanPlayers()
        {
            List<BsonDocument> bans = MyMongoDB.GetAllBans();

            if(bans.Count > 0)
            {
                foreach (BsonDocument ban in bans)
                {
                    if (!ban.GetElement("completed").Value.AsBoolean)
                    {
                        int min = ban.GetElement("endTime").Value.AsDateTime.Subtract(DateTime.Now).Minutes;
                        int hour = ban.GetElement("endTime").Value.AsDateTime.Subtract(DateTime.Now).Hours;
                        int day = ban.GetElement("endTime").Value.AsDateTime.Subtract(DateTime.Now).Days;

                        if (min <= 0 && hour <= 0 && day <= 0)
                        {
                            DiscordMember member = MyMongoDB.storedCTX.Guild.GetMemberAsync(ulong.Parse(ban.GetElement("playerID").Value.ToString())).Result;

                            RevokeRole(member, RankingRanges.BanRole);

                            MyMongoDB.UpdateBan(new BsonDocument { { "playerID", ban.GetElement("playerID").Value.ToString()},
                                                                   { "endTime", ban.GetElement("endTime").Value.AsDateTime},
                                                                   { "reason", ban.GetElement("reason").Value.ToString()},
                                                                   { "completed", true},
                                                                   { "issuedBy", ban.GetElement("issuedBy").Value.ToString()} });

                            member.SendMessageAsync(new DiscordEmbedBuilder { Color = DiscordColor.Green, Title = "Unbanned!", Description = "Your ban timer has ended and you are free to queue Omega 6ixes again."});
                        }
                    }
                }
            }
        }

        /// <summary>
        /// runs all the startup functions
        /// </summary>
        /// <param name="ctx"></param>
        public static async void StartupFunctions(CommandContext ctx)
        {
            MyMongoDB.storedCTX = ctx;
            MyMongoDB.UpdateRanks();
            UpdateLeaderboard(MyMongoDB.storedCTX);
            await Game.CleanUpChannels(MyMongoDB.storedCTX);
            AutoUnbanPlayers();
        }
        #endregion
    }
}