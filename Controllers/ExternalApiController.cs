using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiBankBackBone.Data;
using ApiBankBackBone.Helpers;
using ApiBankBackBone.Helpers.Pagination;
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
		public async Task<string> GetApis(int page = 1, int size = 10, string filter = null)
		{
			ApiResult result;

			try
			{
				var paginationData = await GetApisForPagination(page, size, filter);
				var methods = await GetMethods(paginationData.Item1);

				var listOfDto = new List<ApiDto>();
				foreach (var api in paginationData.Item1)
				{
					var dto = _mapper.Map<ApiDto>(api);
					if (methods.TryGetValue(dto.Id, out var concreteMethods))
					{
						dto.Methods = concreteMethods;
					}

					listOfDto.Add(dto);
				}

				var paginationDto = new SimplePaginationDto<ApiDto>(listOfDto, page, size, paginationData.Item2, filter);
				result = ApiResult.SucceedResult<SimplePaginationDto<ApiDto>>(paginationDto);
			}
			catch (Exception e)
			{
				result = ApiResult.ErrorResult("Api fetching error 1", $"{e.Message} - {e.InnerException} - {e.StackTrace}");
			}
			
			return JsonConvert.SerializeObject(result);
		}

		[EnableCors("LocalApi")]
		[HttpDelete]
		public async Task<string> Delete(Guid id)
		{
			ApiResult result;

			try
			{
				var api = await _context.Apis.FindAsync(id);
				_context.Remove(api);
				await _context.SaveChangesAsync();

				var keyValue = new Dictionary<string, Guid> {{"deletedId", id}};
				result = ApiResult.SucceedResult<Dictionary<string, Guid>>(keyValue);
			}
			catch (Exception e)
			{
				result = ApiResult.ErrorResult("Api deleting error", $"{e.Message} - {e.InnerException} - {e.StackTrace}");
			}

			return JsonConvert.SerializeObject(result);
		}

		[EnableCors("LocalApi")]
		[HttpPost]
		public async Task<string> Create()
		{
			ApiResult result;

			try
			{
				using StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8);
				var body = await reader.ReadToEndAsync();

				var api = JsonConvert.DeserializeObject<Api>(body, new JsonSerializerSettings
				{
					NullValueHandling = NullValueHandling.Ignore
				});

				if (api != null)
				{
					api.Id = Guid.NewGuid();
					await _context.Apis.AddAsync(api);
					await _context.SaveChangesAsync();

					result = ApiResult.SucceedResult<Api>(api);
				}
				else
				{
					result = ApiResult.ErrorResult("Api creation error 1", "api is not serialized");
				}
			}
			catch (Exception e)
			{
				result = ApiResult.ErrorResult("Api creation error 2", $"{e.Message} - {e.InnerException} - {e.StackTrace}");
			}

			return JsonConvert.SerializeObject(result);
		}

		[HttpGet]
		public async Task<string> Add10Apis()
		{
			ApiResult result;
			var apis = new List<Api>();

			try
			{
				foreach (var index in Enumerable.Range(1, 11))
				{
					var api = new Api();
					var random = new Random();
					var temp = random.Next(0, 999);

					api.Id = Guid.NewGuid();

					var isOdd = temp % 2 == 0;
					var partOfName = isOdd ? "Odd api, by crazy maniacs." : "Vovel api, beauty is here!";
					api.Name = $"{index} Api. {partOfName}";

					if (isOdd)
					{
						api.IsFree = true;
					}
					else
					{
						api.IsFree = false;
						api.AccessCost = random.Next(1, 10000);
						if (temp % 8 == 0)
						{
							api.MonthlyCost = random.Next(1, 10000);
						}
					}

					api.License = $"APACHIK {index}.0";
					api.AdditionalAccessRules = "do nothing";
					api.Description = StringHelper.RandomString(random.Next(25, 1589));

					await _context.Apis.AddAsync(api);
					await _context.SaveChangesAsync();

					apis.Add(api);
				}

				result = ApiResult.SucceedResult<List<Api>>(apis);
			}
			catch (Exception e)
			{
				result = ApiResult.ErrorResult("Api creation error 2", $"{e.Message} - {e.InnerException} - {e.StackTrace}");
			}

			return JsonConvert.SerializeObject(result);
		}

		private async Task<Tuple<List<Api>, int>> GetApisForPagination(int page, int pageSize, string filter)
		{
			var rawApis = string.IsNullOrEmpty(filter)
				? _context.Apis
				: _context.Apis.Where(x => EF.Functions.Like(x.Name, $"%{filter.ToLower()}%")
				                           || EF.Functions.Like(x.Description, $"%{filter.ToLower()}%")
				                           || EF.Functions.Like(x.License, $"%{filter.ToLower()}%")
				                           || EF.Functions.Like(x.AdditionalAccessRules, $"%{filter.ToLower()}%"));

			var countToSkip = page == 1 ? 0 : ((page - 1) * pageSize);
			var totalCount = await rawApis.CountAsync();
			var apis = await rawApis.Skip(countToSkip).Take(pageSize).ToListAsync();

			return new Tuple<List<Api>, int>(apis, totalCount);
		}

		private async Task<Dictionary<Guid, Method[]>> GetMethods(IEnumerable<Api> apis)
		{
			var apiIds = apis.Select(x => x.Id);

			return (await _context.Methods.Where(x => apiIds.Contains(x.ApiId)).ToListAsync())
				.GroupBy(x => x.ApiId)
				.ToDictionary(x => x.Key, x => x.ToArray());
		}
	}
}
