using System.Text.Json;
using FirstTests.Data;
using FirstTests.Pages;

//Lớp ánh xạ: UserData (UserName, Password, ThanhCong, GhiChu)
//Tên thuộc tính phải khớp khóa JSON - phân biệt hoa/thường!
namespace FirstTests.Tests;

public class DataDrivenLoginTest : BaseTest{
    private static IEnumerable<TestCaseData> TuJson()   //Bắt buộc STATIC
    {
        var path = Path.Combine(AppContext.BaseDirectory,"Data","users.json"); 
        var ds = JsonSerializer.Deserialize<List<UserData>>(File.ReadAllText(path))!; // Biến chuỗi JSON thành danh sách UserData
        var i = 0;
        foreach(var u in ds)
        {
            i++;
            yield return new TestCaseData(u.Username, u.Password, u.ThanhCong)
                .SetName($"DangNhap_{i:00}_{u.Username}");
        }
    }   

    [TestCaseSource(nameof(TuJson))]   // nameof: gõ sai là trình biên dịch báo ngay
    public void DangNhap_TuFileJson(string tk, string mk, bool ok)
    {
        var sp = new LoginPage(driver).DangNhap(tk,mk);
        Assert.That(sp.DaHienThi(), Is.EqualTo(ok));
    }
}


