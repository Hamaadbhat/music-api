using System.Globalization;
using System.Text.Json;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using myMusicApi.model;

namespace myMusicApi.services
{
    [DisableConcurrentExecution(timeoutInSeconds: 3600)]
    public class FileService:IFileService
    {
        private readonly AppDbContext _context;

        private readonly string _logFilePath = "file-log.json";

        private static readonly object _fileLogLock = new();



        public FileService(AppDbContext context)
        {
            _context = context;
        }


        private List<string> LoadFilePaths()
        {
            lock (_fileLogLock)
            {
                if (!File.Exists(_logFilePath))
                    return new List<string>();

                var json = File.ReadAllText(_logFilePath);
                return JsonSerializer.Deserialize<List<string>>(json) ?? new List<string>();
            }
        }

        private void SaveFilePaths(List<string> paths)
        {
            lock (_fileLogLock)
            {
                var json = JsonSerializer.Serialize(paths, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(_logFilePath, json);
            }
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

            var paths = LoadFilePaths();
            paths.Add(filePath);
            SaveFilePaths(paths);



        }
        [DisableConcurrentExecution(timeoutInSeconds: 3600)]
        public async Task DeleteFileAndRecords()
        {
            var paths = LoadFilePaths();
            foreach (var filePath in paths.ToList())
            {
                Console.WriteLine($"Processing file: {filePath}");
                if (File.Exists(filePath))
                {
                    using (var reader = new StreamReader(filePath))
                    {
                        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                        {
                            HasHeaderRecord = true,
                            MissingFieldFound = null,
                            HeaderValidated = null
                        };
                        using (var csv =new CsvReader(reader,config))
                        {
                            var records = csv.GetRecords<eu_crop_dict>().ToList();
                            foreach (var record in records)
                            {
                                await _context.Database.ExecuteSqlRawAsync(
                                    "delete Top(1) from eu_Crop_Dicts where EU_CROP_CODE = @p0 and EU_CROP_NAME = @p1 and LANGUAGE = @p2",
                                    parameters: new object[] {
                                        record.EU_CROP_CODE,
                                        record.EU_CROP_NAME,
                                        record.LANGUAGE
                                    });
                            }
                            
                        }

                    }
                    File.Delete(filePath);
                    paths.Remove(filePath);
                }
            }
            SaveFilePaths(paths);


        }
    }

        
    }

