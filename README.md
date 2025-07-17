# 🎉 CotoBirthdayBooking API

API REST para la gestión de reservas de salones de cumpleaños.

---

## 🛠️ Tecnologías

- .NET 8  
- Entity Framework Core (InMemory)  
- Swagger / OpenAPI  
- Arquitectura por capas (Controller / Application / Domain / Infrastructure)  
- AutoMapper  
- Docker (opcional, para facilitar la ejecución)  
- xUnit (pruebas unitarias)  

---

## 🚀 Cómo correr el proyecto

### ✅ Opción 1: Desde Visual Studio / VS Code

1. Clonar el repositorio:

   git clone https://github.com/arielmalerba84/CotoBirthdayBooking.git  
   cd CotoBirthdayBooking

2. Ejecutar el proyecto `CotoBirthdayBooking.Api` desde tu entorno de desarrollo (Visual Studio o VS Code).

3. Acceder a Swagger:

   https://localhost:7285/swagger/index.html

---

### 🐳 Opción 2: Usando Docker

1. Clonar el repositorio:

   git clone https://github.com/arielmalerba84/CotoBirthdayBooking.git  
   cd CotoBirthdayBooking

2. Ejecutar con Docker Compose:

   docker-compose -f docker-compose-fixed.yml up --build

3. Acceder a Swagger:

   http://localhost:8082/swagger

---

## 🧪 Endpoints

### 📌 Crear una reserva

POST /api/reserva

Body de ejemplo:

{
  "fecha": "2025-07-15",
  "horaInicio": "10:00:00",
  "horaFin": "12:00:00",
  "salonId": 1
}

#### ✅ Reglas de validación

- Horario entre 09:00 y 18:00 hs  
- Mínimo 30 minutos de margen entre eventos  
- No se permiten superposiciones de horarios  
- horaFin debe ser mayor que horaInicio  

---

### 📅 Consultar reservas por fecha

GET /api/reserva/2025-07-15

Respuesta esperada:

[
  {
    "id": 1,
    "fecha": "2025-07-15",
    "horaInicio": "10:00:00",
    "horaFin": "12:00:00",
    "salonId": 1
  }
]

---

## 🔒 Manejo de concurrencia

La API utiliza un lock estático in-memory para asegurar que las reservas no se solapen.  
Esto garantiza que mientras una reserva se está procesando, las demás esperan su turno.

---

## 🧪 Pruebas unitarias

Las pruebas están ubicadas en el proyecto `CotoBirthdayBooking.Tests`.

Para ejecutarlas:

dotnet test

---

## 📌 Consideraciones

- No se utiliza RabbitMQ (se removió para simplificar la ejecución del desafío)  
- Validaciones centralizadas mediante servicios  
- Separación clara de responsabilidades con el patrón Repository y Arquitectura en Capas
  (para simplificar el desafio se omitio usar el patron mediador con MediaR)
- Preparado para correr tanto con Docker como sin él  

---

## 📬 Autor

Ariel Malerba  
GitHub: https://github.com/arielmalerba84



