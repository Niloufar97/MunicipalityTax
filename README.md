# 🏛️ Tax Management Application

## 📝 Short Description
This project is a small application to manage taxes in different municipalities by date.  
I choose to implement it using **C#**, because it is my preferred language and I feel more confident with it.

## 🧰 Technologies Used
- **C#**
- **ASP.NET Core Web API**
- **Entity Framework Core**
- **SQLite**
- **xUnit** (for unit testing)
- **MVC pattern** (used in controller structure)
- **Scalar** (for API documentation and testing)
  
## 💻 Application Type
I decided to build a **Web API** application for this task.  
I chose Web API mostly because this task was about showing my skills, and I thought this way is a good example of how I usually build things.  
Also, Web APIs are a clean way to separate backend logic and make it easy to connect with different types of clients in the future, like web or mobile apps.

## 🗄️ Database
I didn’t ignore the database file — it is included in the project so reviewers can clone and test it easily. The database contains the tax records.

## 📜 API Documentation
I added **Scalar** to the project, so you can easily see and test the API endpoints in your browser.

## 🏢 Municipality Controller
I also added a Municipality controller where you can add a new municipality and get the list of existing ones.  
This part was not required in the task, but I added it to show some extra effort.  
My main focus was on the Tax controller and making sure tax-related features were working correctly.

## ✅ Unit Tests
I used **xUnit** for unit testing some parts of the application to make sure the logic works as expected.

## 🚀 How to Clone and Use
- Clone the project.
- Open the solution in Visual Studio or your preferred IDE.
- The data is **NOT** seeded automatically in the project, I added the data directly through the database.  
- So, if you want to test the app, you can check the database for existing data or add your own.  
- Run the Web API project and open **Scalar** UI to explore and test the endpoints.  
- Or use any REST client (like Postman) to query the tax rates by municipality and date.

---

Thank you for reviewing my project. 🙏
