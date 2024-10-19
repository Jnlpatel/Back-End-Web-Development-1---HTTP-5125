using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace assignment2_jp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class J2Controller : ControllerBase
    {
        [HttpPost("ChiliPeppers")]
        public IActionResult CalculateSpiciness([FromForm] int[] ratings)
        {
            int totalStars = ratings.Sum();
            return Ok(totalStars);
        }

        [HttpPost("ShiftySum")]
        public IActionResult CalculateShiftySum([FromForm] int n, [FromForm] int k)
        {
            int total = 0;
            for (int i = 0; i <= k; i++)
            {
                total += n * (int)Math.Pow(10, i);
            }
            return Ok(total);
        }
    }
}
