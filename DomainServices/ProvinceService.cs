using AutoMapper;
using RatingEngine.DomainServices.Interfaces;
using RatingEngine.DTO.Province;
using RatingEngine.Persistance.Entities;
using RatingEngine.Persistance.Interfaces;

namespace RatingEngine.DomainServices
{
    public class ProvinceService : IProvinceService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public ProvinceService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async ValueTask<List<ProvinceDto>?> GetAllProvinces(CancellationToken ct = default)
        {
            var provinces = await _unitOfWork.Provinces.GetList(ct, p => p.IsActive, q => q.OrderBy(p => p.Id));
            var provinceDto = _mapper.Map<List<ProvinceDto>>(provinces);
            return provinceDto;
        }

        public async ValueTask<ProvinceDto?> GetProvinceById(int provinceId, CancellationToken ct = default)
        {
            var province = await _unitOfWork.Provinces.GetById(provinceId, ct);
            if (province is null)
            {
                return default;
            }
            var provinceDto = _mapper.Map<ProvinceDto>(province);
            return provinceDto;
        }
        public async ValueTask<List<ProvinceDto>?> GetProvinceByAbrivation(string abbreviation, CancellationToken ct = default)
        {
            var provinces = await _unitOfWork.Provinces.GetList(ct,
                p => p.Abbreviation == abbreviation && p.IsActive);
            if (provinces is null || !provinces.Any())
            {
                return default;
            }
            var provincesDto = _mapper.Map<List<ProvinceDto>>(provinces);
            return provincesDto;
        }

        public async ValueTask<ProvinceDto?> AddProvince(ProvinceDto provinceDto, CancellationToken ct = default)
        {

            if (provinceDto is null)
            {
                return default;
            }
            var provinces = _mapper.Map<Province>(provinceDto);
            provinces.IsActive = true;
            await _unitOfWork.Provinces.Add(provinces, ct);
            var isSuccess = _unitOfWork.SaveChanges() > 0;
            if (isSuccess)
            {
                provinceDto.Id = provinces.Id;
            }
            return provinceDto;
        }

        public async ValueTask<bool> DeleteProvinceById(int provinceId, CancellationToken ct = default)
        {
            var isSuccess = false;
            var province = await _unitOfWork.Provinces.GetById(provinceId, ct);
            if (province is not null)
            {
                province.IsActive = false;
                await _unitOfWork.Provinces.Update(province, ct);
                isSuccess = _unitOfWork.SaveChanges() > 0;
            }
            return isSuccess;
        }

        public async ValueTask<ProvinceDto?> UpdateProvince(ProvinceDto provinceDto, CancellationToken ct = default)
        {
            if (provinceDto is null)
            {
                return default;
            }
            var province = _mapper.Map<Province>(provinceDto);
            await _unitOfWork.Provinces.Update(province, ct);

            provinceDto = _mapper.Map<ProvinceDto>(province);
            return provinceDto;
        }
    }
}
