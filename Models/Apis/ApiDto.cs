using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ApiBankBackBone.Models.Apis
{
	public class ApiDto
	{
		[JsonProperty("id")]
		public Guid Id { get; set; }

		[JsonProperty("tempId")]
		public Guid TempId { get; set; }

		[JsonProperty("description")]
		public string Description { get; set; }

		[JsonProperty("isFree")]
		public bool IsFree { get; set; }

		[JsonProperty("accessCost")]
		public decimal AccessCost { get; set; }

		[JsonProperty("monthlyCost")]
		public decimal MonthlyCost { get; set; }

		[JsonProperty("additionalAccessRules")]
		public string AdditionalAccessRules { get; set; }

		[JsonProperty("license")]
		public string License { get; set; }

		[JsonProperty("methods")]
		public IEnumerable<Method> Methods { get; set; }
	}
}
