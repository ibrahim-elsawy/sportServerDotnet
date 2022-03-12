using System;
using System.ComponentModel.DataAnnotations.Schema;



namespace sportServerDotnet.Controllers.Models
{
    public class Person
    {
        public string personId {get; set;}
        public string Name { get; set; }
        public DateTime Birth { get; set; }
	}
}