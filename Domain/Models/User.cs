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
    public class User : IEntityTypeConfiguration<User>
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        public string Login { get; set; }

        [Required]
        public string DisplayName { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [DataType(DataType.MultilineText), MaxLength(200)]
        public string Description { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid? ProfilePictureId { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid? BackgroundPictureId { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "dd/mm/yyyy")]
        public DateTime BirthDate { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Country { get; set; }

        [Required, DataType(DataType.Date), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime RegisterDate { get; set; }

        [Required]
        public bool Explicit { get; set; }

        [InverseProperty(nameof(Relation.StarterUser))]
        public virtual ICollection<Relation> StartedRelations { get; set; }

        [InverseProperty(nameof(Relation.AskedUser))]
        public virtual ICollection<Relation> ReceivedRelations { get; set; }

        [InverseProperty(nameof(Publication.User))]
        public virtual ICollection<Publication> Publications { get; set; }

        [InverseProperty(nameof(Comment.User))]
        public virtual ICollection<Comment> Comments { get; set; }

        [InverseProperty(nameof(RelatedTag.User))]
        public virtual ICollection<RelatedTag> RelatedTags { get; set; }

        [InverseProperty(nameof(Interaction.User))]
        public virtual ICollection<Interaction> Interactions { get; set; }

        public User()
        {
            StartedRelations = new HashSet<Relation>();
            ReceivedRelations = new HashSet<Relation>();
            Publications = new HashSet<Publication>();
            Comments = new HashSet<Comment>();
            RelatedTags = new HashSet<RelatedTag>();
            Interactions = new HashSet<Interaction>();
        }

        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(a => a.Id)
                .HasDefaultValueSql("NEWSEQUENTIALID()")
                .ValueGeneratedOnAdd();

            builder
                .HasIndex(a => a.Login)
                .IsUnique();

            builder
                .HasIndex(a => a.Email)
                .IsUnique();

            builder
                .HasMany(a => a.Interactions)
                .WithOne(a => a.User)
                .HasForeignKey(async => async.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasMany(a => a.Comments)
                .WithOne(a => a.User)
                .HasForeignKey(async => async.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Property(a => a.RegisterDate)
                .HasDefaultValueSql("GETUTCDATE()")
                .ValueGeneratedOnAdd();

            builder
                .Property(a => a.BirthDate)
                .HasDefaultValue(new DateTime(1900, 1, 1));
        }
    }
}
