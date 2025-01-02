# Project File Documentation

This document explains the purpose of each important file and directory in the **CruiseShip** project. The project is organized into various areas, controllers, views, and models to ensure maintainability and scalability.

## Root Directory

- **appsettings.json**: This file contains configuration settings for the application, such as database connection strings, application secrets, and other settings that the app can use at runtime.
  
- **CruiseShip.csproj**: This is the project file for the ASP.NET Core MVC application. It defines the project's structure, references, and dependencies.
  
- **CruiseShip.sln**: This is the solution file for Visual Studio. It holds information about the project's structure and is used to open the project in Visual Studio.

- **Program.cs**: The entry point for the application. It sets up and configures the ASP.NET Core host and the application's services.

---

## Areas Directory

The **Areas** directory is used to organize the application into different sections, each with its own set of controllers, views, and models. This is helpful for large applications.

### Admin Area

The **Admin** area contains controllers, views, and models that are specific to the admin functionality of the application.

- **Controllers/FacilityController.cs**: This controller handles CRUD operations for the `Facility` model, including creating, editing, and deleting facilities.
  
- **Controllers/RoomController.cs**: This controller handles CRUD operations for the `Room` model, including creating, editing, and deleting rooms.

- **Views/Facility**: This folder contains the views related to `Facility` operations:
  - **Create.cshtml**: View for creating a new facility.
  - **Delete.cshtml**: View for deleting a facility.
  - **Edit.cshtml**: View for editing a facility.
  - **Index.cshtml**: View for displaying a list of facilities.

- **Views/Room**: This folder contains the views related to `Room` operations:
  - **Create.cshtml**: View for creating a new room.
  - **Delete.cshtml**: View for deleting a room.
  - **Edit.cshtml**: View for editing a room.
  - **Index.cshtml**: View for displaying a list of rooms.

- **_ViewImports.cshtml**: Contains common imports for the Admin area views (e.g., shared namespaces and directives).
  
- **_ViewStart.cshtml**: Defines the layout for views in the Admin area, including any shared view settings.

### Identity Area

The **Identity** area handles authentication and user management features such as login, registration, and password management.

- **Pages/Account**: This folder contains pages related to user account management.
  - **ConfirmEmail.cshtml**: Page for confirming the user's email address.
  - **Login.cshtml**: Login page for users.
  - **Register.cshtml**: Registration page for new users.
  - **Manage**: This folder contains pages for managing user profiles, passwords, and emails.
    - **ChangePassword.cshtml**: Page for changing the user's password.
    - **Email.cshtml**: Page for managing the user's email.
    - **PersonalData.cshtml**: Page for viewing and editing personal data.
    - **SetPassword.cshtml**: Page for setting a new password after registration.
  
- **_ViewImports.cshtml**: Contains common imports for the Identity views.
  
- **_ViewStart.cshtml**: Defines the layout for Identity views.

### Voyager Area

The **Voyager** area contains views and controllers for the main functionality accessible to the users (voyagers).

- **Controllers/HomeController.cs**: This controller manages the home page and other user-facing pages.
  
- **Views/Home**: This folder contains views related to the voyager's homepage and other related pages:
  - **Index.cshtml**: The main landing page for voyagers.
  - **Privacy.cshtml**: A page that displays privacy information.

- **_ViewImports.cshtml**: Contains common imports for the Voyager views.

- **_ViewStart.cshtml**: Defines the layout for the Voyager views.

---

## Data Directory

The **Data** directory contains files related to database operations, including models, migrations, and repositories.

- **ApplicationDbContext.cs**: The database context class that interacts with the database and defines the DbSets for the models (e.g., `Facility`, `Room`).
  
- **Migrations/**: This folder contains the migration files for updating the database schema based on model changes. Each migration file represents a change in the database schema.

- **Repository/**: This folder contains the repository classes that abstract database operations for specific models.
  - **FacilityRepository.cs**: Repository class for handling CRUD operations related to the `Facility` model.
  - **RoomRepository.cs**: Repository class for handling CRUD operations related to the `Room` model.
  - **UnitOfWork.cs**: Implements the Unit of Work pattern, ensuring that all changes to the database are committed in a single transaction.
  - **IRepository/IUnitOfWork.cs**: Interface definitions for the repository and unit of work patterns.

---

## Models Directory

The **Models** directory contains the classes that represent the data structures in the application.

- **Bill.cs**: Model representing the billing information for users.
  
- **Booking.cs**: Model representing booking information for voyagers.
  
- **ErrorViewModel.cs**: A view model for error pages, typically used to display error messages.
  
- **Facility.cs**: Model representing the facility data.
  
- **Room.cs**: Model representing room data.

- **ViewModels/FacilityVM.cs**: A ViewModel for handling the data that will be passed to views related to `Facility`.

---

## Views Directory

The **Views** directory contains the Razor views that render HTML to the user.

- **Shared/**: Contains shared views, such as layout pages, partial views, and validation scripts.
  - **_Layout.cshtml**: The main layout view used across the application.
  - **_LoginPartial.cshtml**: Partial view for displaying login information.
  - **_Notification.cshtml**: Partial view for displaying notifications.

- **_ViewImports.cshtml**: Defines common imports for views, such as namespaces and Razor directives.

- **_ViewStart.cshtml**: Configures the layout and other settings for views globally.

---

## wwwroot Directory

The **wwwroot** directory contains static files that are served directly to the client, such as images, stylesheets, and JavaScript files.

- **css/**: Contains CSS files for styling the website.
  - **ShipWallpaper.jpg**: An image used as the wallpaper for the site.
  - **site.css**: The main CSS file for styling the site.

- **js/**: Contains JavaScript files for adding interactivity to the site.
  - **site.js**: Custom JavaScript code for the application.

- **lib/**: Contains third-party libraries such as Bootstrap and jQuery.
  - **bootstrap/**: Contains Bootstrap CSS and JS files for responsive design and UI components.
  - **jquery/**: Contains jQuery files for DOM manipulation and AJAX operations.
  - **jquery-validation/**: Contains jQuery validation scripts for form validation.
  - **jquery-validation-unobtrusive/**: Contains scripts for unobtrusive validation using jQuery.

---

This document provides an overview of the purpose of the key files in the **CruiseShip** project. The organization of files into areas, repositories, and models helps maintain a clean and modular codebase, making it easier to scale and maintain the application.
