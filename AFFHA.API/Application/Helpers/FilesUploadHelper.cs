using AFFHA.API.Infrastructure.Shared.Exceptions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using static AFFHA.API.Application.Constants.Constants;

namespace AFFHA.API.Application.Helpers
{
    public class FilesUploadHelper
    {
        public static IEnumerable<FileDto> GetFilesDetail(IList<IFormFile> files, int Type)
        {
            IList<FileDto> filelist = new List<FileDto>();

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    string extension = Path.GetExtension(formFile.FileName);
                    if (Type == (int)FileType.Picture) {
                        if (extension == ImageType.PNG || extension == ImageType.JPEG )
                      
                            using (var memoryStream = new MemoryStream())
                            {
                                formFile.CopyTo(memoryStream);
                                // Upload the file if less than 200 MB
                                if (memoryStream.Length < 209715200)
                                {
                                    var file = new FileDto()
                                    {
                                        Name = formFile.FileName,
                                        ContentType = formFile.ContentType,
                                        ContentBinary = memoryStream.ToArray()
                                    };
                                    filelist.Add(file);
                                }
                                else
                                {
                                    throw new FileIsTooLargeException(CustomExceptionMessage.MORE_THEN_200_MB); ;
                                }
                            }
                        else
                        {
                            throw new Exception(CustomExceptionMessage.NOT_SUPPORTED_TYPE);
                        }
                    }
                }
            }
            return filelist;
        }
}

    enum FileType
    {
        Picture=1,
        CSV,
        excel
    }
    
    public class FileDto
    {
        public string Name { get; set; }
        public string ContentType { get; set; }
        public byte[] ContentBinary { get; set; }
    }
     
}
