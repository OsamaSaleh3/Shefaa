🏥 Shefaa - Medical Management System

[![.NET](https://img.shields.io/badge/.NET-10.0-purple?style=flat-square&logo=dotnet)](https://dotnet.microsoft.com/)
[![Architecture](https://img.shields.io/badge/Architecture-Clean%20Architecture-blue?style=flat-square)](https://github.com/ardalis/CleanArchitecture)
[![Pattern](https://img.shields.io/badge/Pattern-CQRS%20%26%20DDD-green?style=flat-square)]()
[![Database](https://img.shields.io/badge/Database-SQL%20Server-lightgrey?style=flat-square&logo=microsoft-sql-server)]()

**Shefaa** is a modern, scalable, and robust Medical Management System backend designed to streamline clinic and hospital operations. Built with **.NET 10**, it adheres to the principles of **Clean Architecture** and **Domain-Driven Design (DDD)** to ensure maintainability, scalability, and testability.

## 🚀 Features

### 👨‍⚕️ Core Modules
- **Patient Management:** Comprehensive profiles for patients.
- **Doctor Management:** Managing doctor profiles and specialties.
- **Medical Records:** Tracking patient history and visits.

### 💊 Prescription System
- **DDD-Based Logic:** Prevents invalid states (e.g., duplicate medications).
- **Smart Tracking:** Handles medication details (Dosage, Frequency, Duration).
- **Audit Trails:** Tracks creation and modification dates.

### 💰 Invoicing & Payments
- **Auto-Numbering:** Generates sequential, gap-free invoice numbers (e.g., `INV-2026-0001`).
- **Dynamic Calculations:** Automatic calculation of totals and remaining balances via Domain Logic.
- **Payment Tracking:** Supports partial payments and multiple payment methods.
