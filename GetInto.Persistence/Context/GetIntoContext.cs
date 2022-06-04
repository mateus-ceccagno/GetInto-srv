using GetInto.Domain;
using GetInto.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GetInto.Persistence.Context
{
    public class GetIntoContext : IdentityDbContext<User, Role, long,
                                                       IdentityUserClaim<long>, UserRole, IdentityUserLogin<long>,
                                                       IdentityRoleClaim<long>, IdentityUserToken<long>>
    {
        public GetIntoContext(DbContextOptions<GetIntoContext> options) : base(options) { }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Human> Humans { get; set; }
        public DbSet<HumanProject> HumansProjects { get; set; }
        public DbSet<SocialLink> SocialLinks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(GetIntoContext).Assembly);
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;
            }

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserRole>(userRole =>
            {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                userRole.HasOne(ur => ur.User)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();

                userRole.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();
            });

            modelBuilder.Entity<HumanProject>()
                .HasKey(hp => new { hp.ProjectId, hp.HumanId });

            modelBuilder.Entity<Human>()
                .HasMany(h => h.SocialLinks)
                .WithOne(sl => sl.Human)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Project>()
                .HasMany(p => p.SocialLinks)
                .WithOne(sl => sl.Project)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
