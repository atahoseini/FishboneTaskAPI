namespace Fishbone.Core.Entities
{
    public class Order
    {
        public Int64 Id { get; set; }
        public Int64 UserId { get; set; }
        public virtual User? User { get; set; }
        public Int64 ProductId { get; set; }
        public virtual Product? Product { get; set; }
        public int OrderQuantity { get; set; }
        public Decimal OrderTotal { get; set; }
        public DateTime OrderDate { get; set; }
        public string? OrderDescription { get; set; }
    }
}
