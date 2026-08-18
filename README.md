# BulkyBook — E-Commerce Book Store

A full-featured ASP.NET Core MVC e-commerce application for browsing and purchasing books, built with a multi-tier architecture, role-based access control, and integrations for online payments and email services.

## Overview

BulkyBook is a web application that simulates a real-world online bookstore. It provides a customer-facing storefront where users can browse products, view product details, add items to their shopping cart, place orders, and complete payments. It also provides an administrative area where authorized users can manage products, categories, users, and orders. The project was developed using a multi-project architecture to separate presentation, business logic, data access, models, and shared utilities, making the application easier to maintain, extend, and organize.

## Features

BulkyBook provides complete product management functionality, including creating, viewing, updating, and deleting products. Products can be associated with categories and can contain product images that are uploaded directly through the application. Product listings also support pagination to make browsing large numbers of products easier.

The application includes category management with CRUD operations and validation to prevent duplicate category names. Categories are used to organize products and make the store easier to navigate.

BulkyBook provides a shopping and ordering workflow that allows customers to browse products, add products to their shopping cart, review their orders, and track order status. Administrators can manage orders and update their status throughout the order lifecycle, including Pending, Approved, Processing, Shipped, Cancelled, and Refunded statuses.

The application uses ASP.NET Core Identity for authentication and authorization. Role-based authorization is used to control access to different areas and features of the application, allowing administrators and customers to access functionality appropriate to their roles.

An administrative dashboard is included for managing the main components of the store. Administrators can manage products, categories, users, and orders and view important store statistics such as the number of products, users, orders, and total revenue.

BulkyBook integrates with Stripe to support online payment processing during checkout. The application also integrates with Mailjet for sending emails and application notifications.

## Architecture

The solution follows a multi-project N-Tier architecture that separates the responsibilities of the application into different projects instead of placing all functionality inside the web project.

**BulkyBookWeb** is the presentation layer and contains the ASP.NET Core MVC application, Controllers, Views, Areas, ViewComponents, static files, and the user interface for both customer and administrative functionality.

**Bulky.DataAccess** is responsible for database access and contains the Entity Framework Core DbContext and database migrations. It provides the application's connection to SQL Server and handles persistence of application data.

**Bulky.Models** contains the application's entity models and ViewModels used throughout the different layers of the application.

**bulkyBook.Business** contains the business logic of the application and provides service and interface implementations that separate business operations from the presentation layer and data-access layer.

**BulkyBook.Utitlty** contains shared utility functionality, constants, roles, status values, and other common settings used throughout the application.

This architecture keeps the different responsibilities separated and makes the project easier to maintain, understand, and extend.

## Tech Stack

BulkyBook is built using ASP.NET Core MVC with **.NET 10** as the target framework. Entity Framework Core is used as the ORM for communicating with the SQL Server database and managing database migrations. ASP.NET Core Identity is used for authentication and role-based authorization. The user interface is built using HTML, CSS, Bootstrap, JavaScript, and jQuery, with DataTables used for interactive data management tables. Stripe is used for online payment processing, while Mailjet is used for email services. Git and GitHub are used for source control and project version management.

## Getting Started

### Prerequisites

Before running BulkyBook, make sure that the .NET 10 SDK, SQL Server, and Visual Studio 2022 or a compatible development environment are installed on your machine. Git is also recommended for cloning and managing the repository.

### Setup

Clone the repository from GitHub and open the solution in Visual Studio.

```bash
git clone https://github.com/omarqutmesh/BulkyBook.git
cd BulkyBook
```

Open the solution and make sure the following projects are available:

```text
BulkyBookWeb
Bulky.DataAccess
Bulky.Models
bulkyBook.Business
BulkyBook.Utitlty
```

Set **BulkyBookWeb** as the startup project.

## Database Configuration

BulkyBook uses SQL Server with Entity Framework Core. The database connection string is configured in the `appsettings.json` file inside the `BulkyBookWeb` project.

A typical local SQL Server connection string can be configured as follows:

```json
"ConnectionStrings": {
  "SQLConnection": "Server=.;Database=BulkyBook;Trusted_Connection=True;TrustServerCertificate=True"
}
```

The server name should be changed if your local SQL Server instance uses a different configuration.

## Database Migrations

The project uses Entity Framework Core migrations to create and update the database schema. After configuring the SQL Server connection string, the existing migrations can be applied through the Visual Studio Package Manager Console using:

```powershell
Update-Database
```

Make sure that the correct startup project and migration project are selected before applying the database migration.

## Running the Application

After configuring the database and applying the migrations, set `BulkyBookWeb` as the startup project in Visual Studio and run the application. The application will launch in the browser and provide access to the customer storefront and authorized administrative functionality.

## Security

Sensitive information such as Stripe API keys, Mailjet API credentials, database passwords, and other secrets should not be stored directly in source control. Configuration values containing secrets should be managed using environment variables, User Secrets, or another secure configuration mechanism. Real API keys and passwords should never be committed to the GitHub repository.

## Repository

GitHub: https://github.com/omarqutmesh/BulkyBook

## License

This project was developed for educational and practical software development purposes.
