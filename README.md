[![.NET Version](https://img.shields.io/badge/.NET-10.0--windows-blueviolet?style=flat-for-the-badge&logo=.net)](https://dotnet.microsoft.com/en-us/)
[![Platform](https://img.shields.io/badge/Platform-Windows%20Desktop-blue?style=flat-for-the-badge&logo=windows)](https://www.microsoft.com/windows)
 
 # Vibes – Modern Music Streaming Client (Windows Desktop)

Vibes is a high-performance Windows desktop audio streaming client built using **C#** and **WinForms (.NET 10)**. This application was developed as a **Portfolio Project** to demonstrate advanced native UI rendering, reactive programming, desktop state management, and production-grade software architecture.

The core focus of this project was to push the boundaries of traditional Windows Forms, transforming it into a modern, responsive, and visually compelling audio player without relying on bloated third-party UI frameworks.

---

## 🚀 Architectural & Technical Highlights

This project serves as a showcase for several mid-to-senior level desktop development concepts:

* **Advanced Custom GDI+ Rendering:** Built seamless, flicker-free custom controls (such as `AudioVolumeSlider` and `MediaDisplayControl`) utilizing double-buffering (`OptimizedDoubleBuffer`) and anti-aliased native drawing.
* **Separation of Concerns (Partial Architecture):** Implemented a strict design pattern splitting complex UI controls into distinct files (e.g., `Control.cs` for business logic/events and `Control.Drawing.cs` for GDI+ rendering), maximizing maintainability and testability.
* **Production Deployment & Sandboxing:** Resolved complex operating system sandboxing issues related to `C:\Program Files` write restrictions by dynamically migrating the SQLite database, application logging, and WebView2 cache directories directly into the OS user's secure `%AppData%` space.
* **Asynchronous Native Interop:** Embedded a seamless web authentication flow via Microsoft WebView2, combined with asynchronous background thumbnail extraction and downscaling algorithms (`HighQualityBicubic`) to prevent UI thread blocking.

---

## 🛠️ Tech Stack & Key Components

* **Framework:** .NET 10 (Windows Forms SDK)
* **Data Layer:** Entity Framework Core (EF Core) with SQLite for local user data synchronization (Playlists, Track History, Cache management).
* **Authentication:** Auth0 OIDC Client integration via a securely isolated OAuth 2.0 desktop login flow.
* **Audio Core:** NAudio for native low-level Windows audio stream handling, playback tracking, and real-time volume interpolation.
* **API Integrations:** Jamendo Licensing API for high-quality audio asset fetching and remote cover art metadata synchronization.
* **Logging & Diagnostics:** Serilog structured file logging with rolling daily intervals and runtime trace diagnostics.

---

## 💡 AI-Assisted Development & Engineering Velocity

To achieve optimal engineering speed and maintain a high standard of architectural design, this project utilized state-of-the-art AI collaboration methodologies:

* **GitHub Copilot:** Leveraged heavily during the development lifecycle to eliminate friction, rapidly generating repetitive boilerplate code, data transfer objects (DTOs), and standard service layer mappings, allowing full focus on core systems.
* **Gemini AI Collaboration:** Partnered closely with Gemini as an expert technical consultant to engineer the application’s custom design architecture. This included optimizing advanced GDI+ layout calculations, implementing dynamic dominant-color extraction routines, troubleshooting runtime .NET assembly manifests, and structuring the desktop installers for testing.
