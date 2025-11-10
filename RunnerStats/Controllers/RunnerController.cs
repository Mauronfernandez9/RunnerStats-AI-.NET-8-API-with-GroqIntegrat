using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RunnerStats.Models.Dtos;
using RunnerStats.Services.Interfaces;

namespace RunnerStats.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class RunnerController : ControllerBase
    {

        private readonly IRunnerService _runnerService;
        public RunnerController(IRunnerService runner)
        {
            _runnerService = runner;

        }


        [HttpGet]
        
        public async Task<IActionResult> GetRunner()
        {

            var runnerIdClaim = User.FindFirst("RunnerId");

            if (runnerIdClaim == null) {
                return Unauthorized(new { message = "Runner not found in token"});
            }
            var runner = await _runnerService.GetRunner(int.Parse(runnerIdClaim.Value));
            if (runner == null) {

                return NotFound(new { message = "Runner not found" });
            }
            DtoRunner dtoRunner = new()
            {
                DateOfBirth = runner.DateOfBirth,
                Name = runner.Name,
                Experience = runner.Experience, 
                Height = runner.Height,
                TotalRaces = runner.TotalRaces,
                Weight = runner.Weight
            };

            return Ok(dtoRunner);
            
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRunner([FromBody]DtoRunner runnerToUpdate)

        {
            var idRunnerClaim = User.FindFirst("RunnerId");
            if (idRunnerClaim == null)
            {
                return BadRequest(new { message = "Runner ID is null" });
            }


            if (int.Parse(idRunnerClaim.Value) <= 0)
            {

                return BadRequest(new {message="Invalid runner ID."});
            }
            if (runnerToUpdate == null)
            {
                return BadRequest(new { message = "Runner data is required." });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _runnerService.UpdateRunner(runnerToUpdate, int.Parse(idRunnerClaim.Value));
            if (!response)
            {
                return NotFound(new {message = "Runner not found or update failed."});
            } 
            return Ok("Runner updated successfully.");


        }





    }
}
