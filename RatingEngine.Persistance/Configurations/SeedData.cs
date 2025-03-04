using Microsoft.EntityFrameworkCore;
using RatingEngine.Persistance.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatingEngine.Persistance.Configurations
{
    public class SeedData
    {
        private readonly RatingEngineContext _dbContext;
        private readonly List<Province> _provinces = new List<Province>() {
            new Province(){ Abbreviation = "AB", ProvinceName = "Alberta" },
            new Province(){ Abbreviation = "BC", ProvinceName = "British Columbia" },
            new Province(){ Abbreviation = "MB", ProvinceName = "Manitoba" }
        };
        public SeedData(DbContext dbContext)
        {
            _dbContext = (RatingEngineContext)dbContext;
        }
        public async Task InitSeedDataAsync()
        {
            if (! await _dbContext.Provinces.AnyAsync(x => x.ProvinceName == _provinces[0].ProvinceName))
            {
                await _dbContext.Provinces.AddRangeAsync(_provinces);
                await _dbContext.SaveChangesAsync();
            }
        }

        public void InitSeedData()
        {
            if (! _dbContext.Provinces.Any(x => x.ProvinceName == _provinces[0].ProvinceName))
            {
                _dbContext.Database.EnsureCreated();
                _dbContext.Provinces.AddRange(_provinces);
                _dbContext.SaveChanges();
            }
        }
    }
}
