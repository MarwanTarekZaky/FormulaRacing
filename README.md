🏁 Formula - ASP.NET Core MVC Racing Events Platform
Formula is a full-featured web application built with ASP.NET Core MVC.
It enables administrators to manage racing events, cars, banners, 
and bookings with a sleek UI and rich media features. 
The client-facing interface offers an interactive experience for users, 
including image sliders, modals, and dynamic dropdowns.



🚀 Features
🔧 CRUD Operations for:

Cars (with multiple images)

Races

Banners (displayed in homepage slider)

Events (linked via YouTube videos)

📷 Car Image Gallery with Swiper sliders and modal previews

🏎️ Race-Car Relationship with many-to-many join using RaceCar

📅 Race Details View listing related cars

🔄 Cascading Dropdowns in the Booking view (Race → Car)

🔐 Custom Authentication using a tailored ApplicationUser

🧠 Clean Architecture using Repository, Service, and Unit of Work patterns




🛠️ Tech Stack
Backend: ASP.NET Core MVC (.NET 9)

Frontend: Razor Views, Bootstrap, Swiper.js

Database: Entity Framework Core, SQL Server

Tools: Rider IDE, Git, GitHub



🧱 Architecture Overview
Controllers handle requests and forward to Services

Services contain business logic and interact with Repositories

Repositories abstract data access

Unit of Work manages transactions

ViewModels pass structured data to the frontend


git clone https://github.com/MarwanTarekZaky/FormulaRacing.git

dotnet ef database update

dotnet run

Caution "We useed the Sqlserver over docker container on mac m1 so feel free to modify the setting to the default database connection"







