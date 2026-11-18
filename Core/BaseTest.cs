using FirstTests.Core;
using FirstTests.Pages;
using FirstTests.Utilities;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.DevTools;

public abstract class BaseTest
{
    protected IWebDriver driver = null!;
    protected TestConfig cfg = null!;

    [SetUp]
    public void BaseSetup()
    {   //Cách để có thể dùng log.info trong BaseTesst là tạo class tĩnh và (Mở copilot sẽ rõ)
        //Tên ở dưới là tên ca test đang chạy
        var ten = TestContext.CurrentContext.Test.Name;
        BasePage.Log.Info($"===== BẮT ĐẦU: {ten} =====");
        cfg = ConfigReader.Load();
        driver = DriverFactory.Create(cfg);
        BasePage.Log.Info($"Mở: {cfg.Browser}, tới {cfg.BaseUrl}");
        driver.Navigate().GoToUrl(cfg.BaseUrl);
    }

    //KHÔNG Maximize(): headless bỏ qua nó -> dùng --window-size ở factory


    [TearDown]
    public void BaseTearDown()
    {
        var kq = TestContext.CurrentContext.Result.Outcome.Status;
        try
        {
            if(kq == TestStatus.Failed)
            {
                BasePage.Log.Error($"RỚT: {TestContext.CurrentContext.Test.Name}");
                ScreenshotHelper.Chup(driver);
            }
        }
        finally
        {
            BasePage.Log.Info("XONG: OK");
            driver?.Quit(); driver?.Dispose();  //LUÔN đóng
        }
    }
}