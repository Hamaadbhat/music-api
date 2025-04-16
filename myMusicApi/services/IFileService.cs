namespace myMusicApi.services
{
    public interface IFileService
    {
        public Task InsertCsvRecords(string filePath);
        public  Task DeleteFileAndRecords();


    }

}
