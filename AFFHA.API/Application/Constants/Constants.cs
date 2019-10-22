namespace AFFHA.API.Application.Constants
{
    public static class Constants
    {
        public static class ImageType
        {
           public const string JPEG = ".jpeg";
           public const string PNG = ".png";
        }

        public static class CustomExceptionMessage
        {
            public const string MORE_THEN_200_MB = "File size is more then 200 MB";
            public const string NOT_SUPPORTED_TYPE = "Uploaded file has different extension";
        }
    }
}
