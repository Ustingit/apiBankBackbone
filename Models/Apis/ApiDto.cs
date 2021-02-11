using System;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;

namespace ApiBankBackBone.Models.Apis
{
	public class ApiDto
	{
		[JsonProperty("id")]
		[DisplayName("Идентификатор")]
		public Guid Id { get; set; }

		[JsonProperty("tempId")]
		public Guid TempId { get; set; }

		[JsonProperty("name")]
		[DisplayName("Название")]
		public string Name { get; set; }

		[JsonProperty("description")]
		[DisplayName("Описание")]
		public string Description { get; set; }

		[JsonProperty("isFree")]
		[DisplayName("Апи является бесплатным")]
		public bool IsFree { get; set; }

		[JsonProperty("accessCost")]
		[DisplayName("Стоимость первоначального доступа")]
		public decimal AccessCost { get; set; }

		[JsonProperty("monthlyCost")]
		[DisplayName("Абонентская плата")]
		public decimal MonthlyCost { get; set; }

		[JsonProperty("additionalAccessRules")]
		[DisplayName("Дополнительные условия использования")]
		public string AdditionalAccessRules { get; set; }

		[JsonProperty("license")]
		[DisplayName("Лицензия")]
		public string License { get; set; }

		[JsonProperty("methods")]
		[DisplayName("Методы")]
		public IEnumerable<Method> Methods { get; set; }
	}
}
