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
    public class Publication : IEntityTypeConfiguration<Publication>
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [DataType(DataType.DateTime), DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime LastModified { get; set; }

        [Required, DataType(DataType.MultilineText), MaxLength(256)]
        public string Content { get; set; }

        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        public virtual User User { get; set; }

        [InverseProperty(nameof(Interaction.Publication))]
        public virtual ICollection<Interaction> Interactions { get; set; }

        [InverseProperty(nameof(RelatedTag.Publication))]
        public virtual ICollection<RelatedTag> RelatedTags { get; set; }

        public Publication()
        {
            Interactions = new HashSet<Interaction>();
            RelatedTags = new HashSet<RelatedTag>();
        }

        public void Configure(EntityTypeBuilder<Publication> builder)
        {
            builder.Property(a => a.Id)
                .HasDefaultValueSql("NEWSEQUENTIALID()")
                .ValueGeneratedOnAdd();
        }
    }
}
