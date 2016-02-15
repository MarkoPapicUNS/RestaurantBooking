using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public interface IRepository<TAggregate, in TKey> where TAggregate : IAggregateRoot
    {
        IQueryable<TAggregate> All();
        TAggregate Find(TKey id);
        void Insert(TAggregate item);
        void Commit();
    }
}
