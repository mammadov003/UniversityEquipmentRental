namespace UniversityEquipmentRental.Domain;

public abstract class Equipment
{
    private static int _nextId = 1;

    public int Id { get; }
    public string Name { get; set; }
    public bool IsAvailable { get; set; }

    protected Equipment(string name)
    {
        Id = _nextId++;
        Name = name;
        IsAvailable = true;
    }

    public override string ToString()
    {
        return $"{Id}: {Name} | Available: {IsAvailable}";
    }
}