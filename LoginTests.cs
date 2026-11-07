using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace LoginTests;

public class LoginTests
{
    private IWebDriver driver;

    [SetUp]
    public void SetUp()
    {
        driver = new ChromeDriver();
        driver.Manage().Window.Maximize();
        driver.Navigate().GoToUrl("https://www.saucedemo.com/");
    }

    [TearDown]
    public void TearDown()
    {
        driver.Quit();
        driver.Dispose();
    }

    // [Test]
    // public void DangNhap_DungTaiKhoan_ThanhCong()
    // {
    //     //Lấy id của user name - sendkeys là để lấy dữ liệu trên web (đăng nhập đúng tên)
    //     driver.FindElement(By.Id("user-name"))
    //     .SendKeys("standard_user");

    //     //Password và login tương tự.
    //     driver.FindElement(By.Id("password"))
    //     .SendKeys("secret_sauce");

    //     //Ở đây có click là bởi vì nó là nút và mình phải click vào nó mới hoạt động.
    //     driver.FindElement(By.Id("login-button")).Click();

    //     //Khẳng định khi đăng nhập thành công thì đường dẫn phải có đúng chuỗi
    //     Assert.That(driver.Url,
    //         Does.Contain("inventory.html"));
    // }
    //                                  ||
    //                                  ||
    //                                 \\//
    //                                  \/
    
    //Chuyển nó thanh dạng hàm để tái sử dụng cho các phương thức khác

    private void DangNhap(string taiKhoan = "standard_user", string matKhau = "secret_sauce")
    {
        driver.FindElement(By.Id("user-name")).SendKeys(taiKhoan);
        driver.FindElement(By.Id("password")).SendKeys(matKhau);
        driver.FindElement(By.Id("login-button")).Click();
    }

    [Test]
    public void DangNhap_DungTaiKhoan_ThanhCong()
    {
        //Gọi hàm đăng nhập đúng mật khẩu
        DangNhap();
        //Khẳng định khi đăng nhập thành công thì đường dẫn phải có đúng chuỗi
        Assert.That(driver.Url,Does.Contain("inventory.html"));
    }

    [Test]
    public void ThuThapDanhSachSanPham()
    {
        //Gọi lại hàm đăng nhập vì hàm thu thập này phải đăng nhập mới sử dụng được
        DangNhap();

        var items = driver.FindElements(By.ClassName("inventory_item"));

        foreach(var item in items)
        {
            var name = item.FindElement(By.ClassName("inventory_item_name")).Text;
            var price = item.FindElement(By.ClassName("inventory_item_price")).Text;
            Console.WriteLine($"{name} - {price}");
        }

        Assert.That(items.Count, Is.GreaterThan(0));
    }

}