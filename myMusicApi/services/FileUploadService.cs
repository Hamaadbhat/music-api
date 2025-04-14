using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using myMusicApi.model;

namespace myMusicApi.services
{
    [DisableConcurrentExecution(timeoutInSeconds: 3600)]
    public class FileUploadService:IFileUploadService
    {
        private readonly AppDbContext _context;

        public FileUploadService(AppDbContext context)
        {
            _context = context;
        }
        public async Task InsertCsvRecords(string filePath)
        {

            using (var reader = new StreamReader(filePath))
            {
                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = true,
                    MissingFieldFound = null,
                    HeaderValidated = null
                };
                using (var csv = new CsvReader(reader, config))
                {
                    var records = csv.GetRecords<eu_crop_dict>().ToList();
                

            foreach (var record in records)
            {
                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC InsertEuCrop @p0, @p1, @p2",
                    parameters: new object[] {
                record.EU_CROP_CODE,
                record.EU_CROP_NAME,
                record.LANGUAGE
                    });
            }
                }
            }

            // Optional: Delete the temp file after processing
             File.Delete(filePath);
        }
    }

        
    }

