using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using sportServerDotnet.Data;

namespace sportServerDotnet.Controllers
{
    [ApiController]
    [Route("dashboard")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class DashboardController: ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
		private readonly TokenValidationParameters _tokenValidationParams;
		private readonly ApiDbContext _apiDbContext;


        public DashboardController( 
            UserManager<IdentityUser> userManager, 
            TokenValidationParameters tokenValidationParameters, 
            ApiDbContext apiDbContext)
		{
			_userManager = userManager;
			_tokenValidationParams = tokenValidationParameters;
			_apiDbContext = apiDbContext;
		}
        [HttpGet("home")]
        public void Home()
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
			Request.Headers.TryGetValue("Authorization", out var authHeader);
            var token = authHeader[0];
            var tokenInVerification = jwtTokenHandler.ValidateToken(token, _tokenValidationParams, out var validatedToken);
            
		}
        [HttpGet("search")]
        public void Search(string query, string genre)
        {

            Console.WriteLine($"{query}, and {genre}");
            
        }
    }
}