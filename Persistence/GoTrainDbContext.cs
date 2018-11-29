using Microsoft.EntityFrameworkCore;
using Semesterprojekt.Core.Entites;

namespace Semesterprojekt.Persistence
{
    public class GoTrainDbContext : DbContext {
        public GoTrainDbContext (DbContextOptions<GoTrainDbContext> options) : base (options) { }
        public DbSet<Trophy> Trophies { get; set; }
        public DbSet<TrainingSession> TrainingSessions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserTrophy> UserTrophies {get; set;}
        public DbSet<UserTrainingSession> UserTrainingSessions { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserTrainingSession>()
            .HasKey(x => new {
                x.TrainingSessionId, 
                x.UserId
            });
            modelBuilder.Entity<UserTrophy>()
            .HasKey(x=> new {
                x.UserId,
                x.TrophyId
            });
        }
    }
}