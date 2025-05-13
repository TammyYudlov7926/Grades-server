# Server Project

מערכת שרת פשוטה לניהול ציונים ותלמידים, מבוססת על .NET (C#) ו-Visual Studio.

## 🧰 תכולת הפרויקט

- `Grade.cs`, `Student.cs` – מחלקות המייצגות ציונים ותלמידים.
- `Students.cs` – ממשק ניהול תלמידים.
- `DataSource.cs` – מקור נתונים פנימי (In-Memory).
- `Project.postman_collection.json` – בדיקות API באמצעות Postman.

## 🚀 איך להפעיל את הפרויקט

1. פתח את הקובץ `Server_Project.sln` ב-Visual Studio.
2. וודא שהפרויקט נטען בהצלחה.
3. לחץ `F5` להרצה, או `Ctrl + F5` להרצה ללא דיבאג.

## 🧪 בדיקות API

באמצעות הקובץ `Project.postman_collection.json` תוכל לבדוק את ממשקי ה-API.
- פתח את Postman → Import → גרור את הקובץ פנימה.
- הרץ את הקריאות השונות (GET/POST וכו').

## 🛠 טכנולוגיות בשימוש

- C# (.NET)
- Visual Studio
- Postman

## 📁 מבנה תיקיות

Server Project/
├── .gitignore
├── README.md
├── Server_Project.sln
├── GradeDO/
│ ├── Grade.cs
│ ├── Student.cs
│ ├── DataSource.cs
│ └── Students.cs
└── Project.postman_collection.json

