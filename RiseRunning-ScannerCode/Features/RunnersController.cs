using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RiseRunning_ScannerCode.Commons;
using RiseRunning_ScannerCode.Commons.Repository;
using RiseRunning_ScannerCode.Commons.Services.Contracts;
using RiseRunning_ScannerCode.Model.Entity;

namespace RiseRunning_ScannerCode.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RunnersController(ICacheService cacheService, AppDbContext appDbContext) : ControllerBase
    {
        private readonly AppDbContext _appDbContext = appDbContext;
        private readonly ICacheService _cacheService = cacheService;

        [HttpPost]
        public async Task<IActionResult> PostRunner([FromHeader] string nome, [FromHeader] string email)
        {
            if (!ValidateEmail.IsValidEmail(email)) 
                     Ok(MessageCommons.RunnerCpf);

            
            var existente = await _appDbContext
                .Runners
                .FirstOrDefaultAsync(r => r.Email == email);
            
            if (existente != null)
                return Ok(MessageCommons.RunnerJaRegistrado(existente.Nome));

            var runner = new RunnerEntity
            {
                Nome = nome,
                Email = email,
            };

            appDbContext.AddRunner(runner);
            _cacheService.UpdateCache(runner);

            return Ok(MessageCommons.RunnerRegistrado(runner.Nome));
        }

        [HttpGet]
        public async Task<ActionResult<List<RunnerEntity>>> GetAllRunner() 
        {

            var runners =  _cacheService.GetRunnerEntitiesAsync();

            if (runners is not null && runners.Count() > 0)
            {
                return  runners!
                    .OrderByDescending(p => p.DataHora)
                    .ToList()!; 
            }

            return await _appDbContext.Runners
                .OrderByDescending(p => p.DataHora)
                .ToListAsync();
        }

    }
}
