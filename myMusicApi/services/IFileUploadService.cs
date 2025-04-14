namespace myMusicApi.services
{
    public interface IFileUploadService
    {
        public Task InsertCsvRecords(string filePath);
        
    }

}
