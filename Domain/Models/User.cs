using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialMedia_LifeCycle.Controllers.Params;
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

        [Required, DataType(DataType.EmailAddress), RegularExpression(@"[a-zA-Z0-9._%-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}")]
        public string Email { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [Required, StringLength(32, MinimumLength = 4), RegularExpression(@"[a-zA-Z0-9\s._@]{4,32}")]
        public string Login { get; set; }

        [Required, StringLength(40, MinimumLength = 2), RegularExpression(@"[a-zA-Z0-9\s._\-+*/~^|\\@&$#()%]{1,40}")]
        public string DisplayName { get; set; }

        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\+[0-9]{2}[0-9]{2,3}[0-9]{9}")]
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
        public string Salt { get; set; }

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

        public User(UserCredentials dto) : base()
        {
            DisplayName = dto.DisplayName;
            BirthDate = dto.BirthDate;
            Login = dto.Login;
            Email = dto.Email;
            City = dto.Email;
            State = dto.State;
            Country = dto.Country;
            Password = dto.Password;
            PhoneNumber = dto.PhoneNumber;
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
