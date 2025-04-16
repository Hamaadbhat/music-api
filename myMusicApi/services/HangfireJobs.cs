using Hangfire;

namespace myMusicApi.services
{
    public static class HangfireJobs
    {
        public static void RegisterRecurringJobs(IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var jobManager = scope.ServiceProvider.GetRequiredService<IRecurringJobManager>();

            jobManager.AddOrUpdate<IFileService>(
                "Delete file and records",
                service => service.DeleteFileAndRecords(),
                "*/2 * * * *" // Every 2 minutes
            );
        }
    }
}
