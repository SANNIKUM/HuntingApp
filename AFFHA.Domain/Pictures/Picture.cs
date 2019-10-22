/// <summary>
/// Defining Picture/Photo Model
/// Created By : Sanni Kumar @12 Oct 2019
/// Modified By :
/// Modification :
/// </summary>
namespace AFFHA.Domain
{
    using AFFHA.Domain.SeedWork;
    public class Picture : BaseEntityTrackable, IRoot
    {
        public string Name { get; set; }
        public byte[] BinaryData { get; set; }
        public int ZoneId { get; set; }
        public Zone Zone { get; set; }
        public string ContentType { get; set; }
    }
}
