namespace ChessBrowser.Components.Models;
public class Player 
{
    public string Name { get; set; }
    public int Elo { get; set; }
    
    public int Pid { get; set; }

    public Player(string name, int elo, int pid)
    {
        Name = name;
        Elo = elo;
        Pid = pid;
    }
    
}