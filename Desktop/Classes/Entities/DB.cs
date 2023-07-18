using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Diplom.Classes.Entities
{
    public partial class DB : DbContext
    {
        public DB()
            : base("name=DB")
        {
        }

        public DB(string connection)
            : base(connection)
        {
        }

        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<Film> Film { get; set; }
        public virtual DbSet<Genre> Genre { get; set; }
        public virtual DbSet<Hall> Hall { get; set; }
        public virtual DbSet<Login> Login { get; set; }
        public virtual DbSet<MinAge> MinAge { get; set; }
        public virtual DbSet<Place> Place { get; set; }
        public virtual DbSet<PlaceType> PlaceType { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<Screenshot> Screenshot { get; set; }
        public virtual DbSet<Seance> Seance { get; set; }
        public virtual DbSet<Ticket> Ticket { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>()
                .HasMany(e => e.Film)
                .WithRequired(e => e.Country)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Film>()
                .HasMany(e => e.Screenshot)
                .WithRequired(e => e.Film)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Film>()
                .HasMany(e => e.Seance)
                .WithRequired(e => e.Film)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Film>()
                .HasMany(e => e.Genre)
                .WithMany(e => e.Film)
                .Map(m => m.ToTable("FilmGenre", "public").MapLeftKey("FilmId").MapRightKey("GenreId"));

            modelBuilder.Entity<Hall>()
                .HasMany(e => e.Place)
                .WithRequired(e => e.Hall)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Hall>()
                .HasMany(e => e.Seance)
                .WithRequired(e => e.Hall)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MinAge>()
                .HasMany(e => e.Film)
                .WithRequired(e => e.MinAge)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Place>()
                .HasMany(e => e.Ticket)
                .WithRequired(e => e.Place)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PlaceType>()
                .HasMany(e => e.Place)
                .WithRequired(e => e.PlaceType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Role>()
                .HasMany(e => e.User)
                .WithRequired(e => e.Role)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Seance>()
                .HasMany(e => e.Ticket)
                .WithRequired(e => e.Seance)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Ticket>()
                .Property(e => e.TicketCode)
                .IsFixedLength();

            modelBuilder.Entity<User>()
                .Property(e => e.UserPhoneNumber)
                .IsFixedLength();

            modelBuilder.Entity<User>()
                .HasMany(e => e.Login)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);
        }
    }
}
