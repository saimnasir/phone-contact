using DataModels;
using Microsoft.EntityFrameworkCore;
using PhoneContact.Repositories.SchemaDefinitions;
using System.Threading;
using System.Threading.Tasks;

namespace PhoneContact.Repositories
{
    public class PhoneContactContext : DbContext, IUnitOfWork
    {
        public const string DEFAULT_SCHEMA = "PHC";
        public DbSet<Person> Persons { get; set; }
        public DbSet<ContactInfo> ContactInfos { get; set; }
        //public DbSet<Genre> Genres { get; set; }
        public PhoneContactContext(DbContextOptions<PhoneContactContext> options) :
        base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PersonEntitySchemaConfiguration());
            modelBuilder.ApplyConfiguration(new ContactInfoEntitySchemaConfiguration());
            modelBuilder.ApplyConfiguration(new LocationEntitySchemaConfiguration());
            base.OnModelCreating(modelBuilder);
        }
        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            await SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
