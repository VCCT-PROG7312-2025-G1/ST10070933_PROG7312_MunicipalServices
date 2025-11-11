# ST10070933_PROG7312_MunicipalServices

**Links**  
- 🧩 [GitHub Repository](https://github.com/VCCT-PROG7312-2025-G1/ST10070933_PROG7312_MunicipalServices)  
- 🎥 [YouTube Demonstration Video](https://youtu.be/HEfFEwyDyWY)

---

A web-based **Municipal Services Portal** built with ASP.NET Core MVC.  

The project allows residents to **report issues**, **view local events and announcements**, and track **service requests** through an intuitive interface.

---

## 🏗️ Project Overview

This project was developed to demonstrate practical implementations of:

- ASP.NET Core MVC architecture and dependency injection  
- Advanced C# data structures for optimized data management  
- In-memory data persistence without an external database  
- Bootstrap 5 UI for a responsive, modern user experience  
- Logical separation of concerns using models, controllers, and services

---

## ⚙️ Features

### 🧾 Issue Reporting (Part 1)
- Users can submit issues related to municipal services (e.g., road damage, water leaks).  
- Submitted issues are stored **in-memory** via the `InMemoryDataService`.  
- Provides a confirmation message upon successful submission.

---

### 📅 Local Events & Announcements (Part 2)
- Displays a list of community events.  
- Events can be:
  - **Sorted** by Title, Category, or Date.  
  - **Filtered** by Category or Date.  
- Recommended events are shown based on **user search trends**.  
- Upcoming events are managed using a **queue structure**.  
- Data is initially seeded through `InMemoryDataService` (no database required).

---

### 🚧 Service Request Status (Part 3 - Final Implementation)
- Users can submit, view, and track the progress of service requests.  
- Each request is given a unique ID and stored in memory.  
- Uses advanced data structures to manage and optimize service request data:  
  - ✅ AVL Tree – balances data dynamically for faster retrieval by ID and priority  
  - ✅ Min Heap – manages urgent, high-priority requests efficiently  
  - ✅ Graph – represents departmental routing and service relationships  
  - ✅ Dijkstra’s Algorithm – finds the shortest processing route between departments  
  - ✅ Prim’s MST – identifies optimal resource paths for minimal overhead  
  - ✅ DFS Traversal – used for exploring service dependencies within the system  
- Real-time priority sorting and routing visualization enhance usability and realism.

---

## 🧠 Architecture

### **Models**
`Issue` and `Event` represent data entities used in the system.

### **Controllers**
- `HomeController` — Manages the homepage and navigation.  
- `IssuesController` — Handles issue submission and listing.  
- `EventsController` — Displays local events, filtering, and sorting.  
- `ServiceRequestsController` — Handles submission, sorting, searching, and routing of service requests.

### **Services**
`IDataService.cs` — Service interface defining CRUD operations for issues, events, and requests.  
`InMemoryDataService` implements the `IDataService` interface, handling:
- Manages seeded and runtime data  
- Integrates tree, heap, and graph data structures for optimized storage  
- Demonstrates concurrent thread-safe access using locks  

### **Data Structures (under Services/DataStructures/)**
- `BSTNode.cs` — Foundational node structure for binary trees  
- `AVLTree.cs` — Self-balancing tree used for fast ID and priority searches  
- `MinHeap.cs` — Manages urgent requests efficiently (highest priority = top node)  
- `Graph.cs` — Defines departments and routes as graph nodes/edges  
- `GraphAlgorithms.cs` — Includes Dijkstra’s shortest path, Prim’s MST, and DFS traversal  

---

## 💻 Technologies Used

| Category | Technology |
|-----------|-------------|
| Framework | ASP.NET Core MVC |
| Language | C# |
| Frontend | Bootstrap 5, HTML, CSS |
| IDE | Visual Studio 2022 |
| Data Storage | In-memory collections (no database required) |

---

## 🚀 How to Run the Project

### 1️⃣ Open in Visual Studio 2022
- Go to **File → Open → Project/Solution**
- Select the `.sln` file.

### 2️⃣ Build the Project
- Click **Build → Build Solution** (or press `Ctrl+Shift+B`).

### 3️⃣ Run the Application
- Press **F5** or click the **Run** button.
- The app will launch in your browser (typically `https://localhost:5001` or similar).

### 4️⃣ Explore the Application
Navigate between:
- 🏠 **Home** — Welcome/landing page  
- 🧾 **Report Issues** — Submit and view issues  
- 📅 **Local Events** — Browse, filter, and sort events  
- 🚧 **Service Requests** — Submit and track requests, view priority and department routing

---

## 🧩 Data & Storage

- No external or local database is used.  
- All data is handled in-memory using:
  - `List<Issue>` for user-reported issues  
  - `SortedDictionary<DateTime, List<Event>>` for events grouped by date  
  - `Queue<Event>` for upcoming events  
  - `AVLTree<ServiceRequest>` and `MinHeap<ServiceRequest>` for service request organization  
  - `Graph<ServiceRequest>` for departmental routing and network analysis  
- All data resets to seeded values when the application restarts.

---

## 🌟 Highlights

- Fully functional three-module municipal management system.  
- Intuitive dark-blue Bootstrap interface for accessibility and consistency.  
- Advanced C# data structure integration within ASP.NET MVC.  
- Strong separation of concerns between models, controllers, and services.

---

## 📂 Project Structure

```text
ST10070933_PROG7312_MunicipalServices/
├── Controllers/
│   ├── HomeController.cs
│   ├── IssuesController.cs
│   ├── EventsController.cs
│   └── ServiceRequestsController.cs
├── Models/
│   ├── Issue.cs
│   ├── Event.cs
│   └── ServiceRequest.cs
├── Services/
│   ├── IDataService.cs
│   ├── InMemoryDataService.cs
│   └── DataStructures/
│       ├── BSTNode.cs
│       ├── AVLTree.cs
│       ├── MinHeap.cs
│       ├── Graph.cs
│       └── GraphAlgorithms.cs
├── Views/
│   ├── Home/
│   ├── Issues/
│   ├── Events/
│   └── ServiceRequests/
└── wwwroot/
    ├── css/
    ├── js/
    └── images/
```
---

## 🤖 AI Usage

During development, AI tools were used strictly for learning and design guidance, not automated coding.  
All logic and implementation were written and tested by the developer.

**Tools used:**
- [OpenAI ChatGPT (OpenAI, 2024)](https://openai.com/) – Used for guidance on programming concepts, problem-solving, and code review.  
- [Claude AI (Anthropic, 2024)](https://claude.ai/) – Used for design advice and exploring design patterns.

AI assistance was mainly used to:
- Implement complex data structures (AVL Tree, Heap, Graph)  
- Debugging and code refactoring advice  
- Bootstrap UI enhancement and accessibility suggestions  
- Improving MVC layer organization and naming consistency  

All code was written, tested, and fully understood by the developer.  
AI tools served as educational references, similar to documentation, tutorials, or forums.











