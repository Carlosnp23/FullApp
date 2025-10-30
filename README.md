🚀 FullApp (Backend)

🔑 Local Secrets Configuration (Development)

To enable the application to run successfully in the Development environment, you must securely configure sensitive keys and the database connection string using the .NET User Secrets tool.

In Visual Studio, right-click on the FullApp.Api project and select "Manage User Secrets".

Configure the required keys, specifically ConnectionStrings:Default and JWT:Key, using your local development values (refer to the secrets.json example provided during initial setup).


❗️ Security Note: Under no circumstances should sensitive information or secrets be committed to the Git repository. These values are stored safely outside the project folder structure.