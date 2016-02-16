using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public interface IRepository<TAggregate> where TAggregate : IAggregateRoot
    {
        IQueryable<TAggregate> All();
        void Insert(TAggregate item);
        void Delete(TAggregate item);
        void Commit();
    }
}
