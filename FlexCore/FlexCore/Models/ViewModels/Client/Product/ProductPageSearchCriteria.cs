namespace FlexCore.Models.ViewModels.ClientViewModels.Product
{
	public class ProductPageSearchCriteria
	{
		public Pageable Pageable { get; set; }
		public int? TopCategoryId { get; set; }
		public int? MiddleCategoryId { get; set; }
		public int? BottomCategoryId { get; set; }
		public int? MaxPrice { get; set; }
		public int? MinPrice { get; set; }
	}
}
