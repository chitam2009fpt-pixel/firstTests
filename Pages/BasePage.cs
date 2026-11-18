// using OpenQA.Selenium;
// using OpenQA.Selenium.Support.UI;

// namespace LoginTests.Pages;

// public abstract class BasePage
// {
//     protected readonly IWebDriver Driver;
//     protected readonly WebDriverWait Wait;

//     protected BasePage(IWebDriver driver, int timeoutGiay = 10)
//     {
//         Driver = driver;
//         Wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutGiay));
//     }

//     // Chỉ expose dữ liệu để Test tự Assert, Page không Assert
//     public string CurrentUrl => Driver.Url;
// }

using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace FirstTests.Pages;

public abstract class BasePage
{
    protected readonly IWebDriver driver;
    protected readonly WebDriverWait wait;

    protected BasePage(IWebDriver driver)
    {
        this.driver = driver;
        wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
    }

    public string CurrentUrl => driver.Url;

    // Tìm phần tử KÈM chờ mọi trang đều dùng chung
    protected IWebElement Tim(By by) => wait.Until(d => d.FindElement(by));

    public static class Log
    {
        public static void Info(string msg) =>
            TestContext.Progress.WriteLine($"[{DateTime.Now:HH:mm:ss}] [INFO] {msg}");


        public static void Error(string msg) =>
            TestContext.Progress.WriteLine($"[{DateTime.Now:HH:mm:ss}] [ERROR] {msg}");
    }
}
