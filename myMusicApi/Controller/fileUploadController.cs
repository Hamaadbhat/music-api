using CsvHelper.Configuration;
using CsvHelper;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using myMusicApi.model;
using Hangfire;
using myMusicApi.services;
using Aspose.Pdf.Text;
using Aspose.Pdf;

namespace myMusicApi.Controller
{
    [Route("api/[Controller]")]
    public class fileUploadController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IFileService _uploadService;
        public fileUploadController(AppDbContext dbcontext, IFileService upload)
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

        [HttpGet]

        public IActionResult GeneratePdf()
        {
            // 1. Fetch all records from the table
            var crops = _context.eu_Crop_Dicts.ToList();

            // 2. Create the PDF
            Document pdf = new Document();
            Page page = pdf.Pages.Add();

            TextFragment title = new TextFragment("Crop Dictionary Records");
            title.TextState.FontSize = 16;
            title.TextState.FontStyle = FontStyles.Bold;
            title.Margin.Bottom = 10;
            page.Paragraphs.Add(title);

            // 3. Add table structure
            Aspose.Pdf.Table table = new Aspose.Pdf.Table();
            table.Border = new BorderInfo(BorderSide.All, 0.5f);
            table.DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f);
            table.DefaultCellTextState = new TextState { FontSize = 10 };

            // 4. Add header row (assuming properties like ID, CropName, Description, etc.)
            var headerRow = table.Rows.Add();
            headerRow.Cells.Add("ID");
            headerRow.Cells.Add("Crop Name");
            headerRow.Cells.Add("Language");

            // 5. Add rows from database
            foreach (var crop in crops)
            {
                var row = table.Rows.Add();
                row.Cells.Add(crop.EU_CROP_CODE.ToString()); // Adjust based on actual column names
                row.Cells.Add(crop.EU_CROP_NAME);
                row.Cells.Add(crop.LANGUAGE);
                
            }

            page.Paragraphs.Add(table);

            // 6. Save to memory stream
            MemoryStream ms = new MemoryStream();
            pdf.Save(ms);
            ms.Position = 0;

            return File(ms, "application/pdf", "CropDictionary.pdf");
        }
    }
}


