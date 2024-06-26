﻿using System.Text.Json.Serialization;

namespace FlexCore.Models.Entities
{
    public class ProductPictureEntity
	{
        public int Id { get; set; }
        public int ProductColorId { get; set; }
        public string? Url { get; set; }
        [JsonIgnore]
        public ProductColorEntity? ProductColor { get; set; }
    }
}
