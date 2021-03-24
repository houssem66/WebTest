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



        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //Sprint 2
            builder.Entity<Experience>().HasMany(e => e.Commentaires);
        }

    }



}

