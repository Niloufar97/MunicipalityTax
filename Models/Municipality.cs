using System.Text.Json.Serialization;

namespace MunicipalityTax.Models
{
    public class Municipality
    {
        public int Id { get; set; }
        public string MunicipalityName { get; set; }

        [JsonIgnore]
        public ICollection<Tax> Taxes { get; set; } = new List<Tax>();

    }
}
