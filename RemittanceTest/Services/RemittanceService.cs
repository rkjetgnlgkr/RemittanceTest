using RemittanceTest.Models;

namespace RemittanceTest.Services
{
    public class RemittanceService : IRemittanceService
    {
        // 模擬資料庫 (靜態變數確保跨 Request 資料一致)
        private static readonly List<Remittance> _db = new()
        {
            new Remittance { Id = 1, AccountName = "測試企業A", Amount = 50000, Status = 0 },
            new Remittance { Id = 2, AccountName = "測試企業B", Amount = 12000, Status = 1 }, // 不可取消
            new Remittance { Id = 3, AccountName = "測試企業C", Amount = 30000, Status = 0 }
        };

        // 提示：如何確保多執行緒下的資料安全？
        private static readonly object _lockObj = new object();

        public (bool IsSuccess, string Message) CancelRemittance(int id)
        {
            lock (_lockObj)
            {
                var remittance = _db.FirstOrDefault(r => r.Id == id);

                if (remittance == null)
                    return (false, "找不到指定的匯款資料。");

                if (remittance.Status != 0)
                    return (false, "僅狀態為「待覆核」的資料可執行取消。");

                remittance.Status = 9;
                return (true, "取消成功。");
            }
        }
    }
}