using BeerWorld.Interfaces;
using BeerWorld.Models;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeerWorld.Repo
{
    public class Repository<T> : IRepository<T> where T : BaseModel
    {
        private readonly IMemoryCache _cache;
        private readonly string _key;

        public Repository(IMemoryCache cache, string key)
        {
            _cache = cache;
            _key = key;
        }

        public virtual Task<IEnumerable<T>> GetAllAsync()
        {
            IEnumerable<T> list;
            _cache.TryGetValue(_key, out list);

            return Task.FromResult(list ?? Enumerable.Empty<T>());
        }

        public virtual Task<IEnumerable<T>> SearchAsync(Func<T, bool> criteria)
        {
            IEnumerable<T> list;
            _cache.TryGetValue(_key, out list);

            return list == null ? Task.FromResult(Enumerable.Empty<T>()) : Task.FromResult(list.Where(criteria));
        }

        public virtual async Task<T> UpdateAsync(T entry)
        {
            var list = await this.GetAllAsync();

            _cache.Set(_key, list.Select(item => item.Id == entry.Id ? entry : item));

            return entry;
        }

        public virtual async Task<T> InsertAsync(T entry)
        {
            entry.Id = Guid.NewGuid();

            var list = await this.GetAllAsync();

            _cache.Set("Beerz", list.Append(entry));

            return entry;
        }
    }
}
