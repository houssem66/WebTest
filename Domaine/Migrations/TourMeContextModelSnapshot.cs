﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TourMe.Data;

namespace TourMe.Data.Migrations
{
    [DbContext(typeof(TourMeContext))]
    partial class TourMeContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Domaine.Entities.Utilisateur", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("Adresse")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Carte")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("Interet")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NickName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nom")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("Prenom")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProfilePhoto")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Telephone")
                        .HasColumnType("int");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<int>("gender")
                        .HasColumnType("int");

                    b.Property<int>("region")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Utilisateur");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("TourMe.Data.Entities.Activite", b =>
                {
                    b.Property<int>("activiteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Details")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ExperienceId")
                        .HasColumnType("int");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("dateDebut")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("dateFin")
                        .HasColumnType("datetime2");

                    b.HasKey("activiteId");

                    b.HasIndex("ExperienceId");

                    b.ToTable("Activite");
                });

            modelBuilder.Entity("TourMe.Data.Entities.Commentaire", b =>
                {
                    b.Property<int>("CommentaireId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Details")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ExperienceId")
                        .HasColumnType("int");

                    b.HasKey("CommentaireId");

                    b.HasIndex("ExperienceId");

                    b.ToTable("Commentaires");
                });

            modelBuilder.Entity("TourMe.Data.Entities.Experience", b =>
                {
                    b.Property<int>("ExperienceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Activité")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AvgRating")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CommerçantId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ImagesExperience")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Lieu")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NbPlaces")
                        .HasColumnType("int");

                    b.Property<string>("Rating")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Saison")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Titre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TypeExperience")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("dateDebut")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("dateFin")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("tarif")
                        .HasColumnType("decimal(18,4)");

                    b.HasKey("ExperienceId");

                    b.HasIndex("CommerçantId");

                    b.ToTable("Experience");
                });

            modelBuilder.Entity("TourMe.Data.Entities.Logement", b =>
                {
                    b.Property<int>("LogementId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateFin")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Datedebut")
                        .HasColumnType("datetime2");

                    b.Property<int>("ExperienceId")
                        .HasColumnType("int");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Lieu")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NbJours")
                        .HasColumnType("int");

                    b.Property<decimal>("Prix")
                        .HasColumnType("decimal(18,4)");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LogementId");

                    b.HasIndex("ExperienceId")
                        .IsUnique();

                    b.ToTable("Logements");
                });

            modelBuilder.Entity("TourMe.Data.Entities.Nourriture", b =>
                {
                    b.Property<int>("NourritureId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ExperienceId")
                        .HasColumnType("int");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Plat")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Prix")
                        .HasColumnType("decimal(18,4)");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("NourritureId");

                    b.HasIndex("ExperienceId")
                        .IsUnique();

                    b.ToTable("Nourritures");
                });

            modelBuilder.Entity("TourMe.Data.Entities.Rating", b =>
                {
                    b.Property<int>("ExperienceId")
                        .HasColumnType("int");

                    b.Property<string>("UtilisateurId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Commentaire")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("note")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ExperienceId", "UtilisateurId");

                    b.HasIndex("UtilisateurId");

                    b.ToTable("Ratings");
                });

            modelBuilder.Entity("TourMe.Data.Entities.Reservation", b =>
                {
                    b.Property<int>("ExperienceId")
                        .HasColumnType("int");

                    b.Property<string>("UtilisateurId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CCV")
                        .HasMaxLength(3)
                        .HasColumnType("nvarchar(3)");

                    b.Property<int>("CardType")
                        .HasColumnType("int");

                    b.Property<int>("CodePostale")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateReservation")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("NbrReservation")
                        .HasColumnType("int");

                    b.Property<long>("NumeroCarte")
                        .HasColumnType("bigint");

                    b.Property<decimal>("Tariff")
                        .HasColumnType("decimal(5,2)");

                    b.HasKey("ExperienceId", "UtilisateurId");

                    b.HasIndex("UtilisateurId");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("TourMe.Data.Entities.ServiceLogment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Adresse")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Category")
                        .HasColumnType("int");

                    b.Property<string>("Documents")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FournisseurId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Images")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("PrixParNuit")
                        .HasColumnType("decimal(5,2)");

                    b.Property<string>("Titre")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("FournisseurId");

                    b.ToTable("ServiceLogments");
                });

            modelBuilder.Entity("TourMe.Data.Entities.ServiceNouritture", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FournisseurId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Plat")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Prix")
                        .HasColumnType("decimal(5,2)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FournisseurId");

                    b.ToTable("ServiceNourittures");
                });

            modelBuilder.Entity("Domaine.Entities.Commerçant", b =>
                {
                    b.HasBaseType("Domaine.Entities.Utilisateur");

                    b.Property<string>("DomainActivite")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EffectFemme")
                        .HasColumnType("int");

                    b.Property<int>("EffectHomme")
                        .HasColumnType("int");

                    b.Property<string>("FormeJuridique")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NomGerant")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Patente")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PersAContact")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Secteur")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SituationEntreprise")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TypeOrgan")
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("Commerçant");
                });

            modelBuilder.Entity("TourMe.Data.Entities.Fournisseur", b =>
                {
                    b.HasBaseType("Domaine.Entities.Commerçant");

                    b.Property<int>("TypeService")
                        .HasColumnType("int");

                    b.HasDiscriminator().HasValue("Fournisseur");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Domaine.Entities.Utilisateur", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Domaine.Entities.Utilisateur", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domaine.Entities.Utilisateur", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Domaine.Entities.Utilisateur", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TourMe.Data.Entities.Activite", b =>
                {
                    b.HasOne("TourMe.Data.Entities.Experience", null)
                        .WithMany("Activites")
                        .HasForeignKey("ExperienceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TourMe.Data.Entities.Commentaire", b =>
                {
                    b.HasOne("TourMe.Data.Entities.Experience", null)
                        .WithMany("Commentaires")
                        .HasForeignKey("ExperienceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TourMe.Data.Entities.Experience", b =>
                {
                    b.HasOne("Domaine.Entities.Commerçant", null)
                        .WithMany("Experiences")
                        .HasForeignKey("CommerçantId");
                });

            modelBuilder.Entity("TourMe.Data.Entities.Logement", b =>
                {
                    b.HasOne("TourMe.Data.Entities.Experience", null)
                        .WithOne("Logement")
                        .HasForeignKey("TourMe.Data.Entities.Logement", "ExperienceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TourMe.Data.Entities.Nourriture", b =>
                {
                    b.HasOne("TourMe.Data.Entities.Experience", null)
                        .WithOne("Nourriture")
                        .HasForeignKey("TourMe.Data.Entities.Nourriture", "ExperienceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TourMe.Data.Entities.Rating", b =>
                {
                    b.HasOne("TourMe.Data.Entities.Experience", "experience")
                        .WithMany("Ratings")
                        .HasForeignKey("ExperienceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domaine.Entities.Utilisateur", "utilisateur")
                        .WithMany("Ratings")
                        .HasForeignKey("UtilisateurId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("experience");

                    b.Navigation("utilisateur");
                });

            modelBuilder.Entity("TourMe.Data.Entities.Reservation", b =>
                {
                    b.HasOne("TourMe.Data.Entities.Experience", "Experience")
                        .WithMany("Reservations")
                        .HasForeignKey("ExperienceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domaine.Entities.Utilisateur", "Utilisateur")
                        .WithMany("Reservations")
                        .HasForeignKey("UtilisateurId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Experience");

                    b.Navigation("Utilisateur");
                });

            modelBuilder.Entity("TourMe.Data.Entities.ServiceLogment", b =>
                {
                    b.HasOne("TourMe.Data.Entities.Fournisseur", "Fournisseur")
                        .WithMany("ServiceLogments")
                        .HasForeignKey("FournisseurId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Fournisseur");
                });

            modelBuilder.Entity("TourMe.Data.Entities.ServiceNouritture", b =>
                {
                    b.HasOne("TourMe.Data.Entities.Fournisseur", "Fournisseur")
                        .WithMany("ServiceNourittures")
                        .HasForeignKey("FournisseurId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Fournisseur");
                });

            modelBuilder.Entity("Domaine.Entities.Utilisateur", b =>
                {
                    b.Navigation("Ratings");

                    b.Navigation("Reservations");
                });

            modelBuilder.Entity("TourMe.Data.Entities.Experience", b =>
                {
                    b.Navigation("Activites");

                    b.Navigation("Commentaires");

                    b.Navigation("Logement");

                    b.Navigation("Nourriture");

                    b.Navigation("Ratings");

                    b.Navigation("Reservations");
                });

            modelBuilder.Entity("Domaine.Entities.Commerçant", b =>
                {
                    b.Navigation("Experiences");
                });

            modelBuilder.Entity("TourMe.Data.Entities.Fournisseur", b =>
                {
                    b.Navigation("ServiceLogments");

                    b.Navigation("ServiceNourittures");
                });
#pragma warning restore 612, 618
        }
    }
}
