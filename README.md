# APBD Tutorial 3 - University Equipment Rental

## Project description
This project is a C# console application for a university equipment rental service.  
The system allows adding users and equipment, renting and returning equipment, checking availability, handling invalid operations, calculating penalties for late returns, and generating a short summary report.

## Implemented elements
The project includes:
- abstract class `Equipment`
- equipment types: `Laptop`, `Camera`, `Projector`
- abstract class `User`
- user types: `Student`, `Employee`
- class `Rental`
- class `AppData` for in-memory data storage
- services: `EquipmentService`, `RentalService`
- custom exception: `BusinessRuleException`

## Business rules
The following rules are implemented:
- a student can have at most 2 active rentals
- an employee can have at most 5 active rentals
- unavailable equipment cannot be rented
- equipment marked as unavailable cannot be rented
- a late return results in a penalty
- penalty is calculated as 10 units for each late day

## Project structure and design decisions
The code is divided into a few clear parts.

- `Domain` contains the main objects of the system, such as users, equipment, and rentals.
- `Data` contains `AppData`, which stores lists of users, equipment, and rentals in memory.
- `Services` contains the business logic. This means rental limits, availability checks, penalty calculation, and reports are not placed in `Program.cs`.
- `Exceptions` contains `BusinessRuleException`, which is used when an invalid operation is attempted.

This division improves cohesion because each class has one main responsibility.  
It also reduces coupling because the domain classes do not contain all business logic, and the logic is grouped in service classes.

For example:
- `User` and its subclasses define user type and rental limit
- `Equipment` and its subclasses define shared and specific equipment data
- `RentalService` handles renting, returning, penalties, and checks
- `EquipmentService` handles equipment operations and availability

## Run instruction
1. Open the solution in JetBrains Rider or another C# IDE.
2. Build the project.
3. Run `Program.cs`.
4. The console output presents the required demonstration scenario.

## Demonstration scenario
The program demonstrates:
- adding users
- adding equipment
- a correct rental
- an invalid rental attempt
- an on-time return
- a delayed return with penalty
- a final summary report