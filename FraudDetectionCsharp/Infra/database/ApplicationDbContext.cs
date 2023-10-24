using FraudDetectionCsharp.Domain.entities;
using Microsoft.EntityFrameworkCore;

namespace FraudDetectionCsharp.Infra.database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Fraud> Frauds { get; set; }

        public DbSet<FraudReport> FraudReport { get; set; }

        public DbSet<Payment> Payment { get; set; }

        public DbSet<Transaction> Transaction { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Account>()
                .Property(a => a.Balance)
                .HasColumnType("decimal(18,2)");  // Especifica o tipo de coluna e a precisão

            modelBuilder.Entity<Fraud>()
                .Property(f => f.FraudAmount)
                .HasColumnType("decimal(18,2)");  // Especifica o tipo de coluna e a precisão


            modelBuilder.Entity<Payment>()
                .Property(p => p.Amount)
                .HasColumnType("decimal(18,2)");  // Especifica o tipo de coluna e a precisão

            modelBuilder.Entity<Transaction>()
                .Property(t => t.Amount)
                .HasColumnType("decimal(18,2)");  // Especifica o tipo de coluna e a precisão
        }
    }
}