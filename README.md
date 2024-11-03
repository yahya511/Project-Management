# Project Management System

![Project Management System Logo](https://github.com/yahya511/Project-Management/blob/main/FCILuxorDB_Diagram.jpg)

## Table of Contents

1. [Introduction](#introduction)
2. [Project Structure](#project-structure)
3. [Technologies and Tools](#technologies-and-tools)
4. [Sub-solutions Overview](#sub-solutions-overview)
   - [FCIEmployees](#fciemployees)
   - [FCIProjects](#fciprojects)
   - [AuthService](#authservice)
5. [Roles and Responsibilities](#roles-and-responsibilities)
6. [Benefits of the Patterns and Technologies Used](#benefits-of-the-patterns-and-technologies-used)
7. [Getting Started](#getting-started)
8. [Contributing](#contributing)
9. [License](#license)

# Introduction

The **Project Management System** is a comprehensive solution designed to facilitate effective project management, resource allocation, and team collaboration. With a focus on usability and scalability, this system aims to enhance productivity by providing powerful tools for managing projects and employees seamlessly.

## Project Structure

The project is structured into three primary sub-solutions, each serving a specific function:


### FCIEmployees
- **Purpose**: Manage employee records, roles, and associated data.
- **Features**: Employee creation, updates, deletions, and role assignments.

### FCIProjects
- **Purpose**: Handle project details, tasks, and team assignments.
- **Features**: Project creation, updates, task tracking, and department management.

### AuthService
- **Purpose**: Provide authentication and authorization services across the system.
- **Features**: User management, JWT token generation, and session handling.

## Technologies and Tools

This project employs modern technologies and development practices, including:

- **ASP.NET Core 8**: For building the back-end services.
- **Entity Framework Core**: For database interaction.
- **JWT (JSON Web Tokens)**: For secure authentication.
- **gRPC**: For efficient communication between services.
- **Visual Studio Code**: IDE for development.

## Sub-solutions Overview

### FCIEmployees

The **FCIEmployees** solution is responsible for managing employee data, including their roles, personal information, and work-related details. It ensures that all employee records are up-to-date and accessible to authorized personnel.

### FCIProjects

The **FCIProjects** solution focuses on project management. It allows project managers to create and manage projects, assign tasks, and monitor progress, ensuring that projects are completed on time and within scope.

### AuthService

The **AuthService** serves as the central authentication service, handling user login, registration, and session management. It employs JWT for secure token-based authentication, ensuring that only authorized users can access specific functionalities.

## Roles and Responsibilities

The system incorporates three key roles to ensure effective management and security:

- **System Admin**: Responsible for managing user accounts, permissions, and system settings.
- **Project Manager**: Oversees project execution, team management, and task assignments.
- **Employee**: Engages with projects, updates their task status, and submits progress reports.

## Benefits of the Patterns and Technologies Used

The chosen technologies and design patterns enhance the system's scalability, security, and maintainability:

- **Microservices Architecture**: Promotes modularity and independent service deployment.
- **JWT for Authentication**: Provides a stateless and secure method for user authentication.
- **gRPC for Inter-Service Communication**: Enables high-performance communication between different services.

## Getting Started

To get started with the Project Management System, clone the repository and follow the setup instructions:

```bash
git clone https://github.com/yahya511/Project-Management.git
cd Project-Management
# Follow setup instructions for each sub-solution




