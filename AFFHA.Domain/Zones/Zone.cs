/// <summary>
/// Defining Zone/Area Model
/// Created By : Sanni Kumar @12 Oct 2019
/// Modified By :
/// Modification :
/// </summary>
namespace AFFHA.Domain
{
    using AFFHA.Domain.SeedWork;
    using System.Collections.Generic;

   public class Zone : BaseEntityTrackable, ISoftDeletable, IRoot
    {
        public string Name { get; set; }
        public string JeoData { get; set; }
        public ICollection<Picture> Pictures { get; set; }
    }
}
