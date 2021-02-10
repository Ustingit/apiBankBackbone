using System;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;

namespace ApiBankBackBone.Models.Apis
{
	public class ApiDto
	{
		[DisplayName("Идентификатор")]
		public Guid Id { get; set; }

		public Guid TempId { get; set; }

		[DisplayName("Название")]
		public string Name { get; set; }

		[DisplayName("Описание")]
		public string Description { get; set; }

		[DisplayName("Апи является бесплатным")]
		public bool IsFree { get; set; }

		[DisplayName("Стоимость первоначального доступа")]
		public decimal AccessCost { get; set; }

		[DisplayName("Абонентская плата")]
		public decimal MonthlyCost { get; set; }

		[DisplayName("Дополнительные условия использования")]
		public string AdditionalAccessRules { get; set; }

		[DisplayName("Лицензия")]
		public string License { get; set; }

		[DisplayName("Методы")]
		public IEnumerable<Method> Methods { get; set; }
	}
}
