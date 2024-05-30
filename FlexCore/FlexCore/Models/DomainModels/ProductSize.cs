﻿namespace FlexCore.Models.Entities
{
    public class ProductSize
    {
        public int Id { get; set; }
        public int SizeOptionId { get; set; }
        public int ProductColorId { get; set; }
        public int Stock { get; set; }
        public SizeOption? SizeOption { get; set; }
        public ProductColorEntity? ProductColor { get; set; }
    }
}