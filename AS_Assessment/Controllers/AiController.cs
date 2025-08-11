using AS_Assessment.Services.Implementation;
using Microsoft.AspNetCore.Mvc;

namespace AS_Assessment.Controllers
{
    public class AiController : Controller
    {
        private readonly GroqAiService _groqService;

        public AiController(GroqAiService groqService)
        {
            _groqService = groqService;
        }

        [HttpPost]
        public async Task<IActionResult> EnhanceInventoryItem([FromForm] string name, [FromForm] int quantity, [FromForm] string stockStatus, [FromForm] string whatNeeded, [FromForm] string categoryName)
        {
            if (string.IsNullOrWhiteSpace(name) && string.IsNullOrWhiteSpace(whatNeeded))
                return Json(new { success = false, message = "Please enter at least the name or what is needed." });

            var aiResult = await _groqService.EnhanceInventoryItemAsync(name, quantity, stockStatus, whatNeeded, categoryName);
            return Json(new { success = true, content = aiResult });
        }

    }
}
