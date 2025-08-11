# Inventory Management Project

A clean and organized web application to manage inventory items with user authentication, role-based access, and AI-enhanced item management.

---

## Features

- **User Authentication**: Login and register using ASP.NET Identity with JWT token authentication.
- **Role-Based Access**:
  - **Admin**: Full access to add, edit, and delete categories and inventory items.
  - **Authenticated Users**: Can add, edit, and delete only their own inventory items.
  - **All Users (Authenticated or Not)**: Can view all inventory items, see details, and filter items by various fields.
- **Inventory Management**: Add, update, delete, and view inventory items with category assignments.
- **AI Enhancement**: When adding or updating items, use AI-powered suggestions to improve and complete input fields.

---

## Getting Started

### Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download)
- A supported database (configured via `appsettings.json`)
- An AI API key for enhancements (provided below)

### Installation

1. Clone the repository:

   ```bash
   git clone <repository-url>
   cd <repository-folder>

2. Add the following AI configuration to your appsettings.json:
"Groq": {
  "ApiKey": "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"
}

3. Open the Package Manager Console in Visual Studio and run the following commands to create the database schema:
   Add-Migration assessment_migration1
Update-Database

4. Run the application.



##Default Admin Credentials
Use the following to log in as an admin user:

Email: admin@example.com

Password: Admin@123

##Usage
Browse the inventory items as any user or guest.

Register and login to add your own inventory items.

Admin users can manage all categories and inventory items.

Use the AI enhancement button when adding or editing items to get smart suggestions for item details.

##Technologies Used
ASP.NET Core MVC

Entity Framework Core

ASP.NET Identity with JWT authentication

AI integration for content enhancement

##Notes
Make sure your database connection string is configured properly in appsettings.json.

Keep your AI API key secure and do not expose it publicly.

