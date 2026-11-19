# FirstTests – Selenium Automation Testing Framework

An automated UI testing framework built with **C#, NUnit, and Selenium WebDriver**, implementing the **Page Object Model (POM)** with separated configuration and test data, parallel test execution, and GitHub Actions CI integration.

## 🛠️ Technologies

- **C# / .NET** – Programming language and framework.
- **Selenium WebDriver** – Browser automation.
- **NUnit** – Test framework and parallel test execution.
- **Page Object Model (POM)** – Maintainable test architecture.
- **JSON** – Configuration and test data management.
- **GitHub Actions** – Continuous Integration (CI).

## 📁 Project Structure

```text
FirstTests/
├── .github/
│   └── workflows/
│       └── tests.yml          # GitHub Actions workflow
├── Config/
│   ├── appsettings.json       # Framework configuration
│   ├── ConfigReader.cs        # Configuration reader
│   └── TestConfig.cs          # Test configuration
├── Core/
│   ├── BaseTest.cs            # Test initialization and teardown
│   └── DriverFactory.cs       # WebDriver management
├── Data/
│   ├── UserData.cs            # Test data model
│   └── users.json             # Test user data
├── Pages/
│   ├── BasePage.cs            # Base page
│   ├── InventoryPage.cs       # Inventory page
│   └── LoginPage.cs           # Login page
├── Tests/
│   ├── AlertTests.cs
│   ├── DataDrivenLoginTests.cs
│   ├── FlakyDemoTests.cs
│   ├── IFrameTests.cs
│   ├── LoginTests.cs
│   ├── LuongHopLongTests.cs
│   └── UnitTest1.cs
├── TestResults/               # Test execution reports
├── ketqua/                    # Test output
└── README.md
```

## 🚀 Getting Started

### Prerequisites

- .NET SDK
- Visual Studio or VS Code
- Google Chrome or another supported browser

### 1. Clone Repository

```bash
git clone https://github.com/chitam2009fpt-pixel/firstTests.git
cd firstTests
```

### 2. Restore Dependencies

```bash
dotnet restore
```

### 3. Run Tests

Execute all test cases:

```bash
dotnet test
```

Run tests with detailed console output:

```bash
dotnet test --logger "console;verbosity=normal"
```

## ⚙️ CI/CD – GitHub Actions

The project integrates with **GitHub Actions** to automate test execution.

- Workflow configuration: `.github/workflows/tests.yml`
- Automated test execution through CI.
- Test results and execution status available in the GitHub Actions tab.

## ✨ Key Features

- Page Object Model (POM) architecture.
- Reusable WebDriver setup with `DriverFactory`.
- Separate configuration and test data using JSON.
- Data-driven testing with NUnit.
- Parallel test execution.
- Automated CI pipeline with GitHub Actions.
- Test execution reports.

---

**Repository:** https://github.com/chitam2009fpt-pixel/firstTests
