using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;


namespace myMusicApi.model
{
    public class AppDbContext : IdentityDbContext<ApplicationUser,ApplicationRole,Guid>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Music>Musics { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Music>().HasKey(m => m.Id);
            modelBuilder.Entity<Music>().Property(m=> m.Title).HasMaxLength(30).IsRequired();
            modelBuilder.Entity<Music>().Property(m=> m.Artist).HasMaxLength(20).IsRequired();
            modelBuilder.Entity<Music>().Property(m=> m.ReleaseDate).IsRequired().HasDefaultValueSql("getdate()");
            modelBuilder.Entity<Music>().HasCheckConstraint("CK_Music_Rate", "Rate >= 0 AND Rate <= 10");
            modelBuilder.Entity<Music>().Property(m => m.Rate).HasDefaultValue(0);
        }
        public List<Music> GetAllMusic()
        {
            return Musics.FromSqlRaw("EXEC GetAllMusic").ToList();
        }

        public int addMusic(Music music)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Id", music.Id),
                new SqlParameter("@Title", music.Title),
                new SqlParameter("@ReleaseDate", music.ReleaseDate),
                new SqlParameter("@Artist", music.Artist),
                new SqlParameter("@Rate", music.Rate)
            };

            return Database.ExecuteSqlRaw("EXEC AddMusic @Id ,@Title, @ReleaseDate, @Artist, @Rate", parameters);


        }
    }
}
