// using NUnit.Framework;
// //Cho các class test chạy song song với nhau
// [assembly: Parallelizable(ParallelScope.Fixtures)]
// //Số luồng tối đa chạy cùng lúc (thường = số nhân CPU)
// [assembly: LevelOfParallelism(4)]
// // ==== Lưu ý về phạm vi song song ====
// // ParallelScope.Fixtures -> các CLASS song song (an toàn nhất)
// // ParallelScope.Children -> các CA trong 1 class song song
// //    | nguy hiểm hơn: các ca chia sẻ field của class
// // Muốn 1 class KHÔNG song song (vd: dùng chung dữ liệu):
// [NonParallelizable]   // đặt trên class đó
// // Data-driven Bài 5: [TestCaseSource] vẫn song song tốt
// //   vì mỗi ca là 1 instance riêng - driver không dùng chung