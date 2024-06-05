using FlexCore.Models.ViewModels.Client.BottomCategory;

namespace FlexCore.Models.ViewModels.Client.MiddleCategory
{
	public class MiddleCategoryVM
	{
		public int? Id { get; set; }
		public string? Name { get; set; }
		public ICollection<BottomCategoryVM>? BottomCategories { get; set; }
	}
}
