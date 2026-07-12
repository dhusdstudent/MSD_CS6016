namespace Lab_ScaffoldingLINQ.Entities;

public class Title
{
    public string isbn { get; set; } = "";
    public string titleName { get; set; } = "";
    public string author { get; set; } = "";
    
    public ICollection<Inventory> inventories { get; set; } = new List<Inventory>();
}