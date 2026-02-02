# fitcare

Plataforma para la gestión de gimnasios, desarrollada con ASP.NET Core 6.0 MVC y Clean Architecture monolítica.

## Características del sistema (v1)

### Gestión de usuarios y roles
- Tres roles: Administrador, Instructor, Cliente
- Registro y administración de instructores y clientes con datos personales y ubicación geográfica (Provincia, Cantón, Distrito)
- Gestión de roles y usuarios del sistema
- Autenticación con ASP.NET Identity, recuperación de contraseña por correo electrónico

### Rutinas
- Creación y edición de rutinas asignadas por instructor a cliente
- Gestión de ejercicios dentro de la rutina (series, repeticiones, tiempo de descanso) vía AJAX
- Gestión de medidas corporales dentro de la rutina (valor, comentario, tipo de medida) vía AJAX
- Edición de encabezado de rutina (fechas, objetivo) vía AJAX
- Vista de detalle de rutina
- Filtrado de rutinas por rol (administrador ve todas, instructor ve las suyas, cliente ve las propias)

### Catálogos
- **Ejercicios**: CRUD completo con código autogenerado, clasificación por tipo de ejercicio, relación con grupos musculares y máquinas
- **Tipos de ejercicio**: CRUD completo
- **Máquinas/Equipos**: CRUD completo con código autogenerado, clasificación por tipo de máquina
- **Tipos de máquina**: CRUD completo (patrón moderno DataTables + modales AJAX)
- **Tipos de medida**: CRUD completo (patrón moderno DataTables + modales AJAX)
- **Grupos musculares**: CRUD completo

### Planes de membresía
- Catálogo de planes con nombre, duración en meses, costo y estado
- CRUD vía AJAX con DataTables y modales (patrón moderno)
- Asignación de plan al cliente al momento de registro
- Fecha de renovación de membresía por cliente
- Registro y confirmación de pagos de membresía

### Exportación a Excel
- Exportar rutina individual a Excel
- Exportar listado de rutinas a Excel (filtrado por rol)
- Exportar listado de instructores a Excel
- Exportar listado de clientes a Excel

### División territorial
- Gestión de Provincias, Cantones y Distritos (Costa Rica)
- Selects dependientes (Provincia → Cantón → Distrito) vía AJAX
- Script de carga inicial de datos

### Dashboard
- Panel principal con tarjetas de acceso rápido por rol
- Acceso directo a rutinas, clientes, instructores, catálogos, planes y pagos

### Notificaciones por correo
- Notificación al cliente cuando se le asigna una nueva rutina
- Correo de recuperación de contraseña

### Manual de usuario
- Manual de usuario en PDF accesible desde la barra de navegación

## Arquitectura

- **Patrón**: Monolítica con Clean Architecture
- **Front end**: ASP.NET MVC, Razor, JavaScript, jQuery, Bootstrap 4
- **Back end**: Entities, Interfaces, Logic (Repository pattern vía `IBaseCore<T>`)
- **Persistencia**: Entity Framework Core con SQL Server
- **Dos DbContexts** en la misma base de datos:
  1. `ApplicationDbContext` - Entidades de negocio
  2. `IdentityDBContext` - Tablas de ASP.NET Identity
- **Librerías cliente**: Font Awesome, DataTables, Select2, ClosedXML (Excel)

## Primeros pasos

### Requisitos
- .NET 6.0 SDK
- SQL Server
- Entity Framework Core tools (`dotnet tool install -g dotnet-ef`)
- LibMan CLI (`dotnet tool install -g Microsoft.Web.LibraryManager.Cli`)

### Compilar y ejecutar
```bash
dotnet restore Source/
dotnet build Source/ --no-restore
dotnet run --project Source/fitcare --no-build
```

### Restaurar librerías del cliente
```bash
cd Source/fitcare
libman restore
```

### Base de datos
```bash
# Actualizar tablas de Identity
dotnet ef database update --context IdentityDBContext --project Source/fitcare

# Agregar migración
dotnet ef migrations add "Nombre" -o Data/Migrations --project Source/fitcare --no-build
```

### Pruebas
```bash
dotnet test Source/fitcare.Tests/
```

### Formato de código
```bash
dotnet format --severity info
```

## Recomendaciones de negocio

Funcionalidades sugeridas para futuras versiones, priorizadas por valor de negocio.

### Fase 1 - Alto impacto

#### Control de asistencia
- Registro de entrada/salida (manual o con código QR)
- Historial de asistencias por cliente
- Reporte de asistencia (diaria, semanal, mensual)
- Alertas de clientes inactivos (más de 1 semana sin asistir)

#### Reportes financieros
- Ingresos por período
- Clientes morosos
- Proyección de ingresos
- Análisis de planes más vendidos

#### Dashboard financiero y de gestión
- KPIs: clientes activos vs totales, ingresos del mes, tasa de renovación, ocupación del gimnasio
- Gráficas de ingresos mensuales, nuevos clientes por mes, retención de clientes

### Fase 2 - Impacto medio

#### Seguimiento de progreso del cliente
- Gráficas de evolución de medidas (peso, IMC, grasa corporal)
- Comparación antes/después y fotos de progreso
- Marcar ejercicios completados por sesión con porcentaje de cumplimiento
- Historial de pesos levantados por ejercicio

#### Sistema de notificaciones avanzado
- Notificación de vencimiento de membresía (7 días antes)
- Recordatorio de cita con instructor
- Notificaciones dentro de la aplicación (campana de notificaciones)
- Historial de notificaciones enviadas

#### Mejoras en reportes
- Efectividad de instructores (clientes asignados, tasa de cumplimiento)
- Uso de máquinas (para planificar mantenimiento)
- Ejercicios más utilizados

### Fase 3 - Valor agregado

#### Gestión de clases grupales
- Catálogo de clases (Yoga, Spinning, CrossFit, Zumba)
- Horarios de clases (calendario semanal)
- Inscripción con límite de cupos
- Control de asistencia a clases

#### Sistema de permisos granular
- Acciones por módulo (Ver, Crear, Editar, Eliminar)
- Asignación de permisos a roles
- Middleware de autorización basado en acciones

#### Aplicación móvil para clientes
- Ver rutina asignada
- Registrar ejercicios completados
- Ver progreso con gráficas
- Reservar clases grupales
- Pagar membresía (integración con pasarela de pagos)

### Integraciones futuras
- Pasarela de pagos (Stripe, PayPal, Wompi para Costa Rica)
- Servicio de SMS (Twilio) para recordatorios
- Almacenamiento en la nube (Azure Blob, AWS S3) para fotos de progreso
- API REST para aplicación móvil
