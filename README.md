# BankManagement

The **BankManagement** project is a modular and layered banking management solution developed using the **ABP Framework (.NET)**.  
The goal of the project is to provide a **clean architecture**, **reusable services**, and a **scalable infrastructure**.

---

## 🏗️ Architecture

- **Domain / Domain.Shared**  
  Entities, aggregate roots, domain events, business rules, shared classes  

- **Application / Application.Contracts**  
  Application services, DTOs, and interfaces  

- **EntityFrameworkCore**  
  Data access and repository implementations  

- **HttpApi / HttpApi.Client / HttpApi.Host**  
  API endpoints, client, and host applications  

- **DbMigrator**  
  Database migration operations  

- **Worker / Background Jobs**  
  Queue and background task processing (if applicable)  

---

## 🚀 Key Technologies

- **ABP Framework** (modular, domain-driven design)  
- **.NET 9**  
- **RabbitMQ** (event-driven messaging)  
- **Elasticsearch** (logging and event indexing)  
- **Redis** (distributed caching)  
- **Hangfire** (background and scheduled jobs)  
- **EF Core** (SQL Server)  
- **AutoMapper & FluentValidation**  

---

## 🎯 Purpose

The project aims to build a **scalable**, **maintainable**, and **testable** infrastructure for banking management processes.  
With a layered architecture and service abstractions, the system is designed to be **modular**, **easily extensible**, and **highly observable**.

---
