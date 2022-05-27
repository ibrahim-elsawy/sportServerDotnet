using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using sportServerDotnet.Data;
using System.Linq;
using sportServerDotnet.Controllers.Models.DTOs.Responses;
using sportServerDotnet.Controllers.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using sportServerDotnet.Controllers.Models.DTOs.Requests;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Collections.Generic;

namespace sportServerDotnet.Controllers
{
	[ApiController]
	[Route("challenge")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	public class ChallengeController : ControllerBase
	{
		private readonly UserManager<IdentityUser> _userManager;
		private readonly TokenValidationParameters _tokenValidationParams;
		private readonly ApiDbContext _apiDbContext;


		public ChallengeController(
		    UserManager<IdentityUser> userManager,
		    TokenValidationParameters tokenValidationParameters,
		    ApiDbContext apiDbContext)
		{
			_userManager = userManager;
			_tokenValidationParams = tokenValidationParameters;
			_apiDbContext = apiDbContext;
		}
		[HttpPost("all")]
		public async Task<IActionResult> GetChallenges([FromBody] RangeRequest rangeRequest)
		{
			// var jwtTokenHandler = new JwtSecurityTokenHandler();
			// Request.Headers.TryGetValue("Authorization", out var authHeader);
			// var token = authHeader[0].Replace("Bearer ", "");
			// var tokenInVerification =  jwtTokenHandler.ValidateToken(token, _tokenValidationParams, out var validatedToken);
			// var user_id = tokenInVerification.Claims.ElementAt(0).Value;
			try{
				var challengesList =  _apiDbContext.Challenge
				.OrderBy(c => c.Id)
				.Skip(rangeRequest.skip)
				.Take(rangeRequest.take);
				return Ok(new GetChallengesResponse { Challenges = await challengesList.ToListAsync() });
			}
			catch (Exception){
				return BadRequest(new Error { msg = " mathzrsh ya metnak " });
			}

		}
		[HttpGet("count")]
		public async Task<IActionResult> GetNumChallenge()
		{
			try
			{
				var count = await _apiDbContext.Challenge.CountAsync();
				return Ok(new CountResponse {num=count});
			}
			catch (Exception )
			{
				 // TODO
				return BadRequest(new Error {msg="bad request"});
			}
		}
		[HttpGet("{id}")]
		public async Task<IActionResult> GetChallengeWithId(int id)
		{
			// var jwtTokenHandler = new JwtSecurityTokenHandler();
			// Request.Headers.TryGetValue("Authorization", out var authHeader);
			// var token = authHeader[0].Replace("Bearer ", "");
			// var tokenInVerification =  jwtTokenHandler.ValidateToken(token, _tokenValidationParams, out var validatedToken);
			// var user_id = tokenInVerification.Claims.ElementAt(0).Value;
			// var challengesQuery =  _apiDbContext.Challenge.Where(c => c.Id == id).OrderBy(c => c.Id);
			// var challengesList = await challengesQuery.ToListAsync();
			try
			{
				var challenge = await _apiDbContext.Challenge.Where(c => c.Id == id).OrderBy(c => c.Id).FirstAsync();
				// var ArrBytes = System.IO.File.ReadAllBytes("Controllers/Storage/"+challenge.Url);
				return Ok(new GetChallengeResponse 
				{ 
					Id=id, 
					Url=challenge.Url,
					Admin_Id = challenge.Admin_Id, 
					Description = challenge.Description
				});
			}
			catch (System.InvalidOperationException)
			{

				return BadRequest(new Error { msg = "challenge is not found " });
			}
		}

		[HttpPost]
		// public async Task<IActionResult> CreateChallenge( CreateChallengeRequest _createChallengeReq)
		public async Task<IActionResult> CreateChallenge( IFormCollection data)
		{
			string desc = null;
			foreach(var k in data)
			{
				if(k.Key == "description")
				{
					desc = k.Value[0];
					break;
				}
				System.Console.WriteLine(k);
			}
			var file = data.Files[0];
			// var dir = Path.GetFullPath("./Storage/"); 
			var filename = RandomString(24) + Path.GetExtension(file.FileName); 
			var fullFileName = Path.Join("Controllers/Storage/", filename); 
			if (data.Files.Count != 0)
			{ 
				file = data.Files[0]; 
				using (var stream = System.IO.File.Create(fullFileName)) 
				// using (Stream fileStream = new FileStream(fullFileName, FileMode.Create)) 
				{ 
					// await file.CopyToAsync(fileStream); 
					await file.CopyToAsync(stream); 
				} 
			}
			var jwtTokenHandler = new JwtSecurityTokenHandler();
			Request.Headers.TryGetValue("Authorization", out var authHeader);
			var token = authHeader[0].Replace("Bearer ", "");
			var tokenInVerification =  jwtTokenHandler.ValidateToken(token, _tokenValidationParams, out var validatedToken);
			var user_id = tokenInVerification.Claims.ElementAt(0).Value;
			var ret = await _apiDbContext.Challenge.AddAsync(new Challenge { Description = desc, Url = filename, Admin_Id = user_id});
			await _apiDbContext.SaveChangesAsync();
			return Ok();

		}

		[HttpPost("{id}")]
		public async Task<IActionResult> UpdateChallenge( int id, IFormCollection data)
		{
			try
			{ 
				var challenge =  await _apiDbContext.Challenge.Where(c => c.Id == id).OrderBy(c => c.Id).FirstAsync();
				var jwtTokenHandler = new JwtSecurityTokenHandler();
				Request.Headers.TryGetValue("Authorization", out var authHeader);
				var token = authHeader[0].Replace("Bearer ", "");
				var tokenInVerification =  jwtTokenHandler.ValidateToken(token, _tokenValidationParams, out var validatedToken);
				var user_id = tokenInVerification.Claims.ElementAt(0).Value;
				if (user_id != challenge.Admin_Id)
				{
					return Unauthorized(new Error { msg="Unauthorized request "});
				}
				string desc = null;
				IFormFile file = null;
				foreach(var k in data)
				{
					if(k.Key == "description")
					{
						desc = k.Value[0];
						challenge.Description = desc;
						break;
					}
				}
				if (data.Files.Count != 0)
				{ 
					file = data.Files[0]; 
					var filename = RandomString(24) + Path.GetExtension(file.FileName); 
					var fullFileName = Path.Join("Controllers/Storage/", filename); 
					using (var stream = System.IO.File.Create(fullFileName)) 
					// using (Stream fileStream = new FileStream(fullFileName, FileMode.Create)) 
					{ 
						// await file.CopyToAsync(fileStream); 
						await file.CopyToAsync(stream); 
					}
					challenge.Url = filename;
				}
				// var ret = await _apiDbContext.Challenge.AddAsync(new Challenge { Description = desc, Url = "djfk", Admin_Id = user_id});
				await _apiDbContext.SaveChangesAsync();
				return Ok();

			}
			catch (System.InvalidOperationException )
			{

				return BadRequest(new Error { msg="challenge is not found "}); 
			}

		}

		[HttpPost("join/{challenge_id}")]
		public async Task<IActionResult> AddUser( int challenge_id)
		{
			try
			{ 
				// var challenge =  await _apiDbContext.Challenge.Where(c => c.Id == id).OrderBy(c => c.Id).FirstAsync();
				var jwtTokenHandler = new JwtSecurityTokenHandler();
				Request.Headers.TryGetValue("Authorization", out var authHeader);
				var token = authHeader[0].Replace("Bearer ", "");
				var tokenInVerification =  jwtTokenHandler.ValidateToken(token, _tokenValidationParams, out var validatedToken);
				var user_id = tokenInVerification.Claims.ElementAt(0).Value;
				// var user = await _userManager.GetUserAsync();
				// var ret = await _apiDbContext.Challenge.AddAsync(new Challenge { Description = desc, Url = "djfk", Admin_Id = user_id});
				// var user = await _userManager.FindByIdAsync(user_id);
				var challenge = await _apiDbContext.Challenge.Where(c => c.Id == challenge_id).FirstAsync();
				if (challenge.Users_Id == null)
				{
					challenge.Users_Id = new List<string>(); 
				}
				challenge.Users_Id.Add(user_id);
				await _apiDbContext.SaveChangesAsync();
				return Ok();

			}
			catch (System.InvalidOperationException )
			{

				return BadRequest(new Error { msg="challenge is not found "}); 
			}

		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteChallege( int id)
		{
			try
			{ 	
				var jwtTokenHandler = new JwtSecurityTokenHandler();
				Request.Headers.TryGetValue("Authorization", out var authHeader);
				var token = authHeader[0].Replace("Bearer ", "");
				var tokenInVerification =  jwtTokenHandler.ValidateToken(token, _tokenValidationParams, out var validatedToken);
				var user_id = tokenInVerification.Claims.ElementAt(0).Value;
				var challenge = await _apiDbContext.Challenge.Where(c => c.Id == id).FirstAsync();
				if(challenge.Admin_Id != user_id)
				{
					return BadRequest();
				}
				_apiDbContext.Challenge.Remove(challenge);
				await _apiDbContext.SaveChangesAsync();
				return Ok();

			}
			catch (System.InvalidOperationException )
			{

				return BadRequest(new Error { msg="challenge is not found "}); 
			}

		}
		private string RandomString(int length)
		{
			var random = new Random();
			var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789@$-_=";
			return new string(Enumerable.Repeat(chars, length)
				.Select(x => x[random.Next(x.Length)]).ToArray());
		}



	}
}