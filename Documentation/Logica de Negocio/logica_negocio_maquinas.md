# ⚙️ Lógica de Negocio — Mantenimiento de Máquinas (`fitcare`)

## 1. Objetivo
El módulo de **mantenimiento de máquinas** permite gestionar las máquinas del gimnasio, incluyendo su información técnica, código de activo, fecha de adquisición y clasificación por tipo de máquina.  
Las operaciones principales son: **listar, agregar, editar y eliminar** tanto máquinas como tipos de máquina, bajo un esquema autenticado.

---

## 2. Entidades Principales

### 🧩 `Maquina`
Representa una máquina física utilizada en el gimnasio.

**Atributos:**
- `Id` (Guid): Identificador único.  
- `Codigo` (string): Código interno del sistema.  
- `Nombre` (string): Nombre descriptivo de la máquina.  
- `CodigoActivo` (string): Código del activo físico (número de inventario).  
- `Estado` (bool): Indica si la máquina está activa o fuera de servicio.  
- `FechaAdquisicion` (DateTime): Fecha de adquisición.  
- `IdTipoMaquina` (Guid): Llave foránea hacia `TipoMaquina`.  
- `TipoMaquina` (objeto): Relación con la entidad `TipoMaquina`.

---

### 🗂️ `TipoMaquina`
Clasifica las máquinas por tipo (por ejemplo, fuerza, cardio, resistencia).

**Atributos:**
- `Id` (Guid): Identificador único.  
- `Codigo` (string): Código de tipo.  
- `Nombre` (string): Nombre del tipo.  
- `Estado` (bool): Activo/Inactivo.  
- `Maquinas`: Colección de máquinas asociadas.

---

## 3. Flujo General del Mantenimiento

### 3.1 Listado
- **Controlador:** `MaquinasController → ListarMaquinas()`  
  - Recupera todas las máquinas (`_maquinas.ReadAllAsync()`).  
  - Incluye información del tipo de máquina (`Include(m => m.TipoMaquina)`).  
  - Mapea los resultados a `MaquinaViewModel`.  
  - Envía la lista a la vista para su renderización.

- **Listado de Tipos de Máquina:**  
  - `ListarTiposMaquina()` obtiene todos los tipos registrados y los adapta con `TipoMaquinaViewModel`.

---

### 3.2 Creación (Agregar)
- Vista usa `AgregarMaquinaViewModel` con campos validados.  
- Al enviar el formulario:
  ```csharp
  await _maquinas.CreateAsync(modeloVista.Entidad(), CurrentUser);
  ```
- El método `CreateAsync`:
  - Valida que el tipo de máquina exista.  
  - Asigna usuario y fecha de creación.  
  - Inserta el nuevo registro en la base de datos (`_dbContext.AddAsync()`).

---

### 3.3 Edición
- Recupera la entidad mediante `ReadByIdAsync(id)`.  
- Llena el modelo `EditarMaquinaViewModel` para el formulario.  
- Al guardar:
  - Valida el modelo (`ModelState.IsValid`).  
  - Actualiza atributos como `Codigo`, `Nombre`, `CodigoActivo`, `Estado`, `FechaAdquisicion`, y `IdTipoMaquina`.  
  - Registra usuario y fecha de actualización.  
  - Persiste los cambios (`_dbContext.Update()`).

---

### 3.4 Eliminación
- Confirma con el modelo `EliminarMaquinaViewModel`.  
- Si el modelo es válido:
  ```csharp
  await _maquinas.DeleteAsync(new Guid(modelo.Id));
  ```
- Se elimina físicamente el registro (`_dbContext.Remove()`).

---

### 3.5 Consulta Detallada
- `DetalleMaquina(string id)` devuelve un `MaquinaViewModel` como objeto JSON, útil para consultas dinámicas o AJAX.

---

## 4. Componentes Técnicos Involucrados

| Capa | Componente | Descripción |
|------|-------------|-------------|
| **Controlador** | `MaquinasController` | Coordina las solicitudes de vista y operaciones CRUD de las máquinas y sus tipos. |
| **Lógica de negocio / Repositorio** | `Maquinas`, `TiposMaquina` | Implementan operaciones CRUD con validaciones y relaciones. |
| **Modelo de datos (Entidad)** | `Maquina`, `TipoMaquina` | Representan las tablas `fitcare.MAQUINAS` y `fitcare.TIPOSMAQUINA`. |
| **Modelo de vista (ViewModels)** | `AgregarMaquinaViewModel`, `EditarMaquinaViewModel`, `EliminarMaquinaViewModel`, etc. | Intermedian entre la vista y las entidades, aplicando validaciones. |
| **Configuración** | `appsettings.json` | Contiene la cadena de conexión a la base de datos Azure SQL. |

---

## 5. Reglas de Negocio Clave

1. **Toda máquina debe asociarse a un tipo de máquina existente.**  
   Si el tipo no se encuentra, se lanza una excepción.

2. **Validación de campos obligatorios** antes de crear o editar (`ModelState.IsValid`).

3. **Auditoría:**  
   - `CreatedBy` / `DateCreated` en creación.  
   - `UpdatedBy` / `DateUpdated` en actualización.

4. **Eliminación física:**  
   - Los registros se eliminan directamente de la base de datos.

---

## 6. Arquitectura General
El módulo sigue el patrón **MVC (Model-View-Controller)**:
- **Modelos:** entidades y viewmodels.  
- **Controladores:** gestionan solicitudes y reglas de negocio.  
- **Vistas:** muestran la información al usuario.  
- **Servicios base (`IBaseCore`)**: definen operaciones genéricas CRUD reutilizables.

---

## 7. Diagrama Simplificado de Flujo (CRUD)

```mermaid
flowchart TD
    A[Usuario autenticado] --> B{Operación}
    B -->|Listar| C[ListarMaquinas()]
    B -->|Agregar| D[AgregarMaquina()]
    B -->|Editar| E[EditarMaquina()]
    B -->|Eliminar| F[EliminarMaquina()]

    D --> G[Validar tipo de máquina]
    G -->|Válido| H[Guardar en BD]
    G -->|No válido| I[Error: Tipo no encontrado]

    E --> J[Leer por ID]
    J --> K[Actualizar atributos]
    K --> L[Guardar cambios]

    F --> M[Confirmar eliminación]
    M --> N[Eliminar registro de BD]
```

---

**Autor:** Generado automáticamente por ChatGPT — Análisis de archivos `fitcare`  
**Fecha:** 2025
