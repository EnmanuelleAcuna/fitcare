# Technical notes

## Programación orientada a objetos
- Los datos guardados en una base de datos no deben afectar la forma de los datos definidos en la aplicación (los modelos), por ejemplo: Si existe en la bd una tabla  lamada Usuarios y esta tabla contiene un campo que hace referencia al tipo de usuario, llamada IdTipoUsuario, no quiere decir que en la programación orientada a objetos el objeto Usuario deba tener una propiedad llamada IdTipoUsuario, sino que tambien va a existir un objeto de tipo TipoUsuario, entonces al objeto Usuario se le puede agregar como propiedad un objeto de tipo TipoUsuario, para hacer referencia al tipo de usuario que tiene asignado el objeto Usuario y poder acceder a las propiedades de este tipo, como su nombre, id y demás propiedades que tenga el objeto tipo usuario.
- Si uno instancia una clase, es un objeto único, o sea, Maquina = Maquina, si es una lista, entonces si es plural, ej: ListaMaquinas o Maquinas, para el nombre de la clase, esta debe ser siempre singular, porque se refiere a un único objeto, por ejmplo, tenemos una tabla donde se almacenan las máquinas, esas si es plural, pero cada fila es una
máquina, por lo tanto la entidad va a almacenar una máquina a la vez, por eso es singular.
- En MySQL no se utiliza GO para ir a la siguiente sentencia SQL, sino que al final de lalínea se coloca ; para que al
seleccionar multiples comando estos se ejecuten uno detrás del otro.
- POO: clase
- variables -> POO: atributo, propiedad
- funciones -> POO: métodos

## Ayuda de dotnet CLI
- dotnet -h
- Se debe de estar en la carpeta del proyecto sobre el que se quiere trabajar, es decir, donde está el archivo NombreProyecto.csproj

## Construir / compilar proyecto, sin restaurar dependencias
- dotnet build --no-restore
- dotnet build no-restore --nologo --verbosity q // Construir / compilar proyecto, sin restaurar, sin información del banner de MS y sin detalle de salida, solo el resultado

## Ejecutar proyecto, sin compilar
- dotnet run --no-build

## Crear un nuevo proyecto web de tipo MVC con autenticación de cuentas individuales
- dotnet new mvc --auth Individual

## LibMan: Administrador de paquetes del lado del cliente
- dotnet tool install -g Microsoft.Web.LibraryManager.Cli // Instalar LibMan CLI globalmente
- libman init // Inicializar LibMan en el proyecto
- libman restore // Restaurar los paquetes que se encuentran en el archivo de configuracion libman.json

## Crear modelo de Entity framework a partir de la base de datos existente:
MYSQL:
dotnet ef dbcontext scaffold "Server=217.71.206.171;Uid=fitcare;Pwd=gym2020;Database=fitcare;SslMode=Preferred;TreatTinyAsBoolean=False;" Pomelo.EntityFrameworkCore.MySQL -o EntityFramework -f -c fitcare_DB_Context --no-build

SQL Server:
dotnet ef dbcontext scaffold "Server=sql-svr-arenal.database.windows.net;Uid=arenal;Pwd=6ABD70BEF057D69F529BE5DBA85DE2A3.;Database=db-arenal;" Microsoft.EntityFrameworkCore.SqlServer -o EntityFramework -f -c PCG_DB_Context --no-build --data-annotations

## Paquetes nuget necesarios para trabajar con EntityFrameworkCore
- dotnet add package Microsoft.EntityFrameworkCore.Tools
- dotnet add package Microsoft.EntityFrameworkCore.Design
- dotnet add package MySql.Data
- dotnet add package MySql.Data.EntityFrameworkCore
- Este no por que es para .Net Framework: dotnet add package MySql.Data.EntityFrameworkCore.Design

## Leer información del archivo de configuración del proyecto, como por ejemplo un parámetro definido en la sección AppSettings
- Configuration["AppSettings:ModeloNulo"]

## Null-coalescing assignment
Beginning with C# 8.0, you can use the null-coalescing assignment operator ??= to assign the value of its right-hand operand to its left-hand operand only if the left-hand operand evaluates to null.
