using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using RiseRunning_ScannerCode.Model.Commons;
using RiseRunning_ScannerCode.Model.Entity;
using RiseRunning_ScannerCode.Model.Repository;
using System.Collections.Concurrent;
using System.Linq;
using System.Runtime.CompilerServices;

namespace RiseRunning_ScannerCode.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RunnersController(IMemoryCache memoryCache, AppDbContext appDbContext) : ControllerBase
    {
        private readonly AppDbContext _appDbContext = appDbContext;
        private readonly IMemoryCache _memoryCache = memoryCache;
        private const string CacheKey = "RunnersQueue";

        [HttpPost]
        public async Task<IActionResult> PostRunner([FromHeader] string nome, [FromHeader] string cpf)
        {
            if (!ValidateCPF.IsCpf(cpf)) 
                     Ok(MessageCommons.RunnerCpf);

            var cpfLong = ValidateCPF.cpfToLong(cpf);
            
            var existente = await _appDbContext
                .Runners
                .FirstOrDefaultAsync(r => r.Cpf == cpfLong);
            
            if (existente != null)
                return Ok(MessageCommons.RunnerJaRegistrado(existente.Nome));

            var runner = new RunnerEntity
            {
                Nome = nome,
                Cpf = ValidateCPF.cpfToLong(cpf)
            };

            appDbContext.AddRunner(runner);

            if (!_memoryCache.TryGetValue(CacheKey, out List<RunnerEntity> cacheRunners))
                cacheRunners = new List<RunnerEntity>();

            cacheRunners.Add(runner);

            _memoryCache.Set(CacheKey, cacheRunners, TimeSpan.FromMinutes(30));
            return Ok(MessageCommons.RunnerRegistrado(runner.Nome));
        }

        [HttpGet]
        public async Task<ActionResult<List<RunnerEntity>>> GetAllRunner() 
        {
            if (_memoryCache.TryGetValue(CacheKey, out List<RunnerEntity> runners))
            {
                return runners!
                    .OrderByDescending(p => p.DataHora)
                    .ToList();
            }

            return await _appDbContext.Runners
                .OrderByDescending(p => p.DataHora)
                .ToListAsync();
        }
    }
}
