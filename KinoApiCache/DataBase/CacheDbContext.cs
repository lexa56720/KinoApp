using KinoApiCache.DataBase.Tables;
using KinoTypes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinoApiCache
{
    internal class CacheDbContext : DbContext
    {
        public DbSet<CallDB> Calls { get; set; } = null;
        public DbSet<ArgumentDB> Arguments { get; set; } = null;
        public DbSet<ResultDB> Results { get; set; } = null;

        public DbSet<MovieDB> Movies { get; set; } = null;
        public DbSet<MovieInfoDB> MovieInfos { get; set; } = null;
        public DbSet<Genre> Genres { get; set; } = null;


        private readonly string connectionString;
        public CacheDbContext(string connectionString)
        {
            this.connectionString = connectionString;
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CallDB>(entity =>
            {
                entity.ToTable("calls");

                entity.HasKey(p => p.Id).HasName("calls_pkey");
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.FuncId)
                      .HasColumnName("func_id");

                entity.Property(e => e.Date)
                      .HasColumnName("date");

                entity.HasMany(c => c.Arguments)
                      .WithOne(a => a.Call)
                      .HasForeignKey(a => a.CallId);

                entity.HasMany(c => c.Results)
                      .WithOne(r => r.Call)
                      .HasForeignKey(r => r.CallId);
            });

            modelBuilder.Entity<ArgumentDB>(entity =>
            {
                entity.ToTable("arguments");

                entity.HasKey(p => p.Id).HasName("arguments_pkey");
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CallId)
                      .HasColumnName("call_id");

                entity.Property(e => e.Index)
                      .HasColumnName("index");

                entity.Property(e => e.Value)
                      .HasColumnName("value");
            });

            modelBuilder.Entity<ResultDB>(entity =>
            {
                entity.ToTable("results");

                entity.HasKey(p => p.Id).HasName("results_pkey");
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ValueId)
                      .HasColumnName("value_id");
            });


            modelBuilder.Entity<MovieDB>(entity =>
            {
                entity.ToTable("movies");

                entity.HasKey(p => p.Id).HasName("movies_pkey");
                entity.Property(e => e.Id).HasColumnName("id");
            });

            modelBuilder.Entity<MovieInfoDB>(entity =>
            {
                entity.ToTable("movie_infos");

                entity.HasKey(p => p.Id).HasName("movie_infos_pkey");
                entity.Property(e => e.Id).HasColumnName("id");
            });

            modelBuilder.Entity<GenreDB>(entity =>
            {
                entity.ToTable("genres");

                entity.HasKey(p => p.Id).HasName("genres_pkey");
                entity.Property(e => e.Id).HasColumnName("id");
            });
        }
    }
}
