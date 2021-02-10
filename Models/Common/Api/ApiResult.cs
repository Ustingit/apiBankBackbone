using System;
using Newtonsoft.Json;

namespace ApiBankBackBone.Models.Common.Api
{
	[Serializable]
	public class ApiResult
	{
		[JsonProperty("succeed")]
		public bool Succeed { get; set; }

		[JsonProperty("data")]
		public object Data { get; set; }

		[JsonProperty("errorCode")]
		public string ErrorCode { get; set; }

		[JsonProperty("errorDescription")]
		public string ErrorDescription { get; set; }

		public static ApiResult SucceedResult<T>(object data)
		{
			T result = (T) Convert.ChangeType(data, typeof(T));

			return new ApiResult() { Succeed = true, Data = result };
		}

		public static ApiResult ErrorResult(string errorCode, string errorDescription)
		{
			return new ApiResult() { Succeed = false, ErrorCode = errorCode, ErrorDescription = errorDescription };
		}
	}
}
