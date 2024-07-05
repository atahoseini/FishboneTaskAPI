using Fishbone.Core.Entities;

namespace Fishbone.Core.Dto
{
    public class OrderDto
    {
        public Int64 Id { get; set; }
        public Int64 UserId { get; set; }
        // public User User { get; set; }  
        public virtual User? User { get; set; }
        public Int64 ProductId { get; set; }
        // public User User { get; set; }  
        public virtual Product? Product { get; set; }
        public int OrderQuantity { get; set; }
        public Decimal OrderTotal { get; set; }
        public DateTime OrderDate { get; set; }
        public string? OrderDescription { get; set; }
    }
}
