using Microsoft.AspNetCore.Mvc;
using YaDea.Messaging.EpPlus.Extension;
using YaDea.Messaging.EpPlus.Models;

namespace YaDea.Messaging.EpPlus.Controllers
{
    [Route("api/ep_plus")]
    public class EpPlusController : ControllerBase
    {
        /// <summary>
        /// Excel导入
        /// </summary>
        /// <returns>结果</returns>
        [HttpPost]
        public Task<string> ImportAsync(IFormFile file)
        {
            var reports = ExcelExtend.LoadFromExcel<ReportImport>(file, 1);
            return Task.FromResult("OK");
        }

        /// <summary>
        /// Excl导出
        /// </summary>
        /// <returns>结果</returns>
        [HttpGet]
        public Task<FileContentResult> ExportAsync()
        {
            var reports = ExcelExtend.Export(new List<ReportExport>
            {
                new()
                {
                    Id = Guid.NewGuid().ToString(),
                    Number = 12,
                    NumberNull = null,
                    Status = Status.Success,
                    StatusNull = null,
                    Str = "上升到水电费",
                    Time = DateTime.Now,
                    TimeNull = DateTime.Now
                }
            }, "作者", "标题");
            var fileContentResult = new FileContentResult(reports, "application/vnd.ms-excel")
            {
                FileDownloadName = "名称"
            };
            return Task.FromResult(fileContentResult);
        }

        /// <summary>
        /// Excl导出(标题)
        /// </summary>
        /// <returns>结果</returns>
        [HttpGet("t1")]
        public Task<FileContentResult> ExportT1Async()
        {
            var reports = ExcelExtend.ModelExportEPPlusExcel(new List<ReportExport>
            {
                new()
                {
                    Id = Guid.NewGuid().ToString(),
                    Number = 12,
                    NumberNull = null,
                    Status = Status.Success,
                    StatusNull = null,
                    Str = "上升到水电费",
                    Time = DateTime.Now,
                    TimeNull = DateTime.Now
                }
            });
            var fileContentResult = new FileContentResult(reports, "application/vnd.ms-excel")
            {
                FileDownloadName = "名称"
            };
            return Task.FromResult(fileContentResult);
        }
    }
}
