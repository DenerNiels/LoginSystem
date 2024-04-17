# LoginSystem
Segurança e Autenticação de API’s ASP.NET com JWT e Bearer Authentication!

# What does this project do?
This project is an application that provides an API for managing user accounts. It allows users to create new accounts, authenticate themselves, and activate their accounts through an activation code sent via email. Users' passwords are protected through a hashing process before being stored in the database, ensuring an additional layer of security. In summary, the project handles user account management, implementing security practices such as password hashing and email verification for account activation.

# What do I need to configure for this project to work?

<br>

1.Perform the project clone. \
2.Adjust the connection string in the appsettings.json. \
3.Execute a dotnet-ef database update. \
4.Navigate to the JwtStore.Api project. \
5.Use dotnet user-secrets and configure these keys and values (all information is available on the SendGrid website): 

# Project Structure:
<br>

* Api → Project application.
* Core → Business rules.
* Infra → Configuration and data. 
<br>
 The project was created based on contexts, where each context has its own independence and includes:


* Entities
* UseCases and Interfaces (contracts)
* ValueObjects 
* Database settings
