# Drug Indication API

## About the Architecture

This project follows **Clean Architecture** and **Clean Code** principles. It is designed to be scalable, testable, and production-ready. It solves the challenge of structuring drug program data and exposing it through a secure RESTful API.

### Technologies Used

- **.NET 8 (ASP.NET Core)** – Fast and modular web API framework
- **MongoDB** – Flexible NoSQL database for structured and semi-structured data
- **JWT Authentication** – Secure token-based authentication with `Admin` and `User` roles
- **xUnit + Moq + FluentAssertions** – Complete unit testing stack
- **Docker & Docker Compose** – Containerized API and database
- **Swagger** – Interactive documentation with built-in `Authorize` support
- **Postman** – External API testing and automation
- **FluentValidation**: Used for implementing business logic validations and the Notification Pattern.

---

## How to Run the API

### Running with Docker

1. Install Docker: [Docker Installation Guide](https://docs.docker.com/get-docker/)
2. Install Docker Compose: [Docker Compose Installation Guide](https://docs.docker.com/compose/install/)
3. Open a terminal in the project root folder
4. Run the following command:
   ```SH
   docker-compose up --build
   ```
5. Open your browser and access the API documentation at:
   [http://localhost:5000/swagger](http://localhost:5000/swagger)

**Note:** The `docker-compose` setup includes a database that is preloaded. This allows for a quick demonstration of the application’s functionalities without requiring manual user creation.

### Running Without Docker

1. Install the .NET SDK and Runtime 8: [Download .NET 8](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
2. Install Visual Studio or Visual Studio Code
3. You can start the application using:
   - **Visual Studio**: Click on the "Start" button.
   - **Visual Studio Code**: Open the Debug panel and select the "Development" option.

---

## Database Access
Open MongoDB Compass or any mongo client.

Connect to the server localhost:27017.
Open the database DrugIndicationDb
---

## CI/CD Pipeline

A GitHub Actions pipeline is available in the `.github/workflows` directory. This pipeline is configured to build and push a Docker image to Docker Hub.

## Improvements

Although the application, there are several areas for potential improvement:

### 1. **Performance Optimization**
- Implement caching mechanisms for frequently accessed data.

### 2. **Authentication & Authorization**
- Implement authentication and authorization using industry-standard solutions like:
  - Identity Server
  - Keycloak
  - AWS Cognito

### 3. **Tracing & Observability**
- Integrate OpenTelemetry for distributed tracing and performance monitoring.
- Implement logging with structured logs to enhance debugging capabilities.

### 4. **Infrastructure Unit Tests**
- Implement unit tests for infrastructure layers to ensure robustness and maintainability.
- Validate the correct functioning of database access and external service integrations.
