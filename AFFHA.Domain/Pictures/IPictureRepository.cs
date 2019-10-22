namespace AFFHA.Domain.Pictures
{
    using AFFHA.Domain.SeedWork;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IPictureRepository : IRepository<Picture>
    {
        void AddAsync(Picture zone);

        void AddRangeAsync(IList<Picture> pictures);

        void Update(Picture order);

        Task<Picture> GetAsync(int pictureId);

        Task<IList<Picture>> GetByZoneIdAsync(int zoneId);
    }
}
