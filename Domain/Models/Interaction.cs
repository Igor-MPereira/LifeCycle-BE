using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SocialMedia_LifeCycle.Domain.Models
{
    using Enums;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class Interaction : IEntityTypeConfiguration<Interaction>
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public bool FromComment { get; set; }

        [Required]
        public ELikeState LikeState { get; set; }

        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        public virtual User User { get; set; }

        [InverseProperty(nameof(Comment.Interaction))]
        public virtual ICollection<Comment> Comments { get; set; }

        public Guid PublicationId { get; set; }
        public virtual Publication Publication { get; set; }

        public Guid ParentCommentId { get; set; }
        public virtual Comment ParentComment { get; set; }

        public Interaction()
        {
            Comments = new HashSet<Comment>();
        }

        public void Configure(EntityTypeBuilder<Interaction> builder)
        {
            builder.Property(a => a.Id)
                .HasDefaultValueSql("NEWSEQUENTIALID()")
                .ValueGeneratedOnAdd();

            builder.Property(a => a.LikeState)
                .HasDefaultValue(ELikeState.None);
        }
    }
}
