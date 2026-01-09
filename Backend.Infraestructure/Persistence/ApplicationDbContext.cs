
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using Backend.Application.Interfaces;
using Backend.Models;

namespace Backend.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        
        
        public DatabaseFacade Facade => this.Database;

        public DbSet<DummyDbClass> DummyDbClass { get; set; }

        public ApplicationDbContext():base()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connString = "";
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

        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            
            base.OnModelCreating(builder);
        }
    }

}
