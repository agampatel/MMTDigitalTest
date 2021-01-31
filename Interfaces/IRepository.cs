using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MMT.CustomerOrder.Interfaces
{
    public interface IRepository<T>
    {
        Task<T> Find(int id);
        Task<IEnumerable<T>> GetAll();
    }
}
