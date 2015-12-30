using System.Collections.Specialized;
using System.Linq;
using System.Windows.Controls;
using Microsoft.Practices.Prism.Commands;
using Prism.Regions;

namespace CommonLibUi.RegionAdapters
{
	public class MenuItemRegionAdapter : RegionAdapterBase<MenuItem>
	{
		private MenuItem _regionTarget;

		public MenuItemRegionAdapter(IRegionBehaviorFactory factory)
			: base(factory)
		{
		}

		protected override void Adapt(IRegion region, MenuItem regionTarget)
		{
			_regionTarget = regionTarget;
			region.Views.CollectionChanged -= ItemsCollectionChanged;
			region.Views.CollectionChanged += ItemsCollectionChanged;
		}

		private void ItemsCollectionChanged(object s, NotifyCollectionChangedEventArgs e)
		{
			if (e.Action != NotifyCollectionChangedAction.Add)
				return;
			var items = _regionTarget.Items;
			var initialNumberOfItems = items.Count;

			foreach (MenuItem tab in e.NewItems)
			{
				items.Insert(items.Count - initialNumberOfItems, tab);
			}
		}

		protected override IRegion CreateRegion()
		{
			return new AllActiveRegion();
		}
	}
}