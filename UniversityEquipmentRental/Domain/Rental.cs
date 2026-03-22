namespace UniversityEquipmentRental.Domain;

public class Rental
{
    private static int _nextId = 1;

    public int Id { get; }
    public User User { get; }
    public Equipment Equipment { get; }
    public DateTime RentalDate { get; }
    public DateTime DueDate { get; }
    public DateTime? ReturnDate { get; private set; }
    public decimal Penalty { get; private set; }

    public bool IsReturned => ReturnDate.HasValue;
    public bool IsActive => !IsReturned;
    public bool IsOverdue => !IsReturned && DateTime.Now.Date > DueDate.Date;

    public Rental(User user, Equipment equipment, DateTime rentalDate, DateTime dueDate)
    {
        Id = _nextId++;
        User = user;
        Equipment = equipment;
        RentalDate = rentalDate;
        DueDate = dueDate;
    }

    public void Return(DateTime returnDate, decimal penalty)
    {
        ReturnDate = returnDate;
        Penalty = penalty;
    }

    public override string ToString()
    {
        string status = IsReturned
            ? $"Returned: {ReturnDate:yyyy-MM-dd}, Penalty: {Penalty}"
            : "Active";

        return $"{Id}: {Equipment.Name} rented by {User.FirstName} {User.LastName} from {RentalDate:yyyy-MM-dd} to {DueDate:yyyy-MM-dd} | {status}";
    }
}