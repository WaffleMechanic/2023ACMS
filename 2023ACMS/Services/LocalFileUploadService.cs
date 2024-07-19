namespace _2023ACMS.Services;

//public class LocalFileUploadService : IFileUploadService
//{

//    private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment IWebHostingEnvironment;

//    public LocalFileUploadService(Microsoft.AspNetCore.Hosting.IHostingEnvironment IWHE)
//    {
//        IWebHostingEnvironment = IWHE;
//    }

//    public async Task<string> UploadFileAsync(IFormFile file)
//    {
//        var filePath = Path.Combine(IWebHostingEnvironment.ContentRootPath, @"wwwroot\images", file.FileName);
//        using var fileStream = new FileStream(filePath, FileMode.Create);
//        await file.CopyToAsync(fileStream);
//        return filePath;
//    }
//}
