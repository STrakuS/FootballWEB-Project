using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace FootballWEB.Models;

public partial class FootballDbContext : DbContext
{
    public FootballDbContext()
    {
    }

    public FootballDbContext(DbContextOptions<FootballDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Match> Matches { get; set; }

    public virtual DbSet<Player> Players { get; set; }

    public virtual DbSet<PlayerStatistic> PlayerStatistics { get; set; }

    public virtual DbSet<Standing> Standings { get; set; }

    public virtual DbSet<Team> Teams { get; set; }

    public virtual DbSet<Tournament> Tournaments { get; set; }

    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Match>(entity =>
        {
            entity.HasKey(e => e.MatchId).HasName("PK__Match__4218C837400172A7");

            entity.ToTable("Match");

            entity.Property(e => e.MatchId).HasColumnName("MatchID");
            entity.Property(e => e.AwayScore).HasDefaultValue(0);
            entity.Property(e => e.AwayTeamId).HasColumnName("AwayTeamID");
            entity.Property(e => e.HomeScore).HasDefaultValue(0);
            entity.Property(e => e.HomeTeamId).HasColumnName("HomeTeamID");
            entity.Property(e => e.TournamentId).HasColumnName("TournamentID");

            entity.HasOne(d => d.AwayTeam).WithMany(p => p.MatchAwayTeams)
                .HasForeignKey(d => d.AwayTeamId)
                .HasConstraintName("FK__Match__AwayTeamI__693CA210");

            entity.HasOne(d => d.HomeTeam).WithMany(p => p.MatchHomeTeams)
                .HasForeignKey(d => d.HomeTeamId)
                .HasConstraintName("FK__Match__HomeTeamI__68487DD7");

            entity.HasOne(d => d.Tournament).WithMany(p => p.Matches)
                .HasForeignKey(d => d.TournamentId)
                .HasConstraintName("FK__Match__Tournamen__6A30C649");
        });

        modelBuilder.Entity<Player>(entity =>
        {
            entity.HasKey(e => e.PlayerId).HasName("PK__Player__4A4E74A8A10C4AE9");

            entity.ToTable("Player");

            entity.Property(e => e.PlayerId).HasColumnName("PlayerID");
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.Position).HasMaxLength(50);
            entity.Property(e => e.TeamId).HasColumnName("TeamID");

            entity.HasOne(d => d.Team).WithMany(p => p.Players)
                .HasForeignKey(d => d.TeamId)
                .HasConstraintName("FK__Player__TeamID__6383C8BA");
        });

        modelBuilder.Entity<PlayerStatistic>(entity =>
        {
            entity.HasKey(e => e.StatId).HasName("PK__PlayerSt__3A162D1E8B3262C6");

            entity.Property(e => e.StatId).HasColumnName("StatID");
            entity.Property(e => e.Assists).HasDefaultValue(0);
            entity.Property(e => e.Goals).HasDefaultValue(0);
            entity.Property(e => e.MatchId).HasColumnName("MatchID");
            entity.Property(e => e.PlayerId).HasColumnName("PlayerID");
            entity.Property(e => e.RedCards).HasDefaultValue(0);
            entity.Property(e => e.YellowCards).HasDefaultValue(0);

            entity.HasOne(d => d.Match).WithMany(p => p.PlayerStatistics)
                .HasForeignKey(d => d.MatchId)
                .HasConstraintName("FK__PlayerSta__Match__71D1E811");

            entity.HasOne(d => d.Player).WithMany(p => p.PlayerStatistics)
                .HasForeignKey(d => d.PlayerId)
                .HasConstraintName("FK__PlayerSta__Playe__70DDC3D8");
        });

        modelBuilder.Entity<Standing>(entity =>
        {
            entity.HasKey(e => e.StandingId).HasName("PK__Standing__FC2758E14F2AEB81");

            entity.HasIndex(e => e.TeamId, "UQ__Standing__123AE7B80DCC24CE").IsUnique();

            entity.Property(e => e.StandingId).HasColumnName("StandingID");
            entity.Property(e => e.Draws).HasDefaultValue(0);
            entity.Property(e => e.GoalsAgainst).HasDefaultValue(0);
            entity.Property(e => e.GoalsFor).HasDefaultValue(0);
            entity.Property(e => e.Losses).HasDefaultValue(0);
            entity.Property(e => e.Played).HasDefaultValue(0);
            entity.Property(e => e.Points).HasDefaultValue(0);
            entity.Property(e => e.TeamId).HasColumnName("TeamID");
            entity.Property(e => e.Wins).HasDefaultValue(0);

            entity.HasOne(d => d.Team).WithOne(p => p.Standing)
                .HasForeignKey<Standing>(d => d.TeamId)
                .HasConstraintName("FK__Standings__TeamI__7C4F7684");
        });

        modelBuilder.Entity<Team>(entity =>
        {
            entity.HasKey(e => e.TeamId).HasName("PK__Team__123AE7B9253B71DB");

            entity.ToTable("Team");

            entity.HasIndex(e => e.TeamName, "UQ__Team__4E21CAAC83496027").IsUnique();

            entity.Property(e => e.TeamId).HasColumnName("TeamID");
            entity.Property(e => e.TeamName).HasMaxLength(100);
        });

        modelBuilder.Entity<Tournament>(entity =>
        {
            entity.HasKey(e => e.TournamentId).HasName("PK__Tourname__AC631333494D9484");

            entity.ToTable("Tournament");

            entity.Property(e => e.TournamentId).HasColumnName("TournamentID");
            entity.Property(e => e.City)
                .HasMaxLength(50)
                .HasDefaultValue("Ankara");
            entity.Property(e => e.Season).HasMaxLength(20);
            entity.Property(e => e.Stadium).HasMaxLength(100);
            entity.Property(e => e.TournamentName).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
