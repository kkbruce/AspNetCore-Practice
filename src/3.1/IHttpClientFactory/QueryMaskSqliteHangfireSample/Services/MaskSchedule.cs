using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using QueryMaskSample.Models;

namespace QueryMaskSample.Services
{
    public static class MaskSchedule
    {
        public static async Task InitialDb()
        {
            var db = new MaskContext();
            if (db.MedicalMasks.FirstOrDefault() == null)
            {
                var service = new MaskService(new HttpClient());
                var maskInfos = await service.GetMaskInfo();
                foreach (var maskInfo in maskInfos)
                {
                    db.Add(new MedicalMask()
                    {
                        Code = maskInfo.醫事機構代碼,
                        Name = maskInfo.醫事機構名稱,
                        Address = maskInfo.醫事機構地址,
                        Tel = maskInfo.醫事機構電話,
                        AdultNum = int.Parse(maskInfo.成人口罩剩餘數),
                        KidNum = int.Parse(maskInfo.兒童口罩剩餘數),
                        UpdateTime = DateTime.Parse(maskInfo.來源資料時間)
                    });
                }

                await db.SaveChangesAsync();
            }
        }

        public static async Task MaskDataUpdate()
        {
            var db = new MaskContext();
            var updateTime = db.MedicalMasks.First().UpdateTime;
            var nextTime = updateTime.AddSeconds(600);
            var nowTime = DateTime.Now;
            if (nextTime < nowTime)
            {
                var service = new MaskService(new HttpClient());
                var maskInfos = await service.GetMaskInfo();
                var sourceTime = DateTime.Parse(maskInfos.First().來源資料時間);
                // 0 is same as value
                // Ref: https://docs.microsoft.com/en-us/dotnet/api/system.datetime.compareto?view=netcore-3.1#remarks
                if (sourceTime.CompareTo(updateTime) != 0)
                {
                    db.RemoveRange(db.MedicalMasks);
                    foreach (var maskInfo in maskInfos)
                    {
                        db.Add(new MedicalMask()
                        {
                            Code = maskInfo.醫事機構代碼,
                            Name = maskInfo.醫事機構名稱,
                            Address = maskInfo.醫事機構地址,
                            Tel = maskInfo.醫事機構電話,
                            AdultNum = int.Parse(maskInfo.成人口罩剩餘數),
                            KidNum = int.Parse(maskInfo.兒童口罩剩餘數),
                            UpdateTime = DateTime.Parse(maskInfo.來源資料時間)
                        });
                    }

                    await db.SaveChangesAsync();
                }
            }
        }
    }

}
