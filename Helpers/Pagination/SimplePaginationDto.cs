using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ApiBankBackBone.Helpers.Pagination
{
	public class SimplePaginationDto<T>
	{
		[JsonProperty("total")]
		public int TotalCount { get; set; }

		[JsonProperty("page")]
		public int Page { get; set; }

		[JsonProperty("countPerPage")]
		public int CountPerPage { get; set; }

		[JsonProperty("items")]
		public IEnumerable<T> Items { get; set; }

		[JsonProperty("filter")]
		public string Filter { get; set; }

		public SimplePaginationDto(IEnumerable<T> items, int page, int countPerPage, int total, string filter)
		{
			Items = items;
			Page = page;
			CountPerPage = countPerPage;
			TotalCount = total;
			Filter = filter;
		}
	}
}
