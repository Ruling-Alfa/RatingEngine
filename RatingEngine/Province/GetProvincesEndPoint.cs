using FastEndpoints;
using Infra.ExceptionHandlers.Exceptions;
using RatingEngine.DomainServices.Interfaces;
using RatingEngine.DTO.Province;

namespace RatingEngine.Province
{
    public class GetProvincesEndPoint : EndpointWithoutRequest<List<ProvinceDto>>
    {
        private readonly IProvinceService _provinceService;
        public GetProvincesEndPoint(IProvinceService provinceService)
        {
            _provinceService = provinceService;
        }
        public override void Configure()
        {
            Get("/provinces");
            AllowAnonymous();
        }
        public override async Task<List<ProvinceDto>> ExecuteAsync(CancellationToken ct)
        {
            var provinces = await _provinceService.GetAllProvinces(ct);
            if(provinces is null)
            {
                provinces = new List<ProvinceDto>();
            }
            return provinces;
        }
    }
}
