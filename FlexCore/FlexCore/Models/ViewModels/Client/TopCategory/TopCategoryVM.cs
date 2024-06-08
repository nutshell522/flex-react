using FlexCore.Models.ViewModels.Client.MiddleCategory;

namespace FlexCore.Models.ViewModels.Client.TopCategory
{
	public class TopCategoryVM
	{
		public int? Id { get; set; }
		public string? Name { get; set; }
		public string? Code { get; set; }

		public ICollection<MiddleCategoryVM>? MiddleCategories { get; set; }
	}
}
