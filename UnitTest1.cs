using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace FirstTests;

public class Tests
{
    private IWebDriver driver;

    [SetUp]
    public void Setup()
    {
        driver = new ChromeDriver(); //Mở trình duyệt trước mỗi test
    }

    [Test]
    public void MoTrangChu_ThanhCong()
    {
        var driver = new ChromeDriver();
        driver.Navigate().GoToUrl("https://saucedemo.com");
        Assert.That(driver.Title, Is.Not.Empty);
        Thread.Sleep(300); //Giữ trình duyệt 3 giây để xem kết quả 
    }

    [TearDown]

    public void TestDown()
    {
        driver.Quit(); //Đóng trình duyệt sau mỗi test
        driver.Dispose(); //Giải phóng tài nguyên.
    }

}
