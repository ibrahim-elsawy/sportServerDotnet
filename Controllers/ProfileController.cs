using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using sportServerDotnet.Controllers.Models.DTOs.Responses;
using sportServerDotnet.Data;

namespace sportServerDotnet.Controllers
{
    [ApiController]
	[Route("profile")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	public class ProfileController : ControllerBase
	{
		private readonly UserManager<IdentityUser> _userManager;
		private readonly TokenValidationParameters _tokenValidationParams;
		private readonly ApiDbContext _apiDbContext;


		public ProfileController( 
            UserManager<IdentityUser> userManager, 
            TokenValidationParameters tokenValidationParameters, 
            ApiDbContext apiDbContext)
		{
			_userManager = userManager;
			_tokenValidationParams = tokenValidationParameters;
			_apiDbContext = apiDbContext;
		}
		
		[HttpGet("username/{user_id}")]
		public async Task<IActionResult> GetUsername([FromRoute] string user_id)
		{
			try
			{
				var user = await _userManager.FindByIdAsync(user_id);
				return Ok(user.UserName);
			}
			catch (System.Exception ex)
			{
				 // TODO
				return BadRequest(new Error {msg="User is not found "});
			}
		}
        [HttpGet("{user_id}")]
        public async Task<IActionResult> GetProfile([FromRoute] string user_id)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
			Request.Headers.TryGetValue("Authorization", out var authHeader);
			var token = authHeader[0].Replace("Bearer ", "");
			var tokenInVerification = jwtTokenHandler.ValidateToken(token, _tokenValidationParams, out var validatedToken);
			var init_user_id = tokenInVerification.Claims.ElementAt(0).Value;
			try
			{
                var profileFinal = await _apiDbContext.Profile.Where(p => p.UserId == user_id).OrderBy(c => c.Id).FirstAsync();
                if (profileFinal.Followers == null)
                {
					profileFinal.Followers = new List<string>();
				}
				profileFinal.Followers.Add(init_user_id);
                var profileInit = await _apiDbContext.Profile.Where(p => p.UserId == init_user_id).OrderBy(c => c.Id).FirstAsync();
                if (profileInit.Following == null)
                {
					profileInit.Following = new List<string>();
				}
				profileInit.Following.Add(user_id);
				await _apiDbContext.SaveChangesAsync();
				return Ok();
			}
			catch (System.InvalidOperationException)
			{

				return BadRequest(new Error { msg = "Post is not found " });
			}
        }

        [HttpPost("{id}")]
		public async Task<IActionResult> UpdateProfile([FromRoute] int id, [FromForm] IFormCollection data)
		{
			var jwtTokenHandler = new JwtSecurityTokenHandler();
			Request.Headers.TryGetValue("Authorization", out var authHeader);
			var token = authHeader[0].Replace("Bearer ", "");
			var tokenInVerification = jwtTokenHandler.ValidateToken(token, _tokenValidationParams, out var validatedToken);
			var user_id = tokenInVerification.Claims.ElementAt(0).Value;
			try
			{
				// if (supp.Post_Id != null)
				// {
				// 	var post = await _apiDbContext.Posts.Where(p => p.Id == supp.Post_Id).OrderBy(c => c.Id).FirstAsync();
				// 	if (post.Supports == null)
				// 	{
				// 		post.Supports = new List<Support>();
				// 	}
				// 	post.Supports.Add(new Support
				// 	{
				// 		// PostId = post.Id,
				// 		UserId = user_id
				// 	});
				// }
				string bio = null;
				foreach (var k in data)
				{
					if (k.Key == "bio")
					{
						bio = k.Value[0];
					}
				}
				var file = data.Files[0];
				var filename = RandomString(24) + Path.GetExtension(file.FileName);
				var fullFileName = Path.Join("Controllers/Storage/", filename);
				if (data.Files.Count != 0)
				{
					file = data.Files[0];
					using (var stream = System.IO.File.Create(fullFileName))
					{
						await file.CopyToAsync(stream);
					}
				}
                var profile = await _apiDbContext.Profile.Where(p => p.Id == id).OrderBy(c => c.Id).FirstAsync();
                if (profile.Image != filename)
                {
					profile.Image = filename;
				}
                if (profile.Bio != bio)
                {
					profile.Bio = bio;
				}
				await _apiDbContext.SaveChangesAsync();
				return Ok();
			}
			catch (System.InvalidOperationException)
			{

				return BadRequest(new Error { msg = "Post is not found " });
			}

		}

        // [HttpPost("{id}")]
		// public async Task<IActionResult> UpdateProfile([FromRoute] int id, [FromForm] IFormCollection data)
		// {
		// 	var jwtTokenHandler = new JwtSecurityTokenHandler();
		// 	Request.Headers.TryGetValue("Authorization", out var authHeader);
		// 	var token = authHeader[0].Replace("Bearer ", "");
		// 	var tokenInVerification = jwtTokenHandler.ValidateToken(token, _tokenValidationParams, out var validatedToken);
		// 	var user_id = tokenInVerification.Claims.ElementAt(0).Value;
		// 	try
		// 	{
		// 		// if (supp.Post_Id != null)
		// 		// {
		// 		// 	var post = await _apiDbContext.Posts.Where(p => p.Id == supp.Post_Id).OrderBy(c => c.Id).FirstAsync();
		// 		// 	if (post.Supports == null)
		// 		// 	{
		// 		// 		post.Supports = new List<Support>();
		// 		// 	}
		// 		// 	post.Supports.Add(new Support
		// 		// 	{
		// 		// 		// PostId = post.Id,
		// 		// 		UserId = user_id
		// 		// 	});
		// 		// }
		// 		string bio = null;
		// 		foreach (var k in data)
		// 		{
		// 			if (k.Key == "bio")
		// 			{
		// 				bio = k.Value[0];
		// 			}
		// 		}
		// 		var file = data.Files[0];
		// 		var filename = RandomString(24) + Path.GetExtension(file.FileName);
		// 		var fullFileName = Path.Join("Controllers/Storage/", filename);
		// 		if (data.Files.Count != 0)
		// 		{
		// 			file = data.Files[0];
		// 			using (var stream = System.IO.File.Create(fullFileName))
		// 			{
		// 				await file.CopyToAsync(stream);
		// 			}
		// 		}
        //         var profile = await _apiDbContext.Profile.Where(p => p.Id == id).OrderBy(c => c.Id).FirstAsync();
        //         if (profile.Image != filename)
        //         {
		// 			profile.Image = filename;
		// 		}
        //         if (profile.Bio != bio)
        //         {
		// 			profile.Bio = bio;
		// 		}
		// 		await _apiDbContext.SaveChangesAsync();
		// 		return Ok();
		// 	}
		// 	catch (System.InvalidOperationException)
		// 	{

		// 		return BadRequest(new Error { msg = "Post is not found " });
		// 	}

		// }

		private string RandomString(int length)
		{
			var random = new Random();
			var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789@$-_=";
			return new string(Enumerable.Repeat(chars, length)
				.Select(x => x[random.Next(x.Length)]).ToArray());
		}
	}
}