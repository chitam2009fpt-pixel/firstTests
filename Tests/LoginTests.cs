// using LoginTests.Pages;
// using OpenQA.Selenium;
// using OpenQA.Selenium.Chrome;
// using NUnit.Framework.Interfaces;

// namespace FirstTests;

// public class LoginTests
// {
//     private IWebDriver _driver;
//     private LoginPage _loginPage;

//     [SetUp]
//     public void SetUp()
//     {
//         _driver = new ChromeDriver();
//         _driver.Manage().Window.Maximize();
//         _loginPage = new LoginPage(_driver).Mo();
//     }

//     [Test]
//     public void DangNhap_DungTaiKhoan_ThanhCong()
//     {
//         var inventoryPage = _loginPage.DangNhap();

//         Assert.That(inventoryPage.CurrentUrl, Does.Contain("inventory.html"));
//     }

//     [Test]
//     public void ThuThapDanhSachSanPham()
//     {
//         var sanPhams = _loginPage.DangNhap().LayDanhSachSanPham();

//         foreach (var sp in sanPhams)
//             Console.WriteLine($"{sp.Ten} - {sp.Gia}");

//         Assert.That(sanPhams.Count, Is.GreaterThan(0));
//     }

//     [TearDown]
//     public void TearDown()
//     {
//         var ctx = TestContext.CurrentContext;
//         if (_driver != null && ctx.Result.Outcome.Status == TestStatus.Failed)
//         {
//             var anh = ((ITakesScreenshot)_driver).GetScreenshot();
//             var path = Path.Combine(ctx.WorkDirectory, $"{ctx.Test.Name}.png");
//             anh.SaveAsFile(path);
//             TestContext.AddTestAttachment(path);
//         }

//         _driver?.Quit();
//         _driver?.Dispose();
//     }
// }


using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using NUnit.Framework.Interfaces;
using FirstTests.Pages;

namespace FirstTests;

[Category("Smoke")]    //Cả class thuộc nhóm Smoke
public class LoginTests
{
    private IWebDriver driver;
    private LoginPage loginPage;

    [SetUp]
    public void SetUp()
    {
        driver = new ChromeDriver();
        driver.Manage().Window.Maximize();
        driver.Navigate().GoToUrl(LoginPage.Url);
        loginPage = new LoginPage(driver);
    }

    // [Test]
    // public void DangNhap_DungTaiKhoan_ThanhCong()
    // {
    //     var trangSP = loginPage.DangNhap("standard_user", "secret_sauce");
    //     Assert.That(trangSP.DaHienThi(), Is.True);
    // }


    [TestCase("standard_user","secret_sauce",true)]   // true và false ở đây tượng chưng cho đăng nhập được hay không được.
    [TestCase("locked_out_user","secret_sauce",false)]
    [TestCase("problem_user","secret_sauce",true)]
    [TestCase("khong_ton_tai","sai_mat_khau",false)]
    public void DangNhap_NhieuTaiKhoan(string tk, string mk, bool mongDoiThanhCong)
    {
        var trangSP = new LoginPage(driver).DangNhap(tk,mk);
        Assert.That(trangSP.DaHienThi(), Is.EqualTo(mongDoiThanhCong));
    }

    
    [Test]
    [Category("Login")]
    public void DangNhap_DungTaiKhoan_ThanhCong()
    {
        var trangSP = loginPage.DangNhap("standard_user", "secret_sauce");
        Assert.That(trangSP.DaHienThi(), Is.True);
    }


    [Test]
    public void DangNhap_TaiKhoanBiKhoa_HienLoi()
    {
        loginPage.DangNhap("locked_out_user", "secret_sauce");

        Assert.That(loginPage.LayThongBaoLoi(),
                    Does.Contain("locked out"));
    }

    [TearDown]
    public void TearDown()
    {
        var ctx = TestContext.CurrentContext;
        if (driver != null && ctx.Result.Outcome.Status == TestStatus.Failed)
        {
            var anh = ((ITakesScreenshot)driver).GetScreenshot();
            var path = Path.Combine(ctx.WorkDirectory, $"{ctx.Test.Name}.png");
            anh.SaveAsFile(path);
            TestContext.AddTestAttachment(path);
        }

        driver?.Quit();
        driver?.Dispose();
    }
}


