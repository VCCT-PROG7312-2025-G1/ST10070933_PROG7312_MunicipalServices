# ST10070933\_PROG7312\_MunicipalServices

Links 
GitHub -https://github.com/VCCT-PROG7312-2025-G1/ST10070933\_PROG7312\_MunicipalServices
YouTube demonstration video - https://youtu.be/HEfFEwyDyWY



A web-based \*\*Municipal Services Portal\*\* built with ASP.NET Core MVC.  

The project allows residents to \*\*report issues\*\*, \*\*view local events and announcements\*\*, and track \*\*service requests\*\* through an intuitive interface.



---



\## 🏗️ Project Overview



This project was developed to demonstrate practical implementations of:

\- ASP.NET Core MVC architecture

\- In-memory data management using custom services

\- Bootstrap 5 for responsive UI design

\- Core C# collections (`List`, `Queue`, `HashSet`, `SortedDictionary`)

\- Filtering, sorting, and basic recommendation logic



---



\## ⚙️ Features



\### 🧾 Issue Reporting (Part 1)

\- Users can submit issues related to municipal services (e.g., road damage, water leaks).

\- Submitted issues are stored \*\*in-memory\*\* via the `InMemoryDataService`.



\### 📅 Local Events \& Announcements (Part 2)

\- Displays a list of community events.

\- Events can be:

&nbsp; - \*\*Sorted\*\* by Title, Category, or Date.

&nbsp; - \*\*Filtered\*\* by Category or Date.

\- Recommended events are shown based on \*\*user search trends\*\*.

\- Upcoming events are managed using a \*\*queue structure\*\*.

\- Data is initially seeded through `InMemoryDataService` (no database required).



\### 🚧 Service Request Status (Part 3 - Placeholder)

\- Planned section for future implementation of service request tracking.



---



\## 🧠 Architecture



\- \*\*Models\*\*  

&nbsp; `Issue` and `Event` represent data entities used in the system.



\- \*\*Controllers\*\*  

&nbsp; - `HomeController` — Manages the homepage and navigation.

&nbsp; - `IssuesController` — Handles issue submission and listing.

&nbsp; - `EventsController` — Displays local events, filtering, and sorting.



\- \*\*Services\*\*  

&nbsp; `InMemoryDataService` implements the `IDataService` interface, handling:

&nbsp; - Storage and retrieval of issues and events.

&nbsp; - Seed data creation.

&nbsp; - Event search, sorting, and recommendation logic.



---



\## 💻 Technologies Used



| Category | Technology |

|-----------|-------------|

| Framework | ASP.NET Core MVC |

| Language | C# |

| Frontend | Bootstrap 5, HTML, CSS |

| IDE | Visual Studio 2022 |

| Data Storage | In-memory collections (no database required) |



---

## 🚀 How to Run the Project


Open in Visual Studio 2022

-Go to File → Open → Project/Solution

\-Select the .sln file.



Build the Project

\-Click Build → Build Solution (or press Ctrl+Shift+B).



Run the Application

\-Press F5 or click the Run button.

\-The app will launch in your browser (typically https://localhost:5001 or similar).



Explore the Application

\-Navigate between:

&nbsp; -🏠 Home — Welcome/landing page

&nbsp; -🧾 Report Issues — Submit and view issues

&nbsp; -📅 Local Events — Browse, filter, and sort events



---

🧩 Data \& Storage

-No external or local database is used.

-All data is handled in-memory using:

&nbsp; -List<Issue> for user-reported issues.

&nbsp; -SortedDictionary<DateTime, List<Event>> for events grouped by date.

&nbsp; -Queue<Event> for upcoming events.

-When the application restarts, all data resets to the seeded sample data in InMemoryDataService.



---

🌟 Highlights

-Clean, responsive Bootstrap-based UI.

-Sorting and filtering for event listings.

-Event recommendations powered by recent user searches.

-Demonstrates practical collection usage (HashSet, Queue, SortedDictionary).



---


📂 Project Structure

ST10070933\_PROG7312\_MunicipalServices/

│

├── Controllers/

│   ├── HomeController.cs

│   ├── IssuesController.cs

│   └── EventsController.cs

│

├── Models/

│   ├── Issue.cs

│   └── Event.cs

│

├── Services/

│   ├── IDataService.cs

│   └── InMemoryDataService.cs

│

├── Views/

│   ├── Home/

│   ├── Issues/

│   └── Events/

│

└── wwwroot/

&nbsp;   ├── css/

&nbsp;   ├── js/

&nbsp;   └── images/


---

### AI Usage

During the development of this project, AI tools were used as supplementary resources to support understanding of programming concepts and explore modern design approaches. These tools provided guidance, but all coding and implementation were carried out alone by the developer.

Tools used:

- OpenAI ChatGPT (OpenAI, 2024) – Available at: https://openai.com/ – Used throughout development for guidance on programming concepts, problem-solving, and code review.
- Claude AI (Anthropic, 2024) – Available at: https://claude.ai/ – Used for design advice and exploring design patterns.

AI assistance was mainly used to:
- Understand and implement complex data structures like SortedDictionary and search algorithms
- Learn CSS techniques and UI design principles
- Review MVC best practices and clean architecture approaches
- Receive debugging tips and optimization suggestions
- Design the event recommendation logic
- Explore advanced LINQ queries and async/await patterns

All code was written, tested, and fully understood by the developer. AI tools served as educational references, similar to documentation, tutorials, or forums.







