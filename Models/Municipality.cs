namespace MunicipalityTax.Models
{
    public class Municipality
    {
        public int Id { get; set; }
        public string MunicipalityName { get; set; }
        public ICollection<Tax> Taxes { get; set; }

    }
}
