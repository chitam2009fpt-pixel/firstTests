
using OpenQA.Selenium;

namespace FirstTests.Utilities;

public static class ScreenshotHelper
{
    public static void Chup(IWebDriver? driver)
    {
        if(driver is null) return;

        try
        {
            var ctx = TestContext.CurrentContext;
            var thuMuc = Path.Combine(ctx.WorkDirectory, "screenshots");
            Directory.CreateDirectory(thuMuc);

            //Tên ca test do ca test tự sinh chứa " , ( ) -> KÝ TỰ CẮT trong tên file.
            //Không lọc là dính ArgumentException, che mất lỗi thật.
            var ten = string.Join("-",ctx.Test.Name.Split(Path.GetInvalidFileNameChars()));
            var path = Path.Combine(thuMuc, $"{ten}_{DateTime.Now:HHmmss}.png");

            ((ITakesScreenshot)driver).GetScreenshot().SaveAsFile(path);
            TestContext.AddTestAttachment(path);
            TestContext.Progress.WriteLine($"Ảnh lỗi: {path}");

        }
        catch(Exception ex)
        {
            //Nuốt lỗi: ví dụ có alert đang mở thì GetScreenShot() ném UnhandledAlertException (Bài 3)
            // Cơ chế hỗ trợ mà tự gây lỗi thì còn tệ hơn không có.
            TestContext.Progress.WriteLine($"Không chụp được ảnh: {ex.Message}");
        }
    }
}