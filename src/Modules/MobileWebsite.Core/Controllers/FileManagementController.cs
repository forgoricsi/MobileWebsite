using Microsoft.AspNetCore.Mvc;
using OrchardCore.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileWebsite.Core.Controllers
{
    public class FileManagemenetController : Controller
    {
        private readonly IMediaFileStore _mediaFileStore;
        public FileManagemenetController(IMediaFileStore mediaFileStore)
        {
            _mediaFileStore = mediaFileStore;
        }

        public async Task<string> CreateFile()
        {
            using var stream = new MemoryStream(Encoding.UTF8.GetBytes("Hello World!"));
            await _mediaFileStore.CreateFileFromStreamAsync("Demo.txt", stream);

            return "OK";
        }

        public async Task<string> ReadFile()
        {
            var fileInfo = await _mediaFileStore.GetFileInfoAsync("Demo.txt");

            if (fileInfo == null)
            {
                return "File not found.";
            }

            var stream = await _mediaFileStore.GetFileStreamAsync("Demo.txt");
            using var streamReader = new StreamReader(stream);
            var content = await streamReader.ReadToEndAsync();

            return $"File info: size: {fileInfo.Length}, last modification: {fileInfo.LastModifiedUtc}.";
        }
    }
}
