using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia_LifeCycle.Domain.Models
{
    public class RefreshTokenInfo : IEntityTypeConfiguration<RefreshTokenInfo>
    {
        [Required]
        public string UserName { get; set; }
        [Required, DataType(DataType.DateTime)]
        public DateTime ValidTo { get; set; }
        [Required]
        public string RefreshToken { get; set; }

        public void Configure(EntityTypeBuilder<RefreshTokenInfo> builder)
        {
            builder
                .HasKey(s => new { s.RefreshToken, s.UserName });
        }
    }
}
