# 🏋️‍♂️ Lógica de Negocio — Mantenimiento de Ejercicios (`fitcare`)

## 1. Objetivo
El módulo de **mantenimiento de ejercicios** permite administrar los ejercicios disponibles en la aplicación, junto con sus **tipos de ejercicio**.  
Las operaciones principales son: crear, listar, editar y eliminar ejercicios y tipos de ejercicio, todo bajo un esquema de seguridad (usuarios autenticados).

---

## 2. Entidades Principales

### 🧩 `Ejercicio`
Representa un ejercicio individual dentro del sistema.

**Atributos:**
- `Id` (Guid): Identificador único.  
- `Codigo` (string): Código interno del ejercicio.  
- `Nombre` (string): Nombre del ejercicio.  
- `Estado` (bool): Indica si el ejercicio está activo.  
- `IdTipoEjercicio` (Guid): Llave foránea hacia `TipoEjercicio`.  
- `TipoEjercicio` (objeto): Relación con la entidad `TipoEjercicio`.

---

### 🗂️ `TipoEjercicio`
Clasifica los ejercicios por tipo (por ejemplo, fuerza, cardio, estiramiento).

**Atributos:**
- `Id` (Guid): Identificador único.  
- `Codigo` (string): Código de tipo.  
- `Nombre` (string): Nombre del tipo.  
- `Estado` (bool): Activo/Inactivo.  
- `Ejercicios`: Colección de ejercicios asociados.

---

## 3. Flujo General del Mantenimiento

### 3.1 Listado
- **EjerciciosController → ListarEjercicios()**
  - Consulta todos los ejercicios (`_ejercicios.ReadAllAsync()`).
  - Mapea las entidades a `EjercicioViewModel`.
  - Muestra la lista en la vista.

- **Tipos de Ejercicio**
  - Similar proceso en `ListarTiposEjercicio()` con `_tiposEjercicio.ReadAllAsync()`.

---

### 3.2 Creación (Agregar)
- Vista: formulario de creación (`AgregarEjercicioViewModel`).
- Validación de datos obligatorios (código, nombre, tipo de ejercicio).
- Lógica:
  ```csharp
  await _ejercicios.CreateAsync(modelo.Entidad(), GetCurrentUser());
  ```
- El método `CreateAsync`:
  - Valida que el tipo de ejercicio exista.
  - Asigna usuario creador y fecha.
  - Inserta el nuevo ejercicio en la base de datos (`_dbContext.AddAsync()`).

---

### 3.3 Edición
- Recupera la entidad con `ReadByIdAsync(id)`.
- Muestra los valores en `EditarEjercicioViewModel`.
- Al guardar:
  - Valida el modelo.
  - Actualiza los campos (`Codigo`, `Nombre`, `Estado`, `IdTipoEjercicio`).
  - Registra usuario y fecha de modificación.
  - Guarda cambios en BD (`_dbContext.Update()`).

---

### 3.4 Eliminación
- Confirma la acción con `EliminarEjercicioViewModel`.
- Si el modelo es válido:
  ```csharp
  await _ejercicios.DeleteAsync(new Guid(modelo.Id));
  ```
- `DeleteAsync` elimina el registro físico con `_dbContext.Remove()`.

---

### 3.5 Consulta Detallada
- Acción `DetalleEjercicio(string id)` devuelve un objeto JSON (`EjercicioViewModel`) para visualización o consultas AJAX.

---

## 4. Componentes Técnicos Involucrados

| Capa | Componente | Descripción |
|------|-------------|-------------|
| **Controlador** | `EjerciciosController` | Gestiona la comunicación entre vistas y lógica de negocio. |
| **Lógica de negocio / Repositorio** | `Ejercicios`, `TiposEjercicio` | Implementan operaciones CRUD sobre `Ejercicio` y `TipoEjercicio`. |
| **Modelo de datos (Entidad)** | `Ejercicio`, `TipoEjercicio` | Representan las tablas `fitcare.Ejercicios` y `fitcare.TIPOSEJERCICIO`. |
| **Modelo de vista (ViewModels)** | `AgregarEjercicioViewModel`, `EditarEjercicioViewModel`, etc. | Adaptan los datos de las entidades a la vista con validaciones. |
| **Configuración** | `appsettings.json` | Contiene la cadena de conexión a la base de datos Azure SQL y niveles de logging. |

---

## 5. Reglas de Negocio Clave

1. **Cada ejercicio debe estar asociado a un tipo de ejercicio existente.**  
   Si no existe, se lanza una excepción.

2. **Validación de datos requerida** antes de persistir información (`ModelState.IsValid`).

3. **Control de auditoría:**  
   - `CreatedBy` / `DateCreated` al crear.  
   - `UpdatedBy` / `DateUpdated` al modificar.

4. **Eliminación física:**  
   - Los registros se eliminan definitivamente de la BD (no se usa un campo lógico).

---

## 6. Arquitectura General
El flujo se basa en una **arquitectura MVC**:
- **Modelos:** entidades y viewmodels.  
- **Controladores:** gestionan las solicitudes HTTP y aplican reglas de negocio.  
- **Vistas:** renderizan la información al usuario.  
- **BaseCore Interface:** abstrae operaciones CRUD genéricas.

---

## 7. Diagrama Simplificado de Flujo (CRUD)

```mermaid
flowchart TD
    A[Usuario autenticado] --> B{Operación}
    B -->|Listar| C[ListarEjercicios()]
    B -->|Agregar| D[AgregarEjercicio()]
    B -->|Editar| E[EditarEjercicio()]
    B -->|Eliminar| F[EliminarEjercicio()]

    D --> G[Validar tipo de ejercicio]
    G -->|Válido| H[Guardar en BD]
    G -->|No válido| I[Error: Tipo no encontrado]

    E --> J[Leer por ID]
    J --> K[Actualizar campos]
    K --> L[Guardar cambios]

    F --> M[Confirmar eliminación]
    M --> N[Eliminar registro de BD]
```

---

**Autor:** Generado automáticamente por ChatGPT — Análisis de archivos `fitcare`  
**Fecha:** 2025
