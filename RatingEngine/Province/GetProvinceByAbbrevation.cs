using FastEndpoints;
using Infra.ExceptionHandlers.Exceptions;
using RatingEngine.DomainServices.Interfaces;
using RatingEngine.DTO.Province;

namespace RatingEngine.Province
{
    public class GetProvinceByAbbrevation : EndpointWithoutRequest<List<ProvinceDto>>
    {
        private readonly IProvinceService _provinceService;
        public GetProvinceByAbbrevation(IProvinceService provinceService)
        {
            _provinceService = provinceService;
        }
        public override void Configure()
        {
            Get("/provinces/{abbrevation}");
            AllowAnonymous();
        }

        public override async Task<List<ProvinceDto>> ExecuteAsync(CancellationToken ct)
        {
            var abbrevation = Route<string>("abbrevation");
            if(string.IsNullOrWhiteSpace(abbrevation))
            {
                throw new BadRequestException("Province Abbrevation is required");
            }
            var provinces = await _provinceService.GetProvinceByAbrivation(abbrevation, ct);
            if(provinces is null)
            {
                //provinces = new List<ProvinceDto>();
                throw new NotFoundException("No provinces found");
            }
            return provinces;
        }
    }
}
