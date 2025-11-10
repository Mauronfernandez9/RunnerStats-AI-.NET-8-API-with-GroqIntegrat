using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using RunnerStats.Models.Dtos;
using RunnerStats.Services.Implementations;
using RunnerStats.Services.Interfaces;

namespace RunnerStats.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NutritionController : ControllerBase
    {
        private readonly INutritionService _nutritionService;
        private IGroqApiClient _groqApiClient; 
        public NutritionController(INutritionService nutritionService,IGroqApiClient groqApiClient) 
        { 
            _nutritionService = nutritionService;
            _groqApiClient = groqApiClient;
        
        }

        [HttpGet]
        public async Task<IActionResult> GetDataNutrition()
        {
            var idRunner = (User.FindFirst("RunnerId")!.Value);


            if (idRunner == null)
            {
                return Unauthorized(new {message = "Runner not found in token" });
            }



            DtoNutrition dataNutrition = await _nutritionService.GetDataNutrition(int.Parse(idRunner));

            if (dataNutrition == null) 
            {
                return NotFound();
            }

            return Ok(dataNutrition);

            

        }

        [HttpPut]
        public async Task<IActionResult> UpdateDataNutrition([FromBody] DtoNutrition dtoNutrition)
        {
            var idRunner = User.FindFirst("RunnerId");
            if (idRunner == null) 
            {
                return BadRequest(new { message = "Runner ID is null" });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool response = await _nutritionService.UpdateDataNutrition(dtoNutrition, int.Parse(idRunner.Value));
            if (!response) 
            {
                return NotFound(new { message = "Data Nutrtion of runner not found or update failed." });
            }

            return Ok("Data nutrition of runner updated successfully.");
        }


        [HttpPost]
        [Route("NutritionistAI")]
        public async Task<IActionResult> TalkWithNutritionistAI([FromBody] DtoNutritionChatIa message)
        {
            var idUser = User.FindFirst("RunnerId");
            if (idUser == null)
            {
                return BadRequest(new { message = "Runner ID is null" });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }



            var infoUser = await _nutritionService.GetDataNutrition(int.Parse(idUser.Value));

            if (infoUser == null)
            {
                return NotFound(new { message = "Data Nutrtion of runner not found or there was a problem." });
            }

            string informationUser = $"The user...IsVegan: {(infoUser.IsVegan ? "Yes" : "No")},IsCeliac: {(infoUser.IsCeliac ? "Yes" : "No")},IsDiabetic: {(infoUser.IsDiabetic ? "Yes" : "No")},LactoseIntolerant: {(infoUser.LactoseIntolerant ? "Yes" : "No")},GoalWeightLoss: {(infoUser.GoalWeightLoss ? "Yes" : "No")},GoalMuscleGain: {(infoUser.GoalMuscleGain ? "Yes" : "No")},GoalMaintainWeight: {(infoUser.GoalMaintainWeight ? "Yes" : "No")}, AdditionalNotes: {infoUser.AdditionalNotes}";



            var request = new JObject
            {
                ["model"] = "llama-3.3-70b-versatile",
                ["messages"] = new JArray
                {
                    new JObject
                    {
                        ["role"] = "system",
                        ["content"] = "You are a professional nutrionist. Polyglot, you have to adapt to context of the user that is talking with you. You need the information of user: "+informationUser

                    },
                    new JObject
                    {
                        ["role"] = "user",
                        ["content"] = $"{message.message}"
                    }
                }
                

            };

            Console.WriteLine(request);


            var response = await _groqApiClient.GetResponse(request);
            Console.WriteLine(response);
            if (response?["choices"]?[0]?["message"]?["content"] == null)
            {
                return NotFound(new { message = "No response from AI." });
            }

            string responseBot = response["choices"][0]["message"]["content"]!.ToString();

            return Ok(responseBot);

            
        }




    }
}
