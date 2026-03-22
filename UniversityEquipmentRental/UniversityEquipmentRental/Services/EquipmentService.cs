using UniversityEquipmentRental.Data;
using UniversityEquipmentRental.Domain;
using UniversityEquipmentRental.Exceptions;

namespace UniversityEquipmentRental.Services;

public class EquipmentService
{
    private readonly AppData _data;

    public EquipmentService(AppData data)
    {
        _data = data;
    }

    public void AddEquipment(Equipment equipment)
    {
        _data.EquipmentItems.Add(equipment);
    }

    public List<Equipment> GetAllEquipment()
    {
        return _data.EquipmentItems;
    }

    public List<Equipment> GetAvailableEquipment()
    {
        return _data.EquipmentItems
            .Where(e => e.IsAvailable && !e.IsMarkedUnavailable)
            .ToList();
    }

    public void MarkUnavailable(int equipmentId)
    {
        var equipment = _data.EquipmentItems.FirstOrDefault(e => e.Id == equipmentId);

        if (equipment == null)
            throw new BusinessRuleException("Equipment not found.");

        equipment.IsMarkedUnavailable = true;
        equipment.IsAvailable = false;
    }
}