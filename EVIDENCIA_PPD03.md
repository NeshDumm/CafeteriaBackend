# Evidencia de Funcionamiento y Persistencia - PPD03

**Materia:** Diseño de Aplicaciones y Servicios para Internet (ITID803_02)  
**Actividad:** PPD03 – Arquitectura en Capas, Interfaces y Persistencia  
**Proyecto:** CafeteriaBackend  

---

## 1. Arquitectura en Capas Implementada

La aplicación cumple con la separación estricta de responsabilidades mediante interfaces e inversión de dependencias:

$$\text{ProveedoresController} \xrightarrow{\text{IProveedorService}} \text{ProveedorService} \xrightarrow{\text{IProveedorRepository}} \text{ProveedorRepository} \xrightarrow{\text{CafeteriaDbContext}} \text{SQL Server (dbo.Proveedores)}$$

* **`Interfaces/`**:
  * `IProveedorRepository.cs`: Define métodos `GetAllAsync`, `GetByIdAsync`, `AddAsync`, `UpdateAsync`, `DeleteAsync`, `SaveChangesAsync`.
  * `IProveedorService.cs`: Define métodos de negocio `ObtenerTodosAsync`, `ObtenerPorIdAsync`, `CrearProveedorAsync`, etc.
* **`Repositories/`**:
  * `ProveedorRepository.cs`: Implementa `IProveedorRepository` usando `CafeteriaDbContext`.
* **`Services/`**:
  * `ProveedorService.cs`: Implementa `IProveedorService` inyectando `IProveedorRepository`.
* **`Controllers/`**:
  * `ProveedoresController.cs`: Expone `/api/Proveedores`, inyecta `IProveedorService`, protegido con `[Authorize]`.

---

## 2. Registro de Inyección de Dependencias (`Program.cs`)

```csharp
builder.Services.AddScoped<IProveedorRepository, ProveedorRepository>();
builder.Services.AddScoped<IProveedorService, ProveedorService>();
```

---

## 3. Prueba de Autenticación y Emisión de JWT (Admin)

* **Endpoint:** `POST /api/auth/login`
* **Payload:**
```json
{
  "usuario": "admin",
  "password": "Admin123"
}
```
* **Respuesta HTTP:** `200 OK`
* **Captura:** `capturas/01_Login_200.png`

---

## 4. Prueba del Endpoint `POST /api/Proveedores` (Criterio 8)

* **Endpoint:** `POST /api/Proveedores`
* **Cabecera de Autorización:** `Authorization: Bearer <TOKEN_ADMIN>`
* **Cuerpo de la Petición:**
```json
{
  "nombre": "Distribuidora La Costa",
  "telefono": "0991234567",
  "activo": true
}
```

### Respuesta del Servidor:
* **Código de Estado:** `201 Created`
* **Encabezado `Location`:** `https://localhost:7283/api/Proveedores/4`
* **Cuerpo JSON retornado (con ID generado):**
```json
{
  "id": 4,
  "nombre": "Distribuidora La Costa",
  "ruc": "",
  "telefono": "0991234567",
  "direccion": "",
  "activo": true
}
```

* **Capturas:**
  * Petición enviada: `capturas/02_Swagger_POST_Request.png`
  * Respuesta 201 Created: `capturas/03_Swagger_POST_201_Created.png`

---

## 5. Verificación de Persistencia en SQL Server (`dbo.Proveedores`) (Criterio 10)

Consulta directa ejecutada sobre la base de datos `CafeteriaDb` en `Server=(localdb)\MSSQLLocalDB`:

```sql
SELECT Id, Nombre, Telefono, Activo FROM dbo.Proveedores;
```

### Resultado de la consulta:
```
Id: 4 | Nombre: Distribuidora La Costa | Telefono: 0991234567 | Activo: True
```

*Estado:* **Persistencia confirmada exitosamente en SQL Server.**
