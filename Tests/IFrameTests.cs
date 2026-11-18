using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

using NUnit.Framework.Interfaces;
using OpenQA.Selenium.DevTools.V154.IndexedDB;

namespace FirstTests;
public class IFrameTests
{
    private IWebDriver driver;

    private const string Url = "https://the-internet.herokuapp.com/iframe";

    [SetUp]
    public void SetUp()
    {
        driver = new ChromeDriver();
        driver.Manage().Window.Maximize();
        driver.Navigate().GoToUrl(Url);
    }


    //ẢNH 2 - quên VÀO: Không tìm thấy phần tử bên trong
    [Test]
    public void KhongSwitch_SeRot()
    {
        //Phần tử trên web có id tinymce này nó nằm trong luồng của iFrame nên 
        // nếu mình tìm trực tiếp mà không có Switch thì sẽ bị lỗi không tìm thấy phần tử

        //Phần tử có Id tinymce là phần tử có ô nhập - một dạng ô nhập liệu.
        var o = driver.FindElement(By.Id("tinymce"));  //NoSuchElementException
        o.SendKeys("Xin chao");
    }

    //ẢNH 3 - có switch: Gõ được
    [Test]
    public void CoSwitch_GoDuoc()
    {
        driver.SwitchTo().Frame("mce_0_ifr");

        var o = driver.FindElement(By.Id("tinymce"));

        //Thuộc tính clear dùng để xóa nội dung cũ nhe.
        // o.Clear();

        //Hoặc có thể dùng cách dưới để xóa nội dung
        //Thao tác y chang như mình tự tay xóa một nội dung

        //Click là mình ấn vào vùng nhập liệu
        o.Click();
        //Control + a giống Ctrl a của bàn phím bên ngoài
        o.SendKeys(Keys.Control + "a");
        //Backspace là nút Delete của bàn phím bên ngoài
        o.SendKeys(Keys.Backspace);

        o.SendKeys("Xin chao FPT Polytechnic");

        Assert.That(o.Text, Does.Contain("FPT Polytechnic"));
    }

    //ẢNH 4 - QUÊN RA: Không thấy phần tử bên ngoài
    [Test]
    public void QuenDefaultContent_SeRot()
    {
        driver.SwitchTo().Frame("mce_0_ifr");
        driver.FindElement(By.Id("tinymce")).SendKeys("abc");

        //Nếu bỏ dòng dưới -> hàm rớt (đỏ | failded). Thêm dòng phía dưới -> hàm đậu (xanh | succeded)
        //driver.SwitchTo().DefaultContent();

        var h3 = driver.FindElement(By.TagName("h3")).Text;  //Tìm phần tử h3 ở trang bên ngoài iframe
        Assert.That(h3, Does.Contain("TinyMCE"));
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