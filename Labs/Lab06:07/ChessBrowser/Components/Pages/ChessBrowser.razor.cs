using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks.Dataflow;
using ChessBrowser.Components.Models;
using Microsoft.AspNetCore.Components.Forms;
using Npgsql;

namespace ChessBrowser.Components.Pages
{
  public partial class ChessBrowser
  {
    /// <summary>
    /// Bound to the Unsername form input
    /// </summary>
    private string Username = "";

    /// <summary>
    /// Bound to the Database form input
    /// </summary>
    private string Database = "";

    /// <summary>
    /// Represents the progress percentage of the current
    /// upload operation. Update this value to update 
    /// the progress bar.
    /// </summary>
    private int    Progress = 0;

    /// <summary>
    /// This method runs when a PGN file is selected for upload.
    /// Given a list of lines from the selected file, parses the 
    /// PGN data, and uploads each chess game to the user's database.
    /// </summary>
    /// <param name="PGNFileLines">The lines from the selected file</param>

    private Game BuildGame(Dictionary<string, string> partsOfGame, StringBuilder moves)
    {
      Player whitePlayer = new Player(partsOfGame["White"], int.Parse(partsOfGame["WhiteElo"]), 0);
      Player blackePlayer = new Player(partsOfGame["Black"], int.Parse(partsOfGame["BlackElo"]), 0);

      char result;

      switch (partsOfGame["Result"])
      {
        case "1-0":
          result = 'W';
          break;
        case "0-1":
          result = 'B';
          break;
        default:
          result = 'D';
          break;
      }

      DateTime eventDate;
      if (!DateTime.TryParse(partsOfGame["EventDate"], out eventDate))
      {
        eventDate = DateTime.MinValue;
      }

      ChessEvent chessEvent = new ChessEvent(partsOfGame["Event"], partsOfGame["Site"],
        eventDate, 0);
      
      return new Game(chessEvent, whitePlayer, blackePlayer, partsOfGame["Round"], result,
        moves.ToString());
    }
    
    private List<Game> ParsePGN(string[] PGNFileLines)
    {
      List<Game> allGames = new();
      Dictionary<string, string> partsOfGame = new();
      StringBuilder moves = new();
      bool readingMoves = false;

      foreach (string line in PGNFileLines)
      {
        if (string.IsNullOrEmpty(line))
        {
          if (!readingMoves)
          {
            readingMoves = true;
          }
          else
          {
            allGames.Add(BuildGame(partsOfGame, moves));

            partsOfGame.Clear();
            moves.Clear();
            readingMoves = false;
          }

          continue;
        }

        if (!readingMoves)
          {
            Match match = Regex.Match(line, @"^\[(\w+)\s+""(.*)""\]$"); 
            
            if (match.Success) 
            {
              partsOfGame[match.Groups[1].Value] = match.Groups[2].Value;
            }
          } 
          else 
          {
            moves.AppendLine(line);
          }
      }
      
      if (partsOfGame.Count > 0) {
        allGames.Add(BuildGame(partsOfGame, moves));
      }
      
      return allGames;
    }
    
    
    //---------------------------------------------------

    private async Task<int> SetPlayerPID(Player p)
    {
      string connection = GetConnectionString();
      
      await using var conn = NpgsqlDataSource.Create(connection);
      await using var open = await conn.OpenConnectionAsync();
      await using (var cmd = new NpgsqlCommand(
                     "SELECT pid FROM players WHERE name = @name", open))
      {
        cmd.Parameters.AddWithValue("name", p.Name);
        var result = await cmd.ExecuteScalarAsync();

        if (result != null) return (int) result;
      }

      await using (var cmd = new NpgsqlCommand(
                     @"INSERT INTO players(name, elo) VALUES (@name, @elo) RETURNING pid;", open))
      {
        cmd.Parameters.AddWithValue("name", p.Name);
        cmd.Parameters.AddWithValue("elo", p.Elo);
        return (int) await cmd.ExecuteScalarAsync();
      }
    }

    private async Task<int> SetEventID(ChessEvent e)
    {
    string connection = GetConnectionString();
    
    await using var conn = NpgsqlDataSource.Create(connection);
    await using var open = await conn.OpenConnectionAsync();
    await using (var cmd = new NpgsqlCommand(
                   "SELECT eid FROM events WHERE name = @name AND site=@site AND date=@date", open))
    {
      cmd.Parameters.AddWithValue("name", e.Name);
      cmd.Parameters.AddWithValue("site", e.Site);
      cmd.Parameters.AddWithValue("date", e.Date);
      
      var result = await cmd.ExecuteScalarAsync();

      if (result != null) return (int)result;
    }

    await using (var cmd = new NpgsqlCommand(
                   @"INSERT INTO events(name,site, date) VALUES  (@name, @site, @date) RETURNING eid;", open))
    {
      cmd.Parameters.AddWithValue("name", e.Name);
      cmd.Parameters.AddWithValue("site", e.Site);
      cmd.Parameters.AddWithValue("date", e.Date);
      return (int) await cmd.ExecuteScalarAsync();
    }
    
    }

    private async Task InsertGame(Game game)
    {
      int whitePid = await SetPlayerPID(game.White);
      int blackPid = await SetPlayerPID(game.Black);
      int eventID = await SetEventID(game.Event);
      
      string connection = GetConnectionString();
      await using var conn = NpgsqlDataSource.Create(connection);
      await using var open = await conn.OpenConnectionAsync();

      await using var cmd = new NpgsqlCommand(
        @"INSERT INTO games (round, result, moves, whiteplayer, blackplayer, eid)
      VALUES (@round, @result, @moves, @whiteplayer, @blackplayer, @event)", open);
      
      cmd.Parameters.AddWithValue("round", game.Round);
      cmd.Parameters.AddWithValue("result", game.Result);
      cmd.Parameters.AddWithValue("moves", game.Moves);
      cmd.Parameters.AddWithValue("whiteplayer", whitePid);
      cmd.Parameters.AddWithValue("blackplayer", blackPid);
      cmd.Parameters.AddWithValue("event", eventID);
      
      await cmd.ExecuteNonQueryAsync();
    }
    
    private async Task InsertGameData(string[] PGNFileLines)
    {
      try {
      
        List <Game> manyGames = ParsePGN(PGNFileLines);
        int count = 0;
        
        foreach (Game game in manyGames)
        {
          await InsertGame(game);
          count++;
          Progress = count * 100 / manyGames.Count;
          await InvokeAsync(StateHasChanged);
        }
      }
      catch (Exception e)
      {
        Console.WriteLine("uh oh: " + e.Message + e.StackTrace);
        throw;
      }
    }


    private async Task<string> PerformQuery(string white, string black, string opening,
      string winner, bool useDate, DateTime start, DateTime end, bool showMoves)
    {
      string connection = GetConnectionString();
      StringBuilder parsedResult = new StringBuilder();
      int numRows = 0;

      await using var conn = NpgsqlDataSource.Create(connection);
      await using var open = await conn.OpenConnectionAsync();

      try
      {
        StringBuilder sql = new();

        sql.Append(@"SELECT
            e.name, e.site, e.date, w.name, w.elo, b.name, b.elo, g.result");

        if (showMoves)
        {
          sql.Append(", g.moves");
        }
        
        sql.Append (" FROM games g JOIN players w ON g.whiteplayer = w.pid JOIN players b " +
                    "ON g.blackplayer = b.pid JOIN events e ON g.eid = e.eid WHERE 1=1");    

        if (!string.IsNullOrEmpty(white)) sql.Append(" AND w.name = @white");
        if (!string.IsNullOrEmpty(black)) sql.Append(" AND b.name = @black");
        if (!string.IsNullOrEmpty(winner)) sql.Append(" AND g.result = @winner");
        if (useDate)  sql.Append(" AND e.date BETWEEN @start AND @end");
        if (!string.IsNullOrEmpty(opening)) sql.Append(" AND g.moves LIKE @opening");
        
        await using var cmd = new NpgsqlCommand(sql.ToString(), open);
        
        if (!string.IsNullOrEmpty(white)) cmd.Parameters.AddWithValue("white", white);
        if (!string.IsNullOrEmpty(black)) cmd.Parameters.AddWithValue("black", black);
        if (!string.IsNullOrEmpty(winner)) cmd.Parameters.AddWithValue("winner", winner[0]);

        if (useDate)
        {
          cmd.Parameters.AddWithValue("start", start);
          cmd.Parameters.AddWithValue("end", end);
        }
        
        if (!string.IsNullOrEmpty(opening)) cmd.Parameters.AddWithValue("opening", opening + "%");
        
        await using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
          numRows++;
          
          string eName = reader.GetString(0);
          string site = reader.GetString(1);
          DateTime date = reader.GetDateTime(2);
          
          string wName = reader.GetString(3);
          int whiteElo = reader.GetInt32(4);
          string bName = reader.GetString(5);
          int blackElo = reader.GetInt32(6);
          
          char result = reader.GetChar(7);
          string moves = "";
          
          if (showMoves)
          {
            moves = reader.GetString(8);
          }

          parsedResult.AppendLine(($"Event: {eName}"));
          parsedResult.AppendLine($"Site: {site}");
          parsedResult.AppendLine($"Date: {date}");
          parsedResult.AppendLine($"White: {wName} ({whiteElo})");
          parsedResult.AppendLine($"Black: {bName} ({blackElo})");
          parsedResult.AppendLine($"Result: {result}");

          if (showMoves)
          {
            parsedResult.AppendLine($"Moves: {moves}");
          }

          parsedResult.AppendLine();

        }
      } catch (Exception e)
      {
        System.Diagnostics.Debug.WriteLine(e.Message);
      }

      return numRows + " results\n" + parsedResult;
    }


    private string GetConnectionString()
    {
      //If you install postgres with homebrew, you can use this
      //connection string (with modifications) to connect to it instead of ATR
      //return "Server=localhost; Username=ben; database=chess";
      return "server=atr.eng.utah.edu;database=" + Database + ";Username=" + Username;
    }


    /// <summary>
    /// This method will run when the file chooser is used.
    /// It loads the files contents as an array of strings,
    /// then invokes the InsertGameData method.
    /// </summary>
    /// <param name="args">The event arguments, which contains the selected file name</param>
    private async void HandleFileChooser(EventArgs args)
    {
      try
      {
        string fileContent = string.Empty;

        InputFileChangeEventArgs eventArgs = args as InputFileChangeEventArgs ?? throw new Exception("unable to get file name");
        if (eventArgs.FileCount == 1)
        {
          var file = eventArgs.File;
          if (file is null)
          {
            return;
          }

          // load the chosen file and split it into an array of strings, one per line
          using var stream = file.OpenReadStream(1000000); // max 1MB
          Console.WriteLine("Starting to read file");
          using var reader = new StreamReader(stream);                   
          fileContent = await reader.ReadToEndAsync();
          string[] fileLines = fileContent.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

          // insert the games, and don't wait for it to finish
          // _ = throws away the task result, since we aren't waiting for it
          Console.WriteLine("Read file, about to process it");
          _ = InsertGameData(fileLines);
        }
      }
      catch (Exception e)
      {
        Debug.WriteLine("an error occurred while loading the file..." + e);
      }
    }

  }
  
  

}
