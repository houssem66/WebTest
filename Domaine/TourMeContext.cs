using Domaine.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using TourMe.Data.Entities;

namespace TourMe.Data
{
    public class TourMeContext : IdentityDbContext<Utilisateur>
    {
        
        public TourMeContext(DbContextOptions<TourMeContext> options) : base(options)
        {

        }
        public DbSet<Utilisateur> User { get; set; }
        public DbSet<Commerçant> Commercant { get; set; }
        public DbSet<Experience> Experience { get; set; }
        public DbSet<Commentaire> Commentaires { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Activite> Activite { get; set; }
        public DbSet<Nourriture> Nourritures { get; set; }
        public DbSet<Logement> Logements { get; set; }



        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //Sprint 2
            builder.Entity<Experience>().HasMany(e => e.Commentaires);
            builder.Entity<Rating>().HasKey(e => new { e.ExperienceId, e.UtilisateurId });
            builder.Entity<Rating>()
             .HasOne(bc => bc.experience)
             .WithMany(b => b.Ratings)
             .HasForeignKey(bc => bc.ExperienceId);
            builder.Entity<Rating>()
            .HasOne(bc => bc.utilisateur)
            .WithMany(b => b.Ratings)
            .HasForeignKey(bc => bc.UtilisateurId);




      builder.Entity<Experience>()
      .HasMany(E => E.Activites);
      builder.Entity<Experience>().Property(p => p.tarif)
      .HasColumnType("decimal(18,4)");
      builder.Entity<Experience>()
      .HasOne(E => E.Nourriture);
       builder.Entity<Experience>()
      .HasOne(E => E.Logement);

            builder.Entity<Nourriture>().Property(p => p.Prix)
     .HasColumnType("decimal(18,4)");
     builder.Entity<Logement>().Property(p => p.Prix)
    .HasColumnType("decimal(18,4)");



            builder.Entity<Commerçant>().HasMany(e => e.Experiences);





        }

    }



}

