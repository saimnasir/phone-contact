using DataModels;
using Microsoft.EntityFrameworkCore;
using Reporting.Repositories.SchemaDefinitions;
using System.Threading;
using System.Threading.Tasks;

namespace Reporting.Repositories
{
    public class ReportingContext : DbContext, IUnitOfWork
    {
        public const string DEFAULT_SCHEMA = "RPT";
        public DbSet<Report> Reports { get; set; }
        //public DbSet<Author> Authors { get; set; }
        //public DbSet<Genre> Genres { get; set; }
        public ReportingContext(DbContextOptions<ReportingContext> options) :
        base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ReportEntitySchemaConfiguration());
            //modelBuilder.ApplyConfiguration(new GenreEntitySchemaConfiguration());
            //modelBuilder.ApplyConfiguration(new AuthorEntitySchemaConfiguration());
            base.OnModelCreating(modelBuilder);
        }
        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            await SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
