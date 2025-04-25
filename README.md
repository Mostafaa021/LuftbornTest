# E-Commerce Application with .NET 8 and Angular

This repository showcases an e-commerce application built with **.NET 8**, **SQL Server**, and **Angular**. The back-end utilizes **CleanArchitecture** , **Entity Framework Core**, **Mapster** for object-to-object mapping, and implements various design patterns like **Repository**, **Unit of Work**, **Factory**, **Mediator** (using **MediatR**), and **CQRS** for command-query separation. The application also uses **FluentValidation** for validation and **BackgroundService (IHostedService)** for data seeding.

## Architecture Overview

The application is structured using common design patterns and best practices, ensuring a clean, maintainable, and scalable codebase.

### 1. **Back-End Technologies**
- **.NET 8** - The back-end API is built using **.NET 8**.
- **SQL Server** - The database used to store e-commerce data.
- **Entity Framework Core** - ORM for interacting with the SQL Server database.
- **Mapster** - A library for object mapping between domain models and DTOs.
- **FluentValidation** - For handling validation logic in a consistent manner.
- **MediatR** - Implements the **Mediator Pattern**, ensuring separation of concerns between commands and queries (CQRS).
- **BackgroundService (IHostedService)** - For running background tasks like seeding data.
- **Repository Pattern** - Provides a way to abstract data access and operations.
- **Unit of Work Pattern** - Ensures a coordinated, transactional approach to changes in the database.
- **Strategy Pattern** - Behavioural Pattern.
- **Factory Pattern** - Creational Pattern.

### 2. **Front-End Technologies**
- **Angular** - Front-end application for interacting with the back-end API.
- **Bootstrap** - For styling and responsive layouts.
  
---

## Features

- **CRUD Operations**: Manage products, categories, users, and orders.
- **CQRS**: Command and Query separation for optimized performance and scalability.
- **Unit of Work & Repository Patterns**: Ensures a clean, maintainable data access layer.
- **Mapster for DTO Mapping**: Efficient object-to-object mapping to reduce boilerplate code.
- **FluentValidation**: Ensures consistent validation rules for commands and requests.
- **Mediator Pattern with MediatR**: Implementing the mediator pattern for decoupled requests and responses.
- **Background Service for Data Seeding**: Use of `IHostedService` for background tasks like data seeding.

---
## Key Features & Design Patterns

### **CQRS (Command Query Responsibility Segregation)**
- The system is designed to separate read (Query) and write (Command) operations for scalability.
- Commands modify data, while Queries retrieve data.
- Each operation is handled by a separate handler, ensuring clear boundaries and better performance.

### **Repository & Unit of Work Patterns**
- The **Repository Pattern** is used for data access, abstracting the interactions with the database.
- The **Unit of Work Pattern** ensures that all changes are tracked and saved together as a single transaction.

### **Strategy Pattern** - Behavioural Pattern.
### **Factory Pattern** - Creational Pattern. 

### **Mediator Pattern (MediatR)**
- **MediatR** is used to decouple request and response handling.
- Commands and queries are handled by separate handlers, ensuring that the system remains scalable and maintainable.

### **Mapster**
- **Mapster** is used for mapping entities to DTOs and vice versa.
- This reduces boilerplate code and simplifies the transformation between domain models and data transfer objects.

### **FluentValidation**
- **FluentValidation** is used to define validation rules for commands and queries in a fluent, easy-to-read manner.
- Ensures that incoming data is validated before processing.

### **BackgroundService (IHostedService)**
- An **IHostedService** is used for background tasks like data seeding during application startup.
- This helps to keep the application data populated with seed values, and the seeding process runs automatically when the application starts.
---
