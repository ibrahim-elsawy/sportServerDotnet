using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using sportServerDotnet.Controllers.Models.DTOs.Responses;
using System.IO;

namespace sportServerDotnet.Controllers
{ 
	[ApiController]
	[Route("image")]
	[AllowAnonymous]
	// [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] 
	public class ImageController : ControllerBase 
	{ 
		[HttpGet("{route}")]
		public IActionResult GetImage(string route)
		{
			try
			{
				var absoluteUrl = System.IO.Path.GetFullPath("./Controllers/Storage/");
				return PhysicalFile(absoluteUrl+route, "image/gif");
				// return PhysicalFile(absoluteUrl+route, "video/mp4");
			}
			catch (System.InvalidOperationException)
			{

				return BadRequest(new Error { msg = "Image is not found " });
			}
		}
    }
}