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
    public class Comment : IEntityTypeConfiguration<Comment>
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required, DataType(DataType.MultilineText), MaxLength(256)]
        public string Content { get; set; }

        [Required, ForeignKey(nameof(Interaction))]
        public Guid InteractionId { get; set; }

        [Required, ForeignKey(nameof(User))]
        public Guid UserId { get; set; }

        public virtual User User { get; set; }

        public virtual Interaction Interaction { get; set; }

        [InverseProperty(nameof(Models.Interaction.ParentComment))]
        public virtual ICollection<Interaction> Interactions { get; set; }

        public Comment()
        {
            Interactions = new HashSet<Interaction>();
        }

        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.Property(a => a.Id)
                .HasDefaultValueSql("NEWSEQUENTIALID()")
                .ValueGeneratedOnAdd();

            builder
                .HasMany(a => a.Interactions)
                .WithOne(a => a.ParentComment)
                .HasForeignKey(a => a.ParentCommentId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
