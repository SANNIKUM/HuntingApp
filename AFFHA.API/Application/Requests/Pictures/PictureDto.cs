namespace AFFHA.API.Application.Requests.Pictures
{
    public class PictureDto
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public byte[] BinaryData { get; set; }
        public int ZoneId { get; set; }
        public string ContentType { get; set; }
    }
}
