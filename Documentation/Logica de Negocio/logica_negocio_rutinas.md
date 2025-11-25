
# 🏋️‍♂️ Lógica del Negocio: Mantenimiento de Rutinas

Este documento describe la lógica funcional y de negocio implementada en el módulo de **Rutinas** del sistema *fitcare*.  
El mantenimiento de rutinas permite crear, listar, consultar detalles y generar reportes de rutinas asignadas a los clientes por los instructores.

---

## 📁 Estructura General

El mantenimiento de rutinas está compuesto por los siguientes elementos principales:

- **Controlador:** `RutinasController.cs`
- **Capa de negocio:** `Rutinas.cs`
- **Entidades:** `Rutina`, `EjercicioRutina`, `MedidaRutina`
- **ViewModels:** `RutinasViewModels.cs`
- **Configuración:** `appsettings.json` y `Program.cs`

---

## ⚙️ Flujo General del Módulo

### 1. Listar Rutinas (`Listar`)
Obtiene las rutinas registradas para el usuario actual (cliente o instructor).
- Se consulta la base de datos mediante `_rutinas.ObtenerReporteRutinas(null, user.Id)`.
- Los resultados se transforman en una colección de `RutinaViewModel` para ser presentados en la vista.

### 2. Agregar Rutina (`Agregar`)
Permite registrar una nueva rutina asociando:
- Instructor y cliente.
- Ejercicios, medidas y objetivos.
- Fechas de inicio, fin y realización.

**Validaciones implementadas:**
- No se permite que el instructor y el cliente sean el mismo usuario.
- Se valida la existencia del instructor, cliente, ejercicios, máquinas y tipos de medida en base de datos.
- Se registra fecha y usuario de creación (`DateCreated`, `CreatedBy`).

**Flujo principal:**
1. El usuario selecciona instructor y cliente desde las listas dinámicas (`ViewBags` cargadas por `CargarViewBags()`).
2. Se completan las medidas (`MedidaRutina`) y ejercicios (`EjercicioRutina`) que forman parte de la rutina.
3. Se invoca `CreateAsync()` del servicio `Rutinas` para guardar en base de datos.
4. Se envía un **correo automático** al cliente notificando la creación de su rutina.

### 3. Detalle de Rutina (`Detalle`)
Muestra los datos completos de una rutina seleccionada:
- Instructor y cliente.
- Fechas y objetivo.
- Listado de ejercicios (nombre, máquina, series, repeticiones, descanso).
- Medidas asociadas (valor, tipo de medida, comentarios).

Usa `DetalleRutinaViewModel` para proyectar todos los datos relacionados.

### 4. Reporte de Rutinas (`Reporte`)
Permite filtrar rutinas por instructor o cliente.
- Genera una vista consolidada (`ReporteRutinaViewModel`) con cantidad de días, ejercicios y objetivo de la rutina.

---

## 🧩 Lógica de Negocio Interna (`Rutinas.cs`)

### **CreateAsync(Rutina rutina, string user)**
- Asigna metadatos (`DateCreated`, `CreatedBy`).
- Verifica la existencia de instructor y cliente.
- Itera los ejercicios (`EjercicioRutina`) y medidas (`MedidaRutina`):
  - Valida que el ejercicio, máquina y tipo de medida existan.
  - Asocia las entidades correspondientes.
  - Registra usuario y fecha de inserción.
- Guarda la rutina y todos sus detalles.

### **ReadByIdAsync(Guid id)**
- Devuelve una rutina con sus relaciones cargadas (Instructor, Cliente, Medidas, Ejercicios, Máquinas y Tipos).
- Si no existe, lanza excepción `KeyNotFoundException`.

### **ObtenerReporteRutinas(idInstructor, idCliente)**
- Consulta las rutinas según filtros.
- Incluye relaciones y devuelve la colección resultante.

---

## 🧱 Entidades Principales

### `Rutina`
Representa una rutina asignada a un cliente.  
Incluye:
- Fechas (realización, inicio, fin)
- Objetivo
- Relación con `Instructor` y `Cliente`
- Colección de `Ejercicios` y `Medidas`

### `EjercicioRutina`
Detalle que asocia un **Ejercicio** y una **Máquina** a una rutina.  
Atributos: series, repeticiones, minutos de descanso, máquina utilizada.

### `MedidaRutina`
Detalle que asocia una **medida física** con la rutina (peso, grasa corporal, etc.).  
Incluye valor, comentario y tipo de medida.

---

## 🎨 ViewModels Principales

### `RutinaViewModel`
Simplifica la visualización general de las rutinas.

### `DetalleRutinaViewModel`
Contiene toda la información detallada de una rutina, incluyendo ejercicios y medidas.

### `AgregarRutinaViewModel`
Se utiliza para crear nuevas rutinas desde el formulario.

### `ReporteRutinaViewModel`
Proporciona una vista resumida para reportes y análisis.

---

## 🔗 Relaciones entre Entidades

| Entidad Principal | Relación | Entidad Asociada | Tipo |
|--------------------|-----------|------------------|------|
| Rutina | 1 a N | EjercicioRutina | Una rutina puede tener varios ejercicios. |
| Rutina | 1 a N | MedidaRutina | Una rutina puede tener varias medidas. |
| EjercicioRutina | N a 1 | Ejercicio | Cada ejercicioRutina apunta a un ejercicio existente. |
| EjercicioRutina | N a 1 | Maquina | Cada ejercicioRutina puede usar una máquina. |
| MedidaRutina | N a 1 | TipoMedida | Cada medida corresponde a un tipo de medida física. |

---

## 📬 Notificación por Correo
Al crear una rutina, el sistema envía un correo al cliente con un enlace para visualizar su rutina, usando el servicio `IEmailSender`.

---

## 🧠 Conclusión
El módulo de **Rutinas** centraliza la planificación del entrenamiento de cada cliente, integrando los componentes:
- Ejercicios
- Máquinas
- Tipos de medida
- Instructores y clientes  
Garantizando coherencia, trazabilidad y control de la evolución física de los usuarios.
