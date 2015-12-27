using SpellingMasterUI.ViewModels;

namespace SpellingMasterCommon.RegionAdapters
{
	public interface ITabItemView
	{
		int Index { get; set; }

		bool IsActive { get; set; }

		ITabViewModel TabViewModel { set; }
	}
}