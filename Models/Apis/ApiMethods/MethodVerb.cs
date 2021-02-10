using System;

namespace ApiBankBackBone.Models.Apis.ApiMethods
{
	[Flags]
	public enum MethodVerb
	{
		
		Get = 1,
		Post = 2,
		Put = 4,
		Delete = 8,
		Head = 16,
		Patch = 32,
		Options = 64
	}
}
