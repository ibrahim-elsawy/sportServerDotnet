using System;
using Microsoft.AspNetCore.Mvc;

namespace sportServerDotnet.Controllers
{
    [ApiController]
    [Route("dashboard")]
    public class DashboardController: ControllerBase
    {
        [HttpGet("search")]
        public void Search(string query, string genre)
        {
            Console.WriteLine($"{query}, and {genre}");
            
        }
    }
}