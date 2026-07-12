namespace Lab_ScaffoldingLINQ.Entities;

public class Phone
{
    public int cardNum { get; set; }
    public string phoneNum { get; set; } = "";

    public Patron patron { get; set; } = null!;
}