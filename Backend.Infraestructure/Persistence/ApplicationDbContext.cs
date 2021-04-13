
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using Backend.Application.Interfaces;

namespace Backend.Infraestructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        
        
        public DatabaseFacade Facade => this.Database;

     

        public ApplicationDbContext():base()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connString = "Server=(localdb)\\mssqllocaldb;Database=HavenServer;ConnectRetryCount=0;Trusted_Connection=True;MultipleActiveResultSets=true\"";
                optionsBuilder
                   
                    .EnableSensitiveDataLogging(false)
                    .UseSqlServer(connString);
            }
            base.OnConfiguring(optionsBuilder);
        }

        public ApplicationDbContext(DbContextOptions options):base(options)
        {
           // _currentUserService = currentUserService;
           // _dateTime = dateTime;
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            //foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            //{
            //    switch (entry.State)
            //    {
            //        case EntityState.Added:
            //            entry.Entity.CreatedBy = "";// _currentUserService.UserId;
            //            entry.Entity.Created = DateTime.Now;
            //            break;
            //        case EntityState.Modified:
            //            entry.Entity.LastModifiedBy = ""; //_currentUserService.UserId;
            //            entry.Entity.LastModified = DateTime.Now;
            //            break;
            //    }
            //}

            var response = await base.SaveChangesAsync(cancellationToken);

            return response;

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            
            base.OnModelCreating(builder);
        }
    }

}
