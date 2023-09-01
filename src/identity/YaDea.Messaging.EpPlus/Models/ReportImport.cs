using System.ComponentModel;

namespace YaDea.Messaging.EpPlus.Models
{
    public class ReportImport
    {
        /// <summary>
        /// 字符串
        /// </summary>
        [Description("字符串类型")]
        public string Str { get; set; }

        /// <summary>
        /// 数字类型
        /// </summary>
        [Description(" 数字")]
        public decimal Number { get; set; }

        /// <summary>
        /// 枚举
        /// </summary>
        [Description("枚举")]
        public Status Status { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        [Description("时间")]
        public DateTime Time { get; set; }

        /// <summary>
        /// 数字类型
        /// </summary>
        [Description("数字可空")]
        public decimal? NumberNull { get; set; }

        /// <summary>
        /// 枚举
        /// </summary>
        [Description("枚举可空")]
        public Status? StatusNull { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        [Description("时间可空")]
        public DateTime? TimeNull { get; set; }
    }

    public enum Status
    {
        [Description("成功")]
        Success = 0,
        [Description("失败")]
        Fail = 1
    }
}
