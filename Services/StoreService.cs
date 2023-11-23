using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OrderWebApp.Infrastructure;
using OrderWebApp.Models;

namespace OrderWebApp.Services
{
    public class StoreService
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public StoreService(
            AppDbContext appDbContext,
            IMapper mapper) 
        { 
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<StoreDto>> GetStores()
        {
            var dbStores = await _appDbContext.Stores.AsNoTracking().ToListAsync();
            return _mapper.Map<IEnumerable<StoreDto>>(dbStores);
        }
    }
}
