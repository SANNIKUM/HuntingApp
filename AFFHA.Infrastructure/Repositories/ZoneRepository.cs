using AFFHA.Domain;
using AFFHA.Domain.SeedWork;
using AFFHA.Domain.Zones;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AFFHA.Infrastructure.Repositories
{
    public class ZoneRepository : IZoneRepository
    {
        private readonly AFFHADbContext _context;

        public IUnitOfWork UnitOfWork => _context;

        public ZoneRepository(AFFHADbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public  async void AddAsync(Zone zone)
        {
            await _context.Zones.AddAsync(zone);
        }

        public async Task<IList<Zone>> GetAllAsync()
        {
           return await _context.Zones.ToListAsync();
        }

        public async Task<Zone> GetAsync(int zoneId)
        {
            return await _context.Zones.Include(a => a.Pictures).SingleOrDefaultAsync(z => z.Id == zoneId);
        }

        public async void UpdateAsync(Zone order)
        {
            throw new NotImplementedException();
        }
    }
}
