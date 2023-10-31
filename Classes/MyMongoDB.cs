using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using Microsoft.VisualBasic;
using Microsoft.Win32;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Omega6.Classes.Exceptions;
using Omega6.Commands;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Omega6.Classes
{
    public class MyMongoDB
    {
        private static string ConnectionString = "mongodb+srv://dallasagsmith:bOosWckeDjoGxxtf@homesevrer.tmigh2b.mongodb.net/test";
        private static string DataBase = "Omega6";

        private static MongoClient? dbClient = null;
        private static IMongoDatabase? dbDatabase = null;

        public static CommandContext storedCTX = null;

        /// <summary>
        /// creates a connection tot he database
        /// </summary>
        /// <returns></returns>
        public static bool Connect()
        {
            dbClient = new MongoClient(ConnectionString);
            if (dbClient == null)
            {
                return false;
            }
            else
            {
                dbDatabase = dbClient.GetDatabase(DataBase);
                if (dbDatabase == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        /// <summary>
        /// finds a player in the database or throws usernotregistered exception
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public static Player FindPlayer(CommandContext player)
        {
            Connect();

            if (player.Member == null)
            {
                throw new UserNotInServerException();
            }
            else
            {
                var filter = Builders<BsonDocument>.Filter.Eq("id", player.Member.Id.ToString());

                var collection = dbDatabase.GetCollection<BsonDocument>("Player");

                var foundplayer = collection.Find(filter).FirstOrDefault();

                if (foundplayer != null)
                {
                    return new Player(player.Member, 
                        (int)foundplayer.GetElement("win").Value,
                        (int)foundplayer.GetElement("lose").Value,
                        (int)foundplayer.GetElement("mmr").Value,
                        (int)foundplayer.GetElement("rank").Value);
                }
                else
                {
                    throw new UserNotRegisteredException();
                }
            }
        }

        /// <summary>
        /// finds a player by the memebr
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="player"></param>
        /// <returns></returns>
        /// <exception cref="UserNotInServerException"></exception>
        /// <exception cref="UserNotRegisteredException"></exception>
        public static Player FindPlayer(CommandContext ctx, DiscordMember player)
        {
            Connect();

            if (player == null)
            {
                throw new UserNotInServerException();
            }
            else
            {
                var filter = Builders<BsonDocument>.Filter.Eq("id", player.Id.ToString());

                var collection = dbDatabase.GetCollection<BsonDocument>("Player");

                var foundplayer = collection.Find(filter).FirstOrDefault();

                if (foundplayer != null)
                {
                    return new Player(player,
                        (int)foundplayer.GetElement("win").Value,
                        (int)foundplayer.GetElement("lose").Value,
                        (int)foundplayer.GetElement("mmr").Value,
                        (int)foundplayer.GetElement("rank").Value);
                }
                else
                {
                    throw new UserNotRegisteredException();
                }
            }
        }


        /// <summary>
            /// finds a player in the db based on their id
            /// </summary>
            /// <param name="id"></param>
            /// <returns></returns>
            /// <exception cref="UserNotInServerException"></exception>
            /// <exception cref="UserNotRegisteredException"></exception>
        public static BsonDocument FindPlayer(string id)
        {
            Connect();

            if (id == null)
            {
                throw new UserNotInServerException();
            }
            else
            {
                var filter = Builders<BsonDocument>.Filter.Eq("id", id);

                var collection = dbDatabase.GetCollection<BsonDocument>("Player");

                var foundplayer = collection.Find(filter).FirstOrDefault();

                if (foundplayer != null)
                {
                    return foundplayer;
                }
                else
                {
                    throw new UserNotRegisteredException();
                }
            }
        }

        /// <summary>
        /// update players info
        /// </summary>
        /// <param name="player"></param>
        public static void UpdatePlayer(Player player)
        {
            Connect();

            var collection = dbDatabase.GetCollection<BsonDocument>("Player");

            var filter = Builders<BsonDocument>.Filter.Eq("id", player.Account.Id.ToString());

            BsonDocument newPlayer = new BsonDocument { {"id", player.Account.Id.ToString() },
                        {"win", player.TotalWins },
                        {"lose", player.TotalLosses },
                        {"mmr", player.MMR },
                        {"rank", player.Rank}};

            collection.DeleteOne(filter);
            collection.InsertOne(newPlayer);
        }

        /// <summary>
        /// updates the player in the db
        /// </summary>
        /// <param name="player"></param>
        public static void UpdatePlayer(BsonDocument player)
        {
            Connect();

            var collection = dbDatabase.GetCollection<BsonDocument>("Player");

            var filter = Builders<BsonDocument>.Filter.Eq("id", player.GetElement("id").Value.ToString());

            BsonDocument newPlayer = new BsonDocument { {"id", player.GetElement("id").Value.ToString() },
                            {"win", (int)player.GetElement("win").Value},
                            {"lose", (int)player.GetElement("lose").Value },
                            {"mmr", (int)player.GetElement("mmr").Value},
                            {"rank", player.GetElement("rank").Value}};

            collection.DeleteOne(filter);
            collection.InsertOne(newPlayer);
        }

        /// <summary>
        /// update the players info using BsonDocuments
        /// </summary>
        /// <param name="playerDoc"></param>
        /// <param name="mmr"></param>
        /// <param name="win"></param>
        public static void UpdatePlayer(BsonDocument playerDoc, int mmr, bool win)
        {
            Connect();

            var collection = dbDatabase.GetCollection<BsonDocument>("Player");

            var filter = Builders<BsonDocument>.Filter.Eq("id", playerDoc.GetElement("id").Value);

            BsonDocument newPlayer = new BsonDocument();

            if (win)
            {
                newPlayer = new BsonDocument { {"id", playerDoc.GetElement("id").Value.ToString() },
                            {"win", (int)playerDoc.GetElement("win").Value + 1 },
                            {"lose", (int)playerDoc.GetElement("lose").Value },
                            {"mmr", (int)playerDoc.GetElement("mmr").Value + mmr },
                            {"rank", playerDoc.GetElement("rank").Value}};
            }
            else
            {
                newPlayer = new BsonDocument { {"id", playerDoc.GetElement("id").Value.ToString() },
                            {"win", (int)playerDoc.GetElement("win").Value },
                            {"lose", (int)playerDoc.GetElement("lose").Value + 1 },
                            {"mmr", (int)playerDoc.GetElement("mmr").Value - mmr },
                            {"rank", playerDoc.GetElement("rank").Value}};
            }

            collection.DeleteOne(filter);
            collection.InsertOne(newPlayer);
        }

        /// <summary>
        /// creates a brand new 
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public static Player CreatePlayer(CommandContext player, int rank)
        {
            Connect();

            int startingMmr = 0;

            if (rank == 1)
            {
                startingMmr = RankingRanges.Rank1Start;
            }
            else if (rank == 2)
            {
                startingMmr = RankingRanges.Rank2Start;
            }
            else if (rank == 3)
            {
                startingMmr = RankingRanges.Rank3Start;
            }
            else if (rank == 4)
            {
                startingMmr = RankingRanges.Rank4Start;
            }    

            var collection = dbDatabase.GetCollection<BsonDocument>("Player");
            
            BsonDocument newPlayer = new BsonDocument { {"id", player.Member.Id.ToString() },
                        {"win", 0 },
                        {"lose", 0 },
                        {"mmr", startingMmr },
                        {"rank", rank}};
            
            collection.InsertOne(newPlayer);

            return new Player(player.Member, 0, 0, startingMmr, rank);
        }

        /// <summary>
        /// creates a brand new user with member
        /// </summary>
        /// <param name="player"></param>
        /// <param name="rank"></param>
        /// <returns></returns>
        public static Player CreatePlayer(DiscordMember player, int rank)
        {
            Connect();

            int startingMmr = 0;

            if (rank == 1)
            {
                startingMmr = RankingRanges.Rank1Start;
            }
            else if (rank == 2)
            {
                startingMmr = RankingRanges.Rank2Start;
            }
            else if (rank == 3)
            {
                startingMmr = RankingRanges.Rank3Start;
            }
            else if (rank == 4)
            {
                startingMmr = RankingRanges.Rank4Start;
            }

            var collection = dbDatabase.GetCollection<BsonDocument>("Player");

            BsonDocument newPlayer = new BsonDocument { {"id", player.Id.ToString() },
                        {"win", 0 },
                        {"lose", 0 },
                        {"mmr", startingMmr },
                        {"rank", rank}};

            collection.InsertOne(newPlayer);

            return new Player(player, 0, 0, startingMmr, rank);
        }

        /// <summary>
        /// creates a game in the db
        /// </summary>
        /// <param name="players"></param>
        /// <param name="cap1"></param>
        /// <param name="cap2"></param>
        /// <returns></returns>
        public static Game CreateGame(List<Player> players, Player cap1, Player cap2, bool universal = false)
        {
            Connect();

            var collection = dbDatabase.GetCollection<BsonDocument>("Games");

            var games = collection.Find(_ => true).ToList();

            BsonDocument newGame = new BsonDocument { {"id", (games.Count + 1).ToString() },
                        {"players", new BsonArray { players[0].Account.Id.ToString(),
                                                    players[1].Account.Id.ToString(),
                                                    players[2].Account.Id.ToString(),
                                                    players[3].Account.Id.ToString(),
                                                    players[4].Account.Id.ToString(),
                                                    players[5].Account.Id.ToString()} },
                        { "team1", ""},
                        { "team2", ""},
                        { "startTime", DateTime.Now },
                        { "endTime", ""},
                        { "winningTeam", ""},
                        {"universal", universal } };

            collection.InsertOne(newGame);

            Game game = new Game((games.Count + 1).ToString(), players, universal);
            game.StartTime = DateTime.Now;
            game.MoveToTeam1(cap1);
            game.MoveToTeam2(cap2);
            return game;
        }

        /// <summary>
        /// updates the game info witht he finalized info with who won
        /// </summary>
        /// <param name="game"></param>
        /// <param name="player"></param>
        /// <param name="win"></param>
        /// <returns></returns>
        /// <exception cref="GameAlreadyReportedException"></exception>
        public static Game UpdateGame(Game game, Player player, bool win)
        {
            Connect();

            // Because database adds 4 hours
            DateTime now = DateTime.Now.AddHours(4);

            if (FindGame(game).GetElement("winningTeam").Value.ToString() != "")
            {
                throw new GameAlreadyReportedException();
            }
            else if (now.Subtract(FindGame(game).GetElement("startTime").Value.AsDateTime).Hours == 0 && now.Subtract(FindGame(game).GetElement("startTime").Value.AsDateTime).Minutes < 10)
            {
                throw new TooEarlyException();
            }
            else if (game.Universal)
            {
                throw new UniversalMatchException();
            }
            else if (now.Subtract(FindGame(game).GetElement("startTime").Value.AsDateTime).Hours >= 2)
            {
                throw new TooLateException();
            }
            else
            {
                bool inqueue = false;
                foreach (Player currentPlayer in game.Players)
                {
                    if (currentPlayer.Account.Id.ToString() == player.Account.Id.ToString())
                    {
                        inqueue = true;
                    }
                }
                if (game.Team1[0].Account.Id.ToString() == player.Account.Id.ToString() || game.Team2[0].Account.Id.ToString() == player.Account.Id.ToString())
                {
                    inqueue = true;
                }
                if (!inqueue)
                {
                    throw new PlayerNotInLobbyException();
                }
            }

            var filter = Builders<BsonDocument>.Filter.Eq("id", game.ID.ToString());

            var collection = dbDatabase.GetCollection<BsonDocument>("Games");

            collection.DeleteOne(filter);

            if (win)
            {
                foreach (Player currentPlayer in game.Team1)
                {
                    if (currentPlayer.Account.Id == player.Account.Id)
                    {
                        game.WinningTeam = "Team1";
                    }
                }

                if (game.WinningTeam == "")
                {
                    game.WinningTeam = "Team2";
                }
            }
            else
            {
                foreach (Player currentPlayer in game.Team1)
                {
                    if (currentPlayer.Account.Id == player.Account.Id)
                    {
                        game.WinningTeam = "Team2";
                    }
                }

                if (game.WinningTeam == string.Empty)
                {
                    game.WinningTeam = "Team1";
                }
            }

            game.EndTime = DateTime.Now;

            int mmr = UpdatePlayerStats(game.Team1, game.Team2, game.WinningTeam);

            BsonDocument newGame = new BsonDocument { {"id", game.ID },
                        {"players", new BsonArray { game.Players[0].Account.Id.ToString(),
                                                    game.Players[1].Account.Id.ToString(),
                                                    game.Players[2].Account.Id.ToString(),
                                                    game.Players[3].Account.Id.ToString(),
                                                    game.Team1[0].Account.Id.ToString(),
                                                    game.Team2[0].Account.Id.ToString()} },
                        { "team1", new BsonArray { game.Team1[0].Account.Id.ToString(),
                                                   game.Team1[1].Account.Id.ToString(),
                                                   game.Team1[2].Account.Id.ToString()} },
                        { "team2", new BsonArray { game.Team2[0].Account.Id.ToString(),
                                                   game.Team2[1].Account.Id.ToString(),
                                                   game.Team2[2].Account.Id.ToString()}},
                        { "startTime", game.StartTime },
                        { "endTime", game.EndTime},
                        { "winningTeam", game.WinningTeam},
                        {"universal", game.Universal},
                        {"mmr+-", mmr } };

            collection.InsertOne(newGame);

            return game;
        }

        /// <summary>
        /// updates the game based off the id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="player"></param>
        /// <param name="win"></param>
        /// <exception cref="GameAlreadyReportedException"></exception>
        public static void UpdateGame(string id, Player player, bool win)
        {
            Connect();

            DateTime now = DateTime.Now.AddHours(4);

            if (FindGame(id).GetElement("winningTeam").Value.ToString() != "")
            {
                throw new GameAlreadyReportedException();
            }
            else if (now.Subtract(FindGame(id).GetElement("startTime").Value.AsDateTime).Hours == 0 && now.Subtract(FindGame(id).GetElement("startTime").Value.AsDateTime).Minutes < 10)
            {
                throw new TooEarlyException();
            }
            else if (FindGame(id).GetElement("team1").Value == true)
            {
                throw new UniversalMatchException();
            }
            else if (now.Subtract(FindGame(id).GetElement("startTime").Value.AsDateTime).Hours >= 2)
            {
                throw new TooLateException();
            }
            else
            {
                bool inqueue = false;
                foreach (BsonValue doc in FindGame(id).GetValue("team1").AsBsonArray.Values)
                {
                    if (doc == player.Account.Id.ToString())
                    {
                        inqueue = true;
                    }
                }
                foreach (BsonValue doc in FindGame(id).GetValue("team2").AsBsonArray.Values)
                {
                    if (doc == player.Account.Id.ToString())
                    {
                        inqueue = true;
                    }
                }
                if (!inqueue)
                {
                    throw new PlayerNotInLobbyException();
                }
            }

            var filter = Builders<BsonDocument>.Filter.Eq("id", id);

            var collection = dbDatabase.GetCollection<BsonDocument>("Games");

            var foundGame = collection.Find(filter).FirstOrDefault();

            string winningTeam = string.Empty;

            if (win)
            {
                for (int i = 0; i < foundGame.GetValue("team1").AsBsonArray.Count; i++)
                {
                    if (foundGame.GetValue("team1").AsBsonArray[i].ToString() == player.Account.Id.ToString() )
                    {
                        winningTeam = "Team1";
                    }
                }
                if (winningTeam == string.Empty)
                {
                    winningTeam = "Team2";
                }
            }
            else
            {
                for (int i = 0; i < foundGame.GetValue("team1").AsBsonArray.Count; i++)
                {
                    if (foundGame.GetValue("team1").AsBsonArray[i].ToString() == player.Account.Id.ToString())
                    {
                        winningTeam = "Team2";
                    }
                }
                if (winningTeam == string.Empty)
                {
                    winningTeam = "Team1";
                }
            }

            List<string> team1 = new List<string>();
            List<string> team2 = new List<string>();

            foreach (BsonValue value in foundGame.GetValue("team1").AsBsonArray.Values)
            {
                team1.Add(value.ToString());
            }
            foreach (BsonValue value in foundGame.GetValue("team2").AsBsonArray.Values)
            {
                team2.Add(value.ToString());
            }

            int mmr = UpdatePlayerStats(team1, team2, winningTeam);

            BsonDocument newGame = new BsonDocument { {"id", foundGame.GetElement("id").Value.ToString() },
                        {"players", foundGame.GetValue("players").AsBsonArray},
                        { "team1", foundGame.GetValue("team1").AsBsonArray},
                        { "team2", foundGame.GetValue("team2").AsBsonArray},
                        { "startTime", foundGame.GetElement("startTime").Value.AsDateTime },
                        { "endTime", DateTime.Now},
                        { "winningTeam", winningTeam},
                        {"universal", foundGame.GetElement("universal").Value.AsBoolean },
                        {"mmr+-", mmr } };

            collection.DeleteOne(filter);
            collection.InsertOne(newGame);
        }

        /// <summary>
        /// updates a game base don the BsonDocument
        /// </summary>
        /// <param name="game"></param>
        public static void UpdateGame(BsonDocument game)
        {
            Connect();

            var collection = dbDatabase.GetCollection<BsonDocument>("Games");

            var filter = Builders<BsonDocument>.Filter.Eq("id", game.GetElement("id").Value.ToString());

            collection.DeleteOne(filter);
            collection.InsertOne(game);
        }

        /// <summary>
        /// updates the game with the current values of a game object
        /// </summary>
        /// <param name="game"></param>
        /// <returns></returns>
        public static Game UpdateGame(Game game)
        {
            Connect();

            var filter = Builders<BsonDocument>.Filter.Eq("id", game.ID.ToString());

            var collection = dbDatabase.GetCollection<BsonDocument>("Games");

            collection.DeleteOne(filter);

            BsonDocument newGame = new BsonDocument { {"id", game.ID },
                        {"players", new BsonArray { game.Players[0].Account.Id.ToString(),
                                                    game.Players[1].Account.Id.ToString(),
                                                    game.Players[2].Account.Id.ToString(),
                                                    game.Players[3].Account.Id.ToString(),
                                                    game.Team1[0].Account.Id.ToString(),
                                                    game.Team2[0].Account.Id.ToString()} },
                        { "team1", new BsonArray { game.Team1[0].Account.Id.ToString(),
                                                   game.Team1[1].Account.Id.ToString(),
                                                   game.Team1[2].Account.Id.ToString()} },
                        { "team2", new BsonArray { game.Team2[0].Account.Id.ToString(),
                                                   game.Team2[1].Account.Id.ToString(),
                                                   game.Team2[2].Account.Id.ToString()}},
                        { "startTime", game.StartTime },
                        { "endTime", game.EndTime},
                        { "winningTeam", game.WinningTeam},
                        { "universal", game.Universal } };

            collection.InsertOne(newGame);

            return game;
        }

        /// <summary>
        /// stores a value in the database
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public static void SetStoredValue(string name, string value)
        {
            Connect();

            var collection = dbDatabase.GetCollection<BsonDocument>("Values");

            BsonDocument newPlayer = new BsonDocument { {"name", name },
                        {"value", value }};

            collection.InsertOne(newPlayer);
        }


        /// <summary>
        /// gets a value fromt he datase base don the name given
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string? GetStoredValue(string name)
        {
            Connect();

            var filter = Builders<BsonDocument>.Filter.Eq("name", name);

            var collection = dbDatabase.GetCollection<BsonDocument>("Values");

            var value = collection.Find(filter).FirstOrDefault();

            if (value != null)
            {
                return value.GetElement("value").ToString();
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// gets the leaderboeard and returns an embed message
        /// </summary>
        /// <returns></returns>
        public async static void GetLeaderboard(CommandContext ctx)
        {
            Connect();

            var collection = dbDatabase.GetCollection<BsonDocument>("Player");

            var players = collection.Find(_ => true).ToList();

            List<BsonDocument> RankS = new List<BsonDocument>();
            List<BsonDocument> RankA = new List<BsonDocument>();
            List<BsonDocument> RankB = new List<BsonDocument>();
            List<BsonDocument> RankC = new List<BsonDocument>();

            for (int ranking = 1; ranking < 5; ranking++)
            {
                for (int i = 0; i < players.Count; i++)
                {
                    if (ranking == 1)
                    {
                        if (Int32.Parse(players[i].GetElement("rank").Value.ToString()) == ranking)
                        {
                            RankS.Add(players[i]);
                        }
                    }
                    else if (ranking == 2)
                    {
                        if (Int32.Parse(players[i].GetElement("rank").Value.ToString()) == ranking)
                        {
                            RankA.Add(players[i]);
                        }
                    }
                    else if (ranking == 3)
                    {
                        if (Int32.Parse(players[i].GetElement("rank").Value.ToString()) == ranking)
                        {
                            RankB.Add(players[i]);
                        }
                    }
                    else if (ranking == 4)
                    {
                        if (Int32.Parse(players[i].GetElement("rank").Value.ToString()) == ranking)
                        {
                            RankC.Add(players[i]);
                        }
                    }
                }
            }
            RankS = OrderList(RankS, ctx);
            RankA = OrderList(RankA, ctx);
            RankB = OrderList(RankB, ctx);
            RankC = OrderList(RankC, ctx);


            DiscordEmbedBuilder embed = new DiscordEmbedBuilder
            {
                Color = DiscordColor.Turquoise,
                Title = "Top 25 Leaderboards!",
            };

            string description = "**Top 25 Rank S Players:**\n";
            int rank = 1;

            //var members = ctx.Guild.GetAllMembersAsync().Result;

            foreach (BsonDocument player in RankS)
            {
                try
                {
                    DiscordMember member = ctx.Guild.GetMemberAsync(ulong.Parse(player.GetElement("id").Value.ToString())).Result;
                    description += rank + ". " + member.Mention + " MMR: " + player.GetElement("mmr").Value + "\n";
                    rank++;
                }
                catch
                {
                    description += rank + ". " + player.GetElement("id").Value.ToString() + " MMR: " + player.GetElement("mmr").Value + "\n";
                    rank++;
                }
            }

            description += "\n**Top 25 Rank A Players:**\n";
            rank = 1;

            foreach (BsonDocument player in RankA)
            {
                try
                {
                    DiscordMember member = ctx.Guild.GetMemberAsync(ulong.Parse(player.GetElement("id").Value.ToString())).Result;
                    description += rank + ". " + member.Mention + " MMR: " + player.GetElement("mmr").Value + "\n";
                    rank++;
                }
                catch
                {
                    description += rank + ". " + player.GetElement("id").Value.ToString() + " MMR: " + player.GetElement("mmr").Value + "\n";
                    rank++;
                }
            }

            description += "\n**Top 25 Rank B Players:**\n";
            rank = 1;

            foreach (BsonDocument player in RankB)
            {
                try
                {
                    DiscordMember member = ctx.Guild.GetMemberAsync(ulong.Parse(player.GetElement("id").Value.ToString())).Result;
                    description += rank + ". " + member.Mention + " MMR: " + player.GetElement("mmr").Value + "\n";
                    rank++;
                }
                catch
                {
                    description += rank + ". " + player.GetElement("id").Value.ToString() + " MMR: " + player.GetElement("mmr").Value + "\n";
                    rank++;
                }
            }

            description += "\n**Top 25 Rank C Players:**\n";
            rank = 1;

            foreach (BsonDocument player in RankC)
            {
                try
                {
                    DiscordMember member = ctx.Guild.GetMemberAsync(ulong.Parse(player.GetElement("id").Value.ToString())).Result;
                    description += rank + ". " + member.Mention + " MMR: " + player.GetElement("mmr").Value + "\n";
                    rank++;
                }
                catch
                {
                    description += rank + ". " + player.GetElement("id").Value.ToString() + " MMR: " + player.GetElement("mmr").Value + "\n";
                    rank++;
                }
            }

            embed.Description = "\n\n" + description + "Last updated: " + DateTime.Now + " EST";

            RankingRanges.LeaderboardEmbed = embed;
        }



        /// <summary>
        /// finds a game based on the game given
        /// </summary>
        /// <param name="game"></param>
        /// <returns></returns>
        /// <exception cref="GameNotFoundException"></exception>
        public static BsonDocument FindGame(Game game)
        {
            Connect();

            var filter = Builders<BsonDocument>.Filter.Eq("id", game.ID);

            var collection = dbDatabase.GetCollection<BsonDocument>("Games");

            var foundGame = collection.Find(filter).FirstOrDefault();

            if (foundGame != null)
            {
                return foundGame;
            }
            else
            {
                throw new GameNotFoundException();
            }
        }

        /// <summary>
        /// finds a game based on the string
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="GameNotFoundException"></exception>
        public static BsonDocument FindGame(string id)
        {
            Connect();

            var filter = Builders<BsonDocument>.Filter.Eq("id", id);

            var collection = dbDatabase.GetCollection<BsonDocument>("Games");

            var foundGame = collection.Find(filter).FirstOrDefault();

            if (foundGame != null)
            {
                return foundGame;
            }
            else
            {
                throw new GameNotFoundException();
            }
        }


        /// <summary>
        /// clears all the games
        /// </summary>
        public static void ClearGameCache(string Collection)
        {
            Connect();

            var collection = dbDatabase.GetCollection<BsonDocument>(Collection);

            collection.DeleteMany(_ => true);
        }

        /// <summary>
        /// updatse all the players stats
        /// </summary>
        /// <param name="team1"></param>
        /// <param name="team2"></param>
        /// <param name="winningTeam"></param>
        public static int UpdatePlayerStats(List<Player> team1, List<Player> team2, string winningTeam)
        {
            double team1MMR = 0;
            double team2MMR = 0;


            foreach (Player player in team1)
            {
                team1MMR += player.MMR;
                if (winningTeam == "Team1")
                {
                    player.TotalWins += 1;
                }
                else
                {
                    player.TotalLosses += 1;
                }
            }

            foreach (Player player in team2)
            {
                team2MMR += player.MMR;
                if (winningTeam == "Team2")
                {
                    player.TotalWins += 1;
                }
                else
                {
                    player.TotalLosses += 1;
                }
            }

            int mmr = Int32.Parse(Math.Round(GetMMRGain(team1MMR, team2MMR, winningTeam)).ToString());

            if (winningTeam == "Team1")
            {
                foreach (Player player in team1)
                {
                    player.MMR += mmr;
                    MyMongoDB.UpdatePlayer(player);
                }
                foreach (Player player in team2)
                {
                    player.MMR -= mmr;
                    MyMongoDB.UpdatePlayer(player);
                }


            }
            else
            {
                foreach (Player player in team1)
                {
                    player.MMR -= mmr;
                    MyMongoDB.UpdatePlayer(player);
                }
                foreach (Player player in team2)
                {
                    player.MMR += mmr;
                    MyMongoDB.UpdatePlayer(player);
                }
            }

            return mmr;
        }

        /// <summary>
        /// updates all the players stats
        /// </summary>
        /// <param name="team1"></param>
        /// <param name="team2"></param>
        /// <param name="winningTeam"></param>
        public static int UpdatePlayerStats(List<string> team1, List<string> team2, string winningTeam)
        {
            double team1MMR = 0;
            double team2MMR = 0;

            List<BsonDocument> team1Players = new List<BsonDocument>();
            List<BsonDocument> team2Players = new List<BsonDocument>();

            foreach (string playerID in team1)
            {
                BsonDocument doc = FindPlayer(playerID);
                team1MMR += (int)doc.GetElement("mmr").Value;
                team1Players.Add(doc);
            }

            foreach (string playerID in team2)
            {
                BsonDocument doc = FindPlayer(playerID);
                team2MMR += (int)doc.GetElement("mmr").Value;
                team2Players.Add(doc);
            }

            int mmr = Int32.Parse(Math.Round(GetMMRGain(team1MMR, team2MMR, winningTeam)).ToString());

            if (winningTeam == "Team1")
            {
                foreach (string player in team1)
                {
                    MyMongoDB.UpdatePlayer(FindPlayer(player), mmr, true);
                }
                foreach (string player in team2)
                {
                    MyMongoDB.UpdatePlayer(FindPlayer(player), mmr, false);
                }
            }
            else
            {
                foreach (string player in team1)
                {
                    MyMongoDB.UpdatePlayer(FindPlayer(player), mmr, false);
                }
                foreach (string player in team2)
                {
                    MyMongoDB.UpdatePlayer(FindPlayer(player), mmr, true);
                }
            }

            return mmr;
        }

        /// <summary>
        /// gets the +- difference for each teams mmr
        /// </summary>
        /// <param name="team1MMR"></param>
        /// <param name="team2MMR"></param>
        /// <param name="winningTeam"></param>
        /// <returns></returns>
        private static double GetMMRGain(double team1MMR, double team2MMR, string winningTeam)
        {
            if (winningTeam == "Team 1")
            {
                //

                int percent = Convert.ToInt32(Math.Round((double)team1MMR / ((double)team1MMR + (double)team2MMR) * 100, MidpointRounding.AwayFromZero));

                percent -= 50;

                return RankingRanges.BaseMMR - percent;
            }
            else
            {
                int percent = Convert.ToInt32(Math.Round((double)team1MMR / ((double)team1MMR + (double)team2MMR) * 100, MidpointRounding.AwayFromZero));

                percent -= 50;

                percent = Convert.ToInt32(Math.Round((double)(percent / 5), MidpointRounding.AwayFromZero));

                return RankingRanges.BaseMMR - percent;
            }
        }



        /// <summary>
        /// orders the list by mmr
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private static List<BsonDocument> OrderList(List<BsonDocument> list, CommandContext ctx)
        {
            bool changes = true;
            while (changes)
            {
                changes = false;
                for (int i = 0; i < list.Count - 1; i++)
                {
                    try
                    {
                        DiscordMember member = ctx.Guild.GetMemberAsync(ulong.Parse(list[i].GetElement("id").Value.ToString())).Result;
                        if (Int32.Parse(list[i + 1].GetElement("mmr").Value.ToString()) > Int32.Parse(list[i].GetElement("mmr").Value.ToString()))
                        {
                            BsonDocument holder = list[i + 1];
                            list[i + 1] = list[i];
                            list[i] = holder;
                            changes = true;
                        }
                    }
                    catch
                    {
                        list.Remove(list[i]);
                        changes = true;
                    }
                }
            }

            int count = list.Count;

            for (int i = 25; i < count; i++)
            {
                list.Remove(list.Last());
            }
            return list;
        }

        /// <summary>
        /// returns the rank the player should be in
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        private static Player UpdateRank(Player player)
        {
            if (player.Rank == 2)
            {
                if (player.MMR < RankingRanges.Rank2Demo)
                {
                    player.Rank = 3;
                    CustomCommands.RevokeRole(player, storedCTX, RankingRanges.Rank2Role);
                    Thread.Sleep(1000);
                    CustomCommands.GrantRole(player, storedCTX, RankingRanges.Rank3Role);
                }
            }
            else if (player.Rank == 3)
            {
                if (player.MMR > RankingRanges.Rank3Promo)
                {
                    player.Rank = 2;
                    CustomCommands.RevokeRole(player, storedCTX, RankingRanges.Rank3Role);
                    Thread.Sleep(1000);
                    CustomCommands.GrantRole(player, storedCTX, RankingRanges.Rank2Role);
                }
                else if (player.MMR < RankingRanges.Rank3Demo)
                {
                    player.Rank = 4;
                    CustomCommands.RevokeRole(player, storedCTX, RankingRanges.Rank3Role);
                    Thread.Sleep(1000);
                    CustomCommands.GrantRole(player, storedCTX, RankingRanges.Rank4Role);
                }
            }
            else if (player.Rank == 4)
            {
                if (player.MMR > RankingRanges.Rank4Promo)
                {
                    player.Rank = 3;
                    CustomCommands.RevokeRole(player, storedCTX, RankingRanges.Rank4Role);
                    Thread.Sleep(1000);
                    CustomCommands.GrantRole(player, storedCTX, RankingRanges.Rank3Role);
                }
            }
            return player;
        }

        /// <summary>
        /// updates the rnak of the player
        /// </summary>
        /// <param name="playerDoc"></param>
        /// <returns></returns>
        public static void UpdateRanks()
        {
            Connect();

            var collection = dbDatabase.GetCollection<BsonDocument>("Player");


            var players = collection.Find(_ => true).ToList();

            foreach(BsonDocument playerDoc in players)
            {
                if (Int32.Parse(playerDoc.GetElement("rank").Value.ToString()) == 2)
                {
                    if (Int32.Parse(playerDoc.GetElement("mmr").Value.ToString()) < RankingRanges.Rank2Demo)
                    {
                        DiscordMember member = MyMongoDB.storedCTX.Guild.GetMemberAsync(ulong.Parse(playerDoc.GetElement("id").Value.ToString())).Result;

                        CustomCommands.RevokeRole(member, RankingRanges.Rank2Role);
                        Thread.Sleep(1000);
                        CustomCommands.GrantRole(member, RankingRanges.Rank3Role);
                        Thread.Sleep(1000);

                        UpdatePlayer(new BsonDocument { { "id",  playerDoc.GetElement("id").Value.ToString() },
                                                        { "win", (int)playerDoc.GetElement("win").Value },
                                                        { "lose", (int)playerDoc.GetElement("lose").Value },
                                                        { "mmr", (int)playerDoc.GetElement("mmr").Value },
                                                        { "rank", 3} });

                        member.SendMessageAsync(new DiscordEmbedBuilder { Title = "Notice!", Color = DiscordColor.Black, Description = "You have been demoted to Rank B" });
                    }
                }
                else if (Int32.Parse(playerDoc.GetElement("rank").Value.ToString()) == 3)
                {
                    if (Int32.Parse(playerDoc.GetElement("mmr").Value.ToString()) > RankingRanges.Rank3Promo)
                    {
                        DiscordMember member = MyMongoDB.storedCTX.Guild.GetMemberAsync(ulong.Parse(playerDoc.GetElement("id").Value.ToString())).Result;

                        CustomCommands.RevokeRole(member, RankingRanges.Rank3Role);
                        Thread.Sleep(1000);
                        CustomCommands.GrantRole(member, RankingRanges.Rank2Role);
                        Thread.Sleep(1000);

                        UpdatePlayer(new BsonDocument { { "id",  playerDoc.GetElement("id").Value.ToString() },
                                                        { "win", (int)playerDoc.GetElement("win").Value },
                                                        { "lose", (int)playerDoc.GetElement("lose").Value },
                                                        { "mmr", (int)playerDoc.GetElement("mmr").Value },
                                                        { "rank", 2} });

                        member.SendMessageAsync(new DiscordEmbedBuilder { Title = "Congratulations!", Color = DiscordColor.Gold, Description = "You have been promoted to Rank A" });
                    }
                    else if (Int32.Parse(playerDoc.GetElement("mmr").Value.ToString()) < RankingRanges.Rank3Demo)
                    {
                        DiscordMember member = MyMongoDB.storedCTX.Guild.GetMemberAsync(ulong.Parse(playerDoc.GetElement("id").Value.ToString())).Result;

                        CustomCommands.RevokeRole(member, RankingRanges.Rank3Role);
                        Thread.Sleep(1000);
                        CustomCommands.GrantRole(member, RankingRanges.Rank4Role);
                        Thread.Sleep(1000);

                        UpdatePlayer(new BsonDocument { { "id",  playerDoc.GetElement("id").Value.ToString() },
                                                        { "win", (int)playerDoc.GetElement("win").Value },
                                                        { "lose", (int)playerDoc.GetElement("lose").Value },
                                                        { "mmr", (int)playerDoc.GetElement("mmr").Value },
                                                        { "rank", 4} });

                        member.SendMessageAsync(new DiscordEmbedBuilder { Title = "Notice!", Color = DiscordColor.Black, Description = "You have been demoted to Rank C" });
                    }
                }
                else if (Int32.Parse(playerDoc.GetElement("rank").Value.ToString()) == 4)
                {
                    if (Int32.Parse(playerDoc.GetElement("mmr").Value.ToString()) > RankingRanges.Rank4Promo)
                    {
                        DiscordMember member = MyMongoDB.storedCTX.Guild.GetMemberAsync(ulong.Parse(playerDoc.GetElement("id").Value.ToString())).Result;

                        CustomCommands.RevokeRole(member, RankingRanges.Rank4Role);
                        Thread.Sleep(1000);
                        CustomCommands.GrantRole(member, RankingRanges.Rank3Role);
                        Thread.Sleep(1000);

                        UpdatePlayer(new BsonDocument { { "id",  playerDoc.GetElement("id").Value.ToString() },
                                                        { "win", (int)playerDoc.GetElement("win").Value },
                                                        { "lose", (int)playerDoc.GetElement("lose").Value },
                                                        { "mmr", (int)playerDoc.GetElement("mmr").Value },
                                                        { "rank", 3} });

                        member.SendMessageAsync(new DiscordEmbedBuilder { Title = "Congratulations!", Color = DiscordColor.Gold, Description = "You have been promoted to Rank B" });
                    }
                }
            }
        }


        /// <summary>
        /// saves the voice channels ids in the db
        /// </summary>
        /// <param name="id"></param>
        /// <param name="team1ChannelID"></param>
        /// <param name="team2ChannelID"></param>
        public static void SaveVoiceChannels(string id, ulong team1ChannelID, ulong team2ChannelID)
        {
            Connect();

            var collection = dbDatabase.GetCollection<BsonDocument>("VoiceChannels");

            BsonDocument newSave = new BsonDocument { {"gameID", id.ToString() },
                        {"team1VC", team1ChannelID.ToString() },
                        {"team2VC", team2ChannelID.ToString() },
                        {"creationTime", DateTime.Now } };

            collection.InsertOne(newSave);
        }

        /// <summary>
        /// returns the channel ids of the voice channels
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static BsonDocument GetVoiceChannels(string id)
        {
            Connect();

            var filter = Builders<BsonDocument>.Filter.Eq("gameID", id);

            var collection = dbDatabase.GetCollection<BsonDocument>("VoiceChannels");

            return collection.Find(filter).FirstOrDefault();
        }

        /// <summary>
        /// gets the voice channels
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static BsonDocument? GetVoiceChannels(ulong id)
        {
            Connect();

            var filter = Builders<BsonDocument>.Filter.Eq("team1VC", id.ToString());

            var collection = dbDatabase.GetCollection<BsonDocument>("VoiceChannels");

            var doc = collection.Find(filter).FirstOrDefault();

            if (doc == null)
            {
                filter = Builders<BsonDocument>.Filter.Eq("team2VC", id.ToString());

                collection = dbDatabase.GetCollection<BsonDocument>("VoiceChannels");

                doc = collection.Find(filter).FirstOrDefault();

                if (doc == null)
                {
                    return null;
                }
            }
            return doc;
        }

        /// <summary>
        /// saves registry into db
        /// </summary>
        /// <param name="registry"></param>
        public static void SaveRankCheck(RankCheckRegistry registry)
        {
            Connect();

            var collection = dbDatabase.GetCollection<BsonDocument>("RankCheckedUser");

            BsonDocument newSave = new BsonDocument { {"PlayerID", registry.LinkedDiscordUser.ToString() },
                        {"Omega6Name", registry.OmegaStrikerName },
                        {"StartingRank", registry.StartingRank } };

            collection.InsertOne(newSave);
        }

        /// <summary>
        /// gets the registry of an omega6 account
        /// </summary>
        /// <param name="Omega6Name"></param>
        /// <returns></returns>
        public static RankCheckRegistry? GetRankCheck(string Omega6Name)
        {
            Connect();

            var filter = Builders<BsonDocument>.Filter.Eq("Omega6Name", Omega6Name);

            var collection = dbDatabase.GetCollection<BsonDocument>("RankCheckedUser");

            var registry = collection.Find(filter).FirstOrDefault();

            if (registry == null)
            {
                return null;
            }
            else
            {
                return new RankCheckRegistry(ulong.Parse(registry.GetElement("PlayerID").Value.ToString()), registry.GetElement("Omega6Name").Value.ToString(), registry.GetElement("StartingRank").Value.AsInt32);
            }
        }

        /// <summary>
        /// gets the registry of an omega6 account
        /// </summary>
        /// <param name="Omega6Name"></param>
        /// <returns></returns>
        public static RankCheckRegistry? GetRankCheck(ulong PlayerID)
        {
            Connect();

            var filter = Builders<BsonDocument>.Filter.Eq("PlayerID", PlayerID.ToString());

            var collection = dbDatabase.GetCollection<BsonDocument>("RankCheckedUser");

            var registry = collection.Find(filter).FirstOrDefault();

            if (registry == null)
            {
                return null;
            }
            else
            {
                return new RankCheckRegistry(ulong.Parse(registry.GetElement("PlayerID").Value.ToString()), registry.GetElement("Omega6Name").Value.ToString(), registry.GetElement("StartingRank").Value.AsInt32);
            }
        }

        /// <summary>
        /// updates a rank checked users info
        /// </summary>
        /// <param name="rankcheck"></param>
        public static void UpdateRankCheck(BsonDocument rankcheck)
        {
            Connect();

            var collection = dbDatabase.GetCollection<BsonDocument>("RankCheckedUser");

            var filter = Builders<BsonDocument>.Filter.Eq("PlayerID", rankcheck.GetElement("PlayerID").Value.ToString());

            collection.DeleteOne(filter);
            collection.InsertOne(rankcheck);
        }

        /// <summary>
        /// reverses the outcome of the game and adjusts mmr accordingly
        /// </summary>
        /// <param name="lobbyID"></param>
        public static void ReverseOutcome(string lobbyID)
        {
            Connect();

            BsonDocument doc = FindGame(lobbyID);

            int mmr = doc.GetElement("mmr+-").Value.AsInt32;

            int newMMR = RankingRanges.BaseMMR + (RankingRanges.BaseMMR - mmr);

            string winningTeam = doc.GetElement("winningTeam").Value.ToString();

            foreach (BsonValue member in doc.GetValue("team1").AsBsonArray.Values)
            {
                BsonDocument player =  FindPlayer(member.ToString());

                if (winningTeam == "Team1")
                {
                    UpdatePlayer(new BsonDocument { {"id", player.GetElement("id").Value.ToString() },
                            {"win", ((int)player.GetElement("win").Value.AsInt32 - 1)},
                            {"lose", ((int)player.GetElement("lose").Value.AsInt32 + 1) },
                            {"mmr", ((int)player.GetElement("mmr").Value.AsInt32 - mmr - newMMR)},
                            {"rank", player.GetElement("rank").Value}});
                }
                else
                {
                    UpdatePlayer(new BsonDocument { {"id", player.GetElement("id").Value.ToString() },
                            {"win", ((int)player.GetElement("win").Value.AsInt32 + 1)},
                            {"lose", ((int) player.GetElement("lose").Value.AsInt32 - 1) },
                            {"mmr", ((int)player.GetElement("mmr").Value.AsInt32 + mmr + newMMR)},
                            {"rank", player.GetElement("rank").Value}});
                }
            }
            foreach (BsonValue member in doc.GetValue("team2").AsBsonArray.Values)
            {
                BsonDocument player = FindPlayer(member.ToString());

                if (winningTeam == "Team2")
                {
                    UpdatePlayer(new BsonDocument { {"id", player.GetElement("id").Value.ToString() },
                            {"win", ((int) player.GetElement("win").Value.AsInt32 - 1)},
                            {"lose", ((int) player.GetElement("lose").Value.AsInt32 + 1) },
                            {"mmr", ((int) player.GetElement("mmr").Value.AsInt32 - mmr - newMMR)},
                            {"rank", player.GetElement("rank").Value}});
                }
                else
                {
                    UpdatePlayer(new BsonDocument { {"id", player.GetElement("id").Value.ToString() },
                            {"win", ((int) player.GetElement("win").Value.AsInt32 + 1)},
                            {"lose", ((int) player.GetElement("lose").Value.AsInt32 - 1) },
                            {"mmr", ((int) player.GetElement("mmr").Value.AsInt32 + mmr + newMMR)},
                            {"rank", player.GetElement("rank").Value}});
                }
            }

            if (winningTeam == "Team1")
            {
                winningTeam = "Team2";
            }
            else
            {
                winningTeam = "Team1";
            }

            UpdateGame(new BsonDocument { {"id", doc.GetElement("id").Value.ToString() },
                        {"players", doc.GetValue("players").AsBsonArray},
                        { "team1", doc.GetValue("team1").AsBsonArray},
                        { "team2", doc.GetValue("team2").AsBsonArray},
                        { "startTime", doc.GetElement("startTime").Value.AsDateTime },
                        { "endTime", doc.GetElement("endTime").Value.AsDateTime},
                        { "winningTeam", winningTeam},
                        {"universal", doc.GetElement("universal").Value.AsBoolean },
                        {"mmr+-", newMMR } });
        }

        /// <summary>
        /// cancels the match
        /// </summary>
        /// <param name="lobbyID"></param>
        public static void CancelMatch(string lobbyID)
        {
            Connect();

            var collection = dbDatabase.GetCollection<BsonDocument>("Games");

            var filter = Builders<BsonDocument>.Filter.Eq("id", lobbyID);

            var games = collection.Find(filter).ToList();

            try
            {
                int mmr = games[0].GetElement("mmr+-").Value.AsInt32;

                if (games[0].GetElement("winningTeam").Value.ToString() == "Team1")
                {
                    foreach(var player in games[0].GetElement("team1").Value.AsBsonArray)
                    {
                        BsonDocument playerDoc = FindPlayer(player.ToString());

                        UpdatePlayer(new BsonDocument { { "id", playerDoc.GetElement("id").Value.ToString()},
                                                        { "win", (int)playerDoc.GetElement("win").Value - 1},
                                                        { "lose", (int)playerDoc.GetElement("lose").Value},
                                                        { "mmr", (int)playerDoc.GetElement("mmr").Value - games[0].GetElement("mmr+-").Value.AsInt32},
                                                        { "rank",(int)playerDoc.GetElement("rank").Value } });
                    }
                    foreach (var player in games[0].GetElement("team2").Value.AsBsonArray)
                    {
                        BsonDocument playerDoc = FindPlayer(player.ToString());

                        UpdatePlayer(new BsonDocument { { "id", playerDoc.GetElement("id").Value.ToString()},
                                                        { "win", (int)playerDoc.GetElement("win").Value},
                                                        { "lose", (int)playerDoc.GetElement("lose").Value - 1},
                                                        { "mmr", (int)playerDoc.GetElement("mmr").Value + games[0].GetElement("mmr+-").Value.AsInt32},
                                                        { "rank",(int)playerDoc.GetElement("rank").Value }});
                    }
                }
                else
                {
                    foreach (var player in games[0].GetElement("team2").Value.AsBsonArray)
                    {
                        BsonDocument playerDoc = FindPlayer(player.ToString());

                        UpdatePlayer(new BsonDocument { { "id", playerDoc.GetElement("id").Value.ToString()},
                                                        { "win", (int)playerDoc.GetElement("win").Value - 1},
                                                        { "lose", (int)playerDoc.GetElement("lose").Value},
                                                        { "mmr", (int)playerDoc.GetElement("mmr").Value - games[0].GetElement("mmr+-").Value.AsInt32},
                                                        { "rank",(int)playerDoc.GetElement("rank").Value }});
                    }
                    foreach (var player in games[0].GetElement("team1").Value.AsBsonArray)
                    {
                        BsonDocument playerDoc = FindPlayer(player.ToString());

                        UpdatePlayer(new BsonDocument { { "id", playerDoc.GetElement("id").Value.ToString()},
                                                        { "win", (int)playerDoc.GetElement("win").Value},
                                                        { "lose", (int)playerDoc.GetElement("lose").Value - 1},
                                                        { "mmr", (int)playerDoc.GetElement("mmr").Value + games[0].GetElement("mmr+-").Value.AsInt32},
                                                        { "rank",(int)playerDoc.GetElement("rank").Value }});
                    }
                }
            }
            catch { }
            BsonDocument newGame = new BsonDocument { {"id", lobbyID },
                        {"players", games[0].GetElement("players").Value.AsBsonArray },
                        { "team1", ""},
                        { "team2", ""},
                        { "startTime", games[0].GetElement("startTime").Value.AsDateTime },
                        { "endTime", ""},
                        { "winningTeam", ""},
                        {"universal",  games[0].GetElement("universal").Value } };

            collection.DeleteOne(filter);
            collection.InsertOne(newGame);
        }

        /// <summary>
        /// adds time to the database to allow users to report the scores of a game
        /// </summary>
        /// <param name="lobbyID"></param>
        public static void AddTime(string lobbyID)
        {
            Connect();

            var collection = dbDatabase.GetCollection<BsonDocument>("Games");

            var filter = Builders<BsonDocument>.Filter.Eq("id", lobbyID);

            var games = collection.Find(filter).ToList();

            BsonDocument newGame = new BsonDocument { {"id", lobbyID },
                        {"players", games[0].GetElement("players").Value.AsBsonArray },
                        { "team1", games[0].GetElement("team1").Value.AsBsonArray },
                        { "team2", games[0].GetElement("team2").Value.AsBsonArray },
                        { "startTime", DateTime.Now },
                        { "endTime", games[0].GetElement("endTime").Value.AsDateTime},
                        { "winningTeam", games[0].GetElement("winningTeam").Value.ToString()},
                        {"universal",  games[0].GetElement("universal").Value } };

            collection.DeleteOne(filter);
            collection.InsertOne(newGame);
        }

        /// <summary>
        /// removes player from player table and rankcheck table
        /// </summary>
        /// <param name="playerID"></param>
        public static void UnregisterPlayer(ulong playerID) 
        {
            Connect();

            var filter = Builders<BsonDocument>.Filter.Eq("id", playerID.ToString());

            var collection = dbDatabase.GetCollection<BsonDocument>("Player");

            collection.DeleteOne(filter);

            filter = Builders<BsonDocument>.Filter.Eq("PlayerID", playerID.ToString());

            collection = dbDatabase.GetCollection<BsonDocument>("RankCheckedUser");

            collection.DeleteOne(filter);
        }

        /// <summary>
        /// resets all mmrs to default
        /// </summary>
        public static void ResetMMR()
        {
            Connect();

            var collection = dbDatabase.GetCollection<BsonDocument>("Player");


            var players = collection.Find(_ => true).ToList();

            foreach (var player in players)
            {
                var filter = Builders<BsonDocument>.Filter.Eq("id", player.GetElement("id").Value.ToString());

                if (player.GetElement("rank").Value.AsInt32 == 1)
                {
                    collection.UpdateOne(filter, new BsonDocument { { "id", player.GetElement("id").Value.ToString() },
                                                                    { "win", player.GetElement("win").Value.AsInt32 },
                                                                    { "lose", player.GetElement("lose").Value.AsInt32},
                                                                    { "mmr", RankingRanges.Rank1Start},
                                                                    { "rank", player.GetElement("rank").Value.AsInt32} });
                }
                else if (player.GetElement("rank").Value.AsInt32 == 2)
                {
                    collection.UpdateOne(filter, new BsonDocument { { "id", player.GetElement("id").Value.ToString() },
                                                                    { "win", player.GetElement("win").Value.AsInt32 },
                                                                    { "lose", player.GetElement("lose").Value.AsInt32},
                                                                    { "mmr", RankingRanges.Rank2Start},
                                                                    { "rank", player.GetElement("rank").Value.AsInt32} });
                }
                else if (player.GetElement("rank").Value.AsInt32 == 3)
                {
                    collection.UpdateOne(filter, new BsonDocument { { "id", player.GetElement("id").Value.ToString() },
                                                                    { "win", player.GetElement("win").Value.AsInt32 },
                                                                    { "lose", player.GetElement("lose").Value.AsInt32},
                                                                    { "mmr", RankingRanges.Rank3Start},
                                                                    { "rank", player.GetElement("rank").Value.AsInt32} });
                }
                else if (player.GetElement("rank").Value.AsInt32 == 4)
                {
                    collection.UpdateOne(filter, new BsonDocument { { "id", player.GetElement("id").Value.ToString() },
                                                                    { "win", player.GetElement("win").Value.AsInt32 },
                                                                    { "lose", player.GetElement("lose").Value.AsInt32},
                                                                    { "mmr", RankingRanges.Rank4Start},
                                                                    { "rank", player.GetElement("rank").Value.AsInt32} });
                }
            }
        }

        /// <summary>
        /// sets a ban in the database
        /// </summary>
        /// <param name="ban"></param>
        public static void SetBan(BsonDocument ban)
        {
            Connect();

            var collection = dbDatabase.GetCollection<BsonDocument>("Bans");

            collection.InsertOne(ban);
        }

        /// <summary>
        /// gets all bans for a specific player
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static List<BsonDocument>? GetBans(string userID)
        {
            Connect();

            var collection = dbDatabase.GetCollection<BsonDocument>("Bans");

            var filter = Builders<BsonDocument>.Filter.Eq("playerID", userID);

            return collection.Find(filter).ToList();
        }

        /// <summary>
        /// gets all bans in the DB
        /// </summary>
        /// <returns></returns>
        public static List<BsonDocument>? GetAllBans()
        {
            Connect();

            var collection = dbDatabase.GetCollection<BsonDocument>("Bans");

            return collection.Find(_ => true).ToList();
        }

        /// <summary>
        /// updates a ban in the database
        /// </summary>
        /// <param name="ban"></param>
        public static void UpdateBan(BsonDocument ban)
        {
            Connect();

            var collection = dbDatabase.GetCollection<BsonDocument>("Bans");

            var filter = Builders<BsonDocument>.Filter.Eq("playerID", ban.GetElement("playerID").Value.ToString());

            collection.DeleteOne(filter);
            collection.InsertOne(ban);
        }
    }
}
