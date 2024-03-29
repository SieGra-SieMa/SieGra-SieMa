﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace SieGraSieMa.Models
{
    public partial class SieGraSieMaContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public SieGraSieMaContext()
        {
        }

        public SieGraSieMaContext(DbContextOptions<SieGraSieMaContext> options) : base(options)
        {
            //this.Configuration.LazyLoadingEnabled = false;
        }

        public virtual DbSet<Album> Albums { get; set; }
        public virtual DbSet<Contest> Contests { get; set; }
        public virtual DbSet<Contestant> Contestants { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<Log> Logs { get; set; }
        public virtual DbSet<Match> Matches { get; set; }
        public virtual DbSet<Medium> Media { get; set; }
        public virtual DbSet<MediumInAlbum> MediumInAlbum { get; set; }
        public virtual DbSet<Newsletter> Newsletters { get; set; }
        public virtual DbSet<Player> Players { get; set; }
        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }
        public virtual DbSet<Team> Teams { get; set; }
        public virtual DbSet<TeamInGroup> TeamInGroups { get; set; }
        public virtual DbSet<TeamInTournament> TeamInTournaments { get; set; }
        public virtual DbSet<Tournament> Tournaments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code.
                //You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration
                //- see https://go.microsoft.com/fwlink/?linkid=2131148.
                //For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                //optionsBuilder.UseMySQL("Server=localhost;database=SieGraSieMa;user=siegra;password=siema");

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity => entity.Property(m => m.Id).HasMaxLength(85));
            modelBuilder.Entity<IdentityRole<int>>(entity => entity.Property(m => m.Id).HasMaxLength(85));

            modelBuilder.Entity<IdentityUserClaim<int>>(entity => entity.Property(m => m.Id).HasMaxLength(85));
            modelBuilder.Entity<IdentityRoleClaim<int>>(entity => entity.Property(m => m.Id).HasMaxLength(85));

            modelBuilder.Entity<IdentityUserLogin<int>>(entity => entity.Property(m => m.LoginProvider).HasMaxLength(85));
            modelBuilder.Entity<IdentityUserLogin<int>>(entity => entity.Property(m => m.ProviderKey).HasMaxLength(85));

            modelBuilder.Entity<IdentityUserToken<int>>(entity => entity.Property(m => m.LoginProvider).HasMaxLength(85));
            modelBuilder.Entity<IdentityUserToken<int>>(entity => entity.Property(m => m.Name).HasMaxLength(85));

            modelBuilder.Entity<Album>(entity =>
            {
                entity.ToTable("album");

                entity.Property(e => e.Id)
                    .HasColumnName("id");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(256)
                    .HasColumnName("name");

                entity.Property(e => e.CreateDate)
                    .HasColumnName("create_date");

                entity.Property(e => e.TournamentId)
                    .HasColumnName("tournament_id");

                entity.HasOne(d => d.Tournament)
                    .WithMany(p => p.Albums)
                    .HasForeignKey(d => d.TournamentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("album_tournament");

                entity.HasIndex(e => e.TournamentId, "album_tournament");
            });

            modelBuilder.Entity<Contest>(entity =>
            {
                entity.ToTable("contest");

                entity.Property(e => e.Id)
                    .HasColumnName("id");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(64)
                    .HasColumnName("name");

                entity.Property(e => e.TournamentId)
                    .HasColumnName("tournament_id");

                entity.HasOne(d => d.Tournament)
                    .WithMany(p => p.Contests)
                    .HasForeignKey(d => d.TournamentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("contest_tournament");

                entity.HasIndex(e => e.TournamentId, "contest_tournament");
            });

            modelBuilder.Entity<Contestant>(entity =>
            {
                entity.ToTable("contestants");

                entity.HasKey(e => new { e.ContestId, e.UserId })
                    .HasName("PRIMARY");

                entity.Property(e => e.ContestId)
                    .HasColumnName("contest_id");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id");
                //.HasDefaultValueSql("'1'");

                entity.Property(e => e.Points).HasColumnName("points");

                entity.HasOne(d => d.Contest)
                    .WithMany(p => p.Contestants)
                    .HasForeignKey(d => d.ContestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("contestants_contest");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Contestants)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("contestants_user");

                entity.HasIndex(e => e.ContestId, "contestants_contest");
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.ToTable("group");

                entity.Property(e => e.Id)
                    .HasColumnName("id");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(2)
                    .HasColumnName("name");

                entity.Property(e => e.TournamentId)
                    .HasColumnName("tournament_id");

                entity.Property(e => e.Ladder)
                    .HasColumnName("ladder");

                entity.Property(e => e.Phase)
                    .HasColumnName("phase");

                entity.HasOne(d => d.Tournament)
                    .WithMany(p => p.Groups)
                    .HasForeignKey(d => d.TournamentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("group_tournament");

                entity.HasIndex(e => new { e.Name, e.TournamentId }, "name_in_tournament")
                    .IsUnique();

                entity.HasIndex(e => e.TournamentId, "group_tournament");
            });

            modelBuilder.Entity<Log>(entity =>
            {
                entity.ToTable("logs");

                entity.Property(e => e.Id)
                    .HasColumnName("id");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id");

                entity.Property(e => e.Action)
                    .IsRequired()
                    .HasMaxLength(256)
                    .HasColumnName("action");

                entity.Property(e => e.Time)
                    .HasColumnName("time");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Logs)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("logs_user");

                entity.HasIndex(e => e.UserId, "logs_user");
            });

            modelBuilder.Entity<Match>(entity =>
            {
                entity.ToTable("match");

                entity.HasKey(e => new { e.TournamentId, e.Phase, e.MatchId })
                    .HasName("PRIMARY");

                entity.Property(e => e.TournamentId)
                    .HasColumnName("tournament_id");

                entity.Property(e => e.Phase)
                    .HasColumnName("phase");

                entity.Property(e => e.MatchId)
                    .HasColumnName("match_id");

                entity.Property(e => e.TeamHomeScore)
                    .HasColumnName("team_home_score")
                    .IsRequired(false);

                entity.Property(e => e.TeamAwayScore)
                    .HasColumnName("team_away_score")
                    .IsRequired(false);

                entity.Property(e => e.TeamHomeId)
                    .HasColumnName("team_home_id")
                    .IsRequired(false);

                entity.Property(e => e.TeamAwayId)
                    .HasColumnName("team_away_id")
                    .IsRequired(false);

                entity.HasOne(d => d.Tournament)
                    .WithMany(p => p.Matches)
                    .HasForeignKey(d => d.TournamentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("tournament");

                entity.HasOne(d => d.TeamAway)
                    .WithMany(p => p.MatchTeamAways)
                    .HasForeignKey(d => d.TeamAwayId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("match_away");

                entity.HasOne(d => d.TeamHome)
                    .WithMany(p => p.MatchTeamHomes)
                    .HasForeignKey(d => d.TeamHomeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("match_home");

                entity.HasIndex(e => new { e.TeamHomeId, e.TeamAwayId }, "meet").IsUnique();
            });

            modelBuilder.Entity<Medium>(entity =>
            {
                entity.ToTable("media");

                entity.Property(e => e.Id)
                    .HasColumnName("id");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Url)
                    .IsRequired()
                    .HasMaxLength(256)
                    .HasColumnName("url");

            });

            modelBuilder.Entity<MediumInAlbum>(entity =>
            {
                entity.ToTable("medium_in_album");

                entity.HasKey(e => new { e.AlbumId, e.MediumId })
                    .HasName("PRIMARY");

                entity.Property(e => e.AlbumId)
                    .HasColumnName("album_id");

                entity.Property(e => e.MediumId)
                    .HasColumnName("medium_id");

                entity.HasOne(d => d.Album)
                    .WithMany(p => p.MediumInAlbums)
                    .HasForeignKey(d => d.AlbumId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("medium_in_album_album");

                entity.HasOne(d => d.Medium)
                    .WithMany(p => p.MediumInAlbums)
                    .HasForeignKey(d => d.MediumId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("medium_in_album_media");

                entity.HasIndex(e => e.MediumId, "medium_in_album_media");
            });

            modelBuilder.Entity<Newsletter>(entity =>
            {
                entity.ToTable("newsletter");

                entity.Property(e => e.Id)
                    .HasColumnName("id");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Newsletters)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("newsletter_user");

                entity.HasIndex(e => e.UserId, "newsletter_user");
            });

            modelBuilder.Entity<Player>(entity =>
            {
                entity.HasKey(e => new { e.TeamId, e.UserId })
                    .HasName("PRIMARY");

                entity.ToTable("player");

                entity.Property(e => e.TeamId)
                    .HasColumnName("team_id");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id");

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.Players)
                    .HasForeignKey(d => d.TeamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("player_team");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Players)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("player_user");

                entity.HasIndex(e => e.UserId, "player_user");
            });

            modelBuilder.Entity<RefreshToken>(entity =>
            {
                entity.ToTable("refresh_token");

                entity.Property(e => e.Id)
                    .HasColumnName("id");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Token)
                    .IsRequired()
                    .HasColumnName("token");

                entity.Property(e => e.Expires)
                    .IsRequired()
                    .HasColumnName("expires");

                entity.Property(e => e.Created)
                    .IsRequired()
                    .HasColumnName("created");

                entity.Property(e => e.CreatedByIp)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("createdByIp");

                entity.Property(e => e.Revoked)
                    .HasColumnName("revoked");

                entity.Property(e => e.RevokedByIp)
                    .HasMaxLength(45)
                    .HasColumnName("revokedByIp");

                entity.Property(e => e.ReplacedByToken)
                    .HasColumnName("replacedByToken");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.RefreshTokens)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("refresh_token_user");

                //entity.HasIndex(e => e.Id, "Id").IsUnique();
            });

            modelBuilder.Entity<Team>(entity =>
            {
                entity.ToTable("team");

                entity.Property(e => e.Id)
                    .HasColumnName("id");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(64)
                    .HasColumnName("name");

                entity.Property(e => e.CaptainId)
                    .HasColumnName("captain_id")
                    .IsRequired(false);
                //.HasDefaultValueSql("'1'");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(5)
                    .HasColumnName("code")
                    .IsFixedLength(true);

                entity.HasOne(d => d.Captain)
                    .WithMany(p => p.Teams)
                    .HasForeignKey(d => d.CaptainId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("captain");

                entity.HasOne(t => t.Medium)
                    .WithMany(m => m.Team)
                    .HasForeignKey(t => t.MediumId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("team_medium")
                    .IsRequired(false);

                entity.HasIndex(e => e.CaptainId, "captain");

                entity.HasIndex(e => e.Code, "code")
                    .IsUnique();
            });

            modelBuilder.Entity<TeamInGroup>(entity =>
            {
                entity.ToTable("team_in_group");

                entity.Property(e => e.Id)
                    .HasColumnName("id");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.GroupId)
                    .HasColumnName("group_id");

                entity.Property(e => e.TeamId)
                    .HasColumnName("team_id");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.TeamInGroups)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("team_in_group_group");

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.TeamInGroups)
                    .HasForeignKey(d => d.TeamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("team_in_group_team");

                //entity.HasIndex(e => e.TeamId, "team_in_group_team");

                entity.HasIndex(e => e.GroupId, "team_in_group_group");
            });

            modelBuilder.Entity<TeamInTournament>(entity =>
            {
                entity.ToTable("team_in_tournament");

                entity.HasKey(e => new { e.TournamentId, e.TeamId })
                    .HasName("PRIMARY");

                entity.Property(e => e.TeamId)
                    .HasColumnName("team_id");

                entity.Property(e => e.TournamentId)
                    .HasColumnName("tournament_id");

                entity.Property(e => e.Paid)
                    .HasColumnName("paid");

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.TeamInTournaments)
                    .HasForeignKey(d => d.TeamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("team_in_tournament_team");

                entity.HasOne(d => d.Tournament)
                    .WithMany(p => p.TeamInTournaments)
                    .HasForeignKey(d => d.TournamentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("team_in_tournament_tournament");

                entity.HasIndex(e => e.TournamentId, "team_in_tournament_tournament");
            });

            modelBuilder.Entity<Tournament>(entity =>
            {
                entity.ToTable("tournament");

                entity.Property(e => e.Id)
                    .HasColumnName("id");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("name");

                entity.Property(e => e.StartDate)
                    .HasColumnName("start_date");

                entity.Property(e => e.EndDate)
                    .HasColumnName("end_date");

                entity.Property(e => e.Description)
                    .HasColumnName("description");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(256)
                    .HasColumnName("address");

                entity.HasOne(t => t.Medium)
                    .WithMany(m => m.Tournament)
                    .HasForeignKey(t => t.MediumId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("tournament_medium")
                    .IsRequired(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.Property(e => e.Id)
                    .HasColumnName("id");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(320)
                    .HasColumnName("email");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(32)
                    .HasColumnName("name");

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasMaxLength(64)
                    .HasColumnName("surname");

                entity.HasIndex(e => e.Email, "email")
                    .IsUnique();
            });

            ModelBuilderExtensions.Seed(modelBuilder);

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            /*modelBuilder.Entity<IdentityRole<int>>().HasData(
                new IdentityRole<int>() { Id = 1, Name = "Admin", NormalizedName = "Admin" },
                new IdentityRole<int>() { Id = 2, Name = "Employee", NormalizedName = "Employee" },
                new IdentityRole<int>() { Id = 3, Name = "User", NormalizedName = "User" }
                );*/
        }
    }
}
