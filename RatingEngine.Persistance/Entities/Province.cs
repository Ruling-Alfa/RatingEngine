using Infra.Persistance.Entities;

namespace RatingEngine.Persistance.Entities
{
    public record Province : BaseEntity
    {
        public string? ProvinceName { get; set; }
        public string? Abbreviation { get; set; }
    }
}
