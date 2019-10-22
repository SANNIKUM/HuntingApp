namespace AFFHA.Infrastructure
{
    using Microsoft.EntityFrameworkCore;
    using AFFHA.Infrastructure.EnitiesConfiguration;
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using AFFHA.Infrastructure.Extensions;
    using Microsoft.AspNetCore.Http;
    using System;
    using System.Threading.Tasks;
    using System.Threading;
    using AFFHA.Domain;
    using AFFHA.Domain.SeedWork;
    using Microsoft.EntityFrameworkCore.Storage;
    using System.Data;
    using AFFHA.Domain.Application;
    using Microsoft.EntityFrameworkCore.Design;
    using MediatR;
    using Microsoft.Extensions.Configuration;

    public class AFFHADbContext : DbContext, IUnitOfWork
    {
        private readonly IHttpContextAccessor _context;
        private IDbContextTransaction _currentTransaction;

        public AFFHADbContext(DbContextOptions<AFFHADbContext> options, IHttpContextAccessor context) : base(options)
        {
            _context = context;
        }

        public AFFHADbContext(DbContextOptions<AFFHADbContext> options) : base(options)
        {

        }

        public DbSet<Application> Applications { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<Zone> Zones { get; set; }
      
        public IDbContextTransaction GetCurrentTransaction() => _currentTransaction;

        public bool HasActiveTransaction => _currentTransaction != null;
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.UseSoftDelete();
            builder.Entity<Picture>(ConfigurePicture.Picture);
            builder.Entity<Zone>(ConfigureZone.Zone);
        }

        private void AddTrackingInformation()
        {
            var user = _context.HttpContext?.User?.Identity?.Name ?? "Unknown";
            var now = DateTimeOffset.Now;

            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is BaseEntityTrackable baseEntityTrackable)
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            baseEntityTrackable.CreatedAt = now;
                            baseEntityTrackable.CreatedBy = user;
                            baseEntityTrackable.ModifiedAt = now;
                            baseEntityTrackable.ModifiedBy = user;
                            break;
                        case EntityState.Modified:
                            baseEntityTrackable.ModifiedAt = now;
                            baseEntityTrackable.ModifiedBy = user;
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// soft delete the item
        /// </summary>
        private void HandleSoftDelete()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is ISoftDeletable
                    && entry.State == EntityState.Deleted)
                {
                    entry.State = EntityState.Modified;
                    entry.Property("IsDeleted").CurrentValue = true;
                }
            }
        }

        
        /// <summary>
        /// Override SaveChangesAsync to add 'CreatedAt' and 'ModifiedAt' before saving the changes.
        /// </summary>
        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken)
        {
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            OnBeforeSaving();
            var result = await base.SaveChangesAsync(cancellationToken);
            return true;
        }
        private void OnBeforeSaving()
        {
            HandleSoftDelete();
            AddTrackingInformation();
        }
        /// <summary>
        /// Transaction Setup
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken)
        {
            if (_currentTransaction != null) return null;

            _currentTransaction = await Database.BeginTransactionAsync(cancellationToken);

            return _currentTransaction;
        }

        public async Task CommitTransactionAsync(IDbContextTransaction transaction)
        {
            if (transaction == null) throw new ArgumentNullException(nameof(transaction));
            if (transaction != _currentTransaction) throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");

            try
            {
                await SaveChangesAsync();
                transaction.Commit();
            }
            catch
            {
                RollbackTransaction();
                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        public void RollbackTransaction()
        {
            try
            {
                _currentTransaction?.Rollback();
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        
    }
    public class AFFHADbContextDesignFactory : IDesignTimeDbContextFactory<AFFHADbContext>
    {
        private readonly IHttpContextAccessor _context;
        private readonly IConfiguration _configuration;

        public AFFHADbContextDesignFactory()
        {

        }
         public AFFHADbContextDesignFactory(IHttpContextAccessor context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public AFFHADbContext CreateDbContext(string[] args)
        {
            if (_context != null)
            {
                var optionsBuilder = new DbContextOptionsBuilder<AFFHADbContext>()
                   .UseNpgsql(_configuration["ConnectionStrings:DefaultConnection"]);
                return new AFFHADbContext(optionsBuilder.Options, _context);
            }
            else
            {
                var optionsBuilder = new DbContextOptionsBuilder<AFFHADbContext>()
                .UseNpgsql("Server=localhost;Port=5432;Database=affha;Integrated Security=false;Pooling=true;User Id=postgres;Password=postgres");
                return new AFFHADbContext(optionsBuilder.Options);
            }

          
        }

       
    }
}