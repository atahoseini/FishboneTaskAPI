﻿namespace Fishbone.Core.Dto
{
    public class ProductDto
    {
        public Int64 Id { get; set; }
        public string? ProductName { get; set; }
        public string? ProductDescription { get; set; }
        public decimal ProductPrice { get; set; }
        public decimal ProductStock { get; set; }
    }
}
