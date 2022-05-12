using System.Collections.Generic;

namespace sportServerDotnet.Controllers.Models.DTOs.Responses
{
    public class GetChallengeResponse
    {
        // public List<Challenge> Challenges {get; set;}
        public int Id { get; set; }
		public string Description { get; set; }
        // public byte[] FileData { get; set; }
        public string Url { get; set; }
		public string Admin_Id { get; set; }
	}
    public class GetChallengesResponse
    {
        public List<Challenge> Challenges {get; set;}
	}
}