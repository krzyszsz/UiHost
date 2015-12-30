using System.Collections.Specialized;
using System.Linq;
using System.Windows.Controls;
using Prism.Regions;

namespace CommonLibUi.RegionAdapters
{
	public class TabControlRegionAdapter : RegionAdapterBase<TabControl>
	{
		private TabControl _regionTarget;

		public TabControlRegionAdapter(IRegionBehaviorFactory factory)
			: base(factory)
		{
		}

		protected override void Adapt(IRegion region, TabControl regionTarget)
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

			foreach (ITabItemView tab in e.NewItems.Cast<ITabItemView>().OrderBy(it => it.Index))
			{
				if (tab.Index > items.Count)
					items.Add(tab);
				else
					items.Insert(tab.Index, tab);

				if (tab.IsActive)
				{
					_regionTarget.SelectedItem = tab;
					foreach (var tabItemView in items.SourceCollection.Cast<ITabItemView>()
						.Where(it => it != tab))
					{
						tabItemView.IsActive = false;
					}
				}
			}
		}

		protected override IRegion CreateRegion()
		{
			return new AllActiveRegion();
		}
	}
}
