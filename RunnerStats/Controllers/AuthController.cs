using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RunnerStats.Helpers;
using RunnerStats.Models.Dtos;
using RunnerStats.Models.Entities;
using RunnerStats.Services.Interfaces;

namespace RunnerStats.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private IUserService _userService;
        private Utilities _utilities;
        private IRunnerService _runnerService;
        private INutritionService _nutritionService;

        public AuthController(IUserService userService,IRunnerService runnerService, Utilities utilities,INutritionService nutritionService)
        {
            _userService = userService;
            _runnerService = runnerService;
            _utilities = utilities;
            _nutritionService = nutritionService;
        }


        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(DtoRegister newRegister)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            DtoRunner dtoRunner = new()
            {
                Name = newRegister.Name,
                DateOfBirth = newRegister.DateOfBirth

            };

            int runnerId = await _runnerService.createRunner(dtoRunner);
            Console.WriteLine(runnerId);

            if (runnerId <= 0) 
            {

                return StatusCode(500, new { message = "Could not create runner." });
            
            }

            DtoNutrition dataNutrition = new()
            {
                LactoseIntolerant = false,
                AdditionalNotes = "",
                GoalMaintainWeight = false,
                GoalMuscleGain = false,
                GoalWeightLoss = false,
                IsCeliac = false,
                IsDiabetic = false,
                IsVegan = false
            };

            var response = await _nutritionService.NewDataNutrition(dataNutrition,runnerId);

            if (!response)
            {
                return StatusCode(500, new {message = "Could not create data nutrition" });
            }

            DtoUser dtoUser = new()
            {
                Email = newRegister.Email,
                PasswordHash = newRegister.PasswordHash,
                RunnerId = runnerId
            };

            bool result = await _userService.CreateNewUser(dtoUser);
            if (!result)
            {
                return Conflict(new { message = "Email already exists" });
            }
            else
            {
                return Created("", new {isSuccess = true});
            }
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(DtoLogin loginUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userService.AuthenticateUser(loginUser);
            if (user == null)
            {
                return Unauthorized(new { isSuccess = false, message = "Invalid credentials" });

            }
            return Ok(new { isSucces = true, idRunner = user.RunnerId,token = _utilities.GenerateJWT(user) });

        }



    }
}