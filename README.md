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
8. [Licencia](#licencia)

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
  "name": "John Doe",
  "dateOfBirth": "1990-01-01",
  "gender": "Male",
  "income": 50000
}
```

**Respuesta:**

```json
{
  "id": 1,
  "name": "John Doe",
  "dateOfBirth": "1990-01-01T00:00:00",
  "gender": "Male",
  "income": 50000
}
```

#### Obtener un Cliente por ID

**Respuesta:**

```json
{
  "id": 1,
  "name": "John Doe",
  "dateOfBirth": "1990-01-01T00:00:00",
  "gender": "Male",
  "income": 50000
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
  "accountNumber": "1234567890",
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
  "accountNumber": "1234567890",
  "amount": 500
}
```

**Respuesta:**

```json
{
  "message": "Depósito realizado con éxito."
}
```

#### Realizar un Retiro

**Solicitud:**

```json
{
  "accountNumber": "1234567890",
  "amount": 200
}
```

**Respuesta:**

```json
{
  "message": "Retiro realizado con éxito."
}
```

#### Obtener el Historial de Transacciones

**Respuesta:**

```json
[
  {
    "type": "Depósito",
    "amount": 500,
    "saldoPosterior": 1500,
    "fecha": "2025-03-21T18:29:53.5828027Z"
  },
  {
    "type": "Retiro",
    "amount": 200,
    "saldoPosterior": 1300,
    "fecha": "2025-03-21T18:30:10.1234567Z"
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

---

## Contribuciones

Si deseas contribuir, por favor, abre un **issue** o un **pull request** con tus mejoras o correcciones.

---

## Licencia

Este proyecto está bajo la licencia [MIT](LICENSE).

