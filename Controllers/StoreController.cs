using Microsoft.AspNetCore.Mvc;
using OrderWebApp.Models;
using OrderWebApp.Services;

namespace OrderWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly StoreService _storeService;

        public StoreController(StoreService storeService)
        {
            _storeService = storeService;
        }

        [HttpGet]
        public async Task<IEnumerable<StoreDto>> GetStores()
        {
            return await _storeService.GetStores();
        }
    }
}
