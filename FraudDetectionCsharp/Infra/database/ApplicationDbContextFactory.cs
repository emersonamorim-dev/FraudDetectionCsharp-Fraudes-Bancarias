using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace FraudDetectionCsharp.Infra.database
{ 
public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder.UseSqlServer("server=DESKTOP-95AUUGB; database=FraudDetectionDB;Trusted_Connection=true; TrustServerCertificate=True;");

        return new ApplicationDbContext(optionsBuilder.Options);
    }
}

}