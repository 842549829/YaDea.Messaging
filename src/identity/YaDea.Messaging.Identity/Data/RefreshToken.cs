using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace YaDea.Messaging.Identity.Data
{
    public class RefreshToken
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(128)]
        public string JwtId { get; set; }

        [Required]
        [StringLength(256)]
        public string Token { get; set; }

        /// <summary>
        /// 是否使用，一个RefreshToken只能使用一次
        /// </summary>
        [Required]
        public bool Used { get; set; }

        /// <summary>
        /// 是否失效。修改用户重要信息时可将此字段更新为true，使用户重新登录
        /// </summary>
        [Required]
        public bool Invalidated { get; set; }

        [Required]
        public DateTime CreationTime { get; set; }

        [Required]
        public DateTime ExpiryTime { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        [ForeignKey(nameof(UserId))]
        public AppUser User { get; set; }
    }
}