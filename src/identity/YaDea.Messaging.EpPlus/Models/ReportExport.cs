using System.ComponentModel;
using YaDea.Messaging.EpPlus.Extension;

namespace YaDea.Messaging.EpPlus.Models
{
    public class ReportExport
    {
        /// <summary>
        /// Id
        /// </summary>
        [ExportIgnore]
        public string Id { get; set; }

        /// <summary>
        /// 字符串
        /// </summary>
        [DisplayName("字符串类型")]
        public string Str { get; set; }

        /// <summary>
        /// 数字类型
        /// </summary>
        [DisplayName("数字")]
        public decimal Number { get; set; }

        /// <summary>
        /// 枚举
        /// </summary>
        [DisplayName("枚举")]
        public Status Status { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        [DisplayName("时间")]
        public DateTime Time { get; set; }

        /// <summary>
        /// 数字类型
        /// </summary>
        [DisplayName("数字可空")]
        public decimal? NumberNull { get; set; }

        /// <summary>
        /// 枚举
        /// </summary>
        [DisplayName("枚举可空")]
        public Status? StatusNull { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        [DisplayName("时间可空")]
        public DateTime? TimeNull { get; set; }
    }
}
