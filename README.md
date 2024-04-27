# E-Commerce Project

Welcome to our E-Commerce project! This application is designed to provide a seamless and secure online shopping experience. Below, you'll find information about the key features and technologies used on the server side.

## Key Requirements

### 1- A basic login and registration with user roles (Admin and Customer).
### 2- Only registered users can make orders
### 3- Admin should set the currency exchange rate for each currency available, and the system will save it in the Redis cache, the expiration time of Redis should defined in the configuration file.
### 4- The basic currency should be defined in the configuration file.
### 5- Admin has full control on the items.
### 6- The Discount Promo Code and Discount Value should defined in the configuration file, if the customer sends the Discount Promo Code, the discount should be applied to the order.
### 7- List of available items as the user can review items select needs and add to cart.
### 8- Calculate the detailed and total value considering the currency exchange and discount.
### 9- The create order with details should be one API.
### 10- The customer can get his orders after login.
### 11- The admin can close the orders.
### 12- Use the setup data tables for the definition of orders (Items, Customer, and UOM) with a browser for every table to view and search.

## Key Notes And Facilities
### External Packages Used
- AutoMapper
- JSON Web Token
- MediatR
- Redis

### Architecture Pattern Implemented
- CQRS => Command Query Responsibilty Segregation

- Definition : CQRS (Command Query Responsibility Segregation) Pattern is a design pattern for segregating different responsibility types in a software application. The basic idea behind CQRS is to split an application’s operations into two groups:

Commands, which are responsible for changing the state of the application
Queries, which retrieve data without changing any state
Benefits of using CQRS pattern include higher scalability, simplified code maintenance, and reduced code complexity. It also allows for targeting the specific needs of the system and helps to resolve inconsistencies commonly seen in traditional CRUD architectures.

-Reference : (https://www.codeproject.com/Articles/5377617/CQRS-Pattern-in-Csharp-and-Clean-Architecture-A-Si)

### Design Patterns 
- Mediator Design Pattern With CQRS
- Definition : Mediator is a behavioral design pattern that reduces coupling between components of a program by making them communicate indirectly, through a special mediator object.

-Reference : ([https://www.codeproject.com/Articles/5377617/CQRS-Pattern-in-Csharp-and-Clean-Architecture-A-Si](https://refactoring.guru/design-patterns/mediator/csharp/example))

## Technologies Used
- ![.Net](https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white)
- ![C#](https://img.shields.io/badge/c%23-%23239120.svg?style=for-the-badge&logo=csharp&logoColor=white)
- ![MicrosoftSQLServer](https://img.shields.io/badge/Microsoft%20SQL%20Server-CC2927?style=for-the-badge&logo=microsoft%20sql%20server&logoColor=white)
- ![EntityFramework Core](https://img.shields.io/badge/Entity%20Framework%20Core-626CD9?style=for-the-badge&logo=entity%20framework%20core&logoColor=white)
- ![Redis](https://img.shields.io/badge/redis-%23DD0031.svg?style=for-the-badge&logo=redis&logoColor=white)
- ![JWT](https://img.shields.io/badge/JWT-black?style=for-the-badge&logo=JSON%20web%20tokens)
- ![Docker](https://img.shields.io/badge/docker-%230db7ed.svg?style=for-the-badge&logo=docker&logoColor=white)
- ![Postman](https://img.shields.io/badge/Postman-FF6C37?style=for-the-badge&logo=postman&logoColor=white)
- ![Git](https://img.shields.io/badge/git-%23F05033.svg?style=for-the-badge&logo=git&logoColor=white)


## Getting Started
1. **Clone the repository:**
   - git clone (https://github.com/Hesham-SH/Enhanced-ECommerce.git)

2. **Ensure .NET Core SDK is installed:**
   - Make sure you have the .NET Core SDK installed on your machine. You can download it from [here](https://dotnet.microsoft.com/download).

3. **Navigate to the API directory:**
   - Open your terminal or command prompt and navigate to the "API" directory of the project.
   - Run this command:
   - ```cd API```

3. **Run migrations to set up the database:**
   - No Need To Update Database Manually , A Service is registered in the application pipeline which ensures that database is updated to the latest migration file.

3. **Start the API:**
   - Open your terminal or command prompt and navigate to the "API" directory of the project.
  
   - Run the following command to start the API:
   - ```dotnet run``` or you could use ```dotnet watch --no-hot-reload``` instead to avoid potential problems.


   **<p style="font-size: 2rem; text-align: center; margin:auto;"><strong>Good Luck With Your Code</strong></p>**
