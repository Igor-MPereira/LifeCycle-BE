using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia_LifeCycle.Domain.Models
{
    public class RelatedTag : IEntityTypeConfiguration<RelatedTag>
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public bool FromUser { get; set; }

        [ForeignKey(nameof(Publication))]
        public Guid? PublicationId { get; set; }
        public virtual Publication Publication { get; set; }

        [ForeignKey(nameof(User))]
        public Guid? UserId { get; set; }
        public virtual User User { get; set; }

        [ForeignKey(nameof(Tag))]
        public Guid TagId { get; set; }
        public virtual Tag Tag { get; set; }

        public void Configure(EntityTypeBuilder<RelatedTag> builder)
        {
            builder.Property(a => a.Id)
                .HasDefaultValueSql("NEWSEQUENTIALID()")
                .ValueGeneratedOnAdd();
        }
    }
}
