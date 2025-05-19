namespace MunicipalityTax.Dtos
{
    /// <summary>
    /// Data Transfer Object used for creating a new tax record for a municipality.
    /// </summary>
    public class AddTaxRequestDto
    {
        public required DateOnly startDate { get; set; }
        public required DateOnly endDate { get; set; }
        public required decimal taxRate { get; set; }
        public required int MunicipalityId { get; set; }

    }
}
