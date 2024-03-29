﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace DB_lab2
{
    public partial class DBFilmsContext : DbContext
    {
        public DBFilmsContext()
        {
        }

        public DBFilmsContext(DbContextOptions<DBFilmsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Actor> Actors { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Film> Films { get; set; }
        public virtual DbSet<FilmActorRelationship> FilmActorRelationships { get; set; }
        public virtual DbSet<FilmGanreRelationship> FilmGanreRelationships { get; set; }
        public virtual DbSet<FilmUserRelationship> FilmUserRelationships { get; set; }
        public virtual DbSet<Ganre> Ganres { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server= LAPTOP-VLHU3V0T; Database=DBFilms;Trusted_Connection=True; ");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Polish_CI_AS");

            modelBuilder.Entity<Actor>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Film>(entity =>
            {
                entity.Property(e => e.Info).HasColumnType("ntext");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Films)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Films_Categories");
            });

            modelBuilder.Entity<FilmActorRelationship>(entity =>
            {
                entity.ToTable("FilmActorRelationship");

                entity.HasOne(d => d.Actor)
                    .WithMany(p => p.FilmActorRelationships)
                    .HasForeignKey(d => d.ActorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FilmActorRelationship_Actors");

                entity.HasOne(d => d.Film)
                    .WithMany(p => p.FilmActorRelationships)
                    .HasForeignKey(d => d.FilmId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FilmActorRelationship_Films");
            });

            modelBuilder.Entity<FilmGanreRelationship>(entity =>
            {
                entity.ToTable("FilmGanreRelationship");

                entity.HasOne(d => d.Film)
                    .WithMany(p => p.FilmGanreRelationships)
                    .HasForeignKey(d => d.FilmId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FilmGanreRelationship_Films");

                entity.HasOne(d => d.Ganre)
                    .WithMany(p => p.FilmGanreRelationships)
                    .HasForeignKey(d => d.GanreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FilmGanreRelationship_Ganres");
            });

            modelBuilder.Entity<FilmUserRelationship>(entity =>
            {
                entity.ToTable("FilmUserRelationship");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsFixedLength(true);

                entity.HasOne(d => d.Film)
                    .WithMany(p => p.FilmUserRelationships)
                    .HasForeignKey(d => d.FilmId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FilmUserRelationship_Films");
            });

            modelBuilder.Entity<Ganre>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
