
## 📦 Tecnologías

- ⚙️ **Backend**: ASP.NET Core (.NET 7) + Entity Framework Core
- 🌐 **Frontend**: Angular 15+
- 🗃️ **Base de Datos**: SQL Server
- 🔐 **Autenticación**: JWT 

## ✅ Requisitos

### Backend (.NET Core API)

- SQL Server instalado o disponible online

### Frontend (Angular)

- Node.js v16+
- Angular CLI:
  npm install -g @angular/cli
  
### Pasos
 
 
### 1. Clona el repositorio

	git clone https://github.com/CesarSanchezLopez/StockManagement.git


### 2.  Configura la base de datos

Configura la cadena de conexión al archivo:

Backend/StockManagement.Api/appsettings.Development.json

{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.\\SQLEXPRESS;Database=StockDb;Trusted_Connection=True;TrustServerCertificate=True"
  }
}

### 3. Ejecuta las migraciones

cd Backend/StockManagement.Api

dotnet ef migrations add InitialCreate

dotnet ef database update


### 4. Corre el Backend

dotnet run --project Backend/StockManagement.Api


### 5.  Configura  el Frontend Angular

Configura el punto de acceso a la api al archivo:
StockManagement\Frontend\stock-client\src\environments

export const environment = {
    production: false,
    apiUrl: 'https://localhost:7270/api'
  };


### 6. Corre el Frontend Angular

cd Frontend

npm install

ng serve


### 🔐 Usuarios de prueba

{
  "email": "UserAdmin@gmail.com",
  "password": "12345"
}
{
  "email": "UserCliente@gmail.com",
  "password": "12345"
}
  
