using CsvHelper.Configuration;
using CsvHelper;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using myMusicApi.model;
using Hangfire;
using myMusicApi.services;

namespace myMusicApi.Controller
{
    [Route("api/[Controller]")]
    public class fileUploadController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IFileUploadService _uploadService;
        public fileUploadController(AppDbContext dbcontext, IFileUploadService upload)
        {
            _context = dbcontext;
            _uploadService = upload;
        }

        [HttpPost]
        public async Task<IActionResult> UploadCsv(List<IFormFile> files)
        {
            if (files == null || files.Count == 0)
                return BadRequest("No Files Uploaded");
            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    var filePath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + Path.GetExtension(file.FileName));

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    BackgroundJob.Enqueue(() => _uploadService.InsertCsvRecords(filePath));


                }

            }
            return Ok(new { message = $"{files.Count} files uploaded sucessfully processing will start shortly " });
        }
    }
}

