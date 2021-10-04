using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace SieGraSieMa.Models
{
    public partial class SieGraSieMaContext : DbContext
    {
        public SieGraSieMaContext()
        {
        }

        public SieGraSieMaContext(DbContextOptions<SieGraSieMaContext> options)
            : base(options)
        {
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
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Team> Teams { get; set; }
        public virtual DbSet<TeamInGroup> TeamInGroups { get; set; }
        public virtual DbSet<TeamInTournament> TeamInTournaments { get; set; }
        public virtual DbSet<Tournament> Tournaments { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }

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

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(1)
                    .HasColumnName("name")
                    .IsFixedLength(true);

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
                entity.ToTable("match");

                entity.HasIndex(e => e.TeamAwayId, "match_away");

                entity.HasIndex(e => new { e.TeamHomeId, e.TeamAwayId }, "meet")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.EndDate).HasColumnName("end_date");

                entity.Property(e => e.Referee)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("referee");

                entity.Property(e => e.StartDate).HasColumnName("start_date");

                entity.Property(e => e.TeamAwayId).HasColumnName("team_away_id");

                entity.Property(e => e.TeamAwayScore).HasColumnName("team_away_score");

                entity.Property(e => e.TeamHomeId).HasColumnName("team_home_id");

                entity.Property(e => e.TeamHomeScore).HasColumnName("team_home_score");

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
                    .HasConstraintName("Table_28_user");
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

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("roles");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(32)
                    .HasColumnName("name");
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
                    .HasConstraintName("team_group");

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.TeamInGroups)
                    .HasForeignKey(d => d.TeamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Table_25_team");
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
                    .HasConstraintName("Table_27_team");

                entity.HasOne(d => d.Tournament)
                    .WithMany(p => p.TeamInTournaments)
                    .HasForeignKey(d => d.TournamentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Table_27_tournament");
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
                    .IsRequired()
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

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(256)
                    .HasColumnName("password");

                entity.Property(e => e.Salt)
                    .IsRequired()
                    .HasMaxLength(256)
                    .HasColumnName("salt");

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasMaxLength(64)
                    .HasColumnName("surname");
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId })
                    .HasName("PRIMARY");

                entity.ToTable("user_roles");

                entity.HasIndex(e => e.RoleId, "Table_26_role");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Table_26_role");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Table_26_user");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
