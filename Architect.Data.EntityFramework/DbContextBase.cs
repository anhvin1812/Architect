using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Architect.Entities;

namespace Architect.Data.EntityFramework
{
    public abstract class DbContextBase : DbContext, IDbContext
    {
        private readonly Guid _instanceId;

        protected DbContextBase(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
            _instanceId = new Guid();
            Configuration.LazyLoadingEnabled = false;
        }

        public Guid InstanceId
        {
            get { return _instanceId; }
        }

        public new DbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }

        public override int SaveChanges()
        {
            SyncObjectStatePreCommit();
            var change = base.SaveChanges();
            SyncObjectStatePostCommit();
            return change;
        }

        public override Task<int> SaveChangesAsync()
        {
            SyncObjectStatePostCommit();
            var changeAsync = base.SaveChangesAsync();
            SyncObjectStatePostCommit();
            return changeAsync;
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            SyncObjectStatePostCommit();
            var changeAsync = base.SaveChangesAsync(cancellationToken);
            SyncObjectStatePostCommit();
            return changeAsync;
        }

        public void SyncObjectState(object entity)
        {
            Entry(entity).State = StateHelper.ConvertState(((IObjectState) entity).State);
        }

        private void SyncObjectStatePreCommit()
        {
            foreach (var dbEntityEntry in ChangeTracker.Entries())
            {
                dbEntityEntry.State = StateHelper.ConvertState(((IObjectState)dbEntityEntry.Entity).State);
            }
        }

        private void SyncObjectStatePostCommit()
        {
            foreach (var dbEntityEntry in ChangeTracker.Entries())
            {
                ((IObjectState)dbEntityEntry.Entity).State = StateHelper.ConvertState(dbEntityEntry.State);
            }
        }
    }
}
