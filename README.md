# API de Gestión de Cuentas Bancarias

Esta es una API desarrollada en **.NET 8** que permite gestionar cuentas bancarias, realizar transacciones (depósitos y retiros) y consultar el saldo y el historial de transacciones. La API sigue las mejores prácticas de desarrollo y está diseñada para ser fácil de usar y extender.

---

## Tabla de Contenidos

1. [Requisitos](#requisitos)
2. [Instalación](#instalación)
3. [Configuración](#configuración)
4. [Uso de la API](#uso-de-la-api)
   - [Clientes](#clientes)
   - [Cuentas Bancarias](#cuentas-bancarias)
   - [Transacciones](#transacciones)
5. [Ejemplos de Solicitudes y Respuestas](#ejemplos-de-solicitudes-y-respuestas)
6. [Pruebas Unitarias](#pruebas-unitarias)
7. [Contribuciones](#contribuciones)

---

## Requisitos

- **.NET 8 SDK**: Asegúrate de tener instalado el SDK de .NET 8.
- **Visual Studio 2022** o **Visual Studio Code**: Para editar y ejecutar el proyecto.
- **Postman** o **Swagger**: Para probar los endpoints de la API.

---

## Instalación

1. Clona el repositorio en tu máquina local:

   ```bash
   git clone https://github.com/tu-usuario/pjCuentaBancaria.git
   cd pjCuentaBancaria
   ```

2. Instala las dependencias necesarias:

   ```bash
   Install-Package Microsoft.EntityFrameworkCore.InMemory
   ```

   > **Nota:** Se usa una base de datos **In-Memory** para este ejemplo.

---

## Uso de la API

La API ofrece los siguientes endpoints:

### Clientes

- **Crear un cliente:**

  ```http
  POST /api/customer
  ```

- **Obtener un cliente por ID:**

  ```http
  GET /api/customer/{id}
  ```

### Cuentas Bancarias

- **Crear una cuenta bancaria:**

  ```http
  POST /api/bankaccount/crear
  ```

- **Consultar el saldo de una cuenta:**

  ```http
  GET /api/bankaccount/{accountNumber}/balance
  ```

### Transacciones

- **Realizar un depósito:**

  ```http
  POST /api/transaction/deposit
  ```

- **Realizar un retiro:**

  ```http
  POST /api/transaction/withdraw
  ```

- **Obtener el historial de transacciones:**

  ```http
  GET /api/transaction/{accountNumber}/history
  ```

---

## Ejemplos de Solicitudes y Respuestas

### Clientes

#### Crear un Cliente

**Solicitud:**

```json
{
  "id": 0,
  "name": "Juan",
  "dateOfBirth": "2025-03-21T19:39:24.711Z",
  "gender": "Hombre",
  "income": 0
}
```

**Respuesta:**

```json
{
  "id": 1,
  "name": "Juan",
  "dateOfBirth": "2025-03-21T19:39:24.711Z",
  "gender": "Hombre",
  "income": 0
}
```

#### Obtener un Cliente por ID

**Respuesta:**

```json
{
  "id": 1,
  "name": "Juan",
  "dateOfBirth": "2025-03-21T19:39:24.711Z",
  "gender": "Hombre",
  "income": 0
}
```

### Cuentas Bancarias

#### Crear una Cuenta Bancaria

**Solicitud:**

```json
{
  "balance": 1000,
  "customerId": 1
}
```

**Respuesta:**

```json
{
  "accountNumber": "0659669741",
  "balance": 1000,
  "customerId": 1
}
```

#### Consultar el Saldo de una Cuenta

**Respuesta:**

```json
{
  "balance": 1000
}
```

### Transacciones

#### Realizar un Depósito

**Solicitud:**

```json
{
  "accountNumber": "0659669741",
  "amount": 2000
}
```

**Respuesta:**

```json
{
  "id": 1,
  "type": "Deposito",
  "amount": 2000,
  "transactionDate": "2025-03-21T19:40:05.5400055Z",
  "bankAccountId": 1
  "currentBalance": 3000
}
```

#### Realizar un Retiro

**Solicitud:**

```json
{
  "accountNumber": "0659669741",
  "amount": 1500
}
```

**Respuesta:**

```json
{
  "id": 1,
  "type": "Retiro",
  "amount": 1500,
  "transactionDate": "2025-03-21T19:40:05.5400055Z",
  "bankAccountId": 1
}
```

#### Obtener el Historial de Transacciones

**Respuesta:**

```json
[
  {
    "id": 1,
    "type": "Deposito",
    "amount": 2000,
    "transactionDate": "2025-03-21T19:40:05.5400055Z",
    "accountNumber": "0659669741"
  },
  {
    "id": 2,
    "type": "Retiro",
    "amount": 1500,
    "transactionDate": "2025-03-21T19:40:23.5931375Z",
    "accountNumber": "0659669741"
    "currentBalance": 1500
  }
]
```

---

## Pruebas Unitarias

Para ejecutar las pruebas unitarias, usa el siguiente comando:

```bash
 dotnet test
```

Las pruebas cubren los siguientes casos:

- Creación de clientes y cuentas bancarias.
- Validación de transacciones (depósitos y retiros).
- Consulta de saldo y historial de transacciones.

