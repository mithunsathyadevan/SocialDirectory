using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

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
        public virtual DbSet<Login> Logins { get; set; }
        public virtual DbSet<UserDetail> UserDetails { get; set; }
        public virtual DbSet<UserInterestMapping> UserInterestMappings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
