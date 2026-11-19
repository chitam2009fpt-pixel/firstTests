using System.Globalization;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework.Interfaces;

namespace FirstTests;

public class LuongTongHopTests
{
    private IWebDriver driver;
    private WebDriverWait wait;

    [SetUp]
    public void SetUp()
    {
        driver = new ChromeDriver();
        driver.Manage().Window.Maximize();
        driver.Navigate().GoToUrl("https://www.saucedemo.com/");

        //Lưu biến wait (chờ) với thời gian chờ là 15 giây
        wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
    }

    [Test]
    public void SapXepGiaTangDan_DanhSachDungThuTu()
    {
        //1. Đăng nhập
        driver.FindElement(By.Id("user-name")).SendKeys("standard_user");
        driver.FindElement(By.Id("password")).SendKeys("secret_sauce");
        driver.FindElement(By.Id("login-button")).Click();

        //2. Chờ danh sách sản phẩm thực sự hiện ra (REACH RENDER sau khi tải trang)
        wait.Until(d => d.FindElements(By.ClassName("inventory_item")).Count == 6);

        //3. Sắp xếp giá thấp -> cao
        // Dùng SelectElement chọn sắp xếp giá thấp đến cao                
        // Bên trang web đã có chức năng sort giá, mình dùng SelectElement để chọn chức năng đó.
        var combo = new SelectElement(driver.FindElement(By.ClassName("product_sort_container")));
        //Hàm SelectByValue là để chọn giá trị value của cái hàng thẻ đó     vd: <option   value="lohi"  >Price (low to high)</option>
        combo.SelectByValue("lohi");

        //Thử đổi từ lohi sang az để thử luồng nghiệp vụ sai, từ sắp xếp theo giá thành theo tên.

        //4. Lấy giá và chuyển sang số
        // InvariantCulture có nghĩa là hãy dùng quy ước quốc tế 
        var gia = driver.FindElements(By.ClassName("inventory_item_price"))
                        .Select(e => decimal.Parse(e.Text.Replace("$", ""),CultureInfo.InvariantCulture)).ToList();     //Xem mục 2
        // Ở trên dùng thêm CultureInfo.InvariantCulture là vì nếu máy tính bạn đặt vùng là VN 
        // thì theo quy ước Tiếng Việt dấu phẩy mới là dấu thập phân còn dấu chấm là dấu phân nhóm nghìn
        // Nghĩa là chuỗi 29.99 sẽ được hiểu thành 2999. Giá bị sai gấp 100 lần mà 
        // |Sắp xếp thứ tự vẫn đúng nên assert vẫn pass| test xanh => nó sai một cách âm thầm do sai số chuyển đổi.
        // InvariantCulture có nghĩa là hãy dùng quy ước quốc tế 


        //Chỗ này dùng để viết ra còn console cho đẹp.
        TestContext.Progress.WriteLine(string.Join(" | ",gia));

        //5. Kiểm tra đúng nghiệp vụ
        Assert.That(gia, Is.Ordered);
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