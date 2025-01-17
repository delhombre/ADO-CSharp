namespace ADO.Models;

public class Pokemon
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int Height { get; set; }
    public int Weight { get; set; }
    public string? Description { get; set; }
    public int Type1Id { get; set; }
    public int? Type2Id { get; set; }
    public Type? Type1 { get; set; }
    public Type? Type2 { get; set; }

    public override string ToString()
    {
        return $"{Id} {Name} {Height} {Weight} {Description} {Type1Id} {Type2Id}";
    }
}
