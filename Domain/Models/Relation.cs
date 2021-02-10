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

    public class Relation : IEntityTypeConfiguration<Relation>
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required, DataType(DataType.DateTime), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime Since { get; set; }

        [Required]
        public ERelationNature RelationNature { get; set; }

        public bool Stablished { get; set; }

        [ForeignKey(nameof(StarterUser))]
        public Guid StarterUserId { get; set; }
        public virtual User StarterUser { get; set; }

        [ForeignKey(nameof(AskedUser))]
        public Guid AskedUserId { get; set; }
        public virtual User AskedUser { get; set; }

        public void Configure(EntityTypeBuilder<Relation> builder)
        {
            builder.Property(a => a.Id)
                .HasDefaultValueSql("NEWSEQUENTIALID()")
                .ValueGeneratedOnAdd();

            builder.Property(a => a.Stablished)
                .HasDefaultValue(false);

            builder
                .HasOne(m => m.AskedUser)
                .WithMany(m => m.ReceivedRelations)
                .HasForeignKey(m => m.AskedUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(m => m.StarterUser)
                .WithMany(m => m.StartedRelations)
                .HasForeignKey(m => m.StarterUserId);
        }
    }
}
