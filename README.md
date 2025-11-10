# 🏃‍♂️  RunnerStats API

RunnerStats is an API built with .NET 8 designed to manage runner profiles and nutritional data.
It features secure authentication, layered architecture, and AI integration through the Groq API.

## 🚀 Main Features

Layered architecture, following an MVC-inspired design adapted for APIs:

**Layers:** `Data`, `Models`, `Controllers`, `Services`, `Helpers`.

Entity Framework Core as ORM with migrations and entity relationships.

JWT authentication and authorization:

Public access: register and login.

Protected access: runner and nutrition endpoints.

Password hashing for secure credential storage.

Main controllers:

AuthController: handles registration and login.

RunnerController: manages runner data.

NutritionController: manages nutritional data.

AI Integration (Groq API):

Chat with a virtual nutritionist that adapts answers based on runner data (e.g., vegan, diabetic, goals).

Endpoint testing with Bruno API.

CORS enabled for future frontend integration.

### 🧠 Data Structure

User: email + hashed password.

Runner: automatically created profile linked to each user.

Nutrition: personal data like dietary restrictions, goals, and notes.

### 🧩 Tech Stack

**.NET 8 / C#**

**Entity Framework Core**

**SQL Server**

**JWT Authentication**

**Groq API (LLM Integration)**

**Bruno API (Testing)**

### Configuration

### ⚙️ Configuration

Into `appsettings.json`.
2. Update the values:
   - `ConnectionStrings:SqlConnection` → your SQL Server connection string
   - `Jwt:key` → a secret key for JWT
   - `Groq:apiKey` → your Groq API key



### 🧪 Testing

All endpoints were tested using Bruno API, validating JWT authentication, CRUD operations, and AI responses.

![Register Test](images/apiRunnerIMgRegister.png)

![Login Test](images/apiRunnerImgLogin.png)

![GetRunner Test](images/apiRunnerImgGetRunner.png)

![UpdateRunner Test](images/apiRunnerImgUpdateRunner.png)

![UpdateRunner2 Test](images/apiRunnerImgUpdateRunner2.png)

![GetDataNutrition Test](images/apiRunnerImgGetDataNutrition.png)

![PutDataNutrition Test](images/apiRunnerImgPutDataNutrition.png)

![ChatIA Test](images/apiRunnerImgChatAI.png)

![ChatIA2 Test](images/apiRunnerImgChatAI2.png)

![ChatIA3 Test](images/apiRunnerImgChatAI3.png)


## 👨‍💻 Author

**Mauro Fernández**
.NET Developer
📧 [LinkedIn](https://www.linkedin.com/in/mauro-fern%C3%A1ndez-183b461ba/)

