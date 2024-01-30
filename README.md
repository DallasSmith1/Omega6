<h1>Omega 6ix Bot <img src="https://i.imgur.com/wqjxAUp.png" width="50" height="50"/></h1>
<p>The Omega 6ix Bot was created to act as a more competitive ranking system for the video game Omega Strikers. The bot facilitates private matches by handling team captain selection, lobby creation, and team communication channels. It tracks player ratings, maintains leaderboards, facilitates level changes, and automates administrative tasks such as bans, reports, and support and player categorization based on ratings.</p>
<p>I created this app using C# with the DSharpplus library and used MongoDB as a database to store all the players, games, and any other information stored for the server.</p>
<hr/>
<h3>Main Functionality</h3>
<p>The bot automates rank checks for users, assigning them discord roles based on their in-game rank. It facilitates match queues, creating lobbies when six players join. The bot initiates a team captain process to determine teams and establishes dedicated channels for team communication within the Discord server.</p>
<p>For the automated rank check system, the bot scrapes this website (https://corestrike.gg) for the players rank.</p>
<details>
  <summary>Bot Commands</summary>
  <div>
    <ul>
      <li><b>!q</b> - adds the player to the queue for a game.</li>
      <li><b>!leave</b> - removes the player from the queue.</li>
      <li><b>!status</b> - returns the status of the queue displaying all players in the queue.</li>
      <li><b>!spectate</b> - provides details to the specified game to allow the player to spectate.</li>
      <li><b>!report</b> - reports a game either as a win or a loss for the player.</li>
      <li><b>!rankcheck</b> - rank checks the player into their assigned discord ranks.</li>
      <li><b>!stats</b> - provides the user with their current stats within the server.</li>
    </ul>
  </div>
</details>
<hr/>
<h3>Admin Automation</h3>
<p>The bot automates admin tasks, including banning users and preventing them from queuing by a single command. It offers functionality to automatically unban users and notify them via direct message. Additionally, the bot provides commands for handling faulty rank checks, false reports, game cancellations, and resetting games and ranks for a new season.</p>
<details>
  <summary>Admin Commands</summary>
  <div>
    <ul>
      <li><b>!update_leaderboard</b> - manually updates the leaderboard aside from the automatic hourly update.</li>
      <li><b>!retrieve_game</b> - retrieves all the info about a game such as teams, players, win%, and the winning team along with time stamps. This allows admins to:
        <ul>
          <li>Cancel the match</li>
          <li>Reverse the outcome</li>
          <li>Add 2 hours to the report time</li>
        </ul>
      </li>
      <li><b>!retrieve_player</b> - retrieves a players information including their stats, in game name, and when they registered. This allows admins to unregister players from the server.</li>
      <li><b>!ban</b> - bans a player for a specified number of days.</li>
      <li><b>!unban</b> - manually unbans a player before their time is complete.</li>
      <li><b>!get_record</b> - retrieves a players ban record.</li>
      <li><b>!wipe_games</b> - removes all games in the database.</li>
      <li><b>!reset_mmr</b> - resets all players ratings to default values.</li>
    </ul>
  </div>
</details>
<hr/>
<h3>Summary</h3>
<p>I developed this bot to foster a competitive community for an upcoming game, offering an alternative to the built-in ranking system. The bot underwent beta testing, incorporating real player feedback and bug reports for immediate resolution. A partnership with the game itself was secured, resulting in in-game currency and loot for promotional giveaways during the inaugural season. This project enriched my consumer experience, involving listening to feedback, addressing consumer desires, and exploring alternative solutions through direct communication..</p>
<p>If you are interested in seeing the bot in action, here is an invite link to the discord server: https://discord.gg/AJvdRPAhyu</p>
<p>Update: The game has since parished. The server is still up for demo purposes.</p>
