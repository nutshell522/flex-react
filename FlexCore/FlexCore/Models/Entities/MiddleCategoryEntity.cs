﻿using System.Text.Json.Serialization;

namespace FlexCore.Models.Entities
{
    public class MiddleCategoryEntity
	{
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public int? TopCategoryId { get; set; }

        public virtual TopCategoryEntity? TopCategory { get; set; }

        [JsonIgnore]
        public virtual ICollection<BottomCategoryEntity>? BottomCategories { get; set; }
    }
}
