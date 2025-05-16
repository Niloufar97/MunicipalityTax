namespace MunicipalityTax.Dtos
{
    public class AddTaxRequestDto
    {
        public required DateOnly startDate { get; set; }
        public required DateOnly endDate { get; set; }
        public required decimal taxRate { get; set; }
        public required int MunicipalityId { get; set; }

    }
}
