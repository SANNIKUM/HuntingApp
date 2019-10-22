using AFFHA.Domain;
using AFFHA.Domain.Pictures;
using AFFHA.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AFFHA.Infrastructure.Repositories
{
    public class PictureRepository : IPictureRepository
    {
        private readonly AFFHADbContext _context;

        public IUnitOfWork UnitOfWork => _context;


        public PictureRepository(AFFHADbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async void AddAsync(Picture picture)
        {
            await _context.Pictures.AddAsync(picture);           
        }

        public Task<Picture> GetAsync(int pictureId)
        {
            throw new NotImplementedException();
        }

        public Task<IList<Picture>> GetByZoneIdAsync(int zoneId)
        {
            throw new NotImplementedException();
        }

        public void Update(Picture order)
        {
            throw new NotImplementedException();
        }

        public async void AddRangeAsync(IList<Picture> pictures)
        {
           await _context.Pictures.AddRangeAsync(pictures);
        }
    }
}
