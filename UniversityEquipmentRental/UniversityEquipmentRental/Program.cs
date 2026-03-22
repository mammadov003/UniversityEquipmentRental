using UniversityEquipmentRental.Data;
using UniversityEquipmentRental.Domain;
using UniversityEquipmentRental.Services;
using UniversityEquipmentRental.Exceptions;

var data = new AppData();

var equipmentService = new EquipmentService(data);
var rentalService = new RentalService(data);

// USERS
var student = new Student("John", "Smith", "S123");
var employee = new Employee("Anna", "Brown", "IT");

data.Users.Add(student);
data.Users.Add(employee);

// EQUIPMENT
var laptop = new Laptop("Dell XPS", "i7", 16);
var camera = new Camera("Canon EOS", 24, true);
var projector = new Projector("Epson", 3000, "1080p");

equipmentService.AddEquipment(laptop);
equipmentService.AddEquipment(camera);
equipmentService.AddEquipment(projector);

Console.WriteLine("=== EQUIPMENT LIST ===");
foreach (var e in equipmentService.GetAllEquipment())
    Console.WriteLine(e);

// VALID RENTAL
Console.WriteLine("\n=== VALID RENTAL ===");
var rental1 = rentalService.RentEquipment(student.Id, laptop.Id, 3);
Console.WriteLine("Rental successful");

// INVALID RENTAL (limit or unavailable)
Console.WriteLine("\n=== INVALID RENTAL ===");
try
{
    rentalService.RentEquipment(student.Id, laptop.Id, 2);
}
catch (BusinessRuleException ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}

// RETURN ON TIME
Console.WriteLine("\n=== RETURN ON TIME ===");
var penalty1 = rentalService.ReturnEquipment(rental1.Id, DateTime.Now.AddDays(2));
Console.WriteLine($"Returned. Penalty: {penalty1}");

// LATE RENTAL
Console.WriteLine("\n=== LATE RETURN ===");
var rental2 = rentalService.RentEquipment(employee.Id, camera.Id, 2);

var penalty2 = rentalService.ReturnEquipment(rental2.Id, DateTime.Now.AddDays(5));
Console.WriteLine($"Returned late. Penalty: {penalty2}");

// OVERDUE LIST (optional display)
Console.WriteLine("\n=== OVERDUE RENTALS ===");
foreach (var r in rentalService.GetOverdueRentals())
    Console.WriteLine(r);

// FINAL REPORT
Console.WriteLine("\n=== FINAL REPORT ===");
Console.WriteLine(rentalService.GetSummaryReport());