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
        TAggregate Get(TKey id);
        void Save(TAggregate item);
        void Commit();
    }
}
