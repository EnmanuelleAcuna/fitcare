# 📏 Lógica de Negocio — Mantenimiento de Tipos de Medida (`fitcare`)

## 1. Objetivo
El módulo de **mantenimiento de tipos de medida** permite administrar las unidades o categorías de medida utilizadas en la aplicación (por ejemplo, peso, altura, porcentaje de grasa corporal, etc.).  
Las operaciones incluyen **listar, agregar, editar, eliminar y consultar** los tipos de medida tanto de forma tradicional (vistas Razor) como mediante **operaciones AJAX** para integración dinámica en la interfaz.

---

## 2. Entidad Principal

### 🧩 `TipoMedida`
Representa un tipo de medida física o corporal que puede ser asociada a otras entidades del sistema (como mediciones o rutinas).

**Atributos:**
- `Id` (Guid): Identificador único.  
- `Codigo` (string): Código interno para referencia.  
- `Nombre` (string): Nombre de la medida (por ejemplo, "Peso", "Altura").  
- `Estado` (bool): Indica si está activo o inactivo.  

---

## 3. Flujo General del Mantenimiento

### 3.1 Listado
- Acción: `Listar()`  
  - Carga la vista principal.  
- Acción: `ObtenerTiposMedida()`  
  - Recupera todos los tipos de medida mediante `_tiposMedida.ReadAllAsync()`.  
  - Convierte las entidades a `TipoMedidaViewModel`.  
  - Devuelve los resultados como JSON para integrarse con **DataTables** o componentes dinámicos.

---

### 3.2 Creación (Agregar)
Existen dos métodos de creación: tradicional y mediante AJAX.

#### 🧾 Método tradicional
```csharp
[HttpPost]
[ValidateAntiForgeryToken]
public async Task<ActionResult> Agregar(AgregarTipoMedidaViewModel modelo)
```
- Valida los datos (`ModelState.IsValid`).  
- Si son válidos, crea la entidad con `modelo.Entidad()` y la guarda con `_tiposMedida.CreateAsync()`.  
- Redirige a la lista de tipos de medida.

#### ⚙️ Método AJAX
```csharp
[HttpPost]
public async Task<JsonResult> AgregarAjax([FromBody] AgregarTipoMedidaViewModel modelo)
```
- Permite registrar nuevos tipos sin recargar la página.  
- Retorna un objeto JSON con `success: true` o `false` y mensajes de error en caso de validación fallida.  

---

### 3.3 Edición
También soporta edición tradicional y vía AJAX.

#### 🧾 Método tradicional
- Recupera el registro existente con `ReadByIdAsync(id)`.  
- Carga los datos en `EditarTipoMedidaViewModel`.  
- Al enviar el formulario, si el modelo es válido:  
  ```csharp
  await _tiposMedida.UpdateAsync(modelo.Entidad(), GetCurrentUser());
  ```

#### ⚙️ Método AJAX
- Similar al método tradicional, pero devuelve la respuesta como JSON con éxito o mensajes de error.  
- Ideal para paneles o interfaces SPA (Single Page Application).

---

### 3.4 Eliminación
#### 🧾 Método tradicional
- Carga la vista de confirmación con `EliminarTipoMedidaViewModel`.  
- Si el modelo es válido, elimina la entidad con:
  ```csharp
  await _tiposMedida.DeleteAsync(new Guid(modelo.IdTipoMedida));
  ```

#### ⚙️ Método AJAX
- Permite eliminar registros de forma asíncrona y devuelve respuesta JSON.  
- Incluye manejo de errores por dependencias (por ejemplo, si el tipo está siendo usado en otra entidad).

---

### 3.5 Consulta Detallada
- Acción: `Detalle(string id)`  
  - Recupera un `TipoMedida` por su identificador.  
  - Devuelve un objeto JSON con los datos estructurados en un `TipoMedidaViewModel`.

---

## 4. Componentes Técnicos Involucrados

| Capa | Componente | Descripción |
|------|-------------|-------------|
| **Controlador** | `TiposMedidaController` | Coordina operaciones CRUD y soporta tanto vistas como peticiones AJAX. |
| **Lógica de negocio / Repositorio** | `TiposMedida` | Implementa las operaciones CRUD con Entity Framework Core. |
| **Modelo de datos (Entidad)** | `TipoMedida` | Representa la tabla `fitcare.TiposMedida` en la base de datos. |
| **Modelo de vista (ViewModels)** | `AgregarTipoMedidaViewModel`, `EditarTipoMedidaViewModel`, `EliminarTipoMedidaViewModel`, `TipoMedidaViewModel` | Facilitan la interacción entre la vista y el modelo con validaciones. |
| **Configuración** | `appsettings.json` | Define la conexión a la base de datos y parámetros de registro de logs. |

---

## 5. Reglas de Negocio Clave

1. **Validación obligatoria de campos:** Código y nombre son requeridos y no deben exceder los 50 caracteres.  
2. **Los tipos de medida pueden activarse o inactivarse** mediante el campo `Estado`.  
3. **Auditoría:**  
   - `CreatedBy` / `DateCreated` al crear.  
   - `UpdatedBy` / `DateUpdated` al editar.  
4. **Operaciones seguras:**  
   - Requiere autenticación (`[Authorize]`).  
   - Los métodos con modificación de datos utilizan `[ValidateAntiForgeryToken]`.  
5. **Soporte para AJAX:** permite operaciones CRUD dinámicas sin recargar la página.

---

## 6. Arquitectura General
El módulo está construido bajo el patrón **MVC** (Model–View–Controller), donde:

- **Modelo:** contiene la entidad `TipoMedida` y sus ViewModels asociados.  
- **Controlador:** maneja las acciones y valida los modelos.  
- **Vista:** muestra las tablas y formularios interactivos.  
- **Lógica de negocio:** implementada en `TiposMedida.cs` mediante la interfaz genérica `IBaseCore<T>`.  

---

## 7. Diagrama Simplificado de Flujo (CRUD)

```mermaid
flowchart TD
    A[Usuario autenticado] --> B{Operación}
    B -->|Listar| C[ObtenerTiposMedida() → JSON]
    B -->|Agregar| D[Agregar() / AgregarAjax()]
    B -->|Editar| E[Editar() / EditarAjax()]
    B -->|Eliminar| F[Eliminar() / EliminarAjax()]

    D --> G[Validar modelo]
    G -->|Válido| H[Guardar en BD]
    G -->|No válido| I[Mostrar errores]

    E --> J[Leer por ID]
    J --> K[Actualizar campos]
    K --> L[Guardar cambios]

    F --> M[Confirmar eliminación]
    M --> N[Eliminar registro de BD]
```

---

**Autor:** Generado automáticamente por ChatGPT — Análisis de archivos `fitcare`  
**Fecha:** 2025
