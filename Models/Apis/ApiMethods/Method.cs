using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using ApiBankBackBone.Models.Apis.ApiMethods;
using Newtonsoft.Json;

namespace ApiBankBackBone.Models.Apis
{
	public class Method
	{
		[DisplayName("Идентификатор")]
		[JsonProperty("id")]
		[Required]
		public Guid Id { get; set; }

		[DisplayName("Идентификатор апи-родителя")]
		[JsonProperty("api_id")]
		[Required]
		public Guid ApiId { get; set; }

		[DisplayName("Http метод")]
		[JsonProperty("verb")]
		[Required]
		public MethodVerb Verb { get; set; } 
	}
}
