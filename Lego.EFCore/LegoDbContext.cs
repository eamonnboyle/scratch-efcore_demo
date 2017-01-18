namespace Lego.EFCore {
    using Lego.Shared;
    using Microsoft.EntityFrameworkCore;

    public class LegoDbContext : DbContext {
        private const DatabaseType Type = DatabaseType.MySql;

        public DbSet<LegoSet> Sets { get; set; }

        public DbSet<Theme> Themes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            if (Type == DatabaseType.MySql) {
                optionsBuilder.UseMySql(
                    @"server=192.168.56.10;userid=root;pwd=entitypassword;port=3306;database=legodb;sslmode=none");
            }
            else {
                optionsBuilder.UseSqlServer(
                    @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=legodb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Theme>()
                .HasAlternateKey(x => x.Description);
        }

        private enum DatabaseType {
            MySql,
            SqlServer
        }
    }
}