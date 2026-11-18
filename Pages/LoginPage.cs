// using OpenQA.Selenium;

// namespace LoginTests.Pages;

// public class LoginPage : BasePage
// {
//     private const string Url = "https://www.saucedemo.com/";

//     private readonly By _txtTaiKhoan = By.Id("user-name");
//     private readonly By _txtMatKhau = By.Id("password");
//     private readonly By _btnDangNhap = By.Id("login-button");

//     public LoginPage(IWebDriver driver) : base(driver) { }

//     public LoginPage Mo()
//     {
//         Driver.Navigate().GoToUrl(Url);
//         return this;
//     }

//     public InventoryPage DangNhap(string taiKhoan = "standard_user", string matKhau = "secret_sauce")
//     {
//         Wait.Until(d => d.FindElement(_txtTaiKhoan).Displayed);
//         Driver.FindElement(_txtTaiKhoan).SendKeys(taiKhoan);
//         Driver.FindElement(_txtMatKhau).SendKeys(matKhau);
//         Driver.FindElement(_btnDangNhap).Click();
//         return new InventoryPage(Driver);
//     }
// }


using OpenQA.Selenium;

namespace FirstTests.Pages;

public class LoginPage : BasePage
{
    public const string Url = "https://www.saucedemo.com/";
    private readonly By oTaiKhoan = By.Id("user-name");
    private readonly By oMatKhau = By.Id("password");
    private readonly By nutLogin = By.Id("login-button");
    private readonly By loi = By.CssSelector("h3[data-test='error']");

    public LoginPage(IWebDriver d) : base(d) { }

    public LoginPage Mo()
    {
        driver.Navigate().GoToUrl(Url);
        return this;
    }

    public InventoryPage DangNhap(string tk = "standard_user", string mk = "secret_sauce")
    {
        Tim(oTaiKhoan).Clear();
        Tim(oTaiKhoan).SendKeys(tk);
        Tim(oMatKhau).Clear();
        Tim(oMatKhau).SendKeys(mk);
        Tim(nutLogin).Click();

        return new InventoryPage(driver);       //TRẢ VỀ TRANG TIẾP THEO
    }

    public string LayThongBaoLoi() => Tim(loi).Text;
}