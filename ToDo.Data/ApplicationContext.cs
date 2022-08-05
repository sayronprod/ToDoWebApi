using Microsoft.EntityFrameworkCore;
using ToDo.Models.ModelsDbo;

namespace ToDo.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<UserDbo> Users { get; set; }
        public DbSet<RoleDbo> Roles { get; set; }
        public DbSet<NoteDbo> Notes { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserDbo>(b =>
            {
                b.HasMany(e => e.UserRoles)
                .WithOne(c => c.User)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();
            });
        }
    }
}