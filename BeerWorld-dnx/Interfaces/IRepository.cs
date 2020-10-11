using BeerWorld.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeerWorld.Interfaces
{
    public interface IRepository<T> where T : BaseModel
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> SearchAsync(Func<T, bool> criteria);

        Task<T> UpdateAsync(T entry);

        Task<T> InsertAsync(T entry);
    }
}
