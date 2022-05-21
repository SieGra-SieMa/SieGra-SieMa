using System;
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

        public SieGraSieMaContext(DbContextOptions<SieGraSieMaContext> options)
            : base(options)
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
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySQL("Server=localhost;database=SieGraSieMa;user=siegra;password=siema");
                
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

                entity.HasIndex(e => e.TournamentId, "album_tournament");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.CreateDate).HasColumnName("create_date");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(256)
                    .HasColumnName("name");

                entity.Property(e => e.TournamentId).HasColumnName("tournament_id");

                entity.HasOne(d => d.Tournament)
                    .WithMany(p => p.Albums)
                    .HasForeignKey(d => d.TournamentId)
                    .HasConstraintName("album_tournament");
            });

            modelBuilder.Entity<Contest>(entity =>
            {
                entity.ToTable("contest");

                entity.HasIndex(e => e.TournamentId, "contest_tournament");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(64)
                    .HasColumnName("name");

                entity.Property(e => e.TournamentId).HasColumnName("tournament_id");

                entity.HasOne(d => d.Tournament)
                    .WithMany(p => p.Contests)
                    .HasForeignKey(d => d.TournamentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("contest_tournament");
            });

            modelBuilder.Entity<Contestant>(entity =>
            {
                entity.HasKey(e => new { e.ContestId, e.UserId })
                    .HasName("PRIMARY");

                entity.ToTable("contestants");

                entity.HasIndex(e => e.UserId, "contestants_user");

                entity.Property(e => e.ContestId).HasColumnName("contest_id");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Points).HasColumnName("points");

                entity.HasOne(d => d.Contest)
                    .WithMany(p => p.Contestants)
                    .HasForeignKey(d => d.ContestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("contestants_contest");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Contestants)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("contestants_user");
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.ToTable("group");

                entity.HasIndex(e => e.TournamentId, "group_tournament");

                entity.HasIndex(e => new { e.Name, e.TournamentId }, "name_in_tournament")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Ladder).HasColumnName("ladder");

                entity.Property(e => e.Phase)
                    .HasColumnName("phase");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(2)
                    .HasColumnName("name");

                entity.Property(e => e.TournamentId).HasColumnName("tournament_id");

                entity.HasOne(d => d.Tournament)
                    .WithMany(p => p.Groups)
                    .HasForeignKey(d => d.TournamentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("group_tournament");
            });

            modelBuilder.Entity<Log>(entity =>
            {
                entity.ToTable("logs");

                entity.HasIndex(e => e.UserId, "logs_user");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Action)
                    .IsRequired()
                    .HasMaxLength(256)
                    .HasColumnName("action");

                entity.Property(e => e.Time).HasColumnName("time");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Logs)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("logs_user");
            });

            modelBuilder.Entity<Match>(entity =>
            {
                entity.HasKey(e => new { e.TournamentId, e.Phase, e.MatchId })
                    .HasName("PRIMARY");

                entity.ToTable("match");

                entity.HasIndex(e => e.TeamAwayId, "match_away");

                entity.HasIndex(e => new { e.TeamHomeId, e.TeamAwayId }, "meet")
                    .IsUnique();

                entity.Property(e => e.MatchId).HasColumnName("match_id");

                //entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.HasOne(d => d.Tournament)
                    .WithMany(p => p.Matches)
                    .HasForeignKey(d => d.TournamentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("tournament");

                //entity.Property(e => e.EndDate).HasColumnName("end_date");

                //entity.Property(e => e.Referee)
                //    .IsRequired()
                //    .HasMaxLength(128)
                //    .HasColumnName("referee");

                //entity.Property(e => e.StartDate).HasColumnName("start_date");

                entity.Property(e => e.TeamAwayId).HasColumnName("team_away_id").IsRequired(false);

                entity.Property(e => e.TeamAwayScore).HasColumnName("team_away_score").IsRequired(false);

                entity.Property(e => e.TeamHomeId).HasColumnName("team_home_id").IsRequired(false);

                entity.Property(e => e.TeamHomeScore).HasColumnName("team_home_score").IsRequired(false);

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
            });

            modelBuilder.Entity<Medium>(entity =>
            {
                entity.ToTable("media");

                entity.HasIndex(e => e.AlbumId, "media_album");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.AlbumId).HasColumnName("album_id");

                entity.Property(e => e.Url)
                    .IsRequired()
                    .HasMaxLength(256)
                    .HasColumnName("url");

                entity.HasOne(d => d.Album)
                    .WithMany(p => p.Media)
                    .HasForeignKey(d => d.AlbumId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("media_album");
            });

            modelBuilder.Entity<Newsletter>(entity =>
            {
                entity.ToTable("newsletter");

                entity.HasIndex(e => e.UserId, "Table_28_user");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Newsletters)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("newsletter_user");
            });

            modelBuilder.Entity<Player>(entity =>
            {
                entity.HasKey(e => new { e.TeamId, e.UserId })
                    .HasName("PRIMARY");

                entity.ToTable("player");

                entity.HasIndex(e => e.UserId, "player_user");

                entity.Property(e => e.TeamId).HasColumnName("team_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.Players)
                    .HasForeignKey(d => d.TeamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("player_team");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Players)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("player_user");
            });

            modelBuilder.Entity<RefreshToken>(entity =>
            {
                entity.HasIndex(e => e.Id, "Id")
                    .IsUnique();
                entity.ToTable("refresh_token");
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

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
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("refresh_token_user");

            });

            modelBuilder.Entity<Team>(entity =>
            {
                entity.ToTable("team");

                entity.HasIndex(e => e.CaptainId, "captain");

                entity.HasIndex(e => e.Code, "code")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.CaptainId)
                    .HasColumnName("captain_id")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(5)
                    .HasColumnName("code")
                    .IsFixedLength(true);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(64)
                    .HasColumnName("name");

                entity.HasOne(d => d.Captain)
                    .WithMany(p => p.Teams)
                    .HasForeignKey(d => d.CaptainId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("captain");
            });

            modelBuilder.Entity<TeamInGroup>(entity =>
            {
                entity.ToTable("team_in_group");

                entity.HasIndex(e => e.TeamId, "Table_25_team");

                entity.HasIndex(e => e.GroupId, "team_group");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.GroupId).HasColumnName("group_id");

                entity.Property(e => e.TeamId).HasColumnName("team_id");

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
            });

            modelBuilder.Entity<TeamInTournament>(entity =>
            {
                entity.HasKey(e => new { e.TeamId, e.TournamentId })
                    .HasName("PRIMARY");

                entity.ToTable("team_in_tournament");

                entity.HasIndex(e => e.TournamentId, "Table_27_tournament");

                entity.Property(e => e.TeamId).HasColumnName("team_id");

                entity.Property(e => e.TournamentId).HasColumnName("tournament_id");

                entity.Property(e => e.Paid).HasColumnName("paid");

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
            });

            modelBuilder.Entity<Tournament>(entity =>
            {
                entity.ToTable("tournament");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(256)
                    .HasColumnName("address");

                entity.Property(e => e.Description)
                    .HasColumnName("description");

                entity.Property(e => e.EndDate).HasColumnName("end_date");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("name");

                entity.Property(e => e.StartDate).HasColumnName("start_date");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.HasIndex(e => e.Email, "email")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(320)
                    .HasColumnName("email");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(32)
                    .HasColumnName("name");

               /* entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(256)
                    .HasColumnName("password");*/

                /*entity.Property(e => e.Salt)
                    .IsRequired()
                    .HasMaxLength(256)
                    .HasColumnName("salt");*/

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasMaxLength(64)
                    .HasColumnName("surname");
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
            //var salt = CreateSalt();

            var hasher = new PasswordHasher<User>();

            //hasher.HashPassword();

            modelBuilder.Entity<IdentityRole<int>>().HasData(
                new IdentityRole<int>() { Id = 1, Name = "Admin", NormalizedName="Admin" },
                new IdentityRole<int>() { Id = 2, Name = "Emp", NormalizedName = "Emp" },
                new IdentityRole<int>() { Id = 3, Name = "User", NormalizedName = "User" }
                );
            modelBuilder.Entity<User>().HasData(
            new User() { Id = 1, Name = "Adm", Surname = "In", Email = "admin@gmail.com", NormalizedEmail = "admin@gmail.com", PasswordHash = hasher.HashPassword(null, "haslo123"), EmailConfirmed = true, SecurityStamp = Guid.NewGuid().ToString() },
            new User() { Id = 2, Name = "Prac", Surname = "Ownik", Email = "pracownik@gmail.com", NormalizedEmail = "pracownik@gmail.com", PasswordHash = hasher.HashPassword(null, "haslo123"), EmailConfirmed = true, SecurityStamp = Guid.NewGuid().ToString() },
            new User() { Id = 3, Name = "Kap", Surname = "Itan", Email = "kapitan@gmail.com", NormalizedEmail = "kapitan@gmail.com", PasswordHash = hasher.HashPassword(null, "haslo123"), EmailConfirmed = true, SecurityStamp = Guid.NewGuid().ToString() },
            new User() { Id = 4, Name = "Gr", Surname = "acz", Email = "gracz@gmail.com", NormalizedEmail = "gracz@gmail.com", PasswordHash = hasher.HashPassword(null, "haslo123"), EmailConfirmed = true, SecurityStamp = Guid.NewGuid().ToString() }
            );

            modelBuilder.Entity<IdentityUserRole<int>>().HasData(
            new IdentityUserRole<int>() { UserId = 1, RoleId = 1 },
            new IdentityUserRole<int>() { UserId = 2, RoleId = 2 },
            new IdentityUserRole<int>() { UserId = 2, RoleId = 3 },
            new IdentityUserRole<int>() { UserId = 3, RoleId = 3 },
            new IdentityUserRole<int>() { UserId = 4, RoleId = 3 });


            modelBuilder.Entity<Team>().HasData(
            new Team() { Id = 1, Name = "Bogowie", CaptainId = 3, Code = "ABCDE" },
            new Team() { Id = 2, Name = "Demony", CaptainId = 3, Code = "EDCBA" });


            modelBuilder.Entity<Player>().HasData(
            new Player() { TeamId = 1, UserId = 3 },
            new Player() { TeamId = 1, UserId = 4 },
            new Player() { TeamId = 2, UserId = 3 });


            modelBuilder.Entity<Newsletter>().HasData(
            new Newsletter() { Id = 1, UserId = 3 });

            modelBuilder.Entity<Tournament>().HasData(new Tournament() { Name = "Turniej testowy numer 1" , Id=1, Address="Zbożowa -1"});

            


        }
    }
}
