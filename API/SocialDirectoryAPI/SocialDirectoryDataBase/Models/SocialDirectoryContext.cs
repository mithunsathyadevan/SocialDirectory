using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
#nullable disable

namespace SocialDirectoryDataBase.Models
{
    public partial class SocialDirectoryContext : DbContext
    {
        public SocialDirectoryContext()
        {
        }

        public SocialDirectoryContext(DbContextOptions<SocialDirectoryContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ContactList> ContactLists { get; set; }
        public virtual DbSet<Interest> Interests { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<Login> Logins { get; set; }
        public virtual DbSet<UserDetail> UserDetails { get; set; }
        public virtual DbSet<UserInterestMapping> UserInterestMappings { get; set; }
        public virtual DbSet<SP_MatchesModel> MatchesModels { get; set; }

       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          

            modelBuilder.Entity<ContactList>(entity =>
            {
                entity.HasOne(d => d.User)
                    .WithMany(p => p.ContactLists)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_ContactList_UserDetails");
            });

            modelBuilder.Entity<Login>(entity =>
            {
                entity.HasOne(d => d.User)
                    .WithMany(p => p.Logins)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Login_UserDetails");
            });

            modelBuilder.Entity<UserDetail>(entity =>
            {
                entity.HasOne(d => d.Location)
                    .WithMany(p => p.UserDetails)
                    .HasForeignKey(d => d.LocationId)
                    .HasConstraintName("FK_UserDetails_Location");
            });

            modelBuilder.Entity<UserInterestMapping>(entity =>
            {
                entity.HasOne(d => d.Interest)
                    .WithMany(p => p.UserInterestMappings)
                    .HasForeignKey(d => d.InterestId)
                    .HasConstraintName("FK_UserInterestMapping_Interests");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserInterestMappings)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_UserInterestMapping_UserDetails");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
