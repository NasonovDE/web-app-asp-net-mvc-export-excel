using System.Data.Entity;

namespace KinoAfisha.Models
{
    public class KinoAfishaContext : DbContext
    {

        public DbSet<Film> Films { get; set; }
        public DbSet<Kino> Kinos { get; set; }
        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<FilmCover> FilmCovers { get; set; }
        public DbSet<Description> Descriptions { get; set; }
        public DbSet<Format> Formats { get; set; }
        public KinoAfishaContext() : base("KinoAfishaEntity")
        { }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Film>().HasOptional(x => x.FilmCover).WithRequired().WillCascadeOnDelete(true);
            base.OnModelCreating(modelBuilder);
        }
    }
}