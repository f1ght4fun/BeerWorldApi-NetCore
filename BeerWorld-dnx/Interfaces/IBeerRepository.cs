using BeerWorld.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeerWorld.Interfaces
{
    public interface IBeerRepository : IRepository<BeerModel>
    {
        Task<IEnumerable<BeerModel>> SearchByName(string criteria);
    }
}
