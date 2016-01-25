using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Architect.Data.EntityFramework
{
    public interface IDbContext : IDisposable
    {
        Guid InstanceId { get; }

        DbSet<T> Set<T>() where T : class;

        Database Database { get; }

        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        Task<int> SaveChangesAsync();
        void SyncObjectState(object entity);
    }
}
