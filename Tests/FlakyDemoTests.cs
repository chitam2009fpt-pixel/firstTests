using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using NUnit.Framework.Interfaces;

//Chữa Thread.Sleep (Không nên dùng Thread.sleed để chờ chức năng hoặc bất cứ thứ gì hiện ra)
//Hãy dùng Explicit Wait để chờ.
//Sử dụng thư viên mới có thể dùng Wait
using OpenQA.Selenium.Support.UI;

namespace FlakyDemoTests;

public class FlakyDemoTests
{
    private IWebDriver driver;
    private const string Url = 
        "https://the-internet.herokuapp.com/dynamic_loading/2";

    [SetUp]
    public void SetUp()
    {
        driver = new ChromeDriver();
        driver.Navigate().GoToUrl(Url);
    }

    [Test]
    public void TaiNoiDung_HienThiHelloWord()
    {
        driver.FindElement(By.CssSelector("#start > button")).Click();

        //DEMO TEST CHẬP CHỜN
        // Thread.Sleep(5985);        //Đoán mò, rơi đúng ranh giới

        // var el = driver.FindElement(By.Id("finish"));

        WebDriverWait wait = new WebDriverWait(driver,TimeSpan.FromSeconds(10));
        var el = wait.Until(d =>
        {
            var element = d.FindElement(By.Id("finish"));

            return element.Displayed ? element : null;
        });

        Assert.That(el.Text, Does.Contain("Hello World!"));
    }

    [TearDown]
    public void TearDown()
    {
        //Nếu muốn kiểm tra chụp ảnh faild thì phải khai báo thư viện nhé
        var ctx = TestContext.CurrentContext;
        if(ctx.Result.Outcome.Status == TestStatus.Failed)
        {
            var anh = ((ITakesScreenshot)driver).GetScreenshot();
            var path = Path.Combine(ctx.WorkDirectory, $"{ctx.Test.Name}.png");
            anh.SaveAsFile(path);
            TestContext.AddTestAttachment(path);
        }
        driver.Quit();
        driver.Dispose();
        
    }

}