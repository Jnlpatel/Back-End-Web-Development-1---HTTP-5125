using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace assignment2_jp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class J1Controller : ControllerBase
    {
        /// <summary>
        /// Calculates the final score for the Deliv-e-droid game.
        /// </summary>
        /// <param name="collisions">Number of collisions</param>
        /// <param name="deliveries">Number of deliveries</param>
        /// <returns>The final score</returns>
        [HttpPost("Delivedroid")]
        public IActionResult CalculateScore([FromForm] int collisions, [FromForm] int deliveries)
        {
            int score = deliveries * 50;
            score -= collisions * 10;

            if (deliveries > collisions)
            {
                score += 500;
            }

            return Ok(score);
        }

        [HttpPost("SpecialDay")]
        public IActionResult CheckSpecialDay([FromForm] int month, [FromForm] int day)
        {
            if (month == 2 && day == 18)
            {
                Console.WriteLine("ok");
                return Ok("Special Day");
            }
            else if (month < 2 || (month == 2 && day < 18))
            {
                return Ok("Before");
            }
            else
            {
                return Ok("After");
            }

        }

    }
}
