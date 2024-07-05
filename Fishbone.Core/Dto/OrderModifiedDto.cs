namespace Fishbone.Core.Dto
{
    public class OrderModifiedDto
    {
        public Int64 Id { get; set; }
        public Int64 UserId { get; set; }
        public Int64 ProductId { get; set; }
        public int OrderQuantity { get; set; }
        public Decimal OrderTotal { get; set; }
        public DateTime OrderDate { get; set; }
        public string? OrderDescription { get; set; }
    }
}
