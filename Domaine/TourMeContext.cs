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

        public DbSet<Logement> Logement { get; set; }

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
           .HasMany<Activite>(E => E.Activites);

            //sprint3
            builder.Entity<Experience>().HasOne(e => e.Logement).WithOne(l => l.Experience).HasForeignKey<Logement>(x => x.ExperienceFk);
            builder.Entity<Logement>(eb =>
            {
                eb.Property(l => l.PrixParNuit).HasColumnType("decimal(5, 2)");
                eb.Property(l => l.Titre).HasColumnType("varchar(50)");
                eb.Property(l => l.Adresse).HasColumnType("varchar(100)");
                eb.Property(l => l.Description).HasColumnType("varchar(100)");
                

            });





        }

    }



}

