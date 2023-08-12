# fitcare
Plataforma para la gestión de gimnasios.

## Proceso de planificación y seguimiento de trabajo: Básico
https://docs.microsoft.com/en-us/azure/devops/boards/get-started/plan-track-work?view=azure-devops&tabs=basic-process&source=docs

## Arquitectura de la aplicación: Monolítica
- Se hace uso de Clean architecture para la solución: https://docs.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/common-web-application-architectures#clean-architecture
- Front end:
  - ASP.Net MVC.
  - JavaScript
  - jQuery
  - Razor
- El back end es implementado con librerías de clases:
  - Domain: Contiene las entidades principales, las interfaces (contratos) e implementaciones para managers o services.
  - Infrastructure:
    - Identity: proveedor de autenticación y autorización local, conectado a la base de datos SQLy capacidad de enviar correos electrónicos.
    - Data: Para acceso a datos.
  - Resumen:
    - Frontend
    - Web (MVC)
	- Backend
	- Domain (Entities, interfaces, Logic) (Class library)
	- Data Persistence (EF Core/SQL Server) (Class library)
	- Identity (EF Core/SQL Server) (Class Library)

## Getting Started
1.	[Installation process](#installation-process)
2.	[Software dependencies](#software-dependencies)
3.	[Latest releases](#latests-releases)
4.	[API references](#api-references)
5.	[Contribute](#contribute)

<h2 id="installation-process">Installation proccess</h3>

- Compilar: dotnet build Source/ --no-restore
- Build and Test: dotnet run Source/Web --no-build

<h2 id="software-dependencies">Software dependencies</h2>

- dotnet restore Source/ // Restaurar las dependecias de un proyecto

<h2 id="latests-releases">Latests releases:</h2>

- Actualmente la versión inicial es el MVP, el cual se encuentra en pruebas: https://gimnasios.azurewebsites.net

<h2 id="api=references">API references</h2>
TODO: Add your code references.

<h2 id="contribute">Contribute</h2>

- Format code:
  ```
  dotnet format --severity info
  ```
- LibMan: Administrador de paquetes del lado del cliente
  ```
  dotnet tool install -g Microsoft.Web.LibraryManager.Cli // Instalar
  Estando en carpeta raiz del repositorio: cd  Source/Web
  libman restore // Restaurar los paquetes que se encuentran en el archivo de configuracion libman.json en Source/Web/
  ```
- Instalar Entity Framework Core tools:
  ```
  dotnet tool install -g dotnet-ef
  dotnet-ef ó dotnet ef // Verificar dotnet ef, SOLO DESDE LA TERMINAL DE VSCODE O VISUAL STUDIO
  ```
- Crear modelo de Entity Framework a partir de la base de datos (database-first):
   ```
   Estando en carpeta raiz del repositorio: cd  Source

   dotnet ef dbcontext scaffold  Microsoft.EntityFrameworkCore.SqlServer --context fitcare_DB_Context --schema fitcare --output-dir ../Data/EntityFramework --namespace fitcare.Data.EntityFramework --no-pluralize --no-build

   Nuevo:
   dotnet ef dbcontext scaffold "Server=tcp:sql-svr-arenal.database.windows.net, 1433;Initial Catalog=db-arenal;;Persist Security Info=False;User Id=arenal;Password=6ABD70BEF057D69F529BE5DBA85DE2A3.;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;" Microsoft.EntityFrameworkCore.SqlServer --context fitcare_DB_Context --schema fitcare --output-dir Models/DataAccess/EntityFramework --namespace fitcare.Models.DataAccess.EntityFramework --no-pluralize --no-build
   ```
- Añadir migracion para Identity (No es necesario este paso ya que la migración se encuentra creada, ir al siguiente punto):
  ```
  Estando en carpeta raiz del repositorio: cd  Source/Identity

  dotnet ef migrations add "001" -o Data/Migrations --no-build
  ```
- Actualizar base de datos
  ```
  Estando en carpeta raiz del repositorio: cd  Source/Web
  dotnet ef database update --context IdentityDBContext
  ```
- Instalar dotnet-aspnet-codegenerator:
  ```
  dotnet tool install -g dotnet-aspnet-codegenerator --version 6.0.11
  dotnet-aspnet-codegenerator -h // Consultar ayuda de herramienta
  ```
- Generar las vistas y codigo para las vistas deseadas:
  ```
  dotnet aspnet-codegenerator identity --dbContext PCG.Data.ApplicationDbContext --files "Account.ForgotPassword;Account.ForgotPasswordConfirmation;Account.Login;Account.Logout;Account.ResetPassword;Account.ResetPasswordConfirmation"
  ```
- Orden preferido de métodos
  - Agregar
  - Actualizar/Editar/modificar
  - Elliminar (Opcional)
  - Alguna otra acción
  - ObtenerTodos
  - ObtenerTodosPorParametro
  - ObtenerPorId
  - ObtenerPorParametro
<hr />
