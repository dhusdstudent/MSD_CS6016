namespace Lab_ScaffoldingLINQ.Entities;

public class Checkedout
{
    public int cardNum { get; set; }
    public int serial { get; set; }

    public Patron patron { get; set; } = null!;
    public Inventory Inventory { get; set; } = null;
}