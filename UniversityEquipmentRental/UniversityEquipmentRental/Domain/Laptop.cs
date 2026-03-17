namespace UniversityEquipmentRental.Domain;

public class Laptop : Equipment
{
    public string Cpu { get; set; }
    public int RamGb { get; set; }

    public Laptop(string name, string cpu, int ramGb) : base(name)
    {
        Cpu = cpu;
        RamGb = ramGb;
    }

    public override string ToString()
    {
        return base.ToString() + $" | Type: Laptop | CPU: {Cpu} | RAM: {RamGb}GB";
    }
}