SoulSpeak Authentication API

This project is the authentication and authorization backend for the SoulSpeak mobile application, developed as part of a thesis project to support visually and hearing-impaired individuals.

🔐 Features

User Registration and Login

JWT-based Authentication

Refresh Token support (with expiry)

Secure Logout

User Profile Access

User Profile Update

Role-Ready JWT Claims (e.g., disability_type)

🛠️ Technologies Used

ASP.NET Core Web API

JWT (JSON Web Tokens) for authentication

BCrypt for password hashing

Entity Framework (via repository pattern)

Refresh Tokens (manually managed)

Dependency Injection (IUserService / IUserRepository)

Authorization via [Authorize] attribute

Token stored and passed through Authorization Headers (can be extended to HttpOnly Cookies)

📁 Project Structure

AuthController: Manages login, register, refresh, logout endpoints

UserController: Provides /me and /update for profile management

UserService: Business logic for auth and user handling

UserRepository: Data access operations

![image](https://github.com/user-attachments/assets/7488d2f4-6d2d-4518-94d1-1199f59afd54)


🔐 Example JWT Claims

{
  "sub": "user@example.com",
  "id": "3",
  "disability_type": "VisuallyImpaired"
}

🔐 Authentication Logic

Upon login, user credentials are validated and:

A JWT is generated and returned.

A Refresh Token is generated and stored in the database (valid for 30 days).

On protected endpoints, JWT is validated and claims are extracted to determine the user.

On logout, the refresh token is removed from the user entity.

Token renewal is handled via /api/auth/refresh using stored refresh tokens.

▶️ Getting Started

Prerequisites

.NET 8 SDK

SQL Server or SQLite (depending on your implementation)

Postman (optional for testing)

Configuration

Update the appsettings.json with your JWT configuration:

"Jwt": {
  "Key": "your_secret_key_here",
  "Issuer": "your_app",
  "Audience": "your_users"
}

Run the project

dotnet build
dotnet run

API will be accessible at: https://localhost:{port}/api/

🔒 Security Notes

Passwords are securely hashed with BCrypt.

Tokens are signed with HMAC SHA256.

Refresh tokens are securely stored and invalidated on logout.

Claims-based authorization can be extended for roles and permissions.

📄 License

This project is part of an academic thesis and is open for educational use.

