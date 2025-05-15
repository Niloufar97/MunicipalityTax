namespace MunicipalityTax.Dtos
{
    public class AddTaxRequestDto
    {
        public DateOnly startDate { get; set; }
        public DateOnly endDate { get; set; }
        public decimal taxRate { get; set; }
        public int MunicipalityId { get; set; }

    }
}
