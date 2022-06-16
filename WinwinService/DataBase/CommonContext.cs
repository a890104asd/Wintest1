using DataBase.Models;
using Microsoft.EntityFrameworkCore;

namespace DataBase
{
    public partial class CommonContext : DbContext
    {
        public string ConnectionStr { get; set; }

        public CommonContext(string _connectionStr)
        {
            ConnectionStr = _connectionStr;
        }

        public CommonContext(DbContextOptions<CommonContext> options)
            : base(options)
        {
        }

        public virtual DbSet<UserAccount> UserAccount { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql(ConnectionStr.Replace("{database_name}", "common"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserAccount>(entity =>
            {
                entity.HasKey(e => e.Email)
                    .HasName("PRIMARY");

                entity.ToTable("UserAccount");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasColumnType("varchar(50)");
                entity.Property(e => e.Password)
                    .HasColumnName("password");

                entity.Property(e => e.Age)
                    .HasColumnName("age")
                    .HasColumnType("int(10)");

                entity.Property(e => e.Gender)
                    .HasColumnName("gender")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Area)
                    .IsRequired()
                    .HasColumnName("area")
                    .HasColumnType("nvarchar(50)");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("nvarchar(10)");
            });
        }
    }
}
