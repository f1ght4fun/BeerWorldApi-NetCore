using BeerWorld.Interfaces;
using BeerWorld.Models;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace BeerWorld.Repo
{
    public class BeerRepository : Repository<BeerModel>, IBeerRepository
    {
        private const string beerRepoKey = "Beerz";
        private readonly IMemoryCache _cache;

        public BeerRepository(IMemoryCache cache) : base(cache, beerRepoKey)
        {
            _cache = cache;
        }

        public override async Task<BeerModel> UpdateAsync(BeerModel entry)
        {
            var beerList = await GetAllAsync();
            var beer = beerList.SingleOrDefault(beer => beer.Id == entry.Id);

            if (beer == null)
                throw new KeyNotFoundException();

            return await base.UpdateAsync(entry);
        }

        public override async Task<BeerModel> InsertAsync(BeerModel entry)
        {
            var beerzFound = await this.SearchAsync((beer) => beer.Name == entry.Name && beer.BeerType == entry.BeerType);
            if (beerzFound.Any())
                throw new DuplicateNameException("Beer already exists");

            return await base.InsertAsync(entry);
        }

        public async Task<IEnumerable<BeerModel>> SearchByName(string criteria)
        {
            return await SearchAsync(beer => beer.Name.Contains(criteria));
        }
    }
}
