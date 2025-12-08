
using Microsoft.Extensions.Caching.Memory;
using RiseRunning_ScannerCode.Commons.Services.Contracts;
using RiseRunning_ScannerCode.Model.Entity;
using System.Security.Cryptography;
using System.Text;

namespace RiseRunning_ScannerCode.Commons.UseCase
{
    public class CacheService(IMemoryCache memoryCache, ILogger<CacheService> logger) : ICacheService
    {

        private readonly IMemoryCache _memoryCache = memoryCache;
        private const string CacheKey = "RunnersQueue";
        private ILogger<CacheService> _logger = logger;

        public  void UpdateCache(RunnerEntity runnerEntity)
        {
            if (!_memoryCache.TryGetValue(CacheKey, out List<RunnerEntity> cacheRunners))
                cacheRunners = new List<RunnerEntity>();

            cacheRunners!.Add(runnerEntity);

            _memoryCache.Set(CacheKey, cacheRunners, TimeSpan.FromMinutes(30));
        }

        public List<RunnerEntity?> GetRunnerEntitiesAsync()
        {
            try
            {

                if (_memoryCache.TryGetValue(CacheKey, out List<RunnerEntity?> runners) && runners is not null)
                {
                    return runners; 
                }

                return new List<RunnerEntity?>();
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, "[[Type]] Occured an exception  {@Input}",
                    nameof(CacheService),
                    new
                    {
                        Error = ex.Message,
                    });

                return new List<RunnerEntity?>();
            }
        }
    }
}
