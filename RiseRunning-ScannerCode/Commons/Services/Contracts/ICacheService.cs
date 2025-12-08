using RiseRunning_ScannerCode.Model.Entity;

namespace RiseRunning_ScannerCode.Commons.Services.Contracts
{
    public interface ICacheService
    {

        void UpdateCache(RunnerEntity runnerEntity);
        List<RunnerEntity?> GetRunnerEntitiesAsync();

    }
}
