**Online English Learning Website Microservices Project**

![listening](https://github.com/Zhengtx-hub/Zheng-VNext-learning/blob/main/pictures/infrastructure.png)

**Project Overview**

This project is an online English learning website, designed using a microservices architecture. It includes six independent microservice modules: user authentication and authorization, listening practice, vocabulary learning, management backend, audio and subtitle transcoding, file management, and search service. The project aims to explore the design and implementation of microservices and distributed architecture to improve the system’s scalability and flexibility.



**Project Architecture**

As shown in the architecture diagram, the system uses a B/S architecture, with two front-end pages providing user learning and management functionality. The front-end is built with Vue.js and supports independent deployment. Currently, the focus is on the development of backend microservices, while the optimization of UI and CSS for the front-end will be gradually improved in the future.

This project was developed during my learning journey with .NET technology, inspired by ZackYang’s open-source project. It is based on the original project with deep learning and optimization improvements:

​	•	The original project used Windows environment variables for configuration management. In this project, external configuration files were used to improve flexibility. It is suggested that further optimization could be achieved by implementing a Nacos configuration server for centralized management.

​	•	The backend framework was upgraded from .NET 6 to .NET 8 (chosen because EF Core MySQL official support was not fully available for .NET 9 at the time of development).

​	•	The database was changed from SQL Server to MySQL, leveraging EF Core’s Code First model to enhance entity relationship modeling and avoid direct database intervention.

​	•	**Frontend**: The frontend is built with Vue.js, providing independent modules for user functionality and management.

​	•	**Backend**: The backend is based on ASP.NET, implemented using a microservices architecture with a layered design to ensure code clarity and maintainability.

​	•	**Gateway**: Nginx is used as the reverse proxy service, managing the entry points of all microservices.

The interaction between the front-end and the back-end is done via HTTPS, and internal service communication is via HTTP, reducing security overhead and improving performance.

**Technical Features**

**1. Microservices Architecture**

The backend is based on an ASP.NET microservices architecture, with six independent modules:

​	1.	**User Authorization and Authentication Service** – Provides login, registration, and user management functionality.

​	2.	**General Function Service** – Includes core learning features such as the listening and vocabulary modules.

​	3.	**Management Page Service** – Supports the backend management system.

​	4.	**Transcoding Service** – Handles the processing and transcoding of audio and subtitle files.

​	5.	**File Management Service** – Implements file uploading, storage, and management.

​	6.	**Search Service** – Provides full-text search functionality.

The project uses **Nginx Reverse Proxy** as a unified gateway, decoupling the front-end from the back-end, resulting in high scalability and maintainability.

**2. Domain-Driven Design (DDD)**

​	•	**Unified Design**: Implements the DDD methodology with unified domain language and bounded contexts.

​	•	**Rich Model**: Entities use rich models to enhance the robustness of the domain.

​	•	**Anti-Corruption Layer Design**: To cope with frequent changes in external services, anti-corruption layers reduce dependency coupling.

​	•	**Clean Architecture (Onion Architecture)**: Strict decoupling between domain, infrastructure, and application layers, with interactions through anti-corruption layers.

**3. Event-Driven and Distributed Features**

​	•	**Redis Distributed Cache**: Improves data access performance and supports horizontal scaling.

​	•	**RabbitMQ Message Queue**: Implements service communication through event-driven architecture.

​	•	**ElasticSearch Search Engine**: Provides efficient full-text search capabilities.

​	•	**Nginx Gateway Service**: Implements load balancing and service forwarding. Front-end requests to Nginx use HTTPS, while internal services communicate via HTTP to reduce performance overhead.

**4. Technology Stack**

​	1.	**Backend Technologies**

​	•	**Language**: C# (.NET 8)

​	•	**Architecture**: ASP.NET MVC, combined with DDD principles.

​	•	**Database**: MySQL (replacing SQL Server), using EF Core’s Code First model.

​	•	**Cache**: Redis, providing efficient distributed caching.

​	•	**Message Queue**: RabbitMQ, enabling microservice decoupling and event-driven architecture.

​	•	**Search Engine**: ElasticSearch for full-text search functionality.

​	•	**Logging**: Serilog, supporting log persistence.

​	2.	**Frontend Technologies**

​	•	**Framework**: Vue.js

​	•	**Styles**: CSS (still to be optimized, with plans to introduce a component library later).

​	3.	**Infrastructure**

​	•	**Gateway**: Nginx for reverse proxy and load balancing.

​	•	**Configuration Management**: Managed via external JSON configuration files, allowing flexibility and scalability (future plan to integrate Nacos configuration center).

​	•	**Deployment**: Supports separation of local development and production environments, with external configuration files dynamically loading environment variables.

**5. System Architecture Design and Thought Process**

Through learning .NET theory and ZackYang’s course, combined with years of software development and design experience, the system has the following advantages:

​	1.	**High Scalability and Low Coupling**: The microservice and DDD design principles ensure high decoupling between services, making the system easy to extend and maintain.

​	2.	**Distributed and High-Performance Optimization**: By integrating Vue.js, RabbitMQ, and ElasticSearch, the system significantly improves data access efficiency and response time.

​	3.	**Modern Technology Stack**: Using ASP.NET Core 8 and the latest EF Core stack, compatible with MySQL databases, reflecting cutting-edge technology.

​	4.	**Comprehensive Security Design**: All front-end requests go through HTTPS, ensuring secure transmission. Nginx provides efficient reverse proxy.

​	5.	**Clean Architecture**: Using rich models and anti-corruption layers to reduce complexity and enhance code readability and reusability.

**Future Optimizations**

​	1.	**Frontend Functionality Expansion**: Optimize front-end UI and enrich user interaction.

​	2.	**Centralized Configuration Management**: Migrate configuration files to Nacos or cloud-based secret management services.

​	3.	**Performance Optimization**: Further tune RabbitMQ and Redis configurations to improve handling of high-concurrency scenarios.

**6. Deployment and Running**

**Deployment Requirements**

​	1.   Dependencies:

​	•	.NET 8 SDK

​	•	MySQL Database

​	•	Redis

​	•	RabbitMQ

​	•	ElasticSearch

​	•	Nginx

**System Operation**

**1. Project Source Code Overview**

| **Project**                        | **Class**                                        | **Description**                         |
| ---------------------------------- | ------------------------------------------------ | --------------------------------------- |
| Zheng.ASPNETCore                   | DistributedCacheHelper                           | Distributed cache helper class          |
| MemoryCacheHelper                  | Memory cache helper class                        |                                         |
| UnitOfWorkFilter                   | Unit of work filter                              |                                         |
| Zheng.Commons                      | Validators folder                                | FluentValidation extension classes      |
| LoggerExtensions                   | Using FormattableString for simplified logging   |                                         |
| ModuleInitializerExtensions        | Service registration in individual projects      |                                         |
| Zheng.DomainCommons                | IAggregateRoot                                   | Aggregate root marker interface         |
| BaseEntity、AggregateRootEntity    | Domain event publication                         |                                         |
| MultilingualString                 | Multilingual value object                        |                                         |
| Zheng.EventBus                     |                                                  | Event bus integration                   |
| Zheng.Infrastructure               | BaseDbContext                                    | Domain event publication                |
| EFCoreInitializerHelper            | Automating context registration                  |                                         |
| ExpressionHelper                   | Simplified equality comparison for value objects |                                         |
| MediatorExtensions                 | Domain event registration                        |                                         |
| MultilingualStringEFCoreExtensions | Multilingual value object configuration          |                                         |
| Zheng.JWT                          |                                                  | JWT authentication token implementation |

**2. Clone Project Locally and Switch to Main Directory**

```
git clone https://github.com/your-repo/your-project.git
cd your-project
```

**3. Basic Environment Setup**

Services used in the system include ElasticSearch, Nginx, Redis, RabbitMQ. Please install these services before running the project. Details are not provided in this document.

The project is currently using .NET 8.0.

```xml
  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
  </PropertyGroup>
```

Since the original project uses Windows environment variables for configuration, and I am using a Mac, I created an appsettings.json file to handle system configuration. This configuration is read through internal code. **Note**: It is strongly discouraged to write sensitive system information in plain text within the deployment package in a production environment. It is highly recommended to use centralized configuration management services like Nacos, AWS Systems Manager Parameter Store, or Azure Key Vault to securely manage configurations.

Here’s an example of the local configuration:

```json
{
	.....
  "AllowedHosts": "*",
  "Cors": {
    "Origins": [ "http://localhost:3000", "http://localhost:3001" ]
  },
  "FileService": {
    "SMB": {
      "WorkingDir": "/Users/zheng/Documents/upload"
    },
    "UpYun": {

    },
    "EndPoint": {

    }
  },
  "ConnectionStrings": {
    "DefaultDB": "Server=localhost;Port=3306;Database=zheng-VNext;;User=root;Password=password;"
  },
  "Redis": {
    "ConnStr": "localhost"
  },
  "JWT": {
    "Issuer": "myIssuer",
    "Audience": "myAudience",
    "
```

To handle database configuration in CommonInitializer, modify WebApplicationBuilderExtensions:

```csharp
builder.Host.ConfigureAppConfiguration((hostCtx, configBuilder) =>
{
  var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory()) // Load directory
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true) // Read appsettings.json
    .Build();
  var connStr = configuration.GetConnectionString("DefaultDB");
  configBuilder.AddDbConfiguration(() => new SqlConnection(connStr), reloadOnChange: true, reloadInterval: TimeSpan.FromSeconds(5));
});
```

Nginx:

```
server {
        listen       8090;
        server_name  localhost;

        location /FileService/ {
            proxy_pass http://localhost:50401/;
            proxy_set_header Host $host;
            proxy_set_header X-Real-IP $remote_addr;
            proxy_set_header X-Real-PORT $remote_port;
            proxy_set_header X-Forwarded-For $proxy_add_x_forwarded_for;
            proxy_set_header X-Forwarded-Proto  $scheme;
            client_max_body_size 100m;
        }
        
        location /IdentityService/ {
            proxy_pass  http://localhost:50402/;
            proxy_set_header Host $host;
            proxy_set_header X-Real-IP $remote_addr;
            proxy_set_header X-Real-PORT $remote_port;
            proxy_set_header X-Forwarded-For $proxy_add_x_forwarded_for;
            proxy_set_header X-Forwarded-Proto  $scheme;
        }
        
        location /Listening.Admin/ {
            proxy_pass http://localhost:50403/;
            proxy_set_header Host $host;
            proxy_set_header X-Real-IP $remote_addr;
            proxy_set_header X-Real-PORT $remote_port;
            proxy_set_header X-Forwarded-For $proxy_add_x_forwarded_for;
            proxy_set_header X-Forwarded-Proto  $scheme;
            proxy_http_version 1.1;
            proxy_set_header Upgrade $http_upgrade;
            proxy_set_header Connection "upgrade";
        }

        location /Listening.Main/ {
            proxy_pass http://localhost:50404/;
            proxy_set_header Host $host;
            proxy_set_header X-Real-IP $remote_addr;
            proxy_set_header X-Real-PORT $remote_port;
            proxy_set_header X-Forwarded-For $proxy_add_x_forwarded_for;            
            proxy_set_header X-Forwarded-Proto  $scheme;
        }           
        
        location /MediaEncoder/ {
            proxy_pass http://localhost:50405/;
            proxy_set_header Host $host;
            proxy_set_header X-Real-IP $remote_addr;
            proxy_set_header X-Real-PORT $remote_port;
            proxy_set_header X-Forwarded-For $proxy_add_x_forwarded_for;    
            proxy_set_header X-Forwarded-Proto  $scheme;        
        }

        location /SearchService/ {
            proxy_pass http://localhost:50406/;
            proxy_set_header Host $host;
            proxy_set_header X-Real-IP $remote_addr;
            proxy_set_header X-Real-PORT $remote_port;
            proxy_set_header X-Forwarded-For $proxy_add_x_forwarded_for;
            proxy_set_header X-Forwarded-Proto  $scheme;
        }               
```

**1.Start six microservices, and the Swagger pages will open automatically：**
![listening](https://github.com/Zhengtx-hub/Zheng-VNext-learning/blob/main/pictures/startup.png)
**2.Next is an example of a Swagger page.**

IdentityServer:
![listening](https://github.com/Zhengtx-hub/Zheng-VNext-learning/blob/main/pictures/IdentityService.png)
ListeningService(admin&user):
![listening](https://github.com/Zhengtx-hub/Zheng-VNext-learning/blob/main/pictures/ListeningService.png)
![listening](https://github.com/Zhengtx-hub/Zheng-VNext-learning/blob/main/pictures/Listeningmain.png)
FileService:
![listening](https://github.com/Zhengtx-hub/Zheng-VNext-learning/blob/main/pictures/FileService.png)
SearchService:
![listening](https://github.com/Zhengtx-hub/Zheng-VNext-learning/blob/main/pictures/SearchService.png)

**3.Example for Web pages**




