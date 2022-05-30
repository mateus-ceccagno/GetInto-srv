using GetInto.Domain;
using Microsoft.EntityFrameworkCore;

namespace GetInto.Persistence.Context
{
    public class GetIntoContext : DbContext
    {
        public GetIntoContext(DbContextOptions<GetIntoContext> options) : base(options) { }

        public DbSet<Project> Projects { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Human> Humans { get; set; }
        public DbSet<HumanProject> HumansProjects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<HumanProject>()
                .HasKey(HP => new { HP.ProjectId, HP.HumanId });

            modelBuilder.Entity<Human>();
            modelBuilder.Entity<Project>();
        }
    }
}
