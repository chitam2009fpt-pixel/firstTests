using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Edge;

namespace FirstTests.Core;
public static class DriverFactory
{
    //Trả về IWebDriver, KHÔNG phải ChromeDriver -> Chỗ ĐA HÌNH phát huy
    public static IWebDriver Create(TestConfig cfg) => cfg.Browser.Trim().ToLower() switch
    {
        "chrome" => new ChromeDriver(ChromeOpts(cfg)),
        "firefox" => new FirefoxDriver(FirefoxOpts(cfg)),
        "edge" => new EdgeDriver(EdgeOpts(cfg)),
        _ => throw new ArgumentException(
            $"Trình duyệt không hỗ trợ: '{cfg.Browser}'. " + 
            "Hợp lệ: chrome | firefox | edge")
    };

    // 3 hàm tạo Options -> slide sau


    private static ChromeOptions ChromeOpts(TestConfig cfg)
    {
        var o = new ChromeOptions();
        if(cfg.Headless) o.AddArgument("--headless=new");  // CÓ "new"
        o.AddArgument($"--window-size={cfg.WindowWidth},{cfg.WindowHeight}");
        o.AddArgument("--disable-notifications");
        return o;
    }


    private static FirefoxOptions FirefoxOpts(TestConfig cfg)
    {
        var o = new FirefoxOptions();
        if(cfg.Headless) o.AddArgument("--headless");      // KHÔNG "new"
        o.AddArgument($"--width={cfg.WindowWidth}");       // và --width/--height
        o.AddArgument($"--height={cfg.WindowHeight}");
        return o;
    }


    //EdgeOptions: y hệt Chrome (cùng nhận Chromium)
    private static EdgeOptions EdgeOpts(TestConfig cfg)
    {
        var o = new EdgeOptions();
        if(cfg.Headless) o.AddArgument("--headless=new");
        o.AddArgument($"--window-size={cfg.WindowWidth},{cfg.WindowHeight}");
        o.AddArgument("--disable-notifications");
        return o;
    }
}