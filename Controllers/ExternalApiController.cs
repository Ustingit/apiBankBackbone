using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiBankBackBone.Data;
using ApiBankBackBone.Models.Apis;
using ApiBankBackBone.Models.Common.Api;
using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PagedList.Core;

namespace ApiBankBackBone.Controllers
{
	public class ExternalApiController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly IMapper _mapper;

		public ExternalApiController(ApplicationDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		[EnableCors("LocalApi")]
		public async Task<string> GetApis(int? page, int pageSize = 10)
		{
			var pageIndex = (page ?? 1) - 1; //MembershipProvider expects a 0 for the first page

			var apis = await _context.Apis.ToListAsync();
			var apiIds = apis.Select(x => x.Id);
			var methods = (await _context.Methods.Where(x => apiIds.Contains(x.ApiId)).ToListAsync())
				.GroupBy(x => x.ApiId)
				.ToDictionary(x => x.Key, x => x.ToArray());

			var listOfDto = new List<ApiDto>();
			foreach (var api in apis)
			{
				var dto = _mapper.Map<ApiDto>(api);
				if (methods.TryGetValue(dto.Id, out var concreteMethods))
				{
					dto.Methods = concreteMethods;
				}

				listOfDto.Add(dto);
			}

			var apisAsIPagedList = new StaticPagedList<ApiDto>(listOfDto, pageIndex + 1, pageSize, listOfDto.Count);
			var result = ApiResult.SucceedResult<StaticPagedList<ApiDto>>(apisAsIPagedList);

			return JsonConvert.SerializeObject(result);
		}
	}
}
