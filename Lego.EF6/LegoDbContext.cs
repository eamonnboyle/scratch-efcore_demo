namespace Lego.EF6 {
    using System.Data.Entity;
    using Lego.Shared;

    public class LegoDbContext : DbContext {

        public DbSet<LegoSet> Sets { get; set; }
        public DbSet<Theme> Themes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
        }
    }
}