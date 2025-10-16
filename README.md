# Train Ticket Management System (ASP.NET MVC)

The Train Ticket Management System is an ASP.NET MVC web application for managing trains, tickets, and seat reservations.  
Administrators can add, edit, delete, and view trains and tickets, assign passengers to seats, and ensure proper data consistency.

---

### Models

#### Train.cs
Represents a train with details like:
- `TrainId` (Primary Key)
- `Name`
- `DepartureTime`
- `ArrivalTime`
- `Date`
- `Route`

---

#### **Ticket.cs**
Represents a purchased or reserved ticket.

Attributes:
- `TicketId` (Primary Key)
- `PassengerName`
- `SeatNumber`
- `TrainId` (Foreign Key → Train)

**Relations:**
- Each Ticket belongs to one Train
- When a Train is deleted, its Tickets are deleted automatically
- Each Ticket can optionally be linked to one Seat

---

#### **Seat.cs**
Represents an individual seat within a train.

**Attributes:**
- `SeatId` (Primary Key)
- `SeatNumber`
- `IsReserved`
- `TrainId` (Foreign Key → Train)

**Relations:**
- One Train → Many Seats
- One Seat → Optional Ticket

---

### Relationships Summary
| Relationship | Type | Description |
|---------------|------|-------------|
| Train → Ticket | One-to-Many | Each train can have multiple tickets |
| Train → Seat | One-to-Many | Each train can have multiple seats |
| Seat → Ticket | One-to-One (optional) | Each seat can belong to one ticket or be free |

---

## Features
Full CRUD operations for:
- **Trains** — manage schedules and routes  
- **Tickets** — assign passengers and seats  
- **Seats** — manage availability  

Automatic cascade delete of tickets when a train is deleted  
Form validation for required fields  
Responsive and modern Razor Views  
Clean MVC architecture  

---

## Technologies Used
- ASP.NET MVC 5  
- C#  
- Entity Framework (Code First)  
- SQL Server / LocalDB  
- Razor View Engine  
- Bootstrap 5  


