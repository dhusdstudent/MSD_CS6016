namespace Lab_ScaffoldingLINQ.Entities;

public class Inventory
{
    public int ? serial { get; set; }
    public string isbn { get; set; } = "";

    public Title title { get; set; } = null!;
    public Checkedout?  checkedout { get; set; }
}