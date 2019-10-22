namespace AFFHA.Domain.Zones
{
    using AFFHA.Domain.SeedWork;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IZoneRepository : IRepository<Zone>
    {
        void AddAsync(Zone  zone);

        void UpdateAsync(Zone order);

        Task<Zone> GetAsync(int zoneId);

        Task<IList<Zone>> GetAllAsync();
    }
}
