using Microsoft.EntityFrameworkCore;
using MMT.CustomerOrder.DataSource;
using MMT.CustomerOrder.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MMT.CustomerOrder.Core
{
    public abstract class Repository<T>:IRepository<T> where T: class,new()
    {
        protected readonly MMTDigitalContext Context;
        public Repository(MMTDigitalContext context)
        {
            this.Context = context;
        }

        public async Task<T> Find(int id)
        {
            return await Context.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await Context.Set<T>().ToListAsync();
        }
    }
}
