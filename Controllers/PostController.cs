using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using sportServerDotnet.Controllers.Models;
using sportServerDotnet.Controllers.Models.DTOs.Requests;
using sportServerDotnet.Controllers.Models.DTOs.Responses;
using sportServerDotnet.Data;

namespace sportServerDotnet.Controllers
{
	[ApiController]
	[Route("post")]
	public class PostController : ControllerBase
	{
		private readonly UserManager<IdentityUser> _userManager;
		private readonly TokenValidationParameters _tokenValidationParams;
		private readonly ApiDbContext _apiDbContext;


		public PostController(
	    UserManager<IdentityUser> userManager,
	    TokenValidationParameters tokenValidationParameters,
	    ApiDbContext apiDbContext)
		{
			_userManager = userManager;
			_tokenValidationParams = tokenValidationParameters;
			_apiDbContext = apiDbContext;
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
				var post = await _apiDbContext.Posts.Where(p => p.Id == id).OrderBy(c => c.Id).FirstAsync();
				return Ok(post);
			}
			catch (System.InvalidOperationException)
			{

				return BadRequest(new Error { msg = "Post is not found " });
			}

		}

		[HttpPost()]
		public async Task<IActionResult> CreatePost(IFormCollection data)
		{
			try
			{
				string text = null;
				int challenge_id = 0;
				foreach (var k in data)
				{
					if (k.Key == "text")
					{
						text = k.Value[0];
					}
					if (k.Key == "challenge_id")
					{
						challenge_id = Int32.Parse(k.Value[0]);
					}
					System.Console.WriteLine(k);
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
				var jwtTokenHandler = new JwtSecurityTokenHandler();
				Request.Headers.TryGetValue("Authorization", out var authHeader);
				var token = authHeader[0].Replace("Bearer ", "");
				var tokenInVerification = jwtTokenHandler.ValidateToken(token, _tokenValidationParams, out var validatedToken);
				var user_id = tokenInVerification.Claims.ElementAt(0).Value;

				var challenge = await _apiDbContext.Challenge.Where(c => c.Id == challenge_id).FirstAsync();

				if (challenge.Posts == null)
				{
					challenge.Posts = new List<Post>();
				}
				var post = new Post
				{
					UserId = user_id,
					Text = text,
					Image = filename,
					AddedDate = DateTime.Now
				};

				foreach (string s in challenge.Users_Id)
				{
					var dashboard = await _apiDbContext.Dashboards.Where(d => d.UserId == s).FirstAsync();
					if (dashboard.Posts == null)
					{
						dashboard.Posts = new List<Post>();
					}
					dashboard.Posts.Add(post);

				}

				challenge.Posts.Add(post);
				// var ret = await _apiDbContext.Posts.AddAsync(new Post
				// {
				// 	UserId = user_id,
				// 	Text = text,
				// 	Image = filename,
				// 	AddedDate = DateTime.Now
				// });
				await _apiDbContext.SaveChangesAsync();
				return Ok();
			}
			catch (System.InvalidOperationException)
			{
				return BadRequest(new Error { msg = "Post is not found " });
			}

		}

		[HttpPost("comment")]
		public async Task<IActionResult> Comment([FromBody] CommentRequest com)
		{
			var jwtTokenHandler = new JwtSecurityTokenHandler();
			Request.Headers.TryGetValue("Authorization", out var authHeader);
			var token = authHeader[0].Replace("Bearer ", "");
			var tokenInVerification = jwtTokenHandler.ValidateToken(token, _tokenValidationParams, out var validatedToken);
			var user_id = tokenInVerification.Claims.ElementAt(0).Value;
			try
			{
				var post = await _apiDbContext.Posts.Where(p => p.Id == com.Post_Id).OrderBy(c => c.Id).FirstAsync();
				if (post.Comments == null)
				{
					post.Comments = new List<Comment>();
				}
				post.Comments.Add(new Comment
				{
					Text = com.Text,
					PostId = post.Id,
					UserId = user_id
				});
				await _apiDbContext.SaveChangesAsync();
				return Ok();
			}
			catch (System.InvalidOperationException)
			{

				return BadRequest(new Error { msg = "Post is not found " });
			}

		}

		[HttpPost("reply")]
		public async Task<IActionResult> Reply([FromBody] ReplyRequest rep)
		{
			var jwtTokenHandler = new JwtSecurityTokenHandler();
			Request.Headers.TryGetValue("Authorization", out var authHeader);
			var token = authHeader[0].Replace("Bearer ", "");
			var tokenInVerification = jwtTokenHandler.ValidateToken(token, _tokenValidationParams, out var validatedToken);
			var user_id = tokenInVerification.Claims.ElementAt(0).Value;
			try
			{
				var comment = await _apiDbContext.Comments.Where(p => p.Id == rep.Comment_Id).OrderBy(c => c.Id).FirstAsync();
				if (comment.Replies == null)
				{
					comment.Replies = new List<Replies>();
				}
				comment.Replies.Add(new Replies
				{
					Text = rep.Text,
					CommentId = comment.Id,
					UserId = user_id
				});
				await _apiDbContext.SaveChangesAsync();
				return Ok();
			}
			catch (System.InvalidOperationException)
			{

				return BadRequest(new Error { msg = "Post is not found " });
			}

		}

		[HttpPost("support")]
		public async Task<IActionResult> Support([FromBody] SupportRequest supp)
		{
			var jwtTokenHandler = new JwtSecurityTokenHandler();
			Request.Headers.TryGetValue("Authorization", out var authHeader);
			var token = authHeader[0].Replace("Bearer ", "");
			var tokenInVerification = jwtTokenHandler.ValidateToken(token, _tokenValidationParams, out var validatedToken);
			var user_id = tokenInVerification.Claims.ElementAt(0).Value;
			try
			{
				if (supp.Post_Id != null)
				{
					var post = await _apiDbContext.Posts.Where(p => p.Id == supp.Post_Id).OrderBy(c => c.Id).FirstAsync();
					if (post.Supports == null)
					{
						post.Supports = new List<Support>();
					}
					post.Supports.Add(new Support
					{
						// PostId = post.Id,
						UserId = user_id
					});
				}
				else if (supp.Comment_Id != null)
				{
					var comment = await _apiDbContext.Comments.Where(c => c.Id == supp.Comment_Id).OrderBy(c => c.Id).FirstAsync();
					if (comment.Supports== null)
					{
						comment.Supports = new List<Support>();
					}
					comment.Supports.Add(new Support
					{
						// CommentId = comment.Id,
						UserId = user_id
					});

				}
				else
				{
					var reply = await _apiDbContext.Replies.Where(r => r.Id == supp.Reply_Id).OrderBy(r => r.Id).FirstAsync();
					if (reply.Supports != null)
					{
						reply.Supports = new List<Support>();
					}
					reply.Supports.Add(new Support
					{
						// ReplyId = reply.Id,
						UserId = user_id
					});
				}
				await _apiDbContext.SaveChangesAsync();
				return Ok();
			}
			catch (System.InvalidOperationException)
			{

				return BadRequest(new Error { msg = "Post is not found " });
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