using ApiBankBackBone.Models.Apis;
using AutoMapper;

namespace ApiBankBackBone.Helpers.Mapping
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<Api, ApiDto>();
		}
	}
}
