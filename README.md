I have designed and implemented a comprehensive backend solution that facilitates database support within a completely decoupled architecture. The system's Web API maintains agnosticism from both the Application Core and the Database by interfacing with them, ensuring total decoupling from all other system components.

Backend.Application - Class Library

This component encapsulates all application business logic. It employs the Command and Query Responsibility Segregation (CQRS) pattern to facilitate the execution of all supported operations.

Backend.Common – Class Library

This library houses shared enumerations and values utilized throughout the application, including environment settings and configurations.

Backend.Domain – Class Library

This section details the structure of Database Objects in Plain Old CLR Object (POCO) format, allowing for adaptability with various Relational Database Management Systems (RDBMS). It leverages Entity Framework Core for fluent configuration of POCO properties.

Backend.Infrastructure - Class Library

This module is responsible for configuring the infrastructure, including databases, services, and the definition of entities within the RDBMS. It employs a fluent language for domain validation, independent of the database vendor. This flexibility allows for support of various vendors (e.g., SQLite, PostgreSQL, SQL Server, etc.), enhancing the system's versatility and reuse potential.
