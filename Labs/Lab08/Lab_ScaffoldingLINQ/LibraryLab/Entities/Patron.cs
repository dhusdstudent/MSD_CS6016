namespace Lab_ScaffoldingLINQ.Entities;

public class Patron
{
    public int cardNum { get; set; }
    public string name  { get; set; } = "";
    
    public ICollection<Checkedout> checkedouts { get; set; } = new List<Checkedout>();
}