using Microsoft.EntityFrameworkCore;
using System;

namespace SocialMedia_LifeCycle.DataAccessEF
{
    using Domain.Models;

    public class LifeCycleContext : DbContext
    {
        public LifeCycleContext(DbContextOptions<LifeCycleContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Publication> Publications { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Relation> Relations { get; set; }
        public DbSet<Interaction> Interactions { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new User());   
            modelBuilder.ApplyConfiguration(new RelatedTag());   
            modelBuilder.ApplyConfiguration(new Relation());   
            modelBuilder.ApplyConfiguration(new Tag());   
            modelBuilder.ApplyConfiguration(new Publication());   
            modelBuilder.ApplyConfiguration(new Interaction());   
            modelBuilder.ApplyConfiguration(new Comment());
        }
    }
}
