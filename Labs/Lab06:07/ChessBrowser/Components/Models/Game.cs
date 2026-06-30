namespace ChessBrowser.Components.Models;

public class Game
{
    public ChessEvent Event { get; set; }
    public Player White { get; set; }
    public Player Black { get; set; }
    public string Round { get; set; }
    public char Result { get; set; }
    public string Moves { get; set; }
    
    public Game (ChessEvent @event,  Player playerWhite, Player playerBlack, string round, char result,  string moves)
    {
      Event = @event;
      White = playerWhite;
      Black = playerBlack;
      Round = round;
      Result = result;
      Moves = moves;
    }
}