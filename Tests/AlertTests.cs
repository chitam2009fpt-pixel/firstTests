using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using NUnit.Framework.Interfaces;

namespace FirstTests;

public class AlertTests
{
    private IWebDriver driver;

    private const string Url = 
        "https://the-internet.herokuapp.com/javascript_alerts";

    private readonly By nutAlert = By.XPath("//button[@onclick='jsAlert()']");

    private readonly By nutConfirm = By.XPath("//button[@onclick='jsConfirm()']");

    private readonly By nutPrompt = By.XPath("//button[@onclick='jsPrompt()']");

    private readonly By KetQua = By.Id("result");


    [SetUp]
    public void SetUp()
    {
        driver = new ChromeDriver();
        driver.Manage().Window.Maximize();
        driver.Navigate().GoToUrl(Url);
    }


    //Thực hiện ca test
    [Test]
    public void JsAlert_Accept_ThanhCong()
    {
        driver.FindElement(nutAlert).Click();

        IAlert alert = driver.SwitchTo().Alert(); // Dòng này dùng để chuyển ngữ cảnh bấm nút sang hợp thoại.
                                                    //Từ giây phút ấy, biến alert đại diện cho hộp thoại đó.

        Assert.That(alert.Text, Is.EqualTo("I am a JS Alert"));  //Kiếm trước khi bấm

        //Accept này có thể hiểu theo nghĩa là nhấn nút chấp nhận. Vô trang web và hãy ấn nút đầu là sẽ rõ, có thể xem các dòng tương tự bên dưới
        alert.Accept();

        Assert.That(driver.FindElement(KetQua).Text, Is.EqualTo("You successfully clicked an alert"));
    }

    [Test]
    public void JsConfirm_Dismiss_HienCancel()
    {
        driver.FindElement(nutConfirm).Click();

        IAlert alert = driver.SwitchTo().Alert();
        Assert.That(alert.Text, Is.EqualTo("I am a JS Confirm"));

        //Dismiss có thể hiểu là bấm nút cancel đó.
        alert.Dismiss();  //    DÒNG NÀY MÌNH SẼ KÊU NÓ BẤM CANCEL ĐẤY NHÉ THẰNG CHO NGU

        Assert.That(driver.FindElement(KetQua).Text, Is.EqualTo("You clicked: Cancel"));
    }

    [Test]
    public void jsPrompt_NhapChu_HienDungChu()
    {
        driver.FindElement(nutPrompt).Click();

        IAlert alert = driver.SwitchTo().Alert();

        //Gọi send keys để gõ chữ vào ô nhấp nha.
        alert.SendKeys("I want to be a Manager");
        alert.Accept();

        Assert.That(driver.FindElement(KetQua).Text, Is.EqualTo("You entered: I want to be a Manager"));
    }


    [TearDown]
    public void TearDown()
    {
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