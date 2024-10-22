using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeliveryService.Domain.Inerfaces;

namespace DeliveryService.Domain
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<TEntity> repository<TEntity>() where TEntity : class;
        Task<int> Complete();
    }
}
