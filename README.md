# 💳 Payment API – ASP.NET Core + EF Core 6

A payment processing API built with **ASP.NET Core** and **Entity Framework Core 6**, focused on:

* ✅ Idempotency
* ✅ Concurrency control
* ✅ Atomic state transitions
* ✅ Double-payment protection
* ✅ Load testing under high concurrency

---

## 🚀 Technologies

* .NET 6
* ASP.NET Core
* Entity Framework Core 6
* SQL Server
* Docker (for load testing with `wrk`)

---

## 🏗 Architecture

The project follows a simple layered structure:

```
Controller → Repository → Database
```

* **Controllers**: expose HTTP endpoints
* **Repository**: handles data access
* **DTO + Mapper**: separates domain model from API responses

---

## 🔐 Idempotency

Each payment includes an:

```
IdempotencyKey (GUID)
```

A unique constraint is enforced at the database level:

```csharp
modelBuilder.Entity<Payment>()
    .HasIndex(p => p.IdempotencyKey)
    .IsUnique();
```

If a duplicate request is sent:

* The database throws a unique constraint exception
* The exception is caught
* The existing payment is returned
* No duplicate record is created

This guarantees safe retries.

---

## ⚡ Concurrency Control

To prevent double processing under high load, an atomic state transition is used:

```sql
UPDATE Payment
SET Status = Processing
WHERE Id = @id AND Status = Created
```

This ensures:

* Only one thread can move a payment to `Processing`
* Concurrent requests fail safely
* No race conditions occur

Payment state flow:

```
Created → Processing → Paid
```

---

## 📡 Endpoints

### Create Payment

```
POST /api/payment
```

Body:

```json
{
  "price": 100.50,
  "idempotencyKey": "guid"
}
```

---

### Get Payment by Id

```
GET /api/payment/{id}
```

---

### Process Payment

```
PATCH /api/payment/pay/{id}
```

This endpoint:

1. Atomically moves the payment to `Processing`
2. Simulates processing
3. Updates the payment to `Paid`

---

## 🧪 Load Testing

Load tests were performed using `wrk`:

```
4 threads
100 connections
```

Results:

```
~1200 requests/sec
No race conditions
No double processing
```

Under concurrent load, only one request successfully processes the payment — as expected in a real payment gateway scenario.

---

## 🧠 Engineering Concepts Applied

* Atomic state transitions
* Optimistic concurrency control
* Unique constraint handling
* Exception filtering (`when` in C#)
* Separation of concerns
* High-concurrency load testing

---

## ▶️ Running the Project

1. Configure the connection string in `appsettings.json`
2. Run migrations
3. Execute:

```
dotnet run
```

---

## 📌 Possible Improvements

* Introduce a Service Layer
* Domain-driven design refinements
* Structured logging
* Retry policies
* Docker Compose setup
* Unit and integration tests
* 
## 📌 Possible Improvements

* Introduce a Service Layer
* Domain-driven design refinements
* Structured logging
* Retry policies
* Docker Compose setup
* Unit and integration tests
* Implemente external Payment Service, once i mock the routes

---

## 👨‍💻 Author

Giovanne Colli

---
