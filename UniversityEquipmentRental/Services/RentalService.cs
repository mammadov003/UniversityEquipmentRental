using UniversityEquipmentRental.Data;
using UniversityEquipmentRental.Domain;
using UniversityEquipmentRental.Exceptions;

namespace UniversityEquipmentRental.Services;

public class RentalService
{
    private readonly AppData _data;

    public RentalService(AppData data)
    {
        _data = data;
    }

    public Rental RentEquipment(int userId, int equipmentId, int days)
    {
        var user = _data.Users.FirstOrDefault(u => u.Id == userId);
        if (user == null)
            throw new BusinessRuleException("User not found.");

        var equipment = _data.EquipmentItems.FirstOrDefault(e => e.Id == equipmentId);
        if (equipment == null)
            throw new BusinessRuleException("Equipment not found.");

        if (equipment.IsMarkedUnavailable)
            throw new BusinessRuleException("Equipment is marked as unavailable.");

        if (!equipment.IsAvailable)
            throw new BusinessRuleException("Equipment is not available.");

        int activeRentals = _data.Rentals.Count(r => r.User.Id == userId && r.IsActive);
        if (activeRentals >= user.MaxActiveRentals)
            throw new BusinessRuleException("User has reached the rental limit.");

        var rental = new Rental(user, equipment, DateTime.Now, DateTime.Now.AddDays(days));

        equipment.IsAvailable = false;
        _data.Rentals.Add(rental);

        return rental;
    }

    public decimal ReturnEquipment(int rentalId, DateTime returnDate)
    {
        var rental = _data.Rentals.FirstOrDefault(r => r.Id == rentalId);
        if (rental == null)
            throw new BusinessRuleException("Rental not found.");

        if (rental.IsReturned)
            throw new BusinessRuleException("Equipment has already been returned.");

        decimal penalty = CalculatePenalty(rental.DueDate, returnDate);

        rental.Return(returnDate, penalty);
        rental.Equipment.IsAvailable = true;

        return penalty;
    }

    public List<Rental> GetActiveRentalsForUser(int userId)
    {
        return _data.Rentals
            .Where(r => r.User.Id == userId && r.IsActive)
            .ToList();
    }

    public List<Rental> GetOverdueRentals()
    {
        return _data.Rentals
            .Where(r => r.IsOverdue)
            .ToList();
    }

    public string GetSummaryReport()
    {
        int totalEquipment = _data.EquipmentItems.Count;
        int availableEquipment = _data.EquipmentItems.Count(e => e.IsAvailable && !e.IsMarkedUnavailable);
        int unavailableEquipment = _data.EquipmentItems.Count(e => e.IsMarkedUnavailable);
        int activeRentals = _data.Rentals.Count(r => r.IsActive);
        int overdueRentals = _data.Rentals.Count(r => r.IsOverdue);

        return $"Total equipment: {totalEquipment}\n" +
               $"Available equipment: {availableEquipment}\n" +
               $"Marked unavailable: {unavailableEquipment}\n" +
               $"Active rentals: {activeRentals}\n" +
               $"Overdue rentals: {overdueRentals}";
    }

    private decimal CalculatePenalty(DateTime dueDate, DateTime returnDate)
    {
        if (returnDate.Date <= dueDate.Date)
            return 0;

        int lateDays = (returnDate.Date - dueDate.Date).Days;
        return lateDays * 10m;
    }
}