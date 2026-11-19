// using OpenQA.Selenium;

// namespace LoginTests.Pages;

// public record SanPham(string Ten, string Gia);

// public class InventoryPage : BasePage
// {
//     private readonly By _items = By.ClassName("inventory_item");
//     private readonly By _tenSanPham = By.ClassName("inventory_item_name");
//     private readonly By _giaSanPham = By.ClassName("inventory_item_price");

//     public InventoryPage(IWebDriver driver) : base(driver) { }

//     public List<SanPham> LayDanhSachSanPham()
//     {
//         // Chờ danh sách render xong rồi mới đọc
//         Wait.Until(d => d.FindElements(_items).Count > 0);

//         return Driver.FindElements(_items)
//             .Select(item => new SanPham(
//                 item.FindElement(_tenSanPham).Text,
//                 item.FindElement(_giaSanPham).Text))
//             .ToList();
//     }
// }


using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace FirstTests.Pages;

public class InventoryPage : BasePage
{
    private readonly By tieuDe = By.ClassName("title");
    private readonly By sanPham = By.ClassName("inventory_item");
    private readonly By gia = By.ClassName("inventory_item_price");
    private readonly By combo = By.ClassName("product_sort_container");

    public InventoryPage(IWebDriver driver) : base(driver) { }


    //nó là một cách viết tắt (syntactic sugar) thay thế cho cặp dấu ngoặc nhọn { } và từ khóa return. 
    //Thay vì viết một hàm đầy đủ như thế này:
    //     public bool DaHienThi() 
    // {
    //     return Tim(tieuDe).Text == "Products";
    // }


    // ==== BẢN CŨ (BÀI 4) - đem sang bài 5 sẽ hỏng, trả lỗi Time out after 10 seconds
    //public bool DaHienThi() => Tim(tieuDe).Text == "Products";
    // - Với locked_out_user: trang SP KHÔNG BAO GIỜ hiện
    // - Tim() chở đủ 10 giây rồi NÉM WebDriverTimeoutException
    // - dòng Assert(..., Is. EqualTo(false)) KHÔNG BAO GIỜ chạy tới
    // - test đỏ vì TIMEOUT, không phải vi assert
    // ==== Bản sửa cho bài 5 ====

    public bool DaHienThi()
    {
        var w = new WebDriverWait(driver, TimeSpan.FromSeconds(3));
        try
        {
            return w.Until(d => d.FindElements(tieuDe).Count > 0 && d.FindElement(tieuDe).Text == "Products");
        }
        catch(WebDriverTimeoutException)
        {
            return false;    //Không vào được = câu TRẢ LỜI, không phải sự cố
        }
    }

    public int SoSanPham() => driver.FindElements(sanPham).Count;
    public InventoryPage SapXep(string value)
    {
        new SelectElement(Tim(combo)).SelectByValue(value);
        return this;  //Vẫn ở trang này -> trả về chính nó
    }
    public List<string> LayDanhSachGia() => 
        driver.FindElements(gia).Select(e => e.Text).ToList();
}