namespace MunicipalityTax.Models
{
    public class Tax
    {
        public int Id { get; set; }
        public DateOnly startDate { get; set; }
        public DateOnly endDate { get; set; }
        public decimal taxRate { get; set; }

        public int MunicipalityId { get; set; }
        public Municipality Municipality { get; set; }
    }
}
