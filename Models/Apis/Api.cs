using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiBankBackBone.Models.Apis
{
	public class Api
	{
		[DisplayName("Идентификатор")]
		[Required]
		public Guid Id { get; set; }
		
		[DisplayName("Название")]
		[Required]
		public string Name { get; set; }

		[DisplayName("Описание")]
		[Required]
		public string Description { get; set; }

		[DisplayName("Апи является бесплатным")]
		[Required]
		public bool IsFree { get; set; }

		//initially in dollars only
		[DisplayName("Стоимость первоначального доступа")]
		[Column(TypeName = "decimal(18,4)")]
		[Required]
		public decimal AccessCost { get; set; }

		//initially in dollars only
		[DisplayName("Абонентская плата")]
		[Column(TypeName = "decimal(18,4)")]
		[Required]
		public decimal MonthlyCost { get; set; }

		[DisplayName("Дополнительные условия использования")]
		[Required]
		public string AdditionalAccessRules { get; set; }

		[DisplayName("Лицензия")]
		[Required]
		public string License { get; set; }
	}
}
