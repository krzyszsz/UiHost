namespace CommonLibUi.RegionAdapters
{
	public interface ITabItemView
	{
		int Index { get; set; }

		bool IsActive { get; set; }

		ITabViewModel TabViewModel { set; }
	}
}