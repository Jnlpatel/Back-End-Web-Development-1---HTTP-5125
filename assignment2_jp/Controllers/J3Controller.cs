using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace assignment2_jp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class J3Controller : ControllerBase
    {
        [HttpPost("DoubleDice")]
        public IActionResult CalculateDiceGame([FromForm] int[] player1, [FromForm] int[] player2)
        {
            int player1Score = 0;
            int player2Score = 0;

            for (int i = 0; i < player1.Length; i++)
            {
                if (player1[i] > player2[i])
                {
                    player1Score++;
                }
                else if (player2[i] > player1[i])
                {
                    player2Score++;
                }
            }

            return Ok(new { Player1 = player1Score, Player2 = player2Score });
        }

    }
}
