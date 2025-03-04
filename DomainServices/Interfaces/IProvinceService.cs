using RatingEngine.DTO.Province;

namespace RatingEngine.DomainServices.Interfaces
{
    public interface IProvinceService
    {
        ValueTask<ProvinceDto?> AddProvince(ProvinceDto provinceDto, CancellationToken ct = default);
        ValueTask<bool> DeleteProvinceById(int provinceId, CancellationToken ct = default);
        ValueTask<List<ProvinceDto>?> GetAllProvinces(CancellationToken ct = default);
        ValueTask<List<ProvinceDto>?> GetProvinceByAbrivation(string abbreviation, CancellationToken ct = default);
        ValueTask<ProvinceDto?> GetProvinceById(int provinceId, CancellationToken ct = default);
        ValueTask<ProvinceDto?> UpdateProvince(ProvinceDto provinceDto, CancellationToken ct = default);
    }
}