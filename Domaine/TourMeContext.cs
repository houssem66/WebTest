using Domaine.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
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
        public DbSet<Transport> Transports { get; set; }
        public DbSet<ServiceLogment> ServiceLogments { get; set; }
        public DbSet<ServiceNouritture> ServiceNourittures { get; set; }
        public DbSet<ServiceTransport> ServiceTransports { get; set; }
        public DbSet<Fournisseur> Fournisseurs { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<EmployeDocuments> EmployeDocuments { get; set; }
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
            //sprint3
            builder.Entity<Experience>()
            .HasOne(E => E.Nourriture);
            builder.Entity<Experience>()
           .HasOne(E => E.Logement);
            builder.Entity<Experience>()
           .HasOne(E => E.Transport);
            builder.Entity<Transport>().Property(p => p.ExperienceId).HasColumnName("ExperienceId").IsRequired();
            builder.Entity<Nourriture>().Property(p => p.Prix)
              .HasColumnType("decimal(18,4)");
            builder.Entity<Logement>().Property(p => p.Prix)
           .HasColumnType("decimal(18,4)");
            builder.Entity<Transport>().Property(p => p.Prix)
        .HasColumnType("decimal(18,4)");



            builder.Entity<Commerçant>().HasMany(e => e.Experiences);

            builder.Entity<Fournisseur>().HasMany(f => f.ServiceLogments).WithOne(f => f.Fournisseur).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<Fournisseur>().HasMany(f => f.ServiceNourittures);
            builder.Entity<Fournisseur>().HasMany(f => f.ServiceTransports).WithOne(f => f.Fournisseur).OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ServiceLogment>(eb =>
            {
                eb.Property(l => l.PrixParNuit).HasColumnType("decimal(5, 2)");

            }

          );
            builder.Entity<ServiceNouritture>(eb =>
            {
                eb.Property(l => l.Prix).HasColumnType("decimal(5, 2)");

            }

         );
            builder.Entity<ServiceTransport>(eb =>
            {
                eb.Property(l => l.Prix).HasColumnType("decimal(5, 2)");

            }

         );
            //reservation 
            builder.Entity<Reservation>().HasKey(e => new { e.ExperienceId, e.UtilisateurId });
            builder.Entity<Reservation>()
            .HasOne(bc => bc.Experience)
            .WithMany(b => b.Reservations)
            .HasForeignKey(bc => bc.ExperienceId);
            builder.Entity<Reservation>()
            .HasOne(bc => bc.Utilisateur)
            .WithMany(b => b.Reservations)
            .HasForeignKey(bc => bc.UtilisateurId);
            builder.Entity<Reservation>(eb =>
            {
                eb.Property(l => l.Tariff).HasColumnType("decimal(5, 2)");


            }

         );
            builder.Entity<Commerçant>().HasMany(e => e.EmployeDocuments).WithOne(x=>x.Commerçant).OnDelete(DeleteBehavior.Cascade);
            //sprint4
            builder.Entity<ServiceLogment>().HasMany(e => e.Documents).WithOne(x => x.ServiceLogment).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<ServiceNouritture>().HasMany(e => e.Documents).WithOne(x => x.ServiceNouritture).OnDelete(DeleteBehavior.Cascade);
        }

    }



}

