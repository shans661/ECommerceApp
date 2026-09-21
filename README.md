# ECommerceApp 

Overview

- API: ASP.NET Core Web API (backend) that exposes endpoints for products, categories, users, orders, authentication, and other e‑commerce operations.
- UI: Angular application (frontend) that consumes the API and provides a shopping UI for customers and an admin interface for management.

This README provides a concise overview and common steps to build and run the app locally. For the full guided walkthrough and explanations of design decisions, follow the Udemy course link above.

Prerequisites

- .NET SDK (6.0 or later recommended) — install from https://dotnet.microsoft.com/
- Node.js (16+ recommended) and npm — install from https://nodejs.org/
- Angular CLI (optional but helpful): `npm install -g @angular/cli`
- A SQL-compatible database (SQL Server, LocalDB, or another provider used by the project). Adjust the connection string in the API configuration as needed.
- (Optional) EF Core tools if the project uses Entity Framework Core migrations: `dotnet tool install --global dotnet-ef`

Repository layout (typical)

The exact layout may vary; look for the API project (.csproj) and the Angular project (package.json). Common names/locations:

- /src or /API — ASP.NET Core Web API project (contains *.csproj files and appsettings.*.json)
- /ClientApp or /Client — Angular application (contains package.json, src/, angular.json)

If you are unsure which folders are which, search for a `.csproj` file for the API and `package.json` for the Angular app.

Build & run (local development)

1. Clone the repo

   git clone https://github.com/shans661/ECommerceApp.git
   cd ECommerceApp

2. Backend (API)

   - Change into the folder that contains the API project (*.csproj). For example:
     cd ./src/Api

   - Restore and build dependencies:
     dotnet restore
     dotnet build

   - Configure the database connection:
     Open `appsettings.Development.json` or `appsettings.json` in the API project and set the `ConnectionStrings:DefaultConnection` (or similarly named key) to point to your database.

   - Apply database migrations (if the project uses EF Core migrations):
     dotnet ef database update

   - Run the API:
     dotnet run

   The API typically listens on ports such as `https://localhost:5001` and/or `http://localhost:5000`. Check the output or `launchSettings.json` for exact URLs.

3. Frontend (Angular)

   - Change into the Angular project folder (the folder that contains `package.json`):
     cd ../ClientApp

   - Install Node packages:
     npm install

   - Run the development server:
     ng serve --open
     or
     npm start

   By default Angular serves on http://localhost:4200. The frontend should be configured to call the backend API URL (update environment files under `src/environments` if needed).

Run both together

- Start the API first, then start the Angular dev server. Ensure the frontend environment is pointing at the API base URL.
- If you encounter CORS errors, enable CORS in the API startup configuration or use a proxy configuration in the Angular app.

Production build (summary)

- Backend: `dotnet publish -c Release -o ./publish`
- Frontend: `ng build --configuration production` (outputs `dist/`)

You can either host the Angular `dist/` output separately (nginx, static hosting) or configure the ASP.NET Core app to serve the compiled Angular files from `wwwroot` by copying the `dist/` content into the API's `wwwroot` (if the project is already wired for that).

Configuration and environment

- Secrets and connection strings should not be committed to the repo. Use `appsettings.Development.json` for local development and environment variables or a secrets manager for production.
- Ports, API base URL, and database provider can typically be configured in `appsettings.*.json` or Angular environment files (`src/environments/environment.ts`).

Troubleshooting

- Database connection errors: verify the connection string and that the database server is reachable. Ensure migrations were applied if using EF Core.
- CORS and authentication errors: confirm the API allows requests from the frontend origin or configure a proxy during development.
- Missing Node modules or build failures: run `npm install` in the Angular folder and check for version compatibility (Node / npm).

Where to find more detailed instructions

This README is a concise summary. The full step-by-step project walkthrough and explanations are available in the Udemy course linked above (https://www.udemy.com/course/learn-to-build-an-e-commerce-app-with-net-core-and-angular/learn/lecture/45148779?start=0#overview). Follow the course for guided configuration, explanations of each component, and instructor-provided source details.

Contributing

If you want to contribute, please open issues or pull requests. Describe the changes and include reproducible steps and tests where appropriate.

License

If you want to add a license, include a LICENSE file in the repo. Otherwise, consider adding a short license note here.
