using UniversityEquipmentRental.Domain;

namespace UniversityEquipmentRental.Data;

public class AppData
{
    public List<User> Users { get; } = new();
    public List<Equipment> EquipmentItems { get; } = new();
    public List<Rental> Rentals { get; } = new();
}