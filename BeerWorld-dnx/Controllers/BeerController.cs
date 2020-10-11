using BeerWorld.Extensions;
using BeerWorld.Interfaces;
using BeerWorld.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeerWorld.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeerController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IBeerRepository _repo;

        public BeerController(ILogger<BeerController> logger, IBeerRepository repo)
        {
            _repo = repo;
            _logger = logger;
        }

        [HttpGet]
        public Task<IEnumerable<BeerModel>> Index() => _repo.GetAllAsync();

        [HttpGet]
        [Route("Lookup")]
        public Task<IEnumerable<BeerModel>> Lookup([FromQuery] string criteria) => _repo.SearchByName(criteria);

        [HttpPost]
        [Route("Add")]
        public async Task<BeerModel> Add([FromBody] NewBeerModel beer)
        {
            beer.AssertIsValid();

            return await _repo.InsertAsync(new BeerModel
            {
                Name = beer.Name,
                Rating = beer.Rating,
                BeerType = beer.BeerType
            });
        }

        [HttpPut]
        [Route("Update")]
        public async Task<BeerModel> Update([FromQuery] Guid id, [FromBody] BeerModel beer)
        {
            beer.AssertIsValid();

            return await _repo.UpdateAsync(beer);
        }
    }
}
